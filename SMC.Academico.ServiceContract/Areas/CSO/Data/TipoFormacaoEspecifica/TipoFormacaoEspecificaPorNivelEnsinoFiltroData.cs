using SMC.Academico.Common.Areas.CSO.Enums;
using SMC.Framework.Mapper;
using SMC.Framework.Model;
using System.Collections.Generic;

namespace SMC.Academico.ServiceContract.Areas.CSO.Data
{
    public class TipoFormacaoEspecificaPorNivelEnsinoFiltroData : SMCPagerFilterData, ISMCMappable
    {
        public bool? Ativo { get; set; }

        public List<long> SeqNivelEnsino { get; set; }

        public long? SeqInstituicaoEnsino { get; set; }

        public TipoCurso? TipoCurso { get; set; }
    }
}