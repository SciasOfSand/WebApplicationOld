using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Linq;
using System.Security.Policy;
using System.Web;
using System.Web.UI.WebControls.WebParts;
using WebApplicationOld.Models;
using WebApplicationOld.Contexto;

namespace WebApplicationOld.Contexto
{
    public class EFContext : DbContext, IEFContext
    {
        public EFContext() : base("connProd")
        {

        }

        public DbSet<Produto> Produtos { get; set; }
        public DbSet<Fabricante> Fabricantes { get; set; }

        public void MarkAsModified(Produto item)
        {
            Entry(item).State = EntityState.Modified;
        }

        public void MarkAsModified(Fabricante item)
        {
            Entry(item).State = EntityState.Modified;
        }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
        }
    }
}