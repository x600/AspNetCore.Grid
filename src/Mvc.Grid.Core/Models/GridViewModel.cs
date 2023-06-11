using System.Collections.Generic;
using System.Linq;

namespace NonFactors.Mvc.Grid
{
    public class GridViewModel<T>
    {
        public List<T> ListItems { get; set; } = new List<T>();

        public IQueryable<T> DataSource { get => ListItems.AsQueryable(); }

        public string EmptyGridText { get; set; } = "No items";

        public string Name { get; set; }

        public GridViewModel(string name)
        {
            Name = name;
        }

        public int VirtualRowCount = 0;

        public bool ShowPageSizes { get; set; } 

        //public bool UseCustomPaging {get; set; }
    }
}
