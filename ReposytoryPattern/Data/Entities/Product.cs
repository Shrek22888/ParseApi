using System;
using System.Collections.Generic;
using System.Text;

namespace ReposytoryPattern.Data.Entities
{
    public class Product : BaseEntity
    {
        public string Name { get; set; }
        public string Price { get; set;}
    }
}
