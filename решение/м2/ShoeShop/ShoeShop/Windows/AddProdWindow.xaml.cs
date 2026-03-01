using Microsoft.Win32;
using ShoeShop.DbContexts;
using ShoeShop.Entities;
using System;
using System.Collections.Generic;
using System.IO;
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

namespace ShoeShop.Windows
{
    /// <summary>
    /// Логика взаимодействия для AddProdWindow.xaml
    /// </summary>
    public partial class AddProdWindow : Window
    {
        private string? _selectedImagePath;
        public AddProdWindow()
        {
            InitializeComponent();

            LoadComboBoxes();
            LoadDefaultImage();
        }

        private void LoadComboBoxes()
        {
            using var db = new ShoeshopContext();

            ProdNameCB.ItemsSource = db.Prodnames.ToList();
            ProdNameCB.DisplayMemberPath = "ProdName1";
            ProdNameCB.SelectedValuePath = "IdProdName";

            CategoryCB.ItemsSource = db.Categories.ToList();
            CategoryCB.DisplayMemberPath = "CatName";
            CategoryCB.SelectedValuePath = "IdCat";

            ManufCB.ItemsSource = db.Manufacrurers.ToList();
            ManufCB.DisplayMemberPath = "ManufName";
            ManufCB.SelectedValuePath = "IdManuf";

            SupplierCB.ItemsSource = db.Suppliers.ToList();
            SupplierCB.DisplayMemberPath = "SupName";
            SupplierCB.SelectedValuePath = "IdSup";
        }

        private void LoadDefaultImage()
        {
            string imagesDir = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Images");
            string defaultImage = Path.Combine(imagesDir, "picture.png");

            if (File.Exists(defaultImage))
                ProdImg.Source = new BitmapImage(new Uri(defaultImage, UriKind.Absolute));
        }
        private void CancelBtn_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void SaveBtn_Click(object sender, RoutedEventArgs e)
        {
            if (ProdNameCB.SelectedItem == null ||
                CategoryCB.SelectedItem == null ||
                ManufCB.SelectedItem == null ||
                SupplierCB.SelectedItem == null)
            {
                MessageBox.Show("Выберите название продукта и заполните все поля", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (!decimal.TryParse(PriceTB.Text, out decimal price) || price < 0 ||
                !int.TryParse(CountTB.Text, out int count) || count < 0 ||
                !int.TryParse(SaleTB.Text, out int sale) || sale < 0)
            {
                MessageBox.Show("Цена, количество и скидка должны быть положительными числами", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            string? imageFileName = null;
            if (!string.IsNullOrEmpty(_selectedImagePath))
            {
                string imagesDir = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Images");
                imageFileName = $"{Guid.NewGuid()}{Path.GetExtension(_selectedImagePath)}";
                string destPath = Path.Combine(imagesDir, imageFileName);
                File.Copy(_selectedImagePath, destPath, true);
            }

            using var db = new ShoeshopContext();

            var selectedProdName = (Prodname)ProdNameCB.SelectedItem;

            var product = new Product
            {
                Article = Guid.NewGuid().ToString(),
                ProdName = selectedProdName.IdProdName,
                Price = price,
                Count = count,
                Sale = sale,
                Descrip = DescTB.Text,
                Image = imageFileName,
                CatId = ((Category)CategoryCB.SelectedItem).IdCat,
                ManufId = ((Manufacrurer)ManufCB.SelectedItem).IdManuf,
                SupId = ((Supplier)SupplierCB.SelectedItem).IdSup
            };

            db.Products.Add(product);
            db.SaveChanges();

            MessageBox.Show("Товар успешно добавлен", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
            Close();
        }

        private void ChangeImageBtn_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog
            {
                Filter = "Изображения (*.jpg;*.png)|*.jpg;*.png"
            };

            if (dlg.ShowDialog() != true) return;

            BitmapImage bmp = new BitmapImage();
            bmp.BeginInit();
            bmp.CacheOption = BitmapCacheOption.OnLoad;
            bmp.UriSource = new Uri(dlg.FileName);
            bmp.EndInit();
            bmp.Freeze();

            if (bmp.PixelWidth > 300 || bmp.PixelHeight > 200)
            {
                MessageBox.Show("Размер изображения не должен превышать 300x200", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            _selectedImagePath = dlg.FileName;
            ProdImg.Source = bmp;
        }
    }
}
