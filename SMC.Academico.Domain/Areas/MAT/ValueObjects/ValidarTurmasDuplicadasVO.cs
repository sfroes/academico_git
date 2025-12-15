using SMC.Formularios.Common.Areas.TMP.Enums;
using SMC.Framework.Mapper;
using System.Collections.Generic;

namespace SMC.Academico.Domain.Areas.MAT.ValueObjects
{
    public class ValidarTurmasDuplicadasVO : ISMCMappable
    {
        public long SeqItem { get; set; }

        public long? SeqDivisaoTurma { get; set; }

        public bool PertencePlanoEstudo { get; set; }

        public long SeqTurma { get; set; }

        public int CodigoTurma { get; set; }

        public short NumeroTurma { get; set; }

        public string DescricaoConfiguracaoComponenteAluno { get; set; }

        public long? SeqComponenteCurricularAssunto { get; set; }

        public long? SeqCompomentecurricularAssuntoPrincipal { get; set; }

        public List<ValidarTurmasDuplicadasConfigComponenteVO> ConfiguracoesComponente { get; set; }

        public ValidarTurmasDuplicadasHistoricoSituacaoVO HistoricoSituacaoAtual { get; set; }
    }

    public class ValidarTurmasDuplicadasConfigComponenteVO : ISMCMappable
    {
        public long SeqConfiguracaoComponente { get; set; }

        public bool Principal { get; set; }

        public string Descricao { get; set; }
    }

    public class ValidarTurmasDuplicadasHistoricoSituacaoVO : ISMCMappable
    {
        public long SeqProcessoEtapa { get; set; }

        public long SeqSituacaoItemMatricula { get; set; }

        public bool SituacaoInicial { get; set; }

        public bool SituacaoFinal { get; set; }

        public ClassificacaoSituacaoFinal? ClassificacaoSituacaoFinal { get; set; }
    }
}