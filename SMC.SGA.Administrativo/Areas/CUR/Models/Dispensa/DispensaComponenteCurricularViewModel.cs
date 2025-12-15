using SMC.Framework.Mapper;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.DataAnnotations;

namespace SMC.SGA.Administrativo.Areas.CUR.Models
{
    public class DispensaComponenteCurricularViewModel : SMCViewModelBase, ISMCMappable
    {
        [SMCHidden]
        [SMCKey]
        public long? Seq { get; set; }

        public string Codigo { get; set; }

        public string Descricao { get; set; }

        [SMCDescription]
        [SMCSize(Framework.SMCSize.Grid24_24)]
        public string DescricaoCompleta { get { return $"{Codigo} - {Descricao}"; } }
    }
}