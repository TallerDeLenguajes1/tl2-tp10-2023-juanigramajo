using System.Data.SQLite;

public class UsuarioRepository : IUsuarioRepository
{
    private string cadenaConexion = "Data Source=DB/kandan.db;Cache=Shared";
    
    
    // Crear un nuevo usuario. (recibe un objeto Usuario).
    public void Create(Usuario usuario)
    {   
        var query = $"INSERT INTO Usuario (nombre_de_usuario) VALUES (@nombreDeUsuario)";
        using (SQLiteConnection connection = new SQLiteConnection(cadenaConexion))
        {
            connection.Open();
            var command = new SQLiteCommand(query, connection);

            command.Parameters.Add(new SQLiteParameter("@nombreDeUsuario", usuario.NombreDeUsuario));

            command.ExecuteNonQuery();

            connection.Close();
        }
    }
    
    
    // Modificar un usuario existente. (recibe un Id y un objeto Usuario).
    public void Update(int ID, Usuario usuario)
    {
        var query = $"UPDATE Usuario SET nombre_de_usuario = @nombreDeUsuario WHERE id = @idcambiar;";
        using (SQLiteConnection connection = new SQLiteConnection(cadenaConexion))
        {
            connection.Open();
            var command = new SQLiteCommand(query, connection);

            command.Parameters.Add(new SQLiteParameter("@nombreDeUsuario", usuario.NombreDeUsuario));
            command.Parameters.Add(new SQLiteParameter("@idcambiar", ID));

            command.ExecuteNonQuery();

            connection.Close();
        }
    }


    // Listar todos los usuarios registrados. (devuelve un List de Usuarios).
    public List<Usuario> List()
    {
        var queryString = @"SELECT * FROM Usuario;";

        List<Usuario> ListaUsuarios = new List<Usuario>();


        using (SQLiteConnection connection = new SQLiteConnection(cadenaConexion))
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
                    ListaUsuarios.Add(user);
                }
            }
            connection.Close();
        }

        return ListaUsuarios;
    }


    // Obtener detalles de un usuario por su ID. (recibe un Id y devuelve un Usuario).
    public Usuario GetById(int idUser)
    {
        SQLiteConnection connection = new SQLiteConnection(cadenaConexion);

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
            }
        }
        connection.Close();


        return (user);
    }


    // Eliminar un usuario por ID.
    public void Remove(int idUser)
    {
        using(SQLiteConnection connection = new SQLiteConnection(cadenaConexion))
        {
            SQLiteCommand command = connection.CreateCommand();
            command.CommandText = $"DELETE FROM Usuario WHERE id = @idUsuario;";
            command.Parameters.Add(new SQLiteParameter("@idUsuario", idUser));

            connection.Open();
            
            command.ExecuteNonQuery();
            
            connection.Close();
        }
    }
}