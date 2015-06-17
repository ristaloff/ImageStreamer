using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ImageStreamer.ServiceModel;
using ServiceStack;
using ServiceStack.Logging;
using ServiceStack.Text;
using System.Net;

namespace ImageStreamer.ImageStreamerClient
{
    static class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Starting ImageStreamerClient ...");
            //LogManager.LogFactory = new DebugLogFactory(); //Todo get JsonServiceClient to provide logging
             //var baseUri = "http://192.168.1.19:8000/";
            var baseUri = "http://192.168.226.129:8000/";
            JsonServiceClient client;
            try
            {
                client = new JsonServiceClient(baseUri);                
            }
            catch (Exception ex)
            {
                Console.WriteLine("Failed to create JsonServiceClient(" + baseUri + "): " + ex);
                return;
            }
            var stopWatch = new Stopwatch();
            int i = 0;
            while (true)
            {
                i++;
                stopWatch.Restart();
                try
                {
                    var response = client.Get(new HelloImage() {Name = i.ToString("d5"), Size = "small"});
//                    var response = client.Get(new HelloImage() {Name = i.ToString("d5"), Size = "medium"});
//                    var response = client.Get(new HelloImage() {Name = i.ToString("d5"), Size = "large"});

                    if (response == null)
                        Console.WriteLine("Response is null!");
                    else
                    {
                        using (var stream = response.GetResponseStream())
                        {
                            if (stream != null)
                            {
                                stream.ReadTimeout = stream.WriteTimeout = 1000;  //Set timeout on the System.NetConnectStream
                                using (var sr = new StreamReader(stream))
                                {
                                    var imageBytes = stream.ReadFully();
                                    var elapsedMilliseconds = stopWatch.ElapsedMilliseconds;
                                    SetConsoleTextColor(elapsedMilliseconds);
                                    Console.WriteLine(i.ToString("D5") + " Response lenght: " + imageBytes.Length +
                                                      ". Time elapsed: " + elapsedMilliseconds.ToString("D4") + "ms");
                                }
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Failed to get Response: " + ex);
                    var sleepTime = 100;
                    Console.WriteLine("\nRetry in {0} ms.", sleepTime);
                    Thread.Sleep(sleepTime);
                }
                //Thread.Sleep(20);
            }
        }

        private static void SetConsoleTextColor(long elapsedMilliseconds)
        {
            if (elapsedMilliseconds > 100)
                Console.ForegroundColor = ConsoleColor.Red;
            else if (elapsedMilliseconds > 60)
                Console.ForegroundColor = ConsoleColor.Yellow;
            else
                Console.ForegroundColor = ConsoleColor.Green;
        }
    }
}
