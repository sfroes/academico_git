using SMC.Academico.Common.Areas.CSO.Constants;
using SMC.Academico.Common.Areas.ORG.Constants;
using SMC.Academico.ServiceContract.Areas.CSO.Interfaces;
using SMC.Academico.ServiceContract.Areas.ORG.Interfaces;
using SMC.Academico.UI.Mvc.Areas.CSO.Lookups;
using SMC.DadosMestres.Common.Areas.PES.Enums;
using SMC.EstruturaOrganizacional.UI.Mvc.Areas.ESO.Lookups;
using SMC.Framework;
using SMC.Framework.DataAnnotations;
using SMC.Framework.Mapper;
using SMC.Framework.Model;
using SMC.Framework.Security;
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
using System.Linq;
using System.Web;

namespace SMC.SGA.Administrativo.Areas.CSO.Models
{
    [SMCStepConfiguration]
    [SMCStepConfiguration(Partial = "_ClassificacoesEntidade")]
    [SMCStepConfiguration]
    [SMCGroupedPropertyConfiguration(GroupId = "situacaoEntidade", Size = SMCSize.Grid24_24)]
    public class CursoUnidadeDynamicModel : EntidadeViewModel
    {
        #region [ DataSources ]

        [SMCDataSource]
        [SMCIgnoreProp]
        [SMCServiceReference(typeof(IInstituicaoTipoEntidadeService), nameof(IInstituicaoTipoEntidadeService.BuscarSituacoesTipoEntidadeDaInstituicaoSelect),
            values: new string[] { nameof(SeqTipoEntidade) })]
        public List<SMCDatasourceItem> Situacoes { get; set; }

        [SMCDataSource]
        [SMCIgnoreProp]
        [SMCServiceReference(typeof(ICursoUnidadeService), nameof(ICursoUnidadeService.BuscarUnidadesSemEntidadePaiSelect), values: new string[] { nameof(RemoveEntidadePai) })]
        public List<SMCDatasourceItem> Unidades { get; set; }

        [SMCDataSource(SMCStorageType.TempData)]
        [SMCMapForceFromTo]
        public List<TipoEndereco> TiposEnderecos { get; set; }

        [SMCDataSource(SMCStorageType.TempData)]
        [SMCMapForceFromTo]
        public List<SMCDatasourceItem<string>> TiposTelefone { get; set; }

        [SMCDataSource(SMCStorageType.TempData)]
        [SMCMapForceFromTo]
        public List<SMCDatasourceItem<string>> TiposEnderecoEletronico { get; set; }

        #endregion [ DataSources ]

        #region [ Hidden ]

        [SMCHidden]
        public bool RemoveEntidadePai { get; set; } = true;

        //Utilizado pelo navigation group
        [SMCHidden]
        [SMCMapProperty(nameof(Seq))]
        public long SeqEntidade { get; set; }

        [SMCHidden]
        [SMCStep(0)]
        public override long SeqTipoEntidade { get; set; }

        [SMCHidden]
        [SMCStep(0)]
        public bool ApenasNiveisEnsinoReconhecidosLDB { get; set; } = true;

        [SMCHidden]
        [SMCStep(0)]
        public bool ApenasEntidadesComCategoriasAtivas { get; set; } = true;

        [SMCHidden]
        [SMCStep(2)]
        public bool HabilitaEnderecos { get; set; }

        [SMCHidden]
        [SMCStep(2)]
        public bool HabilitaTelefones { get; set; }

        [SMCHidden]
        [SMCStep(2)]
        public bool HabilitaEnderecosEletronicos { get; set; }

        #endregion [ Hidden ]

        #region [ Aba0 Dados Gerais ]

        [SMCKey]
        [SMCReadOnly]
        [SMCSize(SMCSize.Grid2_24)]
        [SMCStep(0, 0)]
        public override long Seq { get; set; }

        [CursoLookup]
        [SMCDependency(nameof(ApenasEntidadesComCategoriasAtivas))]
        [SMCDependency(nameof(ApenasNiveisEnsinoReconhecidosLDB))]
        [SMCMapProperty("SeqCurso")]
        [SMCRequired]
        [SMCSize(SMCSize.Grid12_24, SMCSize.Grid24_24, SMCSize.Grid20_24, SMCSize.Grid12_24)]
        [SMCStep(0)]
        public CursoLookupViewModel Curso { get; set; }

        [SMCHidden]
        [SMCDependency(nameof(Curso) + ".Seq")]
        [SMCStep(0)]
        public long SeqCursoHidden { get { return Curso?.Seq ?? 0; } }

        [SMCHidden]
        [SMCDependency(nameof(Curso))]
        public override long? SeqEntidadeResponsavel => Curso?.Seq;

        [SMCRequired]
        [SMCSelect(nameof(Unidades))]
        [SMCSize(SMCSize.Grid10_24)]
        [SMCStep(0)]
        public long SeqUnidade { get; set; }

        [SMCDescription]
        [SMCDependency(nameof(SeqCursoHidden), nameof(CursoUnidadeController.RecuperarMascaraCursoUnidade), "CursoUnidade", false, nameof(SeqUnidade), nameof(Nome))]
        [SMCDependency(nameof(SeqUnidade), nameof(CursoUnidadeController.RecuperarMascaraCursoUnidade), "CursoUnidade", false, nameof(SeqCursoHidden), nameof(Nome))]
        [SMCMaxLength(255)]
        [SMCOrder(4)]
        [SMCRequired]
        [SMCSize(SMCSize.Grid14_24)]
        [SMCStep(1, 0)]
        public override string Nome { get; set; }

        [SMCOrder(8)]
        [SMCStep(1, 0)]
        [SMCConditionalDisplay("UnidadeSeoVisivel", true)]
        [SMCConditionalRequired("UnidadeSeoObrigatorio", true)]
        [SMCSize(SMCSize.Grid14_24)]
        [SMCInclude(true)] // O Dynamic gera include automático dos lookups, ignorado por ser uma entidade externa
        [UnidadeLookup]
        public new UnidadeLookupViewModel CodigoUnidadeSeo { get; set; }

        [SMCGroupedProperty("situacaoEntidade")]
        [SMCIgnoreProp(SMCViewMode.Edit)]
        [SMCInclude("HistoricoSituacoes.SituacaoEntidade")]
        [SMCOrder(13)]
        [SMCRequired]
        [SMCSelect("Situacoes")]
        [SMCSize(SMCSize.Grid6_24)]
        [SMCStep(0)]
        public long SeqSituacaoAtual { get; set; }

        [SMCGroupedProperty("situacaoEntidade")]
        [SMCIgnoreProp(SMCViewMode.Edit)]
        [SMCOrder(14)]
        [SMCRequired]
        [SMCSize(SMCSize.Grid3_24)]
        [SMCStep(0)]
        public DateTime DataInicioSituacaoAtual { get; set; } = DateTime.Today;

        [SMCGroupedProperty("situacaoEntidade")]
        [SMCIgnoreProp(SMCViewMode.Edit)]
        [SMCMinDate("DataInicioSituacaoAtual")]
        [SMCOrder(15)]
        [SMCSize(SMCSize.Grid3_24)]
        [SMCStep(0)]
        public DateTime? DataFimSituacaoAtual { get; set; }

        #endregion [ Aba0 Dados Gerais ]

        #region [ Aba1 Classifiações ]

        //Classificações que estão na EntidadeViewModel

        #endregion [ Aba1 Classifiações ]

        #region [ Aba2 Dados de Contato ]

        [Address(Correspondence = true)]
        [SMCConditionalDisplay(nameof(HabilitaEnderecos), SMCConditionalOperation.Equals, true)]
        [SMCMapForceFromTo]
        [SMCOrder(18)]
        [SMCSize(SMCSize.Grid24_24)]
        [SMCCssClass("smc-sga-detalhe-editavel-blocos-endereco-responsivo")]
        [SMCStep(2)]
        public AddressList Enderecos { get; set; }

        [SMCDetail]
        [SMCConditionalDisplay(nameof(HabilitaTelefones), SMCConditionalOperation.Equals, true)]
        [SMCMapForceFromTo]
        [SMCOrder(19)]
        [SMCSize(SMCSize.Grid12_24)]
        [SMCStep(2)]
        public SMCMasterDetailList<TelefoneCategoriaViewModel> Telefones { get; set; }

        [SMCConditionalDisplay(nameof(HabilitaEnderecosEletronicos), SMCConditionalOperation.Equals, true)]
        [SMCDetail]
        [SMCMapForceFromTo]
        [SMCOrder(20)]
        [SMCSize(SMCSize.Grid12_24)]
        [SMCStep(2)]
        public SMCMasterDetailList<EnderecoEletronicoCategoriaViewModel> EnderecosEletronicos { get; set; }

        #endregion [ Aba2 Dados de Contato ]

        #region [ Configuração ]

        public override void ConfigureDynamic(ref SMCDynamicOptions options)
        {
            options.Tab()
                .Detail<CursoUnidadeListarDynamicModel>("_DetailList", x => x.NomeUnidade, "_DetailHeader")
                .HeaderIndexList("Legenda")
                .DisableInitialListing(true)
                .Button("AlterarSituacao", "Index", "CursoUnidadeHistoricoSituacao",
                        UC_ORG_001_10_01.MANTER_SITUACAO_ENTIDADE,
                        i => new
                        {
                            seqEntidade = SMCDESCrypto.EncryptNumberForURL(((ISMCSeq)i).Seq),
                            seqTipoEntidade = SMCDESCrypto.EncryptNumberForURL(((CursoUnidadeListarDynamicModel)i).SeqTipoEntidade)
                        })
                .Button("OfertaCursoLocalidade", "Incluir", "CursoOfertaLocalidade",
                        UC_CSO_001_02_03.MANTER_CURSO_OFERTA_LOCALIDADE,
                        i => new
                        {
                            seqEntidade = SMCDESCrypto.EncryptNumberForURL(((ISMCSeq)i).Seq)
                        })
                .Tokens(tokenEdit: UC_CSO_001_02_02.MANTER_ASSOCIACAO_CURSO_UNIDADE,
                        tokenInsert: UC_CSO_001_02_02.MANTER_ASSOCIACAO_CURSO_UNIDADE,
                        tokenRemove: UC_CSO_001_02_02.MANTER_ASSOCIACAO_CURSO_UNIDADE,
                        tokenList: UC_CSO_001_02_01.PESQUISAR_ASSOCIACAO_CURSO_UNIDADE)
                .Service<ICursoUnidadeService>(index: nameof(ICursoUnidadeService.BuscarCursosUnidade),
                                               insert: nameof(ICursoUnidadeService.BuscarConfiguracoesCursoUnidade),
                                               save: nameof(ICursoUnidadeService.SalvarCursoUnidade),
                                               edit: nameof(ICursoUnidadeService.BuscarCursoUnidadeFiltroDesativado));

            if (HttpContext.Current.Request.QueryString.AllKeys.Contains("seqEntidade"))
                options.ButtonBackIndex("Index", "Curso", x => new { area = "CSO" });
        }

        public override void ConfigureNavigation(ref SMCNavigationGroup navigationGroup)
        {
            navigationGroup = new CursoUnidadeNavigationGroup(this);
        }

        #endregion [ Configuração ]
    }
}