using SMC.Academico.Common.Areas.SRC.Constants;
using SMC.Academico.Common.Areas.SRC.Enums;
using SMC.Academico.Domain.Areas.SRC.DomainServices;
using SMC.Academico.Domain.Areas.SRC.Models;
using SMC.Academico.Domain.Areas.SRC.Specifications;
using SMC.Academico.Domain.Areas.SRC.ValueObjects;
using SMC.Academico.ServiceContract.Areas.SRC.Data;
using SMC.Academico.ServiceContract.Areas.SRC.Interfaces;
using SMC.Framework.Extensions;
using SMC.Framework.Model;
using SMC.Framework.Service;
using System.Collections.Generic;
using System.Linq;

namespace SMC.Academico.Service.Areas.SRC.Services
{
    public class ConfiguracaoEtapaPaginaService : SMCServiceBase, IConfiguracaoEtapaPaginaService
    {
        private ConfiguracaoEtapaPaginaDomainService ConfiguracaoEtapaPaginaDomainService
        {
            get { return this.Create<ConfiguracaoEtapaPaginaDomainService>(); }
        }

        public ConfiguracaoEtapaPaginaData BuscarConfiguracaoEtapaPrimeiraPagina(long seqConfiguracaoEtapa)
        {
            ConfiguracaoEtapaPaginaFilterSpecification spec = new ConfiguracaoEtapaPaginaFilterSpecification()
            {
                SeqConfiguracaoEtapa = seqConfiguracaoEtapa,
            };
            spec.SetOrderBy(p => p.Ordem);
            ConfiguracaoEtapaPagina pagina = ConfiguracaoEtapaPaginaDomainService.SearchBySpecification(spec).FirstOrDefault();
            return pagina.Transform<ConfiguracaoEtapaPaginaData>();
        }

        public bool VerificarConfiguracaoPossuiPaginas(long seqConfiguracaoEtapa)
        {
            return ConfiguracaoEtapaPaginaDomainService.Count(new ConfiguracaoEtapaPaginaFilterSpecification { SeqConfiguracaoEtapa = seqConfiguracaoEtapa }) > 0;
        }

        public bool VerificarConfiguracaoPossuiPaginaAnexarDocumento(long seqConfiguracaoEtapa)
        {
            return ConfiguracaoEtapaPaginaDomainService.Count(new ConfiguracaoEtapaPaginaFilterSpecification { SeqConfiguracaoEtapa = seqConfiguracaoEtapa, Token = TOKEN_SOLICITACAO_SERVICO.SOLICITACAO_PADRAO_SOLICITACAO_UPLOAD_DOCUMENTO }) > 0;
        }

        public bool VerificarConfiguracaoPossuiPaginaRegistrarDocumento(long seqConfiguracaoEtapa)
        {
            return ConfiguracaoEtapaPaginaDomainService.Count(new ConfiguracaoEtapaPaginaFilterSpecification { SeqConfiguracaoEtapa = seqConfiguracaoEtapa, Token = TOKEN_SOLICITACAO_SERVICO.REGISTRO_DOCUMENTO_ENTREGUE }) > 0;
        }

        public bool VerificarConfiguracaoPossuiPaginaConfiguracaoDocumentoUpload(long seqConfiguracaoEtapa)
        {
            var specConfigDocumento = new ConfiguracaoEtapaPaginaFilterSpecification { SeqConfiguracaoEtapa = seqConfiguracaoEtapa, ConfiguracaoDocumento = ConfiguracaoDocumento.UploadDocumento };
            bool configuracaoEtapaPaginaPossuiConfigUploadDocumento = ConfiguracaoEtapaPaginaDomainService.Count(specConfigDocumento) > 0;

            return configuracaoEtapaPaginaPossuiConfigUploadDocumento;
        }

        public bool VerificarConfiguracaoPossuiPaginaConfiguracaoDocumentoRegistro(long seqConfiguracaoEtapa)
        {
            var specConfigDocumento = new ConfiguracaoEtapaPaginaFilterSpecification { SeqConfiguracaoEtapa = seqConfiguracaoEtapa, ConfiguracaoDocumento = ConfiguracaoDocumento.RegistroDocumento };
            bool configuracaoEtapaPaginaPossuiConfigRegistroDocumento = ConfiguracaoEtapaPaginaDomainService.Count(specConfigDocumento) > 0;

            return configuracaoEtapaPaginaPossuiConfigRegistroDocumento;
        }

        public ConfiguracaoEtapaPaginaData BuscarConfiguracaoEtapaPagina(long seqConfiguracaoEtapaPagina)
        {
            return ConfiguracaoEtapaPaginaDomainService.BuscarConfiguracaoEtapaPagina(seqConfiguracaoEtapaPagina).Transform<ConfiguracaoEtapaPaginaData>();
        }
        public List<ConfiguracaoEtapaPaginaData> BuscarConfiguracaoEtapaPaginaPorSeqProcessoEtapa(long seqProcessoEtapa)
        {
            return ConfiguracaoEtapaPaginaDomainService.BuscarConfiguracaoEtapaPaginaPorSeqProcessoEtapa(seqProcessoEtapa).TransformList<ConfiguracaoEtapaPaginaData>();
        }

        public List<SMCDatasourceItem> BuscarPaginasPorEtapa(long seqEtapaSgf)
        {
            return ConfiguracaoEtapaPaginaDomainService.BuscarPaginasPorEtapa(seqEtapaSgf);
        }

        public List<SMCDatasourceItem> BuscarFormularios()
        {
            return ConfiguracaoEtapaPaginaDomainService.BuscarFormularios();
        }

        public List<SMCDatasourceItem> BuscarVisoesPorFormularioSelect(long seqTipoFormulario)
        {
            return ConfiguracaoEtapaPaginaDomainService.BuscarVisoesPorFormularioSelect(seqTipoFormulario);
        }

        public List<NoArvoreConfiguracaoEtapaData> BuscarArvoreConfiguracaoEtapa(ConfiguracaoEtapaPaginaFiltroData filtro)
        {
            return ConfiguracaoEtapaPaginaDomainService.BuscarArvoreConfiguracaoEtapa(filtro.Transform<ConfiguracaoEtapaPaginaFiltroVO>()).TransformList<NoArvoreConfiguracaoEtapaData>();
        }

        public void DuplicarConfiguracaoEtapaPagina(long seqConfiguracaoEtapaPagina)
        {
            this.ConfiguracaoEtapaPaginaDomainService.DuplicarConfiguracaoEtapaPagina(seqConfiguracaoEtapaPagina);
        }

        public void AdicionarPaginas(long seqConfiguracaoEtapa, List<(long, ConfiguracaoDocumento?)> seqsPaginasEConfiguracoesDocumento) //IEnumerable<long> seqPaginasSGF)
        {
            ConfiguracaoEtapaPaginaDomainService.AdicionarPaginas(seqConfiguracaoEtapa, seqsPaginasEConfiguracoesDocumento);
        }

        public long Salvar(ConfiguracaoEtapaPaginaData modelo)
        {
            return ConfiguracaoEtapaPaginaDomainService.Salvar(modelo.Transform<ConfiguracaoEtapaPaginaVO>());
        }

        public void Excluir(long seq)
        {
            ConfiguracaoEtapaPaginaDomainService.Excluir(seq);
        }

        public void ValidarModeloExcluir(long seq)
        {
            ConfiguracaoEtapaPaginaDomainService.ValidarModeloExcluir(seq);
        }

        public List<ConfiguracaoEtapaPaginaData> BuscarConfiguracoesEtapaPaginaPorSeqConfiguracaoEtapa(long seqConfiguracaoEtapa)
        {
            return ConfiguracaoEtapaPaginaDomainService.BuscarConfiguracoesEtapaPaginaPorSeqConfiguracaoEtapa(seqConfiguracaoEtapa).TransformList<ConfiguracaoEtapaPaginaData>();
        }
    }
}