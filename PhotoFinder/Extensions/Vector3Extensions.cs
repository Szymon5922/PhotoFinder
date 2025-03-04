using PhotoFinder.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace PhotoFinder.Extensions
{
    public static class Vector3Extensions
    {
        public static bool IsPointAtPhoto(this Vector3 point, PhotoData photo, out double yaw, out double pitch)
        {
            Vector3 vTarget = Vector3.Normalize(point - photo.CameraPosition);

            Vector3 right = Vector3.Cross(photo.Up, photo.Front);
            yaw = MathF.Atan2(Vector3.Dot(vTarget, right), Vector3.Dot(vTarget, photo.Front)) * (180 / MathF.PI);

            pitch = MathF.Atan2(Vector3.Dot(vTarget, photo.Up), Vector3.Dot(vTarget, photo.Front)) * (180 / MathF.PI);            

            return Math.Abs(yaw) <= photo.HFOV / 2 && Math.Abs(pitch) <= photo.VFOV / 2;
        }
    }
}
