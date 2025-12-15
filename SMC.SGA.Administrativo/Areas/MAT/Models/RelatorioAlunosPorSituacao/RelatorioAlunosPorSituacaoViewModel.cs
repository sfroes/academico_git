using SMC.Framework.Mapper;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.DataAnnotations;

namespace SMC.SGA.Administrativo.Areas.MAT.Models
{
    public class RelatorioAlunosPorSituacaoViewModel : SMCViewModelBase, ISMCMappable
    {
        [SMCKey]
        public long Seq { get; set; }

        public long SeqPessoaAtuacao { get; set; }

        public string Nome { get; set; }

        public string DescricaoTipoAtuacao { get; set; }

        public string DescricaoTipoSituacaoMatricula { get; set; }

        public string DescricaoSituacaoMatricula { get; set; }

        public string DescricaoVinculo { get; set; }

        public string DescricaoCursoOferta { get; set; }
    }
}