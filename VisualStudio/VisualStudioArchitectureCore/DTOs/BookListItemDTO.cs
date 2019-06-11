using System;
using System.Collections.Generic;
using System.Text;
using VisualStudioArchitecture.Models;

namespace VisualStudioArchitecture.DTOs
{
    public class BookListItemDTO
    {
        public string Title { get; private set; }
        public string ISBN { get; set; }
        public BookListItemDTO(Book book)
        {
            Title = book.Title;
            ISBN = book.ISBN;
        }
    }
}
