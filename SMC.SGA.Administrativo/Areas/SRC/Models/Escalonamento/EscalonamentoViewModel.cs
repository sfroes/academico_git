using SMC.Academico.Common.Areas.SRC.Enums;
using SMC.Framework;
using SMC.Framework.DataAnnotations;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SMC.SGA.Administrativo.Areas.SRC.Models
{
    public class EscalonamentoViewModel : SMCViewModelBase
    {
        [SMCHidden]
        [SMCKey]
        public long Seq { get; set; }

        [SMCHidden]
        public long SeqProcesso { get; set; }

        [SMCHidden]
        public long SeqProcessoEtapa { get; set; }

        [SMCDateTimeMode(SMCDateTimeMode.DateTime)]
        [SMCCssClass("smc-size-md-4 smc-size-xs-4 smc-size-sm-4 smc-size-lg-4")]
        public DateTime DataInicio { get; set; }

        [SMCDateTimeMode(SMCDateTimeMode.DateTime)]
        [SMCCssClass("smc-size-md-4 smc-size-xs-4 smc-size-sm-4 smc-size-lg-4")]
        public DateTime DataFim { get; set; }

        [SMCDateTimeMode(SMCDateTimeMode.DateTime)]
        [SMCCssClass("smc-size-md-4 smc-size-xs-4 smc-size-sm-4 smc-size-lg-4")]
        public DateTime? DataEncerramento { get; set; }

        [SMCCssClass("smc-size-md-12 smc-size-xs-12 smc-size-sm-12 smc-size-lg-12")]
        public List<string> DescricaoGruposEscalonamento { get; set; }

        [SMCHidden]
        public SituacaoEtapa SituacaoEtapa { get; set; }

        [SMCHidden]
        public bool Vigente { get; set; }

        [SMCHidden]
        public bool ExisteSolicitacaoGrupoEscalonamento { get; set; }

        #region Instruções aos Botões

        [SMCHidden]
        public bool HabilitaBtnComPermissaoManutencaoProcesso { get; set; }

        [SMCHidden]
        public bool VigenteLiberadoComSolicitacaoGrupo => this.Vigente && this.SituacaoEtapa == SituacaoEtapa.Liberada && ExisteSolicitacaoGrupoEscalonamento;

        [SMCHidden]
        public bool VigenteEncerrada => this.SituacaoEtapa == SituacaoEtapa.Encerrada;

        [SMCHidden]
        public bool ExsiteGrupoEscalonamento => this.DescricaoGruposEscalonamento.SMCAny();

        [SMCHidden]
        public bool HabilitarBotaoEditar
        {
            ///NV06
            ///Senão, se o escalonamento for vigente e, associado a grupos de escalonamento e, algum dos grupos possui pelo
            ///menos 1(uma) solicitação associada e, a situação da etapa é igual a Liberada. O botão Alterar deve ser desabilitado 
            ///com a seguinte mensagem informativa: Opção indisponível. Para alterar um escalonamento vigente e que está associado
            ///a um grupo de escalonamento e que possui solicitações associadas, é necessário que a etapa esteja em manutenção.”.
            ///Senão, o botão Alterar deve ser habilitado.
            get
            {
                if (this.VigenteLiberadoComSolicitacaoGrupo)
                {
                    return false;
                }

                ///Se a situação da etapa for Encerrada, o botão Alterar deve ser desabilitado com a seguinte mensagem informativa: 
                ///“Opção indisponível.A etapa está encerrada.
                //if (this.VigenteEncerrada)
                //{
                //    return false;
                //}

                //Se a existir data de encerramento, o botão Alterar deve ser desabilitado com a seguinte mensagem informativa: 
                //“Opção indisponível.A etapa está encerrada.
                if (this.DataEncerramento.HasValue)
                {
                    return false;
                }

                return true;
            }
        }

        [SMCHidden]
        public string MensagemBotaoEditar
        {
            get
            {
                if (this.VigenteLiberadoComSolicitacaoGrupo)
                {
                    return "botao_instructions_editar_situacao_liberada_vigente";
                }

                //if (this.VigenteEncerrada)
                //{
                //    return "botao_instructions_editar_situacao_encerrada";
                //}

                if (this.DataEncerramento.HasValue)
                {
                    return "botao_instructions_editar_situacao_encerrada";
                }

                return null;
            }
        }

        [SMCHidden]
        public bool HabilitarBotaoExcluir
        {
            get
            {
                if (this.DataEncerramento.HasValue)
                {
                    return false;
                }

                if (this.ExsiteGrupoEscalonamento)
                {
                    return false;
                }

                return true;
            }
        }

        [SMCHidden]
        public string MesnsageBotaoExcluir
        {
            get
            {
                //Se o respectivo escalonamento estiver encerrado, o botão Excluir deve ser desabilitado com a seguinte mensagem informativa:
                //"Opção indisponível. O escalonamento está encerrado."
                if (this.DataEncerramento.HasValue)
                {
                    return "botao_instructions_excluir_escalonamento_encerrado";
                }

                //Se o escalonamento em questão estiver associado a algum grupo de escalonamento, o botão Excluir deverá ser desabilitado com a seguinte mensagem informativa: 
                //"Opção indisponível. Este escalonamento já está associado a um grupo."
                if (this.ExsiteGrupoEscalonamento)
                {
                    return "botao_instructions_excluir_por_grupo";
                }

                return null;
            }
        }

        #endregion

    }
}