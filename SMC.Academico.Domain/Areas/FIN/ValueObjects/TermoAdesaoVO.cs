using SMC.Framework.Mapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMC.Academico.Domain.Areas.FIN.ValueObjects
{
    public class TermoAdesaoVO : ISMCMappable
    {
        public long Seq { get; set; }

        public long SeqContrato { get; set; }

        public string Titulo { get; set; }

        public string Descricao { get; set; }

        public long SeqServico { get; set; }

        public long SeqTipoVinculoAluno { get; set; }

        public bool? Ativo { get; set; }
    }
}
