//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WpfApp1
{
    using System;
    using System.Collections.Generic;
    
    public partial class Sale
    {
        public int ID { get; set; }
        public System.DateTime Date { get; set; }
        public int CustomerID { get; set; }
        public int SalesmanID { get; set; }
        public int ProductID { get; set; }
        public int Quantity { get; set; }
        public decimal Sum { get; set; }
    
        public virtual Customer Customer { get; set; }
        public virtual Product Product { get; set; }
        public virtual Salesman Salesman { get; set; }
    }
}
