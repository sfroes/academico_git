using SMC.Framework.Mapper;
using SMC.Framework.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMC.Academico.Domain.Areas.CAM.ValueObjects
{
    public class ProcessoSeletivoOfertaIntegracaoGpiVO : ISMCMappable
    {

        public long SeqCampanhaOferta { get; set; }

        public long SeqProcessoSeletivoOferta { get; set; }
        
        public long? SeqHierarquiaOfertaGpi { get; set; }
        
        public short QuantidadeVagas { get; set; }
        
        public TipoOfertaIntegracaoGpiVO TipoOferta { get; set; }

        public List<CampanhaOfertaItemIntegracaoGpiVO> Itens { get; set; }
    }
}
