using Dapper;
using Livraria.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace Livraria.DAOs
{
    public class UsuarioDAO
    {
        public IEnumerable<Usuario> RetornarTodos()
        {
            using (var conexao = new SqlConnection(connStr))
            {
                try
                {
                    var result = conexao.Query<Usuario>("select * from TBUsuario");
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
            using (var conexao = new SqlConnection(connStr))
            {
                try
                {
                    var result = conexao.Query<Usuario>(
                        "select TBUsuario.* from TBUsuario " +
                        "where TBUsuario.login like '%"+ busca + "%' " +
                        "order by TBUsuario.login");
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
            using (var conexao = new SqlConnection(connStr))
            {
                try
                {
                    var result = conexao.QueryFirst<Usuario>("select * from TBUsuario where id=@id", new { Id = id });
                    conexao.Close();
                    return result;
                }
                catch (Exception e)
                {
                    throw new Exception("Não foi possível buscar usuário por id!", e);
                }
            }
        }

        public void Inserir(Usuario obj)
        {
            using (var conexao = new SqlConnection(connStr))
            {
                try
                {
                    if (obj.Id == 0)
                        obj.Id = RetornarUltimoId() + 1;

                    DateTime validade = DateTime.Now;
                    validade = validade.AddHours(2);
                    obj.Validade = validade;

                    conexao.Execute("insert into TBUsuario (id, login, senha, validade, privilegio) values (@id, @login, @senha, @validade, @privilegio)", obj);
                    conexao.Close();
                }
                catch (Exception e)
                {
                    throw new Exception("Não foi possível inserir usuário!", e);
                }
            }
        }

        public void Deletar(int id)
        {
            using (var conexao = new SqlConnection(connStr))
            {
                try
                {
                    var result = conexao.Execute("delete from TBUsuario where id=@id", new { Id = id });
                    conexao.Close();
                }
                catch (Exception e)
                {
                    throw new Exception("Não foi possível apagar usuário!", e);
                }
            }
        }

        public void Alterar(Usuario obj)
        {
            using (var conexao = new SqlConnection(connStr))
            {
                try
                {
                    DateTime validade = DateTime.Now;
                    validade = validade.AddHours(2);
                    obj.Validade = validade;

                    conexao.Execute("update TBUsuario set login=@login, senha=@senha, validade=@validade, privilegio=@privilegio where id=@id", obj);
                    conexao.Close();
                }
                catch (Exception e)
                {
                    throw new Exception("Não foi possível alterar usuário!", e);
                }
            }
        }

        private int RetornarUltimoId()
        {
            using (var conexao = new SqlConnection(connStr))
            {
                try
                {
                    var result = conexao.QuerySingle<int>("select max(id) from TBUsuario");
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

        public Usuario Logar(Usuario obj)
        {
            using (var conexao = new SqlConnection(connStr))
            {
                try
                {
                    var result = conexao.QueryFirst<Usuario>("select * from TBUsuario where login=@login and senha=@senha", obj);
                    conexao.Close();
                    return result;
                }
                catch
                {
                    return null;
                }
            }
        }

        private static readonly string connStr = @"Integrated Security=SSPI;Persist Security Info=False;Initial Catalog=Livraria;Data Source=SANTOS-PC\SQLEXPRESS;";
    }
}