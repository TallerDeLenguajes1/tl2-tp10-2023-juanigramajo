using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using tl2_tp10_2023_juanigramajo.Models;


namespace tl2_tp10_2023_juanigramajo.ViewModels.Tableros
{
    public class ListarTablerosViewModel
    {
        public List<Tablero> ListadoTableros { get; set; }
        public List<Tablero> ListadoMisTableros { get; set; }

        public List<Tablero> ListadoTareasEnOtroTablero { get; set; }
        public string IdUsuarioPropietario { get; set; }


        public ListarTablerosViewModel(){
            
        }

        public ListarTablerosViewModel(List<Tablero> tableros, List<Tablero> misTableros, List<Tablero> tareasEnOtroTablero, string idUser){
            this.ListadoTableros = tableros;
            this.ListadoMisTableros = misTableros;
            this.ListadoTareasEnOtroTablero = tareasEnOtroTablero;
            this.IdUsuarioPropietario = idUser;
        }
    }
}