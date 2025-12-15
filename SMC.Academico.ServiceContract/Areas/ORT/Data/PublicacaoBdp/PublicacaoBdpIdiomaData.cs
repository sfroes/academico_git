using SMC.Framework;
using SMC.Framework.Mapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMC.Academico.ServiceContract.Areas.ORT.Data
{
    public class PublicacaoBdpIdiomaData : ISMCMappable
    {
        public long Seq { get; set; }

        public SMCLanguage Idioma { get; set; }

        public string Titulo { get; set; }

        public string Resumo { get; set; }

        public bool IdiomaTrabalho { get; set; }

        public List<PublicacaoBdpPalavraChaveData> PalavrasChave { get; set; }
    }
}
