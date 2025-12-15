using SMC.Academico.Common.Areas.CNC.Enums;
using SMC.Framework;
using SMC.Framework.DataAnnotations;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.DataAnnotations;
using System;
using System.Collections.Generic;

namespace SMC.SGA.Administrativo.Areas.SRC.Models
{
    public class SolicitacaoAnaliseEmissaoDocumentoConclusaoFormacaoConcluidaViewModel : SMCViewModelBase
    {
        [SMCHidden]
        public long SeqFormacaoEspecifica { get; set; }

        [SMCHidden]
        public SituacaoAlunoFormacao SituacaoAlunoFormacao { get; set; }

        [SMCHidden]
        public DateTime Data { get; set; }

        [SMCHidden]
        public long? SeqTitulacao { get; set; }

        [SMCHidden]
        public int? NumeroVia { get; set; }

        [SMCHidden]
        public DateTime? DataFormacao { get; set; }

        [SMCHidden]
        public string DescricaoFormacaoEspecifica { get; set; }

        [SMCHidden]
        public long NumeroRA { get; set; }

        [SMCHidden]
        [SMCCssClass("smc-size-md-4 smc-size-xs-4 smc-size-sm-4 smc-size-lg-4")]
        public string DescricaoDocumentoConclusao { get; set; }

        public List<string> DescricoesFormacaoEspecifica { get; set; }

        [SMCHidden]
        [SMCValueEmpty("-")]
        public string DescricaoTitulacao { get; set; }

        [SMCDateTimeMode(SMCDateTimeMode.Date)]
        [SMCValueEmpty("-")]
        public DateTime? DataColacaoGrau { get; set; }

        [SMCDateTimeMode(SMCDateTimeMode.Date)]
        [SMCValueEmpty("-")]
        public DateTime? DataConclusao { get; set; }

        [SMCHidden]
        [SMCCssClass("smc-size-md-3 smc-size-xs-3 smc-size-sm-3 smc-size-lg-3")]
        [SMCValueEmpty("-")]
        public string NumeroViaGerada
        {
            get
            {
                return NumeroVia.HasValue && NumeroVia != 0 ? $"{NumeroVia}°" : "-";
            }
        }
    }
}