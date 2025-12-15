using SMC.Academico.Common.Areas.CSO.Enums;
using SMC.Academico.Domain.Areas.CSO.Models;
using SMC.Academico.Domain.Areas.ORG.ValueObjects;
using SMC.Framework.Mapper;
using System.Collections.Generic;

namespace SMC.Academico.Domain.Areas.CSO.ValueObjects
{
    public class ProgramaVO : EntidadeVO, ISMCMappable
    {
        #region Primitive Properties

        public long SeqHierarquiaEntidadeItem { get; set; }

        /// <summary>
        /// Sequencia do ItemHierarquiaEntidade da entidade superior
        /// </summary>
        public long SeqHierarquiaEntidadeItemSuperior { get; set; }

        public string DescricaoEntidadeResponsavel { get; set; }

        public string CodigoCapes { get; set; }

        public long SeqRegimeLetivo { get; set; }

        [SMCMapProperty("RegimeLetivo.Descricao")]
        public string DescricaoRegimeLetivo { get; set; }

        public TipoPrograma TipoPrograma { get; set; }

        [SMCMapProperty("SituacaoAtual.Descricao")]
        public string DescricaoSituacaoAtual { get; set; }

        #endregion Primitive Properties

        #region Navigation Properties

        public IList<FormacaoEspecifica> FormacoesEspecificasEntidade { get; set; }

        public IList<CursoVO> Cursos { get; set; }

        public List<EntidadeIdiomaVO> DadosOutrosIdiomas { get; set; }

        public List<ProgramaHistoricoNotaVO> HistoricoNotas { get; set; }

        public List<ProgramaTipoAutorizacaoBdpVO> TiposAutorizacaoBdp { get; set; }

        #endregion Navigation Properties

        #region [Aba Ato Normativo - BI_ORG_002 - Atos Normativos da Entidade]

        public List<AtoNormativoVisualizarVO> AtoNormativo { get; set; }

        public bool AtivaAbaAtoNormativo { get; set; }

        public bool HabilitaColunaGrauAcademico { get; set; }

        #endregion [Aba Ato Normativo - BI_ORG_002 - Atos Normativos da Entidade]
    }
}