using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfApp.Controls
{
    /// <summary>
    /// Interaction logic for ProductSelectionControl.xaml
    /// </summary>
    public partial class ProductSelectionControl : UserControl
    {
        public ProductSelectionControl()
        {
            InitializeComponent();
        }
        private void OpenPopup(object sender, RoutedEventArgs e)
        {
            IsPopupOpen = !IsPopupOpen;
        }

        public bool IsPopupOpen
        {
            get => (bool)GetValue(IsPopupOpenProperty);
            set => SetValue(IsPopupOpenProperty, value);
        }

        public static readonly DependencyProperty IsPopupOpenProperty =
            DependencyProperty.Register(nameof(IsPopupOpen), typeof(bool), typeof(ProductSelectionControl), new PropertyMetadata(false));

        private void OkCommandHandler(object sender, RoutedEventArgs e)
        {
            IsPopupOpen = false;
        }

        private void CancelCommandHandler(object sender, RoutedEventArgs e)
        {
            IsPopupOpen = false;
        }
    }
}

