using SMC.Framework;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.DataAnnotations;
using System.Collections.Generic;

namespace SMC.SGA.Administrativo.Areas.MAT.Models
{
    public class ChancelaItemListaViewModel : SMCViewModelBase
    {
        [SMCHidden(SMCViewMode.List)]
        [SMCOrder(0)]
        public long Seq { get; set; }

        [SMCHidden(SMCViewMode.List)]
        [SMCOrder(1)]
        public long SeqProcesso { get; set; }

        [SMCOrder(2)]
        [SMCCssClass("smc-size-md-6 smc-size-xs-6 smc-size-sm-6 smc-size-lg-6")]
        public string DescricaoProcesso { get; set; }

        [SMCOrder(3)]
        [SMCCssClass("smc-size-md-4 smc-size-xs-4 smc-size-sm-4 smc-size-lg-4")]
        public string DescricaoOfertaCurso { get; set; }

        [SMCOrder(4)]
        [SMCCssClass("smc-size-md-2 smc-size-xs-2 smc-size-sm-2 smc-size-lg-2")]
        public string NumeroProtocolo { get; set; }

        [SMCOrder(5)]
        [SMCCssClass("smc-size-md-4 smc-size-xs-4 smc-size-sm-4 smc-size-lg-4")]
        public string NomePessoaAtuacao { get; set; }

        [SMCOrder(6)]
        [SMCCssClass("smc-size-md-3 smc-size-xs-3 smc-size-sm-3 smc-size-lg-3")]
        public string DescricaoSituacao { get; set; }

        [SMCOrder(7)]
        [SMCCssClass("smc-size-md-2 smc-size-xs-2 smc-size-sm-2 smc-size-lg-2")]
        public string DataEscalonamento { get; set; }

        [SMCOrder(8)]
        [SMCValueEmpty(" - ")]
        [SMCCssClass("smc-size-md-3 smc-size-xs-3 smc-size-sm-3 smc-size-lg-3")]
        public List<string> Bloqueios { get; set; }

        [SMCOrder(9)]
        [SMCHidden(SMCViewMode.List)]
        public bool Bloqueado { get; set; }

        [SMCHidden]
        public bool EscalonamentoVigente { get; set; }

        [SMCHidden]
        public bool EtapaChancelaLiberada { get; set; }

        [SMCHidden]
        public bool EtapaPermiteChancela { get; set; }

        [SMCHidden]
        public bool PermiteVisualizarPlanoEstudo { get; set; }

        [SMCHidden]
        public long SeqPessoaAtuacao { get; set; }

        [SMCHidden]
        public long SeqConfiguracaoEtapa { get; set; }

        [SMCHidden]
        public string TokenEtapaSGF { get; set; }

        [SMCHidden]
        public bool Destaque { get; set; }

        [SMCHidden]
        public bool ExibirIntegralizacao { get; set; }

        [SMCHidden]
        public bool SolicitacaoComAtendimentoIniciado { get; set; }

        [SMCHidden]
        public bool UsuarioLogadoEResponsavelAtualPelaSolicitacao { get; set; }
    }
}