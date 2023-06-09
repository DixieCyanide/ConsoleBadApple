using Emgu.CV;
using System.Text;

namespace Utils{
    public static class Converter{
        private static string[] _AsciiChars = { "@", "%", "#", "*", "+", "=", "-", ":", ".", " " };

        public static string ASCIIConverter(Mat img, int width, int height)
        {
            float stepCharsSize = 255 / (_AsciiChars.Length - 1);
            StringBuilder sb = new StringBuilder();
            var bmpImg = img.ToBitmap();
            var bmp = ResizeBitmap(bmpImg, width, height);
            
            for (int h = 0; h < bmp.Height; h++)
            {
                for (int w = 0; w < bmp.Width; w++)
                {
                    Color pixel = bmp.GetPixel(w, h);
                    int avg = (pixel.R + pixel.G + pixel.B) / 3;
                    sb.Append(_AsciiChars[(int)MathF.Floor(avg / stepCharsSize)]);
                }
                sb.Append(Environment.NewLine);
            }
            bmpImg.Dispose();
            bmp.Dispose();

            return sb.ToString();
        }

        public static Bitmap ResizeBitmap(Bitmap bmp, int width, int height)
		{
			Bitmap result = new Bitmap(width, height);
			using (Graphics g = Graphics.FromImage(result))
			{
				g.DrawImage(bmp, 0, 0, width, height);
			}

			return result;
		}
    }
}