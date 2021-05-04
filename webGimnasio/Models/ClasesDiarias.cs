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
    public class ClasesDiarias
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "Debe completar el Profesor")]
        [DisplayName("Profesor")]
        public string ProfesorID { get; set; }
        public ApplicationUser Profesor { get; set; }
        [Required]
        [DisplayName("Alumno")]
        public string AlumnoID { get; set; }
        public ApplicationUser Alumno { get; set; }
        [Required]
        [DisplayName("Dia y Hora")]
        public DateTime DiaHora { get; set; }
        [MaxLength(200, ErrorMessage = "No puede tener mas de 200 caracteres")]
        public string Observaciones { get; set; }
        [Required]
        [DisplayName("Clase")]
        public int ClaseID { get; set; }
        public Clase Clase { get; set; }

    }
}
