using SMC.Academico.Service.Ioc;
using SMC.AgendadorTarefa.ServiceContract.Areas.ATS.Interfaces;
using SMC.DadosMestres.ServiceContract.Areas.PES.Interfaces;
using SMC.Formularios.UI.Mvc.Attributes;
using SMC.Formularios.UI.Mvc.Controls;
using SMC.Framework;
using SMC.Framework.OpenXml;
using SMC.Framework.Pdf;
using SMC.Framework.Rest;
using SMC.Framework.Security;
using SMC.Framework.Service;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.Html;
using SMC.Infraestrutura.ServiceContract.Areas.PDF.Interfaces;
using SMC.Inscricoes.ServiceContract.Areas.INS.Interfaces;
using SMC.Localidades.ServiceContract.Areas.LOC.Interfaces;
using SMC.Localidades.UI.Mvc;
using SMC.Localidades.UI.Mvc.Controls.Phone;
using SMC.Seguranca.ServiceContract.Areas.APL.Interfaces;
using SMC.SGA.Administrativo.App_GlobalResources;

namespace SMC.SGA.Administrativo.Ioc
{
	public class MvcIocMapping : SMCMvcIocMapping
	{
		protected override void Configure()
		{
			Services.Map<ServiceIocMapping>();

			Resources.RegisterMetadata<MetadataResource>().RegisterUI<UIResource>();

			MapMock = false;

			MapPdfBuilder = false;

			//CacheProvider.Register<SMCHttpContextCache>();

			Container.RegisterFactory<ISMCAuthorizationService, SMCServiceClientFactory<ISMCAuthorizationService>>();
			Container.RegisterFactory<ISMCAuthorization, SMCServiceClientFactory<ISMCAuthorization>>();
			Container.RegisterFactory<IProcessoService, SMCServiceClientFactory<IProcessoService>>();
			Container.RegisterFactory<ITipoProcessoService, SMCServiceClientFactory<ITipoProcessoService>>();
			Container.RegisterFactory<ITipoItemHierarquiaOfertaService, SMCServiceClientFactory<ITipoItemHierarquiaOfertaService>>();
			Container.RegisterFactory<IEtapaProcessoService, SMCServiceClientFactory<IEtapaProcessoService>>();
			Container.RegisterFactory<IAgendamentoService, SMCServiceClientFactory<IAgendamentoService>>();
			Container.RegisterFactory<IHistoricoAgendamentoService, SMCServiceClientFactory<IHistoricoAgendamentoService>>();
			Container.RegisterFactory<IAplicacaoService, SMCServiceClientFactory<IAplicacaoService>>();
			Container.RegisterFactory<IIntegracaoDadoMestreService, SMCServiceClientFactory<IIntegracaoDadoMestreService>>();

			Container.ConfigureMap<Framework.Security.Ioc.SMCIocMapping>();

			// Componentes externos
			Container.RegisterServiceClient<ILocalidadeService>("FONTE_EXTERNA_PAISES");
			Container.RegisterServiceClient<ILocalidadeService>("BUSCAR_ESTADOS_BRASIL");
			Container.RegisterServiceClient<ILocalidadeService>("BUSCAR_CIDADES_ESTADO");
			Container.RegisterType<ISMCEditorFluentConfiguration, AddressFluentConfiguration>("SMCAddress");
			Container.RegisterType<ISMCEditorFluentConfiguration, StateCityConfiguration>("SMCStateCity");
			Container.RegisterType<ISMCEditorFluentConfiguration, PhoneFluentConfiguration>("SMCPhone");
			Container.RegisterType<ISMCEditorFluentConfiguration, SGFFluentConfiguration>(SMCSGFAttribute.KEY_SGF_IOC);

			Container.RegisterType<ISMCPdfBuilder, IPdfService>();

			// Excel - OpenXMLService
			Container.RegisterServiceClient<ISMCWorkbookService>();

			Container.RegisterType<ISMCApiClientFactory, SMCApiClientFactory>();
		}
	}
}