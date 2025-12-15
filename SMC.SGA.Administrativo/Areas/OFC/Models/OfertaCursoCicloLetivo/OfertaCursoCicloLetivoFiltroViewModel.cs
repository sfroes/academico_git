using SMC.Academico.UI.Mvc.Lookups;
using SMC.Framework;
using SMC.Framework.DataAnnotations;
using SMC.Framework.Mapper;
using SMC.Framework.Model;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.Html;
using SMC.SGA.Administrativo.Areas.ORG.Models;
using System.Collections.Generic;

namespace SMC.SGA.Administrativo.Areas.OFC.Models
{
    public class OfertaCursoCicloLetivoFiltroViewModel : SMCPagerViewModel, ISMCMappable
    {
        [SMCFilter]
        [SMCSize(SMCSize.Grid3_24)]
        public long? Seq { get; set; }

        [SMCFilter]
        [SMCSize(SMCSize.Grid10_24)]
        [SMCMaxLength(100)]
        public string Descricao { get; set; }

        [SMCDataSource(SMCStorageType.Cache)]
        public List<SMCTreeViewNode<NivelEnsinoItemArvoreViewModel>> NiveisEnsino { get; set; }

        [SMCFilter]
        [SMCSize(SMCSize.Grid8_24)]
        [LookupNivelEnsinoAssociado]
        public SGALookupViewModel SeqNivelEnsino { get; set; }

        [SMCFilter]
        [SMCMask("0000")]
        [SMCSize(SMCSize.Grid4_24)]
        public int? AnoInicio { get; set; }

        [SMCFilter]
        [SMCMask("0000")]
        [SMCSize(SMCSize.Grid4_24)]
        public int? AnoFim { get; set; }

        [SMCFilter]
        [SMCSelect("CiclosLetivos")]
        [SMCSize(SMCSize.Grid8_24)]
        public long? SeqCicloLetivoSelecionado { get; set; }

        public List<SMCSelectItem> CiclosLetivos { get; set; }

        [SMCFilter]
        [SMCSelect("FormasIngresso")]
        [SMCSize(SMCSize.Grid8_24)]
        public long? SeqFormaIngressoSelecionado { get; set; }

        public List<SMCSelectItem> FormasIngresso { get; set; }
    }
}