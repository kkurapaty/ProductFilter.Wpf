using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;

namespace WpfApp.Model
{
    public interface IFilterable
    {
        bool FilterBy(string searchTerm);
    }
    public class Product: BaseModel,IFilterable
    {
        private int _id;
        private string _name;
        private string _productId;
        private string _parentId;
        private string _businessLine;
        private bool _enabled;
        private bool _isSelected;
        private List<Product> _subProducts = new List<Product>();

        public int Id
        {
            get => _id;
            set => SetField(ref _id, value);
        }
        public string Name { get => _name; set => SetField(ref _name, value); }
        public string ProductId { get => _productId; set => SetField(ref _productId, value); }
        public string ParentId { get => _parentId; set => SetField(ref _parentId, value); }
        public string BusinessLine { get => _businessLine; set => SetField(ref _businessLine, value); }

        public List<Product> SubProducts { get => _subProducts; set => SetField(ref _subProducts, value); }
        public bool Enabled { get => _enabled; set => SetField(ref _enabled, value); }
        public bool IsSelected { get => _isSelected; set => SetField(ref _isSelected, value); }

        public bool FilterBy(string searchTerm)
        {
            return (string.IsNullOrWhiteSpace(searchTerm) ||
                    (!string.IsNullOrEmpty(Name) && Name.IndexOf(searchTerm, StringComparison.OrdinalIgnoreCase) != -1) ||
                    (!string.IsNullOrEmpty(ProductId) && ProductId.IndexOf(searchTerm, StringComparison.OrdinalIgnoreCase) != -1) ||
                    (!string.IsNullOrEmpty(ParentId) && ParentId.IndexOf(searchTerm, StringComparison.OrdinalIgnoreCase) != -1) ||
                    (!string.IsNullOrEmpty(BusinessLine) && BusinessLine.IndexOf(searchTerm, StringComparison.OrdinalIgnoreCase) != -1) ||
                    (Id > 0 && Id.ToString().IndexOf(searchTerm, StringComparison.OrdinalIgnoreCase) != -1) ||
                    (SubProducts != null && SubProducts.Any(x=> x.FilterBy(searchTerm)))
                );
        }
    }
}
