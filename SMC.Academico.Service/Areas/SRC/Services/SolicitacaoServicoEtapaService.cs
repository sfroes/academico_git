using SMC.Academico.Domain.Areas.SRC.DomainServices;
using SMC.Academico.Domain.Areas.SRC.Models;
using SMC.Academico.ServiceContract.Areas.SRC.Data;
using SMC.Framework.Extensions;
using SMC.Framework.Service;
using System;

namespace SMC.Academico.Service.Areas.SRC.Services
{
    public class SolicitacaoServicoEtapaService : SMCServiceBase, ISolicitacaoServicoEtapaService
    {

        private SolicitacaoServicoEtapaDomainService SolicitacaoServicoEtapaDomainService
        {
            get { return this.Create<SolicitacaoServicoEtapaDomainService>(); }
        }


        public SolicitacaoServicoEtapaData ObterUltimaEtapaIniciadaMatricula(long seqSolicitacaoServico)
        {
            return SolicitacaoServicoEtapaDomainService.ObterUltimaEtapaIniciadaMatricula(seqSolicitacaoServico).Transform<SolicitacaoServicoEtapaData>();
        }

        public void VerificarAcessoEtapa(long seqSolicitacaoServico, long seqConfiguracaoEtapa, bool ignoreFinalizedValidation = false)
        {
            SolicitacaoServicoEtapaDomainService.VerificarAcessoEtapa(seqSolicitacaoServico, seqConfiguracaoEtapa, ignoreFinalizedValidation);
        }
    }
}