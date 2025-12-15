using SMC.Framework.Mapper;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.DataAnnotations;
using SMC.Financeiro.Common.Areas.GRA.Enums;
using System.Linq;
using SMC.Framework.Util;

namespace SMC.SGA.Administrativo.Areas.MAT.Models
{
    public class RelatorioAlunosPorSituacaoListarItemViewModel : SMCViewModelBase, ISMCMappable
    {
        [SMCKey]
        [SMCHidden]
        public long Seq { get; set; }

        public long SeqAtuacao { get; set; }

        [SMCHidden]
        public string Nome { get; set; }

        [SMCHidden]
        public string DescricaoTipoAtuacao { get; set; }

        public string DescricaoTipoSituacaoMatricula { get; set; }

        public string DescricaoSituacaoMatricula { get; set; }

        public string DescricaoVinculo { get; set; }

        public string DescricaoCursoOferta { get; set; }

        public string TextoInformativo { get; set; }
    }
}