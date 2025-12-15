using SMC.Academico.Common.Areas.CNC.Enums;
using SMC.Academico.Common.Areas.SRC.Constants;
using SMC.Academico.Common.Constants;
using SMC.Academico.UI.Mvc.Areas.SRC.Models;
using SMC.DadosMestres.Common.Areas.PES.Enums;
using SMC.Framework;
using SMC.Framework.DataAnnotations;
using SMC.Framework.UI.Mvc.DataAnnotations;
using SMC.Framework.Util;
using SMC.SGA.Administrativo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using SMC.SGA.Administrativo.Areas.SRC.Views.RealizarAtendimento.App_LocalResources;
using SMC.Framework.UI.Mvc.Html;
using SMC.Framework.Model;

namespace SMC.SGA.Administrativo.Areas.SRC.Models
{
    public class SolicitacaoAnaliseEmissaoDocumentoConclusaoViewModel : SolicitacaoServicoPaginaViewModelBase
    {
        #region Data Sources

        public List<SMCDatasourceItem> TiposDocumento { get; set; }

        public List<SMCDatasourceItem> TiposHistoricoEscolar { get; set; }

        public List<SMCDatasourceItem> TiposGrauAcademico { get; set; }

        #endregion

        #region CAMPOS HIDDEN

        [SMCHidden]
        public override string Token => TOKEN_SOLICITACAO_SERVICO.ANALISE_EMISSAO_DOCUMENTO_CONCLUSAO;

        [SMCHidden]
        public override string ChaveTextoBotaoProximo => EmissaoPermitida ? "Botao_Concluiratendimento" : "Botao_Cancelarsolicitacao";

        [SMCHidden]
        public long SeqSolicitacaoServicoAuxiliar { get; set; }

        [SMCHidden]
        public long SeqConfiguracaoEtapaAuxiliar { get; set; }

        [SMCHidden]
        public long SeqTipoDocumentoAcademico { get; set; }

        [SMCHidden]
        public bool ExisteDocumentoConclusao { get; set; }

        [SMCHidden]
        public long SeqNivelEnsino { get; set; }

        [SMCHidden]
        public string TokenNivelEnsino { get; set; }

        [SMCHidden]
        public long SeqCursoOfertaLocalidade { get; set; }

        [SMCHidden]
        public string DescricaoCursoOfertaLocalidade { get; set; }

        [SMCHidden]
        public int? CodigoUnidadeSeo { get; set; }

        [SMCHidden]
        public string DescricaoCursoOferta { get; set; }

        [SMCHidden]
        public long SeqPessoaAtuacaoAuxiliar { get; set; }

        [SMCHidden]
        public long SeqPessoa { get; set; }

        [SMCHidden]
        public long SeqTemplateProcessoSGF { get; set; }

        [SMCHidden]
        public List<long> SeqsTiposTermosIntercambio { get; set; }

        [SMCHidden]
        public EstadoCivil? EstadoCivil { get; set; }

        [SMCHidden]
        public DateTime? DataConclusao { get; set; }

        [SMCHidden]
        public long? SeqInstituicaoEnsino { get; set; }

        [SMCHidden]
        public long SeqOrgaoRegistro { get; set; }

        [SMCHidden]
        public bool EmissaoDiplomaDigital1Via { get; set; }

        [SMCHidden]
        public bool ExisteFormacaoAssociada { get; set; }

        [SMCHidden]
        public bool ExibirNomeSocial { get; set; }

        [SMCHidden]
        public bool HabilitarCampoGrauAcademico { get; set; }

        [SMCHidden]
        public bool ExibirDadosDiploma { get; set; }

        [SMCHidden]
        public bool ExibirDadosHistorico { get; set; }

        [SMCHidden]
        public long SeqPessoaDadosPessoais { get; set; }

        [SMCHidden]
        public long? SeqTitulacao { get; set; }

        [SMCHidden]
        public string DescricaoCursoDocumentoAuxiliar { get; set; }

        #endregion

        #region CAMPOS ANALISE

        [SMCSize(SMCSize.Grid5_24)]
        public string DescricaoTipoDocumentoAcademico { get; set; }

        [SMCSize(SMCSize.Grid6_24)]
        public bool EmissaoPermitida { get; set; }

        #endregion

        #region CAMPOS EMISSÃO NÃO PERMITIDA

        [SMCSize(SMCSize.Grid24_24)]
        public string MotivoEmissaoNaoPermitida { get; set; }

        [SMCSize(SMCSize.Grid24_24)]
        [SMCMultiline]
        [SMCConditionalRequired(nameof(EmissaoPermitida), false)]
        public string ObservacoesEmissaoNaoPermitida { get; set; }

        #endregion

        #region CAMPOS DADOS PESSOAIS 

        [SMCSize(SMCSize.Grid2_24)]
        public long NumeroRA { get; set; }

        [SMCSize(SMCSize.Grid3_24)]
        [SMCValueEmpty("-")]
        public long? CodigoAlunoMigracao { get; set; }

        [SMCSize(SMCSize.Grid3_24)]
        public int CodigoPessoaCAD { get; set; }

        [SMCSize(SMCSize.Grid3_24)]
        public string Cpf { get; set; }

        [SMCSize(SMCSize.Grid7_24)]
        public string NomeAluno { get; set; }

        [SMCSize(SMCSize.Grid6_24)]
        [SMCValueEmpty("-")]
        [SMCConditionalDisplay(nameof(ExibirNomeSocial), SMCConditionalOperation.Equals, true)]
        public string NomeSocial { get; set; }

        [SMCSize(SMCSize.Grid3_24)]
        public Sexo Sexo { get; set; }

        [SMCSize(SMCSize.Grid4_24)]
        [SMCDateTimeMode(SMCDateTimeMode.Date)]
        public DateTime DataNascimento { get; set; }

        [SMCSize(SMCSize.Grid4_24)]
        [SMCValueEmpty("-")]
        public string DescricaoNacionalidade { get; set; }

        [SMCSize(SMCSize.Grid4_24)]
        [SMCValueEmpty("-")]
        public string Naturalidade { get; set; }

        [SMCSize(SMCSize.Grid4_24)]
        [SMCDateTimeMode(SMCDateTimeMode.DateTime)]
        [SMCValueEmpty("-")]
        public DateTime? DataInclusaoDadosPessoais { get; set; }

        [SMCSize(SMCSize.Grid5_24)]
        [SMCValueEmpty("-")]
        public string UsuarioInclusaoDadosPessoais { get; set; }

        public List<PessoaFiliacaoViewModel> Filiacao { get; set; }

        [SMCSize(SMCSize.Grid12_24)]
        public List<string> FiliacaoDisplay
        {
            get
            {
                return Filiacao?.Select(f => $"{SMCEnumHelper.GetDescription(f.TipoParentesco)} - {f.Nome}").ToList() ?? new List<string>();
            }
        }

        [SMCHidden]
        public TipoNacionalidade TipoNacionalidade { get; set; }

        [SMCHidden]
        public long CodigoPaisNacionalidade { get; set; }

        [SMCHidden]
        public string UfNaturalidade { get; set; }

        [SMCHidden]
        public bool DesabilitarTipoIdentidade { get; set; }

        [SMCRequired]
        [SMCSize(SMCSize.Grid12_24)]
        [SMCSelect(SortDirection = SMCSortDirection.Descending)]
        [SMCConditionalReadonly(nameof(DesabilitarTipoIdentidade), SMCConditionalOperation.Equals, true, PersistentValue = true, RuleName = "CRO1")]
        [SMCConditionalReadonly(nameof(CamposReadOnly), SMCConditionalOperation.Equals, true, PersistentValue = true, RuleName = "CRO2")]
        [SMCConditionalRule("CRO1 || CRO2")]
        public TipoIdentidade? TipoIdentidade { get; set; }

        [SMCSize(SMCSize.Grid5_24)]
        [SMCValueEmpty("-")]
        public string NumeroIdentidade { get; set; }

        [SMCSize(SMCSize.Grid5_24)]
        [SMCValueEmpty("-")]
        public string OrgaoEmissorIdentidade { get; set; }

        [SMCSize(SMCSize.Grid5_24)]
        [SMCValueEmpty("-")]
        public string UfIdentidade { get; set; }

        [SMCSize(SMCSize.Grid5_24)]
        [SMCValueEmpty("-")]
        public DateTime? DataExpedicaoIdentidade { get; set; }

        [SMCHidden]
        [SMCSize(SMCSize.Grid4_24)]
        [SMCValueEmpty("-")]
        [SMCConditionalDisplay(nameof(ExibirDadosIdentidade), SMCConditionalOperation.Equals, false)]
        public string TipoDocumento { get; set; }

        [SMCHidden]
        [SMCSize(SMCSize.Grid8_24)]
        [SMCValueEmpty("-")]
        [SMCConditionalDisplay(nameof(ExibirDadosIdentidade), SMCConditionalOperation.Equals, false)]
        public string NumeroDocumento { get; set; }

        [SMCSize(SMCSize.Grid5_24)]
        [SMCValueEmpty("-")]
        public string NumeroPassaporte { get; set; }

        [SMCHidden]
        public int? CodigoPaisEmissaoPassaporte { get; set; }

        [SMCSize(SMCSize.Grid5_24)]
        [SMCValueEmpty("-")]
        public string NomePaisEmissaoPassaporte { get; set; }

        [SMCSize(SMCSize.Grid5_24)]
        [SMCValueEmpty("-")]
        public DateTime? DataValidadePassaporte { get; set; }

        [SMCSize(SMCSize.Grid5_24)]
        [SMCValueEmpty("-")]
        public string NumeroIdentidadeEstrangeira { get; set; }

        #endregion

        #region DADOS DE FORMAÇÃO

        [SMCHidden]
        public long? SeqTipoDocumentoSolicitado { get; set; }

        [SMCHidden]
        public string DescricaoTipoDocumentoSolicitado { get; set; }

        [SMCSize(SMCSize.Grid4_24)]
        public string DescricaoNivelEnsino { get; set; }

        [SMCSize(SMCSize.Grid5_24)]
        [SMCValueEmpty("-")]
        public string DescricaoCurso { get; set; }

        [SMCSize(SMCSize.Grid5_24)]
        [SMCValueEmpty("-")]
        public string DescricaoCursoDocumento { get; set; }

        [SMCSize(SMCSize.Grid5_24)]
        [SMCValueEmpty("-")]
        public int? CodigoCursoOfertaLocalidade { get; set; }

        [SMCSize(SMCSize.Grid4_24)]
        [SMCValueEmpty("-")]
        public string DescricaoModalidade { get; set; }

        [SMCHidden]
        public int? NumeroVia { get; set; }

        [SMCSize(SMCSize.Grid4_24)]
        [SMCValueEmpty("-")]
        public string NumeroViaGerada
        {
            get { return NumeroVia.HasValue && NumeroVia != 0 ? $"{NumeroVia}°" : "-"; }
        }

        [SMCSize(SMCSize.Grid4_24)]
        [SMCValueEmpty("-")]
        public string DescricaoTitulacao { get; set; }

        [SMCHidden]
        public long? SeqGrauAcademico { get; set; }

        [SMCHidden]
        public string DescricaoGrauAcademicoSelecionado { get; set; }

        [SMCRequired]
        [SMCSize(SMCSize.Grid5_24)]
        [SMCSelect(nameof(TiposGrauAcademico))]
        [SMCConditionalReadonly(nameof(HabilitarCampoGrauAcademico), SMCConditionalOperation.Equals, true, PersistentValue = true)]
        public long? SeqGrauAcademicoSelecionado { get; set; }

        [SMCHidden]
        [SMCSize(SMCSize.Grid5_24)]
        [SMCValueEmpty("-")]
        public string DescricaoFormaIngresso { get; set; }

        [SMCHidden]
        [SMCSize(SMCSize.Grid5_24)]
        public DateTime DataAdmissao { get; set; }

        [SMCHidden]
        public bool ExibirComandoDocumentacaoAcademica { get; set; }

        [SMCHidden]
        public bool ExibirComandoRVDD { get; set; }

        [SMCHidden]
        public bool ExibirMensagemDiplomaDigitalBachareladoLicenciatura { get; set; }

        [SMCConditionalDisplay(nameof(ExibirMensagemDiplomaDigitalBachareladoLicenciatura), SMCConditionalOperation.Equals, true)]
        [SMCDisplay]
        [SMCHideLabel]
        [SMCSize(SMCSize.Grid24_24)]
        public string MensagemDiplomaDigitalBachareladoLicenciatura => UIResource.MensagemDiplomaDigitalBachareladoLicenciatura;

        //[SMCHidden]
        //public bool ExibirMensagemDiplomaDigitalGrauAcademicoSemAssociacao { get; set; }

        //[SMCConditionalDisplay(nameof(ExibirMensagemDiplomaDigitalGrauAcademicoSemAssociacao), SMCConditionalOperation.Equals, true)]
        //[SMCDisplay]
        //[SMCHideLabel]
        //[SMCSize(SMCSize.Grid24_24)]
        //public string MensagemDiplomaDigitalGrauAcademicoSemAssociacao => UIResource.MensagemDiplomaDigitalGrauAcademicoSemAssociacao;

        [SMCHidden]
        public bool ExibirMensagemHistoricoEscolarNumeroViaMaiorUm { get; set; }

        [SMCConditionalDisplay(nameof(ExibirMensagemHistoricoEscolarNumeroViaMaiorUm), SMCConditionalOperation.Equals, true)]
        [SMCDisplay]
        [SMCHideLabel]
        [SMCSize(SMCSize.Grid24_24)]
        public string MensagemHistoricoEscolarNumeroViaMaiorUm => UIResource.MensagemHistoricoEscolarNumeroViaMaiorUm;

        [SMCHidden]
        public bool ExibirMensagemDiplomaPrimeiraViaComHistorico { get; set; }

        [SMCConditionalDisplay(nameof(ExibirMensagemDiplomaPrimeiraViaComHistorico), SMCConditionalOperation.Equals, true)]
        [SMCDisplay]
        [SMCHideLabel]
        [SMCSize(SMCSize.Grid24_24)]
        public string MensagemDiplomaPrimeiraViaComHistorico => UIResource.MensagemDiplomaPrimeiraViaComHistorico;

        [SMCHidden]
        public bool ExibirMensagemAutorizacaoNomeSocial { get; set; }

        [SMCConditionalDisplay(nameof(ExibirMensagemAutorizacaoNomeSocial), SMCConditionalOperation.Equals, true)]
        [SMCDisplay]
        [SMCHideLabel]
        [SMCSize(SMCSize.Grid24_24)]
        public string MensagemAutorizacaoNomeSocial => UIResource.MensagemAutorizacaoNomeSocial;

        public List<SolicitacaoAnaliseEmissaoDocumentoConclusaoFormacaoConcluidaViewModel> FormacoesEspecificasConcluidas { get; set; }

        public List<SolicitacaoAnaliseEmissaoDocumentoConclusaoFormacaoConcluidaViewModel> FormacoesEspecificasViasAnteriores { get; set; }

        #endregion

        #region CAMPOS DOCUMENTAÇÃO ACADÊMICA

        [SMCHidden]
        public bool ExibirSecaoDocumentacaoAcademica { get; set; }

        [SMCSize(SMCSize.Grid8_24)]
        [SMCConditionalRequired(nameof(ExibirSecaoDocumentacaoAcademica), SMCConditionalOperation.Equals, true)]
        [SMCConditionalReadonly(nameof(CamposReadOnly), SMCConditionalOperation.Equals, true, PersistentValue = true)]
        [SMCSelect]
        public FormatoHistoricoEscolar? FormatoHistoricoEscolar { get; set; }

        [SMCSize(SMCSize.Grid8_24)]
        [SMCDecimalDigits(2)]
        //[SMCMask("#0,00")]
        [SMCConditionalDisplay(nameof(FormatoHistoricoEscolar), SMCConditionalOperation.Equals, Academico.Common.Areas.CNC.Enums.FormatoHistoricoEscolar.NaoInformar, RuleName = "CD1")]
        [SMCConditionalDisplay(nameof(FormatoHistoricoEscolar), SMCConditionalOperation.Equals, Academico.Common.Areas.CNC.Enums.FormatoHistoricoEscolar.InformarSemMatrízCurricular, RuleName = "CD2")]
        [SMCConditionalRequired(nameof(FormatoHistoricoEscolar), SMCConditionalOperation.Equals, Academico.Common.Areas.CNC.Enums.FormatoHistoricoEscolar.NaoInformar, RuleName = "CR1")]
        [SMCConditionalRequired(nameof(FormatoHistoricoEscolar), SMCConditionalOperation.Equals, Academico.Common.Areas.CNC.Enums.FormatoHistoricoEscolar.InformarSemMatrízCurricular, RuleName = "CR2")]
        [SMCConditionalRule("CD1 || CD2")]
        [SMCConditionalRule("CR1 || CR2")]
        [SMCConditionalReadonly(nameof(CamposReadOnly), SMCConditionalOperation.Equals, true, PersistentValue = true)]
        public decimal? CargaHorariaCurso { get; set; }

        [SMCSize(SMCSize.Grid8_24)]
        [SMCDecimalDigits(2)]
        [SMCConditionalDisplay(nameof(FormatoHistoricoEscolar), SMCConditionalOperation.Equals, Academico.Common.Areas.CNC.Enums.FormatoHistoricoEscolar.InformarSemMatrízCurricular)]
        [SMCConditionalRequired(nameof(FormatoHistoricoEscolar), SMCConditionalOperation.Equals, Academico.Common.Areas.CNC.Enums.FormatoHistoricoEscolar.InformarSemMatrízCurricular)]
        [SMCConditionalReadonly(nameof(CamposReadOnly), SMCConditionalOperation.Equals, true, PersistentValue = true)]
        public decimal? CargaHorariaIntegralizada { get; set; }

        [SMCSize(SMCSize.Grid8_24)]
        [SMCMaxDateNow]
        [SMCConditionalDisplay(nameof(FormatoHistoricoEscolar), SMCConditionalOperation.Equals, Academico.Common.Areas.CNC.Enums.FormatoHistoricoEscolar.InformarSemMatrízCurricular)]
        [SMCConditionalRequired(nameof(FormatoHistoricoEscolar), SMCConditionalOperation.Equals, Academico.Common.Areas.CNC.Enums.FormatoHistoricoEscolar.InformarSemMatrízCurricular)]
        [SMCConditionalReadonly(nameof(CamposReadOnly), SMCConditionalOperation.Equals, true, PersistentValue = true)]
        public DateTime? DataEmissaoHistorico { get; set; }

        [SMCSize(SMCSize.Grid8_24)]
        [SMCMaxDateNow]
        [SMCConditionalDisplay(nameof(FormatoHistoricoEscolar), SMCConditionalOperation.Equals, Academico.Common.Areas.CNC.Enums.FormatoHistoricoEscolar.InformarSemMatrízCurricular)]
        [SMCConditionalReadonly(nameof(CamposReadOnly), SMCConditionalOperation.Equals, true, PersistentValue = true)]
        public DateTime? DataEnade { get; set; }

        [SMCSize(SMCSize.Grid8_24)]
        [SMCConditionalDisplay(nameof(FormatoHistoricoEscolar), SMCConditionalOperation.Equals, Academico.Common.Areas.CNC.Enums.FormatoHistoricoEscolar.InformarSemMatrízCurricular)]
        [SMCConditionalReadonly(nameof(CamposReadOnly), SMCConditionalOperation.Equals, true, PersistentValue = true)]
        public string SituacaoEnade { get; set; }

        [SMCConditionalDisplay(nameof(FormatoHistoricoEscolar), SMCConditionalOperation.Equals, Academico.Common.Areas.CNC.Enums.FormatoHistoricoEscolar.InformarComMatrizCurricular)]
        [SMCCssClass("smc-sga-mensagem-informativa smc-sga-mensagem")]
        [SMCHideLabel]
        [SMCSize(SMCSize.Grid24_24)]
        public string MensagemInformativaDocumentacao { get => UIResource.MSG_Dados_Matriz_Curricular; }

        [SMCDetail]
        [SMCSize(SMCSize.Grid24_24)]
        [SMCConditionalReadonly(nameof(CamposReadOnly), SMCConditionalOperation.Equals, true, PersistentValue = true)]
        public SMCMasterDetailList<SolicitacaoAnaliseEmissaoDocumentoConclusaoDocumentacaoViewModel> DocumentacaoComprobatoria { get; set; }

        #endregion

        #region CAMPOS DADOS DO REGISTRO

        [SMCHidden]
        public bool ExibirSecaoDadosRegistro { get; set; }

        [SMCHidden]
        public long? SeqViaAnterior { get; set; }

        [SMCHidden]
        public int? NumeroViaAnterior { get; set; }

        [SMCHidden]
        public string TokenTipoDocumentoAcademicoAnterior { get; set; }

        [SMCHidden]
        public string DescricaoTipoDocumentoAcademicoAnterior { get; set; }

        [SMCSize(SMCSize.Grid18_24)]
        [SMCValueEmpty("-")]
        public string DescricaoViaAnterior { get; set; }

        [SMCSize(SMCSize.Grid10_24)]
        [SMCConditionalRequired(nameof(ExibirSecaoDadosRegistro), SMCConditionalOperation.Equals, true)]
        [SMCConditionalReadonly(nameof(CamposReadOnly), SMCConditionalOperation.Equals, true, PersistentValue = true)]
        [SMCSelect]
        public bool? ReutilizarDados { get; set; }

        [SMCCssClass("smc-sga-mensagem-informativa smc-sga-mensagem")]
        [SMCHideLabel]
        [SMCSize(SMCSize.Grid24_24)]
        public string MensagemInformativaDadosRegistro { get => UIResource.MSG_Dados_Reutilizados_Via_Anterior; }

        #endregion

        #region CAMPOS INFORMAÇÕES ADICIONAIS

        [SMCHidden]
        public bool HabilitarBotaoNovaMensagem { get; set; }

        [SMCHidden]
        public string TokenTipoDocumentoAcademico { get; set; }

        [SMCHidden]
        public long SeqInstituicaoNivel { get; set; }

        [SMCHidden]
        public string NomePais { get; set; }

        [SMCHidden]
        public string DescricaoViaAnteriorMensagem { get; set; }

        [SMCHidden]
        public string DescricaoViaAtual { get; set; }

        [SMCHidden]
        [SMCConditionalDisplay(nameof(ExibirReconhecimentoCurso), SMCConditionalOperation.Equals, true)]
        [SMCSize(SMCSize.Grid24_24)]
        [SMCValueEmpty("-")]
        public string ReconhecimentoCurso { get; set; }

        [SMCSize(SMCSize.Grid8_24)]
        [SMCValueEmpty("Não se aplica")]
        public DateTime? DataDefesa { get; set; }

        [SMCSize(SMCSize.Grid16_24)]
        [SMCValueEmpty("Não se aplica")]
        public string Cotutela { get; set; }

        #endregion

        public bool ExibirBotaoSincronizarDadosAluno
        {
            get { return TokenNivelEnsino == TOKEN_NIVEL_ENSINO.GRADUACAO; }
        }

        public bool TokenNivelMestradoDoutorado
        {
            get
            {
                return TokenNivelEnsino == TOKEN_NIVEL_ENSINO.MESTRADO_ACADEMICO || TokenNivelEnsino == TOKEN_NIVEL_ENSINO.MESTRADO_PROFISSIONAL ||
                       TokenNivelEnsino == TOKEN_NIVEL_ENSINO.DOUTORADO_ACADEMICO || TokenNivelEnsino == TOKEN_NIVEL_ENSINO.DOUTORADO_PROFISSIONAL;
            }
        }

        public bool ExibirDadosIdentidade
        {
            get { return !string.IsNullOrEmpty(NumeroIdentidade); }
        }

        public bool ExibirReconhecimentoCurso { get; set; }

        public bool CamposReadOnly { get; set; }
    }
}