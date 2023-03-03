using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;

namespace WpfApp1
{
    public enum TableType
    {
        Categories, Customer, Sale, Salesman, Product
    }

    public partial class MainWindow : Window
    {
        DbService db;
        TableType currentTableType;

        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            switch (currentTableType)
            {
                case TableType.Salesman:
                    Views.SalesmanView.Filter += (o, ea) =>
                    {
                        if (ea.Item is Salesman v)
                        {
                            string nameFull = v.NameFull.ToLower();
                            string surname = v.Surname.ToLower();
                            string name = v.Name.ToLower();
                            string otchestvo = v.Otchestvo.ToLower();

                            if (nameFull.Contains(SalesmenSearchInic.Text.ToLower()) &&
                                surname.Contains(SalesmenSearchSurname.Text.ToLower()) &&
                                name.Contains(SalesmenSearchName.Text.ToLower()) &&
                                otchestvo.Contains(SalesmenSearchOtch.Text.ToLower()))
                            {
                                ea.Accepted = true;
                            }
                            else
                            {
                                ea.Accepted = false;
                            }
                        }
                    };
                    break;
                case TableType.Customer:
                    Views.CustomesView.Filter += (o, ea) =>
                    {
                        if (ea.Item is Customer p)
                        {
                            string nameFull = p.NameFull.ToLower();
                            string surname = p.Surname.ToLower();
                            string name = p.Name.ToLower();
                            string otchestvo = p.Otchestvo.ToLower();

                            if (nameFull.Contains(CustSearchInic.Text.ToLower()) &&
                                surname.Contains(CustSearchSurname.Text.ToLower()) &&
                                name.Contains(CustSearchName.Text.ToLower()) &&
                                otchestvo.Contains(CustSearchOtch.Text.ToLower()))
                            {
                                ea.Accepted = true;
                            }
                            else
                            {
                                ea.Accepted = false;
                            }
                        }
                    };
                    break;
                case TableType.Sale:
                    Views.SalesView.Filter += (o, ea) =>
                    {
                        if (ea.Item is Sale a)
                        {
                            string date = Convert.ToString(a.Date).ToLower();
                            string salesmen = a.Salesman.NameFull.ToLower();
                            string customer = a.Customer.NameFull.ToLower();
                            string product = a.Product.Name.ToLower();
                            string sum = Convert.ToString(a.Sum);

                            if (date.Contains(SellSearchDate.Text.ToLower()) &&
                                salesmen.Contains(SellSearchSalesmen.Text.ToLower()) &&
                                customer.Contains(SellSearchCustomer.Text.ToLower()) &&
                                product.Contains(SellSearchProduct.Text.ToLower()) &&
                                sum.Contains(SellSearchSum.Text.ToLower()))
                            {
                                ea.Accepted = true;
                            }
                            else
                            {
                                ea.Accepted = false;
                            }
                        }
                    };
                    break;
                case TableType.Categories:
                    Views.CategoriesView.Filter += (o, ea) =>
                    {
                        if (ea.Item is Category k)
                        {
                            string name = k.Name.ToLower();

                            if (name.Contains(catSearchName.Text.ToLower()))
                            {
                                ea.Accepted = true;
                            }
                            else
                            {
                                ea.Accepted = false;
                            }
                        }
                    };
                    break;
                case TableType.Product:
                    Views.ProductsView.Filter += (o, ea) =>
                    {
                        if (ea.Item is Product kk)
                        {
                            string name = kk.Name.ToLower();
                            string cat = kk.Category1.Name.ToLower();

                            if (name.Contains(ProductSearchName.Text.ToLower()) &&
                            cat.Contains(ProductSearchCat.Text.ToLower()))
                            {
                                ea.Accepted = true;
                            }
                            else
                            {
                                ea.Accepted = false;
                            }
                        }
                    };
                    break;
            }
        }

        private void CancelSearchButton_Click(object sender, RoutedEventArgs e)
        {
            switch (currentTableType)
            {
                case TableType.Categories:
                    Views.CategoriesView.Filter += (o, ea) => ea.Accepted = true;

                    SalesmenSearchInic.Text = "";
                    SalesmenSearchSurname.Text = "";
                    SalesmenSearchName.Text = "";
                    SalesmenSearchOtch.Text = "";
                    break;
                case TableType.Customer:
                    Views.CustomesView.Filter += (o, ea) => ea.Accepted = true;

                    CustSearchInic.Text = "";
                    CustSearchName.Text = "";
                    CustSearchOtch.Text = "";
                    CustSearchSurname.Text = "";
                    break;
                case TableType.Product:
                    Views.ProductsView.Filter += (o, ea) => ea.Accepted = true;

                    ProductSearchCat.Text = "";
                    ProductSearchName.Text = "";
                    break;
                case TableType.Sale:
                    Views.SalesView.Filter += (o, ea) => ea.Accepted = true;

                    SellSearchCustomer.Text = "";
                    SellSearchDate.Text = "";
                    SellSearchProduct.Text = "";
                    SellSearchSalesmen.Text = "";
                    SellSearchSum.Text = "";
                    break;
                case TableType.Salesman:
                    Views.SalesmanView.Filter += (o, ea) => ea.Accepted = true;

                    SalesmenSearchInic.Text = "";
                    SalesmenSearchName.Text = "";
                    SalesmenSearchOtch.Text = "";
                    SalesmenSearchSurname.Text = "";
                    break;
            }
        }

        private void ReportButton_Click(object sender, RoutedEventArgs e)
        {
            Report report = new Report();
            report.OtchetEffect(Views.SalesView.Source as IList<Sale>);
        }

        public MainWindow()
        {
            //new LoginWindow().ShowDialog();

            InitializeComponent();

            db = new DbService();
            currentTableType = TableType.Categories;
            RefreshTable(currentTableType);
        }

        private void RefreshTable(TableType tt)
        {
            db = new DbService(); //Создаём обект базы данных
            CollectionViewSource vs = new CollectionViewSource();
            switch (tt)
            {
                case TableType.Categories:
                    db.Categories.Load();

                    vs.Source = db.Categories.Local;
                    categoriesTable.ItemsSource = vs.View;
                    categoriesTable.AddingNewItem += (sender, e) => e.NewItem = new Category() { Name = "<новый>" };

                    Views.CategoriesView = vs;

                    plNCatTable.ItemsSource = db.Categories.ToArray();
                    break;
                case TableType.Customer:
                    db.Customers.Load();

                    vs.Source = db.Customers.Local;
                    customerTable.ItemsSource = vs.View;
                    customerTable.AddingNewItem += (sender, e) => e.NewItem = new Customer() { Name = "<новый>", Otchestvo = "-", NameFull = "-", Surname = "-" };

                    Views.CustomesView = vs;
                    break;
                case TableType.Product:
                    db.Products.Load();

                    vs.Source = db.Products.Local;
                    productTable.ItemsSource = vs.View;
                    productTable.AddingNewItem += (sender, e) => e.NewItem = new Product() { Name = "-", Category = 0, Price = 0 };

                    Views.ProductsView = vs;
                    break;
                case TableType.Sale:
                    db.Sales.Load();

                    vs.Source = db.Sales.Local;
                    sellTable.ItemsSource = vs.View;
                    sellTable.AddingNewItem += (sender, e) => e.NewItem = new Sale()
                    {
                        Date = DateTime.Now,
                        CustomerID = 0,
                        SalesmanID = 0,
                        ProductID = 0,
                        Quantity = 0,
                        Sum = 0,

                    };


                    Views.SalesView = vs;

                    this.plCustomerTable.ItemsSource = db.Customers.ToArray();
                    this.plSalesManTable.ItemsSource = db.Salesmen.ToArray();
                    this.plNProductTable.ItemsSource = db.Products.ToArray();
                    break;
                case TableType.Salesman:
                    db.Salesmen.Load();

                    vs.Source = db.Salesmen.Local;
                    salesmanTable.ItemsSource = vs.View;
                    salesmanTable.AddingNewItem += (sender, e) => e.NewItem = new Salesman() { Name = "<новый>", Otchestvo = "-", NameFull = "-", Surname = "-" };

                    Views.SalesmanView = vs;
                    break;
            }
        }

        private void SaveChanges(TableType tableType)
        {
            db.SaveChanges();

            DataGrid currTable = null;
            switch (tableType)
            {
                case TableType.Categories:
                    currTable = categoriesTable;
                    break;
                case TableType.Customer:
                    currTable = customerTable;
                    break;
                case TableType.Product:
                    currTable = productTable;
                    break;
                case TableType.Sale:
                    currTable = sellTable;
                    break;
                case TableType.Salesman:
                    currTable = salesmanTable;
                    break;
            }

            int si = currTable.SelectedIndex;
            RefreshTable(tableType);
            currTable.SelectedIndex = si;
        }

        private void DeleteRecord(TableType tt)
        {
            switch (tt)
            {
                case TableType.Salesman:
                    if (salesmanTable.SelectedItem is Salesman b && b.Sales.Count == 0)
                        db.Salesmen.Local.Remove(b);
                    else
                        MessageBox.Show("Данный продавец уже в продаже товара. Удаление невозможно!",
                            "Ошибка", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                    break;
                case TableType.Customer:
                    if (customerTable.SelectedItem is Customer p && p.Sales.Count == 0)
                        db.Customers.Local.Remove(p);
                    else
                        MessageBox.Show("Данный покупатель уже в продаже товара. Удаление невозможно!",
                            "Ошибка", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                    break;
                case TableType.Categories:
                    if (categoriesTable.SelectedItem is Category s && s.Products.Count == 0)
                        db.Categories.Local.Remove(s);
                    else
                        MessageBox.Show("Данная категория уже закрепленна у товара. Удаление невозможно!",
                            "Ошибка", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                    break;
                case TableType.Sale:
                    if (sellTable.SelectedItem is Sale v)
                        db.Sales.Local.Remove(v);
                    break;
                case TableType.Product:
                    if (productTable.SelectedItem is Product a && a.Sales.Count == 0)
                        db.Products.Local.Remove(a);
                    else
                        MessageBox.Show("Данный товар уже участвует в продаже товара. Удаление невозможно!",
                            "Ошибка", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                    break;
            }
        }

        private void TabItem_GotFocus(object sender, RoutedEventArgs e)
        {
            if (sender is TabItem ti)
            {
                TableType old = currentTableType;

                string header = ti.Header.ToString();
                if (header == "Категория")
                    currentTableType = TableType.Categories;
                else if (header == "Покупатель")
                    currentTableType = TableType.Customer;
                else if (header == "Продавец")
                    currentTableType = TableType.Salesman;
                else if (header == "Продажа")
                    currentTableType = TableType.Sale;
                else if (header == "Товар")
                    currentTableType = TableType.Product;

                if (currentTableType != old)
                    RefreshTable(currentTableType);
            }
        }

        private void SaveChangesButton_Click(object sender, RoutedEventArgs e)
        {
            SaveChanges(currentTableType);
        }

        private void CancelChangesButton_Click(object sender, RoutedEventArgs e)
        {
            RefreshTable(currentTableType);
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            DeleteRecord(currentTableType);
        }

        private void SearchOnlyDigits_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !char.IsDigit(e.Text, 0);
        }

        private void SearchPrice_LostFocus(object sender, RoutedEventArgs e)
        {
            if (sender is TextBox tb && tb.Text.Trim() == "")
                tb.Text = "0";
        }

        private void productTable_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void UpdataBuildPriceButton_Click(object sender, RoutedEventArgs e)
        {
            //Пересчитываем цены у всех сборок доступных в локальном хранилище
            foreach (Sale Sale in db.Sales.Local)
                Sale.Sum = Sale.Product.Price * Convert.ToDecimal(Sale.Quantity);

            //Сохраняем на сервер
            SaveChanges(currentTableType);
        }

        private void ReportBuy_Click(object sender, RoutedEventArgs e)
        {
            Report report = new Report();
            report.Sells(Views.SalesView.Source as IList<Sale>);
        }
    }
}
