using SMC.Academico.Common.Areas.SRC.Constants;
using SMC.Academico.ServiceContract.Areas.SRC.Data;
using SMC.Academico.ServiceContract.Areas.SRC.Interfaces;
using SMC.Framework;
using SMC.Framework.DataAnnotations;
using SMC.Framework.Extensions;
using SMC.Framework.Security;
using SMC.Framework.UI.Mvc.DataAnnotations;
using SMC.Framework.UI.Mvc.Dynamic;
using SMC.SGA.Administrativo.Areas.SRC.Controllers;
using SMC.SGA.Administrativo.Areas.SRC.Views.Escalonamento.App_LocalResources;
using System;
using System.Collections.Generic;

namespace SMC.SGA.Administrativo.Areas.SRC.Models
{
    public class EscalonamentoDynamicModel : SMCDynamicViewModel
    {
        [SMCKey]
        [SMCReadOnly]
        [SMCSize(SMCSize.Grid4_24, SMCSize.Grid12_24, SMCSize.Grid6_24, SMCSize.Grid6_24)]
        public override long Seq { get; set; }

        [SMCHidden]
        [SMCParameter]
        public long SeqProcesso { get; set; }

        [SMCHidden]
        [SMCParameter]
        public long SeqProcessoEtapa { get; set; }

        [SMCDateTimeMode(SMCDateTimeMode.DateTime)]
        [SMCRequired]
        [SMCSize(SMCSize.Grid6_24, SMCSize.Grid12_24, SMCSize.Grid6_24, SMCSize.Grid6_24)]
        public DateTime DataInicio { get; set; }

        [SMCMinDate(nameof(DataInicio))]
        [SMCMinDateNow]
        [SMCDateTimeMode(SMCDateTimeMode.DateTime)]
        [SMCRequired]
        [SMCSize(SMCSize.Grid6_24, SMCSize.Grid12_24, SMCSize.Grid6_24, SMCSize.Grid6_24)]
        public DateTime DataFim { get; set; }

        [SMCDateTimeMode(SMCDateTimeMode.DateTime)]
        [SMCReadOnly]
        [SMCSize(SMCSize.Grid8_24, SMCSize.Grid12_24, SMCSize.Grid6_24, SMCSize.Grid6_24)]
        public DateTime? DataEncerramento { get; set; }

        [SMCDisplay]
        [SMCHidden(SMCViewMode.Insert)]
        [SMCSize(SMCSize.Grid24_24)]
        public List<string> DescricaoGruposEscalonamento { get; set; }

        [SMCIgnoreProp]
        public string MensagemConfirmacaoAssert { get; set; }

        public override void ConfigureDynamic(ref SMCDynamicOptions options)
        {
            options.EditInModal(allowSaveNew: false, refreshIndexPageOnSubmit: true)
                    ///Assert validado conforme regras usando partialView
                    ///Primeira validação se será exibido a mensagem
                    .Assert("_ConfirmacaoAssertEscalonamento", (controller, model) =>
                    {
                        var modelEscalonamento = (model as EscalonamentoDynamicModel);
                        var controllerEscalonamento = controller.Create<IEscalonamentoService>();

                        if (modelEscalonamento.Seq > 0)
                        {
                            var grupoEscalomentoPorEscalonamento = controllerEscalonamento.RecuperaGrupoEscalonamentoPorEscalonamento(modelEscalonamento.Seq);

                            ///Se o escalonamento que estiver sendo alterado estiver associado a mais de um grupo, exibir a mensagem de confirmação:
                            if (!string.IsNullOrEmpty(grupoEscalomentoPorEscalonamento))
                            {
                                return true;
                            }

                            if (controllerEscalonamento.VerificarDataParcelasDataFimEscalonamento(model.Transform<EscalonamentoData>()))
                            {
                                return true;
                            }
                        }

                        return controllerEscalonamento.ValidarDataFimEscalonamento(modelEscalonamento.SeqProcessoEtapa, modelEscalonamento.DataFim);
                    },
                    ///Segunda retorna o modelo que será exibida na view
                    (controller, model) =>
                    {
                        var modelEscalonamento = (model as EscalonamentoDynamicModel);
                        var controllerEscalonamento = controller.Create<IEscalonamentoService>();
                        var retorno = new EscalonamentoDynamicModel() { MensagemConfirmacaoAssert = string.Empty };

                        if (modelEscalonamento.Seq > 0)
                        {
                            var grupoEscalomentoPorEscalonamento = controllerEscalonamento.RecuperaGrupoEscalonamentoPorEscalonamento(modelEscalonamento.Seq);

                            ///Se o escalonamento que estiver sendo alterado estiver associado a mais de um grupo, exibir a mensagem de confirmação:
                            if (!string.IsNullOrEmpty(grupoEscalomentoPorEscalonamento))
                            {
                                retorno.MensagemConfirmacaoAssert += string.Format(UIResource.mensagem_confirmacao_associado_aos_grupos, grupoEscalomentoPorEscalonamento) + "<br /> <br />";
                            }

                            var parcelasEscalonamento = controllerEscalonamento.VerificarDataParcelasDataFimEscalonamento(model.Transform<EscalonamentoData>());

                            if (parcelasEscalonamento)
                            {
                                retorno.MensagemConfirmacaoAssert += UIResource.mensagem_data_parcelas_data_fim_escalonamento + "<br /> <br />";
                            }

                            // Conforme solicitação da tarefa Task 29766, não deve mais fazer essa verificação.
                            /////Se o escalonamento estiver sendo prorrogado(aumento da data/ fim) e estiver associado a
                            /////grupo(s) de escalonamento, verificar se existem solicitações associadas ao(s) grupo(s) em questão.
                            //if (controllerEscalonamento.VerificarExisteSolicitacaoServicoGrupoPorEscalonamento(modelEscalonamento.Seq, modelEscalonamento.DataFim))
                            //{
                            //    retorno.MensagemConfirmacaoAssert += UIResource.mensagem_confirmacao_solicitacoes_associadas + "<br /> <br />";
                            //}
                        }

                        if (controllerEscalonamento.ValidarDataFimEscalonamento(modelEscalonamento.SeqProcessoEtapa, modelEscalonamento.DataFim))
                        {
                            retorno.MensagemConfirmacaoAssert += UIResource.mensagem_confirmacao_datafimescalonamento_maior_datafimprocesso + "<br /> <br />";
                        }

                        retorno.MensagemConfirmacaoAssert += "Deseja realmente continuar?";

                        return retorno;
                    })
                   .ModalSize(SMCModalWindowSize.Medium)
                   .IgnoreFilterGeneration()
                   .IgnoreInsert()
                   .HeaderIndex(nameof(EscalonamentoController.CabecalhoEscalonamento))
                   .Header(nameof(EscalonamentoController.CabecalhoEscalonamentoCompleto))
                   .Service<IEscalonamentoService>(index: nameof(IEscalonamentoService.BuscarEscalonamentosPorProcesso),
                                                        save: nameof(IEscalonamentoService.SalvarEscalonamento),
                                                        edit: nameof(IEscalonamentoService.BuscarEscalonamento),
                                                        delete: nameof(IEscalonamentoService.ExlcuirEscalonamento))
                   .Tokens(tokenInsert: UC_SRC_002_05_02.MANTER_ESCALONAMENTO_ETAPA,
                           tokenEdit: UC_SRC_002_05_02.MANTER_ESCALONAMENTO_ETAPA,
                           tokenRemove: UC_SRC_002_05_02.MANTER_ESCALONAMENTO_ETAPA,
                           tokenList: UC_SRC_002_05_01.PESQUISAR_ESCALONAMENTO_ETAPA)
                   .ButtonBackIndex("Index", "Processo", model => new
                   {
                       SeqProcesso = SMCDESCrypto.EncryptNumberForURL((model as EscalonamentoFiltroDynamicModel).SeqProcesso.GetValueOrDefault())
                   })
            .Detail<EscalonamentoListarDynamicModel>("_DetailList");
        }

        public override void InitializeModel(SMCViewMode viewMode)
        {
            base.InitializeModel(viewMode);

            if (viewMode == SMCViewMode.Insert)
            {
                this.DescricaoGruposEscalonamento = new List<string>();
                this.DescricaoGruposEscalonamento.Add("-");
            }
        }
    }
}