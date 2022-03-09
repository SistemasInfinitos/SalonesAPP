using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SalonesAPI.ModelsAPI.DataTable
{
    public struct DataTableResponse
    {
        public int draw;
        public int recordsTotal;
        public int recordsFiltered;
        public List<Personas> data;
    }
}
