using SMC.Framework.Mapper;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.DataAnnotations;

namespace SMC.SGA.Administrativo.Areas.PES.Models
{
    public class ConsultaConfiguracaoAvaliacaoPpaTurmaListarViewModel : SMCViewModelBase, ISMCMappable
    {
        [SMCHidden]
        public long SeqConfiguracaoAvaliacaoPpaTurma { get; set; }

        [SMCHidden]
        public long SeqConfiguracaoAvaliacaoPpa { get; set; }

        [SMCHidden]
        public long SeqTurma { get; set; }

        public string Turma { get; set; }

        public string DescricaoTurma { get; set; }

        public int? CodigoInstrumento { get; set; }
    }
}