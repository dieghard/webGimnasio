using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel;

namespace webGimnasio.Models
{
    public class Pagos
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int Id { get; set; }

        [Required]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        [DataType(DataType.Date)]
        public DateTime Fecha { get; set; }

        [Required]
        [DisplayName("Alumno")]
        public string AlumnoID { get; set; }
        public ApplicationUser Alumno { get; set; }
        public string Observaciones { get; set; }
        [Required]
        [DataType(DataType.Currency)]
        [Range(0, double.MaxValue, ErrorMessage = "El importe debe ser un numero mayor a cero")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:0.00}")]
        public double Importe { get; set; }

    }
}
