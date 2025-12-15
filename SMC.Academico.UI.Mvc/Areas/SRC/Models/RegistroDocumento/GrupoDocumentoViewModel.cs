using SMC.Framework.Mapper;

namespace SMC.Academico.UI.Mvc.Areas.SRC.Models
{
    public class GrupoDocumentoViewModel : ISMCMappable
    {
        public long Seq { get; set; }

        public string Descricao { get; set; }

        public int NumeroMinimoDocumentosRequerido { get; set; }

        public override bool Equals(object obj)
        {
            if (!(obj is GrupoDocumentoViewModel))
                return false;

            return (obj as GrupoDocumentoViewModel).Seq == Seq;
        }

        public override int GetHashCode()
        {
            return (int)Seq;
        }
    }
}