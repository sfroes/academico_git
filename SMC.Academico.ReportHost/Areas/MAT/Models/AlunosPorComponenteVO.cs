using SMC.Framework.Mapper;

namespace SMC.Academico.ReportHost.Areas.MAT.Models
{
    public class AlunosPorComponenteVO : ISMCMappable
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