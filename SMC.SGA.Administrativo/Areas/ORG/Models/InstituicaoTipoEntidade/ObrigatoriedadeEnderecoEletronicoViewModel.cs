using SMC.Academico.Common.Enums;
using SMC.DadosMestres.Common.Areas.PES.Enums;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.DataAnnotations;

namespace SMC.SGA.Administrativo.Areas.ORG.Models
{
    public class ObrigatoriedadeEnderecoEletronicoViewModel : SMCViewModelBase
    {
        [SMCHidden]
        public long Seq { get; set; }

        [SMCHidden]
        public long SeqInstituicaoTipoEntidade { get; set; }

        [SMCRequired]
        [SMCSelect()]
        [SMCSize(Framework.SMCSize.Grid8_24)]
        public TipoEnderecoEletronico TipoEnderecoEletronico { get; set; }
        
        [SMCRequired]
        [SMCRadioButtonList]
        [SMCSize(Framework.SMCSize.Grid6_24)]
        public bool Obrigatorio { get; set; }

        [SMCSelect()]
        [SMCSize(Framework.SMCSize.Grid8_24)]
        public CategoriaEnderecoEletronico? CategoriaEnderecoEletronico { get; set; }
    }
}