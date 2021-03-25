using System;

namespace LKXM.FCDTwin.Dto
{
    public class PageReqDto<T> where T : class, new()
    {
        public T parames { get; set; }

        public PageInfoDto page_info { get; set; }

        public PageReqDto()
        {
            parames = new T();
            page_info = new PageInfoDto();
        }
    }
}
