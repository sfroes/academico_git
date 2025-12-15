using SMC.Academico.Common.Areas.FIN.Enums;
using SMC.Academico.ServiceContract.Areas.FIN.Data;
using SMC.Academico.ServiceContract.Areas.FIN.Data.PessoaAtuacaoBeneficio;
using SMC.Framework.Model;
using SMC.Framework.Service;
using System;
using System.Collections.Generic;

namespace SMC.Academico.ServiceContract.Areas.FIN.Interfaces
{
    public interface IMotivoAlteracaoBeneficio : ISMCService
    {
        /// <summary>
        /// Buscar todos os motivos de alteração benefcio por instituicão de ensino
        /// </summary>
        /// <param name="seqIntituicaoEnsino">Sequencial da instituição de ensino</param>
        /// <returns>Lista de todos os motivos de alteração beneficio</returns>
        List<MotivoAlteracaoBeneficioData> BuscarMotivoAlteracaoBeneficioInstituicaoEnsino(long seqIntituicaoEnsino);

        /// <summary>
        /// Buscar o sequencial do motivo de alteração por token
        /// </summary>
        /// <param name="token">Token de validação</param>
        /// <returns>Sequencial motivo alteração</returns>
        long BuscarMotivoAlteracaoBeneficioPorToken(string token);
    }
}