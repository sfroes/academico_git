using SMC.Framework;
using SMC.Framework.Model;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.DataAnnotations;
using SMC.Framework.UI.Mvc.Html;
using System.Collections.Generic;

namespace SMC.SGA.Administrativo.Areas.ORT.Models
{
    public class FichaCatalograficaViewModel : SMCViewModelBase
    {
        [SMCIgnoreProp]
        public List<SMCDatasourceItem> BibliotecariosAtivos { get; set; }

        [SMCHidden]
        [SMCKey]
        public long SeqPublicacaoBdp { get; set; }

        [SMCSize(SMCSize.Grid24_24)]
        public string Aluno { get; set; }

        [SMCSize(SMCSize.Grid24_24)]
        public string Titulo { get; set; }
        
        [SMCSize(SMCSize.Grid4_24)]
        [SMCRequired]
        public string Cutter { get; set; }
        
        [SMCSize(SMCSize.Grid4_24)]
        [SMCRequired]
        public string CDU { get; set; }
        
        [SMCSize(SMCSize.Grid16_24)]
        [SMCRequired]
        [SMCSelect(nameof(BibliotecariosAtivos), SortDirection = SMCSortDirection.Ascending)]
        public long? SeqBibliotecario { get; set; }

        [SMCSize(SMCSize.Grid8_24)]
        [SMCRequired]
        [SMCSelect]
        public bool? PossuiIlustracao { get; set; }

        [SMCDetail(SMCDetailType.Tabular)]
        [SMCSize(SMCSize.Grid24_24)]
        public SMCMasterDetailList<AssuntoIdiomaTrabalhoViewModel> Assunto { get; set; }

    }
}