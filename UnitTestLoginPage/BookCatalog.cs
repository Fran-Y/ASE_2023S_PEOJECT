/* ************************************************************
 * For students to work on assignments and project.
 * Permission required material. Contact: xyuan@uwindsor.ca 
 * ************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace BookStoreLIB
{
    public class BookCatalog
    {
        public DataSet SearchBooks(string searchText)
        {
            DALBookCatalog dalBookCatalog = new DALBookCatalog();
            return dalBookCatalog.SearchBooks(searchText);
        }
        public DataSet GetBookInfo()
        {
            //perform any business logic befor passing to client.
            // None needed at this time.
            DALBookCatalog bookCatalog = new DALBookCatalog();
            return bookCatalog.GetBookInfo();
        }
    }
}
