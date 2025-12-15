using SMC.Framework;
using SMC.Framework.Mapper;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.DataAnnotations;

namespace SMC.SGA.Administrativo.Areas.ORG.Models
{
    public class SituacaoInstituicaoTipoEntidadeViewModel : SMCViewModelBase
    {
        public SituacaoInstituicaoTipoEntidadeViewModel()
        {
            this.Ativo = true;
        }

        [SMCHidden]
        public long Seq { get; set; }

        [SMCHidden]
        public long SeqInstituicaoTipoEntidade { get; set; }

        [SMCOrder(0)]
        [SMCRequired]
        [SMCSelect("SituacoesEntidade")]
        [SMCSize(SMCSize.Grid14_24)]
        public long SeqSituacaoEntidade { get; set; }

        [SMCOrder(1)]
        [SMCRequired]
        [SMCRadioButtonList]
        [SMCSize(SMCSize.Grid8_24)]
        [SMCMapForceFromTo]
        public bool Ativo { get; set; }
    }
}