using Dapper;
using Livraria.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace Livraria.DAOs
{
    public class LocacaoDAO
    {
        private StringBuilder Sql = new StringBuilder();

        public IEnumerable<Locacao> RetornarTodos()
        {
            using (SqlConnection conexao = new SqlConnection(stringConexao))
            {
                try
                {
                    Sql.Clear();
                    Sql.Append("select TBCliente.nome as NomeCliente, TBLivro.nome as NomeLivro, TBLocacao.* ");
                    Sql.Append("from TBLocacao inner join TBCliente on TBLocacao.idcliente = TBCliente.id ");
                    Sql.Append("inner join TBLivro on TBLocacao.idlivro = TBLivro.id");

                    IEnumerable<Locacao> result = conexao.Query<Locacao>(Sql.ToString());

                    conexao.Close();

                    return result;
                }
                catch (Exception e)
                {
                    throw new Exception("Não foi possível buscar todas locações!", e);
                }
            }
        }

        public IEnumerable<Locacao> RetornarPorNome(string busca)
        {
            using (SqlConnection conexao = new SqlConnection(stringConexao))
            {
                try
                {
                    Sql.Clear();
                    Sql.Append("select TBCliente.nome as NomeCliente, TBLivro.nome as NomeLivro, TBLocacao.* ");
                    Sql.Append("from TBLocacao inner join TBCliente on TBLocacao.idcliente = TBCliente.id ");
                    Sql.Append("inner join TBLivro on TBLocacao.idlivro = TBLivro.id ");
                    Sql.Append("where TBCliente.nome like '%" + busca + "%' or TBLivro.nome like '%" + busca + "%'");

                    IEnumerable<Locacao> result = conexao.Query<Locacao>(Sql.ToString());

                    conexao.Close();

                    return result;
                }
                catch (Exception e)
                {
                    throw new Exception("Não foi possível buscar locações pelo nome do cliente ou livro!", e);
                }
            }
        }

        public IEnumerable<Locacao> RetornarPorNomeAlugados(string busca)
        {
            using (SqlConnection conexao = new SqlConnection(stringConexao))
            {
                try
                {
                    Sql.Clear();
                    Sql.Append("select TBCliente.nome as NomeCliente, TBLivro.nome as NomeLivro, TBLocacao.* ");
                    Sql.Append("from TBLocacao inner join TBCliente on TBLocacao.idcliente = TBCliente.id ");
                    Sql.Append("inner join TBLivro on TBLocacao.idlivro = TBLivro.id ");
                    Sql.Append("where TBLocacao.entrega is null and (TBCliente.nome like '%" + busca + "%' or TBLivro.nome like '%" + busca + "%')");

                    IEnumerable<Locacao> result = conexao.Query<Locacao>(Sql.ToString());

                    conexao.Close();

                    return result;
                }
                catch (Exception e)
                {
                    throw new Exception("Não foi possível buscar locações em aberto pelo nome do cliente ou livro!", e);
                }
            }
        }

        public IEnumerable<Locacao> RetornarPorNomeEntregues(string busca)
        {
            using (SqlConnection conexao = new SqlConnection(stringConexao))
            {
                try
                {
                    Sql.Clear();
                    Sql.Append("select TBCliente.nome as NomeCliente, TBLivro.nome as NomeLivro, TBLocacao.* ");
                    Sql.Append("from TBLocacao inner join TBCliente on TBLocacao.idcliente = TBCliente.id ");
                    Sql.Append("inner join TBLivro on TBLocacao.idlivro = TBLivro.id ");
                    Sql.Append("where TBLocacao.entrega is not null and (TBCliente.nome like '%" + busca + "%' or TBLivro.nome like '%" + busca + "%')");

                    IEnumerable<Locacao> result = conexao.Query<Locacao>(Sql.ToString());

                    conexao.Close();

                    return result;
                }
                catch (Exception e)
                {
                    throw new Exception("Não foi possível buscar locações finalizadas pelo nome do cliente ou livro!!", e);
                }
            }
        }

        public IEnumerable<Locacao> ListaAlugados()
        {
            using (SqlConnection conexao = new SqlConnection(stringConexao))
            {
                try
                {
                    Sql.Clear();
                    Sql.Append("select TBCliente.nome as NomeCliente, TBLivro.nome as NomeLivro, TBLocacao.* ");
                    Sql.Append("from TBLocacao inner join TBCliente on TBLocacao.idcliente = TBCliente.id ");
                    Sql.Append("inner join TBLivro on TBLocacao.idlivro = TBLivro.id ");
                    Sql.Append("where TBLocacao.entrega is null");

                    IEnumerable<Locacao> result = conexao.Query<Locacao>(Sql.ToString());

                    conexao.Close();

                    return result;
                }
                catch (Exception e)
                {
                    throw new Exception("Não foi possível buscar todas locações em aberto!", e);
                }
            }
        }

        public IEnumerable<Locacao> ListaEntregues()
        {
            using (SqlConnection conexao = new SqlConnection(stringConexao))
            {
                try
                {
                    Sql.Clear();
                    Sql.Append("select TBCliente.nome as NomeCliente, TBLivro.nome as NomeLivro, TBLocacao.* ");
                    Sql.Append("from TBLocacao inner join TBCliente on TBLocacao.idcliente = TBCliente.id ");
                    Sql.Append("inner join TBLivro on TBLocacao.idlivro = TBLivro.id ");
                    Sql.Append("where TBLocacao.entrega is not null");

                    IEnumerable<Locacao> result = conexao.Query<Locacao>(Sql.ToString());

                    conexao.Close();

                    return result;
                }
                catch (Exception e)
                {
                    throw new Exception("Não foi possível buscar todas locações finalizadas!", e);
                }
            }
        }

        public double Lucros()
        {
            using (SqlConnection conexao = new SqlConnection(stringConexao))
            {
                try
                {
                    Sql.Clear();
                    Sql.Append("SELECT sum(preco) FROM TBLocacao where entrega is not null");

                    double result = conexao.QuerySingle<double>(Sql.ToString());

                    conexao.Close();

                    return result;
                }
                catch
                {
                    return 0;
                }
            }
        }

        public Locacao RetornarPorId(int id)
        {
            using (SqlConnection conexao = new SqlConnection(stringConexao))
            {
                try
                {
                    Sql.Clear();
                    Sql.Append("select TBCliente.nome as NomeCliente, TBLivro.nome as NomeLivro, TBLocacao.* ");
                    Sql.Append("from TBLocacao inner join TBCliente on TBLocacao.idcliente = TBCliente.id ");
                    Sql.Append("inner join TBLivro on TBLocacao.idlivro = TBLivro.id ");
                    Sql.Append("where TBLocacao.id=@id");

                    Locacao result = conexao.QueryFirst<Locacao>(Sql.ToString(), new { Id = id });

                    conexao.Close();

                    return result;
                }
                catch (Exception e)
                {
                    throw new Exception("Não foi possível buscar a locação por id!", e);
                }
            }
        }

        public void Inserir(Locacao obj)
        {
            using (SqlConnection conexao = new SqlConnection(stringConexao))
            {
                try
                {
                    Sql.Clear();
                    Sql.Append("insert into TBLocacao (id, idcliente, idlivro, data, entrega, preco) ");
                    Sql.Append("values (@id, @idcliente, @idlivro, @data, null, @preco)");

                    if (obj.Id == 0)
                        obj.Id = RetornarUltimoId() + 1;

                    conexao.Execute(Sql.ToString(), obj);

                    conexao.Close();
                }
                catch (Exception e)
                {
                    throw new Exception("Não foi possível inserir a locação!", e);
                }
            }
        }

        public void Finalizar(Locacao obj)
        {
            using (SqlConnection conexao = new SqlConnection(stringConexao))
            {
                try
                {
                    Sql.Clear();
                    Sql.Append("update TBLocacao set entrega=@entrega where id=@id");

                    conexao.Execute(Sql.ToString(), obj);

                    conexao.Close();
                }
                catch (Exception e)
                {
                    throw new Exception("Não foi possível finalizar a locação!", e);
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
                    Sql.Append("delete from TBLocacao where id=@id");

                    conexao.Execute(Sql.ToString(), new { Id = id });

                    conexao.Close();
                }
                catch (Exception e)
                {
                    throw new Exception("Não foi possível apagar a locação!", e);
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
                    Sql.Append("select max(id) from TBLocacao");

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