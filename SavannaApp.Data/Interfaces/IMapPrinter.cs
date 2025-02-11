using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SavannaApp.Data.Interfaces
{
    public interface IMapPrinter
    {
        void PrintMap(string header, IMap map);
    }
}
