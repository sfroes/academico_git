using SMC.Academico.Common.Areas.ORG.Enums;
using SMC.Framework.Mapper;

namespace SMC.Academico.Domain.Areas.CSO.ValueObjects
{
    public class EntidadeIdiomaVO: ISMCMappable
    {
        public long Seq { get; set; }

        public long SeqEntidade { get; set; }

        public Idioma Idioma { get; set; }

        public CampoIdioma CampoIdioma { get; set; }

        public string ValorCampo { get; set; }
    }
}
