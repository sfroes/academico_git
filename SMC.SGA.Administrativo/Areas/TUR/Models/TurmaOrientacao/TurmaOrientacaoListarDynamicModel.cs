using SMC.Academico.Common.Areas.MAT.Constants;
using SMC.Academico.Common.Areas.ORT.Enums;
using SMC.Academico.Common.Areas.TUR.Constants;
using SMC.Academico.ServiceContract.Areas.TUR.Interfaces;
using SMC.Academico.UI.Mvc.Areas.DCT.Lookups;
using SMC.Framework;
using SMC.Framework.Security;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.DataAnnotations;
using SMC.Framework.UI.Mvc.Dynamic;
using System.Collections.Generic;

namespace SMC.SGA.Administrativo.Areas.TUR.Models
{
    public class TurmaOrientacaoListarDynamicModel : SMCDynamicViewModel
    {
        [SMCHidden]
        [SMCKey]
        public override long Seq { get; set; }

        public long? SeqOrientacao { get; set; }

        public long SeqPessoaAtuacao { get; set; }

        public long SeqNivelEnsino { get; set; }

        public long[] SeqsAlunos { get; set; }

        public string Nome { get; set; }

        public string RaNome { get; set; }

        public List<TurmaOrientacaoColaboradorViewModel> Colaboradores { get; set; }

        [SMCHidden]
        [SMCParameter]
        public long SeqTurma { get; set; }

        [SMCHidden]
        [SMCParameter]
        public long SeqDivisaoTurma { get; set; }

        public string DescricaoSituacaoMatricula { get; set; }

        public string TokenTipoSituacaoMatricula { get; set; }

        public bool DiarioFechado { get; set; }

        #region Desbiltar botões

        public List<string> tokensBloqueioBotao { get; set; } = new List<string>{TOKENS_TIPO_SITUACAO_MATRICULA.FORMADO,
                                                                                 TOKENS_TIPO_SITUACAO_MATRICULA.APTO_MATRICULA,
                                                                                 TOKENS_TIPO_SITUACAO_MATRICULA.NAO_MATRICULADO };

        public bool HabilitarBotaoEditar
        {
            get
            {            
                if (tokensBloqueioBotao.Contains(this.TokenTipoSituacaoMatricula))
                {
                    return false;
                }

                if(!this.SeqOrientacao.HasValue)
                {
                    return false;
                }

                if (this.DiarioFechado)
                {
                    return false;
                }

                return true;
            }
        }

        public string MensagemBotaoEditar
        {
            get
            {
                if (tokensBloqueioBotao.Contains(this.TokenTipoSituacaoMatricula))
                {
                    return "Botao_Editar_Instruictions_Token";
                }

                if (!this.SeqOrientacao.HasValue)
                {
                    return "Botao_Editar_Instruictions_Orientacao";
                }

                if (this.DiarioFechado)
                {
                    return "Botao_Editar_Instruictions_Diario_Fechado";
                }

                return null;
            }
        }

        #endregion
    }
}