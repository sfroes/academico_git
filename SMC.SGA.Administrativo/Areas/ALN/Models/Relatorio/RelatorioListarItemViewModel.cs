using SMC.Framework.Mapper;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.DataAnnotations;
using SMC.Financeiro.Common.Areas.GRA.Enums;
using System.Linq;
using SMC.Framework.Util;

namespace SMC.SGA.Administrativo.Areas.ALN.Models
{
    public class RelatorioListarItemViewModel : SMCViewModelBase, ISMCMappable
    {
        [SMCKey]
        [SMCHidden]
        public long Seq { get; set; }

        public long NumeroRegistroAcademico { get; set; }

        [SMCDescription]
        public string Nome { get; set; }

        [SMCHidden]
        public string Cpf { get; set; }

        [SMCHidden]
        public string NumeroPassaporte { get; set; }

        /// <summary>
        /// Cpf e/ou passaporte
        /// </summary>
        public string Documento => string.Join(" / ", new[] { SMCMask.ApplyMaskCPF(Cpf), NumeroPassaporte }.Where(w => !string.IsNullOrEmpty(w)));

        public string DescricaoSituacaoMatricula { get; set; }

        public TipoVinculoAlunoFinanceiro TipoVinculoAlunoFinanceiro { get; set; }

        public string DescricaoPessoaAtuacao { get; set; }
    }
}