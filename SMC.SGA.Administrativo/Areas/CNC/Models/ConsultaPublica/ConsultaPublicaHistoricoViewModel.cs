using SMC.Academico.Common.Areas.CNC.Enums;
using SMC.Framework.Mapper;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.DataAnnotations;

namespace SMC.SGA.Administrativo.Areas.CNC.Models
{
    public class ConsultaPublicaHistoricoViewModel : SMCViewModelBase, ISMCMappable
    {
        [SMCHidden]
        public long Seq { get; set; }

        [SMCHidden]
        public long SeqDocumentoConclusao { get; set; }

        public TipoInvalidade? TipoInvalidade { get; set; }

        public string DescricaoClassificacaoInvalidadeDocumento { get; set; }

        public string PeriodoInvalidade { get; set; }
    }
}