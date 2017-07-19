using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HauShop.Model.Models
{
    [Table("Logos")]
    public class Logo
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { set; get; }
        [MaxLength(50)]
        public string Name { set; get; }
        public string Image { set; get; }
        public string Code { set; get; }
    }
}
