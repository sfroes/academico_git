using SMC.Framework.Mapper;
using System.Collections.Generic;

namespace SMC.Academico.Domain.Areas.SRC.ValueObjects
{
    public class HistoricoNavegacaoVO : ISMCMappable
    {
        public string Etapa { get; set; }

        public List<HistoricoNavegacaoItemVO> Paginas { get; set; }
    }
}