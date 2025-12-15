using SMC.Framework.Mapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMC.Academico.Domain.Areas.FIN.ValueObjects
{
    public class TermoAdesaoListarVO : ISMCMappable
    {
        public long Seq { get; set; }

        [SMCMapProperty("Servico.Descricao")]
        public string ServicoDescricao { get; set; }

        [SMCMapProperty("TipoVinculoAluno.Descricao")]
        public string TipoVinculoAlunoDescricao { get; set; }

        public string Titulo { get; set; }

        public string Ativo { get; set; }
    }
}
