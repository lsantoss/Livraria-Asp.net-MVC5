using Dapper;
using Livraria.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace Livraria.DAOs
{
    public class LivroDAO
    {
        public IEnumerable<Livro> RetornarTodos()
        {
            using (var conexao = new SqlConnection(connStr))
            {
                try
                {
                    var result = conexao.Query<Livro>("select * from TBLivro order by TBLivro.nome");
                    conexao.Close();
                    return result;
                }
                catch (Exception e)
                {
                    throw new Exception("Não foi possível buscar todos livros!", e);
                }
            }
        }

        public IEnumerable<Livro> RetornarPorNome(string busca)
        {
            using (var conexao = new SqlConnection(connStr))
            {
                try
                {
                    var result = conexao.Query<Livro>(
                        "select TBLivro.* from TBLivro " +
                        "where TBLivro.nome like '%"+ busca + "%' " +
                        "order by TBLivro.nome");
                    conexao.Close();
                    return result;
                }
                catch (Exception e)
                {
                    throw new Exception("Não foi possível buscar livros pelo nome!", e);
                }
            }
        }

        public IEnumerable<Livro> RetornarAlugados()
        {
            using (var conexao = new SqlConnection(connStr))
            {
                try
                {
                    var result = conexao.Query<Livro>(
                        "select TBLivro.* " +
                        "from TBLocacao " +
                        "inner join TBLivro on TBLocacao.idlivro = TBLivro.id " +
                        "where TBLocacao.entrega is null");
                    conexao.Close();
                    return result;
                }
                catch (Exception e)
                {
                    throw new Exception("Não foi possível buscar os livros alugados!", e);
                }
            }
        }

        public IEnumerable<Livro> RetornarNaoAlugados()
        {
            using (var conexao = new SqlConnection(connStr))
            {
                try
                {
                    var result = conexao.Query<Livro>(
                        "select TBLivro.id, TBLivro.nome, TBLivro.autor, TBLivro.edicao, TBLivro.inbs, TBLivro.imagem " +
                        "from TBLivro left join TBLocacao on TBLivro.id = TBLocacao.idlivro " +
                        "where (TBLocacao.data is null and TBLocacao.entrega is null) or " +
                        "TBLocacao.idlivro not in (select TBLocacao.idlivro from TBLocacao where TBLocacao.entrega is null) " +
                        "group by TBLivro.id, TBLivro.nome, TBLivro.autor, TBLivro.edicao, TBLivro.inbs, TBLivro.imagem " +
                        "order by TBLivro.nome");
                    conexao.Close();
                    return result;
                }
                catch (Exception e)
                {
                    throw new Exception("Não foi possível buscar os livros disponíveis!", e);
                }
            }
        }
        public IEnumerable<Livro> QuantidadeVezesAlugado()
        {
            using (var conexao = new SqlConnection(connStr))
            {
                try
                {
                    var result = conexao.Query<Livro>(
                        "select TBLivro.*, COUNT(TBLivro.id) as VezesLocado " +
                        "from TBLivro inner join TBLocacao on TBLivro.id = TBLocacao.idlivro " +
                        "group by TBLivro.id, TBLivro.nome, TBLivro.autor, TBLivro.edicao, TBLivro.inbs, TBLivro.imagem");
                    conexao.Close();
                    return result;
                }
                catch (Exception e)
                {
                    throw new Exception("Não foi possível buscar a quantidade de vezes que o livro foi alugado!", e);
                }
            }
        }

        public IEnumerable<Livro> Top5MaisAlugados()
        {
            using (var conexao = new SqlConnection(connStr))
            {
                try
                {
                    var result = conexao.Query<Livro>(
                        "select top 5 TBLivro.*, COUNT(TBLivro.id) as VezesLocado " +
                        "from TBLivro inner join TBLocacao on TBLivro.id = TBLocacao.idlivro " +
                        "group by TBLivro.id, TBLivro.nome, TBLivro.autor, TBLivro.edicao, TBLivro.inbs, TBLivro.imagem " +
                        "order by VezesLocado desc");
                    conexao.Close();
                    return result;
                }
                catch (Exception e)
                {
                    throw new Exception("Não foi possível buscar o top 5 livros mais alugados!", e);
                }
            }
        }

        public Livro RetornarPorId(int id)
        {
            using (var conexao = new SqlConnection(connStr))
            {
                try
                {
                    var result = conexao.QueryFirst<Livro>("select * from TBLivro where id=@id", new { Id = id });
                    conexao.Close();
                    return result;
                }
                catch (Exception e)
                {
                    throw new Exception("Não foi possível buscar livro por id!", e);
                }
            }
        }

        public void Inserir(Livro obj)
        {
            using (var conexao = new SqlConnection(connStr))
            {
                try
                {
                    if (obj.Id == 0)
                        obj.Id = RetornarUltimoId() + 1;

                    conexao.Execute("insert into TBLivro (id, nome, autor, edicao, inbs, imagem) values (@id, @nome, @autor, @edicao, @inbs, @imagem)", obj);
                    conexao.Close();
                }
                catch (Exception e)
                {
                    throw new Exception("Não foi possível inserir livro!", e);
                }
            }
        }

        public void Deletar(int id)
        {
            using (var conexao = new SqlConnection(connStr))
            {
                try
                {
                    conexao.Execute("delete from TBLivro where id=@id", new { Id = id });
                    conexao.Close();
                }
                catch (Exception e)
                {
                    throw new Exception("Não foi possível apagar livro!", e);
                }
            }
        }

        public void Alterar(Livro obj)
        {
            using (var conexao = new SqlConnection(connStr))
            {
                try
                {
                    conexao.Execute("update TBLivro set nome=@nome, autor=@autor, edicao=@edicao, inbs=@inbs, imagem=@imagem where id=@id", obj);
                    conexao.Close();
                }
                catch (Exception e)
                {
                    throw new Exception("Não foi possível alterar livro!", e);
                }
            }
        }

        private int RetornarUltimoId()
        {
            using (var conexao = new SqlConnection(connStr))
            {
                try
                {
                    var result = conexao.QuerySingle<int>("select max(id) from TBLivro");
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