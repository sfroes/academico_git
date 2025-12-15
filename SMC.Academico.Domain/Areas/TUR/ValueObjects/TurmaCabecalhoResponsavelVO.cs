using SMC.Framework.Mapper;

namespace SMC.Academico.Domain.Areas.TUR.ValueObjects
{
    public class TurmaCabecalhoResponsavelVO : ISMCMappable
    {
        public long SeqColaborador { get; set; }

        public string NomeColaborador { get; set; }
    }
}
