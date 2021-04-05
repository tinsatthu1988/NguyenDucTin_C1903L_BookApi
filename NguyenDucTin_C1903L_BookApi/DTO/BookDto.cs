using NguyenDucTin_C1903L_BookApi.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NguyenDucTin_C1903L_BookApi.DTO
{
    public class BookDto
    {
        public int Id { get; set; }
        public string ISBN { get; set; }
        public string CategoryName { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public string Description { get; set; }
        public int Price { get; set; }
        public DateTime DatePublished { get; set; } = DateTime.Now;
        public string PhotoUrl { get; set; }
        public ICollection<PhotoBookDto> Photos { get; set; }
    }
}
