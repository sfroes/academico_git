using SMC.Academico.Common.Areas.CUR.Enums;
using SMC.Framework.Mapper;
using SMC.Framework.Model;

namespace SMC.Academico.ServiceContract.Areas.CUR.Data
{
    public class GrupoConfiguracaoComponenteFiltroData : SMCPagerFilterData, ISMCMappable
    {
        public long? Seq { get; set; }

        public string Descricao { get; set; }

        public bool? Ativo { get; set; }

        public TipoGrupoConfiguracaoComponente? TipoGrupoConfiguracaoComponente { get; set; }

        public long? SeqConfiguracaoComponente { get; set; }
    }
}