using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Dto
{
    public class customerDto
    {
        public int? CustomerId { get; set; }
        public string CustomerCode { get; set; } = null!;
        public string? CustomerDescA { get; set; }
        public string? CustomerDescE { get; set; }
        public bool? IsActive { get; set; }
        public string? Email { get; set; }
        public string? Remarks { get; set; }

    }
}
