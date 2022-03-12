using System.Collections.Generic;

namespace SalonesAPI.ModelsAPI.DataTable
{
    public class DataTableParameter
    {
        public int draw { get; set; }
        public int start { get; set; }
        public int length { get; set; }
        public List<Column> columns { get; set; }
        public Search search { get; set; }
        public List<Order> order { get; set; }

        public string fechaDesde { get; set; }
        public string fechaHasta { get; set; }

    }
}
