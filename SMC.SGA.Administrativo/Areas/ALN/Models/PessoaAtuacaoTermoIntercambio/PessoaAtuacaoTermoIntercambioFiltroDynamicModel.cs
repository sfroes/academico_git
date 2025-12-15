using SMC.Academico.Common.Areas.ALN.Enums;
using SMC.Academico.Common.Constants;
using SMC.Academico.ServiceContract.Areas.ALN.Interfaces;
using SMC.Academico.ServiceContract.Areas.ORG.Interfaces;
using SMC.Academico.UI.Mvc.Areas.DCT.Lookups;
using SMC.Framework;
using SMC.Framework.DataAnnotations;
using SMC.Framework.Mapper;
using SMC.Framework.Model;
using SMC.Framework.UI.Mvc.DataAnnotations;
using SMC.Framework.UI.Mvc.Dynamic;
using SMC.SGA.Administrativo.Areas.ALN.Controllers;
using System;
using System.Collections.Generic;

namespace SMC.SGA.Administrativo.Areas.ALN.Models
{
    public class PessoaAtuacaoTermoIntercambioFiltroDynamicModel : SMCDynamicFilterViewModel
    {
        #region [DataSources]     
        
        [SMCDataSource]
        [SMCServiceReference(typeof(IEntidadeService), nameof(IEntidadeService.BuscarEntidadesResponsaveisVisaoOrganizacionalSelect))]
        public List<SMCDatasourceItem> EntidadesResponsaveis { get; set; }

        [SMCDataSource]
        [SMCServiceReference(typeof(IInstituicaoNivelService), nameof(IInstituicaoNivelService.BuscarNiveisEnsinoReconhecidoLDBSelect))]
        public List<SMCDatasourceItem> NiveisEnsino { get; set; }

        [SMCDataSource]
        [SMCIgnoreProp]
        [SMCServiceReference(typeof(ITipoTermoIntercambioService), nameof(ITipoTermoIntercambioService.BuscarTiposTermosIntercambiosInstituicaoNivelPermiteAssociarAlunoSelect))]
        public List<SMCDatasourceItem> TiposTermosIntercambios { get; set; }

        [SMCDataSource]
        [SMCServiceReference(typeof(ITipoVinculoAlunoService), nameof(ITipoVinculoAlunoService.BuscarTipoVinculoAlunoNaInstituicaoSelect))]
        public List<SMCDatasourceItem> TiposVinculoAluno { get; set; }
        #endregion

        [SMCHidden]
        [SMCParameter]
        public long? SeqParceriaIntercambio { get; set; }

        [SMCHidden]
        public string NameDescriptionParceriraIntercabioTipoTermo { get; set; }

        [SMCHidden]
        [SMCParameter(FILTER.INSTITUICAO_ENSINO, true)]
        public long? SeqInstituicaoEnsino { get; set; }

        [SMCHidden]
        [SMCParameter]
        public bool RetornarInstituicaoEnsinoLogada { get; set; } = false;

        [SMCHidden]
        [SMCParameter]
        public bool ListarSomenteInstituicoesEnsino { get; set; } = true;

        [SMCMaxLength(100)]
        [SMCFilter(true, true)]
        [SMCSize(SMCSize.Grid6_24)]        
        public string Nome { get; set; }
                
        [SMCFilter(true, true)]
        [SMCSize(SMCSize.Grid6_24)]
        [SMCMapProperty("SeqEntidadesResponsaveis")]
        [SMCSelect(nameof(EntidadesResponsaveis), autoSelectSingleItem: true)]
        public List<long> SeqsGruposProgramasResponsaveis { get; set; }

        [SMCFilter(true, true)]
        [SMCSelect(nameof(NiveisEnsino))]
        [SMCSize(SMCSize.Grid4_24)]        
        public long? SeqNivelEnsino { get; set; }

        [SMCFilter(true, true)]
        [SMCDependency(nameof(SeqNivelEnsino), nameof(AlunoController.BuscarTipoVinculoAluno), "Aluno", false)]
        [SMCSelect(nameof(TiposVinculoAluno), SortBy = SMCSortBy.Description)]
        [SMCSize(SMCSize.Grid4_24)]
        public long? SeqTipoVinculo { get; set; }

        [SMCFilter]
        [SMCSize(SMCSize.Grid4_24)]
        [SMCSelect(nameof(TiposTermosIntercambios), SortBy = SMCSortBy.Description)]
        public long? SeqTipoTermoIntercambio { get; set; }
        
        [SMCFilter]
        [SMCSize(SMCSize.Grid10_24)]
        [InstituicaoExternaLookup]
        [SMCDependency(nameof(SeqInstituicaoEnsino))]
        [SMCDependency(nameof(RetornarInstituicaoEnsinoLogada))]
        [SMCDependency(nameof(ListarSomenteInstituicoesEnsino))]
        public ParceriaIntercambioInstituicaoExternaFiltroViewModel SeqInstituicaoExterna { get; set; }

        [SMCSelect]
        [SMCFilter(true, true)]
        [SMCSize(SMCSize.Grid6_24)]
        public TipoMobilidade? TipoMobilidade { get; set; }

        [SMCSize(SMCSize.Grid4_24)]
        public DateTime? DataInicio { get; set; }
              
        [SMCSize(SMCSize.Grid4_24)]        
        [SMCMinValue(nameof(DataInicio))]
        [SMCConditionalRequired(nameof(DataInicio), SMCConditionalOperation.NotEqual, "")]
        public DateTime? DataFim { get; set; }
    }
}