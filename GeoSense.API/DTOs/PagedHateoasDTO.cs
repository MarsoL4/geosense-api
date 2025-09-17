using System.Collections.Generic;

namespace GeoSense.API.DTOs
{
    public class PagedHateoasDTO<T>
    {
        public IEnumerable<T> Items { get; set; }
        public int TotalCount { get; set; }
        public int Page { get; set; }
        public int PageSize { get; set; }
        public List<LinkDTO> Links { get; set; }
    }

    public class LinkDTO
    {
        public string Rel { get; set; }      // auto, next, prev, self, etc.
        public string Method { get; set; }   // GET, POST, PUT, DELETE
        public string Href { get; set; }     // URL
    }
}