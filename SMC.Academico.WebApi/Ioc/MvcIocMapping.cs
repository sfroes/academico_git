using SMC.Academico.Service.Ioc;
using SMC.Framework;
using SMC.Framework.Rest;
using SMC.Framework.UI.Mvc;

namespace SMC.Academico.WebApi.Ioc
{
    public class MvcIocMapping : SMCMvcIocMapping
    {
        protected override void Configure()
        {
            this.MapAccessMonitoring = false;
            this.MapAuthorizationService = false;
            this.MapExcelBuilder = false;
            this.MapMock = false;
            this.MapPdfBuilder = false;

            Services.Map<ServiceIocMapping>();

            Container.RegisterType<ISMCApiClientFactory, SMCApiClientFactory>();
        }
    }
}