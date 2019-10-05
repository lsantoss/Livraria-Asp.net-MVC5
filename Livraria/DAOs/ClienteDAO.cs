using Dapper;
using Livraria.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace Livraria.DAOs
{
    public class ClienteDAO
    {
        public IEnumerable<Cliente> RetornarTodos()
        {
            using (var conexao = new SqlConnection(connStr))
            {
                try
                {
                    var result = conexao.Query<Cliente>("select * from TBCliente order by TBCliente.nome");
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
            using (var conexao = new SqlConnection(connStr))
            {
                try
                {
                    var result = conexao.Query<Cliente>(
                        "select TBCliente.* from TBCliente " +
                        "where TBCliente.nome like '%"+ busca + "%' " +
                        "order by TBCliente.nome");
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
            using (var conexao = new SqlConnection(connStr))
            {
                try
                {
                    var result = conexao.QueryFirst<Cliente>("select * from TBCliente where id=@id", new { Id = id });
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
            using (var conexao = new SqlConnection(connStr))
            {
                try
                {
                    if (obj.Id == 0)
                        obj.Id = RetornarUltimoId() + 1;

                    conexao.Execute("insert into TBCliente (id, nome, idade, sexo, telefone, cpf) values (@id, @nome, @idade, @sexo, @telefone, @cpf)", obj);
                    conexao.Close();
                }
                catch (Exception e)
                {
                    throw new Exception("Não foi possível inserir cliente!", e);
                }
            }
        }

        public void Deletar(int id)
        {
            using (var conexao = new SqlConnection(connStr))
            {
                try
                {
                    conexao.Execute("delete from TBCliente where id=@id", new { Id = id });
                    conexao.Close();
                }
                catch(Exception e)
                {
                    throw new Exception("Não foi possível apagar cliente!", e);
                }
            }
        }

        public void Alterar(Cliente obj)
        {
            using (var conexao = new SqlConnection(connStr))
            {
                try
                {
                    conexao.Execute("update TBCliente set nome=@nome, idade=@idade, sexo=@sexo, telefone=@telefone, cpf=@cpf where id=@id", obj);
                    conexao.Close();
                }
                catch (Exception e)
                {
                    throw new Exception("Não foi possível alterar cliente!", e);
                }
            }
        }

        private int RetornarUltimoId()
        {
            using (var conexao = new SqlConnection(connStr))
            {
                try
                {
                    var result = conexao.QuerySingle<int>("select max(id) from TBCliente");
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

        private static readonly string connStr = @"Integrated Security=SSPI;Persist Security Info=False;Initial Catalog=Livraria;Data Source=SANTOS-PC\SQLEXPRESS;";
    }
}