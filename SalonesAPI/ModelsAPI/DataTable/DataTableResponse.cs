using System.Collections.Generic;

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
