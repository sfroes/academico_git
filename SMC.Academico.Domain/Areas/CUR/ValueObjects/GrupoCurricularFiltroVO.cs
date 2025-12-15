using SMC.Framework.Mapper;

namespace SMC.Academico.Domain.Areas.CUR.ValueObjects
{
    public class GrupoCurricularFiltroVO : ISMCMappable
    {
        public long SeqCurriculoCursoOferta { get; set; }

        public bool? DesconsiderarItensQueNaoPermitemCadastroDispensa { get; set; }

        public bool? DesconsiderarItensCursadosAprovacaoOuDispensadosAluno { get; set; }

        public bool? DesconsiderarItensVinculadosAoCurriculoCursoOferta { get; set; }

        public bool? SelecionarComponente { get; set; }
              
        public bool? DesconsiderarGruposTodosItensObrigatorios { get; set; }

        public long? SeqPessoaAtuacao { get; set; }

        public bool FiltrarFormacoesEspecificasAluno { get; set; }
    }
}