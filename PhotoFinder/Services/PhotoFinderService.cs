using PhotoFinder.Extensions;
using PhotoFinder.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace PhotoFinder.Services
{
    public class PhotoFinderService : IPhotoFinderService
    {
        public List<Result> GetResults(List<SearchingTarget> targets, List<PhotoData> photos)
        {
            List<Result> results = new List<Result>();
            foreach (var target in targets)
            {
                var result = new Result
                {
                    Target = target,
                    FoundPhotos = GetPhotosForPoint(target.Position, photos)
                };

                results.Add(result);
            }
            return results;
        }
        public List<FoundPhoto> GetPhotosForPoint(Vector3 target, List<PhotoData> photos)
        {
            List<FoundPhoto> foundPhotos = new();

            foreach (var photo in photos)
            {
                double distance = 0;

                if (target.IsPointAtPhoto(photo, out distance))
                {
                    foundPhotos.Add(new FoundPhoto 
                    {
                        FilePath = photo.PhotoDirectory,
                        Distance = distance
                    });
                }
            }

            if (foundPhotos.Any())
            {
                foundPhotos = foundPhotos.OrderBy(p => p.Distance).ToList();
                foundPhotos.First().IsSelected = true;
            }
            return foundPhotos;
        }
    }
}
