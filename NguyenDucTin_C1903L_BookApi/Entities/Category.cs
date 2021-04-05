using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace NguyenDucTin_C1903L_BookApi.Entities
{
    [Table("Categories")]
    public class Category
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public ICollection<Book> Books { get; set; }
    }
}
