using SMC.Academico.Common.Areas.APR.Enums;
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
    public class AvaliacaoTrabalhoAcademicoCabecalhoVO : ISMCMappable
    {
        public string DescricaoTipoTrabalho { get; set; }

        public string Titulo { get; set; }

        public List<AutorVO> Autores { get; set; }
    }
}
