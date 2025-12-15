using SMC.Academico.Common.Areas.SRC.Exceptions;
using SMC.Academico.Common.Areas.SRC.Includes;
using SMC.Academico.Domain.Areas.CSO.DomainServices;
using SMC.Academico.Domain.Areas.SRC.Models;
using SMC.Academico.Domain.Areas.SRC.Specifications;
using SMC.Academico.Domain.Areas.SRC.ValueObjects;
using SMC.Formularios.Common.Areas.TMP.Enums;
using SMC.Formularios.ServiceContract.Areas.TMP.Interfaces;
using SMC.Framework.Extensions;
using SMC.Framework.Model;
using SMC.Framework.Specification;
using SMC.Framework.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace SMC.Academico.Domain.Areas.SRC.DomainServices
{
    public class ConfiguracaoProcessoDomainService : AcademicoContextDomain<ConfiguracaoProcesso>
    {
        #region DomainServices

        private CursoOfertaLocalidadeTurnoDomainService CursoOfertaLocalidadeTurnoDomainService
        {
            get { return this.Create<CursoOfertaLocalidadeTurnoDomainService>(); }
        }

        private ProcessoDomainService ProcessoDomainService
        {
            get { return this.Create<ProcessoDomainService>(); }
        }

        private SolicitacaoServicoDomainService SolicitacaoServicoDomainService
        {
            get { return this.Create<SolicitacaoServicoDomainService>(); }
        }

        private ConfiguracaoProcessoCursoDomainService ConfiguracaoProcessoCursoDomainService
        {
            get { return this.Create<ConfiguracaoProcessoCursoDomainService>(); }
        }

        private ConfiguracaoProcessoNivelEnsinoDomainService ConfiguracaoProcessoNivelEnsinoDomainService
        {
            get { return this.Create<ConfiguracaoProcessoNivelEnsinoDomainService>(); }
        }

        private ConfiguracaoProcessoTipoVinculoAlunoDomainService ConfiguracaoProcessoTipoVinculoAlunoDomainService
        {
            get { return this.Create<ConfiguracaoProcessoTipoVinculoAlunoDomainService>(); }
        }

        private ConfiguracaoEtapaDomainService ConfiguracaoEtapaDomainService
        {
            get { return this.Create<ConfiguracaoEtapaDomainService>(); }
        }

        #endregion DomainServices

        #region Services

        private IPaginaService PaginaService
        {
            get { return this.Create<IPaginaService>(); }
        }

        #endregion

        public TResult BuscarProjecaoPorParametros<TResult>(long seqVinculo, long seqEntidadeResponsavel,
            long seqProcesso, long? seqCursoOfertaLocalidadeTurno, long seqNivelEnsino, Expression<Func<ConfiguracaoProcesso, TResult>> projection)
        {
            TResult result = default(TResult);

            var spec = new ConfiguracaoProcessoFilterSpecification(seqVinculo, seqEntidadeResponsavel)
            {
                SeqProcesso = seqProcesso
            };

            // Busca pelas configurações utilizando o curso
            if (seqCursoOfertaLocalidadeTurno.HasValue)
            {
                spec.SeqCursoOfertaLocalidadeTurno = seqCursoOfertaLocalidadeTurno;
                result = SearchProjectionByKey(spec, projection);
            }

            // Se não for encontrado nenhum, tenta buscar pelo nível de ensino.
            if (result == null || result.Equals(default(TResult)))
            {
                spec.SeqCursoOfertaLocalidadeTurno = null;
                spec.SeqNivelEnsino = seqNivelEnsino;
                result = SearchProjectionByKey(spec, projection);
            }

            return result;
        }

        public ConfiguracaoProcessoVO BuscarConfiguracaoProcesso(long seqConfiguracaoProcesso)
        {
            var spec = new SMCSeqSpecification<ConfiguracaoProcesso>(seqConfiguracaoProcesso);

            var configuracaoProcesso = this.SearchByKey(spec,
                                         IncludesConfiguracaoProcesso.Cursos_CursoOfertaLocalidadeTurno_CursoOfertaLocalidade |
                                         IncludesConfiguracaoProcesso.Cursos_CursoOfertaLocalidadeTurno_Turno |
                                         IncludesConfiguracaoProcesso.NiveisEnsino_NivelEnsino |
                                         IncludesConfiguracaoProcesso.TiposVinculoAluno_TipoVinculoAluno);

            var retorno = configuracaoProcesso.Transform<ConfiguracaoProcessoVO>();

            //VALIDAÇÃO PARA AS SEÇÕES NÃO OBRIGATÓRIAS NÃO CHEGAREM COMO LISTAS VAZIAS E SEREM SALVAS COMO OBJETOS VAZIOS
            retorno.NiveisEnsino = retorno.NiveisEnsino.Any() ? retorno.NiveisEnsino : null;
            retorno.Cursos = retorno.Cursos.Any() ? retorno.Cursos : null;

            return retorno;
        }

        public SMCPagerData<ConfiguracaoProcessoListarVO> BuscarConfiguracoesProcesso(ConfiguracaoProcessoFiltroVO filtro)
        {
            var spec = filtro.Transform<ConfiguracaoProcessoFilterSpecification>();

            var lista = this.SearchProjectionBySpecification(spec, a => new ConfiguracaoProcessoListarVO()
            {
                Seq = a.Seq,
                SeqProcesso = a.SeqProcesso,
                Descricao = a.Descricao,
                ProcessoEncerrado = a.Processo.DataEncerramento.HasValue && a.Processo.DataEncerramento.Value < DateTime.Now

            }, out int total).ToList();

            lista.ForEach(a => a.SolicitacaoAssociada = this.SolicitacaoServicoDomainService.Count(new SolicitacaoServicoFilterSpecification() { SeqConfiguracaoProcesso = a.Seq }) > 0);

            return new SMCPagerData<ConfiguracaoProcessoListarVO>(lista, total);
        }

        public List<SMCDatasourceItem> BuscarConfiguracoesProcessoSelect(ConfiguracaoProcessoFiltroVO filtro)
        {
            var spec = filtro.Transform<ConfiguracaoProcessoFilterSpecification>();

            var lista = this.SearchBySpecification(spec);

            return lista.OrderBy(o => o.Descricao).TransformList<SMCDatasourceItem>();
        }

        public long Salvar(ConfiguracaoProcessoVO modelo)
        {
            if (modelo.Cursos != null && modelo.Cursos.Any())
                modelo.Cursos.ForEach(a => a.SeqCursoOfertaLocalidadeTurno = this.CursoOfertaLocalidadeTurnoDomainService.BuscarCursoOfertaLocalidadeTurnoPorCursoOfertaLocalidadeETurno(a.SeqCursoOfertaLocalidade, a.SeqTurno).Seq);

            ValidarModelo(modelo);

            var dominio = modelo.Transform<ConfiguracaoProcesso>();
            var configuracaoProcessoOld = this.SearchByKey(new SMCSeqSpecification<ConfiguracaoProcesso>(modelo.Seq));

            //VALIDAÇÃO PARA AS SEÇÕES NÃO OBRIGATÓRIAS QUE NÃO ESTIVEREM PREENCHIDAS FICAREM COMO LISTAS VAZIAS PARA OS RELACIONAMENTOS (FILHOS) SEREM REMOVIDOS            
            dominio.NiveisEnsino = dominio.NiveisEnsino != null ? dominio.NiveisEnsino : new List<ConfiguracaoProcessoNivelEnsino>();
            dominio.Cursos = dominio.Cursos != null ? dominio.Cursos : new List<ConfiguracaoProcessoCurso>();

            if (modelo.Seq == 0)
            {
                var listaConfiguracaoEtapa = new List<ConfiguracaoEtapa>();
                var processo = this.ProcessoDomainService.SearchByKey(new SMCSeqSpecification<Processo>(modelo.SeqProcesso), IncludesProcesso.Etapas);

                foreach (var processoEtapa in processo.Etapas)
                {
                    ConfiguracaoEtapa configuracaoEtapa = new ConfiguracaoEtapa()
                    {
                        SeqProcessoEtapa = processoEtapa.Seq,
                        Descricao = $"{processoEtapa.Ordem}° Etapa - {dominio.Descricao}",
                        OrientacaoEtapa = null
                    };

                    var listaPaginasEtapa = new List<ConfiguracaoEtapaPagina>();
                    var etapasPagina = this.PaginaService.BuscarPaginasCompletasPorEtapa(processoEtapa.SeqEtapaSgf);

                    foreach (var etapaPagina in etapasPagina)
                    {
                        ConfiguracaoEtapaPagina configuracaoEtapaPagina = new ConfiguracaoEtapaPagina()
                        {
                            SeqPaginaEtapaSgf = etapaPagina.Seq,
                            TokenPagina = etapaPagina.Pagina.Token,
                            Ordem = (short)(etapaPagina.Ordem * 10),
                            TituloPagina = etapaPagina.Pagina.Titulo,
                            SeqFormulario = etapaPagina.SeqFormulario,
                            SeqVisaoFormulario = null
                        };

                        var listaTextosSecaoPagina = new List<TextoSecaoPagina>();

                        foreach (var secaoPagina in etapaPagina.Pagina.Secoes.Where(x => x.TipoSecaoPagina == TipoSecaoPagina.Texto))
                        {
                            TextoSecaoPagina textoSecaoPagina = new TextoSecaoPagina
                            {
                                SeqSecaoPaginaSgf = secaoPagina.Seq,
                                TokenSecao = secaoPagina.Token,
                                Texto = secaoPagina.TextoPadrao
                            };

                            listaTextosSecaoPagina.Add(textoSecaoPagina);
                        }

                        configuracaoEtapaPagina.TextosSecao = listaTextosSecaoPagina;
                        listaPaginasEtapa.Add(configuracaoEtapaPagina);
                    }

                    configuracaoEtapa.ConfiguracoesPagina = listaPaginasEtapa;
                    listaConfiguracaoEtapa.Add(configuracaoEtapa);
                }

                dominio.ConfiguracoesEtapa = listaConfiguracaoEtapa;
            }
            else
            {
                //GARANTINDO QUE NA EDIÇÃO NÃO IRÁ REMOVER AS CONFIGURAÇÕES DE ETAPA, PÁGINA E SEÇÃO
                dominio.ConfiguracoesEtapa = null;
            }

            this.SaveEntity(dominio);

            //ATUALIZANDO DESCRIÇÃO DAS CONFIGURAÇÕES DE ETAPA ASSOCIADAS SE A DESCRIÇÃO DA CONFIGURAÇÃO DE PROCESSO FOI ALTERADA
            if (modelo.Seq != 0 && configuracaoProcessoOld.Descricao != modelo.Descricao)
            {
                var configuracoesEtapa = this.ConfiguracaoEtapaDomainService.SearchBySpecification(new ConfiguracaoEtapaFilterSpecification() { SeqConfiguracaoProcesso = modelo.Seq }, x => x.ProcessoEtapa).ToList();

                foreach (var configuracaoEtapa in configuracoesEtapa)
                {
                    configuracaoEtapa.Descricao = $"{configuracaoEtapa.ProcessoEtapa.Ordem}° Etapa - {modelo.Descricao}";
                    this.ConfiguracaoEtapaDomainService.SaveEntity(configuracaoEtapa);
                }
            }

            return dominio.Seq;
        }

        private void ValidarModelo(ConfiguracaoProcessoVO modelo)
        {
            //É OBRIGATÓRIO UMA DAS DUAS OPÇÕES: NE + TVA ou COLT +TVA

            #region VALIDAÇÕES ANTIGAS, QUANDO NÃO OBRIGAVA NENHUMA SEÇÃO E O USUÁRIO NÃO ESCOLHIA INICIALMENTE QUAL SERIA O CADASTRO

            //if (!modelo.NiveisEnsino.Any() && !modelo.TiposVinculoAluno.Any() && !modelo.Cursos.Any())
            //    throw new ConfiguracaoProcessoObrigatoriaException();

            //if (modelo.NiveisEnsino.Any() && modelo.TiposVinculoAluno.Any() && modelo.Cursos.Any())
            //    throw new ConfiguracaoProcessoJaAssociadaException();

            //if (!modelo.NiveisEnsino.Any() && !modelo.Cursos.Any())
            //    throw new ConfiguracaoProcessoJaAssociadaException();

            //if (modelo.NiveisEnsino.Any() && modelo.Cursos.Any())
            //    throw new ConfiguracaoProcessoJaAssociadaException();

            //if ((modelo.NiveisEnsino.Any() && !modelo.TiposVinculoAluno.Any()))
            //    throw new ConfiguracaoProcessoJaAssociadaException();

            //if ((modelo.Cursos.Any() && !modelo.TiposVinculoAluno.Any()))
            //    throw new ConfiguracaoProcessoJaAssociadaException();

            #endregion

            if ((modelo.NiveisEnsino != null && modelo.NiveisEnsino.Any()) && (modelo.TiposVinculoAluno != null && modelo.TiposVinculoAluno.Any()))
            {
                ConfiguracaoProcessoFilterSpecification spec = new ConfiguracaoProcessoFilterSpecification()
                {
                    SeqProcesso = modelo.SeqProcesso,
                    SeqsNivelEnsino = modelo.NiveisEnsino.Select(a => a.SeqNivelEnsino).ToArray(),
                    SeqsVinculo = modelo.TiposVinculoAluno.Select(a => a.SeqTipoVinculoAluno).ToArray()
                };

                var configuracoesPorNivelEnsinoTipoVinculo = this.SearchBySpecification(spec).ToList();

                if (configuracoesPorNivelEnsinoTipoVinculo.Any(a => a.Seq != modelo.Seq))
                    throw new TipoVinculoNivelEnsinoCursoJaAssociadaException();
            }

            if ((modelo.Cursos != null && modelo.Cursos.Any()) && (modelo.TiposVinculoAluno != null && modelo.TiposVinculoAluno.Any()))
            {
                ConfiguracaoProcessoFilterSpecification spec = new ConfiguracaoProcessoFilterSpecification()
                {
                    SeqProcesso = modelo.SeqProcesso,
                    SeqsCursoOfertaLocalidadeTurno = modelo.Cursos.Select(a => a.SeqCursoOfertaLocalidadeTurno).ToArray(),
                    SeqsVinculo = modelo.TiposVinculoAluno.Select(a => a.SeqTipoVinculoAluno).ToArray()
                };

                var configuracoesPorCursoTipoVinculo = this.SearchBySpecification(spec).ToList();

                if (configuracoesPorCursoTipoVinculo.Any(a => a.Seq != modelo.Seq))
                    throw new TipoVinculoNivelEnsinoCursoJaAssociadaException();
            }
        }

        public void Excluir(long seq)
        {
            using (var unitOfWork = SMCUnitOfWork.Begin())
            {
                try
                {
                    var configToDelete = this.SearchByKey(new SMCSeqSpecification<ConfiguracaoProcesso>(seq));
                    this.DeleteEntity(configToDelete);

                    unitOfWork.Commit();
                }
                catch (Exception)
                {
                    unitOfWork.Rollback();
                    throw;
                }
            }
        }
    }
}
