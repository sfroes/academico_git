using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.DataAnnotations;

namespace SMC.SGA.Administrativo.Areas.SRC.Models
{
    public class SolicitacaoAnaliseEmissaoDocumentoConclusaoMensagemFiltroViewModel : SMCViewModelBase
    {
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
        public long? CodigoAlunoMigracao { get; set; }

        [SMCHidden]
        public string TokenNivelEnsino { get; set; }

        [SMCHidden]
        public string DocumentoAcademico { get; set; }
    }
}