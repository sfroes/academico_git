using SMC.Academico.ServiceContract.Areas.CSO.Data;
using SMC.Framework.Mapper;
using System.Collections.Generic;

namespace SMC.Academico.ServiceContract.Areas.ORG.Data
{
    public class EntidadeHierarquiaClassificacaoData : ISMCMappable
    {

        public long Seq { get; set; }
        
        public long? SeqTipoClassificacao { get; set; }

        public string Descricao { get; set; }

        public short? QuantidadeMinima { get; set; }

        public short? QuantidadeMaxima { get; set; }

        public List<ClassificacaoData> Classificacoes { get; set; }
    }
}
