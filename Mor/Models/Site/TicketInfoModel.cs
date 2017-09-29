using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Mor.Models.Site
{
    public class TicketInfoModel
    {
        /// <summary>
        /// Id
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// 参会门票ID
        /// </summary>
        public int oId { get; set; }
        /// <summary>
        /// 姓名
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 身份证
        /// </summary>
        public string IdCard { get; set; }
        /// <summary>
        /// 联系电话
        /// </summary>
        public string Phone { get; set; }
        /// <summary>
        /// 联系邮箱
        /// </summary>
        public string Email { get; set; }
        /// <summary>
        /// 职位(1企业高管;2架构师;3开发/测试;4运维;5运营;6产品;7设计;8市场;9销售;10商务合作;11学生;12其它;)
        /// </summary>
        public int Job { get; set; }
        /// <summary>
        /// 参会时间(多个，以,号分割。例：2000-1-1，2011-8-7)
        /// </summary>
        public string MeetingDateStr { get; set; }
        /// <summary>
        /// 公司名称
        /// </summary>
        public string CorporateName { get; set; }
        /// <summary>
        /// 所属行业(1互联网;2游戏;3物联网;4医疗健康;5汽车;6房地产;7媒体/娱乐;8音/视频;9农林牧渔)
        /// </summary>
        public int Industry { get; set; }
        /// <summary>
        /// 公司规模(1、1-49人;2、50-99人;3、100-499人;4、500-3499人;5、3500-9999人;6、10000人及以上;)
        /// </summary>
        public int CompanySize { get; set; }
        /// <summary>
        /// 第三方单号
        /// </summary>
        public string ThirdOrderNumber { get; set; }
        /// <summary>
        /// 已支付(0未支付1支付)
        /// </summary>
        public int IsPay { get; set; }
        /// <summary>
        /// 支付类型：1微信2支付宝
        /// </summary>
        public int PayType { get; set; }
        /// <summary>
        /// 订单创建时间
        /// </summary>
        public DateTime CreateDate { get; set; }
        /// <summary>
        /// 删除
        /// </summary>
        public int Delete { get; set; }
    }
    public class AddTicketInfoModel
    {
        public AddTicketInfoModel()
        {
            ticketlist = new List<UserTicketInfo>();
        }
        public List<UserTicketInfo> ticketlist { get; set; }
        /// <summary>
        /// 公司名称
        /// </summary>
        public string CorporateName { get; set; }
        /// <summary>
        /// 所属行业(1互联网;2游戏;3物联网;4医疗健康;5汽车;6房地产;7媒体/娱乐;8音/视频;9农林牧渔)
        /// </summary>
        public int Industry { get; set; }
        /// <summary>
        /// 公司规模(1、1-49人;2、50-99人;3、100-499人;4、500-3499人;5、3500-9999人;6、10000人及以上;)
        /// </summary>
        public int CompanySize { get; set; }
    }

    public class UserTicketInfo
    {
        /// <summary>
        /// 参会门票ID
        /// </summary>
        public int oId { get; set; }
        /// <summary>
        /// 姓名
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 身份证
        /// </summary>
        public string IdCard { get; set; }
        /// <summary>
        /// 联系电话
        /// </summary>
        public string Phone { get; set; }
        /// <summary>
        /// 联系邮箱
        /// </summary>
        public string Email { get; set; }
        /// <summary>
        /// 职位(1企业高管;2架构师;3开发/测试;4运维;5运营;6产品;7设计;8市场;9销售;10商务合作;11学生;12其它;)
        /// </summary>
        public int Job { get; set; }
        /// <summary>
        /// 参会时间(多个，以,号分割。例：2000-1-1，2011-8-7)
        /// </summary>
        public string MeetingDateStr { get; set; }
    }
}