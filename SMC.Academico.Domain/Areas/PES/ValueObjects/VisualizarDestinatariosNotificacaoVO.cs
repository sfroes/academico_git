using System.Collections.Generic;
using SMC.Academico.Common.Areas.PES.Enums;
using SMC.Framework.Mapper;
using SMC.Framework.Model;

namespace SMC.Academico.Domain.Areas.PES.ValueObjects
{
    public class VisualizarDestinatariosNotificacaoVO : SMCPagerFilterData, ISMCMappable
    {
        public long SeqNotificacao { get; set; }
        public List<long> SeqsDestinatarios { get; set; }
        public TipoAtuacao TipoAtuacao { get; set; }

    }
}
