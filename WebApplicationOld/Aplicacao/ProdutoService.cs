using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApplicationOld.Contexto;
using WebApplicationOld.Models;

namespace WebApplicationOld.Aplicacao
{
    public static class ProdutoService
    {
        private static readonly IEFContext db = new EFContext();
        public static List<Produto> GetProdutos()
        {
            using (IEFContext db = new EFContext())
            {
                var list = db.Produtos.ToList();
                return list;
            }
        }

        public static Produto GetProduto(int id)
        {
            using (IEFContext db = new EFContext())
            {
                var prod = db.Produtos.FirstOrDefault(x => x.id.Equals(id));
                return prod;
            }
        }

        public static bool AddProduto(Produto produto)
        {
            using (IEFContext db = new EFContext())
            {
                db.Produtos.Add(produto);
                return db.SaveChanges() > 0;
            }
        }

        public static bool EditProduto(Produto produto)
        {
            using (IEFContext db = new EFContext())
            {
                Produto prod = db.Produtos.Find(produto.id);
                if (prod == null) return false;
                prod.nome = produto.nome;
                prod.tipo = produto.tipo;
                prod.id_fabricante = produto.id_fabricante;
                db.MarkAsModified(prod);
                return db.SaveChanges() > 0;
            }
        }

        public static bool DeleteProduto(int id)
        {
            using (IEFContext db = new EFContext())
            {
                var prod = db.Produtos.FirstOrDefault(x => x.id.Equals(id));
                if (prod == null) return false;
                db.Produtos.Remove(prod);
                return db.SaveChanges() > 0;
            }
        }
    }
}