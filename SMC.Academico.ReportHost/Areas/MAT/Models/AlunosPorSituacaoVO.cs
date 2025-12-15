using SMC.Framework.Mapper;

namespace SMC.Academico.ReportHost.Areas.MAT.Models
{
    public class AlunosPorSituacaoVO : ISMCMappable
    {
        public long Seq { get; set; }

        public long SeqPessoaAtuacao { get; set; }

        public string Nome { get; set; }

        public string DescricaoTipoAtuacao { get; set; }

        public string DescricaoTipoSituacaoMatricula { get; set; }

        public string DescricaoSituacaoMatricula { get; set; }

        public string DescricaoVinculo { get; set; }

        public string DescricaoCursoOferta { get; set; }

        public long SeqEntidadeResponsavel { get; set; }

        public string NomeEntidadeResponsavel { get; set; }
    }
}