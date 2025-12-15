using SMC.Framework.Mapper;
using System.Collections.Generic;

namespace SMC.Academico.ServiceContract.Areas.SRC.Data
{
    public class HistoricoNavegacaoData : ISMCMappable
    {
        public string Etapa { get; set; }

        public List<HistoricoNavegacaoItemData> Paginas { get; set; }
    }
}