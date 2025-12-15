using SMC.Academico.Common.Areas.CSO.Enums;
using SMC.Academico.ServiceContract.Areas.ORG.Data;
using SMC.Framework;
using SMC.Framework.Mapper;
using System.Collections.Generic;

namespace SMC.Academico.ServiceContract.Areas.CSO.Data
{
    public class ProgramaData : EntidadeData, ISMCMappable, ISMCSeq​​
    {
        public long SeqHierarquiaEntidadeItem { get; set; }

        public long SeqHierarquiaEntidadeItemSuperior { get; set; }

        public string DescricaoEntidadeResponsavel { get; set; }

        public string CodigoCapes { get; set; }

        public long SeqRegimeLetivo { get; set; }

        public string DescricaoRegimeLetivo { get; set; }

        public TipoPrograma TipoPrograma { get; set; }

        public List<EntidadeIdiomaData> DadosOutrosIdiomas { get; set; }

        public List<ProgramaHistoricoNotaData> HistoricoNotas { get; set; }

        public List<ProgramaTipoAutorizacaoBdpData> TiposAutorizacaoBdp { get; set; }

        #region [Aba Ato Normativo - BI_ORG_002 - Atos Normativos da Entidade]

        public List<AtoNormativoVisualizarData> AtoNormativo { get; set; }

        public bool AtivaAbaAtoNormativo { get; set; }

        public bool HabilitaColunaGrauAcademico { get; set; }

        #endregion [Aba Ato Normativo - BI_ORG_002 - Atos Normativos da Entidade]
    }
}