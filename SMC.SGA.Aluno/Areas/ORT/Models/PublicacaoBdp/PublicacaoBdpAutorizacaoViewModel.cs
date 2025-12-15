using SMC.Framework.Model;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.DataAnnotations;
using System;
using System.Collections.Generic;

namespace SMC.SGA.Aluno.Areas.ORT.Models
{
    public class PublicacaoBdpAutorizacaoViewModel : SMCViewModelBase
    {
        #region DataSource

        [SMCDataSource]
        [SMCIgnoreProp]
        public List<SMCDatasourceItem> TipoAutorizacao { get; set; }

        #endregion

        [SMCHidden]
        public virtual long Seq { get; set; }

        [SMCReadOnly]
        public DateTime DataAutorizacao { get; set; }

        [SMCRequired]
        [SMCSelect(nameof(TipoAutorizacao), autoSelectSingleItem: true)]
        public long SeqTipoAutorizacao { get; set; }

        public string TextoAutorizacao { get; set; }

    }
}