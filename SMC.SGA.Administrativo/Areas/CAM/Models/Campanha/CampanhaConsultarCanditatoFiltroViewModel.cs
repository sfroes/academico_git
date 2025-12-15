using SMC.Academico.Common.Areas.CAM.Enums;
using SMC.Framework;
using SMC.Framework.Mapper;
using SMC.Framework.Model;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.DataAnnotations;
using SMC.SGA.Administrativo.Areas.CAM.Controllers;
using System.Collections.Generic;
using System.ComponentModel;

namespace SMC.SGA.Administrativo.Areas.CAM.Models
{
    public class CampanhaConsultarCandidatoFiltroViewModel : SMCPagerViewModel //IOfertaCampanhaBIFilter, IOfertaCursoNivelEnsinoBIFilter
    {
        #region Data Sources

        public List<SMCDatasourceItem> TiposProcessoSeletivo { get; set; }

        public List<SMCDatasourceItem> ProcessosSeletivos { get; set; }

        public List<SMCDatasourceItem> CiclosLetivos { get; set; }

        public List<SMCDatasourceItem> Convocacoes { get; set; }

        public List<SMCDatasourceItem> Chamadas { get; set; }

        //public List<SMCDatasourceItem> TiposOfertas { get; set; }

        //public List<SMCDatasourceItem> EntidadesResponsaveis { get; set; }

        //public List<SMCDatasourceItem> Localidades { get; set; }

        //public List<SMCDatasourceItem> NiveisEnsino { get; set; }

        //public List<SMCDatasourceItem> Colaboradores { get; set; }

        #endregion Data Sources

        #region Propriedades Auxiliares

        //[SMCHidden]
        //public bool EntidadesResponsaveisApenasAtivas { get { return true; } }

        //[SMCHidden]
        //public bool LocalidadesApenasAtivas { get { return true; } }

        //[SMCHidden]
        //public bool? SelecaoNivelFolha { get { return false; } }

        //[SMCHidden]
        //[SMCDependency(nameof(SeqTipoOferta), nameof(CampanhaController.BloquearCampoFormacaoEspecifica), "Campanha", true, includedProperties: new string[] { nameof(SeqsEntidadesResponsaveis), nameof(SeqCursoOferta) })]
        //[SMCDependency(nameof(SeqCursoOferta), nameof(CampanhaController.BloquearCampoFormacaoEspecifica), "Campanha", true, includedProperties: new string[] { nameof(SeqsEntidadesResponsaveis), nameof(SeqTipoOferta) })]
        //[SMCDependency(nameof(SeqsEntidadesResponsaveis), nameof(CampanhaController.BloquearCampoFormacaoEspecifica), "Campanha", true, includedProperties: new string[] { nameof(SeqCursoOferta), nameof(SeqTipoOferta) })]
        //public bool BloquearCampoFormacaoEspecifica { get; set; }

        #endregion Propriedades Auxiliares

        [SMCHidden]
        public long SeqCampanha { get; set; }

        [SMCSelect(nameof(CiclosLetivos))]
        [SMCSize(SMCSize.Grid3_24, SMCSize.Grid24_24, SMCSize.Grid4_24, SMCSize.Grid3_24)]
        public long? SeqCicloLetivo { get; set; }

        [SMCSelect(nameof(TiposProcessoSeletivo))]
        [SMCSize(SMCSize.Grid6_24, SMCSize.Grid24_24, SMCSize.Grid8_24, SMCSize.Grid6_24)]
        public long? SeqTipoProcessoSeletivo { get; set; }

        [SMCSelect(nameof(ProcessosSeletivos))]
        [SMCSize(SMCSize.Grid12_24, SMCSize.Grid24_24, SMCSize.Grid8_24, SMCSize.Grid12_24)]
        [SMCDependency(nameof(SeqTipoProcessoSeletivo), nameof(CampanhaController.BuscarProcessosSeletivosPorCampanhaTipoProcessoSeletivoSelect), "Campanha", false, includedProperties: new string[] { nameof(SeqCampanha) })]
        public long? SeqProcessoSeletivo { get; set; }

        [SMCSelect(nameof(Convocacoes))]
        [SMCSize(SMCSize.Grid8_24, SMCSize.Grid24_24, SMCSize.Grid12_24, SMCSize.Grid8_24)]
        [SMCDependency(nameof(SeqProcessoSeletivo), nameof(CampanhaController.BuscarConvocacoesPorCampanhaCicloLetivoProcessoSeletivoSelect), "Campanha", false, includedProperties: new string[] { nameof(SeqCicloLetivo), nameof(SeqTipoProcessoSeletivo), nameof(SeqCampanha) })]
        public long? SeqConvocacao { get; set; }

        [SMCSelect(IgnoredEnumItems = new object[] { Academico.Common.Areas.CAM.Enums.TipoChamada.Excedentes })]
        [SMCSize(SMCSize.Grid4_24, SMCSize.Grid24_24, SMCSize.Grid6_24, SMCSize.Grid4_24)]
        public TipoChamada? TipoChamada { get; set; }

        [SMCSelect(nameof(Chamadas))]
        [SMCSize(SMCSize.Grid4_24, SMCSize.Grid24_24, SMCSize.Grid6_24, SMCSize.Grid4_24)]
        [SMCDependency(nameof(SeqConvocacao), nameof(CampanhaController.BuscarChamadasPorCampanhaConvocacaoTipoChamadaSelect), "Campanha", false, includedProperties: new string[] { nameof(SeqCampanha), nameof(TipoChamada) })]
        [SMCDependency(nameof(TipoChamada), nameof(CampanhaController.BuscarChamadasPorCampanhaConvocacaoTipoChamadaSelect), "Campanha", false, includedProperties: new string[] { nameof(SeqCampanha), nameof(SeqConvocacao) })]
        [SMCConditionalReadonly(nameof(SeqConvocacao), "")]
        public long? SeqChamada { get; set; }

        [SMCSize(SMCSize.Grid8_24, SMCSize.Grid24_24, SMCSize.Grid12_24, SMCSize.Grid8_24)]
        public string OfertaCampanha { get; set; }

        [SMCRequired]
        [SMCMapForceFromTo]
        [SMCSelect]
        [SMCSize(SMCSize.Grid3_24, SMCSize.Grid24_24, SMCSize.Grid4_24, SMCSize.Grid3_24)]
        public FiltroExportado Exportado { get; set; } = FiltroExportado.Todos;

        public enum FiltroExportado
        {
            Todos,
            Sim,
            [Description("Não")]
            Nao            
        }
    }    
}