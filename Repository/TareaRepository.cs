using System.Data.SQLite;
using tl2_tp10_2023_juanigramajo.Models;

public class TareaRepository : ITareaRepository
{
    private readonly string _cadenaConexion;

    public TareaRepository(string cadenaConexion)
    {
        _cadenaConexion = cadenaConexion;
    }

    // Crear una nueva tarea en un tablero. (recibe un idTablero, devuelve un objeto Tarea)
    public Tarea Create(int idTab, Tarea tarea)
    {
        var query = $"INSERT INTO Tarea (id_tablero, nombre, estado, descripcion, color, id_usuario_asignado) VALUES (@idTablero, @nombre, @estado, @desc, @color, @idUserAsign)";

        using (SQLiteConnection connection = new (_cadenaConexion))
        {
            connection.Open();
            var command = new SQLiteCommand(query, connection);

            command.Parameters.Add(new SQLiteParameter("@idTablero", idTab));
            command.Parameters.Add(new SQLiteParameter("@nombre", tarea.Nombre));
            command.Parameters.Add(new SQLiteParameter("@estado", (EstadoTarea)(Convert.ToInt32(tarea.Estado))));
            command.Parameters.Add(new SQLiteParameter("@desc", tarea.Descripcion));
            command.Parameters.Add(new SQLiteParameter("@color", tarea.Color));
            command.Parameters.Add(new SQLiteParameter("@idUserAsign", 0));
            // la consigna pedía que no posea usuario asignado

            var commandENonQ = command.ExecuteNonQuery();
            if (commandENonQ == 0) throw new Exception("Se produjo un error al crear la tarea");

            connection.Close();
        }

        return tarea;
    }


    // Modificar una tarea existente. (recibe un id y un objeto Tarea)
    public void Update(int id, Tarea tarea)
    {
        var query = $"UPDATE Tarea SET nombre = @nombre, estado = @estado, descripcion = @desc, color = @color WHERE id = @idcambiar;";
        using (SQLiteConnection connection = new (_cadenaConexion))
        {
            connection.Open();
            var command = new SQLiteCommand(query, connection);

            // command.Parameters.Add(new SQLiteParameter("@idTablero", tarea.IdTablero));
            command.Parameters.Add(new SQLiteParameter("@nombre", tarea.Nombre));
            command.Parameters.Add(new SQLiteParameter("@estado", tarea.Estado));
            command.Parameters.Add(new SQLiteParameter("@desc", tarea.Descripcion));
            command.Parameters.Add(new SQLiteParameter("@color", tarea.Color));
            // command.Parameters.Add(new SQLiteParameter("@idUserAsign", tarea.IdUsuarioAsignado));
            command.Parameters.Add(new SQLiteParameter("@idcambiar", id));

            var commandENonQ = command.ExecuteNonQuery();
            if (commandENonQ == 0) throw new Exception("Se produjo un error al actualizar la tarea");

            connection.Close();
        }
    }


    // Obtener detalles de una tarea por su ID. (devuelve un objeto Tarea)
    public Tarea GetById(int id)
    {
        SQLiteConnection connection = new (_cadenaConexion);

        var tarea = new Tarea();

        SQLiteCommand command = connection.CreateCommand();

        command.CommandText = "SELECT * FROM Tarea WHERE id = @idTarea";
        command.Parameters.Add(new SQLiteParameter("@idTarea", id));
        

        connection.Open();
        using(SQLiteDataReader reader = command.ExecuteReader())
        {
            while (reader.Read())
            {
                tarea.Id = Convert.ToInt32(reader["id"]);
                tarea.IdTablero = Convert.ToInt32(reader["id_tablero"]);
                tarea.Nombre = reader["nombre"].ToString();
                tarea.Estado = (EstadoTarea)(Convert.ToInt32(reader["estado"]));
                tarea.Descripcion = reader["descripcion"].ToString();
                tarea.Color = reader["color"].ToString();
                // tarea.IdUsuarioAsignado = Convert.ToInt32(reader["id_usuario_asignado"]);
                tarea.IdUsuarioAsignado = 0;
            }
        }
        connection.Close();

        if (tarea == null) throw new Exception ($"No se encontró la tarea en la base de datos");

        return (tarea);
    }


    public List<Tarea> List()
    {
        SQLiteConnection connection = new (_cadenaConexion);

        List<Tarea> ListaTareas = new List<Tarea>();

        SQLiteCommand command = connection.CreateCommand();

        command.CommandText = "SELECT * FROM Tarea";
        

        connection.Open();
        using(SQLiteDataReader reader = command.ExecuteReader())
        {
            while (reader.Read())
            {
                var tarea = new Tarea();
                tarea.Id = Convert.ToInt32(reader["id"]);
                tarea.IdTablero = Convert.ToInt32(reader["id_tablero"]);
                tarea.Nombre = reader["nombre"].ToString();
                tarea.Estado = (EstadoTarea)(Convert.ToInt32(reader["estado"]));
                tarea.Descripcion = reader["descripcion"].ToString();
                tarea.Color = reader["color"].ToString();
                // tarea.IdUsuarioAsignado = Convert.ToInt32(reader["id_usuario_asignado"]);
                tarea.IdUsuarioAsignado = 0;
                ListaTareas.Add(tarea);
            }
        }
        connection.Close();

        if (ListaTareas == null) throw new Exception ($"No se encontraron tareas en la base de datos");
        
        return ListaTareas;
    }
    

    
    // Listar todas las tareas asignadas a un usuario específico.(recibe un idUsuario, devuelve un list de tareas)
    public List<Tarea> ListByUser(int idUser)
    {
        SQLiteConnection connection = new (_cadenaConexion);

        List<Tarea> ListaTareas = new List<Tarea>();

        SQLiteCommand command = connection.CreateCommand();

        command.CommandText = "SELECT * FROM Tarea WHERE id_usuario_asignado = @idUsuario";
        command.Parameters.Add(new SQLiteParameter("@idUsuario", idUser));
        

        connection.Open();
        using(SQLiteDataReader reader = command.ExecuteReader())
        {
            while (reader.Read())
            {
                var tarea = new Tarea();
                tarea.Id = Convert.ToInt32(reader["id"]);
                tarea.IdTablero = Convert.ToInt32(reader["id_tablero"]);
                tarea.Nombre = reader["nombre"].ToString();
                tarea.Estado = (EstadoTarea)(Convert.ToInt32(reader["estado"]));
                tarea.Descripcion = reader["descripcion"].ToString();
                tarea.Color = reader["color"].ToString();
                // tarea.IdUsuarioAsignado = Convert.ToInt32(reader["id_usuario_asignado"]);
                tarea.IdUsuarioAsignado = 0;
                ListaTareas.Add(tarea);
            }
        }
        connection.Close();

        if (ListaTareas == null) throw new Exception ($"No se encontraron tareas asignadas al usuario en la base de datos");


        return ListaTareas;
    }


    // Listar todas las tareas de un tablero específico. (recibe un idTablero, devuelve un list de tareas)
    public List<Tarea> ListByTablero(int idTab)
    {
        SQLiteConnection connection = new (_cadenaConexion);

        List<Tarea> ListaTareas = new List<Tarea>();

        SQLiteCommand command = connection.CreateCommand();

        command.CommandText = "SELECT * FROM Tarea WHERE id_tablero = @idTablero  ";
        command.Parameters.Add(new SQLiteParameter("@idTablero", idTab));
        

        connection.Open();
        using(SQLiteDataReader reader = command.ExecuteReader())
        {
            while (reader.Read())
            {
                var tarea = new Tarea();
                tarea.Id = Convert.ToInt32(reader["id"]);
                tarea.IdTablero = Convert.ToInt32(reader["id_tablero"]);
                tarea.Nombre = reader["nombre"].ToString();
                tarea.Estado = (EstadoTarea)(Convert.ToInt32(reader["estado"]));
                tarea.Descripcion = reader["descripcion"].ToString();
                tarea.Color = reader["color"].ToString();
                // tarea.IdUsuarioAsignado = Convert.ToInt32(reader["id_usuario_asignado"]);
                tarea.IdUsuarioAsignado = 0;
                ListaTareas.Add(tarea);
            }
        }
        connection.Close();

        if (ListaTareas == null) throw new Exception ($"No se encontraron tareas en la base de datos");

        return ListaTareas;
    }


    public List<Tarea> CantxEstado(EstadoTarea estado)
    {
        SQLiteConnection connection = new (_cadenaConexion);

        List<Tarea> ListaTareas = new List<Tarea>();

        SQLiteCommand command = connection.CreateCommand();

        command.CommandText = "SELECT * FROM Tarea WHERE estado = @estado";
        command.Parameters.Add(new SQLiteParameter("@estado", (EstadoTarea)(estado)));
        

        connection.Open();
        using(SQLiteDataReader reader = command.ExecuteReader())
        {
            while (reader.Read())
            {
                var tarea = new Tarea();
                tarea.Id = Convert.ToInt32(reader["id"]);
                tarea.IdTablero = Convert.ToInt32(reader["id_tablero"]);
                tarea.Nombre = reader["nombre"].ToString();
                tarea.Estado = (EstadoTarea)(Convert.ToInt32(reader["estado"]));
                tarea.Descripcion = reader["descripcion"].ToString();
                tarea.Color = reader["color"].ToString();
                // tarea.IdUsuarioAsignado = Convert.ToInt32(reader["id_usuario_asignado"]);
                tarea.IdUsuarioAsignado = 0;
                ListaTareas.Add(tarea);
            }
        }
        connection.Close();

        if (ListaTareas == null) throw new Exception ($"No se encontraron tareas en la base de datos");


        return ListaTareas;
    }


    // Eliminar una tarea (recibe un IdTarea)
    public void Remove(int id)
    {
        using(SQLiteConnection connection = new (_cadenaConexion))
        {
            SQLiteCommand command = connection.CreateCommand();
            command.CommandText = $"DELETE FROM Tarea WHERE id = @idTarea;";
            command.Parameters.Add(new SQLiteParameter("@idTarea", id));

            connection.Open();
            
            var commandENonQ = command.ExecuteNonQuery();
            if (commandENonQ == 0) throw new Exception("Se produjo un error al eliminar la tarea");
            
            connection.Close();
        }
    }


    // Asignar Usuario a Tarea (recibe idUsuario y un idTarea)
    public void Asignar(int idUser, int idTarea)
    {
        var query = "UPDATE Tarea SET id_usuario_asignado = @idUser WHERE Id = idTarea";
        using (SQLiteConnection connection = new (_cadenaConexion))
        {
            connection.Open();
            var command = new SQLiteCommand(query, connection);

            command.Parameters.Add(new SQLiteParameter("@idUser", idUser));
            command.Parameters.Add(new SQLiteParameter("@idTarea", idTarea));

            var commandENonQ = command.ExecuteNonQuery();
            if (commandENonQ == 0) throw new Exception("Se produjo un error al modificar la tarea");

            connection.Close();
        }

    }
}