using SMC.Academico.Common.Areas.PES.Enums;
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
    [SMCGroupedPropertyConfiguration(GroupId = "FiltroVinculo", Size = SMCSize.Grid18_24)]
    public class FuncionarioFiltroDynamicModel : SMCDynamicFilterViewModel, ISMCMappable
    {
        #region [ DataSources ]

        [SMCDataSource]
        [SMCServiceReference(typeof(IInstituicaoTipoFuncionarioService), nameof(IInstituicaoTipoFuncionarioService.BuscarTipoFuncionarioInstituicaoETipoEntidadePorFuncionario), values: new[] { nameof(SeqInstituicaoEnsino) })]
        public List<SMCDatasourceItem> TiposFuncionarios { get; set; }

        [SMCDataSource]
        [SMCServiceReference(typeof(IFuncionarioVinculoService), nameof(IFuncionarioVinculoService.BuscarEntidadesPorVinculoFuncionario), values: new[] { nameof(SeqTipoFuncionario) })]
        public List<SMCDatasourceItem> Entidades { get; set; }

        #endregion [ DataSources ]

        [SMCFilter(true, true)]
        [SMCSize(SMCSize.Grid3_24)]
        public long? Seq { get; set; }

        [SMCFilter(true, true)]
        [SMCMapProperty("Nome")]
        [SMCSize(SMCSize.Grid15_24)]
        public string NomeFiltro { get; set; }

        [SMCCpf]
        [SMCFilter(true, true)]
        [SMCSize(SMCSize.Grid3_24)]
        public string Cpf { get; set; }

        [SMCFilter(true, true)]
        [SMCSize(SMCSize.Grid3_24)]
        public string NumeroPassaporte { get; set; }

        [SMCFilter(true, true)]
        [SMCGroupedProperty("FiltroVinculo")]
        [SMCSelect(nameof(TiposFuncionarios))]
        [SMCSize(SMCSize.Grid6_24)]
        public long? SeqTipoFuncionario { get; set; }

        [SMCConditionalRequired(nameof(DataFim), SMCConditionalOperation.NotEqual,"")]
        [SMCFilter(true, true)]
        [SMCGroupedProperty("FiltroVinculo")]
        [SMCSize(SMCSize.Grid6_24)]
        public DateTime? DataInicio { get; set; }

        [SMCConditionalRequired(nameof(DataInicio), SMCConditionalOperation.NotEqual, "")]
        [SMCFilter(true, true)]
        [SMCGroupedProperty("FiltroVinculo")]
        [SMCMinDate(nameof(DataInicio))]
        [SMCSize(SMCSize.Grid6_24)]
        public DateTime? DataFim { get; set; }

        [SMCSelect(nameof(Entidades))]
        [SMCFilter]
        [SMCGroupedProperty("FiltroVinculo")]
        [SMCDependency(nameof(SeqTipoFuncionario), nameof(FuncionarioController.BuscarEntidadesPorVinculoFuncionario), "Funcionario",true)]
        [SMCConditionalDisplay(nameof(ExibirCampoEntidades), SMCConditionalOperation.Equals, true)]
        [SMCSize(SMCSize.Grid6_24)]
        public long? SeqEntidade { get; set; }

        [SMCHidden]
        [SMCParameter(FILTER.INSTITUICAO_ENSINO, true)]
        public long SeqInstituicaoEnsino { get; set; }


        [SMCHidden]
        [SMCDependency(nameof(SeqTipoFuncionario), nameof(FuncionarioController.ExibirCampos), "Funcionario", true)]
        public bool ExibirCampoEntidades { get; set; }
    }
}