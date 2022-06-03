using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Labb4MCVKimWiklund.Models
{
    public class Book
    {
        public int BookId { get; set; }
        public int Pages { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 3)]
        public string Name { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 3)]
        public string Description { get; set; }

        public ICollection<BookLoan> BookLoans { get; set; }

    }
}
