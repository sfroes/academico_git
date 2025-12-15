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
    public class InstituicaoTipoFuncionarioService : SMCServiceBase, IInstituicaoTipoFuncionarioService
    {
        #region [ DomainService ]

        private InstituicaoTipoFuncionarioDomainService InstituicaoTipoFuncionarioDomainService => Create<InstituicaoTipoFuncionarioDomainService>();

        #endregion

        /// <summary>
        /// Buscar tipo funcionario por instituição de ensino
        /// </summary>
        /// <param name="seqInstituicaoEnsino">Sequencial da instituição de ensino</param>
        /// <returns>Lista de todos os tipos de funcionários da instituição de ensino</returns>
        public List<SMCDatasourceItem> BuscarTipoFuncionarioInstituicao()
        {
            return InstituicaoTipoFuncionarioDomainService.BuscarTipoFuncionarioInstituicao();
        }

        public long SalvarInstituicaoTipoFuncionario(InstituicaoTipoFuncionarioData instituicaoTipoFuncionario)
        {
            return InstituicaoTipoFuncionarioDomainService.Salvar(instituicaoTipoFuncionario.Transform<InstituicaoTipoFuncionario>());
        }

        public List<SMCDatasourceItem> BuscarTipoFuncionarioInstituicaoETipoEntidadePorFuncionario(long seqInstituicaoEnsino)
        {
           return InstituicaoTipoFuncionarioDomainService.BuscarTipoFuncionarioInstituicaoETipoEntidadePorFuncionario(seqInstituicaoEnsino);
        }

    }
}
