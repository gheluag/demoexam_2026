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
        private string imagePath = null;
        private string imagesDir = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Images");
        public AddProdWindow()
        {
            InitializeComponent();

            LoadComboBoxes();
            LoadDefaultImage();
            CouTB.Text = "шт";
        }

        private void LoadComboBoxes()
        {
            using var db = new ShoeshopContext();

            ProdNameCB.ItemsSource = db.Prodnames.ToList();;

            CategoryCB.ItemsSource = db.Categories.ToList();

            ManufCB.ItemsSource = db.Manufacrurers.ToList();

            SupplierCB.ItemsSource = db.Suppliers.ToList();
        }
        private void CancelBtn_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }

        private void LoadDefaultImage()
        {
            string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Images", "picture.png");

            if (File.Exists(path))
                ProdImg.Source = new BitmapImage(new Uri(path));
        }

        private void SaveBtn_Click(object sender, RoutedEventArgs e)
        {
            if (ProdNameCB.SelectedItem == null ||
            CategoryCB.SelectedItem == null ||
            ManufCB.SelectedItem == null ||
            SupplierCB.SelectedItem == null)
            {
                MessageBox.Show("Заполните все поля");
                return;
            }

            if (!decimal.TryParse(PriceTB.Text, out decimal price) || price < 0)
            {
                MessageBox.Show("Введите корректную цену");
                return;
            }

            if (!int.TryParse(CountTB.Text, out int count) || count < 0)
            {
                MessageBox.Show("Введите корректное количество");
                return;
            }

            if (!int.TryParse(SaleTB.Text, out int sale) || sale < 0)
            {
                MessageBox.Show("Введите корректную скидку");
                return;
            }

            string imageFile = null;

            if (imagePath != null)
            {
                
                imageFile = Path.GetFileName(imagePath);

                File.Copy(imagePath, Path.Combine(imagesDir, imageFile), true);
            }

            using var db = new ShoeshopContext();
            try
            {

                Product product = new Product()
                {
                    Article = Guid.NewGuid().ToString(),
                    ProdName = ((Prodname)ProdNameCB.SelectedItem).IdProdName,
                    CatId = ((Category)CategoryCB.SelectedItem).IdCat,
                    ManufId = ((Manufacrurer)ManufCB.SelectedItem).IdManuf,
                    SupId = ((Supplier)SupplierCB.SelectedItem).IdSup,

                    Price = price,
                    Count = count,
                    Sale = sale,

                    Descrip = DescTB.Text,
                    Image = imageFile
                };

                db.Products.Add(product);
                db.SaveChanges();

                MessageBox.Show("Товар добавлен");
                DialogResult = true;
            }
            catch(Exception ex)
            {
                MessageBox.Show("Произошла ошибка: " + ex.Message,
                    "Ошибка",
                    MessageBoxButton.OK,
                    MessageBoxImage.Error);
                DialogResult = false;
            }
            Close();
        }

        private void ChangeImageBtn_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "Изображения (*.jpg;*.png)|*.jpg;*.png";

            if (dialog.ShowDialog() == true)
            {
                BitmapImage bmp = new BitmapImage(new Uri(dialog.FileName));

                if (bmp.PixelWidth > 300 || bmp.PixelHeight > 200)
                {
                    MessageBox.Show("Размер изображения не должен превышать 300x200");
                    return;
                }

                imagePath = dialog.FileName;
                ProdImg.Source = bmp;
            }
        }
    }
}
