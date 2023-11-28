using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using tl2_tp10_2023_juanigramajo.Models;


namespace tl2_tp10_2023_juanigramajo.ViewModels.Tableros
{
    public class ListarTablerosViewModel
    {
        private List<Tablero> ListadoTableros { get; set; }


        public ListarTablerosViewModel(){
            
        }

        public ListarTablerosViewModel(List<Tablero> tableros){
            this.ListadoTableros = tableros;
        }
    }
}