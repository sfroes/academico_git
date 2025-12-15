using SMC.Academico.Domain.Areas.SRC.DomainServices;
using SMC.Academico.Domain.Areas.SRC.ValueObjects;
using SMC.Academico.ServiceContract.Areas.SRC.Data;
using SMC.Academico.ServiceContract.Areas.SRC.Interfaces;
using SMC.Framework.Extensions;
using SMC.Framework.Service;
using System.Collections.Generic;

namespace SMC.Academico.Service.Areas.SRC.Services
{
    public class ArquivoSecaoPaginaService : SMCServiceBase, IArquivoSecaoPaginaService
    {
        private ArquivoSecaoPaginaDomainService ArquivoSecaoPaginaDomainService
        {
            get { return this.Create<ArquivoSecaoPaginaDomainService>(); }
        }

        public bool ValidaConfigurarArquivoSecaoReadOnly(long seqConfiguracaoEtapaPagina)
        {
            return this.ArquivoSecaoPaginaDomainService.ValidaConfigurarArquivoSecaoReadOnly(seqConfiguracaoEtapaPagina);
        }

        public List<ArquivoSecaoPaginaData> BuscarArquivosSecaoPagina(ArquivoSecaoPaginaFiltroData filtro)
        {
            return this.ArquivoSecaoPaginaDomainService.BuscarArquivosSecaoPagina(filtro.Transform<ArquivoSecaoPaginaFiltroVO>()).TransformList<ArquivoSecaoPaginaData>();
        }

        public void Salvar(long seqConfiguracaoEtapaPagina, long seqSecaoSGF, List<ArquivoSecaoPaginaData> secaoArquivos)
        {
            this.ArquivoSecaoPaginaDomainService.SalvarArquivosSecaoPagina(seqConfiguracaoEtapaPagina, seqSecaoSGF, secaoArquivos.TransformList<ArquivoSecaoPaginaVO>());
        }
    }
}
