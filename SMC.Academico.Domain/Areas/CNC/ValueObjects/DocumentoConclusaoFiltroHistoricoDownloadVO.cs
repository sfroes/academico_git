using SMC.Framework.Mapper;
using SMC.Framework.Model;

namespace SMC.Academico.Domain.Areas.CNC.ValueObjects
{
    public class DocumentoConclusaoFiltroHistoricoDownloadVO : SMCPagerFilterData, ISMCMappable
    {
        public long SeqSolicitacaoDocumentoConclusao { get; set; }
        public string TokenTipoDocumento { get; set; }
    }
}
