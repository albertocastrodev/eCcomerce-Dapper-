using eCcomerce_Dapper_.Controllers;
using eCcomerce_Dapper_.Models;
using System.Data;
using System.Data.SqlClient;
using Dapper;

namespace eCcomerce_Dapper_.Repository
{
    public class UsuarioRepository : IUsuario_Repository
    {
        private IDbConnection _connection;
        public UsuarioRepository()
        {
            _connection = new SqlConnection(@"Data Source = (localdb)\MSSQLLocalDB; Initial Catalog = Ecommerce; Integrated Security = True; Connect Timeout = 30; Encrypt = False; TrustServerCertificate = False; ApplicationIntent = ReadWrite; MultiSubnetFailover = False");
        }


        public List<Usuario> GetUsuarios()
        {
            return _connection.Query<Usuario>("SELECT * FROM Usuarios").ToList();
          
            // List<Usuario> usuarios = new List<Usuario>();

            //string sql = "select* from usuarios as U  LEFT JOIN Contatos c on u.Id = c.UsuarioId LEFT JOIN EnderecosEntrega EE on U.Id = EE.UsuarioId  where U.Id = @Id";
            //_connection.Query<Usuario, Contato, EnderecoEntrega, Usuario>(sql,
                
            //    (usuario, contato, enderecosentrega) => {  // Mapeamento das informações nos obejtos 

            //        if (usuarios.SingleOrDefault(a => a.Id == usuario.Id) == null)  // Se não achar um usuario igual, adiciona usuário na lista
            //        {
            //            usuarios.Add(usuario);

            //        }
            //        else {

            //           usuario =  usuarios.SingleOrDefault(a => a.Id == usuario.Id);

            //        }   
                   
               // }
                //);
        }
        public Usuario GetUsuario(int id)
        {
            return _connection.Query<Usuario, Contato, Usuario>("select * from usuarios as U  LEFT JOIN Contatos c on  u.Id = c.UsuarioIdLEFT JOIN EnderecosEntrega EE on U.Id = EE.UsuarioId  where U.Id = @Id;", // Os primeiros parametros são os objetos usados, o terceiro é o que será retornado
                (usuario, contato) =>
                {
                    usuario.Contato = contato;
                    return usuario;
                     
                },
                new { Id = id }

                ).SingleOrDefault();


        }


        public void Insert(Usuario usuario)
        {
            _connection.Open();

            var transaction = _connection.BeginTransaction();

            try
            {


                string sql = "INSERT INTO Usuarios(Nome, Email, Sexo, RG,CPF, NomeMae,SituacaoCadastro,DataCadastro) VALUES (@Nome,@Email,@Sexo,@RG,@CPF,@NomeMae,@SituacaoCadastro,@DataCadastro); SELECT CAST(SCOPE_IDENTITY() AS INT);";
                usuario.Id = _connection.Query<int>(sql, usuario, transaction).Single();

                if (usuario.Contato != null)
                {
                    usuario.Contato.UsuarioId = usuario.Id;
                    string sqlContato = "INSERT INTO Contatos (UsuarioId,Telefone, Celular) VALUES (@UsuarioId,@Telefone,@Celular); SELECT CAST(SCOPE_IDENTITY() AS INT);";
                    usuario.Contato.Id = _connection.Query<int>(sqlContato, usuario.Contato, transaction).Single();
                }

                transaction.Commit();

            }

            catch (Exception)
            {
                try
                {
                    transaction.Rollback();
                }
                catch {

                    throw new ArgumentNullException();

                }
                
            }

            finally
            {

                _connection.Close();

            }


        }

        public void Update(Usuario usuario)
        {
            _connection.Open();
            var transaction = (_connection.BeginTransaction());
            try
            {

                string sql = "UPDATE Usuarios(Nome, Email, Sexo, RG,CPF, NomeMae,SituacaoCadastro,DataCadastro) SET (@Nome,@Email,@Sexo,@RG,@CPF,@NomeMae,@SituacaoCadastro,@DataCadastro) WHERE Id = @id ";
                _connection.Execute(sql, usuario);

                if (usuario.Contato != null)
                {
                    string sqlContato = "UPDATE CONTATOS SET Telefone = @Telefone, Celular = @Celular where = Id = @Id";
                    _connection.Execute(sqlContato, usuario.Contato,transaction);
                }

                transaction.Commit();


            }

            catch (Exception ex)
            {
                try
                {
                    transaction.Rollback();
                }
                catch
                {

                    throw new ArgumentNullException();

                }

            }

            finally
            {

                _connection.Close();

            }



        }

        public void Delete(int id)
        {

            _connection.Execute("DELETE FROM USUARIOS WHERE Id = @id", new { Id = id });
        }
    }
}


