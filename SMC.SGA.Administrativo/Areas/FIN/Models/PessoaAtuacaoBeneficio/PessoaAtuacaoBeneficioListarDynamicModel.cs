using SMC.Academico.Common.Areas.FIN.Enums;
using SMC.Framework;
using SMC.Framework.UI.Mvc.DataAnnotations;
using SMC.Framework.UI.Mvc.Dynamic;
using SMC.Framework.Util;
using System;

namespace SMC.SGA.Administrativo.Areas.FIN.Models
{
    public class PessoaAtuacaoBeneficioListarDynamicModel : SMCDynamicViewModel
    {
        [SMCKey]
        public override long Seq { get; set; }

        public long SeqPessoaAtuacao { get; set; }

        [SMCSize(SMCSize.Grid8_24, SMCSize.Grid24_24, SMCSize.Grid7_24, SMCSize.Grid8_24)]
        public long? SeqBeneficio { get; set; }

        public long? SeqPessoaAtuacaoBeneficio { get; set; }

        [SMCSize(SMCSize.Grid3_24, SMCSize.Grid24_24, SMCSize.Grid5_24, SMCSize.Grid2_24)]
        public DateTime DataInicioVigencia { get; set; }

        [SMCSize(SMCSize.Grid3_24, SMCSize.Grid24_24, SMCSize.Grid5_24, SMCSize.Grid2_24)]
        public DateTime DataFimVigencia { get; set; }

        [SMCSize(SMCSize.Grid8_24, SMCSize.Grid24_24, SMCSize.Grid7_24, SMCSize.Grid8_24)]
        public TipoDeducao TipoDeducao { get; set; }

        [SMCSize(SMCSize.Grid4_24, SMCSize.Grid24_24, SMCSize.Grid12_24, SMCSize.Grid4_24)]
        public FormaDeducao? FormaDeducao { get; set; }

        [SMCSize(SMCSize.Grid4_24, SMCSize.Grid24_24, SMCSize.Grid5_24, SMCSize.Grid4_24)]
        public decimal? ValorBeneficio { get; set; }

        [SMCSize(SMCSize.Grid8_24, SMCSize.Grid24_24, SMCSize.Grid7_24, SMCSize.Grid8_24)]
        public string DescricaoBeneficio { get; set; }

        [SMCSize(SMCSize.Grid4_24, SMCSize.Grid24_24, SMCSize.Grid7_24, SMCSize.Grid4_24)]
        public SituacaoChancelaBeneficio SituacaoChancelaBeneficio
        {
            get
            {
                var situacao = SMCEnumHelper.GetEnum<SituacaoChancelaBeneficio>(this.SeqSituacaoChancelaBeneficioAtual.ToString());

                return situacao;
            }
        }

        [SMCIgnoreProp]
        public string Nome { get; set; }

        [SMCIgnoreProp]
        public string NomeSocial { get; set; }

        [SMCIgnoreProp]
        public string NomePessoa
        {
            get
            {
                if (!string.IsNullOrEmpty(this.NomeSocial))
                {
                    return $"{this.NomeSocial} ({this.Nome})";
                }

                return this.Nome;
            }
        }

        public PessoaAtuacaoBeneficioConfiguracaoViewModel ConfiguracaoBeneficio { get; set; }

        [SMCHidden]
        public bool Aluno { get; set; }

        [SMCHidden]
        public int SeqSituacaoChancelaBeneficioAtual { get; set; }

        #region Habilitar Botoes

        [SMCHidden]
        public bool DesabilitarBotaoExcluir
        {
            get
            {
                return this.SituacaoChancelaBeneficio != SituacaoChancelaBeneficio.Excluido;
            }
        }

        [SMCHidden]
        public string MensagemBotaoExcluir
        {
            get
            {
                if (this.SituacaoChancelaBeneficio == SituacaoChancelaBeneficio.Excluido)
                {
                    return "MensagemDesabilitaBotaoExcluir";
                }

                return string.Empty;
            }
        }

        [SMCHidden]
        public string ClasseItemExcluido
        {
            get
            {
                if (this.SituacaoChancelaBeneficio == SituacaoChancelaBeneficio.Excluido)
                {
                    return "smc-listdetailed-item-excluido";
                }

                return string.Empty;
            }
        }

        public bool DesabilitarBotaoAlterar
        {
            get
            {
                return this.SituacaoChancelaBeneficio == SituacaoChancelaBeneficio.AguardandoChancela || this.SituacaoChancelaBeneficio == SituacaoChancelaBeneficio.Indeferido;
            }
        }

        public string MensagemBotaoAlterar
        {
            get
            {
                if (this.SituacaoChancelaBeneficio != SituacaoChancelaBeneficio.AguardandoChancela && this.SituacaoChancelaBeneficio != SituacaoChancelaBeneficio.Indeferido)
                {
                    return "MensagemDesabilitaBotaoAlterar";
                }

                return string.Empty;
            }
        }

        #endregion
    }
}