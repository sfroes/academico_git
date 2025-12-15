using SMC.Framework.Mapper;
using System.Collections.Generic;

namespace SMC.Academico.Domain.Areas.CUR.ValueObjects
{
    public class EstruturaAtividadeComplementarVO : ISMCMappable
    {
        public List<CategoriaAtividadeComplementarVO> Categorias { get; set; }
    }
}
