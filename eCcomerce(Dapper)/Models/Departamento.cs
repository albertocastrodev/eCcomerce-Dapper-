namespace eCcomerce_Dapper_.Models
{
    public class Departamento
    {

        public int Id { get; set; }

        public string Nome { get; set; }

        ICollection<Usuario> Usuarios { get; set; }
    }
}
