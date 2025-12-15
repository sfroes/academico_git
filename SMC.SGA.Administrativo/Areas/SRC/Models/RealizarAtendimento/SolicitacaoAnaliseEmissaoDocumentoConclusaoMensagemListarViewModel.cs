using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.DataAnnotations;
using System;

namespace SMC.SGA.Administrativo.Areas.SRC.Models
{
    public class SolicitacaoAnaliseEmissaoDocumentoConclusaoMensagemListarViewModel : SMCViewModelBase
    {
        [SMCKey]
        [SMCHidden]
        public long Seq { get; set; }

        [SMCHidden]
        public long SeqTipoMensagem { get; set; }

        [SMCCssClass("smc-size-md-8 smc-size-xs-8 smc-size-sm-8 smc-size-lg-8")]
        public string DescricaoTipoMensagem { get; set; }

        [SMCHidden]
        public string Descricao { get; set; }

        [SMCCssClass("smc-size-md-16 smc-size-xs-16 smc-size-sm-16 smc-size-lg-16")]
        public string DescricaoDecode { get; set; }

        [SMCHidden]
        public DateTime DataInicioVigencia { get; set; }

        [SMCHidden]
        public long SeqSolicitacaoServico { get; set; }

        [SMCHidden]
        public bool ExisteDocumentoConclusao { get; set; }

        [SMCHidden]
        public string TokenTipoDocumentoAcademico { get; set; }

        [SMCHidden]
        public long? SeqTipoDocumentoSolicitado { get; set; }

        [SMCHidden]
        public int? NumeroVia { get; set; }

        [SMCHidden]
        public long SeqPessoaAtuacao { get; set; }

        [SMCHidden]
        public long SeqInstituicaoEnsino { get; set; }

        [SMCHidden]
        public long SeqInstituicaoNivel { get; set; }

        [SMCHidden]
        public bool? ReutilizarDados { get; set; }

        [SMCHidden]
        public string NomePais { get; set; }

        [SMCHidden]
        public string DescricaoViaAnterior { get; set; }

        [SMCHidden]
        public string DescricaoViaAtual { get; set; }

        [SMCHidden]
        public int? CodigoUnidadeSeo { get; set; }

        [SMCHidden]
        public string DescricaoGrauAcademico { get; set; }

        [SMCHidden]
        public bool ExibirNomeSocial { get; set; }

        [SMCHidden]
        public string NomeAluno { get; set; }

        [SMCHidden]
        public string DocumentoAcademico { get; set; }

        #region Habilitar Botoes

        [SMCHidden]
        public bool HabilitarBotaoEditar { get; set; }

        [SMCHidden]
        public bool HabilitarBotaoExcluir { get; set; }

        #endregion
    }
}