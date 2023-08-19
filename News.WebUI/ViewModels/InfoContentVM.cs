using Microsoft.VisualBasic;
using News.WebUI.Entities.Concrete;
using System.Collections;
using System.Collections.Generic;

namespace News.WebUI.ViewModels
{
    public class InfoContentVM
    {
        public Entities.Concrete.Information Information { get; set; }
        public ICollection<Content> Contents { get; set; } 
            
    }
}
