using SalonesAPI.ModelsAPI.Persona;
using System.Collections.Generic;

namespace SalonesAPI.ModelsAPI.DataTable.Persona
{
    public partial struct DataTableResponsePersona
    {
        public int draw;
        public int recordsTotal;
        public int recordsFiltered;
        public List<Personas> data;
    }
}
