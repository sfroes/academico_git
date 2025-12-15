using SMC.Academico.Common.Areas.MAT.Enums;
using SMC.Academico.Common.Areas.TUR.Enums;
using SMC.Academico.ServiceContract.Areas.CUR.Data;
using SMC.Formularios.Common.Areas.TMP.Enums;
using SMC.Framework.Mapper;
using System.Collections.Generic;

namespace SMC.Academico.ServiceContract.Areas.TUR.Data
{
    public class TurmaMatriculaListarData : ISMCMappable
    {
        public long Seq { get; set; }
              
        public int Codigo { get; set; }

        public short Numero { get; set; }
                
        public string DescricaoTipoTurma { get; set; }

        public string DescricaoConfiguracaoComponente { get; set; }

        public string DescricaoCicloLetivoInicio { get; set; }

        public string DescricaoCicloLetivoFim { get; set; }

        public string CodigoFormatado { get; set; }

        public string TurmaFormatado { get; set; }

        public List<TurmaMatriculaListarDetailData> TurmaMatriculaDivisoes { get; set; }

        public bool Habilitar { get; set; }

        public short? Credito { get; set; }

        public TurmaOfertaMatricula Pertence { get; set; }

        public long SeqIngressante { get; set; }

        public AssociacaoOfertaMatriz AssociacaoOfertaMatrizTipoTurma { get; set; }

        public bool PreRequisito { get; set; }

        public bool ObrigatorioOrientador { get; set; }

        public List<long> SeqsTurmaCoRequisitos { get; set; }

        public string Situacao { get; set; }

        public string SituacaoEtapa { get; set; }

        public ClassificacaoSituacaoFinal? ClassificacaoSituacaoFinal { get; set; }

        public bool? SituacaoInicial { get; set; }

        public bool? SituacaoFinal { get; set; }

        public MotivoSituacaoMatricula? Motivo { get; set; }

        public MotivoSituacaoMatricula? MotivoEtapa { get; set; }

        public bool? PertencePlanoEstudo { get; set; }

        public ComponenteCurricularData ComponenteCurricular { get; set; }
        public long SeqTurma { get; set; }
    }
}