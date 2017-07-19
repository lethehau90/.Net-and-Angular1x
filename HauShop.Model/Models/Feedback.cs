using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HauShop.Model.Models
{
    [Table("Feedbacks")]
    public class Feedback
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { set; get; }
        [MaxLength(250)]
        [Required]
        public string Name { set; get; }
        [MaxLength(250)]
        public string   Email { set; get; }
        [MaxLength(500)]
        public string Message { set; get; }
        public DateTime CreateDate { set; get; }
        [Required]
        public bool Status { set; get; }
    }
}
