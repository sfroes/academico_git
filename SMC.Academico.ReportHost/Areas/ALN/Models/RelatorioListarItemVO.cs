using SMC.Financeiro.Common.Areas.GRA.Enums;
using SMC.Framework.Mapper;
using SMC.Framework.UI.Mvc;
using SMC.Framework.Util;
using System.Linq;

namespace SMC.Academico.ReportHost.Areas.ALN.Models
{
    public class RelatorioListarItemVO : SMCViewModelBase, ISMCMappable
    {
        public long Seq { get; set; }

        public long NumeroRegistroAcademico { get; set; }

        public string Nome { get; set; }

        public string Cpf { get; set; }

        public string NumeroPassaporte { get; set; }

        public string Documento => string.Join(" / ", new[] { SMCMask.ApplyMaskCPF(Cpf), NumeroPassaporte }.Where(w => !string.IsNullOrEmpty(w)));

        public string DescricaoSituacaoMatricula { get; set; }

        public TipoVinculoAlunoFinanceiro TipoVinculoAlunoFinanceiro { get; set; }

        public string DescricaoPessoaAtuacao { get; set; }

        public string DescricaoCicloLetivo { get; set; }

        public string DescricaoCursoOferta { get; set; }
    }
}