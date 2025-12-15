using SMC.Academico.Common.Areas.DCT.Constants;
using SMC.Academico.Common.Areas.DCT.Enums;
using SMC.Academico.ServiceContract.Areas.ORG.Interfaces;
using SMC.Framework;
using SMC.Framework.Mapper;
using SMC.Framework.Model;
using SMC.Framework.UI.Mvc.DataAnnotations;
using SMC.Framework.UI.Mvc.Dynamic;
using SMC.SGA.Administrativo.Areas.DCT.Views.InstituicaoNivelTipoAtividadeColaborador.App_LocalResources;
using System.Collections.Generic;

namespace SMC.SGA.Administrativo.Areas.DCT.Models
{
    public class InstituicaoNivelTipoAtividadeColaboradorDynamicModel : SMCDynamicViewModel
    {
        [SMCHidden]
        [SMCKey]
        [SMCOrder(0)]
        [SMCRequired]
        public override long Seq { get; set; }

        [SMCFilter(true, true)]
        [SMCHidden(SMCViewMode.List)]
        [SMCRequired]
        [SMCSelect("InstituicaoNiveis")]
        [SMCSize(SMCSize.Grid8_24)]
        public long SeqInstituicaoNivel { get; set; }

        [SMCDataSource()]
        [SMCIgnoreProp]
        [SMCServiceReference(typeof(IInstituicaoNivelService), "BuscarNiveisEnsinoDaInstituicaoSelect")]
        public List<SMCDatasourceItem> InstituicaoNiveis { get; set; }

        [SMCHidden(SMCViewMode.Insert | SMCViewMode.Edit | SMCViewMode.Filter)]
        [SMCInclude("InstituicaoNivel.NivelEnsino")]
        [SMCMapProperty("InstituicaoNivel.NivelEnsino.Descricao")]
        [SMCSortable(true, true, "InstituicaoNivel.NivelEnsino.Descricao")]
        public string DescricaoInstituicaoNivel { get; set; }

        [SMCFilter(true, true)]
        [SMCRequired]
        [SMCSelect]
        [SMCSize(SMCSize.Grid7_24)]
//        [SMCSortable(true)]
        public TipoAtividadeColaborador? TipoAtividadeColaborador { get; set; }

        public override void ConfigureDynamic(ref SMCDynamicOptions options)
        {
            options.EditInModal()
                   .Messages(x => string.Format(UIResource.Listar_Excluir_Confirmacao,
                                ((InstituicaoNivelTipoAtividadeColaboradorDynamicModel)x).TipoAtividadeColaborador.SMCGetDescription(),
                                ((InstituicaoNivelTipoAtividadeColaboradorDynamicModel)x).DescricaoInstituicaoNivel))
                   .Tokens(tokenInsert: UC_DCT_002_02_01.MANTER_TIPO_ATIVIDADE_COLABORADOR_INSTITUICAO_NIVEL,
                       tokenEdit: UC_DCT_002_02_01.MANTER_TIPO_ATIVIDADE_COLABORADOR_INSTITUICAO_NIVEL,
                       tokenRemove: UC_DCT_002_02_01.MANTER_TIPO_ATIVIDADE_COLABORADOR_INSTITUICAO_NIVEL,
                       tokenList: UC_DCT_002_02_01.MANTER_TIPO_ATIVIDADE_COLABORADOR_INSTITUICAO_NIVEL);
        }
    }
}