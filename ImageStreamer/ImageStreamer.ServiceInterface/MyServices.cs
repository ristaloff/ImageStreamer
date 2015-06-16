using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Web;
using ServiceStack;
using ImageStreamer.ServiceModel;

namespace ImageStreamer.ServiceInterface
{
    public class MyServices : Service
    {
        private static byte[] _mediumImageBytes;
        private static byte[] _largeImageBytes;
        private static byte[] _smallImageBytes;

        public MyServices()
        {
            var directoryName = Path.GetDirectoryName(new Uri(Assembly.GetExecutingAssembly().CodeBase).AbsolutePath) ?? "";
            if (_mediumImageBytes == null)
            {
                var imagePath = Path.Combine(directoryName, "testImage_medium.jpeg");
                _mediumImageBytes = File.ReadAllBytes(imagePath);
                Console.WriteLine("Image loaded!");
            }

            if (_largeImageBytes == null)
            {
                var imagePath = Path.Combine(directoryName, "testImage_large.jpeg");
                _largeImageBytes = File.ReadAllBytes(imagePath);
                Console.WriteLine("Large image loaded!");
            }
            
            if (_smallImageBytes == null)
            {
                var imagePath = Path.Combine(directoryName, "testImage_small.jpeg");
                _smallImageBytes = File.ReadAllBytes(imagePath);
                Console.WriteLine("Small image loaded!");
            }
        }
        
        public object Any(Hello request)
        {
            return new HelloResponse { Result = "Hello, {0}!".Fmt(request.Name) };
        }
        
        [AddHeader(ContentType = "image/jpeg")]
        public Stream Get(HelloImage request)
        {
            Console.WriteLine("Get(HelloImage). Name={0}. Size={1}.", request.Name, request.Size);

            Stream ms;
            if (request.Size == "small")
                ms = new MemoryStream(_smallImageBytes);
            else if (request.Size == "large")
                ms = new MemoryStream(_largeImageBytes);
            else
                ms = new MemoryStream(_mediumImageBytes);

            return ms;
        }
        
//        [AddHeader(ContentType = "image/jpeg")]
//        public Stream Get(HelloLargeImage request)
//        {
//            Console.WriteLine("Get(HelloLargeImage) " + request.ImageName);
//
//            var ms = new MemoryStream(_largeImageBytes);
//            return ms;
//        }
    }
}