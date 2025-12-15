using SMC.Academico.UI.Mvc.Lookups;
using SMC.Framework;
using SMC.Framework.DataAnnotations;
using SMC.Framework.Mapper;
using SMC.Framework.Model;
using SMC.Framework.UI.Mvc;
using System;
using System.Collections.Generic;

namespace SMC.SGA.Administrativo.Areas.OFC.Models
{
    public class OfertaCursoCicloLetivoViewModel : SMCViewModelBase, ISMCMappable
    {
        public OfertaCursoCicloLetivoViewModel()
        {
            this.TiposOfertaCurso = new List<SMCSelectItem>();
            this.TiposVinculo = new List<SMCSelectItem>();
        }

        [SMCReadOnly]
        [SMCSize(SMCSize.Grid3_24)]
        public long Seq { get; set; }

        [SMCRequired]
        [SMCSize(SMCSize.Grid11_24)]
        public string Descricao { get; set; }

        [SMCRequired]
        [LookupCicloLetivo]
        [SMCSize(SMCSize.Grid6_24)]
        public SGALookupViewModel SeqCicloLetivoSelecionado { get; set; }
        
        [SMCRequired]
        [SMCSelect("FormasIngresso")]
        [SMCSize(SMCSize.Grid4_24)]
        public long? SeqFormaIngressoSelecionada { get; set; }

        public List<SMCSelectItem> FormasIngresso { get; set; }

        [SMCRequired]
        [SMCSelect("TiposVinculo")]
        [SMCSize(SMCSize.Grid10_24)]
        [SMCDependency("SeqFormaIngressoSelecionada", "FormaIngressoSelecionado", "OfertaCursoCicloLetivo", true)]
        public long? SeqTipoVinculoSelecionado { get; set; }

        public List<SMCSelectItem> TiposVinculo { get; set; }
        
        [SMCRequired]
        [SMCSelect("TiposOfertaCurso")]
        [SMCSize(SMCSize.Grid10_24)]
        [SMCDependency("SeqFormaIngressoSelecionada", "FormaIngressoSelecionado", "OfertaCursoCicloLetivo", true)]
        public long? SeqTipoOfertaCursoSelecionada { get; set; }

        public List<SMCSelectItem> TiposOfertaCurso { get; set; }

        [SMCRequired]
        [SMCSize(SMCSize.Grid4_24)]
        public DateTime DataReferenciaOferta { get; set; }
    }
}