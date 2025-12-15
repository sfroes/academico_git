using SMC.Academico.Domain.Areas.SRC.DomainServices;
using SMC.Academico.Domain.Areas.SRC.ValueObjects;
using SMC.Academico.ServiceContract.Areas.SRC.Data;
using SMC.Academico.ServiceContract.Areas.SRC.Interfaces;
using SMC.Framework.Extensions;
using SMC.Framework.Model;
using SMC.Framework.Service;
using System.Collections.Generic;

namespace SMC.Academico.Service.Areas.SRC.Services
{
    public class ConfiguracaoProcessoService : SMCServiceBase, IConfiguracaoProcessoService
    {
        #region [ DomainService ]

        private ConfiguracaoProcessoDomainService ConfiguracaoProcessoDomainService
        {
            get { return this.Create<ConfiguracaoProcessoDomainService>(); }
        }

        #endregion [ DomainService ]

        public ConfiguracaoProcessoData BuscarConfiguracaoProcesso(long seqConfiguracaoProcesso)
        {
            return this.ConfiguracaoProcessoDomainService.BuscarConfiguracaoProcesso(seqConfiguracaoProcesso).Transform<ConfiguracaoProcessoData>();
        }

        public SMCPagerData<ConfiguracaoProcessoListarData> BuscarConfiguracoesProcesso(ConfiguracaoProcessoFiltroData filtro)
        {
            return this.ConfiguracaoProcessoDomainService.BuscarConfiguracoesProcesso(filtro.Transform<ConfiguracaoProcessoFiltroVO>()).Transform<SMCPagerData<ConfiguracaoProcessoListarData>>();
        }

        public List<SMCDatasourceItem> BuscarConfiguracoesProcessoSelect(ConfiguracaoProcessoFiltroData filtro)
        {
            return this.ConfiguracaoProcessoDomainService.BuscarConfiguracoesProcessoSelect(filtro.Transform<ConfiguracaoProcessoFiltroVO>());
        }

        public long Salvar(ConfiguracaoProcessoData modelo)
        {
            return this.ConfiguracaoProcessoDomainService.Salvar(modelo.Transform<ConfiguracaoProcessoVO>());
        }

        public void Excluir(long seq)
        {
            this.ConfiguracaoProcessoDomainService.Excluir(seq);
        }        
    }
}
