using SMC.Framework.Mapper;

namespace SMC.Academico.ServiceContract.Areas.PES.Data
{
    public class TipoBloqueioData : ISMCMappable
    {
        public long Seq { get; set; }

        public string Descricao { get; set; }

        public string Token { get; set; }

        public bool Ativo { get; set; }
    }
}