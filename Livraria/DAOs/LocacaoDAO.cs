using Dapper;
using Livraria.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace Livraria.DAOs
{
    public class LocacaoDAO
    {
        public IEnumerable<Locacao> RetornarTodos()
        {
            using (var conexao = new SqlConnection(connStr))
            {
                try
                {
                    var result = conexao.Query<Locacao>(
                        "select TBCliente.nome as NomeCliente, TBLivro.nome as NomeLivro, TBLocacao.* " +
                        "from TBLocacao inner join TBCliente on TBLocacao.idcliente = TBCliente.id " +
                        "inner join TBLivro on TBLocacao.idlivro = TBLivro.id");
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
            using (var conexao = new SqlConnection(connStr))
            {
                try
                {
                    var result = conexao.Query<Locacao>(
                        "select TBCliente.nome as NomeCliente, TBLivro.nome as NomeLivro, TBLocacao.* " +
                        "from TBLocacao inner join TBCliente on TBLocacao.idcliente = TBCliente.id " +
                        "inner join TBLivro on TBLocacao.idlivro = TBLivro.id " +
                        "where TBCliente.nome like '%"+ busca + "%' or TBLivro.nome like '%"+ busca + "%'");
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
            using (var conexao = new SqlConnection(connStr))
            {
                try
                {
                    var result = conexao.Query<Locacao>(
                        "select TBCliente.nome as NomeCliente, TBLivro.nome as NomeLivro, TBLocacao.* " +
                        "from TBLocacao inner join TBCliente on TBLocacao.idcliente = TBCliente.id " +
                        "inner join TBLivro on TBLocacao.idlivro = TBLivro.id " +
                        "where TBLocacao.entrega is null and (TBCliente.nome like '%" + busca + "%' or TBLivro.nome like '%" + busca + "%')");
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
            using (var conexao = new SqlConnection(connStr))
            {
                try
                {
                    var result = conexao.Query<Locacao>(
                        "select TBCliente.nome as NomeCliente, TBLivro.nome as NomeLivro, TBLocacao.* " +
                        "from TBLocacao inner join TBCliente on TBLocacao.idcliente = TBCliente.id " +
                        "inner join TBLivro on TBLocacao.idlivro = TBLivro.id " +
                        "where TBLocacao.entrega is not null and (TBCliente.nome like '%" + busca + "%' or TBLivro.nome like '%" + busca + "%')");
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
            using (var conexao = new SqlConnection(connStr))
            {
                try
                {
                    var result = conexao.Query<Locacao>(
                        "select TBCliente.nome as NomeCliente, TBLivro.nome as NomeLivro, TBLocacao.* " +
                        "from TBLocacao inner join TBCliente on TBLocacao.idcliente = TBCliente.id " +
                        "inner join TBLivro on TBLocacao.idlivro = TBLivro.id " +
                        "where TBLocacao.entrega is null");
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
            using (var conexao = new SqlConnection(connStr))
            {
                try
                {
                    var result = conexao.Query<Locacao>(
                        "select TBCliente.nome as NomeCliente, TBLivro.nome as NomeLivro, TBLocacao.* " +
                        "from TBLocacao inner join TBCliente on TBLocacao.idcliente = TBCliente.id " +
                        "inner join TBLivro on TBLocacao.idlivro = TBLivro.id " +
                        "where TBLocacao.entrega is not null");
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
            using (var conexao = new SqlConnection(connStr))
            {
                try
                {
                    var result = conexao.QuerySingle<double>("SELECT sum(preco) FROM TBLocacao where entrega is not null");
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
            using (var conexao = new SqlConnection(connStr))
            {
                try
                {
                    var result = conexao.QueryFirst<Locacao>(
                        "select TBCliente.nome as NomeCliente, TBLivro.nome as NomeLivro, TBLocacao.* " +
                        "from TBLocacao inner join TBCliente on TBLocacao.idcliente = TBCliente.id " +
                        "inner join TBLivro on TBLocacao.idlivro = TBLivro.id " +
                        "where TBLocacao.id=@id", new { Id = id });
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
            using (var conexao = new SqlConnection(connStr))
            {
                try
                {
                    if (obj.Id == 0)
                        obj.Id = RetornarUltimoId() + 1;

                    conexao.Execute("insert into TBLocacao (id, idcliente, idlivro, data, entrega, preco) values (@id, @idcliente, @idlivro, @data, null, @preco)", obj);
                    conexao.Close();
                }
                catch (Exception e)
                {
                    throw new Exception("Não foi possível inserir a locação!", e);
                }
            }
        }

        public void Deletar(int id)
        {
            using (var conexao = new SqlConnection(connStr))
            {
                try
                {
                    conexao.Execute("delete from TBLocacao where id=@id", new { Id = id });
                    conexao.Close();
                }
                catch (Exception e)
                {
                    throw new Exception("Não foi possível apagar a locação!", e);
                }
            }
        }

        public void Finalizar(Locacao obj)
        {
            using (var conexao = new SqlConnection(connStr))
            {
                try
                {
                    conexao.Execute("update TBLocacao set entrega=@entrega where id=@id", obj);
                    conexao.Close();
                }
                catch (Exception e)
                {
                    throw new Exception("Não foi possível finalizar a locação!", e);
                }
            }
        }

        private int RetornarUltimoId()
        {
            using (var conexao = new SqlConnection(connStr))
            {
                try
                {
                    var result = conexao.QuerySingle<int>("select max(id) from TBLocacao");
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