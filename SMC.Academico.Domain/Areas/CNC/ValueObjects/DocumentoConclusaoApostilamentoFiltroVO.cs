using SMC.Framework.Mapper;
using SMC.Framework.Model;

namespace SMC.Academico.Domain.Areas.CNC.ValueObjects
{
    public class DocumentoConclusaoApostilamentoFiltroVO : SMCPagerFilterData, ISMCMappable
    {
        public long SeqDocumentoConclusao { get; set; }
    }
}
