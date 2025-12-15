using SMC.Academico.Common.Areas.PES.Constants;
using SMC.Academico.Common.Constants;
using SMC.Academico.ServiceContract.Areas.PES.Interfaces;
using SMC.Framework;
using SMC.Framework.Mapper;
using SMC.Framework.Model;
using SMC.Framework.UI.Mvc.DataAnnotations;
using SMC.Framework.UI.Mvc.Dynamic;
using SMC.SGA.Administrativo.Areas.PES.Views.InstituicaoTipoFuncionario.App_LocalResources;
using System.Collections.Generic;

namespace SMC.SGA.Administrativo.Areas.PES.Models
{
    public class InstituicaoTipoFuncionarioDynamicModel : SMCDynamicViewModel
    {
        #region DataSources

        [SMCIgnoreProp]
        [SMCDataSource]
        [SMCServiceReference(typeof(ITipoFuncionarioService), nameof(ITipoFuncionarioService.BuscarTiposFuncionarioSelect))]
        public List<SMCDatasourceItem> TiposFuncionario { get; set; }

        #endregion DataSources

        [SMCKey]
        [SMCHidden]
        public override long Seq { get; set; }

        [SMCHidden]
        [SMCParameter(FILTER.INSTITUICAO_ENSINO, true)]
        public long SeqInstituicaoEnsino { get; set; }

        [SMCFilter(true, true)]
        [SMCDescription]
        [SMCSortable(true, true, "TipoFuncionario.DescricaoMasculino")]
        [SMCRequired]
        [SMCSize(SMCSize.Grid15_24, SMCSize.Grid24_24, SMCSize.Grid12_24, SMCSize.Grid15_24)]
        [SMCSelect(nameof(TiposFuncionario))]
        public long? SeqTipoFuncionario { get; set; }

        [SMCHidden]
        [SMCMapProperty("TipoFuncionario.DescricaoMasculino")]
        [SMCInclude("TipoFuncionario")]
        public string DescricaoTipoFuncionario { get; set; }

        public override void ConfigureDynamic(ref SMCDynamicOptions options)
        {
            options.Service<IInstituicaoTipoFuncionarioService>(save: nameof(IInstituicaoTipoFuncionarioService.SalvarInstituicaoTipoFuncionario))
                   .EditInModal()
                   .ModalSize(SMCModalWindowSize.Medium)
                   .Messages(x => string.Format(UIResource.Listar_Excluir_Confirmacao,
                             ((InstituicaoTipoFuncionarioDynamicModel)x).DescricaoTipoFuncionario))
                   .Tokens(tokenInsert: UC_PES_003_04_01.MANTER_TIPO_VINCULO_FUNCIONARIO_INSTITUICAO,
                           tokenEdit: UC_PES_003_04_01.MANTER_TIPO_VINCULO_FUNCIONARIO_INSTITUICAO,
                           tokenRemove: UC_PES_003_04_01.MANTER_TIPO_VINCULO_FUNCIONARIO_INSTITUICAO,
                           tokenList: UC_PES_003_04_01.MANTER_TIPO_VINCULO_FUNCIONARIO_INSTITUICAO);
        }
    }
}