using ServiceStack;

namespace ImageStreamer.ServiceModel
{
    [Route("/helloimage/{Name}")]
    [Route("/helloimage/{Name}/{Size}")]
    public class HelloImage
    {
        public string Name { get; set; }
        public string Size { get; set; }
    }
}