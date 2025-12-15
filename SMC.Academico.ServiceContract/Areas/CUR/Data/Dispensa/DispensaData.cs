using SMC.Academico.Common.Areas.CUR.Enums;
using SMC.Framework.Mapper;
using System.Collections.Generic;

namespace SMC.Academico.ServiceContract.Areas.CUR.Data
{
    public class DispensaData : ISMCMappable
    {
        public long Seq { get; set; }

        public long SeqGrupoOrigem { get; set; }

        public long SeqGrupoDispensado { get; set; }

        public List<DispensaComponenteData> GrupoOrigens { get; set; }

        public List<DispensaComponenteData> GrupoDispensados { get; set; }

        public List<DispensaHistoricoVigenciaData> HistoricosVigencia { get; set; }

        public List<DispensaMatrizExcecaoData> MatrizesExcecao { get; set; }

        public MatrizExcecaoDispensa Associado { get; set; }

        public ModoExibicaoHistoricoEscolar ModoExibicaoHistoricoEscolar { get; set; }
    }
}