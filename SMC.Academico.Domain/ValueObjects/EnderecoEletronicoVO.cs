using SMC.DadosMestres.Common.Areas.PES.Enums;
using SMC.Framework.Mapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMC.Academico.Domain.ValueObjects
{
    public class EnderecoEletronicoVO : ISMCMappable
    {
        public long Seq { get; set; }

        public string Descricao { get; set; }

        public TipoEnderecoEletronico TipoEnderecoEletronico { get; set; }
    }
}
