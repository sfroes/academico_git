using System.Net;

namespace SMC.Academico.FilesCollection
{
    public class ResultBase
    {
        public bool success { get; set; }
        public string errorMessage { get; set; }
        public HttpStatusCode statusCode { get; set; }
    }
}
