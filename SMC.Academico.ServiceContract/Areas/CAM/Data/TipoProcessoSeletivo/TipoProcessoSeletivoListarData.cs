using SMC.Academico.Common.Areas.CAM.Enums;
using SMC.Framework;
using SMC.Framework.Mapper;

namespace SMC.Academico.ServiceContract.Areas.CAM.Data
{
    public class TipoProcessoSeletivoListarData : ISMCMappable, ISMCSeq​​
    {
        public long Seq { get; set; }

        public string Descricao { get; set; }

        public bool IngressoDireto { get; set; }

        public TipoCalculoDataAdmissao TipoCalculoDataAdmissao { get; set; }
    }
}
 