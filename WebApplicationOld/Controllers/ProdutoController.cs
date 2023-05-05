using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web.Http;
using WebApplicationOld.Contexto;
using WebApplicationOld.Models;
using WebApplicationOld.Aplicacao;

namespace WebApplicationOld.Controllers
{
    public class ProdutoController : ApiController
    {
        public ProdutoController()
        {
            this.Request = new HttpRequestMessage();
            this.Configuration = new HttpConfiguration();
        }
        [HttpGet]
        public HttpResponseMessage Get()
        {
            /*
            List<Produto> prods;
            using (var db = dbContext)
            {

                var test = from a in db.Produtos select a;
                prods = test.ToList();
            }
            return prods;
            */
            List<Produto> prod = ProdutoService.GetProdutos();
            if (prod.Count == 0)
                return Request.CreateResponse(HttpStatusCode.NoContent);
            return Request.CreateResponse(HttpStatusCode.OK, prod);
        }

        //public void Post(int id, string name, string type, int fabId)
        [HttpPost]
        public HttpResponseMessage Post(Produto produto)
        {
            if (produto == null)
                return Request.CreateErrorResponse(HttpStatusCode.NoContent, "Erro: Produto nao pode ser null.");
            if (produto.nome == null)
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Erro: Nome nao pode ser null.");
            ProdutoService.AddProduto(produto);
            return Request.CreateResponse(HttpStatusCode.OK);
        }
        [HttpPut]
        public HttpResponseMessage Put(Produto produto)
        {
            if (produto == null)
                return Request.CreateErrorResponse(HttpStatusCode.NoContent, "Erro: Produto nao pode ser null.");
            if (produto.id == 0 || produto == null || produto.id_fabricante == 0)
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Erro: todos os dados sao necessarios.");
            var update = ProdutoService.EditProduto(produto);
            if (update == false)
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Erro ao salvar (Produto existe?).");
            return Request.CreateResponse(HttpStatusCode.OK);
        }

        [HttpDelete]
        public HttpResponseMessage Delete(int id)
        {
            if (ProdutoService.GetProdutos().Count == 0)
                return Request.CreateErrorResponse(HttpStatusCode.NoContent, "Banco Vazio.");
            var del = ProdutoService.DeleteProduto(id);
            if (del == false)
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Erro ao salvar (Produto existe?).");
            return Request.CreateResponse(HttpStatusCode.OK);
        }
    }
}
