using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows.Data;
using WpfApp.Common;
using WpfApp.Model;

namespace WpfApp.ViewModel
{
    public class ProductViewModel: BaseModel
    {
        private string _searchFilter;
        private ObservableCollection<Product> _groupedProducts = new ObservableCollection<Product>();
        private readonly List<Product> _products = new List<Product>();
        private ObservableCollection<Product> _selectedProducts;
        private ICollectionView _productsView;
        public RelayCommand SelectProductCommand { get; private set; }
        public ObservableCollection<Product> GroupedProducts { get => _groupedProducts; set => SetField(ref _groupedProducts, value); }
        public ObservableCollection<Product> SelectedProducts { get => _selectedProducts; set => SetField(ref _selectedProducts, value); }
        public ObservableCollection<string> _businessLines = new ObservableCollection<string>();

        private string _selectedBusinessLine;
        public ProductViewModel()
        {
            SelectedProducts = new ObservableCollection<Product>();
            SelectProductCommand = new RelayCommand(SelectProduct);
            BusinessLines = ProductHelper.BusinessLines.ToObservableCollection();
            SelectedBusinessLine = BusinessLines.FirstOrDefault();
            _products = ProductHelper.GetProducts(new Random().Next(5, 15));
            foreach (var product in _products.Where(x=> x.IsSelected))
            {
                UpdateSelectedProducts(product);
            }
            FilterProducts();
            _productsView = CollectionViewSource.GetDefaultView(GroupedProducts);
            _productsView.Filter = (product) => ((Product)product).FilterBy(SearchFilter);
        }


        public string SearchFilter
        {
            get => _searchFilter;
            set { SetField(ref _searchFilter, value); _productsView?.Refresh(); }
        }

        private void SelectProduct(object obj)
        {
            if (obj is Product product)
            {
                UpdateSelectedProducts(product);
            }
        }

        public ObservableCollection<string> BusinessLines
        {
            get => _businessLines;
            set => SetField(ref _businessLines, value);
        }

        public string SelectedBusinessLine
        {
            get => _selectedBusinessLine;
            set 
            { 
                SetField(ref _selectedBusinessLine, value);
                FilterProducts();
            }
        }

        public void FilterProducts()
        {
            GroupedProducts.Clear();

            var allBusiness = "All Business Lines".Equals(SelectedBusinessLine, StringComparison.CurrentCultureIgnoreCase);

            _products.Where(p=> 
                (allBusiness || (!string.IsNullOrWhiteSpace(p.BusinessLine) && p.BusinessLine.Equals(SelectedBusinessLine, StringComparison.InvariantCultureIgnoreCase))) 
                && string.IsNullOrWhiteSpace(p.ParentId)).ForEach(GroupedProducts.Add);
            
            foreach (var product in GroupedProducts)
            {
                if (product != null)
                {
                    product.SubProducts = _products.Where(x => x.ParentId == product.ProductId).ToList();
                }
            }

        }

        public void UpdateSelectedProducts(Product product)
        {
            if (product.IsSelected)
            {
                product.SubProducts.Clear();
                SelectedProducts.Add(product);
            }
            else
            {
                SelectedProducts.Remove(product);
            }
        }
    }

    

}