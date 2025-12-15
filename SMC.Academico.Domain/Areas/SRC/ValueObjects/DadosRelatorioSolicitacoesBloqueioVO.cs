using SMC.Framework.Mapper;
using System.Collections.Generic;

namespace SMC.Academico.Domain.Areas.SRC.ValueObjects
{
    public class DadosRelatorioSolicitacoesBloqueioVO : ISMCMappable
    {
        public long Seq { get; set; }

        public string Processo { get; set; }

        public List<DadosRelatorioSolicitacoesBloqueioItemVO> Bloqueios { get; set; }
    }
}
