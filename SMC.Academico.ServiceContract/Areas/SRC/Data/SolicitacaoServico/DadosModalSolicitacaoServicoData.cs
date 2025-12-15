using SMC.Academico.Common.Areas.SRC.Enums;
using SMC.DadosMestres.Common.Areas.PES.Enums;
using SMC.Framework.Mapper;
using System;
using System.Collections.Generic;

namespace SMC.Academico.ServiceContract.Areas.SRC.Data
{
    public class DadosModalSolicitacaoServicoData : ISMCMappable
    {
        public long SeqSolicitacaoServico { get; set; }

        public long SeqPessoaAtuacao { get; set; }

        public DadosModalSolicitacaoData Solicitacao { get; set; }

        public DadosModalSolicitacaoAtualData SolicitacaoAtual { get; set; }

        public DadosModalSolicitacaoOriginalData SolicitacaoOriginal { get; set; }

        public DadosModalSolicitacaoAtualizadaData SolicitacaoAtualizada { get; set; }

        public List<DadosModalSolicitacaoDocumentoData> DocumentosSolicitacao { get; set; }

        public List<DadosModalSolicitacaoHistoricoData> HistoricosSolicitacao { get; set; }

        public bool ExibirComprovanteMatriculaETermoAdesao { get; set; }

        public bool SolicitacaoPossuiCodigoAdesao { get; set; }

        public bool SolicitacaoComMatriculaEfetivada { get; set; }

        public bool SolicitacaoPossuiTaxas { get; set; }

        public bool ExibirAbaDocumentos { get; set; }
    }
}
