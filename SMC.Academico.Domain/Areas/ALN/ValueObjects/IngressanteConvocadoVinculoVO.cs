using SMC.Framework.Mapper;
using System.Collections.Generic;

namespace SMC.Academico.Domain.Areas.ALN.ValueObjects
{
    public class IngressanteConvocadoVinculoVO : ISMCMappable
    {
        public long Seq { get; set; }

        public long SeqPessoaAtuacao { get; set; }

        public long SeqConvocado { get; set; }

        public bool ExigeCurso { get; set; }

        public bool ExigeOfertaMatrizCurricular { get; set; }

        public string Nome { get; set; }

        public string NomeSocial { get; set; }

        public List<long> SeqsTipoFormacaoEspecifica { get; set; }
    }
}
