using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using tl2_tp10_2023_juanigramajo.Models;


namespace tl2_tp10_2023_juanigramajo.ViewModels.Tableros
{
    public class ModificarTableroViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Este campo es requerido.")]
        [Display(Name = "Id del usuario propietario")] 
        public int IdUsuarioPropietario { get; set; }

        [Required(ErrorMessage = "Este campo es requerido.")]
        [Display(Name = "Nombre del tablero")] 
        public string Nombre { get; set; }

        [Display(Name = "Descripción")] 
        public string Descripcion { get; set; }


        public ModificarTableroViewModel(){
        
        }

        public ModificarTableroViewModel(Tablero tablero){
            this.IdUsuarioPropietario = tablero.IdUsuarioPropietario;
            this.Nombre = tablero.Nombre;
            this.Descripcion = tablero.Descripcion;
        }
    }
}