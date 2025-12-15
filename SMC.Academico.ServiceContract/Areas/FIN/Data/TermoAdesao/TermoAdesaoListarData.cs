using SMC.Framework.Mapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMC.Academico.ServiceContract.Areas.FIN.Data
{
    public class TermoAdesaoListarData : ISMCMappable
    {
        public long Seq { get; set; }

        public string ServicoDescricao { get; set; }

        public string TipoVinculoAlunoDescricao { get; set; }

        public string Titulo { get; set; }

        public string Ativo { get; set; }
    }
}
