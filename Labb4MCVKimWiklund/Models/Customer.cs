using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Labb4MCVKimWiklund.Models
{
    public class Customer
    {
        [Key]
        public int CustomerId { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 3)]
        public string CustomerName { get; set; }

        public int Phone { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 3)]
        public string Email { get; set; }

        public ICollection<BookLoan> BookLoans { get; set; }
    }
}
