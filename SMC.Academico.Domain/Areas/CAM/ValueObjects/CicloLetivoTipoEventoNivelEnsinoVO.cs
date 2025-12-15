using SMC.Framework.Mapper;

namespace SMC.Academico.Domain.Areas.CAM.ValueObjects
{
    public class CicloLetivoTipoEventoNivelEnsinoVO : ISMCMappable
    {
        public long Seq { get; set; }

        [SMCMapProperty("NiveisEnsino.Descricao")]
        public string Descricao { get; set; }
    }
}