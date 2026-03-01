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
    /// Логика взаимодействия для EditProdWindow.xaml
    /// </summary>
    public partial class EditProdWindow : Window
    {
        private Product _product;
        private string? _newImagePath;
        public EditProdWindow(Product product)
        {
            InitializeComponent();

            _product = product;

           
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

            Product prod = db.Products.First(p => p.IdProd == _product.IdProd);

            prod.Price = price;
            prod.Count = count;
            prod.Sale = sale;
            prod.Descrip = DescTB.Text;

            prod.CatId = ((Category)CategoryCB.SelectedItem).IdCat;
            prod.ManufId = ((Manufacrurer)ManufCB.SelectedItem).IdManuf;
            prod.SupId = ((Supplier)SupplierCB.SelectedItem).IdSup;
            prod.ProdName = ((Prodname)ProdNameCB.SelectedItem).IdProdName;

            if (_newImagePath != null)
                prod.Image = SaveImage(_newImagePath, prod.Image);

            db.SaveChanges();

            DialogResult = true;

            Close();
        }

        private string SaveImage(string sourcePath, string? oldImage)
        {
            string imagesDir = Path.Combine(
                AppDomain.CurrentDomain.BaseDirectory,
                "Images"
            );

            Directory.CreateDirectory(imagesDir);

        
            string newFileName = Guid.NewGuid() + Path.GetExtension(sourcePath);
            string newFullPath = Path.Combine(imagesDir, newFileName);

           
            using (FileStream sourceStream = new FileStream(sourcePath, FileMode.Open, FileAccess.Read))
            using (FileStream destStream = new FileStream(newFullPath, FileMode.Create))
            {
                sourceStream.CopyTo(destStream);
            }

          
            if (!string.IsNullOrEmpty(oldImage))
            {
                string oldFullPath = Path.Combine(imagesDir, oldImage);
                if (File.Exists(oldFullPath))
                {
                    try
                    {
                        File.Delete(oldFullPath);
                    }
                    catch
                    {
                     
                    }
                }
            }

            return newFileName;
        }


        private void CancelBtn_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }



        private void LoadProduct()
        {
            IdTB.Text = _product.IdProd.ToString();
            DescTB.Text = _product.Descrip;
            PriceTB.Text = _product.Price.ToString();
            CountTB.Text = _product.Count.ToString();
            SaleTB.Text = _product.Sale.ToString();

            CouTB.Text = "шт";

            CategoryCB.SelectedValue = _product.CatId;
            ManufCB.SelectedValue = _product.ManufId;
            SupplierCB.SelectedValue = _product.SupId;
            ProdNameCB.SelectedValue = _product.ProdName;

            LoadImage(_product.Image);

        }


      


        private void LoadImage(string? fileName)
        {
            string imagesDir = Path.Combine(
        AppDomain.CurrentDomain.BaseDirectory,
        "Images"
    );

            if (string.IsNullOrEmpty(fileName))
                fileName = "picture.png";

            string fullPath = Path.Combine(imagesDir, fileName);

            if (!File.Exists(fullPath))
                fullPath = Path.Combine(imagesDir, "picture.png");

            BitmapImage bmp = new();
            using (FileStream fs = new(fullPath, FileMode.Open, FileAccess.Read))
            {
                bmp.BeginInit();
                bmp.CacheOption = BitmapCacheOption.OnLoad;
                bmp.StreamSource = fs;
                bmp.EndInit();
                bmp.Freeze();
            }

            ProdImg.Source = bmp;
        }






        private void ChangeImageBtn_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dlg = new()
            {
                Filter = "Изображения (*.jpg;*.png)|*.jpg;*.png"
            };

            if (dlg.ShowDialog() != true)
                return;

            BitmapImage bmp = new();
            bmp.BeginInit();
            bmp.CacheOption = BitmapCacheOption.OnLoad;
            bmp.UriSource = new Uri(dlg.FileName);
            bmp.EndInit();
            bmp.Freeze();

            if (bmp.PixelWidth > 300 || bmp.PixelHeight > 200)
            {
                MessageBox.Show("Размер изображения не должен превышать 300x200");
                return;
            }

            _newImagePath = dlg.FileName;
            ProdImg.Source = bmp;
        }



    }



}

