using SMC.Academico.Common.Areas.CNC.Enums;
using SMC.Academico.Common.Areas.ORG.Enums;
using SMC.Framework;
using SMC.Framework.Mapper;
using SMC.Framework.Model;
using System.Collections.Generic;

namespace SMC.Academico.Domain.Areas.CNC.ValueObjects
{
    public class InstituicaoNivelTipoDocumentoAcademicoVO : ISMCMappable, ISMCSeq
    {
        public List<SMCDatasourceItem> TiposFormacaoEspecifica { get; set; }

        public List<SMCDatasourceItem> ConfiguracaoDiplomaGAD { get; set; }

        public long Seq { get; set; }

        public long SeqInstituicaoNivel { get; set; }

        public long SeqTipoDocumentoAcademico { get; set; }

        public long? SeqConfiguracaoGad { get; set; }

        public string TokenPermissaoEmissaoDocumento { get; set; }

        public bool? HabilitaRegistroDocumento { get; set; }

        public long? SeqGrupoRegistro { get; set; }

        public long? SeqOrgaoRegistro { get; set; }

        public long? SeqConfiguracaoProcessoGed { get; set; }

        public UsoSistemaOrigem UsoSistemaOrigem { get; set; }

        public bool? ExibeTiposFormacao { get; set; }

        public List<InstituicaoNivelTipoDocumentoFormacaoEspecificaVO> FormacoesEspecificas { get; set; }

        public List<InstituicaoNivelTipoDocumentoModelosRelatorioVO> ModelosRelatorio { get; set; }
    }
}
