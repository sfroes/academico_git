using SMC.Framework.Mapper;
using SMC.Framework.Model;

namespace SMC.Academico.ServiceContract.Areas.CNC.Data
{
    public class DocumentoConclusaoFiltroHistoricoDownloadData : SMCPagerFilterData, ISMCMappable
    {
        public long SeqSolicitacaoDocumentoConclusao { get; set; }
        public string TokenTipoDocumento { get; set; }
    }
}
