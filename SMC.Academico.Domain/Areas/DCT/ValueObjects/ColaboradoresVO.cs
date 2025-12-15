using SMC.Framework.Mapper;

namespace SMC.Academico.Domain.Areas.DCT.ValueObjects
{
    public class ColaboradoresVO : ISMCMappable
    {
        public long SeqColaborador { get; set; }

        public long SeqDadosPessoais { get; set; }

        public string Nome { get; set; }

        public string NomeSocial { get; set; }
    }
}
