using System.Data.SQLite;
using tl2_tp10_2023_juanigramajo.Models;

public class UsuarioRepository : IUsuarioRepository
{
    private readonly string _cadenaConexion;

    public UsuarioRepository(string cadenaConexion)
    {
        _cadenaConexion = cadenaConexion;
    }
    
    
    // Crear un nuevo usuario. (recibe un objeto Usuario).
    public void Create(Usuario usuario)
    {   
        var query = $"INSERT INTO Usuario (nombre_de_usuario, password, rol_del_usuario) VALUES (@nombreDeUsuario, @contra, @rolDelUsuario)";
        using (SQLiteConnection connection = new (_cadenaConexion))
        {
            connection.Open();
            var command = new SQLiteCommand(query, connection);

            command.Parameters.Add(new SQLiteParameter("@nombreDeUsuario", usuario.NombreDeUsuario));
            command.Parameters.Add(new SQLiteParameter("@contra", usuario.Password));
            command.Parameters.Add(new SQLiteParameter("@rolDelUsuario", (Rol)(Convert.ToInt32(usuario.RolDelUsuario))));

            var commandENonQ = command.ExecuteNonQuery();
            if (commandENonQ == 0) throw new Exception("Se produjo un error al crear el usuario");

            connection.Close();
        }
    }
    
    
    // Modificar un usuario existente. (recibe un Id y un objeto Usuario).
    public void Update(int ID, Usuario usuario)
    {
        var query = $"UPDATE Usuario SET nombre_de_usuario = @nombreDeUsuario, password = @contra, rol_del_usuario = @rolDelUsuario WHERE id = @idcambiar;";
        using (SQLiteConnection connection = new (_cadenaConexion))
        {
            connection.Open();
            var command = new SQLiteCommand(query, connection);

            command.Parameters.Add(new SQLiteParameter("@nombreDeUsuario", usuario.NombreDeUsuario));
            command.Parameters.Add(new SQLiteParameter("@contra", usuario.Password));
            command.Parameters.Add(new SQLiteParameter("@rolDelUsuario", usuario.RolDelUsuario));
            command.Parameters.Add(new SQLiteParameter("@idcambiar", ID));

            var commandENonQ = command.ExecuteNonQuery();
            if (commandENonQ == 0) throw new Exception("Se produjo un error al actualizar el usuario");

            connection.Close();
        }
    }


    // Listar todos los usuarios registrados. (devuelve un List de Usuarios).
    public List<Usuario> List()
    {
        var queryString = @"SELECT * FROM Usuario;";

        List<Usuario> ListaUsuarios = new List<Usuario>();


        using (SQLiteConnection connection = new (_cadenaConexion))
        {
            SQLiteCommand command = new SQLiteCommand(queryString, connection);
            connection.Open();
        
            using(SQLiteDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    var user = new Usuario();
                    user.Id = Convert.ToInt32(reader["id"]);
                    user.NombreDeUsuario = reader["nombre_de_usuario"].ToString();
                    user.Password = reader["password"].ToString();
                    user.RolDelUsuario = (Rol)(Convert.ToInt32(reader["rol_del_usuario"]));
                    ListaUsuarios.Add(user);
                }
            }
            connection.Close();
        }

        if (ListaUsuarios == null) throw new Exception ($"No se encontraron usuarios en la base de datos");

        return ListaUsuarios;
    }


    // Obtener detalles de un usuario por su ID. (recibe un Id y devuelve un Usuario).
    public Usuario GetById(int idUser)
    {
        SQLiteConnection connection = new (_cadenaConexion);

        var user = new Usuario();

        SQLiteCommand command = connection.CreateCommand();

        command.CommandText = "SELECT * FROM Usuario WHERE id = @idUsuario";
        command.Parameters.Add(new SQLiteParameter("@idUsuario", idUser));
        

        connection.Open();
        using(SQLiteDataReader reader = command.ExecuteReader())
        {
            while (reader.Read())
            {
                user.Id = Convert.ToInt32(reader["id"]);
                user.NombreDeUsuario = reader["nombre_de_usuario"].ToString();
                user.Password = reader["password"].ToString();
                user.RolDelUsuario = (Rol)(Convert.ToInt32(reader["rol_del_usuario"]));
            }
        }
        connection.Close();

        if (user == null) throw new Exception ($"No se encontró el usuario en la base de datos");

        return (user);
    }


    // Eliminar un usuario por ID.
    public void Remove(int idUser)
    {
        using(SQLiteConnection connection = new (_cadenaConexion))
        {
            SQLiteCommand command = connection.CreateCommand();
            command.CommandText = $"DELETE FROM Usuario WHERE id = @idUsuario;";
            command.Parameters.Add(new SQLiteParameter("@idUsuario", idUser));

            connection.Open();
            
            var commandENonQ = command.ExecuteNonQuery();
            if (commandENonQ == 0) throw new Exception("Se produjo un error al eliminar el usuario");
            
            connection.Close();
        }
    }
}