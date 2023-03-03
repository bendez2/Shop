using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace WpfApp1
{
    static class Views
    {
        public static CollectionViewSource CategoriesView { get; set; }
        public static CollectionViewSource ProductsView { get; set; }
        public static CollectionViewSource SalesView { get; set; }
       public static CollectionViewSource CustomesView { get; set; }
        public static CollectionViewSource SalesmanView { get; set; }

    }
}
