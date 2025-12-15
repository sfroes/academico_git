using SMC.Academico.Domain.Areas.PES.DomainServices;
using SMC.Academico.Domain.Areas.PES.Models;
using SMC.Academico.ServiceContract.Areas.PES.Data;
using SMC.Academico.ServiceContract.Areas.PES.Interfaces;
using SMC.Framework.Extensions;
using SMC.Framework.Model;
using SMC.Framework.Service;
using System.Collections.Generic;

namespace SMC.Academico.Service.Areas.PES.Services
{
    public class InstituicaoTipoEntidadeTipoFuncionarioService : SMCServiceBase, IInstituicaoTipoEntidadeTipoFuncionarioService
    {
        #region [ DomainService ]

        private InstituicaoTipoEntidadeTipoFuncionarioDomainService InstituicaoTipoEntidadeTipoFuncionarioDomainService => Create<InstituicaoTipoEntidadeTipoFuncionarioDomainService>();

        #endregion

        public long SalvarInstituicaoTipoEntidadeTipoFuncionario(InstituicaoTipoEntidadeTipoFuncionarioData instituicaoTipoEntidadeTipoFuncionarioData)
        {
            return InstituicaoTipoEntidadeTipoFuncionarioDomainService.Salvar(instituicaoTipoEntidadeTipoFuncionarioData.Transform<InstituicaoTipoEntidadeTipoFuncionario>());
        }

        public List<SMCDatasourceItem> BuscarTipoEntidadePorTipoFuncionario(long seqTipoFuncionario)
        {
            return InstituicaoTipoEntidadeTipoFuncionarioDomainService.BuscarTipoEntidadePorTipoFuncionario(seqTipoFuncionario);
        }

        public bool ListaTiposEntidadePorTipoFuncionario(long seqTipoFuncionario)
        {
            return InstituicaoTipoEntidadeTipoFuncionarioDomainService.ListaTiposEntidadePorTipoFuncionario(seqTipoFuncionario);
        }
    }
}
