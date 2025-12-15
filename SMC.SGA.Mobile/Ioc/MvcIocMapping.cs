using SMC.Academico.Service.Ioc;
using SMC.Framework.Pdf;
using SMC.Framework.Security;
using SMC.Framework.Service;
using SMC.Framework.UI.Mvc;
using SMC.Infraestrutura.ServiceContract.Areas.PDF.Interfaces;
using SMC.SGA.Mobile.App_GlobalResources;

namespace SMC.SGA.Mobile.Ioc
{
    public class MvcIocMapping : SMCMvcIocMapping
    {
        protected override void Configure()
        {
            this.Services
                .Map<ServiceIocMapping>();

            this.Resources
                .RegisterMetadata<MetadataResource>()
                .RegisterUI<UIResource>();

            this.MapMock = true;

            this.MapPdfBuilder = false;

            //this.CacheProvider.Register<SMC.Framework.UI.Mvc.SMCHttpContextCache>();

            this.Container.RegisterFactory<ISMCAuthorizationService, SMCServiceClientFactory<ISMCAuthorizationService>>();
            this.Container.RegisterFactory<ISMCAuthorization, SMCServiceClientFactory<ISMCAuthorization>>();

            Container.RegisterType<ISMCPdfBuilder, IPdfService>();

            this.Container.ConfigureMap<SMC.Framework.Security.Ioc.SMCIocMapping>();
        }
    }
}