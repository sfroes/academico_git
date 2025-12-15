using SMC.Academico.Common.Areas.ORG.Enums;
using SMC.Framework;
using SMC.Framework.Mapper;

namespace SMC.Academico.ServiceContract.Areas.CSO.Data
{
    public class EntidadeIdiomaData : ISMCSeq, ISMCMappable
    {
        public long Seq { get; set; }

        public long SeqEntidade { get; set; }

        public Idioma Idioma { get; set; }

        public CampoIdioma CampoIdioma { get; set; }

        public string ValorCampo { get; set; }
    }
}
