using SMC.Framework.Mapper;

namespace SMC.Academico.Domain.Areas.SRC.ValueObjects
{
    public class GrupoDocumentoVO : ISMCMappable
    {
        public long Seq { get; set; }

        public string Descricao { get; set; }

        public int NumeroMinimoDocumentosRequerido { get; set; }

        public override bool Equals(object obj)
        {
            if (!(obj is GrupoDocumentoVO))
                return false;

            return (obj as GrupoDocumentoVO).Seq == Seq;
        }

        public override int GetHashCode()
        {
            return (int)Seq;
        }

    }
}