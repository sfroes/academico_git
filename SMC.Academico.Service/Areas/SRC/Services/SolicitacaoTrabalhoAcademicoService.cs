using SMC.Academico.Domain.Areas.SRC.DomainServices;
using SMC.Academico.Domain.Areas.SRC.Models;
using SMC.Academico.ServiceContract.Areas.SRC.Data;
using SMC.Academico.ServiceContract.Areas.SRC.Interfaces;
using SMC.Framework.Extensions;
using SMC.Framework.Service;
using System;

namespace SMC.Academico.Service.Areas.SRC.Services
{
    public class SolicitacaoTrabalhoAcademicoService : SMCServiceBase, ISolicitacaoTrabalhoAcademicoService
    {

        private SolicitacaoTrabalhoAcademicoDomainService SolicitacaoTrabalhoAcademicoDomainService
        {
            get { return this.Create<SolicitacaoTrabalhoAcademicoDomainService>(); }
        }


        public long BuscarSeqDivisaoComponentePorSolicitacao(long seqSolicitacaoServico)
        {
            return SolicitacaoTrabalhoAcademicoDomainService.BuscarSeqDivisaoComponentePorSolicitacao(seqSolicitacaoServico);
        }


    }
}