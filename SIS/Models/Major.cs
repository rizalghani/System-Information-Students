using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SIS.Models
{
    public class Major
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "ID")]
        public int ID { get; set; }

        [Display(Name = "Major Name")]
        [StringLength(50, ErrorMessage = "{0} no more than {1} characters !")]
        public string MajorName { get; set; }

        [Display(Name = "Major Code")]
        [StringLength(10, ErrorMessage = "{0} no more than {1} characters !")]
        public string MajorCode { get; set; }

        public ICollection<Student> Students { get; set; }
    }
}
