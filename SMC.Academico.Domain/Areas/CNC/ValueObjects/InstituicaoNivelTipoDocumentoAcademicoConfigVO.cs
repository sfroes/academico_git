using SMC.Academico.Common.Areas.ORG.Enums;
using SMC.Academico.Domain.Areas.CNC.Models;
using SMC.Framework.Mapper;
using System.Collections.Generic;

namespace SMC.Academico.Domain.Areas.CNC.ValueObjects
{
    public class InstituicaoNivelTipoDocumentoAcademicoConfigVO : ISMCMappable
    {
        public long Seq { get; set; }
        public long SeqInstituicaoNivel { get; set; }
        public UsoSistemaOrigem UsoSistemaOrigem { get; set; }
        public string DescricaoTipoDocumentoAcademico { get; set; }
        public long? SeqConfiguracaoGad { get; set; }
        public string TokenSistemaOrigemGAD { get; set; }
        public InstituicaoNivelTipoDocumentoModelosRelatorioVO ModelosRelatorio { get; set; }
        public List<TipoDocumentoAcademicoTagVO> Tags { get; set; }
        public string DescricaoNivelEnsino { get; set; }
        public string TokenNivelEnsino { get; set; }
    }
}
