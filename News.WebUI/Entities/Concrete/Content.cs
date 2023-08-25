using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace News.WebUI.Entities.Concrete
{
    public class Content
    {
        [Key]
        public int ContentID { get; set; }
        public string ContentName { get; set; }

    }
}
