using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace News.WebUI.Entities.Concrete
{
    public class Information
    {
        [Key]
        public int InformationID { get; set; }  
        public string Header { get; set;}
        public string Body { get; set;}
        public DateTime Created { get; set; }
        public bool IsValid { get; set;}
        public int ContentID { get; set; }
        public Content Contents { get; set; }
    }
}
