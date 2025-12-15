using SMC.Framework;
using SMC.Framework.Mapper;

namespace SMC.Academico.Domain.Areas.CSO.ValueObjects
{
    public class ClassificacaoVO : ISMCMappable, ISMCSeq​​
    {
        public long Seq { get; set; }

        public string Descricao { get; set; }

        public string DescricaoTipoClassificacao { get; set; }

        public string CodigoExterno { get; set; }

        public long SeqHierarquiaClassificacao { get; set; }

        public long SeqTipoClassificacao { get; set; }

        public long? SeqClassificacaoSuperior { get; set; }

        public bool? TipoClassificacaoSelecionavel { get; set; }
    }
}