public enum EstadoTarea
{
    Ideas = 0,
    ToDo = 1, 
    Doing = 2, 
    Review = 3, 
    Done = 4
}

public class Tarea
{
    public int Id { get; set; }
    public int IdTablero { get; set; }
    public string Nombre { get; set; }
    public string Descripcion { get; set; } 
    public string Color { get; set; }
    public EstadoTarea Estado { get; set; }
    public int IdUsuarioAsignado { get; set; }
}