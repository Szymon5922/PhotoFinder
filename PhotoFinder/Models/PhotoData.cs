using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace PhotoFinder.Models
{
    public class PhotoData
    {
        public string PhotoDirectory { get; set; }
        public double HFOV { get; set; }
        public double VFOV { get; set; }
        public Vector3 Front { get; set; }
        public Vector3 Up { get; set; }
        public Vector3 CameraPosition { get; set; }
    }
}
