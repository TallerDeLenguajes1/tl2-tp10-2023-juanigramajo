using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using tl2_tp10_2023_juanigramajo.Models;
using tl2_tp10_2023_juanigramajo.ViewModels;
using tl2_tp10_2023_juanigramajo.ViewModels.Tableros;

namespace tl2_tp10_2023_juanigramajo.ViewModels.Usuarios
{
    public class HerramientasUsuariosViewModel
    {
        public CrearUsuarioViewModel CrearUsuarioVM { get; set; }
        public ListarUsuariosViewModel ListarUsuariosVM { get; set; }
        public ModificarUsuarioViewModel ModificarUsuarioVM { get; set; }
        public LoginViewModel LoginVM { get; set; }
        public ListarTablerosViewModel ListarTablerosVM { get; set; }

        public string IdDeSesion { get; set; }
        public string NombreDeSesion { get; set; }
        public string RolDeSesion { get; set; }

        public HerramientasUsuariosViewModel(){

        }

        public HerramientasUsuariosViewModel(string id, string name, string rol){
            this.IdDeSesion = id;
            this.NombreDeSesion = name;
            this.RolDeSesion = rol;
        }

        public HerramientasUsuariosViewModel(CrearUsuarioViewModel crear, string id, string name, string rol){
            this.CrearUsuarioVM = crear;
            this.IdDeSesion = id;
            this.NombreDeSesion = name;
            this.RolDeSesion = rol;
        }

        public HerramientasUsuariosViewModel(ListarUsuariosViewModel listar, string id, string name, string rol){
            this.ListarUsuariosVM = listar;
            this.IdDeSesion = id;
            this.NombreDeSesion = name;
            this.RolDeSesion = rol;
        }

        public HerramientasUsuariosViewModel(ModificarUsuarioViewModel modificar, string id, string name, string rol){
            this.ModificarUsuarioVM = modificar;
            this.IdDeSesion = id;
            this.NombreDeSesion = name;
            this.RolDeSesion = rol;
        }

        public HerramientasUsuariosViewModel(LoginViewModel login, string id, string name, string rol){
            this.LoginVM = login;
            this.IdDeSesion = id;
            this.NombreDeSesion = name;
            this.RolDeSesion = rol;
        }

        public HerramientasUsuariosViewModel(ListarTablerosViewModel listarTableros, string id, string name, string rol){
            this.ListarTablerosVM = listarTableros;
            this.IdDeSesion = id;
            this.NombreDeSesion = name;
            this.RolDeSesion = rol;
        }        
    }
}