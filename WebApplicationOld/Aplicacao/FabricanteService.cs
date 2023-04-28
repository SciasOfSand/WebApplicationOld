using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Dynamic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Web;
using WebApplicationOld.Models;
using WebApplicationOld.Contexto;
using System.Net;

namespace WebApplicationOld.Aplicacao
{
    public static class FabricanteService
    {
        public static List<Fabricante> GetFabricantes()
        {
            using (IEFContext db = new EFContext())
            {
                var list = db.Fabricantes.ToList();
                return list;
            }
        }

        public static Fabricante GetFabricante(int id)
        {
            using (IEFContext db = new EFContext())
            {
                return db.Fabricantes.Single(x => x.id.Equals(id));
            }
        }

        public static bool AddFabricante(Fabricante fabricante)
        {
            using (IEFContext db = new EFContext())
            {
                db.Fabricantes.Add(fabricante);
                return db.SaveChanges() > 0;
            }
        }

        public static bool EditFabricante(Fabricante fabricante)
        {
            using (IEFContext db = new EFContext())
            {
                Fabricante fab = db.Fabricantes.Find(fabricante.id);
                if (fab == null) return false;
                fab.nome = fabricante.nome;
                db.MarkAsModified(fab);
                return db.SaveChanges() > 0;
            }
        }

        public static bool DeleteFabricante(Fabricante fabricante)
        {
            using (IEFContext db = new EFContext())
            {
                var fab = db.Fabricantes.FirstOrDefault(x => x.id.Equals(fabricante.id));
                if (fab == null) return false;
                //if (fab.id == id) return db.SaveChanges() > 0;
                db.Fabricantes.Remove(fab);
                return db.SaveChanges() > 0;
            }
        }
    }
}