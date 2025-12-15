using SMC.Academico.Common.Constants;
using SMC.Framework;
using SMC.Framework.Mapper;
using SMC.Framework.UI.Mvc.DataAnnotations;
using SMC.Framework.UI.Mvc.Dynamic;

namespace SMC.SGA.Administrativo.Areas.ORG.Models
{
    public class InstituicaoTipoEntidadeListarDynamicModel : SMCDynamicViewModel
    {
        [SMCOrder(0)]
        [SMCKey]
        [SMCReadOnly]
        [SMCSize(SMCSize.Grid4_24)]
        [SMCHidden(SMCViewMode.Filter)]
        [SMCSortable(true)]
        public override long Seq { get; set; }

        [SMCOrder(1)]
        [SMCHidden()]
        [SMCParameter(FILTER.INSTITUICAO_ENSINO, true)]
        public long SeqInstituicaoEnsino { get; set; }

        [SMCOrder(2)]
        [SMCDescription]
        [SMCHidden(SMCViewMode.Edit | SMCViewMode.Insert)]
        [SMCMapProperty("TipoEntidade.Descricao")]
        [SMCInclude("TipoEntidade")]
        [SMCSortable(true,true,"TipoEntidade.Descricao")]
        public string DescricaoTipoEntidade { get; set; }

      
    }
}