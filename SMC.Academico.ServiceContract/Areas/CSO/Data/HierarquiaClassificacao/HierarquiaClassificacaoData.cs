using SMC.Framework.Mapper;
using System.Collections.Generic;

namespace SMC.Academico.ServiceContract.Areas.CSO.Data
{
    public class HierarquiaClassificacaoData : ISMCMappable
    {

        public long Seq { get; set; }
        
        public long SeqTipoHierarquiaClassificacao { get; set; }

        public string Descricao { get; set; }

        public List<ClassificacaoData> Classificacoes { get; set; }
    }
}
