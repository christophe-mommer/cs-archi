using System;
using System.Collections.Generic;
using System.Text;
using VisualStudioArchitecture.DTOs;

namespace VisualStudioArchitecture.Models
{
    public class Book
    {
        public string Title { get; set; }
        public string ISBN { get; set; }
        public string Body { get; set; }
        public int NbPages { get; set; }
        public Guid Id { get; set; }
        public BookListItemDTO DTO { get; set; }
    }
}
