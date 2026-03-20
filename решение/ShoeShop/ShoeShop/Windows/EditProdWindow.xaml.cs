using Microsoft.Win32;
using ShoeShop.DbContexts;
using ShoeShop.Entities;
using System;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Media.Imaging;

namespace ShoeShop.Windows
{
    public partial class EditProdWindow : Window
    {
        private Product product;
        private string? newImagePath;

        private string imagesDir = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Images");

        public EditProdWindow(Product prod)
        {
            InitializeComponent();
            product = prod;

            LoadComboBoxes();
            LoadProduct();
        }

        private void LoadComboBoxes()
        {
            using var db = new ShoeshopContext();
            CategoryCB.ItemsSource = db.Categories.ToList();
            ManufCB.ItemsSource = db.Manufacrurers.ToList();
            SupplierCB.ItemsSource = db.Suppliers.ToList();
            ProdNameCB.ItemsSource = db.Prodnames.ToList();
        }

        private void LoadProduct()
        {
            IdTB.Text = product.IdProd.ToString();
            DescTB.Text = product.Descrip;
            PriceTB.Text = product.Price.ToString();
            CountTB.Text = product.Count.ToString();
            SaleTB.Text = product.Sale.ToString();
            CouTB.Text = "шт";

            CategoryCB.SelectedValue = product.CatId;
            ManufCB.SelectedValue = product.ManufId;
            SupplierCB.SelectedValue = product.SupId;
            ProdNameCB.SelectedValue = product.ProdName;

            LoadImage(product.Image);
        }

        private void LoadImage(string? fileName)
        {
            string path = Path.Combine(imagesDir, fileName ?? "picture.png");

            if (!File.Exists(path))
                path = Path.Combine(imagesDir, "picture.png");

            ProdImg.Source = new BitmapImage(new Uri(path));
        }

        private void ChangeImageBtn_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog
            {
                Filter = "Images (*.jpg;*.png)|*.jpg;*.png"
            };

            if (dlg.ShowDialog() != true)
                return;

            BitmapImage bmp = new BitmapImage(new Uri(dlg.FileName));

            if (bmp.PixelWidth > 300 || bmp.PixelHeight > 200)
            {
                MessageBox.Show("Размер изображения не должен превышать 300x200");
                return;
            }

            newImagePath = dlg.FileName;
            ProdImg.Source = bmp;
        }

        private void SaveBtn_Click(object sender, RoutedEventArgs e)
        {
            if (!decimal.TryParse(PriceTB.Text, out decimal price) || price < 0 ||
                !int.TryParse(CountTB.Text, out int count) || count < 0 ||
                !int.TryParse(SaleTB.Text, out int sale) || sale < 0)
            {
                MessageBox.Show("Некорректные числовые значения");
                return;
            }

            using var db = new ShoeshopContext();
            var prod = db.Products.FirstOrDefault(p => p.IdProd == product.IdProd);

            prod.Price = price;
            prod.Count = count;
            prod.Sale = sale;
            prod.Descrip = DescTB.Text;
            prod.CatId = ((Category)CategoryCB.SelectedItem).IdCat;
            prod.ManufId = ((Manufacrurer)ManufCB.SelectedItem).IdManuf;
            prod.SupId = ((Supplier)SupplierCB.SelectedItem).IdSup;
            prod.ProdName = ((Prodname)ProdNameCB.SelectedItem).IdProdName;

            if (newImagePath != null)
            {
                if (!Directory.Exists(imagesDir))
                    Directory.CreateDirectory(imagesDir);

                string newFileName = Path.GetFileName(newImagePath);
                string destPath = Path.Combine(imagesDir, newFileName);
                File.Copy(newImagePath, destPath, true);

                prod.Image = newFileName;
            }

            db.SaveChanges();

            MessageBox.Show("Товар обновлен");
            DialogResult = true;
            Close();
        }

        private void CancelBtn_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }
    }
}