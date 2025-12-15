using SMC.Academico.Common.Areas.ORG.Enums;
using SMC.Academico.Domain.Areas.ORG.DomainServices;
using SMC.Academico.Domain.Areas.ORG.Models;
using SMC.Academico.Domain.Areas.ORG.ValueObjects;
using SMC.Academico.ServiceContract.Areas.ORG.Data;
using SMC.Academico.ServiceContract.Areas.ORG.Interfaces;
using SMC.Framework.Extensions;
using SMC.Framework.Model;
using SMC.Framework.Service;

namespace SMC.Academico.Service.Areas.ORG.Services
{
    public class InstituicaoModeloRelatorioService : SMCServiceBase, IInstituicaoModeloRelatorioService
    {
        #region Serviço de Dominio

        private InstituicaoModeloRelatorioDomainService InstituicaoModeloRelatorioDomainService
        {
            get { return this.Create<InstituicaoModeloRelatorioDomainService>(); }
        }

        #endregion

        public InstituicaoModeloRelatorioData BuscarInstituicaoModeloRelatorio(long seq)
        {
            return InstituicaoModeloRelatorioDomainService.BuscarInstituicaoModeloRelatorio(seq).Transform<InstituicaoModeloRelatorioData>();
        }

        public InstituicaoModeloRelatorioData BuscarTemplateModeloRelatorio(long seqInstituicaoEnsino, ModeloRelatorio modeloRelatorio)
        {
            var obj = InstituicaoModeloRelatorioDomainService.BuscarTemplateModeloRelatorio(seqInstituicaoEnsino, modeloRelatorio);
            var dto = obj.Transform<InstituicaoModeloRelatorioData>();
            return dto;
        }

        public SMCPagerData<InstituicaoModeloRelatorioListarData> BuscarInstituicaoModeloRelatorios(InstituicaoModeloRelatorioFiltroData filtro)
        {
            return InstituicaoModeloRelatorioDomainService.BuscarInstituicaoModeloRelatorios(filtro.Transform<InstituicaoModeloRelatorioFiltroVO>()).Transform<SMCPagerData<InstituicaoModeloRelatorioListarData>>();
        }

        public long SalvarModeloRelatorio(InstituicaoModeloRelatorioData modelo)
        {
            var modeloRelatorio = modelo.Transform<InstituicaoModeloRelatorio>();
            return InstituicaoModeloRelatorioDomainService.SalvarModeloRelatorio(modeloRelatorio);
        }
    }
}