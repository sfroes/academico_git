using SMC.Framework;
using SMC.Framework.DataAnnotations;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.DataAnnotations;
using System;

namespace SMC.SGA.Administrativo.Areas.SRC.Models
{
    public class CabecalhoReaberturaSolicitacaoViewModel : SMCViewModelBase
    {
        [SMCHidden]
        public long SeqSolicitacaoServico { get; set; }

        public string Protocolo { get; set; }

        [SMCHidden]
        public string Nome { get; set; }

        [SMCHidden]
        public string NomeSocial { get; set; }

        public string Solicitante
        {
            get
            {
                var solicitante = string.Empty;

                if (!string.IsNullOrEmpty(NomeSocial))
                {
                    if (!string.IsNullOrEmpty(Nome))
                        solicitante += $"{NomeSocial} ({Nome})";
                    else
                        solicitante += $"{NomeSocial}";
                }
                else
                {
                    if (!string.IsNullOrEmpty(Nome))
                        solicitante += $"{Nome}";
                }
                return solicitante;
            }
        }

        public string Processo { get; set; }

        public string EtapaAtual { get; set; }

        public string SituacaoAtual { get; set; }

        [SMCDateTimeMode(SMCDateTimeMode.DateTime)]
        public DateTime? DataHoraEncerramento { get; set; }
    }
}