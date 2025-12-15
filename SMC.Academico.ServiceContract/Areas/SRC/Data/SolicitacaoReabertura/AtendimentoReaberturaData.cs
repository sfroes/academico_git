using SMC.Framework.Mapper;
using SMC.Framework.Model;
using System.Collections.Generic;

namespace SMC.Academico.ServiceContract.Areas.SRC.Data.SolicitacaoReabertura
{
    public class AtendimentoReaberturaData : ISMCMappable
    {
        public List<SMCDatasourceItem> Processos { get; set; }

        public long SeqSolicitacaoServico { get; set; }

        public bool? GrupoEscalonamentoAtivo { get; set; }

        public long? SeqProcessoEscalonamentoReabertura { get; set; }

        public long? SeqGrupoEscalonamentoMatricula { get; set; }
    }
}