using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace LKXM.FCDTwin.Entity
{
    /// <summary>
    /// 测试表
    /// </summary>
    [Table("t_test")]
    public class TTest : EntityRoot
    {
        /// <summary>
        /// 名称
        /// </summary>
        [Description("名称")]
        public string name { get; set; }

        /// <summary>
        /// 年龄
        /// </summary>
        [Description("年龄")]
        public int age { get; set; }
    }
}
