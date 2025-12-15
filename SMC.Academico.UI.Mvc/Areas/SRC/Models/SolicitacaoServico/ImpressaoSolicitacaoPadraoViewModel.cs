using SMC.Framework.Model;
using SMC.Framework.UI.Mvc.DataAnnotations;
using System.Collections.Generic;

namespace SMC.Academico.UI.Mvc.Areas.SRC.Models.SolicitacaoServico
{
    public class ImpressaoSolicitacaoPadraoViewModel
    {
        #region DataSources

        [SMCDataSource]
        public List<SMCDatasourceItem> JustificativasSolicitacao { get; set; }

        #endregion DataSources

        public List<ImpressaoSolicitacaoPadraoDocumentoViewModel> Documentos { get; set; }

        public string DescricaoJustificativa { get; set; }

        public string DescricaoOriginal { get; set; }

        public string DescricaoAtualizada { get; set; }

        public string SituacaoAtualSolicitacao { get; set; }

        public List<KeyValuePair<long, string>> NomesFormularios { get; set; }

        public bool ExigeFormulario { get; set; }

        public List<FormularioPadraoDadoFormularioViewModel> DadoFormulario { get; set; }

        [SMCSelect(nameof(JustificativasSolicitacao))]
        public long? SeqJustificativa { get; set; }

        public bool ExigeJustificativa { get; set; }

        public string ObservacoesJustificativa { get; set; }

        public string DescricaoEtapa { get; set; }

        public string DescricaoProcessoImprimir { get; set; }

        public string Protocolo { get; set; }

        public byte[] LogoInstituicao { get; set; }

        public string NomeInstituicao { get; set; }

        public string NomeSolicitante { get; set; }

        public long? RASolicitante { get; set; }
    }
}