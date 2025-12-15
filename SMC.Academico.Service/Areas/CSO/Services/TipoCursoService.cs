using SMC.Academico.Common.Areas.CSO.Enums;
using SMC.Academico.Common.Areas.CSO.Includes;
using SMC.Academico.Domain.Areas.CSO.DomainServices;
using SMC.Academico.Domain.Areas.CSO.Specifications;
using SMC.Academico.ServiceContract.Areas.CSO.Interfaces;
using SMC.Framework.Service;
using System.Collections.Generic;

namespace SMC.Academico.Service.Areas.CSO.Services
{
    public class TipoCursoService : SMCServiceBase, ITipoCursoService
    {
        #region Domain Service

        private InstituicaoNivelTipoCursoDomainService InstituicaoNivelTipoCursoDomainService
        {
            get { return this.Create<InstituicaoNivelTipoCursoDomainService>(); }
        }

        #endregion Domain Service

        /// <summary>
        /// Busca a lista de tipo de curso por nivel ensino para popular um Select
        /// </summary>
        /// <returns>Lista de níveis de ensino</returns>
        public List<TipoCurso> BuscarTiposCursoPorNivelEnsinoSelect(long seqNivelEnsino)
        {
            if (seqNivelEnsino == 0)
                return new List<TipoCurso>();

            var specification = new InstituicaoNivelTipoCursoFilterSpecification() { SeqNivelEnsino = seqNivelEnsino };

            //Include necessário para levar em conta o filtro global da instituição de ensino
            var lista = InstituicaoNivelTipoCursoDomainService.SearchBySpecification(specification, IncludesInstituicaoNivelTipoCurso.InstituicaoNivel);
            List<TipoCurso> retorno = new List<TipoCurso>();
            foreach (var item in lista)
            {
                retorno.Add(item.TipoCurso);
            }
            return retorno;
        }
    }
}