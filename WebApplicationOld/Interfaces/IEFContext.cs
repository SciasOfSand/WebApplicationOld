using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApplicationOld.Models;

namespace WebApplicationOld.Contexto
{
    public interface IEFContext : IDisposable
    {
        DbSet<Produto> Produtos { get; }
        DbSet<Fabricante> Fabricantes { get; }

        int SaveChanges();
        void MarkAsModified(Produto produto);
        void MarkAsModified(Fabricante fabricante);
    }
}
