using SMC.Formularios.Common.Areas.TMP.Enums;
using SMC.Framework.Mapper;

namespace SMC.Academico.Domain.Areas.MAT.ValueObjects
{
    public class SituacaoItemMatriculaVO : ISMCMappable
    {
        public long Seq { get; set; }

        public string Descricao { get; set; }

        public long SeqProcessoEtapa { get; set; }

        public ClassificacaoSituacaoFinal? ClassificacaoSituacaoFinal { get; set; }

        public bool SituacaoInicial { get; set; }

        public bool SituacaoFinal { get; set; }
    }
}