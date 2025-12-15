using SMC.Framework.Mapper;

namespace SMC.Academico.ServiceContract.Areas.CUR.Data
{
    public class TipoConfiguracaoGrupoCurricularFilhoData : ISMCMappable
    {
        public long Seq { get; set; }

        public  string Descricao { get; set; }
    }
}