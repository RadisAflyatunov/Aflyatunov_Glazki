using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
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
    /// Логика взаимодействия для AddSalesPage.xaml
    /// </summary>
    public partial class AddSalesPage : Page
    {
        private ProductSale currentProductSale = new ProductSale();
        private List<Product> currentProduct;
        private Agent currentAgent = new Agent();

        public AddSalesPage(Agent agent)
        {
            InitializeComponent();
            currentAgent = agent;
            currentProduct = Aflyatunov_glazkiEntities.GetContext().Product.ToList();
            ComboProduct.ItemsSource = currentProduct;

            DataContext = currentProductSale;
            UpdateAgent();
        }


        public void UpdateAgent()
        {
            //var currentProduct = Aflyatunov_glazkiEntities.GetContext().ProductSale.ToList();
            currentProduct = currentProduct.Where(p => p.Title.ToLower().Contains(ComboProduct.Text.ToLower())).ToList();
            ComboProduct.ItemsSource = currentProduct;
        }

        private void AddProdHistoryBtn_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder errors = new StringBuilder();
            if (currentProductSale.ID == 0)
            {
                Aflyatunov_glazkiEntities.GetContext().ProductSale.Add(currentProductSale);
            }
            
            if (string.IsNullOrWhiteSpace(ProdCount.Text))
            {
                errors.AppendLine("Укажите количество");
            }
            else
            {
                int c = Convert.ToInt32(ProdCount.Text);
                if (c < 1)
                    errors.AppendLine("Укажите количество");
            }
            if (StartDate.Text == "")
                errors.AppendLine("Укажите дату");
            if (ComboProduct.SelectedItem == null)
                errors.AppendLine("Укажите наименование продукта");
            if (errors.Length > 0)
            {
                MessageBox.Show(errors.ToString());
                return;
            }

            currentProductSale.ProductID = ComboProduct.SelectedIndex + 51;
            currentProductSale.AgentID = currentAgent.ID;
            currentProductSale.ProductCount = Convert.ToInt32(ProdCount.Text);
            currentProductSale.SaleDate = Convert.ToDateTime(StartDate.Text);


            if (currentProductSale.ID == 0)
            {
                Aflyatunov_glazkiEntities.GetContext().ProductSale.Add(currentProductSale);
                try
                {
                    Aflyatunov_glazkiEntities.GetContext().SaveChanges();
                    MessageBox.Show("информация сохранена");
                    Manager.MainFrame.GoBack();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString());
                }
            }
        }
    }
}
