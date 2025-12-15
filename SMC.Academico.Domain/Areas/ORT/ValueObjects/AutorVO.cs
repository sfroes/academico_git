using SMC.Academico.Domain.Areas.ALN.Models;
using SMC.Academico.Domain.Areas.APR.ValueObjects;
using SMC.Framework.Mapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMC.Academico.Domain.Areas.ORT.ValueObjects
{
    public class AutorVO : ISMCMappable
    {
        public string Nome { get; set; }

        public string DescricaoNivelEnsino { get; set; }

        public string DescricaoCurso { get; set; }
    }
}
