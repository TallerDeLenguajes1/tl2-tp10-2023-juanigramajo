using System.Data.SQLite;
using tl2_tp10_2023_juanigramajo.Models;

public class TableroRepository : ITableroRepository
{
    private readonly string _cadenaConexion;

    public TableroRepository(string cadenaConexion)
    {
        _cadenaConexion = cadenaConexion;
    }

    
    // Crear un nuevo tablero. (devuelve un objeto Tablero).
    public Tablero Create(Tablero tab)
    {
        var query = $"INSERT INTO Tablero (id_usuario_propietario, nombre, descripcion) VALUES (@idUserProp, @nombre, @desc)";
        using (SQLiteConnection connection = new (_cadenaConexion))
        {
            connection.Open();
            var command = new SQLiteCommand(query, connection);

            // la consigna pedía asumir que el usuario propietario es el mismo, por eso envío un 1
            command.Parameters.Add(new SQLiteParameter("@idUserProp", 1));
            command.Parameters.Add(new SQLiteParameter("@nombre", tab.Nombre));
            command.Parameters.Add(new SQLiteParameter("@desc", tab.Descripcion));

            var commandENonQ = command.ExecuteNonQuery();
            if (commandENonQ == 0) throw new Exception("Se produjo un error al crear el tablero");

            connection.Close();
        }

        return tab;
    }


    // Modificar un tablero existente. (recibe un id y un objeto Tablero).
    public void Update(int id, Tablero tablero)
    {
        var query = $"UPDATE Tablero SET nombre = @nombre, descripcion = @desc WHERE id = @idcambiar;";
        using (SQLiteConnection connection = new (_cadenaConexion))
        {
            connection.Open();
            var command = new SQLiteCommand(query, connection);

            // command.Parameters.Add(new SQLiteParameter("@idUserProp", tablero.IdUsuarioPropietario));
            command.Parameters.Add(new SQLiteParameter("@nombre", tablero.Nombre));
            command.Parameters.Add(new SQLiteParameter("@desc", tablero.Descripcion));
            command.Parameters.Add(new SQLiteParameter("@idcambiar", id));

            var commandENonQ = command.ExecuteNonQuery();
            if (commandENonQ == 0) throw new Exception("Se produjo un error al actualizar el tablero");

            connection.Close();
        }
    }


    // Obtener detalles de un tablero por su ID. (recibe un id y devuelve un Tablero).
    public Tablero GetById(int id)
    {
        SQLiteConnection connection = new (_cadenaConexion);

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

        if (tab == null) throw new Exception($"No se encontraron tableros en la base de datos");

        return (tab);
    }


    // Listar todos los tableros existentes. (devuelve un list de tableros).
    public List<Tablero> List()
    {
        var queryString = @"SELECT * FROM Tablero;";

        List<Tablero> ListaTableros = new List<Tablero>();


        using (SQLiteConnection connection = new (_cadenaConexion))
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

        if (ListaTableros == null) throw new Exception($"No se encontraron tableros en la base de datos");

        return ListaTableros;
    }

    
    // Listar todos los tableros de un usuario específico. (recibe un IdUsuario, devuelve un list de tableros).
    public List<Tablero> ListByUser(int id)
    {
        SQLiteConnection connection = new (_cadenaConexion);

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

        if (ListaTableros == null) throw new Exception($"No se encontraron tableros asignados al usuario en la base de datos");

        return ListaTableros;
    }


    // Eliminar un tablero por ID.
    public void Remove(int id)
    {
        using(SQLiteConnection connection = new (_cadenaConexion))
        {
            SQLiteCommand command = connection.CreateCommand();
            command.CommandText = $"DELETE FROM Tablero WHERE id = @idTablero;";
            command.Parameters.Add(new SQLiteParameter("@idTablero", id));

            connection.Open();
            
            var commandENonQ = command.ExecuteNonQuery();
            if (commandENonQ == 0) throw new Exception("Se produjo un error al eliminar el tablero");

            connection.Close();
        }
    }
}