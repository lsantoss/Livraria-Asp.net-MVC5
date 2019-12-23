using Dapper;
using Livraria.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace Livraria.DAOs
{
    public class ClienteDAO
    {
        private StringBuilder Sql = new StringBuilder();

        public IEnumerable<Cliente> RetornarTodos()
        {
            using (SqlConnection conexao = new SqlConnection(stringConexao))
            {
                try
                {
                    Sql.Clear();
                    Sql.Append("select * from TBCliente order by TBCliente.nome");

                    IEnumerable<Cliente> result = conexao.Query<Cliente>(Sql.ToString());

                    conexao.Close();

                    return result;
                }
                catch(Exception e)
                {
                    throw new Exception("Não foi possível buscar todos clientes!", e);
                }
            }
        }

        public IEnumerable<Cliente> RetornarPorNome(string busca)
        {
            using (SqlConnection conexao = new SqlConnection(stringConexao))
            {
                try
                {
                    Sql.Clear();
                    Sql.Append("select TBCliente.* from TBCliente ");
                    Sql.Append("where TBCliente.nome like '%" + busca + "%' ");
                    Sql.Append("order by TBCliente.nome");

                    IEnumerable<Cliente> result = conexao.Query<Cliente>(Sql.ToString());

                    conexao.Close();

                    return result;
                }
                catch (Exception e)
                {
                    throw new Exception("Não foi possível buscar clientes pelo nome!", e);
                }
            }
        }

        public Cliente RetornarPorId(int id)
        {
            using (SqlConnection conexao = new SqlConnection(stringConexao))
            {
                try
                {
                    Sql.Clear();
                    Sql.Append("select * from TBCliente where id=@id");

                    Cliente result = conexao.QueryFirst<Cliente>(Sql.ToString(), new { Id = id });

                    conexao.Close();

                    return result;
                }
                catch (Exception e)
                {
                    throw new Exception("Não foi possível buscar cliente por id!", e);
                }
            }
        }

        public void Inserir(Cliente obj)
        {
            using (SqlConnection conexao = new SqlConnection(stringConexao))
            {
                try
                {
                    Sql.Clear();
                    Sql.Append("insert into TBCliente (id, nome, idade, sexo, telefone, cpf) values (@id, @nome, @idade, @sexo, @telefone, @cpf)");

                    if (obj.Id == 0)
                        obj.Id = RetornarUltimoId() + 1;

                    conexao.Execute(Sql.ToString(), obj);

                    conexao.Close();
                }
                catch (Exception e)
                {
                    throw new Exception("Não foi possível inserir cliente!", e);
                }
            }
        }

        public void Alterar(Cliente obj)
        {
            using (SqlConnection conexao = new SqlConnection(stringConexao))
            {
                try
                {
                    Sql.Clear();
                    Sql.Append("update TBCliente set nome=@nome, idade=@idade, sexo=@sexo, telefone=@telefone, cpf=@cpf where id=@id");

                    conexao.Execute(Sql.ToString(), obj);

                    conexao.Close();
                }
                catch (Exception e)
                {
                    throw new Exception("Não foi possível alterar cliente!", e);
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
                    Sql.Append("delete from TBCliente where id=@id");

                    conexao.Execute(Sql.ToString(), new { Id = id });

                    conexao.Close();
                }
                catch (Exception e)
                {
                    throw new Exception("Não foi possível apagar cliente!", e);
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
                    Sql.Append("select max(id) from TBCliente");

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