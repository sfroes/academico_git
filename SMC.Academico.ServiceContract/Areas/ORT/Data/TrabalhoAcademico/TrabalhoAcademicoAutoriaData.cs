using SMC.Academico.Common.Areas.ORT.Enums;
using SMC.Framework;
using SMC.Framework.Mapper;
using SMC.Framework.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMC.Academico.ServiceContract.Areas.ORT.Data
{
    public class TrabalhoAcademicoAutoriaData : ISMCMappable
    {
        public long SeqTrabalhoAcademicoAutoria { get; set; }

        public long SeqAluno { get; set; }

        public string NomeAutor { get; set; }

        public string NomeAutorFormatado { get; set; }

        public string EmailAutor { get; set; }
    }
}
