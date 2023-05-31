using System.Diagnostics;
using Emgu.CV;

class Program
{
    const int _frameRate = 60;
    const int _width = 90;
    const int _height = 45;
    static string videoPath = @"\badapple.mp4";
    static string homeDir = Directory.GetCurrentDirectory();

    static void Main()
    {
        Console.Title = "BadApple.ascii";

        videoPath = Path.Combine(homeDir + videoPath);
        Console.SetWindowSize(_width, _height + 2);         // + 2 is for to avoid flickering

        double f = 1000 / _frameRate;
        int frameTime = (int)Math.Ceiling(f);

        using(var video = new VideoCapture(videoPath))
        {
            using var img = new Mat();
            
            while (video.Grab())
            {
                Stopwatch execTime = Stopwatch.StartNew();
                video.Retrieve(img);
                    
                string asciiImg = Utils.Converter.ASCIIConverter(img, _width, _height);
                    
                Console.SetCursorPosition(0,0);
                Console.WriteLine(asciiImg);

                if (execTime.ElapsedMilliseconds < frameTime)
                {
                    int sleepTime = frameTime - (int)execTime.ElapsedMilliseconds;
                    Thread.Sleep(sleepTime);
                    
                    // as conversion times may vary and be more than 16.(6) ms, i just sleep thread so average frametime would be about 17 ms
                }
                execTime.Stop();
            }
        }
        Console.Clear();
        Console.WriteLine("Press any key to exit...");
        Console.ReadKey();
    }
}