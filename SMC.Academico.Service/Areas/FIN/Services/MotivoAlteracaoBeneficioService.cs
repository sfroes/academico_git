using SMC.Academico.Common.Areas.FIN.Enums;
using SMC.Academico.Domain.Areas.ALN.DomainServices;
using SMC.Academico.Domain.Areas.FIN.DomainServices;
using SMC.Academico.Domain.Areas.FIN.Models;
using SMC.Academico.Domain.Areas.FIN.Specifications;
using SMC.Academico.Domain.Areas.FIN.ValueObjects;
using SMC.Academico.ServiceContract.Areas.FIN;
using SMC.Academico.ServiceContract.Areas.FIN.Data;
using SMC.Academico.ServiceContract.Areas.FIN.Data.PessoaAtuacaoBeneficio;
using SMC.Academico.ServiceContract.Areas.FIN.Interfaces;
using SMC.Framework.Extensions;
using SMC.Framework.Model;
using SMC.Framework.Service;
using System;
using System.Collections.Generic;

namespace SMC.Academico.Service.Areas.FIN.Services
{
    public class MotivoAlteracaoBeneficioService : SMCServiceBase, IMotivoAlteracaoBeneficio
    {
        #region [DomainService]

        private MotivoAlteracaoBeneficioDomainService MotivoAlteracaoBeneficioDomainService => Create<MotivoAlteracaoBeneficioDomainService>();

        #endregion

        /// <summary>
        /// Buscar todos os motivos de alteração benefcio por instituicão de ensino
        /// </summary>
        /// <param name="seqIntituicaoEnsino">Sequencial da instituição de ensino</param>
        /// <returns>Lista de todos os motivos de alteração beneficio</returns>
        public List<MotivoAlteracaoBeneficioData> BuscarMotivoAlteracaoBeneficioInstituicaoEnsino(long seqIntituicaoEnsino)
        {
            return this.MotivoAlteracaoBeneficioDomainService.BuscarMotivoAlteracaoBeneficioInstituicaoEnsino(seqIntituicaoEnsino).TransformList<MotivoAlteracaoBeneficioData>();
        }

        /// <summary>
        /// Buscar o sequencial do motivo de alteração por token
        /// </summary>
        /// <param name="token">Token de validação</param>
        /// <returns>Sequencial motivo alteração</returns>
        public long BuscarMotivoAlteracaoBeneficioPorToken(string token)
        {
            return this.MotivoAlteracaoBeneficioDomainService.BuscarMotivoAlteracaoBeneficioPorToken(token);
        }

    }
}