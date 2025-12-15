using SMC.Framework.Mapper;

namespace SMC.Academico.ServiceContract.Areas.CUR.Data
{
    public class DivisaoCurricularItemData : ISMCMappable
    {
        public long Seq { get; set; }

        public string Descricao { get; set; }

        public short Numero { get; set; }
    }
}
