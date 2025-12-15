using SMC.Framework;
using SMC.Framework.Mapper;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.DataAnnotations;
using SMC.Framework.UI.Mvc.Html;

namespace SMC.SGA.Administrativo.Areas.CSO.Models
{
    public class ReplicaFormacaoEspecificaProgramaTitulacaoCursoViewModel : SMCViewModelBase, ISMCMappable
    {
        [SMCHidden]
        public long SeqCurso { get; set; }

        [SMCHidden]
        public string DescricaoCurso { get; set; }

        [SMCDetail]
        [SMCSize(SMCSize.Grid24_24)]
        public SMCMasterDetailList<ReplicaFormacaoEspecificaProgramaTitulacaoViewModel> Titulacoes { get; set; }
    }
}