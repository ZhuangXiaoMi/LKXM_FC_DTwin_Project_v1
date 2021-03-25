using System.ComponentModel;

namespace LKXM.FCDTwin.Dto
{
    public class PageResDto<T> : ResultResDto<T> where T : class, new()
    {
        /// <summary>
        /// 总条数
        /// </summary>
        [Description("总条数")]
        public int total { get; set; }

        public PageResDto()
        {
            data = new T();
        }
    }
}
