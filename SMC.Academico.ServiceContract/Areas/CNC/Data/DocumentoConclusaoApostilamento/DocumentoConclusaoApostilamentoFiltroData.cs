using SMC.Framework.Mapper;
using SMC.Framework.Model;

namespace SMC.Academico.ServiceContract.Areas.CNC.Data
{
    public class DocumentoConclusaoApostilamentoFiltroData : SMCPagerFilterData, ISMCMappable
    {
        public long SeqDocumentoConclusao { get; set; }
    }
}
