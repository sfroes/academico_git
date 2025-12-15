using SMC.Academico.Common.Enums;
using SMC.Academico.Domain.Areas.ORT.Models;
using SMC.Framework;
using SMC.Framework.Mapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMC.Academico.Domain.Areas.ORT.ValueObjects
{
    public class PublicacaoBdpIdiomaVO : ISMCMappable
    {
        public long Seq { get; set; }

        public Linguagem Idioma { get; set; }

        public string Titulo { get; set; }

        public string Resumo { get; set; }

        public bool IdiomaTrabalho { get; set; }

        public List<PublicacaoBdpPalavraChaveVO> PalavrasChave { get; set; }        
    }
}
