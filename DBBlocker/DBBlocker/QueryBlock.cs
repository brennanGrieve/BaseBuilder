using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBBlocker
{
    interface IQueryBlock
    {
        String ExtractSQL();
    }
}
