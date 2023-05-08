using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApplicationOld.Models;
using DBContextTeste;

namespace DBContextTeste
{
    class TestProdutoDbSet : TestDBSet<Produto>
    {
        public override Produto Find(params object[] keyValues)
        {
            return this.SingleOrDefault(prod => prod.id == (int)keyValues.Single());
        }
    }
}