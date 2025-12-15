using SMC.Framework.Mapper;
using SMC.Framework.UI.Mvc;

namespace SMC.SGA.Administrativo.Areas.MAT.Models
{
    public class RelatorioAlunosPorComponenteListaViewModel : SMCViewModelBase, ISMCMappable
    {
        public int NumeroAgrupador { get; set; }

        public long SeqPessoaAtuacao { get; set; }

        public string NomePessoaAtuacao { get; set; }

        public string DescricaoTipoAtuacao { get; set; }

        public string DescricaoTipoVinculoAluno { get; set; }

        public string NumeroProtocolo { get; set; }

        public string DescricaoCursoOferta { get; set; }


    }
}