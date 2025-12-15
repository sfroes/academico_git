using SMC.Academico.Common.Areas.ORG.Enums;
using SMC.Academico.Domain.Areas.ORG.DomainServices;
using SMC.Academico.Domain.Areas.ORG.Models;
using SMC.Academico.ServiceContract.Areas.ORG.Data;
using SMC.Academico.ServiceContract.Areas.ORG.Interfaces;
using SMC.Framework.Extensions;
using SMC.Framework.Service;

namespace SMC.Academico.Service.Areas.ORG.Services
{
    public class InstituicaoNivelModeloRelatorioService : SMCServiceBase, IInstituicaoNivelModeloRelatorioService
    {
        #region Serviço de Dominio

        private InstituicaoNivelModeloRelatorioDomainService InstituicaoNivelModeloRelatorioDomainService
        {
            get { return this.Create<InstituicaoNivelModeloRelatorioDomainService>(); }
        }

        #endregion

        public InstituicaoNivelModeloRelatorioData BuscarInstituicaoNivelModeloRelatorio(long seq)
        {
            return InstituicaoNivelModeloRelatorioDomainService.BuscarInstituicaoNivelModeloRelatorio(seq).Transform<InstituicaoNivelModeloRelatorioData>();
        }

        public InstituicaoNivelModeloRelatorioData BuscarTemplateModeloRelatorio(long seqInstituicaoNivel, ModeloRelatorio modeloRelatorio)
        {
            var obj = InstituicaoNivelModeloRelatorioDomainService.BuscarTemplateModeloRelatorio(seqInstituicaoNivel, modeloRelatorio);
            var dto = obj.Transform<InstituicaoNivelModeloRelatorioData>();
            return dto;
        }
        public InstituicaoNivelModeloRelatorioData VerificarTemplateModeloRelatorio(short? numeroDiasAutorizacaoParcial, long seqInstituicaoNivel)
        {
            if (numeroDiasAutorizacaoParcial > 0)
            {
                return BuscarTemplateModeloRelatorio(seqInstituicaoNivel, ModeloRelatorio.AutorizacaoPublicacaoBDP_ParcialECompleto);
            }
            return BuscarTemplateModeloRelatorio(seqInstituicaoNivel, ModeloRelatorio.AutorizacaoPublicacaoBDP_Completo); 
        }
        public long SalvarInstituicaoNivelModeloRelatorio(InstituicaoNivelModeloRelatorioData modelo)
        {
            var modeloRelatorio = modelo.Transform<InstituicaoNivelModeloRelatorio>();
            return InstituicaoNivelModeloRelatorioDomainService.SalvarInstituicaoNivelModelo(modeloRelatorio);
        }
    }
}