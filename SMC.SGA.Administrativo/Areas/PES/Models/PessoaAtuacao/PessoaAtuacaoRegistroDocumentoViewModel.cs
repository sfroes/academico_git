using SMC.Framework;
using SMC.Framework.Model;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.DataAnnotations;
using SMC.Framework.UI.Mvc.Html;
using System.Collections.Generic;

namespace SMC.SGA.Administrativo.Areas.PES.Models
{
    public class PessoaAtuacaoRegistroDocumentoViewModel : SMCViewModelBase, ISMCWizardViewModel
    {
        #region DataSources

        public List<SMCDatasourceItem> SolicitacoesServico { get; set; }
        public List<SMCDatasourceItem> SituacoesEntregaDocumento { get; set; }

        #endregion DataSources

        public int Step { get; set; }

        [SMCHidden]
        public long SeqPessoaAtuacao { get; set; }

        [SMCHidden]
        public long SeqTipoDocumento { get; set; }

        [SMCHidden]
        public bool PermiteVarios { get; set; }

        [SMCSize(SMCSize.Grid12_24)]
        [SMCSelect(nameof(SolicitacoesServico), ForceMultiSelect = true, AutoSelectSingleItem = true)]
        [SMCRequired]
        public List<long> SeqsSolicitacoesServico { get; set; }

        [SMCSize(SMCSize.Grid24_24)]
        public string DescricaoTipoDocumento { get; set; }

        [SMCDetail(SMCDetailType.Block)]
        public SMCMasterDetailList<PessoaAtuacaoRegistroDocumentoItemViewModel> Documentos { get; set; }
    }
}