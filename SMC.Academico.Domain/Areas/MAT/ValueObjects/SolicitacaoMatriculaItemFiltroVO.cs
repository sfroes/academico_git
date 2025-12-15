using SMC.Formularios.Common.Areas.TMP.Enums;
using SMC.Framework.Mapper;
using System.Collections.Generic;

namespace SMC.Academico.Domain.Areas.MAT.ValueObjects
{
    public class SolicitacaoMatriculaItemFiltroVO : ISMCMappable
    {
        public long? Seq { get; set; }

        public long? SeqSolicitacaoMatricula { get; set; }

        public long? SeqDivisaoTurma { get; set; }

        public List<long> SeqsDivisoesTurma { get; set; }

        public long? SeqConfiguracaoComponente { get; set; }

        public bool? ExibirTurma { get; set; }

        public ClassificacaoSituacaoFinal? ClassificacaoSituacaoFinal { get; set; }

        public ClassificacaoSituacaoFinal[] ClassificacaoSituacoesFinaisDiferentes { get; set; }
    }
}
