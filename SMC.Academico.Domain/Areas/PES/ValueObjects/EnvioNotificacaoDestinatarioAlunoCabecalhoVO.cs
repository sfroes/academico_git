using SMC.Framework.Mapper;

namespace SMC.Academico.Domain.Areas.PES.ValueObjects
{
    public class EnvioNotificacaoDestinatarioAlunoCabecalhoVO : ISMCMappable
    {
        public long SeqPessoaAtuacao { get; set; }

        public long NumeroRegistroAcademico { get; set; }

        public string Nome { get; set; }

        public string DescricaoSituacaoMatricula { get; set; }

        public string DescricaoVinculo { get; set; }
        
        public string NomeEntidadeResponsavel { get; set; }

        public string DescricaoNivelEnsino { get; set; }

    }
}
