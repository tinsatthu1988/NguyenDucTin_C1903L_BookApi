using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NguyenDucTin_C1903L_BookApi.DTO
{
    public class MemberUpdateDto
    {
        public string StreetAddress { get; set; }
        public string Ward { get; set; }
        public string District { get; set; }
        public string City { get; set; }
    }
}
