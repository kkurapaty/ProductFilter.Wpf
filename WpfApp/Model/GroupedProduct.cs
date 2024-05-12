using System.Collections.Generic;

namespace WpfApp.Model
{
    public class GroupedProduct
    {
        public Product ParentProduct { get; set; }
        public List<Product> SubProducts { get; set; }
    }
}