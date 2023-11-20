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

namespace AflyatunovGlazki_saave
{
    /// <summary>
    /// Логика взаимодействия для ProductPage.xaml
    /// </summary>
    public partial class ProductPage : Page
    {
        public ProductPage()
        {
            InitializeComponent();

            var currentServices = Aflyatunov_glazkiEntities.GetContext().Agent.ToList();

            ServiceListView.ItemsSource = currentServices;

            ComboTypeSort.SelectedIndex = 0;
            ComboTypeAgTy.SelectedIndex = 0;

            UpdateProduct();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Manager.MainFrame.Navigate(new AddEditPage());
        }

        private void TBoxSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            UpdateProduct();
        }
        private void ComboTypeAgTy_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateProduct();
        }

        private void ComboTypeSort_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateProduct();
        }

        private void UpdateProduct()
        {
            var currentAgent = Aflyatunov_glazkiEntities.GetContext().Agent.ToList();

            if (ComboTypeSort.SelectedIndex == 1)
            {
                currentAgent = currentAgent.OrderBy(p => p.Title).ToList();
            }
            if (ComboTypeSort.SelectedIndex == 2)
            {
                currentAgent = currentAgent.OrderByDescending(p => p.Title).ToList();
            }
            if (ComboTypeSort.SelectedIndex == 3)
            {
                currentAgent = currentAgent.OrderBy(p => p.Priority).ToList();             
            }
            if (ComboTypeSort.SelectedIndex == 4)
            {
                currentAgent = currentAgent.OrderByDescending(p => p.Priority).ToList();
            }

            currentAgent = currentAgent.Where(p => p.Title.ToLower().Contains(TBoxSearch.Text.ToLower()) || p.Email.ToLower().Contains(TBoxSearch.Text.ToLower()) || p.Phone.Replace("+", "").Replace(" ", "").Replace("-", "").Replace("(", "").Replace(")", "").ToLower().Contains(TBoxSearch.Text.ToLower())).ToList();


            if (ComboTypeAgTy.SelectedIndex == 1)
            {
                currentAgent = currentAgent.Where(p => p.AgentTypeID == 1).ToList();
            }
            if (ComboTypeAgTy.SelectedIndex == 2)
            {
                currentAgent = currentAgent.Where(p => p.AgentTypeID == 2).ToList();
            }
            if (ComboTypeAgTy.SelectedIndex == 3)
            {
                currentAgent = currentAgent.Where(p => p.AgentTypeID == 3).ToList();
            }
            if (ComboTypeAgTy.SelectedIndex == 4)
            {
                currentAgent = currentAgent.Where(p => p.AgentTypeID == 4).ToList();
            }
            if (ComboTypeAgTy.SelectedIndex == 5)
            {
                currentAgent = currentAgent.Where(p => p.AgentTypeID == 5).ToList();
            }
            if (ComboTypeAgTy.SelectedIndex == 6)
            {
                currentAgent = currentAgent.Where(p => p.AgentTypeID == 6).ToList();
            }

            ServiceListView.ItemsSource = currentAgent;

        }
    }
}
