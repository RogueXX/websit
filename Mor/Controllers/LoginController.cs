using Mor.Common;
using Mor.DataAccess;
using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Web.Mvc;
using System.Data.Common;

namespace Mor.Web.Controllers
{

    public class LoginController : __BaseController
    {

        //
        // GET: /Login/
        public ActionResult Index(string act)
        {
            if (act == "logout")
            {
                UserEnvironment.UserInfo = null;
                UserEnvironment.AdminMenus = null;
            }

            ViewBag.ErrorCount = SessionHelper.GetInt32("LOGIN_ERROR_COUNT");

            return View();
        }

        [HttpPost]
        public ActionResult Index(string user_code, string user_pwd,
            int? remember, string verify_code, string reurl)
        {
            // 错误总次数
            var cnt = SessionHelper.GetInt32("LOGIN_ERROR_COUNT");

            try
            {
                if (cnt > 3)  // 登录次数超3次需要输入验证码
                {
                    string vCode = SessionHelper.GetString("VerifyCode");
                    if (!string.IsNullOrEmpty(vCode) && String.Compare(verify_code, vCode, true) != 0)
                        throw new Exception("验证码错误");

                }
                // encrypt to md5
                user_pwd = CommonHelper.MD5Encrypt(user_pwd);

                using (var db = new SqlHelper())
                {

                    // get single
                    var user = db.Single(@"select status,username,gid,a.id,pid,company,isnull(isAgency,0)as isAgency,isnull(isCompany,0)as isCompany
                                from weixin_users a
                                where username=@p0 and password=@p1", user_code, user_pwd); ;

                    // check password
                    if (user == null) throw new Exception("用户名不存在或密码错误！");

                    // check user state
                    if (user.GetInt16("status") != 1)
                    {
                        LogHelper.login("用户账号" + user_code + "登录失败；原因是：用户账号未启用或已停用！");
                        throw new Exception("用户账号" + user_code + "未启用或已停用！请与管理员联系！");
                    }

                    // 用户信息赋值
                    UserEnvironment.UserInfo = new UserInfoModel
                    {
                        UserId = user.GetInt32("id"), 
                        UserName = user.GetString("username") 
                    };

                    LogHelper.login(user_code, "用户账号:" + user_code + "登录成功！");
                    var tokens = new DbArray();
                    var menus = new Dictionary<string, List<MenuModel>>();
                    var sql1 = @"select token from weixin_wxuser a where uid=@p0 ";
 

//                    // ### 获取菜单权限
//                    foreach (var obj in tokens)
//                    {
//                        string token = obj.GetString("token")
//                            , sql = @"select a.menu_id,menu_name,menu_pid,menu_url,menu_icon,menu_remark,menu_order 
//                                from weixin_menu a  inner join  weixin_usergroup_menu b on a.menu_id=b.menu_id inner join weixin_users c on c.gid=b.u_id and c.id=@p0 where menu_state=1";

//                        // ### 子账号受菜单权限控制
//                        if (UserEnvironment.UserInfo.ParentId > 0)
//                        {
//                            // 查询有效的角色
//                            var roles = db.Query(@"select distinct b.role_id,b.role_all_menu  from weixin_role_user a right join weixin_role b on a.role_id=b.role_id 
//                            where b.token=@p0 and (b.role_all_user=1 or a.user_id=@p1)", token, user["id"]);

//                            // 有菜单权限控制
//                            if (!roles.Exists(m => m.GetInt16("role_all_menu") == 1))
//                            {
//                                sql += " and a.menu_id in (select menu_id from weixin_role_menu where role_id in (";
//                                sql += roles.Count == 0 ? "0" : string.Join(",", roles.ConvertAll<string>(m => m.GetString("role_id")));
//                                sql += ")) or menu_pid is null";
//                            }
//                        }
//                        //商户父账号登录菜单权限
//                        //else
//                        //{
//                        //    sql += " inner join  weixin_usergroup_menu b on a.menu_id=b.menu_id inner join weixin_users c on c.gid=b.u_id and c.id=@p0 where menu_state=1";
                                
//                        //}
//                        var uid = user.GetInt32("pid") > 0 ? user.GetInt32("pid") : user.GetInt32("id");
//                        // ## 缓存菜单权限 
//                        var list = db.Query<MenuModel>(sql + " order by a.menu_pid,menu_order", uid);
//                        menus.Add(token, list);
//                    }

                    UserEnvironment.AdminMenus = menus;

                }


                // 清除错误数
                SessionHelper.Set("LOGIN_ERROR_COUNT", 0);

                // 跳转
                if (!string.IsNullOrEmpty(reurl))
                    return Redirect(reurl);

                return RedirectToAction("index", "home");
            }
            catch (Exception ex)
            {

                ViewBag.ErrorCount = ++cnt;
                ViewBag.ErrorMessage = ex.Message;

                SessionHelper.Set("LOGIN_ERROR_COUNT", cnt);

                return View();
            }
        }

        public ActionResult GetPassword() { return View(); }

        [HttpPost]
        public ActionResult GetPassword(string email)
        {
            using (var db = new SqlHelper())
            {
                var obj = db.Single("select username,password from weixin_users where email=@p0", email);
                if (obj == null)
                {
                    ViewBag.ErrorMessage = "邮箱地址输入错误：该邮箱未被注册使用。";
                }
                else
                {
                    // todo: send mail;
                    //var username = ((Hashtable)ls[0])["username"].ToString();
                    //var password = ((Hashtable)ls[0])["password"].ToString();

                    return Content("<p>用户名：“" + obj.GetString("username") + "”</p>");

                }
                return View();
            }
        }

        //[HttpGet]
        //public ActionResult VerifyCode()
        //{

        //    string veryCode;

        //    var stream = VerifyCodeHelper.Create(out veryCode);
        //    SessionHelper.Set("VerifyCode", veryCode);  //set to session

        //    return File(stream.ToArray(), "image/png");
        //}



    }
}