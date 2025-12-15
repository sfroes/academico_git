using SMC.Framework.Mapper;
using System.Collections.Generic;

namespace SMC.Academico.Domain.Areas.CNC.ValueObjects
{
    public class IntegralizacaoMatrizDivisaoVO : ISMCMappable
    {
        public short NumeroDivisao { get; set; }

        public string DescricaoDivisao { get; set; }

        public List<IntegralizacaoMatrizGrupoVO> Grupos { get; set; }
    }
}
