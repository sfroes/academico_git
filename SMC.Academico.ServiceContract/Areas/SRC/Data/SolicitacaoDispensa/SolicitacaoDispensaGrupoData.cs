using SMC.Academico.Common.Areas.CUR.Enums;
using SMC.Framework.Mapper;
using SMC.Framework.Model;
using System.Collections.Generic;

namespace SMC.Academico.ServiceContract.Areas.SRC.Data
{
    public class SolicitacaoDispensaGrupoData : ISMCMappable
    {
        public long Seq { get; set; }

        public long SeqSolicitacaoDispensa { get; set; }

        public long? SeqDispensa { get; set; }

        public ModoExibicaoHistoricoEscolar ModoExibicaoHistoricoEscolar { get; set; }

        public List<SolicitacaoDispensaGrupoDestinoData> Destinos { get; set; }

        public List<SolicitacaoDispensaGrupoOrigemData> OrigensExternas { get; set; }

        public List<SolicitacaoDispensaGrupoOrigemData> OrigensInternas { get; set; }
    }
}