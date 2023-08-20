using Microsoft.VisualBasic;
using News.WebUI.Entities.Concrete;
using System;
using System.Collections;
using System.Collections.Generic;
using Information = News.WebUI.Entities.Concrete.Information;

namespace News.WebUI.ViewModels
{
    public class InfoContentVM
    {
     
        public int InformationID { get; set; }
        public string Header { get; set; }
        public string Body { get; set; }
        public DateTime Created { get; set; }
        public bool IsValid { get; set; }
        public int ContentID { get; set; }
        public ICollection<Content> Contents { get; set; }
        public int SelectedContentID { get; set; }
    }
}
