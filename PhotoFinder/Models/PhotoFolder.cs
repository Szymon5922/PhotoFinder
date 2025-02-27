using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoFinder.Models
{
    public class PhotoFolder
    {
        public string Directory { get; set; }
        public List<string> PhotoFolders { get; set; } = new ();
        public List<(string directory, PositionsInfoType type)> PhotosInfos { get; set; } = new();
    }
    public enum PositionsInfoType
    {
        Alignment,
        Centers
    }
}
