using SMC.Academico.Common.Areas.SRC.Enums;
using SMC.DadosMestres.Common.Areas.PES.Enums;
using SMC.Framework;
using SMC.Framework.Mapper;
using SMC.Framework.UI.Mvc.DataAnnotations;
using System;
using System.Collections.Generic;

namespace SMC.Academico.UI.Mvc.Areas.SRC.Models
{
    public class DadosModalSolicitacaoServicoViewModel
    {

        [SMCHidden]
        public long SeqSolicitacaoServico { get; set; }
        
        [SMCHidden]
        public long SeqPessoaAtuacao { get; set; }
                        
        public DadosModalSolicitacaoViewModel Solicitacao { get; set; }

        public DadosModalSolicitacaoAtualViewModel SolicitacaoAtual { get; set; }

        public DadosModalSolicitacaoOriginalViewModel SolicitacaoOriginal { get; set; }

        public DadosModalSolicitacaoAtualizadaViewModel SolicitacaoAtualizada { get; set; }

        public List<DadosModalSolicitacaoDocumentoViewModel> DocumentosSolicitacao { get; set; }

        public List<DadosModalSolicitacaoTaxaViewModel> TaxasSolicitacao { get; set; }

        public List<DadosModalSolicitacaoHistoricoViewModel> HistoricosSolicitacao { get; set; }

        [SMCHidden]
        public bool ExibirInformacao { get; set; }

        [SMCHidden]
        public bool ExibirComprovanteMatriculaETermoAdesao { get; set; }

        [SMCHidden]
        public bool SolicitacaoPossuiCodigoAdesao { get; set; }

        [SMCHidden]
        public bool SolicitacaoComMatriculaEfetivada { get; set; }

        [SMCHidden]
        public bool SolicitacaoPossuiTaxas { get; set; }

        [SMCHidden]
        public bool ExibirAbaDocumentos { get; set; }

        [SMCHidden]
        public string BackUrl { get; set; }
    }
}
