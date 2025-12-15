using SMC.Academico.Common.Areas.MAT.Enums;
using SMC.Academico.ServiceContract.Areas.TUR.Data;
using SMC.Framework.Mapper;
using System.Collections.Generic;

namespace SMC.Academico.ServiceContract.Areas.MAT.Data
{
    public class SolicitacaoMatriculaTurmaAtividadeSituacaoData : ISMCMappable
    {
        public long Seq { get; set; }

        public long? SeqDivisaoTurma { get; set; }

        public int Codigo { get; set; }

        public short Numero { get; set; }

        public string DescricaoTipoTurma { get; set; }

        public string TurmaFormatado { get; set; }

        public long? SeqConfiguracaoComponente { get; set; }
        
        public string DescricaoConfiguracaoComponente { get; set; }
        
        public string Situacao { get; set; }

        public MotivoSituacaoMatricula Motivo { get; set; }

        public List<TurmaMatriculaListarDetailData> TurmaMatriculaDivisoes { get; set; }

        public TurmaOfertaMatricula Pertence { get; set; }

        public short? Credito { get; set; }

        public string ProgramaTurma { get; set; }

        public long? SeqEntidadeResponsavel { get; set; }

        public bool ExibirEntidadeResponsavelTurma { get; set; }
    }
}
