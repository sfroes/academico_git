using SMC.Academico.Common.Areas.ORG.Enums;
using SMC.Framework.Mapper;
using System;
using System.Collections.Generic;

namespace SMC.Academico.Domain.Areas.ORG.ValueObjects
{
    public class InstituicaoEnsinoVO : EntidadeVO, ISMCMappable
    {
        public long SeqMantenedora { get; set; }

        public long SeqCategoriaInstituicao { get; set; }

        public bool? Ativo { get; set; }

        public DateTime? DataDesativacao { get; set; }

        public IntegracaoBiblioteca IntegracaoBiblioteca { get; set; }

        public string EmailContatoBdp { get; set; }

        public long SeqHierarquiaClassificacaoFormacaoAcademica { get; set; }

        #region [Aba Ato Normativo - BI_ORG_002 - Atos Normativos da Entidade]

        public List<AtoNormativoVisualizarVO> AtoNormativo { get; set; }

        public bool AtivaAbaAtoNormativo { get; set; }

        public bool HabilitaColunaGrauAcademico { get; set; }

        #endregion [Aba Ato Normativo - BI_ORG_002 - Atos Normativos da Entidade]
    }
}