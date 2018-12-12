using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ATNB_Assignment.Models
{
    public class CartItems
    {
        public Book Book { get; set; }
        public int Quantity { get; set; }
        public CartItems(Book book, int quantity)
        {
            Book = book;
            Quantity = quantity;
        }
    }
}