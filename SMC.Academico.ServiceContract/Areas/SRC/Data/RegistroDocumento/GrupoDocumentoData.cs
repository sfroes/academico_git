using SMC.Framework.Mapper;

namespace SMC.Academico.ServiceContract.Areas.SRC.Data
{
    public class GrupoDocumentoData : ISMCMappable
    {
        public long Seq { get; set; }

        public string Descricao { get; set; }

        public int NumeroMinimoDocumentosRequerido { get; set; }

        public override bool Equals(object obj)
        {
            if (!(obj is GrupoDocumentoData))
                return false;

            return (obj as GrupoDocumentoData).Seq == Seq;
        }

        public override int GetHashCode()
        {
            return (int)Seq;
        }
    }
}