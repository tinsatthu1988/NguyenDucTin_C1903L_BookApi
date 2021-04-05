using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NguyenDucTin_C1903L_BookApi.DTO
{
    public class BookUpsertDto
    {
        public string ISBN { get; set; }
        public string CategoryId { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public string Description { get; set; }
        public int Price { get; set; }
    }
}
