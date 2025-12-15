using SMC.Framework.Mapper;

namespace SMC.Academico.ServiceContract.Areas.MAT.Data
{
    public class EfetivacaoTurmaData : ISMCMappable
    {
        public long Seq { get; set; }

        public string Descricao { get; set; }

        public long SeqDivisao { get; set; }

        public string DescricaoDivisao { get; set; }
    }
}
