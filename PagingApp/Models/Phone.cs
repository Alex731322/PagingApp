using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PagingApp.Models
{
    public class Phone
    {
        public int Id { get; set; }
        public string Model { get; set; }
        public string Producer { get; set; }
    }

    public class PageInfo
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }

        public int TotalItems { get; set; }

        public int TotalPages 
        { 
            get  { return (int)Math.Ceiling( (decimal)TotalItems / PageSize); } 
           
        }
    }

    public class IndexViewModel
    {
        public IEnumerable<Phone> Phones { get; set; }
        public PageInfo PageInfo { get; set; }
    }



}