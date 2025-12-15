using SMC.Framework.Mapper;
using System.Collections.Generic;

namespace SMC.Academico.Domain.Areas.ALN.ValueObjects
{
    public class RelatorioHistoricoEscolarVO : ISMCMappable
    {
        public List<ItemRelatorioHistoricoEscolarVO> Alunos { get; set; }
    }
}