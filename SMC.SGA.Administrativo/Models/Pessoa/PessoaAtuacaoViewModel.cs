using SMC.Academico.Common.Areas.PES.Enums;
using SMC.Academico.Common.Constants;
using SMC.Academico.ServiceContract.Areas.PES.Interfaces;
using SMC.Academico.UI.Mvc.Areas.PES.Lookups;
using SMC.DadosMestres.Common.Areas.PES.Enums;
using SMC.Framework;
using SMC.Framework.DataAnnotations;
using SMC.Framework.Mapper;
using SMC.Framework.Model;
using SMC.Framework.UI.Mvc.DataAnnotations;
using SMC.Framework.UI.Mvc.Dynamic;
using SMC.Framework.UI.Mvc.Html;
using SMC.Framework.Util;
using SMC.Localidades.Common.Constants;
using SMC.Localidades.ServiceContract.Areas.LOC.Interfaces;
using SMC.SGA.Administrativo.App_GlobalResources;
using SMC.SGA.Administrativo.Areas.PES.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SMC.SGA.Administrativo.Models
{
    public abstract class PessoaAtuacaoViewModel : SMCDynamicViewModel, ISMCMappable
    {
        #region [ Parâmetros ]

        [SMCKey]
        [SMCOrder(2)]
        [SMCReadOnly]
        [SMCSize(SMCSize.Grid4_24, SMCSize.Grid24_24, SMCSize.Grid14_24, SMCSize.Grid5_24)]
        [SMCStep(1, 0)]
        public override long Seq { get; set; }

        [SMCHidden]
        [SMCParameter(FILTER.INSTITUICAO_ENSINO, true)]
        [SMCStep(3, 0)]
        public long SeqInstituicaoEnsino { get; set; }

        [SMCHidden]
        [SMCStep(0)]
        public long? SeqUsuarioSAS { get; set; }

        [SMCHidden]
        [SMCStep(0)]
        public long SeqPessoaDadosPessoais { get; set; }

        public abstract TipoAtuacao TipoAtuacao { get; }

        [SMCIgnoreProp]
        public abstract object BuscaPessoaExistenteRouteValues { get; }

        #endregion [ Parâmetros ]

        #region [ DataSources ]

        [SMCDataSource(nameof(PaisesNaturalidade), "Codigo", "Nome")]
        [SMCInclude(ignore: true)]
        [SMCServiceReference(typeof(ILocalidadeService), nameof(ILocalidadeService.BuscarPaisesValidosCorreios))]
        public List<SMCDatasourceItem> PaisesNaturalidade { get; set; }

        [SMCDataSource(nameof(PaisesPassaporte), "Codigo", "Nome")]
        [SMCInclude(ignore: true)]
        [SMCServiceReference(typeof(ILocalidadeService), nameof(ILocalidadeService.BuscarPaisesValidosCorreios))]
        public List<SMCDatasourceItem> PaisesPassaporte { get; set; }

        [SMCDataSource(nameof(UfsNaturalidade), "Codigo", "Nome")]
        [SMCInclude(ignore: true)]
        [SMCServiceReference(typeof(ILocalidadeService), nameof(ILocalidadeService.BuscarUfs))]
        public List<SMCSelectListItem> UfsNaturalidade { get; set; }

        [SMCDataSource(nameof(UfsIdentidade), "Codigo", "Nome")]
        [SMCInclude(ignore: true)]
        [SMCServiceReference(typeof(ILocalidadeService), nameof(ILocalidadeService.BuscarUfs))]
        public List<SMCSelectListItem> UfsIdentidade { get; set; }

        [SMCDataSource(nameof(UfsTitulo), "Codigo", "Nome")]
        [SMCInclude(ignore: true)]
        [SMCServiceReference(typeof(ILocalidadeService), nameof(ILocalidadeService.BuscarUfs))]
        public List<SMCSelectListItem> UfsTitulo { get; set; }

        [SMCDataSource(nameof(UfsMilitar), "Codigo", "Nome")]
        [SMCInclude(ignore: true)]
        [SMCServiceReference(typeof(ILocalidadeService), nameof(ILocalidadeService.BuscarUfs))]
        public List<SMCSelectListItem> UfsMilitar { get; set; }

        [SMCDataSource(nameof(Cidades), "Codigo", "Nome")]
        [SMCInclude(ignore: true)]
        [SMCServiceReference(typeof(ILocalidadeService), nameof(ILocalidadeService.BuscarCidadesPorUF),
            values: new[] { nameof(UfDataSource) })]
        public List<SMCSelectListItem> Cidades { get; set; }

        [SMCDataSource]
        [SMCMapForceFromTo]
        public List<TipoEnderecoEletronico> TiposEnderecoEletronico { get; set; }

        //FIX: Remover SMCSelect ao modificar a rotina de geração de datasources do dynamic para considerar datasources sem template
        [SMCDataSource(StorageType = SMCStorageType.TempData)]
        [SMCSelect(nameof(EnderecosCorrespondencia))]
        [SMCServiceReference(typeof(IPessoaEnderecoService),
            nameof(IPessoaEnderecoService.BuscarEnderecosCorrespondenciaSelect),
            values: new[] { nameof(TipoAtuacao) })]
        public List<SMCDatasourceItem> EnderecosCorrespondencia { get; set; }

        #endregion [ DataSources ]

        #region [ Mensagens ]

        /// <summary>
        /// Mensagem passo 1 - Identificação
        /// </summary>
        [SMCCssClass("smc-sga-msg-instrucao-cadastro smc-sga-msg-identificacao")]
        [SMCDisplay]
        [SMCStep(0, -1)]
        public string MensagemIdentificacaoPessoaExistente { get => UIResource.Label_Identificacao_Pessoa_Existente; }

        /// <summary>
        /// Mensagem passo 2 - Dados Pessoais
        /// </summary>
        [SMCCssClass("smc-sga-msg-instrucao-cadastro smc-sga-msg-dados")]
        [SMCDisplay]
        [SMCOrder(0)]
        [SMCStep(1, -1)]
        public string MensagemStep2 { get => UIResource.Label_Dados_Pessoais; }

        /// <summary>
        /// Mensagem passo 3 - Contatos
        /// </summary>
        [SMCCssClass("smc-sga-msg-instrucao-cadastro smc-sga-msg-contatos")]
        [SMCDisplay]
        [SMCOrder(0)]
        [SMCStep(2, -1)]
        public string MensagemStep3 { get => string.Format(UIResource.Label_Contatos, SMCEnumHelper.GetDescription(TipoAtuacao).ToLower()); }

        #endregion [ Mensagens ]

        #region [ Identificação Wizard0 ]

        [SMCHidden]
        [SMCStep(0)]
        public bool IdentificacaoPessoa => true;

        [SMCIgnoreProp(SMCViewMode.Edit | SMCViewMode.ReadOnly)]
        //[SMCOrder(1)]
        [SMCConditionalRequired(nameof(IdentificacaoPessoa), true)]
        [SMCSelect]
        [SMCSize(SMCSize.Grid5_24, SMCSize.Grid24_24, SMCSize.Grid8_24, SMCSize.Grid6_24)]
        [SMCStep(0)]
        public virtual TipoNacionalidade IdentificacaoTipoNacionalidade { get; set; }

        [SMCIgnoreProp(SMCViewMode.Edit | SMCViewMode.ReadOnly)]
        [SMCMaxLength(100)]
        //[SMCOrder(2)]
        [SMCConditionalRequired(nameof(IdentificacaoPessoa), true)]
        [SMCSize(SMCSize.Grid10_24, SMCSize.Grid24_24, SMCSize.Grid16_24, SMCSize.Grid10_24)]
        [SMCStep(0)]
        public string IdentificacaoNome { get; set; }

        [SMCIgnoreProp(SMCViewMode.Edit | SMCViewMode.ReadOnly)]
        [SMCConditionalRequired(nameof(PermitirAlterarDadosPessoaAtuacao), SMCConditionalOperation.Equals, true)]
        [SMCMaxDate(SMCUnitOfTime.Day, -1)]
        //[SMCOrder(3)]
        [SMCSize(SMCSize.Grid5_24, SMCSize.Grid24_24, SMCSize.Grid8_24, SMCSize.Grid4_24)]
        [SMCStep(0)]
        public DateTime? IdentificacaoDataNascimento { get; set; }

        [SMCConditionalDisplay(nameof(IdentificacaoTipoNacionalidade), SMCConditionalOperation.NotEqual, TipoNacionalidade.Estrangeira)]
        [SMCConditionalReadonly(nameof(IdentificacaoTipoNacionalidade), SMCConditionalOperation.Equals, TipoNacionalidade.Estrangeira)]
        [SMCConditionalRequired(nameof(IdentificacaoTipoNacionalidade), SMCConditionalOperation.NotEqual, TipoNacionalidade.Estrangeira)]
        [SMCCpf]
        [SMCIgnoreProp(SMCViewMode.Edit | SMCViewMode.ReadOnly)]
        //[SMCOrder(4)]
        [SMCSize(SMCSize.Grid4_24, SMCSize.Grid24_24, SMCSize.Grid8_24, SMCSize.Grid4_24)]
        [SMCStep(0)]
        public string IdentificacaoCpf { get; set; }

        [SMCConditionalDisplay(nameof(IdentificacaoTipoNacionalidade), SMCConditionalOperation.Equals, TipoNacionalidade.Estrangeira)]
        [SMCConditionalReadonly(nameof(IdentificacaoTipoNacionalidade), SMCConditionalOperation.NotEqual, TipoNacionalidade.Estrangeira)]
        //[SMCConditionalRequired(nameof(IdentificacaoTipoNacionalidade), SMCConditionalOperation.Equals, TipoNacionalidade.Estrangeira)]
        [SMCIgnoreProp(SMCViewMode.Edit | SMCViewMode.ReadOnly)]
        //[SMCOrder(5)]
        [SMCSize(SMCSize.Grid4_24)]
        [SMCStep(0)]
        public string IdentificacaoNumeroPassaporte { get; set; }

        public bool PessoaSelecionada { get; set; }

        #endregion [ Identificação Wizard0 ]

        #region [ Seleção Wizard0 ]

        [SMCHidden]
        public bool? UtilizarMesmaPessoa { get; set; }

        /// <summary>
        /// Setado quando for encontrada uma pessoa com o mesmo cpf e outro nome ou data de nascimento
        /// </summary>
        [SMCHidden]
        public bool PessoaLocalizadaComMesmoDocumento { get; set; }

        [SMCHidden]
        public SMCPagerModel<PessoaExistenteListaViewModel> PessoasExistentes { get; set; }

        //FIX: Remover ao corrigir o método Visualizar do dynamic para configrar os mestres detalhe como readonly
        [SMCIgnoreProp(SMCViewMode.Edit | SMCViewMode.ReadOnly)]
        public bool CadastrarNovaPessoa { get; set; }

        [SMCHidden]
        public long? SelectedValues { get; set; }

        [SMCHidden]
        [SMCStep(2, 1)]
        public long SeqPessoa { get; set; }

        #endregion [ Seleção Wizard0 ]

        #region [ Dados Pessoais Tab0 Wizard2 ]

        [SMCImage(thumbnailHeight: 100, thumbnailWidth: 0, manualUpload: false, maxFileSize: 225651471, AreaDownload = "", ActionDownload = "DownloadFileGuid", ControllerDownload = "Home")]
        [SMCInclude("DadosPessoais")]
        [SMCMapProperty("DadosPessoais.ArquivoFoto")]
        [SMCOrder(1)]
        //TODO: Voltar para 6 quando o css .scm-upload não tiver margem
        //[SMCSize(SMCSize.Grid6_24)]
        [SMCSize(SMCSize.Grid6_24, SMCSize.Grid24_24, SMCSize.Grid10_24, SMCSize.Grid5_24)]
        [SMCStep(1, 0)]
        public SMCUploadFile ArquivoFoto { get; set; }

        [SMCHidden]
        [SMCStep(1, 0)]
        public long? SeqArquivoFoto { get; set; }

        [SMCMaxLength(100)]
        [SMCOrder(3)]
        [SMCRegularExpression(REGEX.NOME)]
        [SMCRequired]
        [SMCSize(SMCSize.Grid14_24, SMCSize.Grid24_24, SMCSize.Grid14_24, SMCSize.Grid14_24)]
        [SMCStep(1, 0)]
        public string Nome { get; set; }

        [SMCConditionalDisplay(nameof(PermitirAlterarDadosPessoaAtuacaoNomeSocial), SMCConditionalOperation.Equals, true)]
        [SMCMaxLength(100)]
        [SMCOrder(4)]
        [SMCRegularExpression(REGEX.NOME)]
        [SMCSize(SMCSize.Grid13_24, SMCSize.Grid24_24, SMCSize.Grid14_24, SMCSize.Grid11_24)]
        [SMCStep(1, 0)]
        public string NomeSocial { get; set; }

        [SMCConditionalDisplay(nameof(PermitirAlterarDadosPessoaAtuacaoNomeSocial), SMCConditionalOperation.Equals, false)]
        [SMCReadOnly]
        [SMCMaxLength(100)]
        [SMCOrder(4)]
        [SMCRegularExpression(REGEX.NOME)]
        [SMCSize(SMCSize.Grid13_24, SMCSize.Grid24_24, SMCSize.Grid14_24, SMCSize.Grid11_24)]
        [SMCStep(1, 0)]
        public string NomeSocialReadOnly { get; set; }

        [SMCConditionalDisplay(nameof(PermitirAlterarDadosPessoaAtuacao), SMCConditionalOperation.Equals, true)]
        [SMCConditionalRequired(nameof(PermitirAlterarDadosPessoaAtuacao), SMCConditionalOperation.Equals, true, RuleName = "Rule1")]
        [SMCConditionalRequired(nameof(RetirarObrigatoridadeParaEstrangeiro), SMCConditionalOperation.Equals, true, RuleName = "Rule2")]
        [SMCConditionalRule("Rule1 && Rule2")]
        [SMCMaxDate(SMCUnitOfTime.Day, -1)]
        [SMCOrder(5)]
        [SMCSize(SMCSize.Grid5_24, SMCSize.Grid24_24, SMCSize.Grid8_24, SMCSize.Grid4_24)]
        [SMCStep(1, 0)]
        public DateTime? DataNascimento { get; set; }

        [SMCConditionalDisplay(nameof(PermitirAlterarDadosPessoaAtuacao), SMCConditionalOperation.Equals, false)]
        [SMCConditionalRequired(nameof(PermitirAlterarDadosPessoaAtuacao), SMCConditionalOperation.Equals, false, RuleName = "Rule1")]
        [SMCConditionalRequired(nameof(RetirarObrigatoridadeParaEstrangeiro), SMCConditionalOperation.Equals, true, RuleName = "Rule2")]
        [SMCConditionalRule("Rule1 && Rule2")]
        [SMCOrder(5)]
        [SMCReadOnly]
        [SMCSize(SMCSize.Grid5_24, SMCSize.Grid24_24, SMCSize.Grid8_24, SMCSize.Grid4_24)]
        [SMCStep(1, 0)]
        public DateTime? DataNascimentoReadOnly { get; set; }

        [SMCOrder(6)]
        [SMCRequired]
        [SMCSelect]
        [SMCSize(SMCSize.Grid3_24, SMCSize.Grid24_24, SMCSize.Grid8_24, SMCSize.Grid4_24)]
        [SMCStep(1, 0)]
        public bool Falecido { get; set; }

        [SMCOrder(7)]
        [SMCSelect]
        [SMCSize(SMCSize.Grid4_24, SMCSize.Grid24_24, SMCSize.Grid8_24, SMCSize.Grid6_24)]
        [SMCStep(1, 0)]
        public EstadoCivil? EstadoCivil { get; set; }

        [SMCStep(1, 0)]
        [SMCHidden]
        [SMCDependency(nameof(TipoNacionalidade), nameof(PessoaController.RetirarObrigatoridadeParaEstrangeiro), "Pessoa", "PES", false, includedProperties: new[] { nameof(TipoAtuacao), nameof(TipoAtuacaoAuxiliar) })]
        public bool RetirarObrigatoridadeParaEstrangeiro => PessoaController.RecuperarObrigatoridadeParaEstrangeiro(TipoNacionalidade, TipoAtuacao, TipoAtuacaoAuxiliar);

        [SMCOrder(8)]
        [SMCRequired]
        [SMCSelect]
        [SMCSize(SMCSize.Grid3_24, SMCSize.Grid24_24, SMCSize.Grid8_24, SMCSize.Grid5_24)]
        [SMCStep(1, 0)]
        public Sexo Sexo { get; set; }

        [SMCOrder(9)]
        [SMCConditionalRequired(nameof(RetirarObrigatoridadeParaEstrangeiro), SMCConditionalOperation.Equals, true)]
        [SMCSelect(SortBy = SMCSortBy.Value)]
        [SMCSize(SMCSize.Grid3_24, SMCSize.Grid24_24, SMCSize.Grid8_24, SMCSize.Grid4_24)]
        [SMCStep(1, 0)]
        public RacaCor? RacaCor { get; set; }

        [SMCConditionalDisplay(nameof(PermitirAlterarDadosPessoaAtuacao), SMCConditionalOperation.Equals, true)]
        [SMCConditionalRequired(nameof(TipoNacionalidade), SMCConditionalOperation.NotEqual, TipoNacionalidade.Estrangeira)]
        [SMCCpf]
        [SMCOrder(10)]
        [SMCSize(SMCSize.Grid5_24, SMCSize.Grid24_24, SMCSize.Grid8_24, SMCSize.Grid4_24)]
        [SMCStep(1, 0)]
        public string Cpf { get; set; }

        [SMCConditionalDisplay(nameof(PermitirAlterarDadosPessoaAtuacao), SMCConditionalOperation.Equals, false)]
        [SMCReadOnly]
        [SMCConditionalRequired(nameof(TipoNacionalidade), SMCConditionalOperation.NotEqual, TipoNacionalidade.Estrangeira)]
        [SMCCpf]
        [SMCOrder(10)]
        [SMCSize(SMCSize.Grid5_24, SMCSize.Grid24_24, SMCSize.Grid8_24, SMCSize.Grid4_24)]
        [SMCStep(1, 0)]
        public string CpfReadOnly { get; set; }

        [SMCConditionalDisplay(nameof(PermitirAlterarDadosPessoaAtuacao), SMCConditionalOperation.Equals, true)]
        [SMCGroupedProperty("Nacionalidade")]
        [SMCOrder(11)]
        [SMCRequired]
        [SMCSelect]
        [SMCSize(SMCSize.Grid12_24)]
        [SMCStep(1, 0)]
        public TipoNacionalidade TipoNacionalidade { get; set; }

        [SMCConditionalDisplay(nameof(PermitirAlterarDadosPessoaAtuacao), SMCConditionalOperation.Equals, false)]
        [SMCConditionalRequired(nameof(PermitirAlterarDadosPessoaAtuacao), SMCConditionalOperation.Equals, false)]
        [SMCReadOnly]
        [SMCGroupedProperty("Nacionalidade")]
        [SMCOrder(11)]
        [SMCSelect]
        [SMCSize(SMCSize.Grid12_24)]
        [SMCStep(1, 0)]
        public TipoNacionalidade TipoNacionalidadeReadOnly { get; set; }

        [SMCConditionalDisplay(nameof(PermitirAlterarDadosPessoaAtuacao), SMCConditionalOperation.Equals, true)]
        //[SMCConditionalReadonly(nameof(TipoNacionalidade), SMCConditionalOperation.Equals, TipoNacionalidade.Brasileira, PersistentValue = true)]
        [SMCConditionalRequired(nameof(PermitirAlterarDadosPessoaAtuacao), SMCConditionalOperation.Equals, true)]
        [SMCDependency(nameof(TipoNacionalidade), nameof(PessoaController.BuscarPaisesPorTipoNacionalidade), "Pessoa", "PES", true)]
        [SMCGroupedProperty("Nacionalidade")]
        [SMCOrder(12)]
        [SMCSelect(nameof(PaisesNaturalidade), AutoSelectSingleItem = true)]
        [SMCSize(SMCSize.Grid12_24)]
        [SMCStep(1, 0)]
        public int CodigoPaisNacionalidade { get; set; }

        [SMCConditionalDisplay(nameof(PermitirAlterarDadosPessoaAtuacao), SMCConditionalOperation.Equals, false)]
        [SMCConditionalRequired(nameof(PermitirAlterarDadosPessoaAtuacao), SMCConditionalOperation.Equals, false)]
        [SMCGroupedProperty("Nacionalidade")]
        [SMCOrder(12)]
        [SMCReadOnly]
        [SMCSelect(nameof(PaisesNaturalidade), AutoSelectSingleItem = true)]
        [SMCSize(SMCSize.Grid12_24)]
        [SMCStep(1, 0)]
        public int CodigoPaisNacionalidadeReadOnly { get; set; }

        [SMCConditionalDisplay(nameof(CodigoPaisNacionalidade), SMCConditionalOperation.Equals, LocalidadesDefaultValues.SEQ_PAIS_BRASIL)]
        [SMCGroupedProperty("Naturalidade")]
        [SMCOrder(13)]
        [SMCSelect(nameof(UfsNaturalidade))]
        [SMCSize(SMCSize.Grid10_24)]
        [SMCStep(1, 0)]
        public string UfNaturalidade { get; set; }

        [SMCIgnoreProp]
        public string UfDataSource { get { return this.UfNaturalidade ?? "MG"; } }

        [SMCConditionalDisplay(nameof(CodigoPaisNacionalidade), SMCConditionalOperation.Equals, LocalidadesDefaultValues.SEQ_PAIS_BRASIL)]
        [SMCConditionalRequired(nameof(UfNaturalidade), SMCConditionalOperation.NotEqual, "")]
        [SMCDependency(nameof(UfNaturalidade), "BuscarCidadesPorUF", "Pessoa", "PES", required: true)]
        [SMCGroupedProperty("Naturalidade")]
        [SMCOrder(14)]
        [SMCSelect(nameof(Cidades))]
        [SMCSize(SMCSize.Grid14_24)]
        [SMCStep(1, 0)]
        public int? CodigoCidadeNaturalidade { get; set; }

        [SMCConditionalDisplay(nameof(CodigoPaisNacionalidade), SMCConditionalOperation.NotEqual, LocalidadesDefaultValues.SEQ_PAIS_BRASIL)]
        [SMCGroupedProperty("Naturalidade")]
        [SMCMaxLength(100)]
        [SMCOrder(15)]
        [SMCSize(SMCSize.Grid24_24)]
        [SMCStep(1, 0)]
        public string DescricaoNaturalidadeEstrangeira { get; set; }

        [SMCDependency(nameof(TipoNacionalidade), nameof(PessoaController.ValidarObrigatoriedadePassaporte), "Pessoa", "PES", false, includedProperties: new[] { nameof(NumeroPassaporte), nameof(DataValidadePassaporte), nameof(CodigoPaisEmissaoPassaporte), nameof(TipoAtuacao), nameof(TipoAtuacaoAuxiliar) })]
        [SMCDependency(nameof(NumeroPassaporte), nameof(PessoaController.ValidarObrigatoriedadePassaporte), "Pessoa", "PES", false, includedProperties: new[] { nameof(TipoNacionalidade), nameof(DataValidadePassaporte), nameof(CodigoPaisEmissaoPassaporte), nameof(TipoAtuacao), nameof(TipoAtuacaoAuxiliar) })]
        [SMCDependency(nameof(DataValidadePassaporte), nameof(PessoaController.ValidarObrigatoriedadePassaporte), "Pessoa", "PES", false, includedProperties: new[] { nameof(TipoNacionalidade), nameof(NumeroPassaporte), nameof(CodigoPaisEmissaoPassaporte), nameof(TipoAtuacao), nameof(TipoAtuacaoAuxiliar) })]
        [SMCDependency(nameof(CodigoPaisEmissaoPassaporte), nameof(PessoaController.ValidarObrigatoriedadePassaporte), "Pessoa", "PES", false, includedProperties: new[] { nameof(TipoNacionalidade), nameof(NumeroPassaporte), nameof(DataValidadePassaporte), nameof(TipoAtuacao), nameof(TipoAtuacaoAuxiliar) })]
        [SMCHidden]
        [SMCStep(1, 0)]
        public bool ObrigatoriedadePassaporte => PessoaController.RecuperarObrigatoriedadePassaporte(TipoNacionalidade, NumeroPassaporte, DataValidadePassaporte, CodigoPaisEmissaoPassaporte, TipoAtuacao, TipoAtuacaoAuxiliar);


        //Propriedade usada para validação de obrigatoriedade, uma vez que o TipoAtuacao da tela está sendo enviado sempre como null no cadastro, apesar de ser preenchido
        [SMCGroupedProperty("DocumentoPassaporte")]
        [SMCHidden]
        [SMCStep(1, 0)]
        public TipoAtuacao TipoAtuacaoAuxiliar { get; set; }

        [SMCConditionalRequired(nameof(ObrigatoriedadePassaporte), true)]
        [SMCGroupedProperty("DocumentoPassaporte")]
        [SMCMaxLength(255)]
        [SMCOrder(16)]
        [SMCSize(SMCSize.Grid15_24, SMCSize.Grid24_24, SMCSize.Grid24_24, SMCSize.Grid9_24)]
        [SMCStep(1, 0)]
        public string NumeroPassaporte { get; set; }

        [SMCConditionalRequired(nameof(ObrigatoriedadePassaporte), true)]
        [SMCGroupedProperty("DocumentoPassaporte")]
        [SMCOrder(17)]
        [SMCSize(SMCSize.Grid9_24, SMCSize.Grid24_24, SMCSize.Grid24_24, SMCSize.Grid7_24)]
        [SMCStep(1, 0)]
        public DateTime? DataValidadePassaporte { get; set; }

        [SMCConditionalRequired(nameof(ObrigatoriedadePassaporte), true)]
        [SMCGroupedProperty("DocumentoPassaporte")]
        [SMCOrder(18)]
        [SMCSelect(nameof(PaisesPassaporte))]
        [SMCSize(SMCSize.Grid24_24, SMCSize.Grid24_24, SMCSize.Grid24_24, SMCSize.Grid8_24)]
        [SMCStep(1, 0)]
        public int? CodigoPaisEmissaoPassaporte { get; set; }

        [SMCGroupedProperty("DocumentoRg")]
        [SMCConditionalReadonly(nameof(TipoNacionalidade), TipoNacionalidade.Estrangeira)]
        [SMCMaxLength(255)]
        [SMCOrder(19)]
        [SMCSize(SMCSize.Grid12_24, SMCSize.Grid24_24, SMCSize.Grid12_24, SMCSize.Grid5_24)]
        [SMCStep(1, 0)]
        public string NumeroIdentidade { get; set; }

        [SMCGroupedProperty("DocumentoRg")]
        [SMCConditionalReadonly(nameof(TipoNacionalidade), TipoNacionalidade.Estrangeira)]
        [SMCMaxLength(10)]
        [SMCOrder(20)]
        [SMCSize(SMCSize.Grid12_24, SMCSize.Grid24_24, SMCSize.Grid12_24, SMCSize.Grid6_24)]
        [SMCStep(1, 0)]
        public string OrgaoEmissorIdentidade { get; set; }

        [SMCGroupedProperty("DocumentoRg")]
        [SMCConditionalReadonly(nameof(TipoNacionalidade), TipoNacionalidade.Estrangeira)]
        [SMCOrder(21)]
        [SMCSelect(nameof(UfsIdentidade))]
        [SMCSize(SMCSize.Grid12_24, SMCSize.Grid24_24, SMCSize.Grid24_24, SMCSize.Grid7_24)]
        [SMCStep(1, 0)]
        public string UfIdentidade { get; set; }

        [SMCGroupedProperty("DocumentoRg")]
        [SMCConditionalReadonly(nameof(TipoNacionalidade), TipoNacionalidade.Estrangeira)]
        [SMCMaxDate(SMCUnitOfTime.Day, -1)]
        [SMCOrder(22)]
        [SMCSize(SMCSize.Grid12_24, SMCSize.Grid24_24, SMCSize.Grid24_24, SMCSize.Grid6_24)]
        [SMCStep(1, 0)]
        public DateTime? DataExpedicaoIdentidade { get; set; }

        [SMCGroupedProperty("DocumentoIdentidadeEstrangeira")]
        [SMCConditionalReadonly(nameof(TipoNacionalidade), TipoNacionalidade.Brasileira)]
        [SMCOrder(23)]
        [SMCSize(SMCSize.Grid24_24, SMCSize.Grid24_24, SMCSize.Grid24_24, SMCSize.Grid24_24)]
        [SMCStep(1, 0)]
        [SMCMaxLength(255)]
        public string NumeroIdentidadeEstrangeira { get; set; }

        [SMCGroupedProperty("DocumentoCnh")]
        [SMCMaxLength(255)]
        [SMCOrder(24)]
        [SMCSize(SMCSize.Grid16_24, SMCSize.Grid24_24, SMCSize.Grid12_24, SMCSize.Grid16_24)]
        [SMCStep(1, 0)]
        public string NumeroRegistroCnh { get; set; }

        [SMCGroupedProperty("DocumentoCnh")]
        [SMCOrder(25)]
        [SMCSelect]
        [SMCSize(SMCSize.Grid8_24, SMCSize.Grid24_24, SMCSize.Grid12_24, SMCSize.Grid8_24)]
        [SMCStep(1, 0)]
        public CategoriaCnh? CategoriaCnh { get; set; }

        [SMCGroupedProperty("DocumentoCnh")]
        [SMCMaxDate(SMCUnitOfTime.Day, -1)]
        [SMCOrder(26)]
        [SMCSize(SMCSize.Grid12_24, SMCSize.Grid24_24, SMCSize.Grid24_24, SMCSize.Grid8_24)]
        [SMCStep(1, 0)]
        public DateTime? DataEmissaoCnh { get; set; }

        [SMCGroupedProperty("DocumentoCnh")]
        [SMCMinDate(nameof(DataEmissaoCnh))]
        [SMCOrder(27)]
        [SMCSize(SMCSize.Grid12_24, SMCSize.Grid24_24, SMCSize.Grid24_24, SMCSize.Grid8_24)]
        [SMCStep(1, 0)]
        public DateTime? DataVencimentoCnh { get; set; }

        [SMCGroupedProperty("DocumentoTituloEleitor")]
        [SMCMaxLength(255)]
        [SMCOrder(28)]
        [SMCSize(SMCSize.Grid16_24, SMCSize.Grid24_24, SMCSize.Grid24_24, SMCSize.Grid10_24)]
        [SMCStep(1, 0)]
        public string NumeroTituloEleitor { get; set; }

        [SMCGroupedProperty("DocumentoTituloEleitor")]
        [SMCMaxLength(255)]
        [SMCOrder(29)]
        [SMCSize(SMCSize.Grid8_24, SMCSize.Grid24_24, SMCSize.Grid24_24, SMCSize.Grid6_24)]
        [SMCStep(1, 0)]
        public string NumeroZonaTituloEleitor { get; set; }

        [SMCGroupedProperty("DocumentoTituloEleitor")]
        [SMCMaxLength(255)]
        [SMCOrder(30)]
        [SMCSize(SMCSize.Grid12_24, SMCSize.Grid24_24, SMCSize.Grid24_24, SMCSize.Grid8_24)]
        [SMCStep(1, 0)]
        public string NumeroSecaoTituloEleitor { get; set; }

        [SMCGroupedProperty("DocumentoTituloEleitor")]
        [SMCOrder(31)]
        [SMCSelect(nameof(UfsTitulo))]
        [SMCSize(SMCSize.Grid12_24, SMCSize.Grid24_24, SMCSize.Grid24_24, SMCSize.Grid10_24)]
        [SMCStep(1, 0)]
        public string UfTituloEleitor { get; set; }

        [SMCGroupedProperty("DocumentoPisPasep")]
        [SMCOrder(32)]
        [SMCSelect]
        [SMCSize(SMCSize.Grid8_24, SMCSize.Grid24_24, SMCSize.Grid12_24, SMCSize.Grid8_24)]
        [SMCStep(1, 0)]
        public TipoPisPasep? TipoPisPasep { get; set; }

        [SMCGroupedProperty("DocumentoPisPasep")]
        [SMCMaxLength(255)]
        [SMCOrder(33)]
        [SMCSize(SMCSize.Grid16_24, SMCSize.Grid24_24, SMCSize.Grid12_24, SMCSize.Grid16_24)]
        [SMCStep(1, 0)]
        public string NumeroPisPasep { get; set; }

        [SMCGroupedProperty("DocumentoPisPasep")]
        [SMCMaxDate(SMCUnitOfTime.Day, -1)]
        [SMCOrder(34)]
        [SMCSize(SMCSize.Grid8_24, SMCSize.Grid24_24, SMCSize.Grid12_24, SMCSize.Grid8_24)]
        [SMCStep(1, 0)]
        public DateTime? DataPisPasep { get; set; }

        [SMCGroupedProperty("DocumentoMilitar")]
        [SMCMaxLength(255)]
        [SMCOrder(35)]
        [SMCSize(SMCSize.Grid9_24, SMCSize.Grid24_24, SMCSize.Grid12_24, SMCSize.Grid9_24)]
        [SMCStep(1, 0)]
        public string NumeroDocumentoMilitar { get; set; }

        [SMCGroupedProperty("DocumentoMilitar")]
        [SMCMaxLength(50)]
        [SMCOrder(36)]
        [SMCSize(SMCSize.Grid15_24, SMCSize.Grid24_24, SMCSize.Grid12_24, SMCSize.Grid15_24)]
        [SMCStep(1, 0)]
        public string CsmDocumentoMilitar { get; set; }

        [SMCGroupedProperty("DocumentoMilitar")]
        [SMCOrder(37)]
        [SMCSelect]
        [SMCSize(SMCSize.Grid15_24, SMCSize.Grid24_24, SMCSize.Grid12_24, SMCSize.Grid15_24)]
        [SMCStep(1, 0)]
        public TipoDocumentoMilitar? TipoDocumentoMilitar { get; set; }

        [SMCGroupedProperty("DocumentoMilitar")]
        [SMCOrder(38)]
        [SMCSelect(nameof(UfsMilitar))]
        [SMCSize(SMCSize.Grid9_24, SMCSize.Grid24_24, SMCSize.Grid12_24, SMCSize.Grid9_24)]
        [SMCStep(1, 0)]
        public string UfDocumentoMilitar { get; set; }

        [SMCGroupedProperty("RegistroProfissional")]
        [SMCOrder(39)]
        [SMCSelect]
        [SMCSize(SMCSize.Grid15_24, SMCSize.Grid24_24, SMCSize.Grid12_24, SMCSize.Grid15_24)]
        [SMCStep(1, 0)]
        public TipoRegistroProfissional? TipoRegistroProfissional { get; set; }

        [SMCGroupedProperty("RegistroProfissional")]
        [SMCOrder(40)]
        [SMCSize(SMCSize.Grid9_24, SMCSize.Grid24_24, SMCSize.Grid12_24, SMCSize.Grid9_24)]
        [SMCStep(1, 0)]
        public string NumeroRegistroProfissional { get; set; }

        [SMCGroupedProperty("NecessiadesEspeciais")]
        [SMCOrder(41)]
        [SMCRadioButtonList]
        [SMCRequired]
        [SMCSize(SMCSize.Grid14_24, SMCSize.Grid24_24, SMCSize.Grid12_24, SMCSize.Grid14_24)]
        [SMCStep(1, 0)]
        public bool NecessidadeEspecial { get; set; }

        [SMCConditionalReadonly(nameof(NecessidadeEspecial), SMCConditionalOperation.Equals, false)]
        [SMCConditionalRequired(nameof(NecessidadeEspecial), SMCConditionalOperation.Equals, true)]
        [SMCGroupedProperty("NecessiadesEspeciais")]
        [SMCOrder(42)]
        [SMCSelect]
        [SMCSize(SMCSize.Grid7_24, SMCSize.Grid24_24, SMCSize.Grid12_24, SMCSize.Grid7_24)]
        [SMCStep(1, 0)]
        public TipoNecessidadeEspecial? TipoNecessidadeEspecial { get; set; }

        //FIX: Remover ao corrigir o método Visualizar do dynamic para configrar os mestres detalhe como readonly
        [SMCConditionalDisplay(nameof(PermitirAlterarDadosPessoaAtuacao), SMCConditionalOperation.Equals, true)]
        [SMCReadOnly(SMCViewMode.ReadOnly)]
        [SMCDetail]
        [SMCOrder(43)]
        [SMCSize(SMCSize.Grid24_24)]
        [SMCStep(1, 0)]
        public SMCMasterDetailList<PessoaFiliacaoViewModel> Filiacao { get; set; }

        [SMCConditionalDisplay(nameof(PermitirAlterarDadosPessoaAtuacao), SMCConditionalOperation.Equals, false)]
        [SMCReadOnly]
        [SMCDetail]
        [SMCOrder(44)]
        [SMCSize(SMCSize.Grid24_24)]
        [SMCStep(1, 0)]
        public SMCMasterDetailList<PessoaFiliacaoReadOnlyViewModel> FiliacaoReadOnly { get; set; }

        [SMCHidden]
        [SMCStep(1, 0)]
        public bool PermitirAlterarDadosPessoaAtuacao { get; set; }

        [SMCHidden]
        [SMCStep(1, 0)]
        public bool PermitirAlterarDadosPessoaAtuacaoNomeSocial { get; set; }

        #endregion [ Dados Pessoais Tab0 Wizard2 ]

        #region [ Contatos Tab1 Wizard3 ]

        [PessoaEnderecoLookup]
        [SMCDependency(nameof(SeqPessoa))]
        [SMCDependency(nameof(TipoAtuacao))]
        [SMCOrder(1)]
        [SMCSize(SMCSize.Grid24_24)]
        [SMCStep(2, 1)]
        public List<PessoaEnderecoLookupViewModel> Enderecos { get; set; }

        [PessoaTelefoneLookup]
        [SMCDependency(nameof(SeqPessoa))]
        [SMCOrder(2)]
        [SMCSize(SMCSize.Grid24_24, SMCSize.Grid24_24, SMCSize.Grid24_24, SMCSize.Grid12_24)]
        [SMCStep(2, 1)]
        public List<PessoaTelefoneLookupViewModel> Telefones { get; set; }

        [PessoaEnderecoEletronicoLookup]
        [SMCDependency(nameof(SeqPessoa))]
        [SMCOrder(3)]
        [SMCSize(SMCSize.Grid24_24, SMCSize.Grid24_24, SMCSize.Grid24_24, SMCSize.Grid12_24)]
        [SMCStep(2, 1)]
        public List<PessoaEnderecoEletronicoLookupViewModel> EnderecosEletronicos { get; set; }

        #endregion [ Contatos Tab1 Wizard3 ]

        #region [ Configuração ]

        public override void InitializeModel(SMCViewMode viewMode)
        {
            if (viewMode == SMCViewMode.Insert)
            {
                this.IdentificacaoTipoNacionalidade = TipoNacionalidade.Brasileira;
            }
        }

        public override void ConfigureDataSources()
        {
            this.TiposEnderecoEletronico = Enum.GetValues(typeof(TipoEnderecoEletronico))
                .OfType<TipoEnderecoEletronico>()
                .Where(w => w != TipoEnderecoEletronico.Nenhum)
                .ToList();
        }

        #endregion [ Configuração ]
    }
}