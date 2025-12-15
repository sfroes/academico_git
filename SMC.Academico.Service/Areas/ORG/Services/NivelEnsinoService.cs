using SMC.Academico.Common.Areas.CAM.Includes;
using SMC.Academico.Common.Areas.ORG.Includes;
using SMC.Academico.Domain.Areas.CAM.DomainServices;
using SMC.Academico.Domain.Areas.CAM.Models;
using SMC.Academico.Domain.Areas.ORG.DomainServices;
using SMC.Academico.Domain.Areas.ORG.Models;
using SMC.Academico.ServiceContract.Areas.ORG.Data;
using SMC.Academico.ServiceContract.Areas.ORG.Interfaces;
using SMC.Framework.Extensions;
using SMC.Framework.Model;
using SMC.Framework.Service;
using SMC.Framework.Specification;
using System.Collections.Generic;
using System.Linq;
using System;
using SMC.Academico.Domain.Areas.ALN.DomainServices;
using SMC.Academico.Domain.Areas.ALN.Models;
using SMC.Academico.Domain.Areas.ORG.Specifications;

namespace SMC.Academico.Service.Areas.ORG.Services
{
    public class NivelEnsinoService : SMCServiceBase, INivelEnsinoService
    {
        #region Domain Service

        private NivelEnsinoDomainService NivelEnsinoDomainService
        {
            get { return Create<NivelEnsinoDomainService>(); }
        }

        private CicloLetivoDomainService CicloLetivoDomainService
        {
            get { return Create<CicloLetivoDomainService>(); }
        }

        private ParceriaIntercambioDomainService ParceriaIntercambioDomainService
        {
            get { return Create<ParceriaIntercambioDomainService>(); }
        }

        private InstituicaoNivelDomainService InstituicaoNivelDomainService
        {
            get { return Create<InstituicaoNivelDomainService>(); }
        }

        #endregion Domain Service

        /// <summary>
        /// Busca os níveis de ensino
        /// </summary>
        /// <returns>Lista com os níveis de ensino</returns>
        public List<NivelEnsinoHierarquiaData> BuscarNiveisEnsino()
        {
            return NivelEnsinoDomainService.SearchAll(n => n.Ordem, IncludesNivelEnsino.NiveisEnsinoFilhos)
                                           .TransformList<NivelEnsinoHierarquiaData>();
        }

        public List<SMCDatasourceItem> BuscarNiveisEnsinoPorCicloLetivoSelect(long seqCicloLetivo)
        {
            var cicloLetivo = CicloLetivoDomainService.SearchByKey(new SMCSeqSpecification<CicloLetivo>(seqCicloLetivo), IncludesCicloLetivo.NiveisEnsino);

            return cicloLetivo.NiveisEnsino?.OrderBy(x => x.Descricao).TransformList<SMCDatasourceItem>();
        }

        public NivelEnsinoData BuscarNivelEnsino(long seqNivelEnsino)
        {
            return NivelEnsinoDomainService.SearchProjectionByKey(
                new SMCSeqSpecification<NivelEnsino>(seqNivelEnsino),
                n => new NivelEnsinoData()
                {
                    Seq = n.Seq,
                    Descricao = n.Descricao,
                    Sigla = n.Sigla,
                    Token = n.Token,
                    Ordem = n.Ordem,
                    SeqNivelEnsinoSuperior = n.SeqNivelEnsinoSuperior,
                    NivelSuperior = n.NivelEnsinoSuperior.Descricao
                });
        }

        /// <summary>
        /// Busca os sequenciais dos níveis de ensino utilizados no stricto sensu
        /// </summary>
        /// <returns>Lista com os sequenciais dos níveis de ensino</returns>
        public List<long> BuscarSeqsNiveisEnsinoStrictoSensu()
        {
            return NivelEnsinoDomainService.BuscarSeqsNiveisEnsinoStrictoSensu();
        }

        /// <summary>
        ///  Lista de Níveis de acordo com o que foi parametrizado para a Instituição da Parceria em questão
        ///  e a regra RN_USG_004 - Filtro por Nível de Ensino.
        /// </summary>
        /// <param name="seqParceriaIntercambio"></param>
        /// <returns></returns>
        public List<SMCDatasourceItem> BuscarNiveisEnsinoParceriaIntercambioSelect(long seqParceriaIntercambio)
        {
            long seqInstituicaoEnsino = ParceriaIntercambioDomainService.SearchProjectionByKey(new SMCSeqSpecification<ParceriaIntercambio>(seqParceriaIntercambio), a => a.SeqInstituicaoEnsino);

            InstituicaoNivelFilterSpecification spec = new InstituicaoNivelFilterSpecification() { SeqInstituicaoEnsino = seqInstituicaoEnsino };
            return InstituicaoNivelDomainService.SearchProjectionBySpecification(spec, x => new SMCDatasourceItem()
            {
                Seq = x.NivelEnsino.Seq,
                Descricao = x.NivelEnsino.Descricao
            }).ToList().OrderBy(o => o.Descricao).ToList();
        }

        public List<SMCDatasourceItem> BuscarNiveisEnsinoPorServicoSelect(long seqServico)
        {
            return NivelEnsinoDomainService.BuscarNiveisEnsinoPorServicoSelect(seqServico);
        }
    }
}