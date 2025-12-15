using SMC.Academico.Common.Areas.CSO.Constants;
using SMC.Academico.Common.Areas.ORG.Enums;
using SMC.Academico.Common.Constants;
using SMC.Academico.ServiceContract.Areas.CSO.Interfaces;
using SMC.Academico.ServiceContract.Areas.ORG.Interfaces;
using SMC.Academico.UI.Mvc.Areas.CSO.Lookups;
using SMC.Academico.UI.Mvc.Areas.ORG.InterfaceBlocks;
using SMC.Academico.UI.Mvc.Areas.ORG.Models;
using SMC.DadosMestres.Common.Areas.PES.Enums;
using SMC.EstruturaOrganizacional.UI.Mvc.Areas.ESO.Lookups;
using SMC.Framework;
using SMC.Framework.DataAnnotations;
using SMC.Framework.Mapper;
using SMC.Framework.Model;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.DataAnnotations;
using SMC.Framework.UI.Mvc.Dynamic;
using SMC.Framework.UI.Mvc.Html;
using SMC.Localidades.Common.Areas.LOC.Enums;
using SMC.Localidades.UI.Mvc.DataAnnotation;
using SMC.Localidades.UI.Mvc.Models;
using SMC.SGA.Administrativo.Areas.CSO.Controllers;
using SMC.SGA.Administrativo.Models;
using System;
using System.Collections.Generic;

namespace SMC.SGA.Administrativo.Areas.CSO.Models
{
    [SMCStepConfiguration]
    [SMCStepConfiguration(Partial = "_ClassificacoesEntidade")]
    [SMCStepConfiguration]
    [SMCStepConfiguration(Partial = "_AtoNormativo")]
    [SMCGroupedPropertyConfiguration(GroupId = "situacaoEntidade", Size = SMCSize.Grid24_24)]
    [SMCGroupedPropertyConfiguration(GroupId = "origemFinanceira", Size = SMCSize.Grid24_24)]
    public class CursoOfertaLocalidadeDynamicModel : EntidadeViewModel, IAtoNormativoBI
    {
        #region [ DataSources ]

        [SMCServiceReference(typeof(IInstituicaoTipoEntidadeService), nameof(IInstituicaoTipoEntidadeService.BuscarSituacoesTipoEntidadeDaInstituicaoSelect),
            values: new string[] { nameof(SeqTipoEntidade) })]
        [SMCDataSource]
        [SMCIgnoreProp]
        public List<SMCDatasourceItem> Situacoes { get; set; }        

        [SMCDataSource]
        [SMCIgnoreProp]
        [SMCServiceReference(typeof(ICursoOfertaLocalidadeService), nameof(ICursoOfertaLocalidadeService.BuscarModalidadesPorCursoUnidadeSelect),
            values: new string[] { nameof(SeqCursoUnidade) })]
        public List<SMCDatasourceItem> Modalidades { get; set; }

        [SMCDataSource]
        [SMCIgnoreProp]
        [SMCServiceReference(typeof(ICursoOfertaLocalidadeService), nameof(ICursoOfertaLocalidadeService.BuscarLocalidadesPorModalidadeSelect), values: new string[] { nameof(SeqInstituicaoNivel), nameof(SeqCursoUnidade), nameof(DesativarfiltrosHierarquia) })]
        public List<SMCDatasourceItem> Localidades { get; set; }

        [SMCDataSource]
        [SMCIgnoreProp]
        [SMCServiceReference(typeof(IInstituicaoNivelTurnoService), nameof(IInstituicaoNivelTurnoService.BuscarInstituicaoNivelTurnoSelect),
            values: new string[] { nameof(SeqInstituicaoNivel) })]
        public List<SMCDatasourceItem> TurnosInstituicaoNivel { get; set; }

        [SMCDataSource(SMCStorageType.None)]
        [SMCHidden]
        public List<SMCSelectListItem> OrgaoRegulador { get; set; }

        /// <summary>
        /// Dadasource armazenado no TempData para sobrescrever o datasource do componente de endereço
        /// </summary>
        [SMCDataSource(SMCStorageType.TempData)]
        [SMCMapForceFromTo]
        public List<TipoEndereco> TiposEnderecos { get; set; }

        [SMCDataSource(SMCStorageType.TempData)]
        [SMCMapForceFromTo]
        public List<SMCDatasourceItem<string>> TiposTelefone { get; set; }

        /// <summary>
        /// Datasource armazenado no TempData para manter o padrão mesmo sendo utilizado num componente interno
        /// </summary>
        [SMCDataSource(SMCStorageType.TempData)]
        [SMCMapForceFromTo]
        public List<SMCDatasourceItem<string>> TiposEnderecoEletronico { get; set; }

        [SMCDataSource]
        [SMCIgnoreProp]
        [SMCServiceReference(typeof(ICursoOfertaLocalidadeService), nameof(ICursoOfertaLocalidadeService.BuscarOrigensFinanceirasGRASelect))]
        public List<SMCDatasourceItem> OrigensFinanceiras { get; set; }

        #endregion [ DataSources ]

        #region [ Hidden ]

        //Utilizado pelo navigation group
        [SMCHidden]
        [SMCParameter]
        [SMCStep(0)]
        public long SeqEntidade { get; set; }

        //Utilizado pelo navigation group
        [SMCHidden]
        [SMCParameter]
        [SMCStep(0)]
        public override long SeqTipoEntidade { get; set; }

        [SMCHidden]
        [SMCStep(0)]
        [SMCParameter(FILTER.INSTITUICAO_ENSINO, true)]
        public override long SeqInstituicaoEnsino { get; set; }

        [SMCHidden]
        [SMCParameter("SeqEntidade")]
        [SMCStep(0)]
        public long SeqCursoUnidade { get; set; }

        [SMCHidden]
        [SMCStep(0)]
        //[SMCParameter("SeqCursoOferta")]
        public long SeqCurso { get; set; }

        [SMCHidden]
        [SMCStep(0)]
        public long SeqCursoID { get { return SeqCurso; } set { SeqCurso = value; } }

        [SMCHidden]
        public override long? SeqEntidadeResponsavel => SeqCurso;

        /// <summary>
        /// Sequencial do nível de ensino do curso associado
        /// </summary>
        [SMCHidden]
        [SMCStep(0)]
        public long SeqNivelEnsino { get; set; }

        /// <summary>
        /// Sequencial do instituição nível de ensino do curso associado
        /// </summary>
        [SMCHidden]
        [SMCStep(0)]
        public long SeqInstituicaoNivel { get; set; }

        /// <summary>
        /// Sequencial da situação do curso associado
        /// </summary>
        [SMCHidden]
        [SMCStep(0)]
        public long SeqSituacaoAtual { get; set; }

        /// <summary>
        /// Sequencial dos HierarquiaItem que representam as entidades responsáveis pelo curso
        /// </summary>
        [SMCHidden]
        [SMCStep(0)]
        public List<long> SeqsEntidadesResponsaveis { get; set; }

        [SMCHidden]
        [SMCStep(0)]
        public long SeqTipoEntidadeCursoOfertaLocalidade { get; set; }

        [SMCHidden]
        [SMCStep(0)]
        public bool DesativarfiltrosHierarquia { get; set; } = true;

        [SMCHidden]
        [SMCStep(2)]
        public bool HabilitaEnderecos { get; set; }

        [SMCHidden]
        [SMCStep(2)]
        public bool HabilitaTelefones { get; set; }

        [SMCHidden]
        [SMCStep(2)]
        public bool HabilitaEnderecosEletronicos { get; set; }

        [SMCHidden]
        [SMCStep(1,0)]
        public bool RestricaoCursoOferta { get; set; }

        #endregion [ Hidden ]

        #region [ Aba0 Dados Gerais ]

        [SMCKey]
        [SMCOrder(2)]
        [SMCReadOnly]
        [SMCSize(SMCSize.Grid4_24, SMCSize.Grid24_24, SMCSize.Grid6_24, SMCSize.Grid3_24)]
        [SMCStep(0)]
        public override long Seq { get; set; }

        [SMCDependency(nameof(SeqCursoOferta), nameof(CursoOfertaLocalidadeController.RecuperarMascaraCursoOfertaLocalidade), "CursoOfertaLocalidade", false, nameof(SeqLocalidade), nameof(Nome))]
        [SMCDependency(nameof(SeqLocalidade), nameof(CursoOfertaLocalidadeController.RecuperarMascaraCursoOfertaLocalidade), "CursoOfertaLocalidade", false, nameof(SeqCursoOferta), nameof(Nome))]
        [SMCDescription]
        [SMCMaxLength(100)]
        [SMCOrder(4)]
        [SMCRequired]
        [SMCSize(SMCSize.Grid16_24, SMCSize.Grid24_24, SMCSize.Grid18_24, SMCSize.Grid17_24)]
        [SMCStep(1, 0)]
        [SMCConditionalReadonly(nameof(RestricaoCursoOferta), SMCConditionalOperation.Equals, false, PersistentValue = true)]
        public override string Nome { get; set; }

        [SMCOrder(5)]
        [SMCStep(1, 0)]
        [SMCSize(SMCSize.Grid4_24, SMCSize.Grid24_24, SMCSize.Grid6_24, SMCSize.Grid4_24)]
        [SMCMaxLength(15)]
        [SMCConditionalDisplay(nameof(SiglaVisivel), true)]
        [SMCConditionalRequired(nameof(SiglaObrigatoria), true)]
        [SMCConditionalReadonly(nameof(RestricaoCursoOferta), SMCConditionalOperation.Equals, false, PersistentValue = true)]
        public new string Sigla { get; set; }

        [SMCOrder(6)]
        [SMCStep(1, 0)]
        [SMCSize(SMCSize.Grid7_24, SMCSize.Grid24_24, SMCSize.Grid18_24, SMCSize.Grid5_24)]
        [SMCMaxLength(50)]
        [SMCConditionalDisplay(nameof(NomeReduzidoVisivel), true)]
        [SMCConditionalRequired(nameof(NomeReduzidoObrigatorio), true)]
        [SMCConditionalReadonly(nameof(RestricaoCursoOferta), SMCConditionalOperation.Equals, false, PersistentValue = true)]
        public new string NomeReduzido { get; set; }

        [SMCConditionalRequired(nameof(UnidadeSeoObrigatorio), true)]
        [SMCConditionalReadonly(nameof(RestricaoCursoOferta), SMCConditionalOperation.Equals, false, PersistentValue = true)]
        [SMCConditionalDisplay(nameof(UnidadeSeoVisivel), true)]
        [SMCInclude(ignore: true)] // O Dynamic gera include automático dos lookups, ignorado por ser uma entidade externa
        [SMCOrder(8)]
        [SMCSize(SMCSize.Grid17_24, SMCSize.Grid24_24, SMCSize.Grid24_24, SMCSize.Grid14_24)]
        [SMCStep(1, 0)]
        [UnidadeLookup]
        public new UnidadeLookupViewModel CodigoUnidadeSeo { get; set; }

        [SMCConditionalReadonly(nameof(RestricaoCursoOferta), SMCConditionalOperation.Equals, false, PersistentValue = true)]
        [SMCMask("999999999")]
        [SMCOrder(13)]
        [SMCSize(SMCSize.Grid7_24, SMCSize.Grid24_24, SMCSize.Grid12_24, SMCSize.Grid5_24)]
        [SMCStep(0)]
        public int? Codigo { get; set; }

        [SMCConditionalReadonly(nameof(RestricaoCursoOferta), SMCConditionalOperation.Equals, false, PersistentValue = true)]
        [CursoOfertaLookup]
        [SMCDependency(nameof(SeqCursoID))]
        [SMCDependency(nameof(SeqsEntidadesResponsaveis))]
        [SMCDependency(nameof(SeqNivelEnsino))]
        [SMCDependency(nameof(SeqSituacaoAtual))]
        [SMCOrder(14)]
        [SMCRequired]
        [SMCSize(SMCSize.Grid17_24, SMCSize.Grid24_24, SMCSize.Grid12_24, SMCSize.Grid9_24)]
        [SMCStep(0)]
        public CursoOfertaLookupViewModel SeqCursoOferta { get; set; }

        [SMCConditionalReadonly(nameof(RestricaoCursoOferta), SMCConditionalOperation.Equals, false, PersistentValue = true)]
        [SMCOrder(15)]
        [SMCRequired]
        [SMCSelect(nameof(Modalidades), AutoSelectSingleItem = true)]
        [SMCSize(SMCSize.Grid7_24, SMCSize.Grid24_24, SMCSize.Grid8_24, SMCSize.Grid3_24)]
        [SMCStep(0)]
        public long? SeqModalidade { get; set; }

        [SMCConditionalReadonly(nameof(RestricaoCursoOferta), SMCConditionalOperation.Equals, false, PersistentValue = true)]
        [SMCDependency(nameof(SeqModalidade), nameof(CursoOfertaLocalidadeController.BuscarLocalidadesPorModalidadeSelect), "CursoOfertaLocalidade", true, includedProperties: new string[] { nameof(SeqInstituicaoNivel), nameof(SeqCursoUnidade), nameof(DesativarfiltrosHierarquia) })]
        [SMCOrder(16)]
        [SMCRequired]
        [SMCSelect(nameof(Localidades))]
        [SMCSize(SMCSize.Grid9_24, SMCSize.Grid24_24, SMCSize.Grid8_24, SMCSize.Grid7_24)]
        [SMCStep(0)]
        public long SeqLocalidade { get; set; }

        [SMCConditionalReadonly(nameof(RestricaoCursoOferta), SMCConditionalOperation.Equals, false, PersistentValue = true)]
        //[SMCSelect(nameof(OrigensFinanceiras))]
        [SMCStep(0)]
        [SMCRequired]
        [SMCOrder(17)]
        [SMCSize(SMCSize.Grid8_24, SMCSize.Grid24_24, SMCSize.Grid8_24, SMCSize.Grid5_24)]
        public int? SeqOrigemFinanceira { get; set; }

        [SMCConditionalReadonly(nameof(RestricaoCursoOferta), SMCConditionalOperation.Equals, false, PersistentValue = true)]
        [SMCStep(0)]
        [SMCOrder(18)]
        [SMCDependency(nameof(SeqCurso), nameof(CursoOfertaLocalidadeController.BuscarTipoOrgaoReguladorInstituicaoNivelEnsino), "CursoOfertaLocalidade", true, nameof(SeqNivelEnsino), nameof(SeqInstituicaoEnsino))]
        [SMCSelect(nameof(OrgaoRegulador), autoSelectSingleItem: true)]
        [SMCReadOnly]
        [SMCSize(SMCSize.Grid6_24, SMCSize.Grid24_24, SMCSize.Grid6_24, SMCSize.Grid6_24)]
        public TipoOrgaoRegulador? SeqTipoOrgaoRegulador { get; set; }

        [SMCHidden]
        [SMCStep(0)]
        [SMCDependency(nameof(SeqTipoOrgaoRegulador), nameof(CursoOfertaLocalidadeController.TipoOrgaoReguladorMEC), "CursoOfertaLocalidade", true)]
        public bool? OrgaoReguladorMEC { get; set; }

        [SMCStep(0)]
        [SMCOrder(19)]
        [SMCConditionalDisplay(nameof(OrgaoReguladorMEC), true)]
        [SMCSize(SMCSize.Grid8_24, SMCSize.Grid24_24, SMCSize.Grid8_24, SMCSize.Grid5_24)]
        public int? CodigoOrgaoRegulador { get; set; }

        [SMCStep(0)]
        [SMCOrder(20)]
        [SMCConditionalDisplay(nameof(OrgaoReguladorMEC), true)]
        [SMCSize(SMCSize.Grid8_24, SMCSize.Grid24_24, SMCSize.Grid8_24, SMCSize.Grid5_24)]
        public int? CodigoHabilitacao { get; set; }

        [SMCConditionalReadonly(nameof(RestricaoCursoOferta), SMCConditionalOperation.Equals, false, PersistentValue = true)]
        [SMCStep(0)]
        [SMCOrder(21)]
        [SMCRequired]
        [SMCRadioButtonList]
        [SMCSize(SMCSize.Grid8_24, SMCSize.Grid24_24, SMCSize.Grid8_24, SMCSize.Grid5_24)]
        public bool? Hibrido { get; set; }

        [SMCConditionalReadonly(nameof(RestricaoCursoOferta), SMCConditionalOperation.Equals, false, PersistentValue = true)]
        [SMCGroupedProperty("situacaoEntidade")]
        [SMCIgnoreProp(SMCViewMode.Edit)]
        [SMCInclude("HistoricoSituacoes.SituacaoEntidade")]
        [SMCOrder(22)]
        [SMCRequired]
        [SMCSelect(nameof(Situacoes))]
        [SMCSize(SMCSize.Grid6_24)]
        [SMCStep(0)]
        public long SeqSituacaoAtualCursoOfertaLocalidade { get; set; }

        [SMCConditionalReadonly(nameof(RestricaoCursoOferta), SMCConditionalOperation.Equals, false, PersistentValue = true)]
        [SMCGroupedProperty("situacaoEntidade")]
        [SMCIgnoreProp(SMCViewMode.Edit)]
        [SMCOrder(23)]
        [SMCRequired]
        [SMCSize(SMCSize.Grid3_24)]
        [SMCStep(0)]
        public DateTime DataInicioSituacaoAtual { get; set; } = DateTime.Today;

        [SMCConditionalReadonly(nameof(RestricaoCursoOferta), SMCConditionalOperation.Equals, false, PersistentValue = true)]
        [SMCGroupedProperty("situacaoEntidade")]
        [SMCIgnoreProp(SMCViewMode.Edit)]
        [SMCMinDate(nameof(DataInicioSituacaoAtual))]
        [SMCOrder(24)]
        [SMCSize(SMCSize.Grid3_24)]
        [SMCStep(0)]
        public DateTime? DataFimSituacaoAtual { get; set; }

        [SMCConditionalReadonly(nameof(RestricaoCursoOferta), SMCConditionalOperation.Equals, false, PersistentValue = true)]
        [SMCMapForceFromTo]
        [SMCOrder(25)]
        [SMCSize(SMCSize.Grid24_24)]
        [SMCStep(0)]
        [SMCDetail(min: 1)]
        public SMCMasterDetailList<CursoOfertaLocalidadeTurnoViewModel> Turnos { get; set; }

        [SMCHidden]
        [SMCStep(0)]
        public bool SelecaoNivelFolha { get; set; } = true;

        [SMCDependency(nameof(SeqCursoOferta), nameof(CursoOfertaLocalidadeController.RecuperarFormacaoEspecificaCursoOferta), "CursoOfertaLocalidade", true)]
        [SMCHidden]
        [SMCStep(0)]
        public long? SeqFormacaoEspecifica { get; set; }

        [SMCConditionalReadonly(nameof(RestricaoCursoOferta), SMCConditionalOperation.Equals, false, PersistentValue = true)]
        [FormacaoEspecificaLookup]
        [SMCDependency(nameof(SelecaoNivelFolha))]
        [SMCDependency(nameof(SeqCurso))]
        [SMCDependency(nameof(SeqFormacaoEspecifica))]
        [SMCOrder(25)]
        [SMCSize(SMCSize.Grid24_24)]
        [SMCStep(0)]
        public List<FormacaoEspecificaLookupGridViewModel> FormacoesEspecificas { get; set; }

        #endregion [ Fim Aba0 Dados Gerais ]

        #region [ Aba1 Classifiações ]

        //Classificações que estão na EntidadeViewModel

        #endregion [ Aba1 Classifiações ]

        #region [ Aba2 Dados de Contato ]

        [Address(Correspondence = true)]
        [SMCConditionalDisplay(nameof(HabilitaEnderecos), SMCConditionalOperation.Equals, true)]
        [SMCMapForceFromTo]
        [SMCOrder(22)]
        [SMCSize(SMCSize.Grid24_24)]
        [SMCCssClass("smc-sga-detalhe-editavel-blocos-endereco-responsivo")]
        [SMCStep(2)]
        public AddressList Enderecos { get; set; }

        [SMCConditionalDisplay(nameof(HabilitaTelefones), SMCConditionalOperation.Equals, true)]
        [SMCMapForceFromTo]
        [SMCOrder(23)]
        [SMCDetail]
        [SMCSize(SMCSize.Grid12_24)]
        [SMCStep(2)]
        public SMCMasterDetailList<TelefoneCategoriaViewModel> Telefones { get; set; }

        [SMCConditionalDisplay(nameof(HabilitaEnderecosEletronicos), SMCConditionalOperation.Equals, true)]
        [SMCDetail]
        [SMCMapForceFromTo]
        [SMCOrder(24)]
        [SMCSize(SMCSize.Grid12_24)]
        [SMCStep(2)]
        public SMCMasterDetailList<EnderecoEletronicoCategoriaViewModel> EnderecosEletronicos { get; set; }      


        #endregion [ Aba2 Dados de Contato ]

        #region [Aba Ato Normativo - BI_ORG_002 - Atos Normativos da Entidade]

        [SMCHidden]
        [SMCStep(0)]
        public bool AtivaAbaAtoNormativo { get; set; }

        [SMCHidden]
        [SMCStep(0)]
        public bool HabilitaColunaGrauAcademico { get; set; }

        [SMCConditionalReadonly(nameof(RestricaoCursoOferta), SMCConditionalOperation.Equals, false, PersistentValue = true)]
        [SMCStep(3)]
        [SMCIgnoreProp(SMCViewMode.Insert)]
        [SMCConditional(SMCConditionalBehavior.Visibility, nameof(AtivaAbaAtoNormativo), SMCConditionalOperation.Equals, true)]
        public List<AtoNormativoVisualizarViewModel> AtoNormativo { get; set; }

        #endregion [Aba Ato Normativo - BI_ORG_002 - Atos Normativos da Entidade]

        #region [ Configuração ]

        public override void ConfigureDynamic(ref SMCDynamicOptions options)
        {
            options.Tab()
                .Header("CabecalhoCursoOfertaLocalidade")
                .RedirectIndexTo("Index", "CursoUnidade", null)
                .Service<ICursoOfertaLocalidadeService>(insert: nameof(ICursoOfertaLocalidadeService.BuscarConfiguracoesCursoOfertaLocalidade),
                                                        edit: nameof(ICursoOfertaLocalidadeService.BuscarCursoOfertaLocalidadeFiltroDesativado),
                                                        save: nameof(ICursoOfertaLocalidadeService.SalvarCursoOfertaLocalidadeFiltroDesativado))
                .Tokens(tokenInsert: UC_CSO_001_02_03.MANTER_CURSO_OFERTA_LOCALIDADE,
                        tokenEdit: UC_CSO_001_02_03.MANTER_CURSO_OFERTA_LOCALIDADE,
                        tokenRemove: UC_CSO_001_02_03.MANTER_CURSO_OFERTA_LOCALIDADE,
                        tokenList: UC_CSO_001_02_03.MANTER_CURSO_OFERTA_LOCALIDADE);
        }

        public override void ConfigureNavigation(ref SMCNavigationGroup navigationGroup)
        {
            navigationGroup = new CursoOfertaLocalidadeNavigationGroup(this);
        }

        #endregion [ Configuração ]
    }
}