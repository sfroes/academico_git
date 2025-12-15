using SMC.Framework.Mapper;
using SMC.Framework.Model;
using System.Collections.Generic;

namespace SMC.Academico.Domain.Areas.MAT.ValueObjects
{
    public class AtendimentoReaberturaVO : ISMCMappable
    {
        public List<SMCDatasourceItem> Processos { get; set; }

        public bool? GrupoEscalonamentoAtivo { get; set; }

        public long? SeqProcessoEscalonamentoReabertura { get; set; }

        public long? SeqGrupoEscalonamentoMatricula { get; set; }
    }
}