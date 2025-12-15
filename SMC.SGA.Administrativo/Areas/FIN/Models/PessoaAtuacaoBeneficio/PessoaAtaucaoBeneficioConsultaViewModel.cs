using SMC.Academico.Common.Areas.ALN.Enums;
using SMC.Academico.Common.Areas.FIN.Enums;
using SMC.Academico.UI.Mvc.Areas.CAM.Lookups;
using SMC.Framework;
using SMC.Framework.DataAnnotations;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.DataAnnotations;
using SMC.Framework.Util;
using System;
using System.Collections.Generic;

namespace SMC.SGA.Administrativo.Areas.FIN.Models
{
    public class PessoaAtaucaoBeneficioConsultaViewModel : SMCViewModelBase
    {
        public long Seq { get; set; }

        public long SeqPessoaAtuacao { get; set; }

        [SMCValueEmpty("-")]
        [SMCSize(SMCSize.Grid14_24, SMCSize.Grid24_24, SMCSize.Grid24_24, SMCSize.Grid12_24)]
        public string DescricaoBeneficio { get; set; }

        [SMCValueEmpty("-")]
        [SMCSize(SMCSize.Grid10_24, SMCSize.Grid24_24, SMCSize.Grid24_24, SMCSize.Grid12_24)]
        public string DescricaoConfiguracaoBeneficio { get; set; }

        [SMCValueEmpty("-")]
        [SMCSize(SMCSize.Grid6_24, SMCSize.Grid24_24, SMCSize.Grid14_24, SMCSize.Grid6_24)]
        public string DescricaoSituacaoBeneficio { get; set; }

        [SMCValueEmpty("-")]
        [SMCSize(SMCSize.Grid10_24, SMCSize.Grid24_24, SMCSize.Grid14_24, SMCSize.Grid5_24)]
        public string DescricaoFormaDeducao { get; set; }

        [SMCValueEmpty("-")]
        [SMCSize(SMCSize.Grid9_24, SMCSize.Grid24_24, SMCSize.Grid10_24, SMCSize.Grid6_24)]
        public string DescricaoTipoDeducao { get; set; }

        [SMCDateTimeMode(SMCDateTimeMode.Date)]
        [SMCSize(SMCSize.Grid4_24, SMCSize.Grid24_24, SMCSize.Grid7_24, SMCSize.Grid4_24)]
        public DateTime DataInicioVigencia { get; set; }

        [SMCDateTimeMode(SMCDateTimeMode.Date)]
        [SMCValueEmpty("-")]
        [SMCSize(SMCSize.Grid4_24, SMCSize.Grid24_24, SMCSize.Grid7_24, SMCSize.Grid4_24)]
        public DateTime? DataFimVigencia { get; set; }

        [SMCValueEmpty("-")]
        [SMCSize(SMCSize.Grid8_24, SMCSize.Grid24_24, SMCSize.Grid10_24, SMCSize.Grid5_24)]
        public decimal? ValorBeneficio { get; set; }

        public bool Aluno { get; set; }

        [SMCSize(SMCSize.Grid7_24, SMCSize.Grid24_24, SMCSize.Grid10_24, SMCSize.Grid5_24)]
        public bool? IncideParcelaMatricula { get; set; }

        public int? IdAssociarResponsavelFinanceiro { get; set; }

        public bool? ExibeValoresTermoAdesao { get; set; }

        public List<PessoaAtuacaoBeneficioBeneficioHistoricoSituacaoViewModel> HistoricoSituacoes { get; set; }

        public List<PessoaAtuacaoBeneficioResponsavelFinanceiroViewModel> ResponsaveisFinanceiro { get; set; }

        public List<PessoaAtaucaoBeneficioControleFinanceiroViewModel> ControlesFinanceiros { get; set; }

        public List<PessoaAtuacaoBeneficioBeneficioHistoricoVigenciaViewModel> HistoricoVigencias { get; set; }
        
        public List<PessoaAtuacaoBeneficioBeneficioEnvioNotificacaoViewModel> Notificacoes { get; set; }

        public List<PessoaAtuacaoBeneficioAnexoViewModel> ArquivosAnexo { get; set; }

        #region Hidden

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

        [SMCHidden]
        public int SeqSituacaoChancelaBeneficioAtual { get; set; }

        public SituacaoChancelaBeneficio SituacaoChancelaBeneficio
        {
            get
            {
                var situacao = SMCEnumHelper.GetEnum<SituacaoChancelaBeneficio>(this.SeqSituacaoChancelaBeneficioAtual.ToString());

                return situacao;
            }
        }

        public bool AlunoAtivo { get; set; }

        public SituacaoIngressante? SituacaoIngressante { get; set; }

        [SMCHidden]
        public bool IdDeducaoValorParcelaTitular { get; set; }

        #endregion

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

        public bool HabilitarBotaoAlterar
        {
            get
            {
                if (this.SituacaoChancelaBeneficio == SituacaoChancelaBeneficio.Deferido || this.SituacaoChancelaBeneficio == SituacaoChancelaBeneficio.Excluido)
                {
                    return false;
                }

                if (Aluno && !AlunoAtivo)
                {
                    return false;
                }
                else if (!Aluno)
                {
                    if (SituacaoIngressante == Academico.Common.Areas.ALN.Enums.SituacaoIngressante.Matriculado
                           || SituacaoIngressante == Academico.Common.Areas.ALN.Enums.SituacaoIngressante.Desistente
                           || SituacaoIngressante == Academico.Common.Areas.ALN.Enums.SituacaoIngressante.Cancelado)
                    {
                        return false;
                    }
                }

                return true;
            }
        }

        public string MensagemBotaoAlterar
        {
            get
            {
                if (this.SituacaoChancelaBeneficio == SituacaoChancelaBeneficio.Deferido || this.SituacaoChancelaBeneficio == SituacaoChancelaBeneficio.Excluido)
                {
                    return "MensagemDesabilitaBotaoAlterar";
                }

                if (Aluno && !AlunoAtivo)
                {
                    return "MensagemDesabilitaBotaoAlterarAluno";
                }
                else if (!Aluno)
                {
                    if (SituacaoIngressante == Academico.Common.Areas.ALN.Enums.SituacaoIngressante.Matriculado
                           || SituacaoIngressante == Academico.Common.Areas.ALN.Enums.SituacaoIngressante.Desistente
                           || SituacaoIngressante == Academico.Common.Areas.ALN.Enums.SituacaoIngressante.Cancelado)
                    {
                        return "MensagemDesabilitaBotaoAlterarIngressante";
                    }
                }

                return string.Empty;
            }
        }

        public bool HabilitarBotaoAlterarVigencia
        {
            get
            {
                if (this.SituacaoChancelaBeneficio != SituacaoChancelaBeneficio.Deferido)
                {
                    return false;
                }

                //if (Aluno && !AlunoAtivo)
                //{
                //    return false;
                //}
                else if (!Aluno)
                {
                    if (SituacaoIngressante == Academico.Common.Areas.ALN.Enums.SituacaoIngressante.Matriculado
                           || SituacaoIngressante == Academico.Common.Areas.ALN.Enums.SituacaoIngressante.Desistente
                           || SituacaoIngressante == Academico.Common.Areas.ALN.Enums.SituacaoIngressante.Cancelado)
                    {
                        return false;
                    }
                }

                return true;
            }
        }

        public string MensagemBotaoAlterarVigencia
        {
            get
            {
                if (this.SituacaoChancelaBeneficio != SituacaoChancelaBeneficio.Deferido)
                {
                    return "MensagemDesabilitaBotaoAlterarVigencia";
                }

                //if (Aluno && !AlunoAtivo)
                //{
                //    return "MensagemDesabilitaBotaoAlterarVigenciaAluno";
                //}
                else if (!Aluno)
                {
                    if (SituacaoIngressante == Academico.Common.Areas.ALN.Enums.SituacaoIngressante.Matriculado
                           || SituacaoIngressante == Academico.Common.Areas.ALN.Enums.SituacaoIngressante.Desistente
                           || SituacaoIngressante == Academico.Common.Areas.ALN.Enums.SituacaoIngressante.Cancelado)
                    {
                        return "MensagemDesabilitaBotaoAlterarVigenciaIngressante";
                    }
                }
                return string.Empty;
            }

        }

        #endregion
    }
}