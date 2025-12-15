using SMC.Academico.Common.Areas.PES.Enums;
using System.Collections.Generic;
using SMC.Framework.Mapper;
using SMC.Framework.Model;

namespace SMC.Academico.ServiceContract.Areas.PES.Data
{
    public class VisualizarDestinatariosNotificacaoData : SMCPagerFilterData, ISMCMappable
    {
        public long SeqNotificacao { get; set; }
        public List<long> SeqsDestinatarios { get; set; }
        public TipoAtuacao TipoAtuacao { get; set; }

    }
}
