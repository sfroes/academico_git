using SMC.Framework.Mapper;
using System.Collections.Generic;

namespace SMC.Academico.Domain.Areas.ALN.ValueObjects
{
    public class RelatorioDisciplinasCursadasVO : ISMCMappable
    {
        public List<ItemRelatorioDisciplinasCursadasVO> Alunos { get; set; }
    }
}