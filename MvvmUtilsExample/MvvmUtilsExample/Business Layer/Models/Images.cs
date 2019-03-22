using System;
using System.Collections.Generic;

namespace MvvmUtilsExample.BusinessLayer.Models
{

    public class Image
    {
        public int id { get; set; }
        public string url { get; set; }
        public string large_url { get; set; }
        public int? source_id { get; set; }
    }

    public class Images
    {
        public List<Image> images { get; set; }
    }

}
