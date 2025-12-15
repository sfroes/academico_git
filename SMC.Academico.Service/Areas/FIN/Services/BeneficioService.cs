using SMC.Academico.Domain.Areas.FIN.DomainServices;
using SMC.Academico.Domain.Areas.FIN.Models;
using SMC.Academico.ServiceContract.Areas.FIN;
using SMC.Academico.ServiceContract.Areas.FIN.Interfaces;
using SMC.Framework.Extensions;
using SMC.Framework.Model;
using SMC.Framework.Service;
using SMC.Framework.Specification;
using SMC.Financeiro.ServiceContract.BLT;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SMC.Academico.Service.Areas.FIN.Services
{
    public class BeneficioService : SMCServiceBase, IBeneficioService
    {
        #region [ Services ]

        private BeneficioDomainService BeneficioDomainService
        {
            get { return Create<BeneficioDomainService>(); }
        }

        private TipoBeneficioDomainService TipoBeneficioDomainService
        {
            get { return Create<TipoBeneficioDomainService>(); }
        }

        private IIntegracaoFinanceiroService IntegracaoFinanceiroService
        {
            get { return Create<IIntegracaoFinanceiroService>(); }
        }

        #endregion [ Services ]

        /// <summary>
        /// Busca um beneficio
        /// </summary>
        /// <param name="seq">Sequencial do beneficio</param>
        /// <returns>Dados do beneficio</returns>
        public BeneficioData BuscarBeneficio(long seq)
        {
            return this.BeneficioDomainService.BuscarBeneficio(seq).Transform<BeneficioData>();
        }

        /// <summary>
        /// Busca os benefícios por nível de ensino na instituição
        /// </summary>
        /// <param name="seqNivelEnsino">Sequencial do nível de ensino</param>
        /// <returns>Dados dos benefícios do nível de ensino informado</returns>
        public List<SMCDatasourceItem> BuscarBeneficioPorNivelEnsinoSelect(long seqNivelEnsino)
        {
            return this.BeneficioDomainService.BuscarBeneficioPorNivelEnsinoSelect(seqNivelEnsino);
        }

        public List<SMCDatasourceItem> BuscarBeneficiosGRASelect(long seqTipoBeneficio)
        {
            try
            {
                TipoBeneficio tipoBeneficio = TipoBeneficioDomainService.SearchByKey(new SMCSeqSpecification<TipoBeneficio>(seqTipoBeneficio));
                return tipoBeneficio == null ? new List<SMCDatasourceItem>() : IntegracaoFinanceiroService.BuscarBeneficiosAtivosSelect((Financeiro.Common.Areas.TXA.Enums.TipoBeneficio)tipoBeneficio.ClassificacaoBeneficio);
            }
            catch (Exception e)
            {
                return new List<SMCDatasourceItem>();
            }
        }

        public List<SMCDatasourceItem> BuscarTipoBeneficioSelect()
        {
            return TipoBeneficioDomainService.SearchProjectionAll(s => new SMCDatasourceItem { Seq = s.Seq, Descricao = s.Descricao }).OrderBy(o => o.Descricao).ToList();
        }

        public long SalvarBeneficio(BeneficioData beneficio)
        {
            var beneficioTr = beneficio.Transform<Beneficio>();
            return BeneficioDomainService.SalvarBeneficio(beneficioTr);
        }

        /// <summary>
        /// Busca beneficios
        /// </summary>
        /// <returns>Lista de benefícios</returns>
        public List<SMCDatasourceItem> BuscarBeneficiosSelect()
        {
            return BeneficioDomainService.BuscarBeneficiosSelect();
        }
    }
}