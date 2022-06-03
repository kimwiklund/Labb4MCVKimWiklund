using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Labb4MCVKimWiklund.Models
{
    public class BookLoan
    {
        [Key]
        public int LoanId { get; set; }

        [ForeignKey ("Customers")]
        public int fkCustomerId { get; set; }
        public Customer Customers { get; set; }

        [ForeignKey("Books")]
        public int fkBookId { get; set; }
        public Book Books { get; set; }

        public DateTime LoanDate { get; set; }
        public DateTime ReturnDate { get; set; }


    }
}
