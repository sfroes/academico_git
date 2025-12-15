using SMC.Framework.Mapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMC.Academico.ServiceContract.Areas.ALN.Data
{
    public class InstituicaoNivelTipoVinculoAlunoListarData : ISMCMappable
    {
        public long Seq { get; set; }

        [SMCMapProperty("InstituicaoNivel.NivelEnsino.Descricao")]
        public string NivelEnsino { get; set; }

        [SMCMapProperty("TipoVinculoAluno.Descricao")]
        public string Vinculo { get; set; }

        public List<InstituicaoNivelTipoOrientacaoData> TiposOrientacao
        {
            get; set;
        }
    }
}
