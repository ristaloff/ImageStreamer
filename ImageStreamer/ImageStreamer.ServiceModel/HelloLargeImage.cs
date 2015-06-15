using ServiceStack;

namespace ImageStreamer.ServiceModel
{
    [Route("/hellolargeimage/{ImageName}")]
    public class HelloLargeImage
    {
        public string ImageName { get; set; }
    }
}