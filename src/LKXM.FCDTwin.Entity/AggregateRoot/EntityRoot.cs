using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LKXM.FCDTwin.Entity
{
    /// <summary>
    /// 聚合根
    /// </summary>
    public abstract class EntityRoot
    {
        /// <summary>
        /// 主键
        /// </summary>
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Browsable(false)]
        [Description("主键")]
        public long id { get; set; }

        /// <summary>
        /// 创建用户Id
        /// </summary>
        [Description("创建用户Id")]
        public long create_uid { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        [Description("创建时间")]
        public DateTime create_at { get; set; }

        /// <summary>
        /// 更新用户Id
        /// </summary>
        [Description("更新用户Id")]
        public long update_uid { get; set; }

        /// <summary>
        /// 更新时间
        /// </summary>
        [Description("更新时间")]
        public DateTime update_at { get; set; }

        /// <summary>
        /// 数据状态：0 正常 1 删除
        /// </summary>
        [Description("数据状态：0 正常 1 删除")]
        public int normal { get; set; }

        public int version { get; set; }

        public EntityRoot()
        {
            create_at = DateTime.Now;
            update_at = create_at;
            normal = 0;
            version = 0;
        }
    }
}
