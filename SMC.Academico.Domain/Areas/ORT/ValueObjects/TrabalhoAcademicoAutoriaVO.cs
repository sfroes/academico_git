using SMC.Academico.Domain.Areas.ALN.Models;
using SMC.Framework.Mapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMC.Academico.Domain.Areas.ORT.ValueObjects
{
    public class TrabalhoAcademicoAutoriaVO : ISMCMappable
    {
        [SMCMapProperty("Seq")]
        public long SeqTrabalhoAcademicoAutoria { get; set; }

        public long SeqAluno { get; set; }

        public string NomeAutor { get; set; }

        public string NomeAutorFormatado { get; set; }

        public string EmailAutor { get; set; }
    }
}
