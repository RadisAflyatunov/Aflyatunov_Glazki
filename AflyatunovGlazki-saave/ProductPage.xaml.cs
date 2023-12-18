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
        int CountRecords; //Кол-во записей в таблице
        int CountPage; //Общее кол-во страниц
        int CurrentPage = 0; //Текущая страница

        List<Agent> CurrentPageList = new List<Agent>();
        List<Agent> TableList;
        public ProductPage()
        {
            InitializeComponent();
            List<Agent> currentAgents = Aflyatunov_glazkiEntities.GetContext().Agent.ToList();

            ServiceListView.ItemsSource = currentAgents;

            ComboTypeSort.SelectedIndex = 0;
            ComboTypeAgTy.SelectedIndex = 0;

            UpdateProduct();
        }



        private void ChangePage(int direction, int? selectedPage)
        {
            CurrentPageList.Clear();
            CountRecords = TableList.Count;

            if (CountRecords % 10 > 0)
            {
                CountPage = CountRecords / 10 + 1;
            }
            else
            {
                CountPage = CountRecords / 10;
            }

            Boolean Ifupdate = true;

            int min;

            if (selectedPage.HasValue)
            {
                if (selectedPage >= 0 && selectedPage <= CountPage)
                {
                    CurrentPage = (int)selectedPage;
                    min = CurrentPage * 10 + 10 < CountRecords ? CurrentPage * 10 + 10 : CountRecords;
                    for (int i = CurrentPage * 10; i < min; i++)
                    {
                        CurrentPageList.Add(TableList[i]);
                    }
                }
            }
            else
            {
                switch (direction)
                {
                    case 1:
                        if (CurrentPage > 0)
                        {
                            CurrentPage--;
                            min = CurrentPage * 10 + 10 < CountRecords ? CurrentPage * 10 + 10 : CountRecords;
                            for (int i = CurrentPage * 10; i < min; i++)
                            {
                                CurrentPageList.Add(TableList[i]);
                            }
                        }
                        else
                        {
                            Ifupdate = false;
                        }
                        break;

                    case 2:
                        if (CurrentPage < CountPage - 1)
                        {
                            CurrentPage++;
                            min = CurrentPage * 10 + 10 < CountRecords ? CurrentPage * 10 + 10 : CountRecords;
                            for (int i = CurrentPage * 10; i < min; i++)
                            {
                                CurrentPageList.Add(TableList[i]);
                            }
                        }
                        else
                        {
                            Ifupdate = false;
                        }
                        break;
                }
            }

            if (Ifupdate)
            {
                PageListBox.Items.Clear();

                for (int i = 1; i <= CountPage; i++)
                {
                    PageListBox.Items.Add(i);
                }
                PageListBox.SelectedIndex = CurrentPage;

                min = CurrentPage * 10 + 10 < CountRecords ? CurrentPage * 10 + 10 : CountRecords;
                TBCount.Text = min.ToString();
                TBAllRecords.Text = " из " + CountRecords.ToString();

                ServiceListView.ItemsSource = CurrentPageList;

                ServiceListView.Items.Refresh();
            }

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


            TableList = currentAgent;

            ChangePage(0, 0);

            ServiceListView.ItemsSource = currentAgent;

        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Manager.MainFrame.Navigate(new AddEditPage(null));
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


        private void LeftDirButton_Click(object sender, RoutedEventArgs e)
        {
            ChangePage(1, null);
        }

        private void RightDirButton_Click(object sender, RoutedEventArgs e)
        {
            ChangePage(2, null);
        }

        private void PageListBox_MouseUp(object sender, MouseButtonEventArgs e)
        {
            ChangePage(0, Convert.ToInt32(PageListBox.SelectedItem.ToString()) - 1);
        }

        private void AddBtn(object sender, RoutedEventArgs e)
        {
            Manager.MainFrame.Navigate(new AddEditPage(null));
        }

        private void Page_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            UpdateProduct();
        }

        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            Manager.MainFrame.Navigate(new AddEditPage((sender as Button).DataContext as Agent));
        }

        private void PriorityEdit_Click(object sender, RoutedEventArgs e)
        {
            int max = 0;
            foreach (Agent agent in ServiceListView.SelectedItems)
            {
                if (agent.Priority >= max)
                {
                    max = agent.Priority;
                }
            }
            PriorityEditWindow window = new PriorityEditWindow(max);
            window.ShowDialog();
            if (string.IsNullOrEmpty(window.PriorityText.Text)) return;
            MessageBox.Show(window.PriorityText.Text);

            foreach (Agent agent in ServiceListView.SelectedItems)
            {

                agent.Priority = Convert.ToInt32(window.PriorityText.Text);
            }
            try
            {
                Aflyatunov_glazkiEntities.GetContext().SaveChanges();
                MessageBox.Show("информация сохранена");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
            UpdateProduct();
        }

        private void AgentListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ServiceListView.SelectedItems.Count > 1)
            {
                PriorityEdit.Visibility = Visibility.Visible;
            }
            else
            {
                PriorityEdit.Visibility = Visibility.Hidden;
            }
        }
    }
}
