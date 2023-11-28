using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using tl2_tp10_2023_juanigramajo.Models;


namespace tl2_tp10_2023_juanigramajo.ViewModels.Usuarios
{
    public class CrearUsuarioViewModel
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


        public CrearUsuarioViewModel(){            

        }
        
        public CrearUsuarioViewModel(Usuario usuario){
            this.NombreDeUsuario = usuario.NombreDeUsuario;
            this.Password = usuario.Password;
            this.RolDelUsuario = usuario.RolDelUsuario;
        }
    }
}