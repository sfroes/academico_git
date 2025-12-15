using SMC.Academico.Common.Areas.CUR.Enums;
using SMC.Framework;
using SMC.Framework.Mapper;
using System.Collections.Generic;

namespace SMC.Academico.Domain.Areas.CUR.ValueObjects
{
    public class DispensaVO : ISMCMappable, ISMCSeq
    {
        public long Seq { get; set; }

        public long SeqGrupoOrigem { get; set; }

        public long SeqGrupoDispensado { get; set; }

        public List<DispensaComponenteVO> GrupoOrigens { get; set; }

        public List<DispensaComponenteVO> GrupoDispensados { get; set; }

        public List<DispensaHistoricoVigenciaVO> HistoricosVigencia { get; set; }

        public List<DispensaMatrizExcecaoVO> MatrizesExcecao { get; set; }

        public MatrizExcecaoDispensa Associado { get; set; }

        public ModoExibicaoHistoricoEscolar ModoExibicaoHistoricoEscolar { get; set; }
    }
}