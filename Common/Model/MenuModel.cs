using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mor.Common
{
    [Serializable]
    public class MenuModel
    {
        /// <summary>
        /// 菜单编号
        /// </summary>
        public string menu_id { get; set; }
        /// <summary>
        /// 父级编号
        /// </summary>
        public string menu_pid { get; set; }
        /// <summary>
        /// 菜单名称
        /// </summary>
        public string menu_name { get; set; }
        /// <summary>
        /// 菜单图标
        /// <para>font-awesome</para>
        /// </summary>
        public string menu_icon { get; set; }
        /// <summary>
        /// URL地址
        /// </summary>
        public string menu_url { get; set; }
        /// <summary>
        /// 描述
        /// </summary>
        public string menu_remark { get; set; }


    }
}
