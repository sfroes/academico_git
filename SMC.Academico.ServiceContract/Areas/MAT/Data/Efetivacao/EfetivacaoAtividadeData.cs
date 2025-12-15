using SMC.Framework.Mapper;

namespace SMC.Academico.ServiceContract.Areas.MAT.Data
{
    public class EfetivacaoAtividadeData : ISMCMappable
    {
        public long Seq { get; set; }

        public string Descricao { get; set; }
    }
}
