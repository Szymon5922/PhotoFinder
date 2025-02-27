using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoFinder.Models
{
    public class Result
    {
        public SearchingTarget Target { get; set; }
        public List<FoundPhoto> FoundPhotos { get; set; }
    }
}
