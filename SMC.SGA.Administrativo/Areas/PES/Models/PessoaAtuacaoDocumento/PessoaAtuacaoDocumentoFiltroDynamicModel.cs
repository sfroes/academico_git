using SMC.Academico.Service.Areas.PES.Services;
using SMC.Academico.ServiceContract.Areas.PES.Interfaces;
using SMC.Framework;
using SMC.Framework.Model;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.DataAnnotations;
using SMC.Framework.UI.Mvc.Dynamic;
using System.Collections.Generic;

namespace SMC.SGA.Administrativo.Areas.PES.Models
{
    public class PessoaAtuacaoDocumentoFiltroDynamicModel : SMCDynamicFilterViewModel
    {
        #region [Data Source]
        //[SMCDataSource]
        //[SMCServiceReference(typeof(IPessoaAtuacaoDocumentoService), nameof(IPessoaAtuacaoDocumentoService.BuscarDocumentosSelect))]
        //public List<SMCDatasourceItem> TiposDocumentos { get; set; }
        #endregion [Data Source]

        //[SMCSize(SMCSize.Grid10_24)]
        //[SMCFilter(true)]
        //[SMCSelect(nameof(TiposDocumentos))]
        //public long? SeqTipoDocumento { get; set; }

        [SMCSize(SMCSize.Grid8_24)]
        [SMCFilter(true)]
        [SMCSelect]
        public bool? EntreguePorSolicitacao { get; set; }

        [SMCParameter]
        [SMCHidden]
        public long? SeqPessoaAtuacao { get; set; }

    }
}