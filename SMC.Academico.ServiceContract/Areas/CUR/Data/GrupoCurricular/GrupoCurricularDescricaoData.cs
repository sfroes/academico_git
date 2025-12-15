using SMC.Framework.Mapper;
using System.Collections.Generic;

namespace SMC.Academico.ServiceContract.Areas.CUR.Data
{
    public class GrupoCurricularDescricaoData : ISMCMappable
    {
        public string Descricao { get; set; }

        public string DescricaoFormacaoEspecifica { get; set; }

        public List<string> DescricoesBeneficios { get; set; }

        public List<string> DescricoesCondicoesObrigatoriedade { get; set; }
    }
}