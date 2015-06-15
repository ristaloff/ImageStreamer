using ServiceStack;

namespace ImageStreamer.ServiceModel
{
    [Route("/helloimage/{ImageName}")]
    public class HelloImage
    {
        public string ImageName { get; set; }
    }
}