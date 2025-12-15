using SMC.Framework.Mapper;

namespace SMC.Academico.Domain.Areas.ORT.ValueObjects
{
    public class ListaTrabalhoAcademicoVO : ISMCMappable
    {
        public long SeqTrabalhoAcademico { get; set; }

        public long SeqPublicacaoBdp { get; set; }

        public long SeqAtuacaoAluno { get; set; }

        public string NomeEntidadeVinculo { get; set; }

        public string NomeAutor { get; set; }

        public string Titulo { get; set; }

        public string TipoAutorizacao { get; set; }

        public long SeqConfiguracaoNotificacao { get; set; }

    }
}
