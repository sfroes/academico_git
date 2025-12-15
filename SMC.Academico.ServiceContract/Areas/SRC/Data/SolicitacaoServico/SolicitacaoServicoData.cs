using SMC.Academico.Common.Areas.SRC.Enums;
using SMC.DadosMestres.Common.Areas.PES.Enums;
using SMC.Framework.Mapper;
using System;
using System.Collections.Generic;

namespace SMC.Academico.ServiceContract.Areas.SRC.Data
{
    public class SolicitacaoServicoData : ISMCMappable
    {
        public long Seq { get; set; }

        public long SeqPessoaAtuacao { get; set; }

        public string NumeroProtocolo { get; set; }

        public DateTime DataSolicitacao { get; set; }

        public string DescricaoOriginal { get; set; }

        public DateTime? DataPrevistaSolucao { get; set; }

        public DateTime? DataSolucao { get; set; }

        public long? SeqJustificativaSolicitacaoServico { get; set; }

        public SituacaoDocumentacao SituacaoDocumentacao { get; set; }

        public long SeqConfiguracaoProcesso { get; set; }

        public long? SeqGrupoEscalonamento { get; set; }

        public long SeqEntidadeResponsavel { get; set; }

        public long? SeqEntidadeCompartilhada { get; set; }

        public string DescricaoAtualizada { get; set; }

        public string JustificativaComplementar { get; set; }

        public long? SeqAlunoHistorico { get; set; }

        public List<SolicitacaoServicoEtapaData> Etapas { get; set; }

        [SMCMapProperty("ConfiguracaoProcesso.SeqProcesso")]
        public long SeqProcesso { get; set; }

        [SMCMapProperty("ConfiguracaoProcesso.Processo.Descricao")]
        public string DescricaoProcesso { get; set; }

        [SMCMapProperty("ConfiguracaoProcesso.Processo.SeqServico")]
        public long SeqServico { get; set; }

        [SMCMapProperty("ConfiguracaoProcesso.Processo.Servico.Descricao")]
        public string DescricaoServico { get; set; }

        [SMCMapProperty("ConfiguracaoProcesso.Processo.Servico.Token")]
        public string TokenServico { get; set; }

        [SMCMapProperty("ConfiguracaoProcesso.Processo.Servico.TipoServico.Token")]
        public string TokenTipoServico { get; set; }
        
        public string NomeSolicitante { get; set; }

        public string NomeSocial { get; set; }

        public long? RASolicitante { get; set; }
    }
}