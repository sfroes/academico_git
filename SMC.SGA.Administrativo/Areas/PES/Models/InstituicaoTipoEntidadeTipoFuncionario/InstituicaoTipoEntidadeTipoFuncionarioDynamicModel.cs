using SMC.Academico.Common.Areas.PES.Constants;
using SMC.Academico.Common.Constants;
using SMC.Academico.ServiceContract.Areas.ORG.Interfaces;
using SMC.Academico.ServiceContract.Areas.PES.Interfaces;
using SMC.Framework;
using SMC.Framework.Mapper;
using SMC.Framework.Model;
using SMC.Framework.UI.Mvc.DataAnnotations;
using SMC.Framework.UI.Mvc.Dynamic;
using System.Collections.Generic;

namespace SMC.SGA.Administrativo.Areas.PES.Models
{
    public class InstituicaoTipoEntidadeTipoFuncionarioDynamicModel : SMCDynamicViewModel
    {
        #region DataSources

        [SMCIgnoreProp]
        [SMCDataSource]
        [SMCServiceReference(typeof(IInstituicaoTipoEntidadeService), nameof(IInstituicaoTipoEntidadeService.BuscarInstituicaoTiposEntidadeSelect))]
        public List<SMCDatasourceItem> TiposEntidades { get; set; }

        [SMCIgnoreProp]
        [SMCDataSource]
        [SMCServiceReference(typeof(ITipoFuncionarioService), nameof(ITipoFuncionarioService.BuscarTiposFuncionarioSelect))]
        public List<SMCDatasourceItem> TiposVinculosFuncionarios { get; set; }

        #endregion DataSources

        [SMCKey]
        [SMCHidden]
        public override long Seq { get; set; }

        [SMCHidden]
        [SMCParameter(FILTER.INSTITUICAO_ENSINO, true)]
        public long? SeqInstituicaoEnsino { get; set; }

        [SMCRequired]
        [SMCSize(SMCSize.Grid12_24, SMCSize.Grid24_24, SMCSize.Grid12_24)]
        [SMCSelect(nameof(TiposEntidades), AutoSelectSingleItem = true)]
        [SMCFilter(true, true)]
        public long? SeqInstituicaoTipoEntidade { get; set; }

        [SMCRequired]
        [SMCSize(SMCSize.Grid12_24, SMCSize.Grid24_24, SMCSize.Grid12_24)]
        [SMCSelect(nameof(TiposVinculosFuncionarios), AutoSelectSingleItem = true)]
        [SMCFilter(true,true)]
        public long? SeqTipoFuncionario { get; set; }

        public override void ConfigureDynamic(ref SMCDynamicOptions options)
        {
            options.Service<IInstituicaoTipoEntidadeTipoFuncionarioService>(
                        save: nameof(IInstituicaoTipoEntidadeTipoFuncionarioService.SalvarInstituicaoTipoEntidadeTipoFuncionario))
                   .EditInModal()
                   .ModalSize(SMCModalWindowSize.Medium)
                   .Tokens(tokenInsert: UC_PES_003_05_01.MANTER_TIPO_VINCULO_FUNCIONARIO_INSTITUICAO_TIPO_ENTIDADE,
                            tokenEdit: UC_PES_003_05_01.MANTER_TIPO_VINCULO_FUNCIONARIO_INSTITUICAO_TIPO_ENTIDADE,
                            tokenRemove: UC_PES_003_05_01.MANTER_TIPO_VINCULO_FUNCIONARIO_INSTITUICAO_TIPO_ENTIDADE,
                            tokenList: UC_PES_003_05_01.MANTER_TIPO_VINCULO_FUNCIONARIO_INSTITUICAO_TIPO_ENTIDADE);
        }
    }
}