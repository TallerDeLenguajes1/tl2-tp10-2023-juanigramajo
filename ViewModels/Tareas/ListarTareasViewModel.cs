using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using tl2_tp10_2023_juanigramajo.Models;


namespace tl2_tp10_2023_juanigramajo.ViewModels.Tareas
{
    public class ListarTareasViewModel
    {
        public List<Tarea> ListadoTareas { get; set; }


        public ListarTareasViewModel(){
            
        }

        public ListarTareasViewModel(List<Tarea> tareas){
            this.ListadoTareas = tareas;
        }
    }
}