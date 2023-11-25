public enum Rol
{
    Administrador = 0,
    Operador = 1
}

public class Usuario
{
    public int Id { get; set; }
    public string NombreDeUsuario { get; set; }
    public string Password { get; set; }
    public Rol RolDelUsuario { get; set; }
}
