using SMC.Academico.Common.Areas.MAT.Enums;
using SMC.Formularios.Common.Areas.TMP.Enums;
using SMC.Framework.Mapper;
using System;

namespace SMC.Academico.ServiceContract.Areas.SRC.Data
{
    public class SolicitacaoHistoricoSituacaoData : ISMCMappable
    {
        public long Seq { get; set; }

        public long SeqSolicitacaoServicoEtapa { get; set; }

        public long SeqSituacaoEtapaSgf { get; set; }

        public CategoriaSituacao CategoriaSituacao { get; set; }

        public DateTime? DataExclusao { get; set; }

        public MotivoSituacaoMatricula? MotivoSituacaoMatricula { get; set; }
    }
}