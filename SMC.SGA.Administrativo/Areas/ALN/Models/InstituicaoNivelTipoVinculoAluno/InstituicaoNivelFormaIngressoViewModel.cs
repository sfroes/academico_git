using SMC.Framework;
using SMC.Framework.Mapper;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.DataAnnotations;
using SMC.Framework.UI.Mvc.Html;
using SMC.SGA.Administrativo.Areas.ALN.Controllers;
using System.Collections.Generic;
using System.Linq;

namespace SMC.SGA.Administrativo.Areas.ALN.Models
{
    public class InstituicaoNivelFormaIngressoViewModel : SMCViewModelBase, ISMCStatefulView, ISMCMappable
    { 

        [SMCKey]
        [SMCHidden]
        public long Seq { get; set; }

        [SMCRequired]
        [SMCSelect(nameof(InstituicaoNivelTipoVinculoAlunoDynamicModel.FormasIngressoSelect))]
        [SMCSize(SMCSize.Grid12_24)] 
        public long SeqFormaIngresso { get; set; }

        [SMCDependency(nameof(SeqFormaIngresso), nameof(InstituicaoNivelTipoVinculoAlunoController.BuscarTipoFormaIngresso), "InstituicaoNivelTipoVinculoAluno", true)]
        [SMCDescription] 
        [SMCReadOnly]
        [SMCSize(SMCSize.Grid12_24)]
        public string TipoFormaIngressoDescricao { get; set; }

        [SMCDetail(min: 1)] 
        [SMCHidden(SMCViewMode.List)]
        [SMCMapForceFromTo] 
        [SMCSize(Framework.SMCSize.Grid24_24)]
        public SMCMasterDetailList<InstituicaoNivelTipoProcessoSeletivoViewModel> TiposProcessoSeletivo { get; set; }

        [SMCHidden(SMCViewMode.Insert | SMCViewMode.Edit)]
        public List<string> TiposProcessoSeletivoDescricao => TiposProcessoSeletivo?.Select(s => s.TipoProcessoSeletivoDescricao).ToList();
    }
}