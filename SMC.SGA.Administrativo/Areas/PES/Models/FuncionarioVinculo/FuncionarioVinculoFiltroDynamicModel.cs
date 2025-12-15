using SMC.Academico.Common.Constants;
using SMC.Academico.ServiceContract.Areas.PES.Interfaces;
using SMC.Framework;
using SMC.Framework.Model;
using SMC.Framework.UI.Mvc.DataAnnotations;
using SMC.Framework.UI.Mvc.Dynamic;
using SMC.SGA.Administrativo.Areas.PES.Controllers;
using System.Collections.Generic;

namespace SMC.SGA.Administrativo.Areas.PES.Models
{
    public class FuncionarioVinculoFiltroDynamicModel : SMCDynamicFilterViewModel
    {
        #region [ DataSources ]

        [SMCDataSource]
        [SMCServiceReference(typeof(IInstituicaoTipoFuncionarioService), nameof(IInstituicaoTipoFuncionarioService.BuscarTipoFuncionarioInstituicaoETipoEntidadePorFuncionario), values: new[] { nameof(SeqInstituicaoEnsino) })]
        public List<SMCDatasourceItem> TiposFuncionarios { get; set; }

        [SMCDataSource]
        [SMCServiceReference(typeof(IFuncionarioVinculoService), nameof(IFuncionarioVinculoService.BuscarEntidadesPorVinculoFuncionario), values: new[] { nameof(SeqTipoFuncionario) })]
        public List<SMCDatasourceItem> Entidades { get; set; }

        #endregion [ DataSources ]

        [SMCSelect(nameof(TiposFuncionarios))]
        [SMCSize(SMCSize.Grid12_24)]
        public long? SeqTipoFuncionario { get; set; }

        [SMCSelect(nameof(Entidades))]
        [SMCFilter]
        [SMCDependency(nameof(SeqTipoFuncionario), nameof(FuncionarioController.BuscarEntidadesPorVinculoFuncionario), "Funcionario", true)]
        [SMCSize(SMCSize.Grid12_24)]
        public long? SeqEntidade { get; set; }

        [SMCHidden]
        [SMCParameter(FILTER.INSTITUICAO_ENSINO, true)]
        public long SeqInstituicaoEnsino { get; set; }

        [SMCHidden]
        [SMCParameter]
        public long SeqFuncionario { get; set; }
    }
}