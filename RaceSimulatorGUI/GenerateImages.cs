using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Media.Imaging;
using System.Drawing.Imaging;
using System.Windows.Media;

namespace RaceSimulatorGUI
{
    public static class GenerateImages
    {
        private static Dictionary<string, Bitmap> _imageCache = new Dictionary<string, Bitmap>();

        public static Bitmap GetBitmapOfImage(string key)
        {
            Bitmap bitmap;
            if (_imageCache.TryGetValue(key, out bitmap))
            {
                return (Bitmap) bitmap.Clone();
            }
            else
            {
                bitmap = new Bitmap("../../../Assets/" + key);
                _imageCache.Add(key, bitmap);
                return (Bitmap)bitmap.Clone();
            }
        }

        public static Bitmap GetEmptyBitmap(int width, int height)
        {
            Bitmap bitmap;
            if (!_imageCache.TryGetValue("empty", out bitmap))
            {
                bitmap = new Bitmap(width, height);

                var g = Graphics.FromImage(bitmap);
                var rect = new Rectangle(0, 0, width, height);
                g.FillRectangle(System.Drawing.Brushes.SeaGreen, rect);
                _imageCache.Add("empty", bitmap);
            }
            return (Bitmap)bitmap.Clone();
        }

        public static BitmapSource CreateBitmapSourceFromGdiBitmap(Bitmap bitmap)
        {
            if (bitmap == null)
                throw new ArgumentNullException("bitmap");

            var rect = new Rectangle(0, 0, bitmap.Width, bitmap.Height);

            var bitmapData = bitmap.LockBits(
                rect,
                ImageLockMode.ReadWrite,
                System.Drawing.Imaging.PixelFormat.Format32bppArgb);

            try
            {
                var size = (rect.Width * rect.Height) * 4;

                return BitmapSource.Create(
                    bitmap.Width,
                    bitmap.Height,
                    bitmap.HorizontalResolution,
                    bitmap.VerticalResolution,
                    PixelFormats.Bgra32,
                    null,
                    bitmapData.Scan0,
                    size,
                    bitmapData.Stride);
            }
            finally
            {
                bitmap.UnlockBits(bitmapData);
            }
        }

        public static void ClearCache()
        {
            _imageCache.Clear();
        }
    }
}
