using Dapper;
using Livraria.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace Livraria.DAOs
{
    public class UsuarioDAO
    {
        private StringBuilder Sql = new StringBuilder();

        public IEnumerable<Usuario> RetornarTodos()
        {
            using (var conexao = new SqlConnection(stringConexao))
            {
                try
                {
                    Sql.Clear();
                    Sql.Append("select * from TBUsuario");

                    IEnumerable<Usuario> result = conexao.Query<Usuario>(Sql.ToString());

                    conexao.Close();

                    return result;
                }
                catch(Exception e)
                {
                    throw new Exception("Não foi possível buscar todos os usuários!", e);
                }
            }
        }

        public IEnumerable<Usuario> RetornarPorNome(string busca)
        {
            using (SqlConnection conexao = new SqlConnection(stringConexao))
            {
                try
                {
                    Sql.Clear();
                    Sql.Append("select TBUsuario.* from TBUsuario ");
                    Sql.Append("where TBUsuario.login like '%" + busca + "%' ");
                    Sql.Append("order by TBUsuario.login");

                    IEnumerable<Usuario> result = conexao.Query<Usuario>(Sql.ToString());

                    conexao.Close();

                    return result;
                }
                catch (Exception e)
                {
                    throw new Exception("Não foi possível buscar os usuários pelo nome!", e);
                }
            }
        }

        public Usuario RetornarPorId(int id)
        {
            using (SqlConnection conexao = new SqlConnection(stringConexao))
            {
                try
                {
                    Sql.Clear();
                    Sql.Append("select * from TBUsuario where id=@id");

                    Usuario result = conexao.QueryFirst<Usuario>(Sql.ToString(), new { Id = id });

                    conexao.Close();

                    return result;
                }
                catch (Exception e)
                {
                    throw new Exception("Não foi possível buscar usuário por id!", e);
                }
            }
        }

        public Usuario Logar(Usuario obj)
        {
            using (SqlConnection conexao = new SqlConnection(stringConexao))
            {
                try
                {
                    Sql.Clear();
                    Sql.Append("select * from TBUsuario where login=@login and senha=@senha");

                    Usuario result = conexao.QueryFirst<Usuario>(Sql.ToString(), obj);

                    conexao.Close();

                    return result;
                }
                catch
                {
                    return null;
                }
            }
        }

        public void Inserir(Usuario obj)
        {
            using (SqlConnection conexao = new SqlConnection(stringConexao))
            {
                try
                {
                    Sql.Clear();
                    Sql.Append("insert into TBUsuario (id, login, senha, validade, privilegio) values (@id, @login, @senha, @validade, @privilegio)");

                    if (obj.Id == 0)
                        obj.Id = RetornarUltimoId() + 1;

                    DateTime validade = DateTime.Now;
                    validade = validade.AddHours(2);
                    obj.Validade = validade;

                    conexao.Execute(Sql.ToString(), obj);

                    conexao.Close();
                }
                catch (Exception e)
                {
                    throw new Exception("Não foi possível inserir usuário!", e);
                }
            }
        }

        public void Alterar(Usuario obj)
        {
            using (SqlConnection conexao = new SqlConnection(stringConexao))
            {
                try
                {
                    Sql.Clear();
                    Sql.Append("update TBUsuario set login=@login, senha=@senha, validade=@validade, privilegio=@privilegio where id=@id");

                    DateTime validade = DateTime.Now;
                    validade = validade.AddHours(2);
                    obj.Validade = validade;

                    conexao.Execute(Sql.ToString(), obj);

                    conexao.Close();
                }
                catch (Exception e)
                {
                    throw new Exception("Não foi possível alterar usuário!", e);
                }
            }
        }

        public void Deletar(int id)
        {
            using (SqlConnection conexao = new SqlConnection(stringConexao))
            {
                try
                {
                    Sql.Clear();
                    Sql.Append("delete from TBUsuario where id=@id");

                    conexao.Execute(Sql.ToString(), new { Id = id });

                    conexao.Close();
                }
                catch (Exception e)
                {
                    throw new Exception("Não foi possível apagar usuário!", e);
                }
            }
        }

        private int RetornarUltimoId()
        {
            using (SqlConnection conexao = new SqlConnection(stringConexao))
            {
                try
                {
                    Sql.Clear();
                    Sql.Append("select max(id) from TBUsuario");

                    int result = conexao.QuerySingle<int>(Sql.ToString());

                    conexao.Close();

                    return result;
                }
                catch
                {
                    conexao.Close();
                    return 0;
                }
            }
        }

        private static readonly string stringConexao = @"Integrated Security=SSPI;Persist Security Info=False;Initial Catalog=Livraria;Data Source=SANTOS-PC\SQLEXPRESS;";
    }
}