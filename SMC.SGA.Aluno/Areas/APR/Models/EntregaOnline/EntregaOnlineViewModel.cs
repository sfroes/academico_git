using SMC.Academico.Common.Areas.APR.Enums;
using SMC.Framework;
using SMC.Framework.DataAnnotations;
using SMC.Framework.Mapper;
using SMC.Framework.Model;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.DataAnnotations;
using SMC.Framework.UI.Mvc.Html;
using System;
using System.Collections.Generic;

namespace SMC.SGA.Aluno.Areas.APR.Models.EntregaOnline
{
    public class EntregaOnlineViewModel : SMCViewModelBase
    {
        #region DataSource

        public List<SMCDatasourceItem> Alunos { get; set; }

        #endregion

        [SMCHidden]
        public long Seq { get; set; }

        [SMCHidden]
        public long SeqAplicacaoAvaliacao { get; set; }

        [SMCHidden]
        public long SeqOrigemAvaliacao { get; set; }

        #region Cabecalho

        public string DescricaoOrigemAvaliacao { get; set; }

        public string Sigla { get; set; }

        public string Descricao { get; set; }

        public double Valor { get; set; }

        public DateTime DataInicio { get; set; }

        public DateTime? DataFim { get; set; }

        public short? QuantidadeMaximaPessoasGrupo { get; set; }

        [SMCHideLabel]
        public string Instrucao { get; set; }

        public long? SeqArquivoAnexadoInstrucao { get; set; }

        public Guid? UidArquivoAnexadoInstrucao { get; set; }

        public string Data
        {
            get
            {
                string retorno;
                retorno = $"{DataInicio.ToString()} {(DataFim.HasValue ? $"- {DataFim.Value.ToString()}" : "")}";
                return retorno;
            }
        }

        #endregion

        #region Entrega

        [SMCHidden]
        public long SeqArquivoAnexado { get; set; }

        [SMCHidden]
        public long SeqTuma { get; set; }

        [SMCConditionalRequired(nameof(HabilitarCampos), SMCConditionalOperation.Equals, true)]
        [SMCConditionalReadonly(nameof(HabilitarCampos), SMCConditionalOperation.Equals, false, PersistentValue = true)]
        [SMCFile(AreaDownload = "", ActionDownload = "DownloadFileGuid", ControllerDownload = "Home", HideDescription = true)]
        [SMCHidden(SMCViewMode.Filter)]
        [SMCRequired]
        [SMCSize(SMCSize.Grid24_24)]
        public SMCUploadFile ArquivoAnexado { get; set; }

        [SMCConditionalReadonly(nameof(HabilitarCampos), SMCConditionalOperation.Equals, false, PersistentValue = true)]
        [SMCMultiline(Rows = 2)]
        [SMCSize(SMCSize.Grid24_24)]
        public string Observacao { get; set; }

        #endregion

        #region Participantes

        [SMCConditionalReadonly(nameof(HabilitarCampos), SMCConditionalOperation.Equals, false, PersistentValue = true)]
        [SMCDetail(SMCDetailType.Tabular, HideMasterDetailButtons = true)]
        [SMCSize(SMCSize.Grid24_24)]
        public SMCMasterDetailList<EntregaOnlineParticipanteViewModel> Participantes { get; set; } 
        
        #endregion

        #region Detalhes da entrega

        [SMCReadOnly]
        [SMCDateTimeMode(SMCDateTimeMode.DateTime)]
        [SMCSize(SMCSize.Grid6_24, SMCSize.Grid6_24,SMCSize.Grid6_24,SMCSize.Grid6_24)]
        public DateTime? DataEntrega { get; set; }

        [SMCReadOnly]
        [SMCSize(SMCSize.Grid12_24,SMCSize.Grid12_24,SMCSize.Grid12_24,SMCSize.Grid12_24)]
        public Guid? CodigoProtocolo { get; set; }

        [SMCReadOnly]
        [SMCSize(SMCSize.Grid6_24, SMCSize.Grid6_24, SMCSize.Grid6_24, SMCSize.Grid6_24)]
        public string SituacaoEntrega { get; set; }

        #endregion

        #region Controle de Exibição

        [SMCHidden]
        public bool HabilitarCampos { get; set; }

        [SMCHidden]
        public bool HabilitarBotaoNovo { get; set; }

        [SMCHidden]
        public bool HabilitarBotaoLiberar { get; set; }

        [SMCHidden]
        public bool HabilitarBotaoNovaEntrega { get; set; }

        [SMCHidden]
        public bool HabilitarBotaoHistorico { get; set; }

        #endregion
    }
}