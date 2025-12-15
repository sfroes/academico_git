using SMC.Academico.Common.Areas.APR.Enums;
using SMC.Framework;
using SMC.Framework.Mapper;
using System;
using System.Collections.Generic;

namespace SMC.Academico.Domain.Areas.GRD.ValueObjects
{
    public class EventoAulaLoteVO : ISMCMappable
    {
        public long Seq { get; set; }
        public string DescricaoDivisaoTurma { get; set; }
        public string DescricaoColaboradores { get; set; }
        public string DataHorario { get; set; }
        public string QtdAlunoFalta { get; set; }
        public string PrazoApuracao { get; set; }
        public string DescricaoNivelEnsino { get; set; }
        public string PrazoAlteracao { get; set; }
        public string CodigoDivisaoTurma { get; set; }
        public bool AlunosHistoricoEscolar { get; set; }
    }
}
