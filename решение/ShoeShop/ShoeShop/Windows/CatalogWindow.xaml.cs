
using Microsoft.EntityFrameworkCore;
using ShoeShop.DbContexts;
using ShoeShop.Entities;
using ShoeShop.Windows;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace ShoeShop
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class CatalogWindow : Window
    {
        public CatalogWindow()
        {
            InitializeComponent();

            UserNameTB.Text = CurrentUser.FullName;
            ConfigInfo();
            LoadSuppliers();
            LoadProdData();
        }



        private void ConfigInfo()
        {
            AdminPanel.Visibility =
                CurrentUser.Role == "Администратор"
                ? Visibility.Visible
                : Visibility.Collapsed;

            SortGrid.Visibility =
                CurrentUser.Role == "Администратор" ||
                CurrentUser.Role == "Менеджер"
                ? Visibility.Visible
                : Visibility.Collapsed;

        }


        private void LoadProdData()
        {
            if (prodList == null || SupplierCB == null || SortCB == null || SearchTB == null)
                return;

            try
            {
                using var db = new ShoeshopContext();

                var query = db.Products
                    .Include(p => p.Cat)
                    .Include(p => p.Manuf)
                    .Include(p => p.Sup)
                    .Include(p => p.ProdNameNavigation)
                    .AsQueryable();

                if (!string.IsNullOrWhiteSpace(SearchTB.Text))
                {
                    string text = SearchTB.Text.ToLower();

                    query = query.Where(p =>
                        p.Descrip.ToLower().Contains(text) ||
                        p.Cat.CatName.ToLower().Contains(text) ||
                        p.Manuf.ManufName.ToLower().Contains(text) ||
                        p.Sup.SupName.ToLower().Contains(text) ||
                        p.ProdNameNavigation.ProdName1 .ToLower().Contains(text));
                }

                if (SupplierCB.SelectedValue is int supId && supId > 0)
                {
                    query = query.Where(p => p.SupId == supId);
                }

                if (SortCB.SelectedIndex == 1)
                    query = query.OrderBy(p => p.Count);

                if (SortCB.SelectedIndex == 2)
                    query = query.OrderByDescending(p => p.Count);

                prodList.ItemsSource = query.ToList();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка загрузки:\n" + ex.Message);
            }
        }


        private void exBtn_Click(object sender, RoutedEventArgs e)
        {
            LoginWin loginWin = new();
            loginWin.Show();
            Close();
        }

        private void SearchTB_TextChanged(object sender, TextChangedEventArgs e)
        {
            LoadProdData();
        }


        private void SupplierCB_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            LoadProdData();
        }

        private void LoadSuppliers()
        {
            try
            {
                using var db = new ShoeshopContext();

                var suppliers = db.Suppliers.ToList();

                suppliers.Insert(0, new Supplier
                {
                    IdSup = 0,
                    SupName = "Все поставщики"
                });

                SupplierCB.ItemsSource = suppliers;
                SupplierCB.DisplayMemberPath = "SupName";
                SupplierCB.SelectedValuePath = "IdSup";
                SupplierCB.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Ошибка загрузки поставщиков:\n" + ex.Message,
                    "Ошибка",
                    MessageBoxButton.OK,
                    MessageBoxImage.Error);
            }
        }

        private void SortCB_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            LoadProdData();
        }

        private void AddProdBtn_Click(object sender, RoutedEventArgs e)
        {
            AddProdWindow addProdWindow = new();
            addProdWindow.ShowDialog();
            if(addProdWindow.DialogResult == true)
            LoadProdData();
        }

        private void EditProdBtn_Click(object sender, RoutedEventArgs e)
        {
            if (prodList.SelectedItem is not Product product)
            {
                MessageBox.Show(
                    "Выберите товар для редактирования",
                    "Ошибка",
                    MessageBoxButton.OK,
                    MessageBoxImage.Warning);
                return;
            }

            EditProdWindow win = new(product);
            win.ShowDialog();
            if(win.DialogResult == true)
            LoadProdData();

        }

        private void DelProdBtn_Click(object sender, RoutedEventArgs e)
        {
            if (prodList.SelectedItem is not Product product)
            {
                MessageBox.Show(
                    "Выберите товар для удаления",
                    "Ошибка",
                    MessageBoxButton.OK,
                    MessageBoxImage.Warning);
                return;
            }

            try
            {
                using var db = new ShoeshopContext();

                bool isInOrders = db.Prodorders.Any(po => po.ProdId == product.IdProd);

                if (isInOrders)
                {
                    MessageBox.Show(
                        "Этот товар присутствует в заказах и не может быть удалён.",
                        "Ошибка удаления",
                        MessageBoxButton.OK,
                        MessageBoxImage.Warning);
                    return;
                }

                var result = MessageBox.Show(   // НАЧАЛО
                    "Удалить выбранный товар?",
                    "Подтверждение",
                    MessageBoxButton.YesNo,        // ЭТОТ БЛОК ЯВЛЯЕТСЯ НЕОБЯЗАТЕЛЬНЫМ
                    MessageBoxImage.Question);

                if (result != MessageBoxResult.Yes)
                    return;                         // КОНЕЦ

                var prod = db.Products.Find(product.IdProd);
                db.Products.Remove(prod);
                db.SaveChanges();

                LoadProdData();

                MessageBox.Show(
                    "Товар удалён",
                    "Успех",
                    MessageBoxButton.OK,
                    MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Ошибка удаления:\n" + ex.Message,
                    "Ошибка",
                    MessageBoxButton.OK,
                    MessageBoxImage.Error);
            }
        }


    }
}

