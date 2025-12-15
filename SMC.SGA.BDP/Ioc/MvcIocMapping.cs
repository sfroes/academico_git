using SMC.Academico.Service.Ioc;
using SMC.Framework.Security;
using SMC.Framework.Service;
using SMC.Framework.UI.Mvc;
using SMC.SGA.BDP.App_GlobalResources;

namespace SMC.SGA.BDP.Ioc
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

            this.MapMock = false;

            this.MapPdfBuilder = false;

            //this.CacheProvider.Register<SMC.Framework.UI.Mvc.SMCHttpContextCache>();

            this.Container.RegisterFactory<ISMCAuthorizationService, SMCServiceClientFactory<ISMCAuthorizationService>>();
            this.Container.RegisterFactory<ISMCAuthorization, SMCServiceClientFactory<ISMCAuthorization>>();            

            this.Container.ConfigureMap<SMC.Framework.Security.Ioc.SMCIocMapping>();
        }
    }
}