using SMC.Academico.Common.Areas.CUR.Enums;
using SMC.Framework.Mapper;

namespace SMC.Academico.ServiceContract.Areas.CUR.Data
{
    public class ComponenteCurricularCabecalhoData : ISMCMappable
    {
        [SMCMapProperty("TipoComponente.Descricao")]
        public string DescricaoTipoComponenteCurricular { get; set; }

        public string DescricaoCompleta { get; set; }

        public TipoOrganizacao? TipoOrganizacao { get; set; }
    }
}