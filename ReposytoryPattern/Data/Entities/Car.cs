using System;
using System.Collections.Generic;
using System.Text;

namespace ReposytoryPattern.Data.Entities
{
    public class Car : BaseEntity
    {
        public string Name { get; set; }
        public string Year { get; set; }
    }
}
