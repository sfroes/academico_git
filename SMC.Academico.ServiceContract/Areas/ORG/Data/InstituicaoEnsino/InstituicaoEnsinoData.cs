using SMC.Academico.Common.Areas.ORG.Enums;
using System;
using System.Collections.Generic;

namespace SMC.Academico.ServiceContract.Areas.ORG.Data
{
    public class InstituicaoEnsinoData : EntidadeData
    {
        public long SeqMantenedora { get; set; }

        public long SeqCategoriaInstituicao { get; set; }

        public bool? Ativo { get; set; }

        public DateTime? DataDesativacao { get; set; }

        public IntegracaoBiblioteca IntegracaoBiblioteca { get; set; }

        public string EmailContatoBdp { get; set; }

        public long SeqHierarquiaClassificacaoFormacaoAcademica { get; set; }

        #region [Aba Ato Normativo - BI_ORG_002 - Atos Normativos da Entidade]

        public List<AtoNormativoVisualizarData> AtoNormativo { get; set; }

        public bool AtivaAbaAtoNormativo { get; set; }

        public bool HabilitaColunaGrauAcademico { get; set; }

        #endregion [Aba Ato Normativo - BI_ORG_002 - Atos Normativos da Entidade]
    }
}
