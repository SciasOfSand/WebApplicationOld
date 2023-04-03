using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApplicationOld.Models;
using WebApplicationOld.Contexto;

namespace DBContextTeste
{
    public class TestDBContext : DbContext, IEFContext
    {
        public TestDBContext() :base("connTest")
        {
            this.Produtos = new TestProdutoDbSet();
            this.Fabricantes = new TestFabricanteDbSet();
        }

        public DbSet<Produto> Produtos { get; set; }
        public DbSet<Fabricante> Fabricantes { get; set; }

        public int SaveChanges()
        {
            return 0;
        }

        public void MarkAsModified(Produto item) { }
        public void MarkAsModified(Fabricante item) { }

        public void Dispose() { }
    }
}
