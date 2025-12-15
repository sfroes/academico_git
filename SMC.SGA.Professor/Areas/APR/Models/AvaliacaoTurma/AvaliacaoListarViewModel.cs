using SMC.Framework.Mapper;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.DataAnnotations;
using System.Collections.Generic;
using System.Linq;

namespace SMC.SGA.Professor.Areas.APR.Models
{
    public class AvaliacaoListarViewModel : SMCViewModelBase, ISMCMappable
    {
        public long Seq { get; set; }

        public string Sigla
        {
            get
            {
                return AplicacoesAvaliacao.FirstOrDefault().Sigla;
            }
        }

        public string Descricao { get; set; }

        public string Valor { get; set; }

        [SMCDecimalDigits(2)]
        [SMCValueEmpty("-")]
        public decimal? Media { get; set; }

        public int TotalNotasLancadas { get; set; }

        public string Data
        {
            get
            {
                string hifen = AplicacoesAvaliacao.FirstOrDefault().DataFimAplicacaoAvaliacao.HasValue ? "-" : "";

                return $"{AplicacoesAvaliacao.FirstOrDefault().DataInicioAplicacaoAvaliacao} {hifen} {AplicacoesAvaliacao.FirstOrDefault().DataFimAplicacaoAvaliacao}";
            }
        }

        public bool DiarioFechado { get; set; }

        /// <summary>
        /// Inverte valor para atender regra ou seja o Diario estiver fechado desabilita os botões
        /// </summary>
        public bool HabilitarBotoes { get { return DiarioFechado ? false : true; } }

        public string MensagemDiarioFechado { get { return DiarioFechado ? "Mensagem_Diario_Fechado" : ""; } }

        public List<AplicacaoAvaliacaoViewModel> AplicacoesAvaliacao { get; set; }
    }
}