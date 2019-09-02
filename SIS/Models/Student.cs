using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SIS.Models
{
    public class Student
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Display(Name = "NIS")]
        [StringLength(20, ErrorMessage = "{0} no more than {1} characters !")]
        public string NIS { get; set; }

        [Display(Name = "Name")]
        [StringLength(50, ErrorMessage = "{0} no more than {1} characters !")]
        public string Name { get; set; }

        [Display(Name = "Birth of Date")]
        //[DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MMM-yyyy}")]
        public DateTime? BoD { get; set; }

        [Display(Name = "Gender")]
        [StringLength(5, ErrorMessage = "{0} no more than {1} characters !")]
        public string Gender { get; set; }

        [Display(Name = "Religion")]
        [StringLength(20, ErrorMessage = "{0} no more than {1} characters !")]
        public string Religion { get; set; }

        [ForeignKey("major")]
        [Display(Name = "Major")]
        [Required(ErrorMessage = "{0} is RequiredAttribute !")]
        public int MajorID { get; set; }

        public Major Major { get; set; }

        [Display(Name = "Address")]
        [StringLength(250, ErrorMessage = "{0} no more than {1} characters !")]
        public string Address { get; set; }
    }
}
