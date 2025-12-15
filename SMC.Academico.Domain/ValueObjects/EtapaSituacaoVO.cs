using SMC.Formularios.Common.Areas.TMP.Enums;
using SMC.Framework.Mapper;

namespace SMC.Academico.Domain.ValueObjects
{
    public class EtapaSituacaoVO : ISMCMappable
    {
        public long Seq { get; set; }

        public long SeqSituacao { get; set; }

        public string Descricao { get; set; }

        public ClassificacaoSituacaoFinal? ClassificacaoSituacaoFinal { get; set; }

        public CategoriaSituacao CategoriaSituacao { get; set; }

        public bool SituacaoFinalEtapa { get; set; }

        public bool SituacaoFinalProcesso { get; set; }

        public bool SituacaoInicialEtapa { get; set; }

        public bool SituacaoSolicitante { get; set; }
    }
}