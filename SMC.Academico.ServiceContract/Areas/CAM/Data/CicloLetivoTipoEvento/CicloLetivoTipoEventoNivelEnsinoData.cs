using SMC.Framework.Mapper;

namespace SMC.Academico.ServiceContract.Areas.CAM.Data
{
    public class CicloLetivoTipoEventoNivelEnsinoData : ISMCMappable
    {
        public long Seq { get; set; }

        public string Descricao { get; set; }
    }
}