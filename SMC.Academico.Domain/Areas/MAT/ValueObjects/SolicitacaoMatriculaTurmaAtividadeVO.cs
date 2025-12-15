using SMC.Framework.Mapper;
using System.Collections.Generic;

namespace SMC.Academico.Domain.Areas.MAT.ValueObjects
{
    public class SolicitacaoMatriculaTurmaAtividadeVO : ISMCMappable
    {
        public long Seq { get; set; }

        public string Etapa { get; set; }

        public string TokenEtapa { get; set; }

        public List<SolicitacaoMatriculaTurmaAtividadeSituacaoVO> Turmas { get; set; }

        public List<SolicitacaoMatriculaTurmaAtividadeSituacaoVO> Atividades { get; set; }
    }
}
