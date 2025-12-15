using SMC.Framework.Mapper;
using System;

namespace SMC.Academico.ServiceContract.Areas.ALN.Data
{
    public class PessoaAtuacaoTermoIntercambioPeriodoData : ISMCMappable
    {
        public long Seq { get; set; }

        public long SeqPessoaAtuacao { get; set; }

        public long SeqTermoIntercambio { get; set; }

        public long SeqCicloLetivo { get; set; }
        
        public long? SeqCursoOfertaLocalidadeTurno { get; set; }
        
        public string Nome { get; set; }

        public string NomeSocial { get; set; }

        public string TipoAtuacao { get; set; }

        public string TipoVinculoAlunoDescricao { get; set; }

        public string NomeEntidadeResponsavel { get; set; }

        public string DescricaoNivelEnsino { get; set; }

        public string DescricaoTermoIntercambio { get; set; }
        
        public string DescricaoTipoTermo { get; set; }

        public string InstituicaoExternaNome { get; set; }

        public string TipoMobilidade { get; set; }

        public DateTime DataInicio { get; set; }

        public DateTime DataFim { get; set; }

        public long SeqParceriaIntercambio { get; set; }

        public long SeqAluno { get; set; }

        public bool PermiteEdicao { get; set; }

        public long SeqPessoaAtuacaoTermoIntercambio { get; set; }
    }
}