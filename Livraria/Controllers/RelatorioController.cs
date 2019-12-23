using Livraria.App_Start;
using Livraria.DAOs;
using PdfSharp;
using PdfSharp.Pdf;
using System;
using System.IO;
using System.Text;
using System.Web.Mvc;
using TheArtOfDev.HtmlRenderer.PdfSharp;

namespace Livraria.Controllers
{
    [Autenticacao]
    [HandleError(View = "Error", ExceptionType = typeof(InvalidOperationException))]
    public class RelatorioController : Controller
    {
        private readonly LocacaoDAO _locacaoDao = new LocacaoDAO();
        private readonly LivroDAO _livroDao = new LivroDAO();
        private readonly ClienteDAO _clienteDao = new ClienteDAO();

        // GET: Relatorio
        public ActionResult Index()
        {
            return View();
        }
        
        public void GerarPDF(string conteudo, string titulo)
        {
            PdfDocument document = PdfGenerator.GeneratePdf(conteudo, PageSize.A4);

            document.Info.Title = titulo;
            document.Info.Author = "Lucas Santos";
            document.Info.Subject = DateTime.Now.ToString();

            MemoryStream stream = new MemoryStream();
            document.Save(stream, false);
            Response.Clear();
            Response.ContentType = "application/pdf";
            Response.AddHeader("content-length", stream.Length.ToString());
            Response.BinaryWrite(stream.ToArray());
            Response.Flush();
            stream.Close();
            Response.End();
        }


        // GET: Relatorio/Lucros
        public void Lucros()
        {
            string titulo = "Relatório de Lucros de Locações";

            string conteudo =
                "<table class='table' cellspacing='5' width='100%'>" +
                "   <tr>" +
                "       <td><img src='C:\\Users\\l_san\\Documents\\GitHub\\Livraria\\Livraria\\Content\\Imagens\\logo-otacom.png' height='50' width='180' /></td>" +
                "       <td><h1>Relatório de Lucros de Locações</h1></td>" +
                "   </tr>" +
                "</table>" +
                "<br/>" +
                "<div style='margin-left:5%;>" +
                    "<h2>Lucros gerados: R$ " + @String.Format("{0:n}", _locacaoDao.Lucros()) + "</h2>" +
                "</div>" +
                "<p align='center'>Livraria Otacom - " + DateTime.Now.ToString() + "</p>";

            GerarPDF(conteudo, titulo);
        }

        // GET: Relatorio/TodosLivros
        public void TodosLivros()
        {
            string titulo = "Relatório de todos os livros";

            var livros = _livroDao.RetornarTodos();

            string cabecalho =
                "<table class='table' cellspacing='5' width='100%'>" +
                "   <tr>" +
                "       <td><img src='C:\\Users\\l_san\\Documents\\GitHub\\Livraria\\Livraria\\Content\\Imagens\\logo-otacom.png' height='50' width='180' /></td>" +
                "       <td><h1>Relatório de todos os livros</h1></td>" +
                "   </tr>" +
                "</table>" +
                "<br/>" +
                "<table class='table' align='center'  cellspacing='15' width='100%'>" +
                "   <tr>" +
                "       <th>Imagem</th>" +
                "       <th>Nome</th>" +
                "       <th>Autor</th>" +
                "       <th>Edicão</th>" +
                "       <th>IBSN</th>" +
                "   </tr>";

            StringBuilder myStringBuilder = new StringBuilder(cabecalho);            

            foreach (var item in livros)
            {
                myStringBuilder.Append("<tr>");
                myStringBuilder.Append("<td><img src='C:\\Users\\l_san\\Documents\\GitHub\\Livraria\\Livraria\\Content\\Imagens\\" + item.Imagem + "' height='80' width='60' /></td>");
                myStringBuilder.Append("<td>" + item.Nome + "</td>");
                myStringBuilder.Append("<td>" + item.Autor + "</td>");
                myStringBuilder.Append("<td>" + item.Edicao + "</td>");
                myStringBuilder.Append("<td>" + item.Inbs + "</td>");
                myStringBuilder.Append("</tr>");
            }

            string footer =
                "</table>" +
                "<br/>" +
                "<p align='center'>Livraria Otacom - " + DateTime.Now.ToString() + "</p>";

            myStringBuilder.Append(footer);

            GerarPDF(myStringBuilder.ToString(), titulo);
        }

        // GET: Relatorio/DisponiveisLivros
        public void DisponiveisLivros()
        {
            string titulo = "Relatório de livros disponíveis";

            var livros = _livroDao.RetornarNaoAlugados();

            string cabecalho = 
                "<table class='table' cellspacing='5' width='100%'>" +
                "   <tr>" +
                "       <td><img src='C:\\Users\\l_san\\Documents\\GitHub\\Livraria\\Livraria\\Content\\Imagens\\logo-otacom.png' height='50' width='180' /></td>" +
                "       <td><h1>Relatório de livros disponíveis</h1></td>" +
                "   </tr>" +
                "</table>" +
                "<br/>" +
                "<table class='table' align='center'  cellspacing='15' width='100%'>" +
                "   <tr>" +
                "       <th>Imagem</th>" +
                "       <th>Nome</th>" +
                "       <th>Autor</th>" +
                "       <th>Edicão</th>" +
                "       <th>IBSN</th>" +
                "   </tr>";

            StringBuilder myStringBuilder = new StringBuilder(cabecalho);

            foreach (var item in livros)
            {
                myStringBuilder.Append("<tr>");
                myStringBuilder.Append("<td><img src='C:\\Users\\l_san\\Documents\\GitHub\\Livraria\\Livraria\\Content\\Imagens\\" + item.Imagem + "' height='80' width='60' /></td>");
                myStringBuilder.Append("<td>" + item.Nome + "</td>");
                myStringBuilder.Append("<td>" + item.Autor + "</td>");
                myStringBuilder.Append("<td>" + item.Edicao + "</td>");
                myStringBuilder.Append("<td>" + item.Inbs + "</td>");
                myStringBuilder.Append("</tr>");
            }

            string footer =
                "</table>" +
                "<br/>" +
                "<p align='center'>Livraria Otacom - " + DateTime.Now.ToString() + "</p>";

            myStringBuilder.Append(footer);

            GerarPDF(myStringBuilder.ToString(), titulo);
        }

        // GET: Relatorio/AlugadosLivros
        public void AlugadosLivros()
        {
            string titulo = "Relatório de livros alugados";

            var livros = _livroDao.RetornarAlugados();

            string cabecalho =
                "<table class='table' cellspacing='5' width='100%'>" +
                "   <tr>" +
                "       <td><img src='C:\\Users\\l_san\\Documents\\GitHub\\Livraria\\Livraria\\Content\\Imagens\\logo-otacom.png' height='50' width='180' /></td>" +
                "       <td><h1>Relatório de livros alugados</h1></td>" +
                "   </tr>" +
                "</table>" +
                " <br/>" +
                "<table class='table' align='center'  cellspacing='15' width='100%'>" +
                "   <tr>" +
                "       <th>Imagem</th>" +
                "       <th>Nome</th>" +
                "       <th>Autor</th>" +
                "       <th>Edicão</th>" +
                "       <th>IBSN</th>" +
                "   </tr>";

            StringBuilder myStringBuilder = new StringBuilder(cabecalho);

            foreach (var item in livros)
            {
                myStringBuilder.Append("<tr>");
                myStringBuilder.Append("<td><img src='C:\\Users\\l_san\\Documents\\GitHub\\Livraria\\Livraria\\Content\\Imagens\\" + item.Imagem + "' height='80' width='60' /></td>");
                myStringBuilder.Append("<td>" + item.Nome + "</td>");
                myStringBuilder.Append("<td>" + item.Autor + "</td>");
                myStringBuilder.Append("<td>" + item.Edicao + "</td>");
                myStringBuilder.Append("<td>" + item.Inbs + "</td>");
                myStringBuilder.Append("</tr>");
            }

            string footer =
                "</table>" +
                "<br/>" +
                "<p align='center'>Livraria Otacom - " + DateTime.Now.ToString() + "</p>";

            myStringBuilder.Append(footer);

            GerarPDF(myStringBuilder.ToString(), titulo);
        }

        // GET: Relatorio/QuantidadeVezesAlugado
        public void QuantidadeVezesAlugado()
        {
            string titulo = "Relátorio da quantidade de vezes alugado";

            var livros = _livroDao.QuantidadeVezesAlugado();

            string cabecalho =
                "<table class='table' cellspacing='5' width='100%'>" +
                "   <tr>" +
                "       <td><img src='C:\\Users\\l_san\\Documents\\GitHub\\Livraria\\Livraria\\Content\\Imagens\\logo-otacom.png' height='50' width='180' /></td>" +
                "       <td><h1>Quantidade de vezes locado</h1></td>" +
                "   </tr>" +
                "</table>" +
                "<br/>" +
                "<table class='table' align='center'  cellspacing='15' width='100%'>" +
                "   <tr>" +
                "       <th>Imagem</th>" +
                "       <th>Nome</th>" +
                "       <th>Autor</th>" +
                "       <th>Edicão</th>" +
                "       <th>IBSN</th>" +
                "       <th>Vezes Locado</th>" +
                "   </tr>";

            StringBuilder myStringBuilder = new StringBuilder(cabecalho);

            foreach (var item in livros)
            {
                myStringBuilder.Append("<tr>");
                myStringBuilder.Append("<td><img src='C:\\Users\\l_san\\Documents\\GitHub\\Livraria\\Livraria\\Content\\Imagens\\" + item.Imagem + "' height='80' width='60' /></td>");
                myStringBuilder.Append("<td>" + item.Nome + "</td>");
                myStringBuilder.Append("<td>" + item.Autor + "</td>");
                myStringBuilder.Append("<td>" + item.Edicao + "</td>");
                myStringBuilder.Append("<td>" + item.Inbs + "</td>");
                myStringBuilder.Append("<td>" + item.VezesLocado + "</td>");
                myStringBuilder.Append("</tr>");
            }

            string footer =
                "</table>" +
                "<br/>" +
                "<p align='center'>Livraria Otacom - " + DateTime.Now.ToString() + "</p>";

            myStringBuilder.Append(footer);

            GerarPDF(myStringBuilder.ToString(), titulo);
        }

        // GET: Relatorio/Top5MaisAlugados
        public void Top5MaisAlugados()
        {
            string titulo = "Relatório dos top 5 mais alugados";

            var livros = _livroDao.Top5MaisAlugados();

            string cabecalho =
                "<table class='table' cellspacing='5' width='100%'>" +
                "   <tr>" +
                "       <td><img src='C:\\Users\\l_san\\Documents\\GitHub\\Livraria\\Livraria\\Content\\Imagens\\logo-otacom.png' height='50' width='180' /></td>" +
                "       <td><h1>Relatório dos top 5 mais locados</h1></td>" +
                "   </tr>" +
                "</table>" +
                "<br/>" +
                "<table class='table' align='center'  cellspacing='15' width='100%'>" +
                "   <tr>" +
                "       <th>Imagem</th>" +
                "       <th>Nome</th>" +
                "       <th>Autor</th>" +
                "       <th>Edicão</th>" +
                "       <th>IBSN</th>" +
                "       <th>Vezes Locado</th>" +
                "   </tr>";

            StringBuilder myStringBuilder = new StringBuilder(cabecalho);

            foreach (var item in livros)
            {
                myStringBuilder.Append("<tr>");
                myStringBuilder.Append("<td><img src='C:\\Users\\l_san\\Documents\\GitHub\\Livraria\\Livraria\\Content\\Imagens\\" + item.Imagem + "' height='80' width='60' /></td>");
                myStringBuilder.Append("<td>" + item.Nome + "</td>");
                myStringBuilder.Append("<td>" + item.Autor + "</td>");
                myStringBuilder.Append("<td>" + item.Edicao + "</td>");
                myStringBuilder.Append("<td>" + item.Inbs + "</td>");
                myStringBuilder.Append("<td>" + item.VezesLocado + "</td>");
                myStringBuilder.Append("</tr>");
            }

            string footer =
                "</table>" +
                "<br/>" +
                "<p align='center'>Livraria Otacom - " + DateTime.Now.ToString() + "</p>";

            myStringBuilder.Append(footer);

            GerarPDF(myStringBuilder.ToString(), titulo);
        }

        // GET: Relatorio/TodosClientes
        public void TodosClientes()
        {
            string titulo = "Relatório de todos clientes";

            var clientes = _clienteDao.RetornarTodos();

            string cabecalho =
                "<table class='table' cellspacing='5' width='100%'>" +
                "   <tr>" +
                "       <td><img src='C:\\Users\\l_san\\Documents\\GitHub\\Livraria\\Livraria\\Content\\Imagens\\logo-otacom.png' height='50' width='180' /></td>" +
                "       <td><h1>Relatório de todos clientes</h1></td>" +
                "   </tr>" +
                "</table>" +
                "<br/>" +
                "<table class='table' align='center'  cellspacing='15' width='100%'>" +
                "   <tr>" +
                "       <th>Nome</th>" +
                "       <th>Idade</th>" +
                "       <th>Sexo</th>" +
                "       <th>Telefone</th>" +
                "       <th>CPF</th>" +
                "   </tr>";

            StringBuilder myStringBuilder = new StringBuilder(cabecalho);

            foreach (var item in clientes)
            {
                myStringBuilder.Append("<tr>");
                myStringBuilder.Append("<td>" + item.Nome + "</td>");
                myStringBuilder.Append("<td>" + item.Idade + "</td>");
                myStringBuilder.Append("<td>" + item.Sexo + "</td>");
                myStringBuilder.Append("<td>" + item.Telefone + "</td>");
                myStringBuilder.Append("<td>" + item.Cpf + "</td>");
                myStringBuilder.Append("</tr>");
            }

            string footer =
                "</table>" +
                "<br/>" +
                "<p align='center'>Livraria Otacom - " + DateTime.Now.ToString() + "</p>";

            myStringBuilder.Append(footer);

            GerarPDF(myStringBuilder.ToString(), titulo);
        }
        
        // GET: Relatorio/TodasLocacoes
        public void TodasLocacoes()
        {
            string titulo = "Relatório de todas locações";

            var locacoes = _locacaoDao.RetornarTodos();

            string cabecalho =
                "<table class='table' cellspacing='5' width='100%'>" +
                "   <tr>" +
                "       <td><img src='C:\\Users\\l_san\\Documents\\GitHub\\Livraria\\Livraria\\Content\\Imagens\\logo-otacom.png' height='50' width='180' /></td>" +
                "       <td><h1>Relatório de todas locações</h1></td>" +
                "   </tr>" +
                "</table>" +
                "<br/>" +
                "<table class='table' align='center'  cellspacing='15' width='100%'>" +
                "   <tr>" +
                "       <th>Cliente</th>" +
                "       <th>Livro</th>" +
                "       <th>Data Locação</th>" +
                "       <th>Data Entrega</th>" +
                "       <th>Preço</th>" +
                "   </tr>";

            StringBuilder myStringBuilder = new StringBuilder(cabecalho);

            foreach (var item in locacoes)
            {
                myStringBuilder.Append("<tr>");
                myStringBuilder.Append("<td>" + item.NomeCliente + "</td>");
                myStringBuilder.Append("<td>" + item.NomeLivro + "</td>");
                myStringBuilder.Append("<td>" + item.Data.ToShortDateString() + "</td>");
                
				DateTime dataInv = Convert.ToDateTime("01/01/0001 00:00:00");
				int status = DateTime.Compare(item.Entrega, dataInv);
                if (Convert.ToBoolean(status))
                    myStringBuilder.Append("<td>" + item.Entrega.ToShortDateString() + "</td>");
                else
                    myStringBuilder.Append("<td>Em aberto</td>");

                myStringBuilder.Append("<td> R$ " + @String.Format("{0:n}", item.Preco) + "</td>");
                myStringBuilder.Append("</tr>");
            }

            string footer =
                "</table>" +
                "<br/>" +
                "<p align='center'>Livraria Otacom - " + DateTime.Now.ToString() + "</p>";

            myStringBuilder.Append(footer);

            GerarPDF(myStringBuilder.ToString(), titulo);
        }

        // GET: Relatorio/AbertoLocacoes
        public void AbertoLocacoes()
        {
            string titulo = "Relatório de locações em aberto";

            var locacoes = _locacaoDao.ListaAlugados();

            string cabecalho =
                "<table class='table' cellspacing='5' width='100%'>" +
                "   <tr>" +
                "       <td><img src='C:\\Users\\l_san\\Documents\\GitHub\\Livraria\\Livraria\\Content\\Imagens\\logo-otacom.png' height='50' width='180' /></td>" +
                "       <td><h1>Relatório de locações em aberto</h1></td>" +
                "   </tr>" +
                "</table>" +
                "<br/>" +
                "<table class='table' align='center'  cellspacing='15' width='100%'>" +
                "   <tr>" +
                "       <th>Cliente</th>" +
                "       <th>Livro</th>" +
                "       <th>Data Locação</th>" +
                "       <th>Data Entrega</th>" +
                "       <th>Preço</th>" +
                "   </tr>";

            StringBuilder myStringBuilder = new StringBuilder(cabecalho);

            foreach (var item in locacoes)
            {
                myStringBuilder.Append("<tr>");
                myStringBuilder.Append("<td>" + item.NomeCliente + "</td>");
                myStringBuilder.Append("<td>" + item.NomeLivro + "</td>");
                myStringBuilder.Append("<td>" + item.Data.ToShortDateString() + "</td>");

                DateTime dataInv = Convert.ToDateTime("01/01/0001 00:00:00");
                int status = DateTime.Compare(item.Entrega, dataInv);
                if (Convert.ToBoolean(status))
                    myStringBuilder.Append("<td>" + item.Entrega.ToShortDateString() + "</td>");
                else
                    myStringBuilder.Append("<td>Em aberto</td>");

                myStringBuilder.Append("<td> R$ " + @String.Format("{0:n}", item.Preco) + "</td>");
                myStringBuilder.Append("</tr>");
            }

            string footer =
                "</table>" +
                "<br/>" +
                "<p align='center'>Livraria Otacom - " + DateTime.Now.ToString() + "</p>";

            myStringBuilder.Append(footer);

            GerarPDF(myStringBuilder.ToString(), titulo);
        }

        // GET: Relatorio/FinalizadaLocacoes
        public void FinalizadaLocacoes()
        {
            string titulo = "Relatório de locações finalizadas";

            var locacoes = _locacaoDao.ListaEntregues();

            string cabecalho =
                "<table class='table' cellspacing='5' width='100%'>" +
                "   <tr>" +
                "       <td><img src='C:\\Users\\l_san\\Documents\\GitHub\\Livraria\\Livraria\\Content\\Imagens\\logo-otacom.png' height='50' width='180' /></td>" +
                "       <td><h1>Relatório de locações finalizadas</h1></td>" +
                "   </tr>" +
                "</table>" +
                "<br/>" +
                "<table class='table' align='center'  cellspacing='15' width='100%'>" +
                "   <tr>" +
                "       <th>Cliente</th>" +
                "       <th>Livro</th>" +
                "       <th>Data Locação</th>" +
                "       <th>Data Entrega</th>" +
                "       <th>Preço</th>" +
                "   </tr>";

            StringBuilder myStringBuilder = new StringBuilder(cabecalho);

            foreach (var item in locacoes)
            {
                myStringBuilder.Append("<tr>");
                myStringBuilder.Append("<td>" + item.NomeCliente + "</td>");
                myStringBuilder.Append("<td>" + item.NomeLivro + "</td>");
                myStringBuilder.Append("<td>" + item.Data.ToShortDateString() + "</td>");

                DateTime dataInv = Convert.ToDateTime("01/01/0001 00:00:00");
                int status = DateTime.Compare(item.Entrega, dataInv);
                if (Convert.ToBoolean(status))
                    myStringBuilder.Append("<td>" + item.Entrega.ToShortDateString() + "</td>");
                else
                    myStringBuilder.Append("<td>Em aberto</td>");

                myStringBuilder.Append("<td> R$ " + @String.Format("{0:n}", item.Preco) + "</td>");
                myStringBuilder.Append("</tr>");
            }

            string footer =
                "</table>" +
                "<br/>" +
                "<p align='center'>Livraria Otacom - " + DateTime.Now.ToString() + "</p>";

            myStringBuilder.Append(footer);

            GerarPDF(myStringBuilder.ToString(), titulo);
        }
    }
}