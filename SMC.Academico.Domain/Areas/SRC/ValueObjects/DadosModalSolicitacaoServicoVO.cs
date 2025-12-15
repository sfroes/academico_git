using SMC.Academico.Common.Areas.SRC.Enums;
using SMC.Academico.Domain.Areas.SRC.Models;
using SMC.DadosMestres.Common.Areas.PES.Enums;
using SMC.Framework.Mapper;
using System;
using System.Collections.Generic;

namespace SMC.Academico.Domain.Areas.SRC.ValueObjects
{
    public class DadosModalSolicitacaoServicoVO : ISMCMappable
    {
        public long SeqSolicitacaoServico { get; set; }

        public long SeqPessoaAtuacao { get; set; }

        public DadosModalSolicitacaoVO Solicitacao { get; set; }

        public DadosModalSolicitacaoAtualVO SolicitacaoAtual { get; set; }

        public DadosModalSolicitacaoOriginalVO SolicitacaoOriginal { get; set; }

        public DadosModalSolicitacaoAtualizadaVO SolicitacaoAtualizada { get; set; }

        public List<DadosModalSolicitacaoDocumentoVO> DocumentosSolicitacao { get; set; }

        public List<DadosModalSolicitacaoHistoricoVO> HistoricosSolicitacao { get; set; }

        public bool ExibirComprovanteMatriculaETermoAdesao { get; set; }

        public bool SolicitacaoPossuiCodigoAdesao { get; set; }

        public bool SolicitacaoComMatriculaEfetivada { get; set; }

        public bool SolicitacaoPossuiTaxas { get; set; }

        public bool ExibirAbaDocumentos { get; set; }
    }
}
