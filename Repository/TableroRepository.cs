using System.Data.SQLite;

public class TableroRepository : ITableroRepository
{
    private string cadenaConexion = "Data Source=DB/kandan.db;Cache=Shared";

    
    // Crear un nuevo tablero. (devuelve un objeto Tablero).
    public Tablero Create(Tablero tab)
    {
        var query = $"INSERT INTO Tablero (id_usuario_propietario, nombre, descripcion) VALUES (@idUserProp, @nombre, @desc)";
        using (SQLiteConnection connection = new SQLiteConnection(cadenaConexion))
        {
            connection.Open();
            var command = new SQLiteCommand(query, connection);

            command.Parameters.Add(new SQLiteParameter("@idUserProp", tab.IdUsuarioPropietario));
            command.Parameters.Add(new SQLiteParameter("@nombre", tab.Nombre));
            command.Parameters.Add(new SQLiteParameter("@desc", tab.Descripcion));

            command.ExecuteNonQuery();

            connection.Close();
        }

        return tab;
    }


    // Modificar un tablero existente. (recibe un id y un objeto Tablero).
    public void Update(int id, Tablero tablero)
    {
        var query = $"UPDATE Tablero SET id_usuario_propietario = @idUserProp, nombre = @nombre, descripcion = @desc WHERE id = @idcambiar;";
        using (SQLiteConnection connection = new SQLiteConnection(cadenaConexion))
        {
            connection.Open();
            var command = new SQLiteCommand(query, connection);

            command.Parameters.Add(new SQLiteParameter("@idUserProp", tablero.IdUsuarioPropietario));
            command.Parameters.Add(new SQLiteParameter("@nombre", tablero.Nombre));
            command.Parameters.Add(new SQLiteParameter("@desc", tablero.Descripcion));
            command.Parameters.Add(new SQLiteParameter("@idcambiar", id));

            command.ExecuteNonQuery();

            connection.Close();
        }
    }


    // Obtener detalles de un tablero por su ID. (recibe un id y devuelve un Tablero).
    public Tablero GetById(int id)
    {
        SQLiteConnection connection = new SQLiteConnection(cadenaConexion);

        var tab = new Tablero();

        SQLiteCommand command = connection.CreateCommand();

        command.CommandText = "SELECT * FROM Tablero WHERE id = @idTablero";
        command.Parameters.Add(new SQLiteParameter("@idTablero", id));
        

        connection.Open();
        using(SQLiteDataReader reader = command.ExecuteReader())
        {
            while (reader.Read())
            {
                tab.Id = Convert.ToInt32(reader["id"]);
                tab.IdUsuarioPropietario = Convert.ToInt32(reader["id_usuario_propietario"]);
                tab.Nombre = reader["nombre"].ToString();
                tab.Descripcion = reader["descripcion"].ToString();
            }
        }
        connection.Close();


        return (tab);
    }


    // Listar todos los tableros existentes. (devuelve un list de tableros).
    public List<Tablero> List()
    {
        var queryString = @"SELECT * FROM Tablero;";

        List<Tablero> ListaTableros = new List<Tablero>();


        using (SQLiteConnection connection = new SQLiteConnection(cadenaConexion))
        {
            SQLiteCommand command = new SQLiteCommand(queryString, connection);
            connection.Open();
        
            using(SQLiteDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    var tab = new Tablero();
                    tab.Id = Convert.ToInt32(reader["id"]);
                    tab.IdUsuarioPropietario = Convert.ToInt32(reader["id_usuario_propietario"]);
                    tab.Nombre = reader["nombre"].ToString();
                    tab.Descripcion = reader["descripcion"].ToString();
                    ListaTableros.Add(tab);
                }
            }
            connection.Close();
        }

        return ListaTableros;
    }

    
    // Listar todos los tableros de un usuario específico. (recibe un IdUsuario, devuelve un list de tableros).
    public List<Tablero> ListByUser(int id)
    {
        SQLiteConnection connection = new SQLiteConnection(cadenaConexion);

        List<Tablero> ListaTableros = new List<Tablero>();

        SQLiteCommand command = connection.CreateCommand();

        command.CommandText = "SELECT * FROM Tablero WHERE id_usuario_propietario = @idUsuario";
        command.Parameters.Add(new SQLiteParameter("@idUsuario", id));
        

        connection.Open();
        using(SQLiteDataReader reader = command.ExecuteReader())
        {
            while (reader.Read())
            {
                var tab = new Tablero();
                tab.Id = Convert.ToInt32(reader["id"]);
                tab.IdUsuarioPropietario = Convert.ToInt32(reader["id_usuario_propietario"]);
                tab.Nombre = reader["nombre"].ToString();
                tab.Descripcion = reader["descripcion"].ToString();
                ListaTableros.Add(tab);
            }
        }
        connection.Close();


        return ListaTableros;
    }


    // Eliminar un tablero por ID.
    public void Remove(int id)
    {
        using(SQLiteConnection connection = new SQLiteConnection(cadenaConexion))
        {
            SQLiteCommand command = connection.CreateCommand();
            command.CommandText = $"DELETE FROM Tablero WHERE id = @idTablero;";
            command.Parameters.Add(new SQLiteParameter("@idTablero", id));

            connection.Open();
            
            command.ExecuteNonQuery();
            
            connection.Close();
        }
    }
}