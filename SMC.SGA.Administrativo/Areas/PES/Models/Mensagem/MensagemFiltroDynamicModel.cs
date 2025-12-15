using SMC.Academico.Common.Areas.PES.Enums;
using SMC.Academico.ServiceContract.Areas.PES.Interfaces;
using SMC.Framework;
using SMC.Framework.Model;
using SMC.Framework.UI.Mvc.DataAnnotations;
using SMC.Framework.UI.Mvc.Dynamic;
using System.Collections.Generic;

namespace SMC.SGA.Administrativo.Areas.PES.Models
{
    public class MensagemFiltroDynamicModel : SMCDynamicFilterViewModel
    {
        #region [ DataSources ]

        [SMCDataSource]
        [SMCIgnoreProp]
        [SMCServiceReference(typeof(IInstituicaoNivelTipoMensagemService), nameof(IInstituicaoNivelTipoMensagemService.BuscarTiposMensagemSelect), values: new[] { nameof(SeqPessoaAtuacao), nameof(ApenasCadastroManual) })]
        public List<SMCDatasourceItem> TiposMensagem { get; set; }

        #endregion [ DataSources ]

        [SMCHidden]
        public long SeqPessoaAtuacao { get; set; }

        [SMCHidden]
        public bool ApenasCadastroManual { get; } = false;

        [SMCFilter(true, true)]
        [SMCSelect]
        [SMCSize(SMCSize.Grid4_24, SMCSize.Grid24_24, SMCSize.Grid12_24, SMCSize.Grid4_24)]
        public CategoriaMensagem? CategoriaMensagem { get; set; }

        [SMCFilter(true, true)]
        [SMCSize(SMCSize.Grid6_24, SMCSize.Grid24_24, SMCSize.Grid11_24, SMCSize.Grid6_24)]
        [SMCSelect(nameof(TiposMensagem), SortBy = SMCSortBy.Description)]
        public long? SeqTipoMensagem { get; set; }

        [SMCFilter(true, true)]
        [SMCSelect]
        [SMCSize(SMCSize.Grid6_24, SMCSize.Grid24_24, SMCSize.Grid13_24, SMCSize.Grid6_24)]
        public bool? MensagensValidas { get; set; } = true;
    }
}