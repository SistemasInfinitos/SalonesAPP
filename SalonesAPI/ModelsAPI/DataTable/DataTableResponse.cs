using SalonesAPI.ModelsAPI.Persona;
using System.Collections.Generic;

namespace SalonesAPI.ModelsAPI.DataTable
{
    public partial struct DataTableResponse
    {
        public int draw;
        public int recordsTotal;
        public int recordsFiltered;
        public List<Personas> data;
    }
}
