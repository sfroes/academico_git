using SMC.Academico.Common.Areas.PES.Enums;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.DataAnnotations;
using System;

namespace SMC.SGA.Administrativo.Areas.SRC.Models
{
    public class AtendimentoParecerAssertViewModel : SMCViewModelBase
    {
        [SMCSize(Framework.SMCSize.Grid24_24)]
        public string DescricaoProcesso { get; set; }

        [SMCSize(Framework.SMCSize.Grid24_24)]
        public string NomeSolicitante
        {
            get
            {
                if (!string.IsNullOrEmpty(NomeSocial))
                    return $"{NomeSocial} ({Nome})";
                else
                    return Nome;
            }
        }

        [SMCHidden]
        public string NomeSocial { get; set; }

        [SMCHidden]
        public string Nome { get; set; }

        [SMCSize(Framework.SMCSize.Grid24_24)]
        public string Protocolo { get; set; }

        [SMCSize(Framework.SMCSize.Grid24_24)]
        public string OrientacoesDeferimento { get; set; }

        [SMCHidden]
        public long SeqSolicitacaoServico { get; set; }

        [SMCHidden]
        public bool ValidarSituacaoFutura { get; set; }

        [SMCHidden]
        public TipoAtuacao TipoAtuacao { get; set; }

        [SMCHidden]
        public DateTime DataSolicitacao { get; set; }

    }
}