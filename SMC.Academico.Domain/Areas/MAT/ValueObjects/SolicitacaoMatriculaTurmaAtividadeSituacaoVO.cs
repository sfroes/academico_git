using SMC.Academico.Common.Areas.MAT.Enums;
using SMC.Academico.Common.Areas.TUR.Enums;
using SMC.Academico.Domain.Areas.TUR.ValueObjects;
using SMC.Formularios.Common.Areas.TMP.Enums;
using SMC.Framework.Mapper;
using System.Collections.Generic;

namespace SMC.Academico.Domain.Areas.MAT.ValueObjects
{
    public class SolicitacaoMatriculaTurmaAtividadeSituacaoVO : ISMCMappable
    {

        public long? SeqTurma { get; set; }

        /// <summary>
        /// Este sequencial é do item da matrícula
        /// </summary>
        public long Seq { get; set; }

        public long SeqPessoaAtuacao { get; set; }

        public long SeqNivelEnsino { get; set; }

        public long SeqTipoComponenteCurricular { get; set; }

        public long? SeqDivisaoTurma { get; set; }

        public long? SeqConfiguracaoComponente { get; set; }
                
        public long SeqConfiguracaoComponentePrincipal { get; set; }

        public int? Codigo { get; set; }

        public short? Numero { get; set; }

        public string CodigoFormatado { get { return $"{Codigo}.{Numero}"; } }

        public string TurmaFormatado { get { return $"{Codigo}.{Numero} - {DescricaoConfiguracaoComponenteTurma}"; } }

        public string DescricaoTipoTurma { get; set; }

        public string DescricaoConfiguracaoComponente { get; set; }

        public string DescricaoConfiguracaoComponenteTurma { get; set; }

        public long? SeqSituacao { get; set; }

        public List<long> HistoricoSeqSituacao { get; set; }

        public string Situacao { get; set; }

        public MotivoSituacaoMatricula? Motivo { get; set; }                       
        
        public List<TurmaDivisoesVO> TurmaDivisoes { get; set; }

        public List<DivisaoTurmaVO> DivisoesTurma { get; set; }

        public List<TurmaMatriculaListarDetailVO> TurmaMatriculaDivisoes { get; set; }
        
        public TurmaDivisoesVO TurmaDivisoesItem { get; set; }

        public TurmaOfertaMatricula Pertence { get; set; }

        public short? QuantidadeVagas { get; set; }
        
        public AssociacaoOfertaMatriz? AssociacaoOfertaMatrizTipoTurma { get; set; }

        public short? Credito { get; set; }

        public ClassificacaoSituacaoFinal? ClassificacaoSituacaoFinal { get; set; }

        public MatriculaPertencePlanoEstudo SituacaoPlanoEstudo { get; set; }

        public bool? PertencePlanoEstudo { get; set; }

        public string ProgramaTurma { get; set; }

        public string ProgramaCompartilhado { get; set; }

        public long? SeqProgramaTurma { get; set; }

        public long? SeqEntidadeResponsavel { get; set; }

        public bool ExibirEntidadeResponsavelTurma { get; set; }

        public bool? SituacaoInicial { get; set; }

        public bool? SituacaoFinal { get; set; }
    }
}
