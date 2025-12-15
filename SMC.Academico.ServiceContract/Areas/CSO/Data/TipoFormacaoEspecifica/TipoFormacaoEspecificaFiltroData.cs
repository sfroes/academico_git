using SMC.Academico.Common.Areas.CSO.Enums;
using SMC.Framework.Mapper;
using SMC.Framework.Model;

namespace SMC.Academico.ServiceContract.Areas.CSO.Data
{
    public class TipoFormacaoEspecificaFiltroData : SMCPagerFilterData, ISMCMappable
    {
        public ClasseTipoFormacao ClasseTipoFormacao { get; set; }

        public TipoCurso? TipoCurso { get; set; }

        public bool? Ativo { get; set; }
    }
}