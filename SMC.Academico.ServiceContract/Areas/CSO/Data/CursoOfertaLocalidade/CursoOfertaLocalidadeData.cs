using SMC.Academico.ServiceContract.Areas.ORG.Data;
using SMC.Framework.Mapper;
using System.Collections.Generic;

namespace SMC.Academico.ServiceContract.Areas.CSO.Data
{
    public class CursoOfertaLocalidadeData : EntidadeData, ISMCMappable
    {
        [SMCMapProperty("SeqCursoUnidade")]
        public long SeqEntidade { get; set; }

        public long SeqCurso { get; set; }

        /// <summary>
        /// Sequencial do nível de ensino do curso associado
        /// </summary>
        public long SeqNivelEnsino { get; set; }

        /// <summary>
        /// Sequencial da instituição nível ensino do curso associado
        /// </summary>
        public long SeqInstituicaoNivel { get; set; }

        /// <summary>
        /// Sequencial da situação do curso associado
        /// </summary>
        public long SeqSituacaoAtual { get; set; }

        public long SeqSituacaoAtualCursoOfertaLocalidade { get; set; }

        /// <summary>
        /// Sequencial dos HierarquiaItem que representam as entidades responsáveis pelo curso
        /// </summary>
        public List<long> SeqsEntidadesResponsaveis { get; set; }

        public long SeqCursoUnidade { get; set; }

        /// <summary>
        /// Descrição da oferta de curso oferta (desconsiderado na edição)
        /// </summary>
        public string DescricaoCursoOferta { get; set; }

        public long SeqCursoOferta { get; set; }

        public long? SeqModalidade { get; set; }

        /// <summary>
        /// Sequencial do HierarquiaEntidadeItem que representa a localidade a qual o CursoOfertaLocalidade será vinculado
        /// </summary>
        public long SeqLocalidade { get; set; }

        /// <summary>
        /// Nome da localidade do curso oferta (desconsiderado na edição)
        /// </summary>
        public string NomeLocalidade { get; set; }

        public int? Codigo { get; set; }

        /// <summary>
        /// Sequencial da formação específica da oferta de curso
        /// </summary>
        public long? SeqFormacaoEspecifica { get; set; }

        public List<FormacaoEspecificaHierarquiaData> FormacoesEspecificas { get; set; }

        public List<CursoOfertaLocalidadeTurnoData> Turnos { get; set; }

        public int SeqOrigemFinanceira { get; set; }

        public int? CodigoOrgaoRegulador { get; set; }

        public int? CodigoHabilitacao { get; set; }

        public bool? Hibrido { get; set; }

        public bool? RestricaoCursoOferta { get; set; }

        #region [Aba Ato Normativo - BI_ORG_002 - Atos Normativos da Entidade]

        public List<AtoNormativoVisualizarData> AtoNormativo { get; set; }

        public bool AtivaAbaAtoNormativo { get; set; }

        public bool HabilitaColunaGrauAcademico { get; set; }

        #endregion [Aba Ato Normativo - BI_ORG_002 - Atos Normativos da Entidade]
    }
}