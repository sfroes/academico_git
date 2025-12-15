using SMC.Academico.Common.Areas.PES.Enums;
using SMC.Academico.Common.Areas.SRC.Enums;
using SMC.Academico.ServiceContract.Areas.MAT.Data;
using SMC.Academico.ServiceContract.Areas.SRC.Data;
using SMC.Academico.ServiceContract.Areas.SRC.Data.SolicitacaoReabertura;
using SMC.Framework.Model;
using SMC.Framework.Service;
using System;
using System.Collections.Generic;

namespace SMC.Academico.ServiceContract.Areas.SRC.Interfaces
{
    public interface ISolicitacaoTrabalhoAcademicoService : ISMCService
    {

        /// <summary>
        ///Busca o seq divisão pela solicitação trabalho academico
        /// </summary>
        /// <param name="seqSolicitacaoServico"></param>
        /// <returns>sequencial da solicitação de serviço</returns>
        long BuscarSeqDivisaoComponentePorSolicitacao(long seqSolicitacaoServico);

    }
}