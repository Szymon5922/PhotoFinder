using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace PhotoFinder.Models
{
    public class SearchingTarget
    {
        public Vector3 Position { get; set; }
        public string? ImageDirectory { get; set; }
        public string ImageName => Path.GetFileName(ImageDirectory);
        public SearchingTarget(Vector3 position)
        {
            Position = position;
        }
    }
}
