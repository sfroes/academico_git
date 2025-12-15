using SMC.DadosMestres.Common.Areas.PES.Enums;
using SMC.Framework.Mapper;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.DataAnnotations;

namespace SMC.Academico.UI.Mvc.Areas.PES.Models
{
    public class EnderecoEletronicoViewModel : SMCViewModelBase, ISMCMappable
    {
        [SMCHidden]
        public long Seq { get; set; }

        [SMCRequired]
        [SMCSelect]
        [SMCSize(Framework.SMCSize.Grid9_24, Framework.SMCSize.Grid24_24, Framework.SMCSize.Grid24_24, Framework.SMCSize.Grid9_24)]
        public TipoEnderecoEletronico TipoEnderecoEletronico { get; set; }

        [SMCDescription]
        [SMCRequired]
        [SMCSize(Framework.SMCSize.Grid13_24, Framework.SMCSize.Grid24_24, Framework.SMCSize.Grid24_24, Framework.SMCSize.Grid13_24)]
        [SMCMaxLength(100)]
        public string Descricao { get; set; }
    }
}