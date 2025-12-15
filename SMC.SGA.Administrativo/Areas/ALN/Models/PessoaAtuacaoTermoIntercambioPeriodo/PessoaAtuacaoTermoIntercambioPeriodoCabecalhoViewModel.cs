using SMC.Academico.Common.Areas.ALN.Enums;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.DataAnnotations;

namespace SMC.SGA.Administrativo.Areas.ALN.Models
{
    public class PessoaAtuacaoTermoIntercambioPeriodoCabecalhoViewModel : SMCViewModelBase
    {
        [SMCKey]
        [SMCHidden]
        public long Seq { get; set; }

        [SMCHidden]
        public long SeqPessoaAtuacao { get; set; }

        [SMCHidden]
        public long SeqTermoIntercambio { get; set; }

        [SMCDisplay]
        [SMCMaxLength(100)]
        public string Nome { get; set; }

        [SMCHidden]
        public string NomeSocial { get; set; }

        [SMCDisplay]
        [SMCMaxLength(100)]
        public string TipoAtuacao { get; set; }

        [SMCDisplay]
        [SMCMaxLength(100)]
        public string TipoVinculoAlunoDescricao { get; set; }

        [SMCDisplay]
        [SMCMaxLength(100)]
        public string NomeEntidadeResponsavel { get; set; }

        [SMCDisplay]
        [SMCMaxLength(100)]
        public string DescricaoNivelEnsino { get; set; }

        [SMCDisplay]
        [SMCMaxLength(100)]
        public string DescricaoTermoIntercambio { get; set; }

        [SMCDisplay]
        [SMCMaxLength(100)]
        public string DescricaoTipoTermo { get; set; }

        [SMCDisplay]
        [SMCMaxLength(100)]
        public string InstituicaoExternaNome { get; set; }

        [SMCDisplay]
        [SMCMaxLength(100)]
        public TipoMobilidade TipoMobilidade { get; set; }
    }
}