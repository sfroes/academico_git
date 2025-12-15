using SMC.Academico.Domain.Areas.MAT.DomainServices;
using SMC.Academico.ServiceContract.Areas.MAT.Interfaces;
using SMC.Framework.Model;
using SMC.Framework.Service;
using System.Collections.Generic;

namespace SMC.Academico.Service.Areas.MAT.Services
{
    public class TipoSituacaoMatriculaService : SMCServiceBase, ITipoSituacaoMatriculaService
    {
        #region [DomainServices]

        private TipoSituacaoMatriculaDomainService TipoSituacaoMatriculaDomainService { get => Create<TipoSituacaoMatriculaDomainService>(); }

        #endregion [DomainServices]

        /// <summary>
        /// Busca todos os tipos de situações de matrícula que tenham o token matriculado
        /// </summary>
        /// <returns>Dados dos tipos de situaçoes de matrícula configuradas com o token matriculado</returns>
        public List<SMCDatasourceItem> BuscarTiposSituacoesMatriculasTokenMatriculadoSelect()
        {
            return this.TipoSituacaoMatriculaDomainService.BuscarTiposSituacoesMatriculasTokenMatriculadoSelect();
        }

        /// <summary>
        /// Busca todos os tipos de situações de matrícula
        /// </summary>
        /// <returns>Dados dos tipos de situaçoes de matrícula configuradas</returns>
        public List<SMCDatasourceItem> BuscarTiposSituacoesMatriculasSelect()
        {
            return this.TipoSituacaoMatriculaDomainService.BuscarTiposSituacoesMatriculasSelect();
        }

        /// <summary>
        /// Busca o token de uma situação de matrícula pelo sequancial
        /// </summary>
        /// <param name="seqTipoSituacaoMatricula">Sequencial do tipo de situação da matrícula</param>
        /// <returns></returns>
        public string BuscarTokenTipoSituacaoMatricula(long seqTipoSituacaoMatricula)
        {
            return this.TipoSituacaoMatriculaDomainService.BuscarTokenTipoSituacaoMatricula(seqTipoSituacaoMatricula);
        }
    }
}