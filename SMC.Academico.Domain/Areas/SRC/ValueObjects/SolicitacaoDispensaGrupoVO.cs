using SMC.Academico.Common.Areas.CUR.Enums;
using SMC.Framework.Mapper;
using System.Collections.Generic;

namespace SMC.Academico.Domain.Areas.SRC.ValueObjects
{
    public class SolicitacaoDispensaGrupoVO : ISMCMappable
    {
        public long Seq { get; set; }

        public long? SeqDispensa { get; set; }

        public long SeqSolicitacaoDispensa { get; set; }

        public ModoExibicaoHistoricoEscolar ModoExibicaoHistoricoEscolar { get; set; }

        public List<SolicitacaoDispensaGrupoOrigemInternaVO> OrigensInternas { get; set; }

        public List<SolicitacaoDispensaGrupoOrigemExternaVO> OrigensExternas { get; set; }

        public List<SolicitacaoDispensaGrupoDestinoVO> Destinos { get; set; }
    }
}