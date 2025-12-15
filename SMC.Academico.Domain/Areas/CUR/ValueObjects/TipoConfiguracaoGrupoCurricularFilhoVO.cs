using SMC.Framework.Mapper;

namespace SMC.Academico.Domain.Areas.CUR.ValueObjects
{
    public class TipoConfiguracaoGrupoCurricularFilhoVO : ISMCMappable
    {
        public long Seq { get; set; }

        public string Descricao { get; set; }
    }
}
