using SMC.Framework;
using SMC.Framework.Mapper;
using SMC.Framework.UI.Mvc.DataAnnotations;
using SMC.Framework.UI.Mvc.Dynamic;

namespace SMC.SGA.Administrativo.Areas.CSO.Models
{
    public class InstituicaoTipoEntidadeFormacaoEspecificaListarDynamicModel : SMCDynamicViewModel, ISMCTreeNode
    {
        [SMCKey]
        [SMCOrder(0)]
        [SMCRequired]
        [SMCReadOnly(SMCViewMode.Edit | SMCViewMode.Insert)]
        public override long Seq { get; set; }

        [SMCParameter]
        [SMCHidden]
        public long? SeqPai { get; set; }

        [SMCHidden]
        [SMCParameter]
        [SMCMapProperty("TipoFormacaoEspecificaPai.TipoFormacaoEspecifica.Descricao")]
        [SMCInclude("TipoFormacaoEspecificaPai.TipoFormacaoEspecifica")]
        public string TipoFormacaoSuperior { get; set; }

        [SMCInclude("TipoFormacaoEspecifica")]
        [SMCMapProperty("TipoFormacaoEspecifica.Descricao")]
        [SMCReadOnly]
        [SMCSize(SMCSize.Grid20_24)]
        [SMCOrder(1)]
        [SMCDescription]
        public string Descricao { get; set; }

        [SMCParameter]
        [SMCHidden]
        public long SeqInstituicaoTipoEntidade { get; set; }

        [SMCInclude("InstituicaoTipoEntidade.TipoEntidade")]
        [SMCMapProperty("InstituicaoTipoEntidade.TipoEntidade.Descricao")]
        [SMCParameter(Name = "Description")] //Para passar a descrição do pai para o no filho (quando for novo)
        [SMCHidden]
        public string DescricaoInstituicaoTipoEntidade { get; set; }
    }
}