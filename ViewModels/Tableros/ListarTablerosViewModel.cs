using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using tl2_tp10_2023_juanigramajo.Models;


namespace tl2_tp10_2023_juanigramajo.ViewModels.Tableros
{
    public class ListarTablerosViewModel
    {
        public List<Tablero> ListadoTableros { get; set; }
        public List<Tablero> ListadoMisTableros { get; set; }


        public ListarTablerosViewModel(){
            
        }

        public ListarTablerosViewModel(List<Tablero> tableros, List<Tablero> misTableros){
            this.ListadoTableros = tableros;
            this.ListadoMisTableros = misTableros;
        }
    }
}