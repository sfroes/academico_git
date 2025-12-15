using SMC.Framework.Mapper;
using System.Collections.Generic;

namespace SMC.Academico.ServiceContract.Areas.MAT.Data
{
    public class SolicitacaoMatriculaTurmaAtividadeData : ISMCMappable
    {
        public long Seq { get; set; }

        public string Etapa { get; set; }

        public List<SolicitacaoMatriculaTurmaAtividadeSituacaoData> Turmas { get; set; }

        public List<SolicitacaoMatriculaTurmaAtividadeSituacaoData> Atividades { get; set; }
    }
}
