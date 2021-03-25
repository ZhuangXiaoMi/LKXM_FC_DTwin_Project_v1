using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace LKXM.FCDTwin.Dto
{
    public class PageInfoDto
    {
        /// <summary>
        /// 页码
        /// </summary>
        [Description("页码")]
        [Display(Name = "页码")]
        [Required(ErrorMessage = "{0}必填")]
        public int page_index { get; set; }

        /// <summary>
        /// 页数
        /// </summary>
        [Description("页数")]
        [Display(Name = "页数")]
        [Required(ErrorMessage = "{0}必填")]
        public int page_size { get; set; }

        public PageInfoDto() : this(1, 20) { }

        public PageInfoDto(int page_index, int page_size)
        {
            this.page_index = page_index;
            this.page_size = page_size;
        }
    }
}
