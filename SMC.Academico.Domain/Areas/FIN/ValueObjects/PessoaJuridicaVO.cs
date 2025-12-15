using SMC.Framework.Mapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMC.Academico.Domain.Areas.FIN.ValueObjects
{
    public class PessoaJuridicaVO : ISMCMappable
    {
        public long Seq { get; set; }

        public string RazaoSocial { get; set; }

        public string Cnpj { get; set; }

        public string NomeFantasia { get; set; }
    }
}
