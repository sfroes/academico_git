using SMC.Academico.Common.Areas.CUR.Constants;
using SMC.Academico.Common.Areas.CUR.Enums;
using SMC.Academico.ServiceContract.Areas.CUR.Interfaces;
using SMC.Framework;
using SMC.Framework.DataAnnotations;
using SMC.Framework.Mapper;
using SMC.Framework.Model;
using SMC.Framework.Security;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.DataAnnotations;
using SMC.Framework.UI.Mvc.Dynamic;
using SMC.Framework.UI.Mvc.Html;
using System.Collections.Generic;

namespace SMC.SGA.Administrativo.Areas.CUR.Models
{
    public class MatrizCurricularConfiguracaoComponenteDynamicModel : SMCDynamicViewModel
    {
        #region [ DataSources ]

        [SMCDataSource]
        [SMCServiceReference(typeof(ICurriculoCursoOfertaGrupoService), nameof(ICurriculoCursoOfertaGrupoService.BuscarCurriculoCursoOfertasGruposSelect),
            values: new string[] { nameof(SeqMatrizCurricular), nameof(Seq)})]
        public List<SMCDatasourceItem> CurriculoCursoOfertasGrupos { get; set; }

        [SMCDataSource]
        [SMCServiceReference(typeof(IDivisaoMatrizCurricularService), nameof(IDivisaoMatrizCurricularService.BuscarDivisoesMatrizCurricularDescricaoSelect),
            values: new string[] { nameof(SeqMatrizCurricular) })]
        public List<SMCDatasourceItem> DivisoesMatrizCurricular { get; set; }

        #endregion [ DataSources ]

        [SMCHidden]
        [SMCParameter]
        public long SeqMatrizCurricular { get; set; }

        [SMCHidden]
        [SMCParameter]
        public long SeqCurriculoCursoOferta { get; set; }

        [SMCKey]
        [SMCHidden]
        [SMCOrder(1)]
        [SMCRequired]
        public override long Seq { get; set; }

        [SMCReadOnly(SMCViewMode.Edit)]
        [SMCRequired]
        [SMCSize(SMCSize.Grid9_24)]
        [SMCSelect(nameof(CurriculoCursoOfertasGrupos))]
        public long SeqCurriculoCursoOfertaGrupo { get; set; }

        [SMCDescription]
        [SMCHidden(SMCViewMode.Edit | SMCViewMode.Insert)]
        public string DescricaoCurriculoCursoOfertaGrupo { get; set; }

        [SMCDependency(nameof(SeqCurriculoCursoOfertaGrupo), "BuscarDescricaoTipoConfiguracaoGrupoCurricular", "MatrizCurricularConfiguracaoComponente", true)]
        [SMCReadOnly]
        [SMCSize(SMCSize.Grid9_24)]
        public string DescricaoTipoConfiguracaoGrupoCurricular { get; set; }

        [SMCIgnoreProp]
        public string TituloConfiguracaoGrupo
        {
            get
            {                
                if(string.IsNullOrEmpty(QuantidadeFormatada))
                    return $"{DescricaoCurriculoCursoOfertaGrupo} - {DescricaoTipoConfiguracaoGrupoCurricular}";
                else
                    return $"{DescricaoCurriculoCursoOfertaGrupo} - {DescricaoTipoConfiguracaoGrupoCurricular} : {QuantidadeFormatada}"; 
            }
        }

        [SMCDependency(nameof(SeqCurriculoCursoOfertaGrupo), "BuscarFormatoTipoConfiguracaoGrupoCurricular", "MatrizCurricularConfiguracaoComponente", true)]
        [SMCHidden]
        public FormatoConfiguracaoGrupo? FormatoConfiguracaoGrupoGrupoCurricular { get; set; }

        [SMCConditionalDisplay(nameof(FormatoConfiguracaoGrupoGrupoCurricular), SMCConditionalOperation.NotEqual, new[] { FormatoConfiguracaoGrupo.Nenhum })]
        [SMCDependency(nameof(SeqCurriculoCursoOfertaGrupo), "BuscarQuantidadeFormatadaTipoConfiguracaoGrupoCurricular", "MatrizCurricularConfiguracaoComponente", true)]
        [SMCSize(SMCSize.Grid6_24)]
        [SMCMinValue(1)]
        [SMCReadOnly]
        public string QuantidadeFormatada { get; set; }

        [SMCDetail(SMCDetailType.Block)]
        [SMCMapForceFromTo]
        [SMCSize(SMCSize.Grid24_24)]
        public SMCMasterDetailList<MatrizCurricularConfiguracaoComponenteDivisaoViewModel> DivisaoMatrizCurricularGrupo { get; set; }

        #region [ Configurações ]

        public override void ConfigureDynamic(ref SMCDynamicOptions options)
        {
            options.EditInModal()
                   .ModalSize(SMCModalWindowSize.Large)
                   .HeaderIndex("MatrizCurricularConfiguracaoComponenteCabecalho")
                   .Detail<MatrizCurricularConfiguracaoComponenteDynamicModel>("_DetailList")
                   .ButtonBackIndex("Index", "MatrizCurricular", model => new
                   {
                       SeqCurriculoCursoOferta = SMCDESCrypto.EncryptNumberForURL((model as MatrizCurricularConfiguracaoComponenteFiltroDynamicModel).SeqCurriculoCursoOferta)
                   })
                   .Service<IMatrizCurricularConfiguracaoComponenteService>(
                      delete: nameof(IMatrizCurricularConfiguracaoComponenteService.ExcluirMatrizCurricularConfiguracaoComponente),
                        edit: nameof(IMatrizCurricularConfiguracaoComponenteService.BuscarMatrizCurricularConfiguracaoComponente),
                       index: nameof(IMatrizCurricularConfiguracaoComponenteService.BuscarMatrizesCurricularesConfiguracoesComponente),
                        save: nameof(IMatrizCurricularConfiguracaoComponenteService.SalvarMatrizCurricularConfiguracaoComponente))
                   .Tokens(
                   tokenEdit: UC_CUR_001_05_05.MANTER_CONFIGURACAO_GRUPO_MATRIZ,
                 tokenInsert: UC_CUR_001_05_05.MANTER_CONFIGURACAO_GRUPO_MATRIZ,
                   tokenList: UC_CUR_001_05_04.PESQUISAR_CONFIGURACAO_GRUPO_MATRIZ,
                 tokenRemove: UC_CUR_001_05_05.MANTER_CONFIGURACAO_GRUPO_MATRIZ);
        }

        public override void ConfigureNavigation(ref SMCNavigationGroup navigationGroup)
        {
            navigationGroup = new MatrizCurricularNavigationGroup(this);
        }

        #endregion [ Configurações ]
    }
}