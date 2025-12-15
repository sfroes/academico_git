using SMC.Framework.Mapper;

namespace SMC.Academico.ServiceContract.Areas.CNC.Data
{
    public class IntegralizacaoTipoComponenteCurricularData : ISMCMappable
    {
        public long SeqComponenteCurricular { get; set; }

        public long SeqTipoComponenteCurricular { get; set; }

        public string Sigla { get; set; }

        public string Descricao { get; set; }
    }
}
