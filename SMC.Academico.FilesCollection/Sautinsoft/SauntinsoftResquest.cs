using SMC.Academico.Common.Enums;
using System.Net;

namespace SMC.Academico.FilesCollection
{
    public static class SauntinsoftResquest
    {
        public static T Send<T>(object value, MetodoHttp metodoHttp, string rota) where T : ResultBase
        {
            ServicePointManager.Expect100Continue = false;
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3 | SecurityProtocolType.Tls | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;

            var sautinSoftInvoke = new SautinsoftInvokeHttp();
            return sautinSoftInvoke.Send<T>(value, metodoHttp, rota);
        }
    }
}
