using SMC.Academico.Common.Areas.MAT.Enums;
using SMC.Academico.Common.Areas.ORT.Enums;
using SMC.Formularios.Common.Areas.TMP.Enums;
using SMC.Framework.Mapper;
using System.Collections.Generic;

namespace SMC.Academico.Domain.Areas.MAT.ValueObjects
{
    public class SolicitacaoMatriculaItemVO : ISMCMappable
    {
        public long Seq { get; set; }

        public long SeqSolicitacaoMatricula { get; set; }

        public long? SeqDivisaoTurma { get; set; }

        public long? SeqConfiguracaoComponente { get; set; }

        public long? SeqComponenteCurricular { get; set; }

        public string DescricaoFormatada { get; set; }

        public string DescricaoOrdenacao { get; set; }

        public bool PreRequisito { get; set; }

        public bool ObrigatorioOrientador { get; set; }

        public long? SeqTurma { get; set; }

        public List<long> SeqsConfiguracoesComponentes { get; set; }

        public List<long> SeqsAtividadeCoRequisitos { get; set; }

        public short? Credito { get; set; }

        public ClassificacaoSituacaoFinal? ClassificacaoSituacaoFinal { get; set; }

        public string Situacao { get; set; }

        public string SituacaoEtapa { get; set; }

        public bool? SituacaoInicial { get; set; }

        public bool? SituacaoFinal { get; set; }

        public MotivoSituacaoMatricula? Motivo { get; set; }

        public MotivoSituacaoMatricula? MotivoEtapa { get; set; }

        public bool? PertencePlanoEstudo { get; set; }

        public bool? GeraOrientacao { get; set; }

        public long? SeqTipoOrientacao { get; set; }

        public TipoParticipacaoOrientacao? TipoParticipacaoOrientacao { get; set; }

        public long? SeqDivisaoComponente { get; set; }

        public bool? PossuiEtapaExclusao { get; set; }

        public int? AnoTurmaSga { get; set; }

        public int? SemestreTurmaSga { get; set; }
    }
}
