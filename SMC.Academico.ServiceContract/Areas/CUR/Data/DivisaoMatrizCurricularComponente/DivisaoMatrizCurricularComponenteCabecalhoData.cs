using SMC.Framework.Mapper;
using System.Collections.Generic;

namespace SMC.Academico.ServiceContract.Areas.CUR.Data
{
    public class DivisaoMatrizCurricularComponenteCabecalhoData : ISMCMappable
    {
        public string CodigoMatriz { get; set; }
        public string DescricaoMatriz { get; set; }
        public string DescricaoMatrizComplementar { get; set; }
        public string CodigoComponente { get; set; }
        public string DescricaoComponente { get; set; }
        public short? CargaHorariaComponente { get; set; }
        public short? CreditosComponente { get; set; }
        public string Formato { get; set; }
        public List<string> GruposPertecentes { get; set; }
    }
}
