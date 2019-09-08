using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SIS.Models
{
    public class PPG
    {
        [Key]
        //[DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ID { get; set; }

        [Display(Name = "Nopes PPG")]
        [Required(ErrorMessage = "{0} tidak boleh kosong !")]
        [StringLength(100, ErrorMessage = "{0} tidak boleh lebih dari {1} angka !")]
        public string No_PPG { get; set; }

        [Display(Name = "No Ujian")]
        [Required(ErrorMessage = "{0} tidak boleh kosong !")]
        [StringLength(100, ErrorMessage = "{0} tidak boleh lebih dari {1} angka !")]
        public string No_Ujian { get; set; }

        [Display(Name = "Nama")]
        [Required(ErrorMessage = "{0} tidak boleh kosong !")]
        [StringLength(100, ErrorMessage = "{0} tidak boleh lebih dari {1} karakter !")]
        public string Nama { get; set; }


        [Display(Name = "Lokasi PPG")]
        [StringLength(100, ErrorMessage = "{0} tidak boleh lebih dari {1} karakter !")]
        public string Lokasi { get; set; }


        [Display(Name = "Mapel")]
        [StringLength(100, ErrorMessage = "{0} tidak boleh lebih dari {1} karakter !")]
        public string Mapel { get; set; }

        [Display(Name = "Stat Gabungan")]
        [StringLength(50, ErrorMessage = "{0} tidak boleh lebih dari {1} karakter !")]
        public string Status { get; set; }
    }
}
