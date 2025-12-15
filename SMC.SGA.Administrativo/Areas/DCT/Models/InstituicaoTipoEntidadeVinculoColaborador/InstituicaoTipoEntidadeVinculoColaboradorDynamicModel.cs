using SMC.Academico.Common.Areas.DCT.Constants;
using SMC.Academico.ServiceContract.Areas.DCT.Interfaces;
using SMC.Academico.ServiceContract.Areas.ORG.Interfaces;
using SMC.Framework;
using SMC.Framework.Mapper;
using SMC.Framework.Model;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.DataAnnotations;
using SMC.Framework.UI.Mvc.Dynamic;
using SMC.SGA.Administrativo.Areas.DCT.Views.InstituicaoTipoEntidadeVinculoColaborador.App_LocalResources;
using SMC.SGA.Administrativo.Areas.ORG.Models;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SMC.SGA.Administrativo.Areas.DCT.Models
{
    public class InstituicaoTipoEntidadeVinculoColaboradorDynamicModel : SMCDynamicViewModel
    {
        [SMCHidden]
        [SMCKey]
        [SMCOrder(0)]
        public override long Seq { get; set; }

        [SMCFilter(true, true)]
        [SMCHidden(SMCViewMode.List)]
        [SMCOrder(1)]
        [SMCRequired]
        [SMCSelect("InstituicoesTipoEntidade")]
        [SMCSize(SMCSize.Grid12_24)]
        public long SeqInstituicaoTipoEntidade { get; set; }

        [SMCDataSource()]
        [SMCIgnoreProp]
        [SMCServiceReference(typeof(IInstituicaoTipoEntidadeService), nameof(IInstituicaoTipoEntidadeService.BuscarInstituicaoTiposEntidadeSelect))]
        public List<SMCDatasourceItem> InstituicoesTipoEntidade { get; set; }

        [SMCHidden(SMCViewMode.Insert | SMCViewMode.Edit | SMCViewMode.Filter)]
        [SMCInclude("InstituicaoTipoEntidade.TipoEntidade")]
        [SMCMapProperty("InstituicaoTipoEntidade.TipoEntidade.Descricao")]
        [SMCOrder(1)]
        [SMCSortable(true, true, "InstituicaoTipoEntidade.TipoEntidade.Descricao")]
        [SMCSize(SMCSize.Grid12_24)]
        public string DescricaoInstituicaoTipoEntidade { get; set; }

        [SMCFilter(true, true)]
        [SMCHidden(SMCViewMode.List)]
        [SMCOrder(2)]
        [SMCRequired]
        [SMCSelect("TiposVinculoColaborador", "Seq", "Descricao", autoSelectSingleItem: true)]
        [SMCSize(SMCSize.Grid12_24)]
        public long SeqTipoVinculoColaborador { get; set; }

        [SMCDataSource("TipoVinculoColaborador")]
        [SMCIgnoreProp]
        [SMCServiceReference(typeof(IDCTDynamicService))]
        public List<SMCDatasourceItem> TiposVinculoColaborador { get; set; }

        [SMCHidden(SMCViewMode.Insert | SMCViewMode.Edit | SMCViewMode.Filter)]
        [SMCOrder(2)]
        [SMCInclude("TipoVinculoColaborador")]
        [SMCMapProperty("TipoVinculoColaborador.Descricao")]
        [SMCSortable(true, true, "TipoVinculoColaborador.Descricao")]
        public string DescricaoTipoVinculoColaborador { get; set; }

        public override void ConfigureDynamic(ref SMCDynamicOptions options)
        {
            options.EditInModal()
                   .Messages(x => string.Format(UIResource.Listar_Excluir_Confirmacao,
                                ((InstituicaoTipoEntidadeVinculoColaboradorDynamicModel)x).DescricaoInstituicaoTipoEntidade,
                                ((InstituicaoTipoEntidadeVinculoColaboradorDynamicModel)x).DescricaoTipoVinculoColaborador))
                   .Tokens(tokenInsert: UC_DCT_002_01_01.MANTER_TIPO_VINCULO_COLABORADOR_INSTITUICAO_TIPO_ENTIDADE,
                           tokenEdit: UC_DCT_002_01_01.MANTER_TIPO_VINCULO_COLABORADOR_INSTITUICAO_TIPO_ENTIDADE,
                           tokenRemove: UC_DCT_002_01_01.MANTER_TIPO_VINCULO_COLABORADOR_INSTITUICAO_TIPO_ENTIDADE,
                           tokenList: UC_DCT_002_01_01.MANTER_TIPO_VINCULO_COLABORADOR_INSTITUICAO_TIPO_ENTIDADE);

            if (HttpContext.Current.Request.QueryString.AllKeys.Contains("seqInstituicaoTipoEntidade"))
                options.ButtonBackIndex("Index", "InstituicaoTipoEntidade", x => new { area = "ORG" });
        }

    }
}