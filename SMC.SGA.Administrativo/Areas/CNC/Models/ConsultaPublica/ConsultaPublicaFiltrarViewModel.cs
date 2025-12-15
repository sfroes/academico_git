using SMC.Framework;
using SMC.Framework.Mapper;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.DataAnnotations;
using System.Configuration;

namespace SMC.SGA.Administrativo.Areas.CNC.Models
{
    public class ConsultaPublicaFiltrarViewModel : SMCViewModelBase, ISMCMappable
    {
        [SMCRequired]
        [SMCSize(SMCSize.Grid6_24, SMCSize.Grid24_24, SMCSize.Grid8_24,SMCSize.Grid6_24)]
        public string CodigoVerificacao { get; set; }

        [SMCRequired]
        [SMCSize(SMCSize.Grid18_24, SMCSize.Grid24_24, SMCSize.Grid16_24, SMCSize.Grid18_24)]
        public string NomeCompletoDiplomado { get; set; }

        [SMCHidden]
        public bool ExibirApenasConsulta { get; set; }

        [SMCHidden]
        public string Url => ConfigurationManager.AppSettings["UrlConsultaPublica"];
    }
}