namespace eCcomerce_Dapper_.Models
{
    public class Contato
    {
        public int Id { get; set; }

        public int UsuarioId { get; set; }

        public string Telefone { get; set; }

        public string Celular { get; set; }

        public Usuario usuario { get; set; }    
    }
}
