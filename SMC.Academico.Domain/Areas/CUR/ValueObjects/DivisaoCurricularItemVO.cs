using SMC.Framework.Mapper;

namespace SMC.Academico.Domain.Areas.CUR.ValueObjects
{
    public class DivisaoCurricularItemVO : ISMCMappable
    {
        public long Seq { get; set; }

        public string Descricao { get; set; }

        public short Numero { get; set; }
    }
}
