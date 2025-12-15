using SMC.Framework.Mapper;

namespace SMC.Academico.Domain.Areas.PES.ValueObjects
{
    class GrupoAuxiliarDocumentoVO : ISMCMappable
    {
        public long SeqTipoDocumento { get; set; }
        public string DescricaoTipoDocumento { get; set; }
        public short MinimoObrigatorio { get; set; }
        public string NomeGrupo { get; set; }
    }
}
