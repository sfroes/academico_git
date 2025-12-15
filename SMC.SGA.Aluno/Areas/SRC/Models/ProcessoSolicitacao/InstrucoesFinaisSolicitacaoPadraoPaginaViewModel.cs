using SMC.Academico.Common.Areas.SRC.Constants;
using SMC.Academico.UI.Mvc.Areas.SRC.Models;
using SMC.Formularios.Common.Areas.FRM.Enums;
using SMC.Formularios.UI.Mvc.Attributes;
using SMC.Framework;
using SMC.Framework.Model;
using SMC.Framework.UI.Mvc.DataAnnotations;
using System.Collections.Generic;

namespace SMC.SGA.Aluno.Areas.SRC.Models
{
    public class InstrucoesFinaisSolicitacaoPadraoPaginaViewModel : SolicitacaoServicoPaginaViewModelBase
    {
        #region DataSources

        [SMCDataSource]
        public List<SMCDatasourceItem> JustificativasSolicitacao { get; set; }

        #endregion DataSources

        [SMCHidden]
        public override string Token => TOKEN_SOLICITACAO_SERVICO.SOLICITACAO_PADRAO_SOLICITACAO_INSTRUCOES_FINAIS;

        public override string ChaveTextoBotaoProximo => "Botao_Sairprocesso";

        [SMCSize(SMCSize.Grid24_24)]
        public string DescricaoOriginal { get; set; }

        [SMCSize(SMCSize.Grid24_24)]
        public string DescricaoAtualizada { get; set; }

        [SMCSize(SMCSize.Grid10_24)]
        public string SituacaoAtualSolicitacao { get; set; }

        [SMCSize(SMCSize.Grid14_24)]
        public string ObservacaoSituacaoAtual { get; set; }

        public bool ExigeFormulario { get; set; }

        [SMCSGF(RenderMode = FormaExibicaoSecao.Nenhum)]
        [SMCSize(Framework.SMCSize.Grid24_24)]
        public List<FormularioPadraoDadoFormularioViewModel> DadoFormulario { get; set; }

        public List<KeyValuePair<long, string>> NomesFormularios { get; set; }

        public bool ExigeJustificativa { get; set; }

        [SMCSelect(nameof(JustificativasSolicitacao))]
        [SMCSize(SMCSize.Grid24_24)]
        public long? SeqJustificativa { get; set; }

        [SMCSize(SMCSize.Grid24_24)]
        public string DescricaoJustificativa { get; set; }

        [SMCSize(SMCSize.Grid24_24)]
        public string ObservacoesJustificativa { get; set; }

        public List<DocumentosSolicitacaoViewModel> Documentos { get; set; }

        public List<TaxasSolicitacaoViewModel> Taxas { get; set; }

        public override string Protocolo { get; set; }

        public override string DescricaoEtapa { get; set; }

        public string DescricaoProcessoImprimir { get; set; }

    }
}