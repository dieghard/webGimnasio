using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace webGimnasio.Models
{
    public class ApplicationUser: IdentityUser
    {
        [DisplayName("Nombre y apellido")]
        public string NombreApellido { get; set; }
        public string Telefono { get; set; }
        
        [NotMapped]
        [DisplayName("Rol")]
        public string RolID { get; set; }

        public virtual ICollection<IdentityUserRole<string>> Roles { get; set; }
    }
}
