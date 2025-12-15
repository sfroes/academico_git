using SMC.Academico.Common.Areas.TUR.Enums;
using SMC.Academico.ServiceContract.Areas.ORG.Interfaces;
using SMC.Academico.UI.Mvc.Areas.CAM.Lookups;
using SMC.Academico.UI.Mvc.Areas.CSO.Lookups;
using SMC.Framework;
using SMC.Framework.Mapper;
using SMC.Framework.Model;
using SMC.Framework.UI.Mvc.DataAnnotations;
using SMC.Framework.UI.Mvc.Dynamic;
using System.Collections.Generic;

namespace SMC.SGA.Administrativo.Areas.TUR.Models
{
    public class TurmaFiltroDynamicModel : SMCDynamicFilterViewModel
    {

        #region [ DataSources ]

        [SMCDataSource]
        [SMCServiceReference(typeof(IEntidadeService), nameof(IEntidadeService.BuscarUnidadesResponsaveisGPILocalSelect))]
        public List<SMCDatasourceItem> EntidadesResponsaveis { get; set; }

        [SMCDataSource]
        [SMCServiceReference(typeof(IInstituicaoNivelService), nameof(IInstituicaoNivelService.BuscarNiveisEnsinoReconhecidoLDBSelect))]
        public List<SMCDatasourceItem> NiveisEnsino { get; set; }

        #endregion [ DataSources ]

        [CicloLetivoLookup]
        [SMCFilter(true, true)]
        [SMCOrder(0)]
        [SMCSize(SMCSize.Grid6_24, SMCSize.Grid24_24, SMCSize.Grid8_24, SMCSize.Grid6_24)]
        public CicloLetivoLookupViewModel SeqCicloLetivoInicio { get; set; }

        [SMCFilter(true, true)]
        [SMCOrder(1)]
        [SMCMapProperty("SeqEntidadesResponsaveis")]
        [SMCRequired]
        [SMCSelect(nameof(EntidadesResponsaveis))]
        [SMCSize(SMCSize.Grid12_24, SMCSize.Grid24_24, SMCSize.Grid16_24, SMCSize.Grid13_24)]
        public List<long> SeqsEntidadesResponsaveis { get; set; }

        [SMCFilter(true, true)]
        [SMCOrder(2)]
        [SMCSelect(nameof(NiveisEnsino), autoSelectSingleItem: true)]
        [SMCSize(SMCSize.Grid6_24, SMCSize.Grid24_24, SMCSize.Grid8_24, SMCSize.Grid5_24)]
        public long? SeqNivelEnsino { get; set; }

        [CursoOfertaLookup]
        [SMCDependency(nameof(SeqsEntidadesResponsaveis))]
        [SMCDependency(nameof(SeqNivelEnsino))]
        [SMCFilter(true, true)]
        [SMCOrder(3)]
        [SMCSize(SMCSize.Grid18_24, SMCSize.Grid24_24, SMCSize.Grid16_24, SMCSize.Grid19_24)]
        public CursoOfertaLookupViewModel SeqCursoOferta { get; set; }

        [SMCFilter(true, true)]
        [SMCOrder(4)]
        [SMCRegularExpression(@"^[0-9]{0,}[.]{0,1}[0-9]{0,}$", FormatErrorResourceKey = "ERR_Expression")]
        [SMCSize(SMCSize.Grid6_24, SMCSize.Grid24_24, SMCSize.Grid8_24, SMCSize.Grid5_24)]
        public string CodigoFormatado { get; set; }

        [SMCFilter(true, true)]
        [SMCOrder(5)]
        [SMCSize(SMCSize.Grid18_24, SMCSize.Grid24_24, SMCSize.Grid16_24, SMCSize.Grid11_24)]
        public string DescricaoConfiguracao { get; set; }

        [SMCFilter(true, true)]
        [SMCOrder(6)]
        [SMCSelect]
        [SMCSize(SMCSize.Grid6_24, SMCSize.Grid24_24, SMCSize.Grid8_24, SMCSize.Grid4_24)]
        public SituacaoTurma? SituacaoTurmaAtual { get; set; }

        [SMCFilter(true, true)]
        [SMCOrder(7)]
        [SMCSelect]
        [SMCSize(SMCSize.Grid7_24, SMCSize.Grid24_24, SMCSize.Grid8_24, SMCSize.Grid4_24)]
        public SituacaoTurmaDiario? SituacaoTurmaDiario { get; set; }

        [SMCFilter(true, true)]
        [SMCOrder(8)]
        [SMCSelect]
        [SMCSize(SMCSize.Grid7_24, SMCSize.Grid24_24, SMCSize.Grid8_24, SMCSize.Grid5_24)]
        public bool? TurmaComOrientacao { get; set; }
    }
}