using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApplicationOld.Models;
using DBContextTeste;

namespace DBContextTeste
{
    class TestFabricanteDbSet : TestDBSet<Fabricante>
    {
        public override Fabricante Find(params object[] keyValues)
        {
            return this.SingleOrDefault(fab => fab.id == (int)keyValues.Single());
        }
    }
}