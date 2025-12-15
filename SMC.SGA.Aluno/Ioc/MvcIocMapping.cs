using SMC.Academico.Service.Ioc;
using SMC.Financeiro.FIN.Service;
using SMC.Financeiro.Service.FIN;
using SMC.Formularios.UI.Mvc.Attributes;
using SMC.Formularios.UI.Mvc.Controls;
using SMC.Framework.Pdf;
using SMC.Framework.Security;
using SMC.Framework.Service;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.Html;
using SMC.Infraestrutura.ServiceContract.Areas.PDF.Interfaces;
using SMC.Localidades.ServiceContract.Areas.LOC.Interfaces;
using SMC.Localidades.UI.Mvc;
using SMC.Localidades.UI.Mvc.Controls.Phone;
using SMC.SGA.Aluno.App_GlobalResources;

namespace SMC.SGA.Aluno.Ioc
{
    public class MvcIocMapping : SMCMvcIocMapping
    {
        protected override void Configure()
        {
            this.Services
                .Map<ServiceIocMapping>();

            // Registrando serviço para o SGF
            this.Container.RegisterServiceClient<ILocalidadeService>("FONTE_EXTERNA_PAISES");
            this.Container.RegisterServiceClient<ILocalidadeService>("BUSCAR_ESTADOS_BRASIL");
            this.Container.RegisterServiceClient<ILocalidadeService>("BUSCAR_CIDADES_ESTADO");

            // Componentes externos
            this.Container.RegisterType<ISMCEditorFluentConfiguration, AddressFluentConfiguration>("SMCAddress");
            this.Container.RegisterType<ISMCEditorFluentConfiguration, StateCityConfiguration>("SMCStateCity");
            this.Container.RegisterType<ISMCEditorFluentConfiguration, PhoneFluentConfiguration>("SMCPhone");
            this.Container.RegisterType<ISMCEditorFluentConfiguration, SGFFluentConfiguration>(SMCSGFAttribute.KEY_SGF_IOC);

            this.Resources
                .RegisterMetadata<MetadataResource>()
                .RegisterUI<UIResource>();

            this.MapMock = false;
            this.MapPdfBuilder = false;

            //this.CacheProvider.Register<SMC.Framework.UI.Mvc.SMCHttpContextCache>();

            this.Container.RegisterFactory<ISMCAuthorizationService, SMCServiceClientFactory<ISMCAuthorizationService>>();
            this.Container.RegisterFactory<ISMCAuthorization, SMCServiceClientFactory<ISMCAuthorization>>();
            

            Container.RegisterType<ISMCPdfBuilder, IPdfService>();

            Container.RegisterType<INegociacaoParcelaService, FinanceiroService>();

            this.Container.ConfigureMap<SMC.Framework.Security.Ioc.SMCIocMapping>();

            Container
                .ConfigureMap<Framework.Log4Net.SMCIocMapping>();
        }
    }
}