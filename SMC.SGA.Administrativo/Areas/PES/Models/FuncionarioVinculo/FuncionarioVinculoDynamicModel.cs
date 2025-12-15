using SMC.Academico.Common.Areas.PES.Constants;
using SMC.Academico.Common.Constants;
using SMC.Academico.ServiceContract.Areas.PES.Interfaces;
using SMC.Framework;
using SMC.Framework.DataAnnotations;
using SMC.Framework.Mapper;
using SMC.Framework.Model;
using SMC.Framework.UI.Mvc.DataAnnotations;
using SMC.Framework.UI.Mvc.Dynamic;
using SMC.SGA.Administrativo.Areas.PES.Controllers;
using System;
using System.Collections.Generic;

namespace SMC.SGA.Administrativo.Areas.PES.Models
{
    public class FuncionarioVinculoDynamicModel : SMCDynamicViewModel
    {
        #region [ DataSources ]

        [SMCDataSource]
        [SMCServiceReference(typeof(IInstituicaoTipoFuncionarioService), nameof(IInstituicaoTipoFuncionarioService.BuscarTipoFuncionarioInstituicaoETipoEntidadePorFuncionario), values: new[] { nameof(SeqInstituicaoEnsino) })]
        public List<SMCDatasourceItem> TiposFuncionarios { get; set; }

        [SMCDataSource]
        [SMCServiceReference(typeof(IFuncionarioVinculoService), nameof(IFuncionarioVinculoService.BuscarEntidadesPorVinculoFuncionario), values: new[] { nameof(SeqTipoFuncionario) })]
        public List<SMCDatasourceItem> Entidades { get; set; }

        [SMCDataSource]
        [SMCServiceReference(typeof(IInstituicaoTipoEntidadeTipoFuncionarioService), nameof(IInstituicaoTipoEntidadeTipoFuncionarioService.BuscarTipoEntidadePorTipoFuncionario), values: new[] { nameof(SeqTipoFuncionario) })]
        public List<SMCDatasourceItem> TipoEntidades { get; set; }
        #endregion [ DataSources ]

        [SMCHidden]
        [SMCKey]
        public override long Seq { get; set; }

        [SMCHidden]
        [SMCParameter]
        public long SeqFuncionario { get; set; }

        [SMCSelect(nameof(TiposFuncionarios))]
        [SMCSize(SMCSize.Grid10_24)]
        public long? SeqTipoFuncionario { get; set; }

        [SMCMapProperty("DataInicio")]
        [SMCRequired]
        [SMCSize(SMCSize.Grid6_24, SMCSize.Grid12_24, SMCSize.Grid6_24, SMCSize.Grid6_24)]
        public DateTime DataInicioVinculo { get; set; }

        [SMCMapProperty("DataFim")]
        [SMCMinDate(nameof(DataInicioVinculo))]
        [SMCSize(SMCSize.Grid6_24, SMCSize.Grid12_24, SMCSize.Grid6_24, SMCSize.Grid6_24)]
        public DateTime? DataFimVinculo { get; set; }


        [SMCSelect(nameof(TipoEntidades), AutoSelectSingleItem = true)]
        [SMCSize(SMCSize.Grid12_24, SMCSize.Grid12_24, SMCSize.Grid12_24, SMCSize.Grid12_24)]
        [SMCConditionalDisplay(nameof(ExibirCamposTipoEntidadesEEntidades), SMCConditionalOperation.Equals, true)]
        [SMCDependency(nameof(SeqTipoFuncionario), nameof(FuncionarioController.BuscarTiposEntidades), "Funcionario", true)]
        [SMCStep(4)]
        public long? SeqTipoEntidade { get; set; }


        [SMCSelect(nameof(Entidades))]
        [SMCSize(SMCSize.Grid12_24, SMCSize.Grid12_24, SMCSize.Grid12_24, SMCSize.Grid12_24)]
        [SMCConditionalDisplay(nameof(ExibirCamposTipoEntidadesEEntidades), SMCConditionalOperation.Equals, true)]
        [SMCConditionalRequired(nameof(ExibirCamposTipoEntidadesEEntidades), SMCConditionalOperation.Equals, true)]
        [SMCDependency(nameof(SeqTipoEntidade), nameof(FuncionarioController.BuscarEntidades), "Funcionario", true)]
        public long? SeqEntidadeVinculo { get; set; }

        [SMCHidden]
        [SMCDependency(nameof(SeqTipoFuncionario), nameof(FuncionarioController.ExibirCampos), "Funcionario", true)]
        public bool ExibirCamposTipoEntidadesEEntidades { get; set; }

        [SMCHidden]
        [SMCParameter(FILTER.INSTITUICAO_ENSINO, true)]
        public long SeqInstituicaoEnsino { get; set; }



        public override void ConfigureDynamic(ref SMCDynamicOptions options)
        {
            options
                   .EditInModal()
                   .ModalSize(SMCModalWindowSize.Large)
                   .HeaderIndex(nameof(FuncionarioVinculoController.CabecalhoFuncionarioVinculo))
                   .Header(nameof(FuncionarioVinculoController.CabecalhoFuncionarioVinculo))
                   .RequiredIncomingParameters(new[] { nameof(SeqFuncionario) })
                   .ButtonBackIndex("Index", "Funcionario")
                   .Service<IFuncionarioVinculoService>(edit: nameof(IFuncionarioVinculoService.BuscarFuncionarioVinculo),
                                                        delete: nameof(IFuncionarioVinculoService.ExcluirFuncionarioVinculo),
                                                        index: nameof(IFuncionarioVinculoService.BuscarVinculosFuncionario),
                                                        save: nameof(IFuncionarioVinculoService.SalvarFuncionarioVinculo))
                   .Tokens(tokenInsert: UC_PES_006_02_04.MANTER_VINCULO_FUNCIONARIO,
                           tokenEdit: UC_PES_006_02_04.MANTER_VINCULO_FUNCIONARIO,
                           tokenRemove: UC_PES_006_02_04.MANTER_VINCULO_FUNCIONARIO,
                           tokenList: UC_PES_006_02_03.PESQUISAR_VINCULO_FUNCIONARIO);
        }
    }
}