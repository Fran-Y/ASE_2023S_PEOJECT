/* ************************************************************
 * For students to work on assignments and project.
 * Permission required material. Contact: xyuan@uwindsor.ca 
 * ************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace BookStoreLIB {
    public class OrderItem : INotifyPropertyChanged {
        #region INotifyPropertyChanged Members
        public event PropertyChangedEventHandler PropertyChanged;
        protected void Notify(string propName)
        {
            if (this.PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propName));
            }
        }
        #endregion
        
        public string BookID { get; set; }
        public string BookTitle { get; set; }
        public int Quantity { get; set; }
        public double UnitPrice { get; set; }
        public double SubTotal { get; set; }

        public OrderItem(String isbn, String title, 
            double unitPrice, int quantity) {
            BookID = isbn;
            BookTitle = title;
            UnitPrice = unitPrice;
            Quantity = quantity;
            SubTotal = UnitPrice * Quantity;
        }
        public override string ToString() {
            string xml = "<OrderItem ISBN='" + BookID + "'";
            xml += " Quantity='" + Quantity + "' />";
            return xml;
        }
    }
}
