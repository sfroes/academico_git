using SMC.Framework.Mapper;
using System.Collections.Generic;

namespace SMC.Academico.ServiceContract.Areas.CNC.Data
{
    public class IntegralizacaoMatrizDivisaoData : ISMCMappable
    {
        public short NumeroDivisao { get; set; }

        public string DescricaoDivisao { get; set; }

        public List<IntegralizacaoMatrizGrupoData> Grupos { get; set; }
    }
}
