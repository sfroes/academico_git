using SMC.Framework.UI.Mvc;
using SMC.Framework.Util;
using SMC.Financeiro.Common.Areas.GRA.Enums;
using System;
using SMC.Framework.UI.Mvc.DataAnnotations;
using SMC.SGA.Aluno.Areas.FIN.Views.TermoConcessaoBolsa.App_LocalResources;
using SMC.Framework;
using SMC.Academico.Common.Areas.FIN.Enums;

namespace SMC.SGA.Aluno.Areas.FIN.Models
{
    public class TermoConcessaoBolsaListaViewModel : SMCViewModelBase
    {
        public Guid? CodigoAutenticacaoAdesao { get; set; }

        public DateTime? DataAdesaoTermo { get; set; }

        public DateTime DataFimValidade { get; set; }

        public DateTime DataInicioValidade { get; set; }

        [SMCValueEmpty("-")]
        public decimal? PercentualContrato { get; set; }

        public byte TipoBolsaEstudo { get; set; }

        public TermoConcessaoBolsa Situacao
        {
            get
            {
                if(!CodigoAutenticacaoAdesao.HasValue || !DataAdesaoTermo.HasValue)
                {
                    EhPendente = true;
                    return TermoConcessaoBolsa.Pendente;

                }
                else
                {
                    return TermoConcessaoBolsa.Aderido;
                }
            }
        }

        public bool EhPendente { get; set; }

        public int SeqTermoConcessaoBolsa { get; set; }

        public string DescricaoTipoLancamento { get; set; }

        [SMCSize(SMCSize.Grid24_24)]
        [SMCRequired]
        public bool TermoAceite { get; set; }
    }
}