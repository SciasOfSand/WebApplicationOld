using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Net.Http.Headers;
using WebApplicationOld.Contexto;
using WebApplicationOld.Models;
using System.Data.Entity;
using System.Text.Json;
using WebApplicationOld.Aplicacao;

namespace WebApplicationOld.Controllers
{
    /// <summary>
    /// FabricanteController
    /// </summary>
    public class FabricanteController : ApiController
    {
        /// <summary>
        /// Construtor do FabricanteController
        /// </summary>
        public FabricanteController()
        {
            this.Request = new HttpRequestMessage();
            this.Configuration = new HttpConfiguration();

        }
        /// <summary>
        /// Método GET de Fabricantes
        /// </summary>
        /// <remarks>Obtém a lista de Fabricantes</remarks>
        /// <returns></returns>
        /// <response code="200"></response>
        [HttpGet]
        public HttpResponseMessage Get()
        {
            List<Fabricante> fab = FabricanteService.GetFabricantes();
            if (fab.Count == 0)
                return Request.CreateResponse(HttpStatusCode.NoContent);

            return Request.CreateResponse(HttpStatusCode.OK, fab);
        }
        /// <summary>
        /// Método POST de Fabricantes
        /// </summary>
        /// <remarks>
        /// Adiciona um novo fabricante no Banco
        /// Exemplo de entrada:
        /// 
        /// POST /Todo
        /// {
        ///     "nome": "nome_do_fabricante"
        /// }
        /// </remarks>
        /// <response code="200"></response>
        [HttpPost]
        public HttpResponseMessage Post(Fabricante fabricante)
        {
            if (fabricante == null)
                return Request.CreateErrorResponse(HttpStatusCode.NoContent, "Erro: Fabricante nao pode ser null.");
            if (fabricante.nome == null)
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Erro: Nome nao pode ser null.");
            FabricanteService.AddFabricante(fabricante);
            return Request.CreateResponse(HttpStatusCode.OK);

        }
        /// <summary>
        /// Método POST de Fabricantes
        /// </summary>
        /// <remarks>
        /// Altera dados de um fabricante no Banco
        /// Exemplo de entrada:
        /// 
        /// PUT /Todo
        /// {
        ///     "id" : id_do_fabricante
        ///     "nome": "novo_nome_do_fabricante"
        /// }
        /// </remarks>
        /// <response code="200"></response>
        [HttpPut]
        public HttpResponseMessage Put(Fabricante fabricante)
        {
            if (fabricante == null)
                return Request.CreateErrorResponse(HttpStatusCode.NoContent, "Erro: Fabricante nao pode ser null.");
            if (fabricante.id == 0 || fabricante.nome == null)
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Erro: todos os dados sao necessarios.");
            var update = FabricanteService.EditFabricante(fabricante);
            if (update == false)
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Erro ao salvar (Fabricante existe?).");
            return Request.CreateResponse(HttpStatusCode.OK);
        }
        /// <summary>
        /// Método POST de Fabricantes
        /// </summary>
        /// <remarks>
        /// Remove o fabricante do Banco
        /// </remarks>
        /// <response code="200"></response>
        [HttpDelete]
        public HttpResponseMessage Delete(int id)
        {
            if (FabricanteService.GetFabricantes().Count() == 0)
                return Request.CreateErrorResponse(HttpStatusCode.NoContent, "Banco Vazio.");
            var del = FabricanteService.DeleteFabricante(id);
            if (del == false)
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Erro ao remover (Fabricante existe?");
            return Request.CreateResponse(HttpStatusCode.OK);
        }
    }
}
