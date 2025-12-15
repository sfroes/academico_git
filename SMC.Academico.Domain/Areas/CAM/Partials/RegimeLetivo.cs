using SMC.Framework;

namespace SMC.Academico.Domain.Areas.CAM.Models
{
    public partial class RegimeLetivo
    {
        // Fix: Verificar se deve ser colocado na geração das classes
        #region Override

        public override bool Equals(object obj)
        {
            return this.Seq == (obj as ISMCSeq)?.Seq;
        }

        public override int GetHashCode()
        {
            return this.Seq.GetHashCode();
        }

        #endregion
    }
}
