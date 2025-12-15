using SMC.Framework.Mapper;

namespace SMC.SGA.Aluno.Areas.MAT.Models.Matricula
{
    public class GrupoDocumentoViewModel : ISMCMappable
    {
        public long Seq { get; set; }

        public string Descricao { get; set; }

        public int NumeroMinimoDocumentosRequerido { get; set; }

        public bool ExibeGrupo { get; set; }

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