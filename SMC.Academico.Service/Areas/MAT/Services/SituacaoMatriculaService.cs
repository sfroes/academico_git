using SMC.Academico.Domain.Areas.MAT.DomainServices;
using SMC.Academico.Domain.Areas.MAT.Models;
using SMC.Academico.Domain.Areas.MAT.ValueObjects;
using SMC.Academico.ServiceContract.Areas.MAT.Data;
using SMC.Academico.ServiceContract.Areas.MAT.Interfaces;
using SMC.Framework.Extensions;
using SMC.Framework.Model;
using SMC.Framework.Service;
using SMC.Framework.Specification;
using System.Collections.Generic;

namespace SMC.Academico.Service.Areas.MAT.Services
{
    public class SituacaoMatriculaService : SMCServiceBase, ISituacaoMatriculaService
    {
        #region [DomainServices]

        private SituacaoMatriculaDomainService SituacaoMatriculaDomainService { get => Create<SituacaoMatriculaDomainService>(); }

        #endregion [DomainServices]

        public List<SMCDatasourceItem> BuscarSituacoesMatriculasSelect()
        {
            var lista = SituacaoMatriculaDomainService.SearchAll();
            List<SMCDatasourceItem> retorno = new List<SMCDatasourceItem>();
            foreach (var item in lista)
            {
                retorno.Add(new SMCDatasourceItem(item.Seq, item.Descricao));
            }
            return retorno;
        }

        /// <summary>
        /// Busca as situações de matrícula configuradas na instituição
        /// </summary>
        /// <param name="filtroData">Dados do filtro</param>
        /// <returns>Dados das situaçoes de matrícula configuradas na instituição</returns>
        public List<SMCDatasourceItem> BuscarSituacoesMatriculasDaInstiuicaoSelect(SituacaoMatriculaFiltroData filtroData)
        {
            return this.SituacaoMatriculaDomainService.BuscarSituacoesMatriculasDaInstituicaoSelect(filtroData.Transform<SituacaoMatriculaFiltroVO>());
        }

        public List<SMCDatasourceItem> BuscarSituacoesMatriculaPorTipo(long seq)
        {
            return this.SituacaoMatriculaDomainService.BuscarSituacoesMatriculaPorTipo(seq);
        }

        public List<SMCDatasourceItem> BuscarSituacoesMatricula(SituacaoMatriculaFiltroData filtro)
        {
            return this.SituacaoMatriculaDomainService.BuscarSituacoesMatricula(filtro.Transform<SituacaoMatriculaFiltroVO>());
        }

        public SituacaoMatriculaData BuscarSituacaoMatricula(long seq)
        {
            return this.SituacaoMatriculaDomainService.SearchByKey(new SMCSeqSpecification<SituacaoMatricula>(seq)).Transform<SituacaoMatriculaData>();
        }

        public SMCDatasourceItem BuscarSituacaoMatriculaItemSelectPorToken(string token)
        {
            return this.SituacaoMatriculaDomainService.BuscarSituacaoMatriculaItemSelectPorToken(token);
        }
    }
}