using SMC.Framework.Mapper;
using System.Collections.Generic;

namespace SMC.Academico.Domain.Areas.CUR.ValueObjects
{
    public class EmentaVO : ISMCMappable
    {
        public List<ItemEmentaVO> Itens { get; set; }
    }
}
