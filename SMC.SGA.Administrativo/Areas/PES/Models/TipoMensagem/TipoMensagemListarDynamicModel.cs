using SMC.Academico.Common.Areas.PES.Enums;
using SMC.Framework;
using SMC.Framework.Mapper;
using SMC.Framework.Model;
using SMC.Framework.UI.Mvc.DataAnnotations;
using SMC.Framework.UI.Mvc.Dynamic;
using SMC.Framework.UI.Mvc.Html;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace SMC.SGA.Administrativo.Areas.PES.Models
{
    public class TipoMensagemListarDynamicModel : SMCDynamicViewModel
    {
        [SMCKey]
        [SMCHidden]
        public override long Seq { get; set; }
         
        public string Descricao { get; set; }
         
        public CategoriaMensagem CategoriaMensagem { get; set; }
 
        public bool PermiteCadastroManual { get; set; } 

        [SMCDetail(SMCDetailType.Tabular, min: 1)]
        [SMCSize(SMCSize.Grid12_24)]
        public SMCMasterDetailList<TipoMensagemTipoAtuacaoViewModel> TiposAtuacao { get; set; }

        [SMCDetail(SMCDetailType.Tabular, min: 1)]
        [SMCSize(SMCSize.Grid12_24)]
        public SMCMasterDetailList<TipoMensagemTipoUsoViewModel> TiposUso { get; set; }
    }
}