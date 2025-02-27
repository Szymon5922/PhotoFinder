using PhotoFinder.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoFinder.Services
{
    public interface IPhotoFinderService
    {
        public List<Result> GetResults(List<SearchingTarget> targets, List<PhotoData> photos);
    }
}
