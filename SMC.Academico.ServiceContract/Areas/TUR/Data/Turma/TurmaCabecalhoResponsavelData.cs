using SMC.Framework.Mapper;

namespace SMC.Academico.ServiceContract.Areas.TUR.Data
{
    public class TurmaCabecalhoResponsavelData : ISMCMappable
    {
        public long SeqColaborador { get; set; }

        public string NomeColaborador { get; set; }
    }
}
