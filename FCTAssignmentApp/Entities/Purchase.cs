using FCTAssignmentApp.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace FCTAssignmentApp.Models
{
    public class Purchase
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public virtual Customer User { get; set; }
        public virtual Product Product { get; set; }
        public Status OrderStatus { get; set; }
    }
}
