using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace PhotoFinder.Helpers
{
    public static class FilesHelper
    {
        public static DragDropEffects AllowIfFolder(DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                var paths = (string[])e.Data.GetData(DataFormats.FileDrop);

                if (paths.Any() && Directory.Exists(paths[0]))
                    return DragDropEffects.Copy;
            }
            return DragDropEffects.None;
        }
        public static bool AllowIfSHP(DragEventArgs e)
        {
            if(e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                var paths = (string[])e.Data.GetData(DataFormats.FileDrop);
                
                foreach(string path in paths)
                {
                    if (Path.GetExtension(path) != ".shp")
                        return true;
                }
            }
            return false;//not blocking
        }
    }

}
