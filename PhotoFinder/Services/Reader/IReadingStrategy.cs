using PhotoFinder.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoFinder.Services.Reader
{
    public interface IReadingStrategy
    {
        public List<PhotoData> Read(string directory);
    }
}
