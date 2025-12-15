using SMC.Academico.Common.Areas.SRC.Enums;
using SMC.Framework.Mapper;

namespace SMC.Academico.ServiceContract.Areas.SRC.Data
{
    public class ProcessoEtapaFiltroDadoData : ISMCMappable
    {
        public long Seq { get; set; }

        public long SeqProcessoEtapa { get; set; }

        public FiltroDado FiltroDado { get; set; }
    }
}
