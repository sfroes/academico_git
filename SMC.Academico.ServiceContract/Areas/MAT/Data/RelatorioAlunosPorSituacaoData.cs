using SMC.Framework.Mapper;

namespace SMC.Academico.ServiceContract.Areas.MAT.Data
{
    public class RelatorioAlunosPorSituacaoData : ISMCMappable
    {
        public long SeqPessoaAtuacao { get; set; }

        public string Nome { get; set; }

        public string DescricaoTipoAtuacao { get; set; }

        public string DescricaoTipoSituacaoMatricula { get; set; }

        public string DescricaoSituacaoMatricula { get; set; }

        public string DescricaoVinculo { get; set; }

        public string DescricaoCursoOferta { get; set; }

        public long SeqEntidadeResponsavel { get; set; }

    }
}