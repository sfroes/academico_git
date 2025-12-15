using SMC.Academico.Common.Areas.ALN.Enums;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.DataAnnotations;
using System;
using System.Collections.Generic;

namespace SMC.SGA.Administrativo.Areas.ALN.Models
{
    public class TermoIntercambioCabecalhoViewModel : SMCViewModelBase
    {
        [SMCHidden]
        [SMCKey]
        public long Seq { get; set; }

        [SMCHidden]
        public long SeqParceriaIntercambio { get; set; }

        public string Parceria { get; set; }

        public TipoParceriaIntercambio TipoParceriaIntercambio { get; set; }

        public List<ParceriaIntercambioTipoTermoViewModel> TiposTermo { get; set; }

        public bool Ativo { get; set; }
        public DateTime DataInicio { get; set; }

        public DateTime? DataFim { get; set; }

        public List<string> InstituicoesExternas { get; set; }

        public bool ProcessoNegociacao { get; set; }

        public List<ParceriaIntercambioVigenciaViewModel> Vigencias { get; set; }
    }
}