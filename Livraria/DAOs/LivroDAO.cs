using Dapper;
using Livraria.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace Livraria.DAOs
{
    public class LivroDAO
    {
        private StringBuilder Sql = new StringBuilder();

        public IEnumerable<Livro> RetornarTodos()
        {
            using (SqlConnection conexao = new SqlConnection(stringConexao))
            {
                try
                {
                    Sql.Clear();
                    Sql.Append("select * from TBLivro order by TBLivro.nome");

                    IEnumerable<Livro> result = conexao.Query<Livro>(Sql.ToString());

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
            using (SqlConnection conexao = new SqlConnection(stringConexao))
            {
                try
                {
                    Sql.Clear();
                    Sql.Append("select TBLivro.* from TBLivro ");
                    Sql.Append("where TBLivro.nome like '%" + busca + "%' ");
                    Sql.Append("order by TBLivro.nome");

                    IEnumerable<Livro> result = conexao.Query<Livro>(Sql.ToString());

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
            using (SqlConnection conexao = new SqlConnection(stringConexao))
            {
                try
                {
                    Sql.Clear();
                    Sql.Append("select TBLivro.* ");
                    Sql.Append("from TBLocacao ");
                    Sql.Append("inner join TBLivro on TBLocacao.idlivro = TBLivro.id ");
                    Sql.Append("where TBLocacao.entrega is null");

                    IEnumerable<Livro> result = conexao.Query<Livro>(Sql.ToString());

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
            using (SqlConnection conexao = new SqlConnection(stringConexao))
            {
                try
                {
                    Sql.Clear();
                    Sql.Append("select TBLivro.id, TBLivro.nome, TBLivro.autor, TBLivro.edicao, TBLivro.inbs, TBLivro.imagem ");
                    Sql.Append("from TBLivro left join TBLocacao on TBLivro.id = TBLocacao.idlivro ");
                    Sql.Append("where (TBLocacao.data is null and TBLocacao.entrega is null) or ");
                    Sql.Append("TBLocacao.idlivro not in (select TBLocacao.idlivro from TBLocacao where TBLocacao.entrega is null) ");
                    Sql.Append("group by TBLivro.id, TBLivro.nome, TBLivro.autor, TBLivro.edicao, TBLivro.inbs, TBLivro.imagem ");
                    Sql.Append("order by TBLivro.nome");

                    IEnumerable<Livro> result = conexao.Query<Livro>(Sql.ToString());

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
            using (SqlConnection conexao = new SqlConnection(stringConexao))
            {
                try
                {
                    Sql.Clear();
                    Sql.Append("select TBLivro.*, COUNT(TBLivro.id) as VezesLocado ");
                    Sql.Append("from TBLivro inner join TBLocacao on TBLivro.id = TBLocacao.idlivro ");
                    Sql.Append("group by TBLivro.id, TBLivro.nome, TBLivro.autor, TBLivro.edicao, TBLivro.inbs, TBLivro.imagem");

                    IEnumerable<Livro> result = conexao.Query<Livro>(Sql.ToString());

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
            using (SqlConnection conexao = new SqlConnection(stringConexao))
            {
                try
                {
                    Sql.Clear();
                    Sql.Append("select top 5 TBLivro.*, COUNT(TBLivro.id) as VezesLocado ");
                    Sql.Append("from TBLivro inner join TBLocacao on TBLivro.id = TBLocacao.idlivro ");
                    Sql.Append("group by TBLivro.id, TBLivro.nome, TBLivro.autor, TBLivro.edicao, TBLivro.inbs, TBLivro.imagem ");
                    Sql.Append("order by VezesLocado desc");

                    IEnumerable<Livro> result = conexao.Query<Livro>(Sql.ToString());

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
            using (SqlConnection conexao = new SqlConnection(stringConexao))
            {
                try
                {
                    Sql.Clear();
                    Sql.Append("select * from TBLivro where id=@id");

                    Livro result = conexao.QueryFirst<Livro>(Sql.ToString(), new { Id = id });

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
            using (SqlConnection conexao = new SqlConnection(stringConexao))
            {
                try
                {
                    Sql.Clear();
                    Sql.Append("insert into TBLivro (id, nome, autor, edicao, inbs, imagem) values (@id, @nome, @autor, @edicao, @inbs, @imagem)");

                    if (obj.Id == 0)
                        obj.Id = RetornarUltimoId() + 1;

                    conexao.Execute(Sql.ToString(), obj);

                    conexao.Close();
                }
                catch (Exception e)
                {
                    throw new Exception("Não foi possível inserir livro!", e);
                }
            }
        }

        public void Alterar(Livro obj)
        {
            using (SqlConnection conexao = new SqlConnection(stringConexao))
            {
                try
                {
                    Sql.Clear();
                    Sql.Append("update TBLivro set nome=@nome, autor=@autor, edicao=@edicao, inbs=@inbs, imagem=@imagem where id=@id");

                    conexao.Execute(Sql.ToString(), obj);

                    conexao.Close();
                }
                catch (Exception e)
                {
                    throw new Exception("Não foi possível alterar livro!", e);
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
                    Sql.Append("delete from TBLivro where id=@id");

                    conexao.Execute(Sql.ToString(), new { Id = id });

                    conexao.Close();
                }
                catch (Exception e)
                {
                    throw new Exception("Não foi possível apagar livro!", e);
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
                    Sql.Append("select max(id) from TBLivro");

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