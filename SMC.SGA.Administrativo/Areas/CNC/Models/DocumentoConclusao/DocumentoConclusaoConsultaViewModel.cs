using SMC.Academico.Common.Areas.CNC.Enums;
using SMC.DadosMestres.Common.Areas.PES.Enums;
using SMC.Framework;
using SMC.Framework.Mapper;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.DataAnnotations;
using System;
using System.Collections.Generic;
using SMC.SGA.Administrativo.Areas.CNC.Views.DocumentoConclusao.App_LocalResources;

namespace SMC.SGA.Administrativo.Areas.CNC.Models
{
    public class DocumentoConclusaoConsultaViewModel : SMCViewModelBase, ISMCMappable
    {
        #region Aluno

        [SMCHidden]
        public long? SeqPessoaDadosPessoais { get; set; }

        [SMCHidden]
        public long SeqPessoaAtuacao { get; set; }

        [SMCDisplay]
        [SMCSize(SMCSize.Grid2_24)]
        public long NumeroRegistroAcademico { get; set; }

        [SMCDisplay]
        [SMCValueEmpty("-")]
        [SMCSize(SMCSize.Grid3_24)]
        public int? CodigoAlunoMigracao { get; set; }

        [SMCHidden]
        public string Nome { get; set; }

        [SMCHidden]
        public string NomeSocial { get; set; }

        [SMCDisplay]
        [SMCSize(SMCSize.Grid7_24)]
        public string NomeCompleto
        {
            get
            {
                var nomeCompleto = string.Empty;

                if (!string.IsNullOrEmpty(NomeSocial))
                {
                    if (!string.IsNullOrEmpty(Nome))
                        nomeCompleto += $"{NomeSocial} ({Nome})";
                    else
                        nomeCompleto += $"{NomeSocial}";
                }
                else
                {
                    if (!string.IsNullOrEmpty(Nome))
                        nomeCompleto += $"{Nome}";
                }
                return nomeCompleto;
            }
        }

        [SMCDisplay]
        [SMCValueEmpty("-")]
        [SMCCpf]
        [SMCSize(SMCSize.Grid4_24)]
        public string Cpf { get; set; }

        [SMCDisplay]
        [SMCValueEmpty("-")]
        [SMCSize(SMCSize.Grid4_24)]
        public string NumeroPassaporte { get; set; }

        [SMCDisplay]
        [SMCSize(SMCSize.Grid4_24)]
        public string DescricaoNivelEnsino { get; set; }

        [SMCDisplay]
        [SMCSize(SMCSize.Grid6_24)]
        public string DescricaoCursoOferta { get; set; }

        [SMCDisplay]
        [SMCSize(SMCSize.Grid6_24)]
        public string DescricaoDocumentoConclusao { get; set; }

        [SMCDisplay]
        [SMCSize(SMCSize.Grid4_24)]
        public string DescricaoGrauAcademico { get; set; }

        [SMCDisplay]
        [SMCSize(SMCSize.Grid4_24)]
        public string DescricaoTitulacao { get; set; }

        [SMCHideLabel]
        [SMCSize(SMCSize.Grid24_24)]
        [SMCCssClass("smc-sga-mensagem-informativa smc-sga-mensagem")]
        [SMCConditionalDisplay(nameof(ExibirMensagemInformativaBachareladoLicenciatura), SMCConditionalOperation.Equals, true)]
        public string MensagemInformativaBachareladoLicenciatura => UIResource.Mensagem_Informativa_Grau_Bacharelado_Licenciatura;

        [SMCHidden]
        public bool ExibirMensagemInformativaBachareladoLicenciatura { get; set; }

        [SMCSize(SMCSize.Grid24_24)]
        public List<DocumentoConclusaoFormacaoListarViewModel> FormacoesEspecificas { get; set; }

        #endregion

        #region Documento

        [SMCHidden]
        public string TokenSituacaoDocumentoAcademicoAtual { get; set; }

        [SMCHidden]
        public string TokenTipoDocumento { get; set; }

        [SMCDisplay]
        [SMCSize(SMCSize.Grid2_24)]
        public long Seq { get; set; }

        [SMCHidden]
        public long SeqTipoDocumentoAcademico { get; set; }

        [SMCDisplay]
        [SMCSize(SMCSize.Grid4_24)]
        public string DescricaoTipoDocumentoAcademico { get; set; }

        [SMCHidden]
        public short NumeroViaDocumento { get; set; }

        [SMCDisplay]
        [SMCSize(SMCSize.Grid1_24)]
        public string NumeroViaDocumentoFormatado
        {
            get
            {
                if (NumeroViaDocumento == 0)
                    return $"-";

                return $"{NumeroViaDocumento}°";
            }
        }

        [SMCDisplay]
        [SMCValueEmpty("-")]
        [SMCSize(SMCSize.Grid4_24)]
        public string NumeroProcesso { get; set; }

        [SMCHidden]
        public long? SeqSolicitacaoServico { get; set; }

        [SMCDisplay]
        public string NumeroProtocolo { get; set; }

        [SMCDisplay]
        [SMCSize(SMCSize.Grid5_24)]
        public string CodigoMigracaoDocumento { get; set; }

        [SMCDisplay]
        [SMCValueEmpty("-")]
        [SMCSize(SMCSize.Grid4_24)]
        public long? SeqDocumentoDiplomaGAD { get; set; }

        [SMCHidden]
        public string UrlDocumentoGAD { get; set; }

        [SMCDisplay]
        [SMCSize(SMCSize.Grid5_24)]
        public bool LancamentoHistorico { get; set; }

        [SMCDisplay]
        [SMCSize(SMCSize.Grid4_24)]
        public bool PossuiApostilamentos { get; set; }

        [SMCHidden]
        public long? SeqDocumentoConclusaoViaAnterior { get; set; }

        [SMCHidden]
        public short? NumeroViaDocumentoAnterior { get; set; }

        [SMCHidden]
        public string DescricaoTipoDocumentoAnterior { get; set; }

        [SMCHidden]
        public string NumeroRegistroAnterior { get; set; }

        [SMCHidden]
        public DateTime? DataRegistroAnterior { get; set; }

        //[SMCDisplay]
        //[SMCValueEmpty("-")]
        //[SMCSize(SMCSize.Grid24_24)]
        //public string DescricaoDocumentoAnterior { get; set; }

        [SMCHidden]
        public short TipoPapel { get; set; }

        [SMCDisplay]
        [SMCValueEmpty("-")]
        [SMCSize(SMCSize.Grid5_24)]
        public string UsuarioImpressao { get; set; }

        [SMCDisplay]
        [SMCValueEmpty("-")]
        [SMCSize(SMCSize.Grid4_24)]
        public DateTime? DataImpressao { get; set; }

        [SMCDisplay]
        [SMCValueEmpty("-")]
        [SMCSize(SMCSize.Grid5_24)]
        public long? NumeroSerie { get; set; }

        [SMCDisplay]
        [SMCValueEmpty("-")]
        [SMCSize(SMCSize.Grid3_24)]
        public TipoRegistroDocumento? TipoRegistroDocumento { get; set; }

        [SMCDisplay]
        [SMCValueEmpty("-")]
        [SMCSize(SMCSize.Grid6_24)]
        public string GrupoRegistro { get; set; }

        [SMCDisplay]
        [SMCValueEmpty("-")]
        [SMCSize(SMCSize.Grid4_24)]
        public string OrgaoDeRegistro { get; set; }

        [SMCDisplay]
        [SMCValueEmpty("-")]
        [SMCSize(SMCSize.Grid4_24)]
        public string NumeroRegistro { get; set; }

        [SMCDisplay]
        [SMCValueEmpty("-")]
        [SMCSize(SMCSize.Grid3_24)]
        public DateTime? DataRegistro { get; set; }

        [SMCDisplay]
        [SMCValueEmpty("-")]
        [SMCSize(SMCSize.Grid4_24)]
        public string UsuarioRegistro { get; set; }

        [SMCDisplay]
        [SMCValueEmpty("-")]
        [SMCSize(SMCSize.Grid3_24)]
        public string Livro { get; set; }

        [SMCDisplay]
        [SMCValueEmpty("-")]
        [SMCSize(SMCSize.Grid3_24)]
        public string Folha { get; set; }

        [SMCHidden]
        public bool ExibirSecaoEntrega { get; set; }

        [SMCDisplay]
        [SMCSize(SMCSize.Grid8_24)]
        public bool? ConfirmacaoAluno { get; set; }

        #endregion

        #region Histórico de Download 

        [SMCHidden]
        public bool ExibirSecaoHistoricosDownload { get; set; }

        [SMCHidden]
        public DocumentoConclusaoFiltroHistoricoDownloadViewModel FiltroHistoricoDownload { get; set; }

        #endregion

        #region Documento Conclusão Anterior 

        [SMCSize(SMCSize.Grid24_24)]
        public List<DocumentoConclusaoViaAnteriorListarViewModel> DocumentoConclusaoViaAnterior { get; set; }

        #endregion

        #region Situações 

        [SMCSize(SMCSize.Grid24_24)]
        public List<DocumentoConclusaoHistoricoListarViewModel> Situacoes { get; set; }

        #endregion

        #region Informações Adicionais 

        [SMCSize(SMCSize.Grid24_24)]
        public List<DocumentoConclusaoMensagemViewModel> InformacoesAdicionais { get; set; }

        #endregion

        #region Habilitar Botoes

        [SMCHidden]
        public bool HabilitarBotaoApostilamento { get; set; }

        [SMCHidden]
        public bool ExibirBotaoApostilamento { get; set; }

        [SMCHidden]
        public bool HabilitarSecoes { get; set; }

        [SMCHidden]
        public bool HabilitarSecaoRegistro { get; set; }

        [SMCHidden]
        public string MensagemBotaoApostilamento
        {
            get
            {
                if (!this.HabilitarBotaoApostilamento)
                {
                    return "MensagemDesabilitaBotaoApostilamento";
                }

                return string.Empty;
            }
        }

        [SMCHidden]
        public bool ExibirBotaoAnularDocumentoDigital { get; set; }

        [SMCHidden]
        public bool ExibirBotaoRestaurarDocumentoDigital { get; set; }

        [SMCHidden]
        public bool ExibirMensagemRestaurarDocumentoDigital { get; set; }

        #endregion
    }
}