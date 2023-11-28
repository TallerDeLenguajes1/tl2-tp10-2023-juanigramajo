using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using tl2_tp10_2023_juanigramajo.Models;

namespace tl2_tp10_2023_juanigramajo.ViewModels.Usuario
{
    public class ModificarUsuarioViewModel
    {
        [Required(ErrorMessage = "Este campo es requerido.")]
        [Display(Name = "Nombre de Usuario")]
        public string NombreDeUsuario { get; set; }

        [Required(ErrorMessage = "Este campo es requerido.")] 
        [PasswordPropertyText]
        [Display(Name = "Contraseña")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Este campo es requerido.")]
        [Display(Name = "Rol")]
        public Rol RolDelUsuario { get; set; }



        public ModificarUsuarioViewModel(){            

        }
        
        public ModificarUsuarioViewModel(Usuario usuario){
            NombreDeUsuario = usuario.NombreDeUsuario;
            Password = usuario.Password;
            RolDelUsuario = usuario.RolDelUsuario;
        }
    }
}