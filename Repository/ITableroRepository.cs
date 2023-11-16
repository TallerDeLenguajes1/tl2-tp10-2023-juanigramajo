public interface ITableroRepository
{
    public Tablero Create(Tablero tablero);
    public void Update(int id, Tablero tablero);
    public Tablero GetById(int id);
    public List<Tablero> List();
    public List<Tablero> ListByUser(int id);
    public void Remove(int id);
}