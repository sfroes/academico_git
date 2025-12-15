using SMC.Academico.Common.Areas.ALN.Enums;
using SMC.Framework.UI.Mvc.DataAnnotations;
using SMC.Framework.UI.Mvc.Dynamic;
using SMC.Framework.Util;
using SMC.SGA.Administrativo.Areas.ALN.Views.Ingressante.App_LocalResources;
using System;
using System.Collections.Generic;
using System.Text;

namespace SMC.SGA.Administrativo.Areas.ALN.Models
{
    public class IngressanteListarDynamicModel : SMCDynamicViewModel
    {
        #region [ Dados Pessoais ]

        [SMCKey]
        public override long Seq { get; set; }

        [SMCDescription]
        public string Nome { get; set; }

        [SMCCpf]
        public string Cpf { get; set; }

        public string NumeroPassaporte { get; set; }

        /// <summary>
        /// Valor do Cpf ou Passaporte
        /// </summary>
        public string CpfOuPassaporte { get => string.IsNullOrEmpty(Cpf) ? NumeroPassaporte : SMCMask.ApplyMaskCPF(Cpf); }

        public DateTime DataNascimento { get; set; }

        public SituacaoIngressante SituacaoIngressante { get; set; }

        public OrigemIngressante OrigemIngressante { get; set; }

        public bool Falecido { get; set; }

        #endregion [ Dados Pessoais ]

        #region [ Dados Acadêmicos ]

        public long SeqNivelEnsino { get; set; }

        public long SeqCurso { get; set; }

        public long? SeqFormacaoEspecifica { get; set; }

        public string Campanha { get; set; }

        public string ProcessoSeletivo { get; set; }

        public string GrupoEscalonamento { get; set; }

        public string Vinculo { get; set; }

        public string FormaIngresso { get; set; }

        public string DescricaoNivelEnsino { get; set; }

        public List<string> OfertasCampanha { get; set; }

        public string MatrizCurricularOferta { get; set; }

        public List<string> FormacoesEspecificas { get; set; }

        public bool PossuiSituacaoImpeditivaIngressante { get; set; }

        public bool NaoExigeOfertaMatrizCurricular { get; set; }

        public bool NaoPossuiVinculoAssociacaoOrientador { get; set; }

        public bool NaoPermiteAssociacaoOrientador { get; set; }

        public string TipoOrientacao { get; set; }

        public List<string> Orientadores { get; set; }

        public bool TipoVinculoAlunoExigeCurso { get; set; }

        public string DescricaoInstituicaoExterna { get; set; }

        public string InformacaoEscalonamentoFormatada
        {
            get
            {
                var descricao = new StringBuilder();
                if (TipoVinculoAlunoExigeCurso && !string.IsNullOrEmpty(DescricaoInstituicaoExterna))
                    descricao.AppendLine($"<b>{UIResource.Label_InstituicaoExterna}</b><br />{DescricaoInstituicaoExterna}<br />");
                if (ExigePeriodoIntercambioTermo && DataInicioTermoIntercambio.HasValue && DataFimTermoIntercambio.HasValue)
                    descricao.AppendLine($"<b>{UIResource.Label_PeriodoIntercambio}</b><br />{DataInicioTermoIntercambio.Value.ToString("dd/MM/yyyy")} à {DataFimTermoIntercambio.Value.ToString("dd/MM/yyyy")}");
                return descricao.ToString();
            }
        }

        public bool ExigePeriodoIntercambioTermo { get; set; }

        public DateTime? DataInicioTermoIntercambio { get; set; }

        public DateTime? DataFimTermoIntercambio { get; set; }

        public string DescricaoInstituicaoTransferenciaExterna { get; set; }

        public string CursoTransferenciaExterna { get; set; }

        public bool NaoPermiteEdicao => IngressanteDynamicModel.SituacaoNaoPermiteEdicao(SituacaoIngressante);

        public bool ConfirmacaoEdicao
        {
            get
            {
                return (SituacaoIngressante == SituacaoIngressante.AguardandoLiberacaMatricula ||
                        SituacaoIngressante == SituacaoIngressante.AptoMatricula) &&
                       (
                           OrigemIngressante == OrigemIngressante.Convocacao ||
                           OrigemIngressante == OrigemIngressante.ImportacaoPlanilha ||
                           OrigemIngressante == OrigemIngressante.SelecionadoGPI
                       );
            }
        }

        public string MensagemEdicao => NaoPermiteEdicao ? UIResource.MSG_Visualizacao_Situacao : UIResource.MSG_Edicao_DadosPessoais;

        public bool ExibirLiberacaoMaricula { get; set; }

        public bool PermitirLiberacaoMatricula { get; set; }

        public bool VinculoInstituicaoNivelEnsinoExigeCurso { get; set; }

        #endregion [ Dados Acadêmicos ]
    }
}