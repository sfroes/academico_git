using SMC.Academico.Common.Areas.TUR.Enums;
using SMC.Framework;
using SMC.Framework.Mapper;

namespace SMC.Academico.ServiceContract.Areas.TUR.Data
{
    public class TipoTurmaData : ISMCMappable, ISMCSeq
    {
        public long Seq { get; set; }

        public string Descricao { get; set; }

        public AssociacaoOfertaMatriz AssociacaoOfertaMatriz { get; set; }
    }
}
