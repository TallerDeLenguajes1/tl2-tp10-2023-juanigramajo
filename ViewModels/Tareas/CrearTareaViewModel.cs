using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using tl2_tp10_2023_juanigramajo.Models;


namespace tl2_tp10_2023_juanigramajo.ViewModels.Tareas
{
    public class CrearTareaViewModel
    {
        [Required(ErrorMessage = "Este campo es requerido.")]
        [Display(Name = "Id del tablero")]
        public int IdTablero { get; set; }

        [Required(ErrorMessage = "Este campo es requerido.")]
        [Display(Name = "Nombre")] 
        public string Nombre { get; set; }

        [Display(Name = "Descripción")] 
        public string Descripcion { get; set; }

        [Display(Name = "Color")] 
        public string Color { get; set; }

        [Required(ErrorMessage = "Este campo es requerido.")]
        [Display(Name = "Estado")] 
        public EstadoTarea Estado { get; set; }        

        [Display(Name = "Id del usuario asignado")] 
        public int IdUsuarioAsignado { get; set; }


        public CrearTareaViewModel(){

        }

        public CrearTareaViewModel(Tarea tarea){
            this.IdTablero = tarea.IdTablero;
            this.Nombre = tarea.Nombre;
            this.Descripcion = tarea.Descripcion;
            this.Color = tarea.Color;
            this.Estado = tarea.Estado;
            this.IdUsuarioAsignado = tarea.IdUsuarioAsignado;
        }
    }
}