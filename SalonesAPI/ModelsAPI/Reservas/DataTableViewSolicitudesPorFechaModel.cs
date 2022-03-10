using System.Collections.Generic;

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
