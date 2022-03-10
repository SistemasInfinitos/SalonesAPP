using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SalonesAPI.ModelsAPI.Reservas
{
    public struct DataTableViewSolicitudesPorFechaModel
    {
        public int draw;
        public int recordsTotal;
        public int recordsFiltered;
        public List<ViewSolicitudesPorFechaModel> data;
    }
}
