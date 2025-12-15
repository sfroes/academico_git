using SMC.Framework.UI.Mvc;
using System.Collections.Generic;

namespace SMC.SGA.Administrativo.Areas.SRC.Models
{
    public class RealizarAtendimentoPadraoCabecalhoViewModel : SMCViewModelBase
    {
        public string Protocolo { get; set; }

        public string NomeSolicitante { get; set; }

        public string NomeSocialSolicitante { get; set; }

        public string DescricaoProcesso { get; set; }

        public string CriadoPor { get; set; }

        public List<string> FormacoesEspecificas { get; set; }

        public string DescricaoVinculo { get; set; }

        public string DadosVinculo { get; set; }

        public long SeqPessoaAtuacao { get; set; }

        public string Nome
        {
            get
            {
                if (string.IsNullOrWhiteSpace(NomeSocialSolicitante))
                    return $"{NomeSolicitante}";
                else
                    return $"{NomeSocialSolicitante} ({NomeSolicitante})";
            }
        }
    }
}