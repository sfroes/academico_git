using SMC.Academico.Common.Areas.DCT.Constants;
using SMC.Academico.Common.Constants;
using SMC.Framework;
using SMC.Framework.Mapper;
using SMC.Framework.UI.Mvc.DataAnnotations;
using SMC.Framework.UI.Mvc.Dynamic;
using SMC.Framework.UI.Mvc.Html;
using SMC.Pessoas.ServiceContract.Areas.PES.Interfaces;
using System.Collections.Generic;

namespace SMC.SGA.Administrativo.Areas.DCT.Models
{
    public class TipoVinculoColaboradorDynamicModel : SMCDynamicViewModel
    {
        #region DataSources

        [SMCIgnoreProp]
        [SMCDataSource]
        [SMCServiceReference(typeof(IPessoaService), nameof(IPessoaService.BuscarPapeis), null, null)]
        public List<SMCSelectListItem> Papeis { get; set; }

        #endregion DataSources



        [SMCFilter(true, true)]
        [SMCKey]
        [SMCOrder(0)]
        [SMCReadOnly(SMCViewMode.Edit | SMCViewMode.Insert)]
        [SMCRequired]
        [SMCSize(SMCSize.Grid4_24, SMCSize.Grid24_24, SMCSize.Grid6_24, SMCSize.Grid4_24)]
        [SMCSortable(true)]
        public override long Seq { get; set; }

        [SMCDescription]
        [SMCFilter(true, true)]
        [SMCMaxLength(100)]
        [SMCOrder(1)]
        [SMCRequired]
        [SMCSize(SMCSize.Grid13_24, SMCSize.Grid24_24, SMCSize.Grid10_24, SMCSize.Grid14_24)]
        [SMCSortable(true, true)]
        public string Descricao { get; set; }

        [SMCOrder(2)]
        [SMCRequired]
        [SMCIgnoreProp(SMCViewMode.Filter | SMCViewMode.List)]
        [SMCRegularExpression(REGEX.TOKEN)]
        [SMCMaxLength(100)]
        [SMCSize(Framework.SMCSize.Grid7_24, Framework.SMCSize.Grid24_24, Framework.SMCSize.Grid10_24, Framework.SMCSize.Grid6_24)]
        public string Token { get; set; }

        [SMCHidden(SMCViewMode.Filter)]
        [SMCHorizontalAlignment(SMCHorizontalAlignment.Left)]
        [SMCMapForceFromTo]
        [SMCOrder(3)]
        [SMCRadioButtonList]
        [SMCRequired]
        [SMCSize(SMCSize.Grid14_24, SMCSize.Grid24_24, SMCSize.Grid24_24, SMCSize.Grid13_24)]
        public bool RequerAcompanhamentoSupervisor { get; set; }

        [SMCHidden(SMCViewMode.Filter)]
        [SMCHorizontalAlignment(SMCHorizontalAlignment.Left)]
        [SMCOrder(4)]
        [SMCRadioButtonList]
        [SMCRequired]
        [SMCSize(SMCSize.Grid10_24, SMCSize.Grid24_24, SMCSize.Grid24_24, SMCSize.Grid11_24)]
        public bool PermiteInclusaoManualVinculo { get; set; }

        [SMCHidden(SMCViewMode.Filter)]
        [SMCHorizontalAlignment(SMCHorizontalAlignment.Left)]
        [SMCOrder(5)]
        [SMCRadioButtonList]
        [SMCRequired]
        [SMCSize(SMCSize.Grid14_24, SMCSize.Grid24_24, SMCSize.Grid24_24, SMCSize.Grid13_24)]
        public bool IntegraCorpoDocente { get; set; }

        [SMCHidden(SMCViewMode.Filter | SMCViewMode.List)]
        [SMCHorizontalAlignment(SMCHorizontalAlignment.Left)]
        [SMCOrder(6)]
        [SMCRadioButtonList]
        [SMCRequired]
        [SMCSize(SMCSize.Grid10_24, SMCSize.Grid24_24, SMCSize.Grid24_24, SMCSize.Grid11_24)]
        public bool CriaVinculoInstitucional { get; set; }

        [SMCSelect(nameof(Papeis))]
        [SMCSize(SMCSize.Grid14_24, SMCSize.Grid24_24, SMCSize.Grid24_24, SMCSize.Grid13_24)]
        [SMCOrder(7)]
        [SMCHidden(SMCViewMode.List)]
        public int? CodigoPapeVinculoCad { get; set; }

        [SMCRadioButtonList]
        [SMCSize(SMCSize.Grid10_24, SMCSize.Grid24_24, SMCSize.Grid24_24, SMCSize.Grid11_24)]
        [SMCOrder(8)]
        [SMCHidden(SMCViewMode.Filter)]
        [SMCHorizontalAlignment(SMCHorizontalAlignment.Left)]
        [SMCRequired]
        public bool ExigeFormacaoAcademica { get; set; }

        public override void ConfigureDynamic(ref SMCDynamicOptions options)
        {
            options.EditInModal()
                   .Tokens(tokenInsert: UC_DCT_001_03_01.MANTER_TIPO_VINCULO_COLABORADOR,
                           tokenEdit: UC_DCT_001_03_01.MANTER_TIPO_VINCULO_COLABORADOR,
                           tokenRemove: UC_DCT_001_03_01.MANTER_TIPO_VINCULO_COLABORADOR,
                           tokenList: UC_DCT_001_03_01.MANTER_TIPO_VINCULO_COLABORADOR);
        }
    }
}