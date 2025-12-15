using SMC.Academico.Common.Constants;
using SMC.DadosMestres.Common.Areas.PES.Enums;
using SMC.Framework;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.DataAnnotations;

namespace SMC.SGA.Administrativo.Models
{
    public class PessoaFiliacaoReadOnlyViewModel : SMCViewModelBase
    {
        [SMCHidden]
        [SMCKey]
        public long Seq { get; set; }

        [SMCHidden]
        public long SeqPessoa { get; set; }

        [SMCConditionalDisplay(nameof(PessoaAtuacaoViewModel.PermitirAlterarDadosPessoaAtuacao), SMCConditionalOperation.Equals, false)]
        [SMCReadOnly]
        [SMCOrder(91)]
        [SMCRequired]
        [SMCSelect(IncludedEnumItems = new object[] { TipoParentesco.Pai, TipoParentesco.Mae })]
        [SMCSize(SMCSize.Grid4_24, SMCSize.Grid24_24, SMCSize.Grid6_24, SMCSize.Grid4_24)]
        [SMCStep(2, 0)]
        public TipoParentesco TipoParentescoReadOnly { get; set; }

        [SMCConditionalDisplay(nameof(PessoaAtuacaoViewModel.PermitirAlterarDadosPessoaAtuacao), SMCConditionalOperation.Equals, false)]
        [SMCReadOnly]
        [SMCMaxLength(100)]
        [SMCOrder(92)]
        [SMCRegularExpression(REGEX.NOME)]
        [SMCRequired]
        [SMCSize(SMCSize.Grid18_24, SMCSize.Grid24_24, SMCSize.Grid14_24, SMCSize.Grid18_24)]
        [SMCStep(2, 0)]
        public string NomeFiliacaoReadOnly { get; set; }
    }
}