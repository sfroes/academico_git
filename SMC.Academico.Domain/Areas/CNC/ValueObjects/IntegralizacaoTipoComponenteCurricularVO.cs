using SMC.Framework.Mapper;

namespace SMC.Academico.Domain.Areas.CNC.ValueObjects
{
    public class IntegralizacaoTipoComponenteCurricularVO : ISMCMappable
    {
        public long SeqComponenteCurricular { get; set; }

        public long SeqTipoComponenteCurricular { get; set; }

        public string Sigla { get; set; }

        public string Descricao { get; set; }
    }
}
