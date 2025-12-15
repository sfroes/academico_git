using SMC.Academico.ReportHost.App_GlobalResources;
using SMC.Academico.Service.Ioc;
using SMC.Framework.Pdf;
using SMC.Framework.UI.Mvc;
using SMC.Infraestrutura.ServiceContract.Areas.PDF.Interfaces;

namespace SMC.Academico.ReportHost.Ioc
{
    public class MvcIocMapping : SMCMvcIocMapping
    {
        protected override void Configure()
        {
            Resources.RegisterMetadata<MetadataResource>().RegisterUI<UIResource>();

            this.Services.Map<ServiceIocMapping>();
            this.MapMock = false;
            this.MapPdfBuilder = false;

            Container.RegisterType<ISMCPdfBuilder, IPdfService>();

            // Excel - OpenXMLService
            //this.Container.RegisterServiceClient<SMC.Framework.OpenXml.ISMCWorkbookService>();
        }
    }
}