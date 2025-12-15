using SMC.Academico.Common.Areas.ALN.Enums;
using SMC.Framework;
using SMC.Framework.UI.Mvc.DataAnnotations;
using SMC.Framework.UI.Mvc.Dynamic;
using SMC.Framework.UI.Mvc.Html;
using System;
using System.Collections.Generic;

namespace SMC.SGA.Administrativo.Areas.ALN.Models
{
    public class ParceriaIntercambioListarDynamicModel : SMCDynamicViewModel
    {
        [SMCKey]
        public override long Seq { get; set; }

        [SMCFilter]
        [SMCMaxLength(100)]
        [SMCSize(SMCSize.Grid12_24)]
        [SMCOrder(0)]
        [SMCSortable(true, true)]
        public string Descricao { get; set; }

        public TipoParceriaIntercambio TipoParceriaIntercambio { get; set; }

        public bool Ativo
        {
            get { return (DataInicio <= DateTime.Today && (!DataFim.HasValue || DataFim.Value >= DateTime.Today)) ? true : false; }
        }

        public bool ProcessoNegociacao { get; set; }

        [SMCHidden]
        public SMCMasterDetailList<ParceriaIntercambioArquivoViewModel> Arquivos { get; set; }

        public bool PossuiAnexo
        {
            get { return (Arquivos != null && Arquivos.Count > 0) ? true : false; }
        }

        public DateTime DataInicio { get; set; }

        public DateTime? DataFim { get; set; }

        public List<string> TiposTermo { get; set; }

        public List<string> InstituicoesExternas { get; set; }

        public int TotalTermos { get; set; }
    }
}