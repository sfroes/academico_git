using SMC.Framework.Mapper;
using SMC.Framework.Model;
using System.Collections.Generic;

namespace SMC.Academico.Domain.Areas.MAT.ValueObjects
{
    public class SolicitacaoMatriculaItemDivisoesVO : ISMCMappable
    {
        public long? Seq { get; set; }

        public List<SMCDatasourceItem> DivisoesTurmas { get; set; }

        public long? SeqDivisaoTurma { get; set; }

        public string DivisaoTurmaDescricao { get; set; }

        public long? SeqConfiguracaoComponente { get; set; }
    }
}
