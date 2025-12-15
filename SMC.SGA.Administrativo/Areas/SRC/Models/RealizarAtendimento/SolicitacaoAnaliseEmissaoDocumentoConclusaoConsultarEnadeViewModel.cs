using SMC.Framework;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.DataAnnotations;

namespace SMC.SGA.Administrativo.Areas.SRC.Models
{
    public class SolicitacaoAnaliseEmissaoDocumentoConclusaoConsultarEnadeViewModel 
    {
        [SMCValueEmpty("-")]
        [SMCSize(SMCSize.Grid5_24)]
        public string Categoria { get; set; }

        [SMCValueEmpty("-")]
        [SMCSize(SMCSize.Grid3_24)]
        public int? Ano { get; set; }

        [SMCValueEmpty("-")]
        [SMCSize(SMCSize.Grid8_24)]
        public string Condicao { get; set; }

        [SMCValueEmpty("-")]
        [SMCSize(SMCSize.Grid8_24)]
        public string Descricao { get; set; }
    }
}