using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BoolBox.Models
{
    public class BoolTypeViewModel
    {
        public List<Bool> Bools { get; set; }
        public SelectList Types { get; set; }
        public string BoolType { get; set; }
        public string SearchString { get; set; }
    }
}
