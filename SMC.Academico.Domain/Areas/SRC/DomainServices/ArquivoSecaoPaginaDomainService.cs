using SMC.Academico.Common.Areas.SRC.Enums;
using SMC.Academico.Common.Areas.SRC.Exceptions;
using SMC.Academico.Domain.Areas.SRC.Models;
using SMC.Academico.Domain.Areas.SRC.Specifications;
using SMC.Academico.Domain.Areas.SRC.ValueObjects;
using SMC.Academico.Domain.Models;
using SMC.Formularios.ServiceContract.Areas.TMP.Interfaces;
using SMC.Framework.Extensions;
using SMC.Framework.Specification;
using SMC.Framework.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SMC.Academico.Domain.Areas.SRC.DomainServices
{
    public class ArquivoSecaoPaginaDomainService : AcademicoContextDomain<ArquivoSecaoPagina>
    {
        #region DomainServices

        private ConfiguracaoEtapaPaginaDomainService ConfiguracaoEtapaPaginaDomainService
        {
            get { return this.Create<ConfiguracaoEtapaPaginaDomainService>(); }
        }

        #endregion DomainServices

        #region Services

        private IPaginaService PaginaService
        {
            get { return this.Create<IPaginaService>(); }
        }

        #endregion

        public bool ValidaConfigurarArquivoSecaoReadOnly(long seqConfiguracaoEtapaPagina)
        {
            var situacao = this.ConfiguracaoEtapaPaginaDomainService.SearchProjectionByKey(
                                       new SMCSeqSpecification<ConfiguracaoEtapaPagina>(seqConfiguracaoEtapaPagina),
                                       x => x.ConfiguracaoEtapa.ProcessoEtapa.SituacaoEtapa);

            if (situacao == SituacaoEtapa.Liberada || situacao == SituacaoEtapa.Encerrada)
                return true;

            return false;
        }

        public List<ArquivoSecaoPaginaVO> BuscarArquivosSecaoPagina(ArquivoSecaoPaginaFiltroVO filtro)
        {
            var spec = filtro.Transform<ArquivoSecaoPaginaFilterSpecification>();
            spec.SetOrderBy(o => o.Ordem);

            var arquivosSecaoPagina = this.SearchBySpecification(spec, x => x.ArquivoAnexado).ToList();

            var retorno = arquivosSecaoPagina.TransformList<ArquivoSecaoPaginaVO>();

            foreach (var itemRetorno in retorno)
            {
                if (itemRetorno.ArquivoAnexado != null)
                {
                    var arquivoAnexadoOrigem = arquivosSecaoPagina.FirstOrDefault(a => a.SeqArquivoAnexado == itemRetorno.SeqArquivoAnexado).ArquivoAnexado;
                    itemRetorno.ArquivoAnexado.GuidFile = arquivoAnexadoOrigem.UidArquivo.ToString();
                }
            }

            return retorno;
        }

        public void SalvarArquivosSecaoPagina(long seqConfiguracaoEtapaPagina, long seqSecaoSGF, List<ArquivoSecaoPaginaVO> secaoArquivos)
        {
            using (var unitOfWork = SMCUnitOfWork.Begin())
            {
                try
                {
                    ValidarModelo(seqConfiguracaoEtapaPagina);

                    var dominio = secaoArquivos.TransformList<ArquivoSecaoPagina>();

                    if (dominio != null)
                    {
                        //VERIFICA SE TODOS POSSUEM UMA ORDEM DIFERENTE 
                        for (int i = 0; i < secaoArquivos.Count; i++)
                        {
                            for (int x = 0; x < secaoArquivos.Count; x++)
                            {
                                if (x != i)
                                {
                                    if (secaoArquivos[i].Ordem == secaoArquivos[x].Ordem)
                                        throw new ArquivosSecaoOrdemRepetidaException();
                                }
                            }
                        }

                        var token = PaginaService.BuscarSecaoPagina(seqSecaoSGF).Token;

                        foreach (var arq in dominio)
                        {
                            EnsureFileIntegrity<ArquivoSecaoPagina, ArquivoAnexado>(arq, x => x.SeqArquivoAnexado, x => x.ArquivoAnexado);
                            arq.TokenSecao = token;
                        }

                        //É MAIS FÁCIL SALVAR OS ARQUIVOS PELA CONFIGURAÇÃO ETAPA PÁGINA POIS JÁ É GERENCIADO QUAIS FORAM DELETADOS OU INSERIDOS
                        //AUTOMATICAMENTE PELO FRAMEWORK, AO INVÉS DE FAZER NA MÃO ESSA VERIFICAÇÃO, DELETAR TODOS OS ARQUIVOS PARA INSERIR DE NOVO
                        var configuracaoEtapaPagina = ConfiguracaoEtapaPaginaDomainService.SearchByKey(new SMCSeqSpecification<ConfiguracaoEtapaPagina>(seqConfiguracaoEtapaPagina));
                        configuracaoEtapaPagina.Arquivos = dominio;

                        ConfiguracaoEtapaPaginaDomainService.SaveEntity(configuracaoEtapaPagina);
                    }
                    unitOfWork.Commit();
                }
                catch (Exception)
                {
                    unitOfWork.Rollback();
                    throw;
                }
            }
        }

        private void ValidarModelo(long seqConfiguracaoEtapaPagina)
        {
            var situacao = this.ConfiguracaoEtapaPaginaDomainService.SearchProjectionByKey(
                           new SMCSeqSpecification<ConfiguracaoEtapaPagina>(seqConfiguracaoEtapaPagina),
                           x => x.ConfiguracaoEtapa.ProcessoEtapa.SituacaoEtapa);

            if (situacao == SituacaoEtapa.Liberada || situacao == SituacaoEtapa.Encerrada)
                throw new ConfiguracaoEtapaOperacaoNaoPermitidaException();
        }
    }
}