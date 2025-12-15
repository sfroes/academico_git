using SMC.Academico.Common.Areas.CUR.Enums;
using SMC.Framework.Mapper;

namespace SMC.Academico.Domain.Areas.CUR.ValueObjects
{
    public class ComponenteCurricularCabecalhoVO : ISMCMappable
    {
        [SMCMapProperty("TipoComponente.Descricao")]
        public string DescricaoTipoComponenteCurricular { get; set; }

        public string DescricaoCompleta { get; set; }

        public TipoOrganizacao? TipoOrganizacao { get; set; }
    }
}