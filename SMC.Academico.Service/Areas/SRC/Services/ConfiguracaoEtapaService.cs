using SMC.Academico.Common.Areas.SRC.Includes;
using SMC.Academico.Domain.Areas.SRC.DomainServices;
using SMC.Academico.Domain.Areas.SRC.Models;
using SMC.Academico.Domain.Areas.SRC.ValueObjects;
using SMC.Academico.ServiceContract.Areas.SRC.Data;
using SMC.Academico.ServiceContract.Areas.SRC.Interfaces;
using SMC.Framework.Extensions;
using SMC.Framework.Model;
using SMC.Framework.Service;
using SMC.Framework.Specification;
using System.Collections.Generic;

namespace SMC.Academico.Service.Areas.SRC.Services
{
    public class ConfiguracaoEtapaService : SMCServiceBase, IConfiguracaoEtapaService
    {
        #region [ DomainService ]

        private ConfiguracaoEtapaDomainService ConfiguracaoEtapaDomainService
        {
            get { return this.Create<ConfiguracaoEtapaDomainService>(); }
        }

        #endregion [ DomainService ]

        public ConfiguracaoEtapaData BuscarConfiguracaoEtapa(long seqConfiguracaoEtapa, IncludesConfiguracaoEtapa includes)
        {
            return ConfiguracaoEtapaDomainService.SearchByKey(new SMCSeqSpecification<ConfiguracaoEtapa>(seqConfiguracaoEtapa), includes).Transform<ConfiguracaoEtapaData>();
        }

        public string BuscarTokenServicoConfiguracaoEtapa(long seqConfiguracaoEtapa)
        {
            var token = ConfiguracaoEtapaDomainService.SearchProjectionByKey(new SMCSeqSpecification<ConfiguracaoEtapa>(seqConfiguracaoEtapa),
                        x => new { x.ConfiguracaoProcesso.Processo.Servico.Token })?.Token;

            return token;
        }

        public CabecalhoConfiguracaoEtapaData BuscarCabecalhoConfiguracaoEtapa(long seqConfiguracaoEtapa)
        {
            return ConfiguracaoEtapaDomainService.BuscarCabecalhoConfiguracaoEtapa(seqConfiguracaoEtapa).Transform<CabecalhoConfiguracaoEtapaData>();
        }

        public SMCPagerData<ConfiguracaoEtapaListarData> BuscarConfiguracoesEtapa(ConfiguracaoEtapaFiltroData filtro)
        {
            return ConfiguracaoEtapaDomainService.BuscarConfiguracoesEtapa(filtro.Transform<ConfiguracaoEtapaFiltroVO>()).Transform<SMCPagerData<ConfiguracaoEtapaListarData>>();
        }

        public List<SMCDatasourceItem> BuscarPaginasNaoCriadas(long seqConfiguracaoEtapa)
        {
            return ConfiguracaoEtapaDomainService.BuscarPaginasNaoCriadas(seqConfiguracaoEtapa);
        }

        public long Salvar(ConfiguracaoEtapaData modelo)
        {
            return ConfiguracaoEtapaDomainService.Salvar(modelo.Transform<ConfiguracaoEtapaVO>());
        }

        public void Excluir(long seq)
        {
            ConfiguracaoEtapaDomainService.Excluir(seq);
        }
    }
}