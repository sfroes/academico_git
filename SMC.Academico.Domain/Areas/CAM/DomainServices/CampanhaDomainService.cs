using SMC.Academico.Common.Areas.CAM.Exceptions;
using SMC.Academico.Common.Areas.CAM.Includes;
using SMC.Academico.Common.Constants;
using SMC.Academico.Domain.Areas.ALN.DomainServices;
using SMC.Academico.Domain.Areas.ALN.Models;
using SMC.Academico.Domain.Areas.CAM.Models;
using SMC.Academico.Domain.Areas.CAM.Specifications;
using SMC.Academico.Domain.Areas.CAM.ValueObjects;
using SMC.Academico.Domain.Areas.ORG.DomainServices;
using SMC.Academico.Domain.Areas.ORG.Models;
using SMC.Academico.Domain.Areas.TUR.DomainServices;
using SMC.Framework;
using SMC.Framework.Extensions;
using SMC.Framework.Model;
using SMC.Framework.Specification;
using SMC.Framework.UnitOfWork;
using SMC.Inscricoes.Common;
using SMC.Inscricoes.Common.Areas.INS.Constants;
using SMC.Inscricoes.ServiceContract.Areas.INS.Data;
using SMC.Inscricoes.ServiceContract.Areas.INS.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SMC.Academico.Domain.Areas.CAM.DomainServices
{
    public class CampanhaDomainService : AcademicoContextDomain<Campanha>
    {
        #region DomainServices

        private ConvocadoOfertaDomainService ConvocadoOfertaDomainService
        {
            get { return this.Create<ConvocadoOfertaDomainService>(); }
        }

        private CicloLetivoDomainService CicloLetivoDomainService
        {
            get { return this.Create<CicloLetivoDomainService>(); }
        }

        private ProcessoSeletivoDomainService ProcessoSeletivoDomainService
        {
            get { return this.Create<ProcessoSeletivoDomainService>(); }
        }

        private InstituicaoNivelTipoProcessoSeletivoDomainService InstituicaoNivelTipoProcessoSeletivoDomainService
        {
            get { return this.Create<InstituicaoNivelTipoProcessoSeletivoDomainService>(); }
        }

        private CampanhaOfertaDomainService CampanhaOfertaDomainService
        {
            get { return this.Create<CampanhaOfertaDomainService>(); }
        }

        private CampanhaOfertaItemDomainService CampanhaOfertaItemDomainService
        {
            get { return this.Create<CampanhaOfertaItemDomainService>(); }
        }

        private ConfiguracaoEventoLetivoDomainService ConfiguracaoEventoLetivoDomainService
        {
            get { return this.Create<ConfiguracaoEventoLetivoDomainService>(); }
        }

        private IngressanteOfertaDomainService IngressanteOfertaDomainService
        {
            get { return this.Create<IngressanteOfertaDomainService>(); }
        }

        private ProcessoSeletivoOfertaDomainService ProcessoSeletivoOfertaDomainService
        {
            get { return this.Create<ProcessoSeletivoOfertaDomainService>(); }
        }

        private EntidadeDomainService EntidadeDomainService => Create<EntidadeDomainService>();

        private ConvocacaoDomainService ConvocacaoDomainService => Create<ConvocacaoDomainService>();

        private TipoOfertaDomainService TipoOfertaDomainService => Create<TipoOfertaDomainService>();

        private ConvocacaoOfertaDomainService ConvocacaoOfertaDomainService => Create<ConvocacaoOfertaDomainService>();

        private CampanhaCicloLetivoDomainService CampanhaCicloLetivoDomainService => Create<CampanhaCicloLetivoDomainService>();

        private TipoOfertaUnidadeResponsavelDomainService TipoOfertaUnidadeResponsavelDomainService => Create<TipoOfertaUnidadeResponsavelDomainService>();

        private TurmaDomainService TurmaDomainService => Create<TurmaDomainService>();

        #endregion DomainServices

        #region Serviços

        private IProcessoService ProcessoService
        {
            get { return this.Create<IProcessoService>(); }
        }

        private IItemHierarquiaOfertaService ItemHierarquiaOfertaService
        {
            get { return this.Create<IItemHierarquiaOfertaService>(); }
        }

        private IIntegracaoService IntegracaoService
        {
            get { return this.Create<IIntegracaoService>(); }
        }

        #endregion Serviços

        /// <summary>
        /// Busca as camanhas que atendam aos filtros.
        /// </summary>
        /// <param name="filtros">Filtros da busca</param>
        /// <returns>Dados das campanhas encontradas</returns>
        public SMCPagerData<Campanha> BuscarCampanhas(CampanhaFilterSpecification filtros)
        {
            //De acordo com a regra, o resultado de pesquisa deve retornar as campanhas
            //ordenadas por ano e número do ciclo letivo, decrescentes e por descrição.
            //caso não tenha nenhuma coluna para ordenar, usar ordenação padrão
            if (filtros.OrderByClauses.Count == 0)
            {
                filtros.SetOrderByDescending(o => o.CiclosLetivos.FirstOrDefault().CicloLetivo.Ano);
                filtros.SetOrderByDescending(o => o.CiclosLetivos.FirstOrDefault().CicloLetivo.Numero);
                filtros.SetOrderBy(o => o.Descricao);
            }
            var campanhas = SearchBySpecification(filtros, out int total, IncludesCampanha.CiclosLetivos_CicloLetivo);

            return new SMCPagerData<Campanha>(campanhas, total);
        }

        /// <summary>
        /// Busca uma campanha pelo sequencial do processo seletivo
        /// </summary>
        /// <param name="seqProcessoSeletivo">Sequencial do processo seletivo.</param>
        /// <returns></returns>
        public CampanhaVO BuscarCampanhaProcessoSeletivo(long seqProcessoSeletivo)
        {
            var filtro = new CampanhaFilterSpecification() { SeqProcessoSeletivo = seqProcessoSeletivo };
            var campanha = SearchProjectionByKey(filtro, x => new CampanhaVO()
            {
                Seq = x.Seq,
                Descricao = x.Descricao
            });

            return campanha;
        }

        public CampanhaCopiaVO BuscarCampanhaOrigem(long seqCampamhaOrigem)
        {
            var spec = new CampanhaFilterSpecification() { Seq = seqCampamhaOrigem };

            // Verifica se não há descrições repetidas
            var campanhaOrigem = SearchProjectionByKey(spec, x => new CampanhaCopiaVO()
            {
                SeqCampanhaOrigem = x.Seq,
                DescricaoCampanhaOrigem = x.Descricao,
                CiclosLetivosCampanhaOrigem = x.CiclosLetivos.Select(c => c.CicloLetivo.Numero + "/" + c.CicloLetivo.Ano).ToList()
            });

            return campanhaOrigem;
        }

        /// <summary>
        /// RN_CAM_002 Consistência Campanha        Inclusão/Alteração
        /// Verificar se existe outra campanha com a mesma descrição.
        /// Em caso afirmativo, abortar a operação e emitir a mensagem de erro:
        /// "<Inclusão/Alteração> não permitida. Já existe outra campanha com a mesma descrição."
        /// Verificar se existe alguma outra campanha cadastrada para a unidade responsável da campanha em questão no mesmo ciclo letivo.
        /// Em caso afirmativo, abortar a operação e emitir a mensagem de erro:
        /// "<Inclusão/Alteração> não permitida. Existe outra campanha para <descrição da unidade responsável> no(s) ciclo(s) letivo(s) informado(s)."
        /// </summary>
        /// <param name="campanha"></param>
        /// <returns></returns>
        public long SalvarCampanha(Campanha campanha)
        {
            //Verificar se existe outra campanha com a mesma descrição.
            ValidarDescricaoCampanha(campanha.Seq, campanha.Descricao);

            //Verificar se existe alguma outra campanha cadastrada para a unidade responsável da campanha em questão no mesmo ciclo letivo.
            ValidarCampanhaUnidadeResponsavelCicloLetivo(campanha);

            SaveEntity(campanha);
            return campanha.Seq;
        }

        /// <summary>
        /// Verificar se existe alguma outra campanha cadastrada para a unidade responsável da campanha em questão no mesmo ciclo letivo.
        /// Em caso afirmativo, abortar a operação e emitir a mensagem de erro:
        /// "<Inclusão/Alteração> não permitida. Existe outra campanha para <descrição da unidade responsável> no(s) ciclo(s) letivo(s) informado(s)."
        /// </summary>
        /// <param name="campanha"></param>
        private void ValidarCampanhaUnidadeResponsavelCicloLetivo(Campanha campanha)
        {
            var seqsCicloLetivo = campanha.CiclosLetivos?.Select(x => x.SeqCicloLetivo).ToArray();
            var spec = new CampanhaFilterSpecification() { SeqEntidadeResponsavel = campanha.SeqEntidadeResponsavel, SeqsCicloLetivo = seqsCicloLetivo };

            var resultado = SearchBySpecification(spec);

            // Verificar se existe alguma outra campanha cadastrada para a unidade responsável da campanha em questão no mesmo ciclo letivo.
            if (resultado.SMCAny() && resultado.Count(x => x.Seq != campanha.Seq) > 0)
            {
                var entidadeResponsavel = EntidadeDomainService.SearchProjectionByKey(new SMCSeqSpecification<Entidade>(campanha.SeqEntidadeResponsavel), x => x.Nome);
                throw new CampanhaUnidadeResponsavelCicloLetivoRepetidoException(entidadeResponsavel);
            }
        }

        /// <summary>
        /// Busca as camanhas que atendam aos filtros para lookup.
        /// </summary>
        /// <param name="filtros">Filtros da busca</param>
        /// <returns>Dados das campanhas encontradas</returns>
        public SMCPagerData<Campanha> BuscarCampanhasLookup(CampanhaFilterSpecification filtros)
        {
            int total = 0;
            filtros.SetOrderByDescending(o => o.CiclosLetivos.FirstOrDefault().CicloLetivo.AnoNumeroCicloLetivo);
            filtros.SetOrderBy(o => o.Descricao);

            var campanhas = SearchBySpecification(filtros, out total, IncludesCampanha.CiclosLetivos_CicloLetivo);

            return new SMCPagerData<Campanha>(campanhas, total);
        }

        /// <summary>
        /// Busca as camanhas que atendam aos filtros
        /// </summary>
        /// <param name="filtros">Filtros da busca</param>
        /// <returns>Dados das campanhas encontradas</returns>
        public List<SMCDatasourceItem> BuscarCampanhasSelect(CampanhaFilterSpecification filtros)
        {
            filtros.SetOrderBy(o => o.Descricao);

            return SearchProjectionBySpecification(filtros, p => new SMCDatasourceItem()
            {
                Seq = p.Seq,
                Descricao = p.Descricao
            }).ToList();
        }

        public SMCPagerData<CampanhaConsultarCandidatoListaVO> BuscarCandidatosCampanha(CampanhaConsultarCandidatoFiltroVO filtros)
        {
            //Cria a lista que será retornada
            var candidatos = new List<CampanhaConsultarCandidatoListaVO>();

            //Cria o specification para ser usado no filtro
            var spec = new SMCSeqSpecification<Campanha>(filtros.SeqCampanha);

            //Retorna os dados da campanha baseados nos filtros informados
            var campanha = this.SearchByKey(spec, IncludesCampanha.ProcessosSeletivos_Convocacoes | IncludesCampanha.CiclosLetivos);

            //Retorna os seqs dos processos seletivos do GPI
            var seqsProcessosSeletivosGPI = campanha.ProcessosSeletivos.Where(f =>
                                                            f.SeqProcessoGpi.HasValue &&
                                                            (!filtros.SeqConvocacao.HasValue || f.Convocacoes.Any(x => x.Seq == filtros.SeqConvocacao))
                                                        ).Select(p => p.SeqProcessoGpi.Value).ToArray();

            //Cria o filtro para ser passado para o GPI
            var filtroGpi = new InscritoProcessoIntegracaoFiltroData
            {
                SeqProcessos = seqsProcessosSeletivosGPI,
                Exportado = filtros.Exportado
            };

            //Retorna os inscritos do GPI
            var inscritosProcessoGPI = this.IntegracaoService.BuscarInscritosProcesso(filtroGpi);

            //Caso tenha algum inscrito no GPI
            if (inscritosProcessoGPI.Count() > 0)
            {
                //Recupera todos os seqs das inscrições ofertas do GPI
                var seqsIncricaoOfertaGpi = inscritosProcessoGPI.Select(i => i.SeqInscricaoOferta).ToArray();

                //Recupera todos os seqs das hierarquias ofertas do GPI
                var seqsHierarquiaOfertaGpi = inscritosProcessoGPI.Select(i => i.SeqHierarquiaOferta).ToArray();

                //Cria o spec para filtrar os dados de ConvocadoOferta com os seqs dos GPI recuperados
                var specConvocadoOferta = new SMCContainsSpecification<ConvocadoOferta, long>(c => c.SeqInscricaoOfertaGpi.Value, seqsIncricaoOfertaGpi);

                //Filtra os dados de ConvocadosOferta
                var convocadosOferta = this.ConvocadoOfertaDomainService.SearchBySpecification(specConvocadoOferta, IncludesConvocadoOferta.Chamada_Convocacao_ProcessoSeletivo).ToList();

                //Cria o spec para filtrar os dados de IngressanteOferta com os seqs dos GPI recuperados
                var specIngressanteOferta = new SMCContainsSpecification<IngressanteOferta, long>(i => i.SeqInscricaoOfertaGpi.Value, seqsIncricaoOfertaGpi);

                //Filtra os dados de IngressanteOferta
                var ingressantesOferta = this.IngressanteOfertaDomainService.SearchBySpecification(specIngressanteOferta, IncludesIngressanteOferta.Ingressante_HistoricosSituacao | IncludesIngressanteOferta.CampanhaOferta).ToList();

                //Cria o spec para filtrar os dados de ProcessoSeletivioOferta
                var specProcessoSeletivoOferta = new SMCContainsSpecification<ProcessoSeletivoOferta, long>(p => p.SeqHierarquiaOfertaGpi.Value, seqsHierarquiaOfertaGpi);

                //Filtra os dados de ProcessoSeletivoOferta
                var processosSeletivosOferta = this.ProcessoSeletivoOfertaDomainService.SearchBySpecification(specProcessoSeletivoOferta, IncludesProcessoSeletivoOferta.CampanhaOferta | IncludesProcessoSeletivoOferta.ProcessoSeletivo).ToList();

                //Para cada inscrito no GPI
                foreach (var inscritoProcessoGPI in inscritosProcessoGPI)
                {
                    //Racupera os dados do convocado oferta
                    var convocadoOferta = convocadosOferta.FirstOrDefault(c => c.SeqInscricaoOfertaGpi == inscritoProcessoGPI.SeqInscricaoOferta);

                    //Recupera os dados do ingressante oferta
                    var ingressanteOferta = ingressantesOferta.FirstOrDefault(c => c.SeqInscricaoOfertaGpi == inscritoProcessoGPI.SeqInscricaoOferta);

                    //Recupera os dados do processo seletivo oferta
                    var processoSeletivoOferta = processosSeletivosOferta.FirstOrDefault(c => c.SeqHierarquiaOfertaGpi == inscritoProcessoGPI.SeqHierarquiaOferta);

                    //Adiciona o objeto na lista para retorno
                    candidatos.Add(new CampanhaConsultarCandidatoListaVO()
                    {
                        SeqInscricao = inscritoProcessoGPI.SeqInscricao,
                        Candidato = inscritoProcessoGPI.Nome,
                        ProcessoSeletivo = processoSeletivoOferta?.ProcessoSeletivo?.Descricao,
                        Oferta = processoSeletivoOferta?.CampanhaOferta?.Descricao,
                        NumeroChamada = convocadoOferta?.Chamada?.Numero,
                        TipoChamada = convocadoOferta?.Chamada?.TipoChamada,
                        DataSituacaoIngressante = ingressanteOferta?.Ingressante?.HistoricosSituacao?.OrderByDescending(h => h.Seq).FirstOrDefault().DataInclusao,
                        SituacaoIngressante = ingressanteOferta?.Ingressante?.HistoricosSituacao?.OrderByDescending(h => h.Seq).FirstOrDefault().SituacaoIngressante,
                        DataCadastroIngressante = ingressanteOferta?.Ingressante?.DataInclusao,
                        SeqChamada = convocadoOferta?.SeqChamada,
                        SeqsCiclosLetivos = campanha?.CiclosLetivos?.Select(c => (long?)c.SeqCicloLetivo).ToList(),
                        SeqConvocacao = convocadoOferta?.Chamada?.SeqConvocacao,
                        SeqProcessoSeletivo = processoSeletivoOferta?.ProcessoSeletivo?.Seq,
                        SeqTipoProcessoSeletivo = processoSeletivoOferta?.ProcessoSeletivo?.SeqTipoProcessoSeletivo,
                    });
                }
            }
            //}

            IEnumerable<CampanhaConsultarCandidatoListaVO> retorno = candidatos;

            if (!string.IsNullOrEmpty(filtros.OfertaCampanha))
                retorno = retorno.Where(c => c.Oferta.ToUpper().Contains(filtros.OfertaCampanha?.ToUpper()));
            if (filtros.SeqChamada.HasValue)
                retorno = retorno.Where(c => c.SeqChamada == filtros.SeqChamada);
            if (filtros.SeqCicloLetivo.HasValue)
                retorno = retorno.Where(c => c.SeqsCiclosLetivos.Contains(filtros.SeqCicloLetivo));
            if (filtros.SeqProcessoSeletivo.HasValue)
                retorno = retorno.Where(c => c.SeqProcessoSeletivo == filtros.SeqProcessoSeletivo);
            if (filtros.SeqTipoProcessoSeletivo.HasValue)
                retorno = retorno.Where(c => c.SeqTipoProcessoSeletivo == filtros.SeqTipoProcessoSeletivo);
            if (filtros.TipoChamada.HasValue)
                retorno = retorno.Where(c => c.TipoChamada == filtros.TipoChamada);

            var total = retorno.Count();

            retorno = retorno.OrderBy(c => c.Candidato)
                                .ThenBy(c => c.Oferta)
                                .Skip((filtros.PageSettings.PageIndex - 1) * filtros.PageSettings.PageSize)
                                .Take(filtros.PageSettings.PageSize);

            return new SMCPagerData<CampanhaConsultarCandidatoListaVO>(retorno, total);
        }

        #region Validações da Copia de Campanha

        //RN_CAM_002 - Consistência campanha
        public void ValidarCampanhaCopiaCampanha(long seqCampanha, string descricaoCampanha, List<long> seqsCiclosLetivos)
        {
            ValidarDescricaoCampanha(0, descricaoCampanha);

            ValidarCampanhaUnidadeResponsavelCicloLetivo(seqCampanha, seqsCiclosLetivos);
        }

        //RN_CAM_073 - Consistência da cópia da campanha - Ofertas da campanha
        //RN_CAM_063 - Consistência da seleção de oferta
        public void ValidarOfertaCopiaCampanha(long seqCampanha, List<long> seqsCiclosLetivos, List<long> seqsOfertas)
        {
            ValidarVinculoTipoOfertaUnidadeResponsavel(seqCampanha, seqsOfertas);

            var specOferta = new CampanhaOfertaFilterSpecification() { Seqs = seqsOfertas.ToArray() };

            //Recupera as ofertas selecionadas
            var ofertas = this.CampanhaOfertaDomainService.SearchProjectionBySpecification(specOferta, o => new
            {
                o.Seq,
                o.Descricao,
                o.TipoOferta,
                SeqCampanha = seqCampanha,
                o.Itens.FirstOrDefault().SeqCursoOfertaLocalidadeTurno,
                o.Itens.FirstOrDefault().SeqTurma,
                o.Itens.FirstOrDefault().SeqFormacaoEspecifica
            }).ToList();

            foreach (var oferta in ofertas)
            {
                var campanhaOferta = new CampanhaOfertaVO()
                {
                    Seq = oferta.Seq,
                    Descricao = oferta.Descricao,
                    SeqCampanha = oferta.SeqCampanha,
                    SeqCursoOfertaLocalidadeTurno = oferta.SeqCursoOfertaLocalidadeTurno,
                    SeqTurma = oferta.SeqTurma,
                    SeqFormacaoEspecifica = oferta.SeqFormacaoEspecifica
                };

                CampanhaOfertaDomainService.ValidarConsistenciaOfertaCampanha(campanhaOferta, oferta.TipoOferta);
            }
        }

        //RN_CAM_078 - Consistência da cópia do processo seletivo
        public void ValidarProcessoSeletivoCopiaCampanha(List<long> seqsProcessosSeletivos)
        {
            //Se o tipo de processo seletivo não estiver mais associado a todos os níveis de ensino,
            //ao vínculo e à forma de ingresso do processo seletivo selecionado, abortar a operação e
            //emitir a mensagem de erro:
            //
            //O processo "descrição do processo seletivo" não pode ser copiado.Seu tipo de processo não
            //está mais ativo para os níveis de ensino, o vínculo e / ou a forma de ingresso do processo.
            //
            //Observação: o vínculo e a forma de ingresso podem não ter sido preenchidos no processo de origem.
            //Nesse caso, eles não deverão ser verificados nessa regra.

            var specProcessoSeletivo = new ProcessoSeletivoFilterSpecification() { SeqsProcessosSeletivos = seqsProcessosSeletivos.ToArray() };

            var processosSeletivos = ProcessoSeletivoDomainService.SearchProjectionBySpecification(specProcessoSeletivo, p => new
            {
                p.Seq,
                p.Descricao,
                p.SeqTipoProcessoSeletivo,
                SeqsNiveisEnsino = p.NiveisEnsino.Select(n => n.SeqNivelEnsino).ToList(),
                p.SeqFormaIngresso,
                p.Campanha.EntidadeResponsavel.SeqInstituicaoEnsino,
                p.SeqTipoVinculoAluno
            }).ToList();

            foreach (var processoSeletivo in processosSeletivos)
            {
                var specInstituicaoNivelTipoProcessoSeletivo = new InstituicaoNivelTipoProcessoSeletivoFilterSpecification()
                {
                    SeqTipoProcessoSeletivo = processoSeletivo.SeqTipoProcessoSeletivo,
                    SeqFormaIngresso = processoSeletivo.SeqFormaIngresso,
                    SeqsNiveisEnsino = processoSeletivo.SeqsNiveisEnsino,
                    SeqInstituicaoEnsino = processoSeletivo.SeqInstituicaoEnsino,
                    SeqTipoVinculoAluno = processoSeletivo.SeqTipoVinculoAluno
                };

                if (InstituicaoNivelTipoProcessoSeletivoDomainService.Count(specInstituicaoNivelTipoProcessoSeletivo) == 0)
                    throw new CampanhaTipoProcessoNaoVilculadoNivelEnsinoVinculoFormaIngressoException(processoSeletivo.Descricao);
            }
        }

        //RN_CAM_079 - Consistência da cópia do processo GPI
        public void ValidarProcessoGPICopiaCampanha(List<long> seqsOfertas, List<CampanhaCopiaProcessoSeletivoListaVO> processosSeletivosSelecionados)
        {
            //O período de inscrição das ofertas não pode ultrapassar o período da etapa de inscrição
            foreach (var processoSeletivoSelecionado in processosSeletivosSelecionados)
            {
                var etapaInscricaoSelecionada = processoSeletivoSelecionado.EtapasGPI.FirstOrDefault(e => e.Token == TOKENS.ETAPA_INSCRICAO && e.Checked);

                if (etapaInscricaoSelecionada != null)
                {
                    var dataInicioInscricao = processoSeletivoSelecionado.DataInicioInscricaoProcessoGPI;
                    var dataFimInscricao = processoSeletivoSelecionado.DataFimInscricaoProcessoGPI;

                    var dataInicioEtapaInscricao = etapaInscricaoSelecionada.DataInicioEtapa;
                    var dataFimEtapaInscricao = etapaInscricaoSelecionada.DataFimEtapa;

                    if (dataInicioInscricao < dataInicioEtapaInscricao || dataFimInscricao > dataFimEtapaInscricao)
                        throw new CampanhaPeriodoInscricaoOfertaMaiorPeriodoInscricaoEtapaException();
                }
            }

            //Recupera os sequenciais dos processos seletivos para serem copiados do GPI
            var seqsProcessosSeletivosCopiaGpi = processosSeletivosSelecionados.Select(p => p.Seq).ToList();

            //Spec para recuperar os processos seletivos para serem copiados do GPI
            var specProcessoSeletivoSelecionados = new ProcessoSeletivoFilterSpecification() { SeqsProcessosSeletivos = seqsProcessosSeletivosCopiaGpi.ToArray() };

            //Prepara o objeto para as validações
            var processosSeletivosCopiaGpi = this.ProcessoSeletivoDomainService.SearchProjectionBySpecification(specProcessoSeletivoSelecionados, p => new
            {
                p.Seq,
                p.Descricao,
                SeqsTiposProcessosGpi = p.TipoProcessoSeletivo.TiposProcessosGpi.Select(t => t.SeqTipoProcessoGpi).ToList(),
                p.SeqProcessoGpi,
                p.Campanha.EntidadeResponsavel.SeqUnidadeResponsavelGpi,
            }).ToList();

            //Para buscar os dados do Gpi, recupera os sequenciais das unidades responsaveis e os sequenciais dos
            //processos seletivos do Gpi
            var seqsUnidadesResponsaveisGpi = processosSeletivosCopiaGpi.Where(p => p.SeqUnidadeResponsavelGpi.HasValue).Select(p => p.SeqUnidadeResponsavelGpi.GetValueOrDefault()).Distinct().ToArray();
            var seqsProcessosSeletivosGpi = processosSeletivosCopiaGpi.Where(p => p.SeqProcessoGpi.HasValue).Select(p => p.SeqProcessoGpi.GetValueOrDefault()).Distinct().ToArray();

            //Caso possua alguma unidade responsavel vilculada e algum processo seletivo GPI vinculado
            if (seqsUnidadesResponsaveisGpi.Any() && seqsProcessosSeletivosGpi.Any())
            {
                //Recupera do Gpi as situações dos tipos dos processos nas unidades responsaveis
                var situacoesTiposHierarquiasTiposProcessosUnidadesResponsaveis = this.ProcessoService.BuscarSituacoesCopiaCampanhaProcessoGpi(seqsUnidadesResponsaveisGpi, seqsProcessosSeletivosGpi).ToList();

                //Para cada processo marcado para ser copiado
                foreach (var processoSeletivoCopiaGpi in processosSeletivosCopiaGpi)
                {
                    //Se o tipo de processo do processo do GPI não estiver mais associado ao tipo de processo
                    //do processo seletivo selecionado, abortar a operação.
                    if (processoSeletivoCopiaGpi.SeqsTiposProcessosGpi.Count == 0)
                        throw new ProcessoSeletivoTipoProcessoGpiNaoVinculadoTipoProcessoException(processoSeletivoCopiaGpi.Descricao);

                    //Recupera o processo seletivo selecionado da lista de processos que veio da tela
                    var processoSeletivoSelecionado = processosSeletivosSelecionados.FirstOrDefault(w => w.Seq == processoSeletivoCopiaGpi.Seq);

                    //Recupera o processo da lista de processos seletivos recuperados do Gpi
                    var situacaoTipoHierarquiaTipoProcessoUnidadeResponsavel = situacoesTiposHierarquiasTiposProcessosUnidadesResponsaveis.FirstOrDefault(p => p.SeqProcessoSeletivo == processoSeletivoCopiaGpi.SeqProcessoGpi);

                    //Se alguma oferta ou etapa tiver sido marcada para ser copiada e o processo do GPI também,
                    //verificar se o tipo de processo do processo de origem do GPI está desativado para sua
                    //unidade responsável. Em caso afirmativo, abortar a operação
                    if ((seqsOfertas.Count > 0 || processoSeletivoSelecionado.EtapasGPI.Any(e => e.Checked)) && processoSeletivoSelecionado.CopiarProcessoGPI.GetValueOrDefault())
                    {
                        //Verifica se o tipo de processo está desativado para a unidade responsável
                        if (!situacaoTipoHierarquiaTipoProcessoUnidadeResponsavel.TipoProcessoAtivo)
                            throw new ProcessoSeletivoTipoProcessoGpiDesativadoUnidadeResponsavelException(processoSeletivoCopiaGpi.Descricao);
                    }

                    //Se alguma oferta estiver sendo copiada e algum processo do GPI também, verificar se o tipo de
                    //hierarquia do processo de origem do GPI está desativado para seu tipo de processo e sua unidade
                    //responsável. Em caso afirmativo, abortar a operação
                    if (seqsOfertas.Count > 0 && processoSeletivoSelecionado.CopiarProcessoGPI.GetValueOrDefault())
                    {
                        if (!situacaoTipoHierarquiaTipoProcessoUnidadeResponsavel.TipoHierarquiaOfertaAtivo)
                            throw new ProcessoSeletivoTipoHierarquiaProcessoGpiDesativadoTipoProcessoUnidadeResponsavelException(processoSeletivoCopiaGpi.Descricao);
                    }

                    //Se alguma etapa estiver sendo copiada, verificar se o template de processo do processo de origem do GPI
                    //está desativado para seu tipo de processo. Em caso afirmativo, abortar a operação
                    if (processoSeletivoSelecionado.EtapasGPI.Any(e => e.Checked))
                    {
                        if (!situacaoTipoHierarquiaTipoProcessoUnidadeResponsavel.TipoProcessoTemplateAtivo)
                            throw new ProcessoSeletivoTipoProcessoTemplateDesativadoTipoProcessoException(processoSeletivoCopiaGpi.Descricao);
                    }
                }
            }
        }

        //RN_CAM_019 - Copia de Campanha
        public long SalvarCopiaCampanha(CampanhaCopiaVO model)
        {
            using (var unitOfWork = SMCUnitOfWork.Begin())
            {
                try
                {
                    //Spec para recuperar a campanha origem
                    var specCampanha = new CampanhaFilterSpecification() { Seq = model.SeqCampanhaOrigem };

                    //Recupera a campanha origem
                    var campanha = SearchByKey(specCampanha);

                    //1. Criar um novo registro de campanha com a descrição informada, a unidade responsável da campanha
                    //de origem e o(s) ciclo(s) letivo(s) informado(s).
                    var novaCampanha = new Campanha()
                    {
                        Descricao = model.DescricaoCampanhaDestino,
                        SeqEntidadeResponsavel = campanha.SeqEntidadeResponsavel,
                        CiclosLetivos = model.CiclosLetivosCampanhaDestino.Select(c => new CampanhaCicloLetivo() { SeqCicloLetivo = c.SeqCicloLetivo }).ToList(),
                    };

                    //RN_CAM_002
                    //Verificar se existe outra campanha com a mesma descrição.
                    ValidarDescricaoCampanha(novaCampanha.Seq, novaCampanha.Descricao);

                    //RM_CAM_002
                    //Verificar se existe alguma outra campanha cadastrada para a unidade responsável da campanha em questão no mesmo ciclo letivo.
                    ValidarCampanhaUnidadeResponsavelCicloLetivo(novaCampanha);

                    //Salva a campanha
                    SaveEntity(novaCampanha);

                    //2.Para cada oferta selecionada para a cópia, associar um novo registro de oferta à nova campanha
                    //criada, executando a regra: RN_CAM_015 - Associação de Oferta da Campanha, porém, com a quantidade
                    //de vagas igual à da oferta de origem. É importante salientar que a descrição da oferta não deverá
                    //ser copiada, deverá ser gerada novamente, a partir dos dados do item da oferta de campanha de origem,
                    //pois a hierarquia de entidades e as descrições dos itens que compõe a descrição da oferta podem
                    //ter sofrido alteração. Ao criar o item da oferta da campanha, utilizar os mesmos dados do item
                    //de oferta da oferta de origem, mas referenciando o novo item à nova oferta de campanha criada.

                    //Cria um dicionario de campanhas ofertas que vai armazenar o vinculo entre as campanhas ofertas selecionadas
                    //e as novas campanhas ofertas com as suas quantidades de vagas
                    var dicionarioCampanhasOfertas = new Dictionary<long, long>();

                    //Recupera os sequenciais das ofertas selecionadas
                    var seqsCampanhasOfertasSelecionadas = model.GridCampanhaOferta == null ? new List<long>() : model.GridCampanhaOferta?.Select(g => Convert.ToInt64(g)).ToList();

                    if (seqsCampanhasOfertasSelecionadas.Any())
                    {
                        var dadosCampanhasOfertasSelecionadas = CampanhaOfertaDomainService.BuscarCampanhasOfertasIntegracaoGpi(seqsCampanhasOfertasSelecionadas.ToArray());

                        //Para cada campanha oferta selecionada
                        foreach (var dadoCampanhaOfertaSelecionada in dadosCampanhasOfertasSelecionadas)
                        {
                            //TODO: Quando for graduação, refatorar. Por hora, pegar o primeiro item.
                            var itemCampanhaOferta = dadoCampanhaOfertaSelecionada.Itens.FirstOrDefault();

                            //Cria um objeto com os dados do item da campanha para formatação da descrição da nova campanha oferta
                            var campanhaOfertaVO = itemCampanhaOferta.Transform<SelecaoOfertaCampanhaLookupListaVO>();

                            //Cria a descrição da nova campanha oferta
                            var descricaoOfertaCampanha = this.CampanhaOfertaDomainService.FormatarDescricaoOfertaCampanha(campanhaOfertaVO, dadoCampanhaOfertaSelecionada.TipoOferta.Transform<TipoOferta>());

                            //Cria a nova campanha oferta, vinculada a nova campanha que foi criada
                            var novaCampanhaOferta = new CampanhaOferta()
                            {
                                SeqCampanha = novaCampanha.Seq,
                                SeqTipoOferta = dadoCampanhaOfertaSelecionada.TipoOferta.Seq,
                                Descricao = descricaoOfertaCampanha,
                                QuantidadeVagas = dadoCampanhaOfertaSelecionada.QuantidadeVagas
                            };

                            //Salva a nova campanha oferta
                            CampanhaOfertaDomainService.SaveEntity(novaCampanhaOferta);

                            //Armazena os sequenciais das campanhas ofertas no dicionario de apoio
                            dicionarioCampanhasOfertas.Add(dadoCampanhaOfertaSelecionada.Seq, novaCampanhaOferta.Seq);

                            //Cria um novo item campanha oferta, para a nova campanha oferta, baseado no item da campanha oferta selecionada
                            var novoCampanhaOfertaItem = new CampanhaOfertaItem()
                            {
                                SeqCampanhaOferta = novaCampanhaOferta.Seq,
                                SeqColaborador = itemCampanhaOferta.SeqColaborador,
                                SeqCursoOfertaLocalidadeTurno = itemCampanhaOferta.SeqCursoOfertaLocalidadeTurno,
                                SeqFormacaoEspecifica = itemCampanhaOferta.SeqFormacaoEspecifica,
                                SeqTurma = itemCampanhaOferta.SeqTurma,
                            };

                            //Salva o item da campanha oferta, para a nova campanha oferta
                            CampanhaOfertaItemDomainService.SaveEntity(novoCampanhaOfertaItem);
                        }
                    }

                    //3.Para cada processo seletivo selecionado para cópia, criar um novo processo seletivo para a
                    //nova campanha, com a mesma descrição, níveis de ensino, tipo de processo seletivo, tipo de vínculo,
                    //forma de ingresso e configuração de reserva de vaga.

                    //Recupera os sequenciais dos processos seletivos selecionadas
                    var seqsProcessosSeletivosSelecionados = model.GridProcessoSeletivo == null ? new List<long>() : model.GridProcessoSeletivo?.Select(p => Convert.ToInt64(p)).ToList();

                    if (seqsProcessosSeletivosSelecionados.Any())
                    {
                        var dadosProcessosSeletivosSelecionados = ProcessoSeletivoDomainService.BuscarProcessosSeletivosIntegracaoGpi(seqsProcessosSeletivosSelecionados.ToArray(), seqsCampanhasOfertasSelecionadas.ToArray());

                        //Para cada processo seletivo selecionado
                        foreach (var dadoProcessoSeletivoSelecionado in dadosProcessosSeletivosSelecionados)
                        {
                            //Cria um novo processo seletivo, vinculado a nova campanha
                            var novoProcessoSeletivo = new ProcessoSeletivo()
                            {
                                SeqCampanha = novaCampanha.Seq,
                                Descricao = dadoProcessoSeletivoSelecionado.Descricao,
                                SeqTipoProcessoSeletivo = dadoProcessoSeletivoSelecionado.SeqTipoProcessoSeletivo,
                                SeqTipoVinculoAluno = dadoProcessoSeletivoSelecionado.SeqTipoVinculoAluno,
                                SeqFormaIngresso = dadoProcessoSeletivoSelecionado.SeqFormaIngresso,
                                ReservaVaga = dadoProcessoSeletivoSelecionado.ReservaVaga,
                                NiveisEnsino = dadoProcessoSeletivoSelecionado.SeqsNiveisEnsino.Select(n => new ProcessoSeletivoNivelEnsino() { SeqNivelEnsino = n }).ToList(),
                            };

                            //Salva o novo processo seletivo
                            ProcessoSeletivoDomainService.SaveEntity(novoProcessoSeletivo);

                            //Cria um dicionário de apoio para armazenar os sequenciais dos processos seletivos oferta.
                            var dicionarioProcessoSeletivoOferta = new Dictionary<long?, long?>();

                            //Para cada oferta vinculada ao processo seletivo selecionado
                            foreach (var oferta in dadoProcessoSeletivoSelecionado.Ofertas)
                            {
                                //3.1.Se alguma das ofertas selecionadas para cópia estiver vinculada ao processo seletivo que está
                                //sendo copiado, a nova oferta criada para a nova campanha deverá ser associada ao novo processo
                                //seletivo, com a quantidade de vagas igual à da oferta associada ao processo seletivo de origem.

                                //Se existir alguma oferta selecionada, vinculado ao processo seletivo selecionado
                                if (seqsCampanhasOfertasSelecionadas != null && seqsCampanhasOfertasSelecionadas.Contains(oferta.SeqCampanhaOferta))
                                {
                                    //Recupera do dicionario a nova campanha oferta com a quantidade de vagas
                                    var seqNovaCampanhaOferta = dicionarioCampanhasOfertas.FirstOrDefault(d => d.Key == oferta.SeqCampanhaOferta).Value;

                                    //Cria um novo processo seletivo oferta
                                    var novoProcessoSeletivoOferta = new ProcessoSeletivoOferta()
                                    {
                                        SeqProcessoSeletivo = novoProcessoSeletivo.Seq,
                                        SeqCampanhaOferta = seqNovaCampanhaOferta,
                                        QuantidadeVagas = oferta.QuantidadeVagas
                                    };

                                    //Salva o novo processo seletivo oferta
                                    ProcessoSeletivoOfertaDomainService.SaveEntity(novoProcessoSeletivoOferta);

                                    //Armazena o novo sequencial do processo seletivo oferta no dicionario de apoio, caso tenha vinculo com a hierarquia oferta do GPI
                                    if (oferta.SeqHierarquiaOfertaGpi.HasValue)
                                        dicionarioProcessoSeletivoOferta.Add(oferta.SeqHierarquiaOfertaGpi, novoProcessoSeletivoOferta.Seq);
                                }
                            }

                            //3.2.Se existir processo GPI associado ao processo cópia e seus respectivos dados. seletivo
                            //selecionado para cópia e esse processo GPI também tiver sido selecionado para ser copiado,
                            //copiar o processo do GPI, setando que ele possui integração e executando a regra
                            //RN_INS_095 -Cópia de processo, passando como parâmetro a descrição informada, o ano/ semestre
                            //do ciclo letivo informado, as datas inicio e fim de inscrição, o identificador de cópia de
                            //notificação, as etapas marcadas para cópia e seus respectivos dados.
                            //Ao executar o item 5 desta regra, considerar que a hierarquia deverá ser copiada somente
                            //se alguma oferta deste processo seletivo do GPI tiver sido selecionada para cópia e deverão
                            //ser copiadas somente as ofertas selecionadas.Ao criar a nova hierarquia de ofertas, a
                            //descrição dos itens deverá ser regerada, conforme regra RN_CAM_075 - Retorna descrição do
                            //item da hierarquia de ofertas. Caso algum “galho” da hierarquia de ofertas, fique sem
                            //oferta associada, esse não deverá ser copiado.Após criar a oferta no GPI, setar na nova
                            //oferta do processo seletivo do SGA o sequencial da referida oferta do GPI.Após criar o
                            //processo no GPI, setar seu sequencial no novo processo seletivo do SGA

                            //Recupera o processo seletivo selecionado
                            var processoSeletivoSelecionado = model.ProcessosSeletivos.FirstOrDefault(p => p.Seq == dadoProcessoSeletivoSelecionado.Seq);

                            //Caso o processo seletivo selecionado esteja vinculado a um processo do GPI e tenha sido marcado para cópia
                            if (dadoProcessoSeletivoSelecionado.SeqProcessoGpi.HasValue && processoSeletivoSelecionado.CopiarProcessoGPI.GetValueOrDefault())
                            {
                                //Recupera dados do ciclo letivo referência GPI selecionado
                                var cicloLetivoProcessoGpi = CicloLetivoDomainService.SearchProjectionByKey(new SMCSeqSpecification<CicloLetivo>(processoSeletivoSelecionado.SeqCicloLetivoReferenciaProcessoGPI.GetValueOrDefault()), c => new
                                {
                                    c.Ano,
                                    c.Numero
                                });

                                //Cria um novo processo do GPI para cópia
                                var novoProcessoGpi = new CopiaProcessoData()
                                {
                                    SeqProcessoOrigem = dadoProcessoSeletivoSelecionado.Seq,
                                    SeqProcessoGpi = dadoProcessoSeletivoSelecionado.SeqProcessoGpi.GetValueOrDefault(),
                                    NovoProcessoDescricao = processoSeletivoSelecionado.DescricaoProcessoGPI,
                                    DataInicioInscricao = processoSeletivoSelecionado.DataInicioInscricaoProcessoGPI,
                                    DataFimInscricao = processoSeletivoSelecionado.DataFimInscricaoProcessoGPI,
                                    CopiarNotificacoes = processoSeletivoSelecionado.CopiarNotificacoesGPI.GetValueOrDefault(),
                                    Etapas = processoSeletivoSelecionado.EtapasGPI.Where(e => e.Checked).Select(e => new CopiaEtapaProcessoData()
                                    {
                                        SeqEtapa = e.Seq,
                                        SeqEtapaSGF = e.Seq,
                                        Copiar = e.Checked,
                                        DataInicio = e.DataInicioEtapa,
                                        DataFim = e.DataFimEtapa,
                                        CopiarConfiguracoes = e.CopiarConfiguracaoEtapa,
                                    }).ToList(),
                                    NovoProcessoAnoReferencia = cicloLetivoProcessoGpi.Ano,
                                    NovoProcessoSemestreReferencia = cicloLetivoProcessoGpi.Numero,
                                    MontarHierarquiaOfertaGPI = true,
                                    CopiarItens = true,
                                    ItensOfertasHierarquiasOfertas = PrepararItensHierarquiaOfertaIntegracaoGpi(dadoProcessoSeletivoSelecionado.SeqProcessoGpi.GetValueOrDefault(), dadoProcessoSeletivoSelecionado.Ofertas)
                                };

                                //Aciona o serviço do GPI para copiar o processo
                                var copiaProcessoRetorno = ProcessoService.CopiarProcesso(novoProcessoGpi);

                                //Atualiza o sequancial do processo Gpi que foi gerado na copia
                                novoProcessoSeletivo.SeqProcessoGpi = copiaProcessoRetorno.ProcessosGpi.FirstOrDefault(p => p.Key == dadoProcessoSeletivoSelecionado.SeqProcessoGpi.GetValueOrDefault()).Value;
                                ProcessoSeletivoDomainService.UpdateFields(novoProcessoSeletivo, x => x.SeqProcessoGpi);

                                //Atualiza os sequenciais gerados pelo GPI nos processos seletivos ofertas do SGA
                                foreach (var itemProcessoSeletivoOferta in dicionarioProcessoSeletivoOferta)
                                {
                                    //Recupera o sequencial do item hierarquia oferta gerado no GPI
                                    var itemOfertaHierarquiaOferta = copiaProcessoRetorno.ItensOfertasHierarquiasOfertas.FirstOrDefault(d => d.Key == itemProcessoSeletivoOferta.Key);

                                    //Recupera o processo seletivo oferta do SGA e atualiza o seq hierarquia oferta gerado no GPI
                                    var processoSeletivoOferta = ProcessoSeletivoOfertaDomainService.SearchByKey(new SMCSeqSpecification<ProcessoSeletivoOferta>(itemProcessoSeletivoOferta.Value.GetValueOrDefault()));
                                    processoSeletivoOferta.SeqHierarquiaOfertaGpi = itemOfertaHierarquiaOferta.Value;
                                    ProcessoSeletivoOfertaDomainService.UpdateFields<ProcessoSeletivoOferta>(processoSeletivoOferta, p => p.SeqHierarquiaOfertaGpi);
                                }
                            }

                            //4.Para cada convocação selecionada para cópia, criar uma nova convocação para o
                            //novo processo seletivo, com a descrição e o ciclo letivo informados e a quantidade
                            //de chamadas regulares igual a “1”.

                            //Para cada convocação selecionada do processo selecionado
                            foreach (var convocacaoSelecionada in processoSeletivoSelecionado.Convocacoes.Where(c => c.Checked))
                            {
                                //Spec para recuperar o seq da campanha ciclo letivo
                                var specCampanhaCicloLetivo = new CampanhaCicloLetivoFilterSpecification() { SeqCampanha = novaCampanha.Seq, SeqCicloLeitivo = convocacaoSelecionada.SeqCicloLetivo };

                                //Recupera o seq da campanha ciclo letivo
                                var seqCampanhaCicloLetivo = CampanhaCicloLetivoDomainService.SearchProjectionByKey(specCampanhaCicloLetivo, c => c.Seq);

                                //Cria uma nova convocação para o novo processo seletivo
                                var novaConvocacao = new Convocacao()
                                {
                                    SeqProcessoSeletivo = novoProcessoSeletivo.Seq,
                                    Descricao = convocacaoSelecionada.Descricao,
                                    SeqCampanhaCicloLetivo = seqCampanhaCicloLetivo,
                                    QuantidadeChamadasRegulares = 1,
                                };

                                //Salva a nova convocação
                                ConvocacaoDomainService.SaveEntity(novaConvocacao);

                                //4.1.Se alguma das ofertas selecionadas para cópia estiver vinculada à convocação que está
                                //sendo copiada, a nova oferta criada para o novo processo seletivo da referida convocação
                                //deverá ser associada a esta nova convocação, com a quantidade de vagas igual à da oferta
                                //associada à convocação de origem.

                                //Recupera os dados das convocação selecionada
                                var dadosConvocacaoSelecionada = ConvocacaoDomainService.SearchProjectionByKey(new SMCSeqSpecification<Convocacao>(convocacaoSelecionada.Seq), c => new
                                {
                                    Ofertas = c.Ofertas.Select(o => new { o.SeqProcessoSeletivoOferta, o.QuantidadeVagas }).ToList()
                                });

                                //Para cada oferta vinculada a convocação selecionada
                                foreach (var oferta in dadosConvocacaoSelecionada.Ofertas)
                                {
                                    //Se existir alguma oferta selecionada, vinculada a convocação selecionada
                                    if (seqsCampanhasOfertasSelecionadas.Contains(oferta.SeqProcessoSeletivoOferta))
                                    {
                                        //Recupera do dicionario a nova campanha oferta com a quantidade de vagas
                                        var seqNovaCampanhaOferta = dicionarioCampanhasOfertas.FirstOrDefault(d => d.Key == oferta.SeqProcessoSeletivoOferta).Value;

                                        //Spec para recuperar o seq do processo seletivo oferta
                                        var specProcessoSeletivoOferta = new ProcessoSeletivoOfertaFilterSpecification() { SeqCampanhaOferta = seqNovaCampanhaOferta, SeqProcessoSeletivo = novoProcessoSeletivo.Seq };

                                        //Recupera o seq do processo seletivo oferta
                                        var seqProcessoSeletivoOferta = ProcessoSeletivoOfertaDomainService.SearchProjectionByKey(specProcessoSeletivoOferta, p => p.Seq);

                                        //Cria um nova convocação oferta
                                        var novaConvocacaoOferta = new ConvocacaoOferta()
                                        {
                                            SeqConvocacao = novaConvocacao.Seq,
                                            SeqProcessoSeletivoOferta = seqProcessoSeletivoOferta,
                                            QuantidadeVagas = oferta.QuantidadeVagas
                                        };

                                        //Salva a nova convocação oferta
                                        ConvocacaoOfertaDomainService.SaveEntity(novaConvocacaoOferta);
                                    }
                                }
                            }
                        }
                    }
                    unitOfWork.Commit();

                    return novaCampanha.Seq;
                }
                catch (Exception ex)
                {
                    unitOfWork.Rollback();
                    throw ex;
                }
            }
        }

        public List<ItemOfertaHierarquiaOfertaData> PrepararItensHierarquiaOfertaIntegracaoGpi(long seqProcessoGpi, List<ProcessoSeletivoOfertaIntegracaoGpiVO> ofertas)
        {
            var itensOfertaHierarquiaOferta = new List<ItemOfertaHierarquiaOfertaData>();

            //Busca no GPI a árvore de itens de hierarquia de oferta
            var itensHieraquiaOfertaArvore = ItemHierarquiaOfertaService.BuscarItensHierarquiaOfertaArvore(seqProcessoGpi);

            //Para cada oferta vinculada ao processo seletivo
            //Adiciona os itens na lista de itens para que o GPI monte a hierarquia de ofertas
            foreach (var oferta in ofertas)
            {
                ItemHierarquiaOfertaArvoreData itemHierarquiaOfertaArvoreData = null;

                // Buscan os itens correspondentes do GPI, conforme os tipos de oferta do SGA
                switch (oferta.TipoOferta.Token)
                {
                    case TOKEN_TIPO_OFERTA.CURSO_OFERTA_LOCALIDADE_TURNO:

                        itemHierarquiaOfertaArvoreData = itensHieraquiaOfertaArvore.FirstOrDefault(i => i.Token == TOKENS_TIPO_ITEM_HIERARQUIA_OFERTA.CURSO);

                        break;

                    case TOKEN_TIPO_OFERTA.AREA_CONCENTRACAO:

                        itemHierarquiaOfertaArvoreData = itensHieraquiaOfertaArvore.FirstOrDefault(i => i.Token == TOKENS_TIPO_ITEM_HIERARQUIA_OFERTA.AREA_DE_CONCENTRACAO);

                        break;

                    case TOKEN_TIPO_OFERTA.LINHA_PESQUISA:

                        itemHierarquiaOfertaArvoreData = itensHieraquiaOfertaArvore.FirstOrDefault(i => i.Token == TOKENS_TIPO_ITEM_HIERARQUIA_OFERTA.LINHA_DE_PESQUISA);

                        break;

                    case TOKEN_TIPO_OFERTA.EIXO_TEMATICO:

                        itemHierarquiaOfertaArvoreData = itensHieraquiaOfertaArvore.FirstOrDefault(i => i.Token == TOKENS_TIPO_ITEM_HIERARQUIA_OFERTA.EIXO_TEMATICO);

                        break;

                    case TOKEN_TIPO_OFERTA.ORIENTADOR:

                        itemHierarquiaOfertaArvoreData = itensHieraquiaOfertaArvore.FirstOrDefault(i => i.Token == TOKENS_TIPO_ITEM_HIERARQUIA_OFERTA.ORIENTADOR);

                        break;

                    case TOKEN_TIPO_OFERTA.TURMA:

                        itemHierarquiaOfertaArvoreData = itensHieraquiaOfertaArvore.FirstOrDefault(i => i.Token == TOKENS_TIPO_ITEM_HIERARQUIA_OFERTA.DISCIPLINA);

                        break;

                    default:
                        break;
                }

                //Adiciona o item a lista de itens de hierarquia de oferta
                AdicionaItensHierarquiaOfertaGpi(itemHierarquiaOfertaArvoreData.Seq, oferta, itensHieraquiaOfertaArvore, itensOfertaHierarquiaOferta);
            }

            //Inverte a ordem dos itens da lista (pai -> filho -> neto...)
            itensOfertaHierarquiaOferta.Reverse();

            //Retorna os itens
            return itensOfertaHierarquiaOferta;
        }

        private void AdicionaItensHierarquiaOfertaGpi(long seqItemHierarquiaOfertaArvore, ProcessoSeletivoOfertaIntegracaoGpiVO oferta, List<ItemHierarquiaOfertaArvoreData> itensHieraquiaOfertaArvore, List<ItemOfertaHierarquiaOfertaData> itensOfertaHierarquiaOferta)
        {
            //Separa o item de hierarquia de acordo com o seq
            var itemHieraquiaOfertaArvore = itensHieraquiaOfertaArvore.FirstOrDefault(i => i.Seq == seqItemHierarquiaOfertaArvore);

            //Prepara o objeto para ser adicionado na lista
            var itemOfertaHierarquiaOfertaData = new ItemOfertaHierarquiaOfertaData()
            {
                SeqHierarquiaOfertaOrigem = oferta.SeqProcessoSeletivoOferta,
                SeqHierarquiaOfertaGPI = oferta.SeqHierarquiaOfertaGpi,
                TokenTipoItemHierarquiaOferta = itemHieraquiaOfertaArvore.Token
            };

            //Para cada tipo de item do GPI, busca a destrição do item da oferta do SGA.
            switch (itemHieraquiaOfertaArvore.Token)
            {
                case TOKENS_TIPO_ITEM_HIERARQUIA_OFERTA.CURSO:

                    //itemOfertaHierarquiaOfertaData.Descricao = $"{oferta.Itens.FirstOrDefault().Curso} - {oferta.Itens.FirstOrDefault().Localidade} - Turno {oferta.Itens.FirstOrDefault().Turno}";
                    itemOfertaHierarquiaOfertaData.Descricao = oferta.Itens.FirstOrDefault().Curso;

                    break;

                case TOKENS_TIPO_ITEM_HIERARQUIA_OFERTA.AREA_DE_CONCENTRACAO:

                    itemOfertaHierarquiaOfertaData.Descricao = oferta.Itens.FirstOrDefault().AreaConcentracao;

                    break;

                case TOKENS_TIPO_ITEM_HIERARQUIA_OFERTA.LINHA_DE_PESQUISA:

                    itemOfertaHierarquiaOfertaData.Descricao = oferta.Itens.FirstOrDefault().LinhaPesquisa;

                    break;

                case TOKENS_TIPO_ITEM_HIERARQUIA_OFERTA.EIXO_TEMATICO:

                    itemOfertaHierarquiaOfertaData.Descricao = oferta.Itens.FirstOrDefault().EixoTematico;

                    break;

                case TOKENS_TIPO_ITEM_HIERARQUIA_OFERTA.ORIENTADOR:

                    itemOfertaHierarquiaOfertaData.Descricao = oferta.Itens.FirstOrDefault().Orientador;

                    break;

                case TOKENS_TIPO_ITEM_HIERARQUIA_OFERTA.DISCIPLINA:

                    itemOfertaHierarquiaOfertaData.Descricao = oferta.Itens.FirstOrDefault().SeqTurma.HasValue ? TurmaDomainService.BuscarDescricaoTurmaConcatenado(oferta.Itens.FirstOrDefault().SeqTurma.Value) : string.Empty;

                    break;

                default:
                    break;
            }

            //Adiciona o item de hierarquia a lista
            itensOfertaHierarquiaOferta.Add(itemOfertaHierarquiaOfertaData);

            //Caso o item tenha pai, chama a função novamente de forma recursiva, até adicionar a hierarquia inteira
            if (itemHieraquiaOfertaArvore.SeqPai.HasValue)
                AdicionaItensHierarquiaOfertaGpi(itemHieraquiaOfertaArvore.SeqPai.GetValueOrDefault(), oferta, itensHieraquiaOfertaArvore, itensOfertaHierarquiaOferta);
        }

        /// <summary>
        /// Verificar se existe outra campanha com a mesma descrição.
        /// Em caso afirmativo, abortar a operação e emitir a mensagem de erro:
        /// "<Inclusão/Alteração> não permitida. Já existe outra campanha com a mesma descrição."
        /// </summary>
        /// <param name="seqCampanha"></param>
        /// <param name="descricaoCampanha"></param>
        private void ValidarDescricaoCampanha(long seqCampanha, string descricaoCampanha)
        {
            // Verifica se não há descrições repetidas
            var verificaDescricao = SearchBySpecification(new CampanhaFilterSpecification() { DescricaoDuplicada = descricaoCampanha });
            if (verificaDescricao.Any(f => f.Seq != seqCampanha))
            {
                /*Em caso afirmativo, abortar a operação e emitir a mensagem de erro:
                    "<Inclusão/Alteração> não permitida. Já existe outra campanha com a mesma descrição."*/
                throw new CampanhaDescricaoRepetidaException();
            }
        }

        private void ValidarCampanhaUnidadeResponsavelCicloLetivo(long seqCampanha, List<long> seqsCiclosLetivos)
        {
            var specCampanha = new CampanhaFilterSpecification() { Seq = seqCampanha };

            var campanha = SearchProjectionByKey(specCampanha, c => new
            {
                c.Seq,
                c.SeqEntidadeResponsavel,
            });

            // Verifica se não há campanhas para a mesma unidade responsável e ciclo(s) letivo(s)
            if (Count(new CampanhaUnidadeResponsavelCicloLetivoSpecification(campanha.Seq, campanha.SeqEntidadeResponsavel, seqsCiclosLetivos)) > 0)
            {
                var entidadeResponsavel = EntidadeDomainService.SearchProjectionByKey(new SMCSeqSpecification<Entidade>(campanha.SeqEntidadeResponsavel), x => x.Nome);
                throw new CampanhaUnidadeResponsavelCicloLetivoRepetidoException(entidadeResponsavel);
            }
        }

        private void ValidarVinculoTipoOfertaUnidadeResponsavel(long seqCampanha, List<long> seqsOfertas)
        {
            var specCampanha = new CampanhaFilterSpecification() { Seq = seqCampanha };

            var dadosCampanha = SearchProjectionByKey(specCampanha, c => new
            {
                EntidadeResponsavel = new { c.SeqEntidadeResponsavel, c.EntidadeResponsavel.Nome },
                TiposOfertas = c.Ofertas.Where(o => seqsOfertas.Contains(o.Seq)).Select(t => new { t.TipoOferta.Seq, t.TipoOferta.Descricao }).Distinct().ToList()
            });

            //Valida se o tipo de oferta da oferta selecionada não estiver mais associado à unidade responsável da campanha
            foreach (var seqTipoOferta in dadosCampanha.TiposOfertas.Select(t => t.Seq).ToList())
            {
                var specTipoOfertaUnidadeResponsavel = new TipoOfertaUnidadeResponsavelSpecification()
                {
                    SeqEntidadeResponsavel = dadosCampanha.EntidadeResponsavel.SeqEntidadeResponsavel,
                    SeqTipoOferta = seqTipoOferta
                };

                if (TipoOfertaUnidadeResponsavelDomainService.Count(specTipoOfertaUnidadeResponsavel) == 0)
                {
                    var entidadeResponsavel = dadosCampanha.EntidadeResponsavel.Nome;
                    var tipoOferta = dadosCampanha.TiposOfertas.FirstOrDefault(t => t.Seq == seqTipoOferta).Descricao;
                    throw new CampanhaUnidadeResponsavelTipoOfertaNaoVinculadoException(tipoOferta, entidadeResponsavel);
                }
            }
        }

        #endregion Validações da Copia de Campanha
    }
}