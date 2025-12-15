using SMC.Academico.ServiceContract.Areas.SRC.Data;
using SMC.Framework.Mapper;
using System;
using System.Collections.Generic;

namespace SMC.Academico.ServiceContract.Areas.MAT.Data
{
    public class SolicitacaoMatriculaData : ISMCMappable
    {
        public long Seq { get; set; }

        public int? SeqCondicaoPagamentoGra { get; set; }

        [SMCMapProperty("ConfiguracaoProcesso.SeqProcesso")]
        public long SeqProcesso { get; set; }

        public long SeqPessoaAtuacao { get; set; }

        public string NumeroProtocolo { get; set; }

        public DateTime DataSolicitacao { get; set; }

        public string Descricao { get; set; }

        public string CodigoAdesao { get; set; }

        public DateTime? DataAdesao { get; set; }

        public DateTime? DataPrevistaSolucao { get; set; }

        public DateTime? DataSolucao { get; set; }

        public long? SeqJustificativaSolicitacaoServico { get; set; }

        public List<SolicitacaoServicoEtapaData> Etapas { get; set; }

        public long SeqEntidadeResponsavel { get; set; }

        [SMCMapProperty("ConfiguracaoProcesso.Processo.Servico.Token")]
        public string TokenServico { get; set; }

    }
}