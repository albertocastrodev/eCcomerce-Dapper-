using eCcomerce_Dapper_.Models;

namespace eCcomerce_Dapper_.Controllers
{
    public interface IUsuario_Repository
    {
        public List<Usuario> GetUsuarios();
        public Usuario GetUsuario(int id);

        public void Insert(Usuario usuario);
        public void Update(Usuario usuario);

        public void Delete(int id);

        

    }
}