using SMC.Framework.Mapper;

namespace SMC.Academico.Domain.Areas.CNC.ValueObjects
{
    public class DadosInstituicaoNivelTipoDocumentoAcademicoVO : ISMCMappable
    {
        public long Seq { get; set; }
        public bool HabilitaRegistroDocumento { get; set; }
        public long? SeqGrupoRegistro { get; set; }
        public long? SeqConfiguracaoGad { get; set; }
    }
}
