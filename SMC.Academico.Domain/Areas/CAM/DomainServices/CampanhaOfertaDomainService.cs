using SMC.Academico.Common.Areas.ALN.Enums;
using SMC.Academico.Common.Areas.CAM.Exceptions;
using SMC.Academico.Common.Areas.CAM.Includes;
using SMC.Academico.Common.Areas.CSO.Constants;
using SMC.Academico.Common.Areas.DCT.Enums;
using SMC.Academico.Common.Areas.TUR.Enums;
using SMC.Academico.Common.Constants;
using SMC.Academico.Domain.Areas.ALN.DomainServices;
using SMC.Academico.Domain.Areas.ALN.Specifications;
using SMC.Academico.Domain.Areas.CAM.Models;
using SMC.Academico.Domain.Areas.CAM.Specifications;
using SMC.Academico.Domain.Areas.CAM.ValueObjects;
using SMC.Academico.Domain.Areas.CSO.DomainServices;
using SMC.Academico.Domain.Areas.CSO.Specifications;
using SMC.Academico.Domain.Areas.CSO.ValueObjects;
using SMC.Academico.Domain.Areas.DCT.DomainServices;
using SMC.Academico.Domain.Areas.DCT.Specifications;
using SMC.Academico.Domain.Areas.DCT.ValueObjects;
using SMC.Academico.Domain.Areas.TUR.DomainServices;
using SMC.Academico.Domain.Areas.TUR.Specifications;
using SMC.Framework;
using SMC.Framework.Domain;
using SMC.Framework.Extensions;
using SMC.Framework.Model;
using SMC.Framework.Specification;
using SMC.Framework.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SMC.Academico.Domain.Areas.CAM.DomainServices
{
    public class CampanhaOfertaDomainService : AcademicoContextDomain<CampanhaOferta>
    {
        #region [ DomainSerivce ]

        private ColaboradorVinculoDomainService ColaboradorVinculoDomainService => Create<ColaboradorVinculoDomainService>();
        private InstituicaoNivelTipoVinculoAlunoDomainService InstituicaoNivelTipoVinculoAlunoDomainService { get => Create<InstituicaoNivelTipoVinculoAlunoDomainService>(); }

        private CampanhaDomainService CampanhaDomainService { get { return this.Create<CampanhaDomainService>(); } }

        private ProcessoSeletivoDomainService ProcessoSeletivoDomainService { get { return this.Create<ProcessoSeletivoDomainService>(); } }

        private ProcessoSeletivoOfertaDomainService ProcessoSeletivoOfertaDomainService { get { return this.Create<ProcessoSeletivoOfertaDomainService>(); } }

        private ConvocacaoOfertaDomainService ConvocacaoOfertaDomainService { get { return this.Create<ConvocacaoOfertaDomainService>(); } }

        private TurmaDomainService TurmaDomainService { get { return this.Create<TurmaDomainService>(); } }

        private ConfiguracaoEventoLetivoDomainService ConfiguracaoEventoLetivoDomainService => Create<ConfiguracaoEventoLetivoDomainService>();

        private CampanhaOfertaItemDomainService CampanhaOfertaItemDomainService => Create<CampanhaOfertaItemDomainService>();

        private ColaboradorDomainService ColaboradorDomainService => Create<ColaboradorDomainService>();

        private TipoOfertaDomainService TipoOfertaDomainService => Create<TipoOfertaDomainService>();

        private ConvocacaoDomainService ConvocacaoDomainService => Create<ConvocacaoDomainService>();

        private FormacaoEspecificaDomainService FormacaoEspecificaDomainService => Create<FormacaoEspecificaDomainService>();

        private CursoOfertaLocalidadeTurnoDomainService CursoOfertaLocalidadeTurnoDomainService => Create<CursoOfertaLocalidadeTurnoDomainService>();

        private DivisaoTurmaDomainService DivisaoTurmaDomainService => Create<DivisaoTurmaDomainService>();

        #endregion [ DomainSerivce ]

        #region [ Constantes ]

        private const char SEPARADOR_SEQS = '|';

        #endregion [ Constantes ]

        /// <summary>
        /// Busca as oferta de campanha que atendam aos filtros informados
        /// </summary>
        /// <param name="filtroVO">Dados dos filtros</param>
        /// <returns>Oferta de campanhas que atendam aos filtros paginadas</returns>
        public SMCPagerData<CampanhaOferta> BuscarCampanhasOfertaLookup(CampanhaOfertaFiltroVO filtroVO)
        {
            var filtros = filtroVO.Transform<CampanhaOfertaFilterSpecification>();
            // FIX: O Mapper está mapeando a lista de long por referência. Então quando a lista do specification é modificada, a lista do VO tb estava sendo.
            if (filtroVO.SeqNivelEnsino != null && filtroVO.SeqNivelEnsino.Any())
                filtros.SeqNivelEnsino = filtroVO.SeqNivelEnsino.Select(f => f).ToList();

            if (filtroVO.SeqNivelEnsino != null && filtroVO.SeqNivelEnsino.Count > 0 && filtroVO.SeqTipoVinculoAluno.HasValue)
            {
                foreach (var seqNivelEnsino in filtroVO.SeqNivelEnsino)
                {
                    var specVinculo = new InstituicaoNivelTipoVinculoAlunoFilterSpecification()
                    {
                        SeqNivelEnsino = seqNivelEnsino,
                        SeqTipoVinculoAluno = filtroVO.SeqTipoVinculoAluno
                    };
                    filtros.VinculoExigeCurso = InstituicaoNivelTipoVinculoAlunoDomainService.SearchProjectionByKey(specVinculo, p => p.ExigeCurso);
                    if (!filtros.VinculoExigeCurso.GetValueOrDefault())
                        filtros.SeqNivelEnsino.Remove(seqNivelEnsino);
                }
            }

            var campanhas = this.SearchBySpecification(filtros, out int total);
            return new SMCPagerData<CampanhaOferta>(campanhas, total);
        }

        public SMCPagerData<CampanhaOfertaListaVO> BuscarCampanhaOfertas(CampanhaOfertaFiltroTelaVO filtro)
        {
            var spec = filtro.Transform<CampanhaOfertaFilterTelaSpecification>();
            spec.SetOrderBy(o => o.TipoOferta.Descricao);
            spec.SetOrderBy(o => o.Descricao);
            spec.MaxResults = int.MaxValue;
            var lista = SearchProjectionBySpecification(spec, x => new CampanhaOfertaListaVO
            {
                Seq = x.Seq,
                TipoOferta = x.TipoOferta.Descricao,
                TipoOfertaToken = x.TipoOferta.Token,
                Oferta = x.Descricao,
                Vagas = x.QuantidadeVagas,
                VagasBase = x.QuantidadeVagas,
                Ocupadas = x.Ingressantes.Count(f =>
                    f.Ingressante.HistoricosSituacao.OrderByDescending(o => o.DataInclusao).FirstOrDefault().SituacaoIngressante != SituacaoIngressante.Desistente &&
                    f.Ingressante.HistoricosSituacao.OrderByDescending(o => o.DataInclusao).FirstOrDefault().SituacaoIngressante != SituacaoIngressante.Cancelado)
            }, out int total).ToList();

            foreach (var oferta in lista)
            {
                // Preencho a flag para validar se a oferta possui associação com processo seletivo
                oferta.PossuiVinculoProcessoSeletivo = ValidarExclusao(oferta.Seq);

                // RN_CAM_069 - Usar vagas disponíveis na campanha
                oferta.Disponiveis = (oferta.Vagas - oferta.Ocupadas);
            }

            return new SMCPagerData<CampanhaOfertaListaVO>(lista, total);
        }

        public SMCPagerData<CampanhaCopiaOfertaListaVO> BuscarCampanhaOfertas(CampanhaCopiaOfertaFiltroVO filtro)
        {
            var spec = filtro.Transform<CampanhaCopiaOfertaFilterSpecification>();

            spec.SetOrderBy(x => x.Descricao);

            var lista = SearchProjectionBySpecification(spec, x => new CampanhaCopiaOfertaListaVO
            {
                Seq = x.Seq,
                TipoOferta = x.TipoOferta.Descricao,
                Oferta = x.Descricao,
                Vagas = x.QuantidadeVagas,
            }, out int total).ToList();

            return new SMCPagerData<CampanhaCopiaOfertaListaVO>(lista, total);
        }

        public List<CampanhaOfertaIntegracaoGpiVO> BuscarCampanhasOfertasIntegracaoGpi(long[] seqsCampanhasOfertas)
        {
            var specCampanhasOfertas = new CampanhaOfertaFilterSpecification() { Seqs = seqsCampanhasOfertas };

            var campanhasOfertas = SearchProjectionBySpecification(specCampanhasOfertas, c => new CampanhaOfertaIntegracaoGpiVO()
            {
                Seq = c.Seq,
                TipoOferta = new TipoOfertaIntegracaoGpiVO() { Seq = c.TipoOferta.Seq, Token = c.TipoOferta.Token, ExigeCursoOfertaLocalidadeTurno = c.TipoOferta.ExigeCursoOfertaLocalidadeTurno },
                QuantidadeVagas = c.QuantidadeVagas,
                Itens = c.Itens.Select(i => new CampanhaOfertaItemIntegracaoGpiVO()
                {
                    Seq = i.Seq,
                    TokenTipoOferta = i.CampanhaOferta.TipoOferta.Token,
                    CursoOferta = i.CursoOfertaLocalidadeTurno.CursoOfertaLocalidade.CursoOferta.Descricao,
                    Localidade = i.CursoOfertaLocalidadeTurno.CursoOfertaLocalidade.HierarquiasEntidades.FirstOrDefault().ItemSuperior.Entidade.Nome,
                    Turno = i.CursoOfertaLocalidadeTurno.Turno.Descricao,
                    AreaTematica = i.CursoOfertaLocalidadeTurno.CursoOfertaLocalidade.FormacoesEspecificas.FirstOrDefault(f => f.FormacaoEspecifica.TipoFormacaoEspecifica.Token == TOKEN_TIPO_FORMACAO_ESPECIFICA.AREA_TEMATICA).FormacaoEspecifica.Descricao,
                    //AreaConcentracao = i.CursoOfertaLocalidadeTurno.CursoOfertaLocalidade.FormacoesEspecificas.FirstOrDefault(f => f.FormacaoEspecifica.FormacaoEspecificaSuperior.TipoFormacaoEspecifica.Token == TOKEN_TIPO_FORMACAO_ESPECIFICA.AREA_CONCENTRACAO).FormacaoEspecifica.FormacaoEspecificaSuperior.Descricao,
                    //LinhaPesquisa = i.CursoOfertaLocalidadeTurno.CursoOfertaLocalidade.FormacoesEspecificas.FirstOrDefault(f => f.FormacaoEspecifica.TipoFormacaoEspecifica.Token == TOKEN_TIPO_FORMACAO_ESPECIFICA.LINHA_PESQUISA).FormacaoEspecifica.Descricao,
                    EixoTematico = i.CursoOfertaLocalidadeTurno.CursoOfertaLocalidade.FormacoesEspecificas.FirstOrDefault(f => f.FormacaoEspecifica.TipoFormacaoEspecifica.Token == TOKEN_TIPO_FORMACAO_ESPECIFICA.EIXO_TEMATICO).FormacaoEspecifica.Descricao,
                    Orientador = i.Colaborador.DadosPessoais.Nome,
                    SeqTurma = i.SeqTurma,
                    SeqColaborador = i.SeqColaborador,
                    SeqCursoOfertaLocalidadeTurno = i.SeqCursoOfertaLocalidadeTurno,
                    SeqFormacaoEspecifica = i.SeqFormacaoEspecifica,

                }).ToList()
            }).ToList();


            foreach (var campanhaOfertaItem in campanhasOfertas.SelectMany(c => c.Itens))
            {

                switch (campanhaOfertaItem.TokenTipoOferta)
                {

                    case TOKEN_TIPO_OFERTA.AREA_CONCENTRACAO:

                        campanhaOfertaItem.AreaConcentracao = CampanhaOfertaItemDomainService.SearchProjectionByKey(campanhaOfertaItem.Seq, c => c.FormacaoEspecifica.Descricao);

                        break;

                    case TOKEN_TIPO_OFERTA.LINHA_PESQUISA:

                        campanhaOfertaItem.AreaConcentracao = CampanhaOfertaItemDomainService.SearchProjectionByKey(campanhaOfertaItem.Seq, c => c.FormacaoEspecifica.FormacaoEspecificaSuperior.Descricao);
                        campanhaOfertaItem.LinhaPesquisa = CampanhaOfertaItemDomainService.SearchProjectionByKey(campanhaOfertaItem.Seq, c => c.FormacaoEspecifica.Descricao);

                        break;

                    default:

                        campanhaOfertaItem.AreaConcentracao = CampanhaOfertaItemDomainService.SearchProjectionByKey(campanhaOfertaItem.Seq, c => c.CursoOfertaLocalidadeTurno.CursoOfertaLocalidade.FormacoesEspecificas.FirstOrDefault(f => f.FormacaoEspecifica.FormacaoEspecificaSuperior.TipoFormacaoEspecifica.Token == TOKEN_TIPO_FORMACAO_ESPECIFICA.AREA_CONCENTRACAO).FormacaoEspecifica.FormacaoEspecificaSuperior.Descricao);
                        campanhaOfertaItem.LinhaPesquisa = CampanhaOfertaItemDomainService.SearchProjectionByKey(campanhaOfertaItem.Seq, c => c.CursoOfertaLocalidadeTurno.CursoOfertaLocalidade.FormacoesEspecificas.FirstOrDefault(f => f.FormacaoEspecifica.TipoFormacaoEspecifica.Token == TOKEN_TIPO_FORMACAO_ESPECIFICA.LINHA_PESQUISA).FormacaoEspecifica.Descricao);

                        break;

                }

            }

            return campanhasOfertas;
        }

        #region Configurar Vagas

        public long SalvarVagasCampanhaOferta(VagasCampanhaOfertaVO model)
        {
            // Início da transação
            using (var unitOfWork = SMCUnitOfWork.Begin())
            {
                try
                {
                    var seqsCampanhasOfertas = model.CampanhaOfertas.Select(c => c.Seq).ToArray();

                    var spec = new CampanhaOfertaFilterTelaSpecification { SeqCampanha = model.SeqCampanha, SeqsCampanhaOfertas = seqsCampanhasOfertas };

                    var processosSeletivosSelecionados = VincularProcessoSeletivoConvocacao(model);
                    // Replica e Persiste as vagas do PROCESSO SELETIVO e suas respectivas CONVOCAÇÕES selecionados

                    SalvarVagasOfertas(processosSeletivosSelecionados, model.CampanhaOfertas);

                    // Valida a replicação de vagas
                    ValidarReplicarVagasOferta(model);

                    /*Verificar se o novo valor de vagas de cada oferta é menor que a quantidade de vagas
                    * configurada para esta mesma oferta nos processos seletivos da campanha.
                    * Em caso afirmativo, abortar a operação e emitir a mensagem de erro*/
                    ValidarVagasCampanhaOfertaProcessoSeletivo(spec, model);

                    // Validação das vagas das ofertas dos processos seletivos com as suas respectivas ofertas
                    // da convocação
                    ValidarVagasCampanhaOfertaConvocacao(spec, model);

                    // Persiste as ofertas da Campanha
                    PersistirVagasCampanhaOferta(spec, model);

                    // Valida processos Seletivos com as ofertas das campanhas
                    //RN_CAM_037 - Atualização das vagas das ofertas do processo seletivo
                    ValidarReplicarVagasProcessoSeletivoOferta(spec, model, processosSeletivosSelecionados);

                    // Valida se alguma replicação irá ocorrer, gerando inconsistências com as vagas das outras ofertas da hierarquia
                    ValidarVagasHierarquia(model);

                    unitOfWork.Commit();

                    return model.SeqCampanha;
                }
                catch (Exception ex)
                {
                    unitOfWork.Rollback();
                    throw ex;
                }
            }
        }

        private long PersistirVagasCampanhaOferta(CampanhaOfertaFilterTelaSpecification spec, VagasCampanhaOfertaVO model)
        {
            //Persiste as vagas da campanha oferta
            var campanhasOfertas = SearchBySpecification(spec);

            foreach (var item in model.CampanhaOfertas)
            {
                var campanhaOferta = campanhasOfertas.FirstOrDefault(f => f.Seq == item.Seq);

                campanhaOferta.QuantidadeVagas = (short)item.Vagas;
                this.SaveEntity(campanhaOferta);
            }

            return model.SeqCampanha;
        }

        /// <summary>
        /// Verificar se o novo valor de vagas de cada oferta é menor que a quantidade de vagas
        /// configurada para esta mesma oferta nos processos seletivos da campanha e suas ofertas da convocação
        /// Em caso afirmativo, abortar a operação e emitir a mensagem de erro
        /// </summary>
        /// <param name="spec"></param>
        /// <param name="campanhaOfertas"></param>
        private void ValidarVagasCampanhaOfertaProcessoSeletivo(CampanhaOfertaFilterTelaSpecification spec, VagasCampanhaOfertaVO model)
        {
            var processoSeletivoOfertas = SearchProjectionByKey(spec, x => x.Campanha.ProcessosSeletivos.SelectMany(a => a.Ofertas));

            foreach (var ofertaCampanha in model.CampanhaOfertas)
            {
                var processoSeletivoOferta = processoSeletivoOfertas.FirstOrDefault(p => p.SeqCampanhaOferta == ofertaCampanha.Seq);

                if (processoSeletivoOferta != null && ofertaCampanha.Vagas < processoSeletivoOferta.QuantidadeVagas)
                {
                    throw new OfertaVagaMenorProcessoSeletivoException();
                }
            }
        }

        /*RN_CAM_044 - Atualização das vagas das ofertas da convocação
          * 1. Ao aumentar a quantidade de vagas de uma oferta, verificar se o novo valor é maior que a
          * quantidade de vagas configurada para esta mesma oferta no processo seletivo. Em caso afirmativo,
          * abortar a operação e emitir a mensagem de erro:
          * "Alteração não permitida. Existe oferta cuja quantidade de vagas é maior que as vagas configuradas no
          * processo seletivo <descrição do processo seletivo>"*/

        private void ValidarVagasCampanhaOfertaConvocacao(CampanhaOfertaFilterTelaSpecification spec, VagasCampanhaOfertaVO model)
        {
            if (model.SeqsConvocacoes == null || !model.SeqsConvocacoes.Any()) { return; }

            var seqsCampanhasOfertas = model.CampanhaOfertas.Select(c => c.Seq).ToList();

            var processoSeletivoOfertas = SearchProjectionByKey(spec, x => x.Campanha.ProcessosSeletivos.SelectMany(a => a.Ofertas).Where(a => seqsCampanhasOfertas.Contains(a.SeqCampanhaOferta))).ToList();

            foreach (var ofertaCampanha in model.CampanhaOfertas)
            {
                if (ofertaCampanha.VagasDiferenca > 0)
                {
                    var processoSeletivoOferta = processoSeletivoOfertas.FirstOrDefault(p => p.SeqCampanhaOferta == ofertaCampanha.Seq);

                    var convocacaoOferta = SearchProjectionByKey(spec, x => x.Campanha.ProcessosSeletivos.SelectMany(a => a.Convocacoes.Where(c => model.SeqsConvocacoes.Contains(c.Seq)).SelectMany(o => o.Ofertas))).FirstOrDefault(o => o.SeqProcessoSeletivoOferta == processoSeletivoOferta.Seq);

                    if (convocacaoOferta != null && ofertaCampanha.Vagas > convocacaoOferta.QuantidadeVagas)
                    {
                        var processoSeletivo = ProcessoSeletivoDomainService.SearchByKey(processoSeletivoOferta.SeqProcessoSeletivo);

                        throw new OfertaVagaMaiorConvocacaoProcessoSeletivoException(processoSeletivo?.Descricao);
                    }
                }
            }
        }

        /// <summary>
        /// RN_CAM_037 - Atualização das vagas das ofertas do processo seletivo
        /// </summary>
        /// <param name="model"></param>
        /// <param name="processosSeletivos"></param>
        private void ValidarReplicarVagasProcessoSeletivoOferta(CampanhaOfertaFilterTelaSpecification spec, VagasCampanhaOfertaVO model, IEnumerable<ProcessoSeletivo> processosSeletivos)
        {
            var includeCampanhaOferta = IncludesCampanhaOferta.TipoOferta
                                        | IncludesCampanhaOferta.Itens
                                        | IncludesCampanhaOferta.Itens_Turma
                                        | IncludesCampanhaOferta.Itens_Turma_DivisoesTurma;

            //Busco as campanhas ofertas, para comparar com os novos valores das vagas
            var campanhasOfertas = SearchBySpecification(spec, includeCampanhaOferta).ToList();

            /*2. Verificar se o novo valor de cada oferta é maior que a quantidade de vagas configurada
             * para esta mesma oferta na campanha.
             * Em caso afirmativo, abortar a operação e emitir a mensagem de erro*/
            foreach (var processoSeletivo in processosSeletivos)
            {
                foreach (var ofertaProcessoSeletivo in processoSeletivo.Ofertas)
                {
                    var novaOfertaCampanha = model.CampanhaOfertas.FirstOrDefault(p => p.Seq == ofertaProcessoSeletivo.SeqCampanhaOferta);
                    if (novaOfertaCampanha != null)
                    {
                        var oldOfertaCampanha = campanhasOfertas.FirstOrDefault(c => c.Seq == novaOfertaCampanha.Seq
                                                && novaOfertaCampanha.Vagas > c.QuantidadeVagas);

                        if (oldOfertaCampanha != null)
                        {
                            throw new OfertaVagaMaiorProcessoSeletivoException();
                        }
                    }
                }
            }

            List<CampanhaOferta> OfertasTurma = new List<CampanhaOferta>();

            /*3. Se o processo seletivo das ofertas em questão estiver configurado para trabalhar com reserva de vaga:*/
            var processosSeletivosReservaVaga = processosSeletivos.Where(p => p.ReservaVaga);
            if (processosSeletivosReservaVaga.SMCAny())
            {
                /*3.1. Verificar se para todas as ofertas do tipo TURMA que tiverem sua quantidade de
                 * vagas aumentada existe vaga suficiente na respectiva turma.
                 * A disponibilidade de vagas na turma deve ser verificada da seguinte forma
                 * (novo valor de vaga - valor de vaga anterior) <= (quantidade de vagas da divisão da turma - quantidade de vagas ocupadas).
                 * Caso alguma turma não tenha vagas suficientes, abortar a operação e emitir a mensagem de erro:
                  "Não é permitido aumentar a quantidade de vagas das ofertas listadas abaixo, pois suas turmas não possuem vagas suficientes:
                    <lista de ofertas cujas turmas não possuem vagas suficientes>" */
                var ofertasTurma = campanhasOfertas.Where(c => c.TipoOferta.Token == TOKEN_TIPO_OFERTA.TURMA);

                List<CampanhaOferta> OfertasTurmaVagasInsuficientes = new List<CampanhaOferta>();
                List<CampanhaOferta> OfertasTurmaVagasOcupadasInsuficientes = new List<CampanhaOferta>();

                foreach (var processoSeletivoReservaVaga in processosSeletivosReservaVaga)
                {
                    foreach (var ofertaReservaVaga in processoSeletivoReservaVaga.Ofertas)
                    {
                        var novaOfertaCampanha = model.CampanhaOfertas.FirstOrDefault(p => p.Seq == ofertaReservaVaga.SeqCampanhaOferta);

                        if (novaOfertaCampanha == null) { continue; }

                        //Busco a oferta do tipo turma, que corresponde a oferta alterada pelo usuário e que possui configuração
                        //para trabalhar com reserva de vaga
                        var oldOfertaCampanhaTurma = ofertasTurma.FirstOrDefault(t => t.Seq == ofertaReservaVaga.SeqCampanhaOferta);

                        //Se não houver Oferta do Tipo Turma, pulo a iteração
                        if (oldOfertaCampanhaTurma == null) { continue; }

                        //Adiciono as ofertas do tipo turma para a lista de atualização do item 3.3
                        OfertasTurma.Add(oldOfertaCampanhaTurma);

                        // Buscar a divisão da turma
                        var specOferta = new CampanhaOfertaFilterSpecification() { Seq = novaOfertaCampanha.Seq };
                        var divisaoTurma = SearchProjectionByKey(specOferta, x => x.Itens.Select(s => s.Turma.DivisoesTurma.FirstOrDefault())).FirstOrDefault();

                        //Oferta com vaga aumentada
                        if (novaOfertaCampanha.VagasDiferenca > 0)
                        {
                            bool possuiVagas = false;

                            if (divisaoTurma != null)
                            {
                                int novaOfertaCampanhaVagas = novaOfertaCampanha.VagasDiferenca + novaOfertaCampanha.VagasBase
                                    , divisaoTurmaVagas = divisaoTurma.QuantidadeVagas
                                    , divisaoTurmaVagasOcupadas = (int)(divisaoTurma.QuantidadeVagasOcupadas ?? 0);

                                //A disponibilidade de vagas na turma deve ser verificada da seguinte forma:
                                // (novo valor de vaga - valor de vaga anterior) <= (quantidade de vagas da divisão da turma - quantidade de vagas ocupadas).
                                possuiVagas = (novaOfertaCampanhaVagas - novaOfertaCampanha.VagasBase) <= (divisaoTurmaVagas - divisaoTurmaVagasOcupadas);
                            }

                            if (!possuiVagas)
                            {
                                OfertasTurmaVagasInsuficientes.Add(oldOfertaCampanhaTurma);
                            }
                        }
                        //Oferta com quantidade de vagas diminuída
                        else if (novaOfertaCampanha.VagasDiferenca < 0)
                        {
                            int novaOfertaCampanhaVagas = novaOfertaCampanha.VagasDiferenca + novaOfertaCampanha.VagasBase;
                            /*3.2. Verificar se para todas as ofertas do tipo TURMA que tiverem sua quantidade de
                             * vagas diminuída o novo valor não vai ficar menor que a quantidade de
                             * vagas ocupadas na própria oferta do processo seletivo.*/
                            bool qtdVagasInsuficiente = novaOfertaCampanhaVagas < ofertaReservaVaga.QuantidadeVagasOcupadas;

                            /* Caso o valor de vagas de alguma oferta fique menor que as vagas ocupadas, abortar
                            * a operação e emitir a mensagem de erro:
                            "Não é permitido diminuir as vagas das ofertas listadas abaixo, pois já estão ocupadas:
                            <lista de ofertas cujo novo valor de vaga ficou menor que a quantidade de vagas ocupadas>*/
                            if (qtdVagasInsuficiente)
                            {
                                OfertasTurmaVagasOcupadasInsuficientes.Add(oldOfertaCampanhaTurma);
                            }
                        }

                        /*3.3. Para as ofertas do tipo TURMA, atualizar a diferença do novo valor de vaga para o valor anterior,
                         * na quantidade de vagas ocupadas da divisão da turma de cada oferta alterada.*/
                        if (divisaoTurma != null)
                        {
                            divisaoTurma.QuantidadeVagasOcupadas += (short)novaOfertaCampanha.VagasDiferenca;

                            DivisaoTurmaDomainService.SaveEntity(divisaoTurma);
                        }
                    }
                }
                /* Caso alguma turma não tenha vagas suficientes, abortar a operação e emitir a mensagem de erro:
                 *
                 * "Não é permitido aumentar a quantidade de vagas das ofertas listadas abaixo, pois suas turmas
                 *  não possuem vagas suficientes: <lista de ofertas cujas turmas não possuem vagas suficientes>"*/
                if (OfertasTurmaVagasInsuficientes.Count() > 0)
                {
                    var ofertas = string.Join(", ", OfertasTurmaVagasInsuficientes.Select(o => o.Descricao).ToArray());
                    throw new OfertaVagaTurmaInsuficienteException(ofertas);
                }

                /* Caso o valor de vagas de alguma oferta fique menor que as vagas ocupadas, abortar a operação e
                 * emitir a mensagem de erro:
                  "Não é permitido diminuir as vagas das ofertas listadas abaixo, pois já estão ocupadas:
                   <lista de ofertas cujo novo valor de vaga ficou menor que a quantidade de vagas ocupadas>*/
                if (OfertasTurmaVagasOcupadasInsuficientes.Count() > 0)
                {
                    var ofertas = string.Join(", ", OfertasTurmaVagasOcupadasInsuficientes.Select(o => o.Descricao).ToArray());
                    throw new OfertaVagaTurmaOcupadaInsuficienteException(ofertas);
                }
            }
        }

        /// <summary>
        /// Replica e Persiste as vagas do processo seletivo e suas respectivas convocações selecionados
        /// </summary>
        /// <param name="processosSeletivos"></param>
        /// <param name="campanhaOfertas"></param>
        private void SalvarVagasOfertas(IEnumerable<ProcessoSeletivo> processosSeletivos, List<CampanhaOfertaListaVO> campanhaOfertas)
        {
            //Percorro os processos seletivos
            foreach (var processoSeletivo in processosSeletivos)
            {
                //Percorro as ofertas da campanha
                foreach (var campanhaOfertaAlterada in campanhaOfertas)
                {
                    //Busco as ofertas do processo seletivo, que estão fazem parta da oferta da campanha
                    var ofertasProcessoSeletivo = processoSeletivo.Ofertas.Where(p => p.SeqCampanhaOferta == campanhaOfertaAlterada.Seq).ToList();

                    //Percorro as ofertas do processo seletivo
                    foreach (var ofertaProcessoSeletivo in ofertasProcessoSeletivo)
                    {
                        //RN_CAM_018 - Atualização das vagas das ofertas da campanha
                        //1.1. Caso esteja diminuindo a quantidade de vagas, verificar se a diferença de vagas
                        //é maior que a quantidade de vagas da respectiva oferta do processo seletivo.
                        ValidarReducaoVagasOfertaProcessoSeletivo(ofertaProcessoSeletivo, campanhaOfertaAlterada.VagasDiferenca);

                        //Altero as vagas da oferta do processo seletivo com a diferença do valor das vagas da
                        //Oferta da campanha.
                        ofertaProcessoSeletivo.QuantidadeVagas += (short)campanhaOfertaAlterada.VagasDiferenca;

                        //Persisto a alteração da oferta do processo seletivo
                        ProcessoSeletivoOfertaDomainService.SaveEntity(ofertaProcessoSeletivo);
                    }
                    if (ofertasProcessoSeletivo.SMCAny())
                    {
                        //Convocação
                        if (processoSeletivo.Convocacoes.SMCAny())
                        {
                            foreach (var convocacao in processoSeletivo.Convocacoes)
                            {
                                // Percorro as ofertas da convocação
                                if (convocacao.Ofertas.SMCAny())
                                {
                                    var ofertasConvocacao = convocacao.Ofertas.Where(c => ofertasProcessoSeletivo.Any(op => op.Seq == c.SeqProcessoSeletivoOferta)).ToList();
                                    foreach (var convocacaoOferta in ofertasConvocacao)
                                    {
                                        // 1.1. Caso esteja diminuindo a quantidade de vagas, verificar se a diferença de vagas é maior que a quantidade de
                                        // vagas da respectiva oferta da convocação.
                                        ValidarReducaoVagasOfertaConvocacao(convocacaoOferta, campanhaOfertaAlterada.VagasDiferenca);

                                        //diferença do valor das vagas da Oferta da campanha.
                                        convocacaoOferta.QuantidadeVagas += (short)campanhaOfertaAlterada.VagasDiferenca;

                                        ConvocacaoOfertaDomainService.SaveEntity(convocacaoOferta);
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        /// <summary>
        /// RN_CAM_037 - Atualização das vagas das ofertas do processo seletivo
        /// 1.1. Caso esteja diminuindo a quantidade de vagas, verificar se a diferença de vagas é maior que a quantidade de vagas da respectiva oferta da convocação. Em caso afirmativo, abortar a operação e emitir a mensagem de erro:
        /// "Alteração não permitida. Não é possível reduzir <diferença de vagas> vagas da oferta <descrição da oferta da campanha> na convocação <descrição da convocação que não tem vagas suficientes para a oferta>, pois não existem vagas suficientes."
        /// </summary>
        /// <param name="convocacaoOferta"></param>
        /// <param name="vagasDiferenca"></param>
        public void ValidarReducaoVagasOfertaConvocacao(ConvocacaoOferta convocacaoOferta, int vagasDiferenca)
        {
            // Verifico se há uma redução do número de vagas
            if (vagasDiferenca < 0)
            {
                // 1.1. Caso esteja diminuindo a quantidade de vagas, verificar se a diferença de vagas é maior que a
                // quantidade de vagas da respectiva oferta da convocação.
                if (Math.Abs(vagasDiferenca) > convocacaoOferta.QuantidadeVagas)
                {
                    // Em caso afirmativo, abortar a operação e emitir a mensagem de erro:
                    // "Alteração não permitida. Não é possível reduzir <diferença de vagas> vagas da oferta
                    // <descrição da oferta da campanha> na convocação <descrição da convocação que não tem vagas suficientes para a oferta>, pois não existem vagas suficientes."
                    var descOferta = ConvocacaoOfertaDomainService.SearchProjectionByKey(convocacaoOferta.Seq, x => x.ProcessoSeletivoOferta.CampanhaOferta.Descricao);

                    var descConvocacao = ConvocacaoDomainService.SearchProjectionByKey(convocacaoOferta.SeqConvocacao, x => x.Descricao);

                    throw new ReducaoVagaOfertaConvocacaoInvalidaException(Math.Abs(vagasDiferenca).ToString(), descOferta, descConvocacao);
                }
            }
        }

        /// <summary>
        /// RN_CAM_018 - Atualização das vagas das ofertas da campanha
        /// 1. Para os processos seletivos selecionados, para cada oferta alterada:
        /// 1.1. Caso esteja diminuindo a quantidade de vagas, verificar se a diferença de vagas é maior que a quantidade de vagas da respectiva oferta do processo seletivo.Em caso afirmativo, abortar a operação e emitir a mensagem de erro:
        /// "Alteração não permitida. Não é possível reduzir <diferença de vagas> vagas da oferta <descrição da oferta da campanha> no processo seletivo <descrição do processo seletivo que não tem vagas suficientes para a oferta>, pois não existem vagas suficientes."
        /// </summary>
        /// <param name="ofertaProcessoSeletivo"></param>
        /// <param name="vagasDiferenca"></param>
        public void ValidarReducaoVagasOfertaProcessoSeletivo(ProcessoSeletivoOferta ofertaProcessoSeletivo, int vagasDiferenca)
        {
            // Verifico se há uma redução do número de vagas
            if (vagasDiferenca < 0)
            {
                //1.1. Caso esteja diminuindo a quantidade de vagas.
                //verificar se a diferença de vagas é maior que a quantidade de vagas da respectiva oferta do processo seletivo
                if (Math.Abs(vagasDiferenca) > ofertaProcessoSeletivo.QuantidadeVagas)
                {
                    //Em caso afirmativo, abortar a operação e emitir a mensagem de erro:
                    //"Alteração não permitida. Não é possível reduzir <diferença de vagas> vagas da oferta <descrição da oferta da campanha> no processo seletivo
                    //<descrição do processo seletivo que não tem vagas suficientes para a oferta>, pois não existem vagas suficientes."
                    var descOferta = this.SearchProjectionByKey(ofertaProcessoSeletivo.SeqCampanhaOferta, x => x.Descricao);

                    var descProcessoSeletivo = ProcessoSeletivoDomainService.SearchProjectionByKey(ofertaProcessoSeletivo.SeqProcessoSeletivo, x => x.Descricao);

                    throw new ReducaoVagaOfertaProcessoSeletivoInvalidaException(Math.Abs(vagasDiferenca).ToString(), descOferta, descProcessoSeletivo);
                }
            }
        }

        /// <summary>
        /// Valida se alguma replicação irá ocorrer, gerando inconsistências com as vagas das outras ofertas da hierarquia
        /// </summary>
        /// <param name="model"></param>
        private void ValidarVagasHierarquia(VagasCampanhaOfertaVO model)
        {
            if (model.SeqsProcessosSeletivos == null || !model.SeqsProcessosSeletivos.Any()) { return; }

            var seqsProcessosSeletivos = CampanhaDomainService.SearchProjectionByKey(new SMCSeqSpecification<Campanha>(model.SeqCampanha),
                c => c.ProcessosSeletivos.Where(p => !model.SeqsProcessosSeletivos.Contains(p.Seq)).Select(p => p.Seq)).ToArray();

            if (!seqsProcessosSeletivos.Any()) return;

            var specProcessosSeletivos = new ProcessoSeletivoFilterSpecification() { SeqCampanha = model.SeqCampanha, SeqsProcessosSeletivos = seqsProcessosSeletivos };

            var processosSeletivos = this.ProcessoSeletivoDomainService.SearchBySpecification(specProcessosSeletivos, IncludesProcessoSeletivo.Ofertas).ToList();

            foreach (var ofertaCampanha in model.CampanhaOfertas)
            {
                foreach (var processoSeletivo in processosSeletivos)
                {
                    if (processoSeletivo.Ofertas.Any())
                    {
                        var ofertaProcessoSeletivo = processoSeletivo.Ofertas.FirstOrDefault(o => o.SeqCampanhaOferta == ofertaCampanha.Seq && ofertaCampanha.Vagas > o.QuantidadeVagas);
                        if (ofertaProcessoSeletivo != null) { throw new OfertaVagaMenorProcessoSeletivoException(); }
                    }
                }
            }
        }

        /// <summary>
        /// Vincular os seqs, dos processos seletivos e suas convocações, Selecionados na treeview.
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        private IEnumerable<ProcessoSeletivo> VincularProcessoSeletivoConvocacao(VagasCampanhaOfertaVO data)
        {
            var specPs = new ProcessoSeletivoFilterSpecification { SeqCampanha = data.SeqCampanha, SeqsProcessosSeletivos = data.SeqsProcessosSeletivos };

            var includeProcessoSeletivo = IncludesProcessoSeletivo.Ofertas;

            //Busco os processos seletivos
            var processosSeletivos = ProcessoSeletivoDomainService.SearchBySpecification(specPs, includeProcessoSeletivo).ToList();

            var specConv = new ConvocacaoFilterSpecification { SeqCampanha = data.SeqCampanha, Seqs = data.SeqsConvocacoes };

            // Busco as convocações
            var convocacoes = ConvocacaoDomainService.SearchBySpecification(specConv, IncludesConvocacao.Ofertas);

            // Valido as convocações selecionadas
            if (data.SeqsConvocacoes != null && data.SeqsConvocacoes.Any() && convocacoes != null && convocacoes.Any())
            {
                foreach (var processoSeletivo in processosSeletivos)
                {
                    //Vinculo as convocações selecionadas para seu respectivo processo seletivo
                    processoSeletivo.Convocacoes = convocacoes.Where(c => c.SeqProcessoSeletivo == processoSeletivo.Seq
                                                                        && data.SeqsConvocacoes.Contains(c.Seq)).ToList();
                }
            }

            return processosSeletivos;
        }

        /// <summary>
        /// Validar se, ao selecionar as ofertas de convocação, foram selecionados seus respectivos processos seletivos
        /// ou seja, se existe convocações sem Pai
        /// </summary>
        /// <param name="model"></param>
        private void ValidarReplicarVagasOferta(VagasCampanhaOfertaVO model)
        {
            if (model.SeqsConvocacoes == null || !model.SeqsConvocacoes.Any()) { return; }

            var specPs = new ProcessoSeletivoFilterSpecification
            {
                SeqCampanha = model.SeqCampanha,
                SeqsConvocacoes = model.SeqsConvocacoes
            };

            var includeProcessoSeletivo = IncludesProcessoSeletivo.Ofertas
                                           | IncludesProcessoSeletivo.Convocacoes
                                           | IncludesProcessoSeletivo.Convocacoes_Ofertas;

            //Busco os processos seletivos, baseados nas convocações selecionadas
            var processosSeletivos = ProcessoSeletivoDomainService.SearchBySpecification(specPs, includeProcessoSeletivo);

            foreach (var processo in processosSeletivos)
            {
                if (!model.SeqsProcessosSeletivos.Contains(processo.Seq))
                {
                    throw new ReplicarOfertaVagaProcessoSeletivoException();
                }
            }
        }

        #endregion Configurar Vagas

        public List<SMCDatasourceItem> BuscarCampanhaOfertasConvocacoes(CampanhaOfertaFiltroTelaVO filtro)
        {
            var spec = filtro.Transform<CampanhaOfertaFilterTelaSpecification>();
            var lista = SearchProjectionByKey(spec, x => x.Campanha.ProcessosSeletivos
            .SelectMany(a => a.Convocacoes.Select(
                        c => new SMCDatasourceItem()
                        {
                            Seq = c.Seq,
                            Descricao = c.Descricao
                        }).ToList()
                )).ToList();

            return lista;
        }

        #region Lookup Ofertas

        /// <summary>
        /// Método principal de pesquisa do lookup de ofertas da campanha
        /// </summary>
        /// <param name="filtro"></param>
        /// <returns>Lista de ofertas</returns>
        public SMCPagerData<SelecaoOfertaCampanhaLookupListaVO> BuscarCampanhasOfertaSelecaoLookup(SelecaoOfertaCampanhaLookupFiltroVO filtro)
        {
            var spec = filtro.Transform<CursoOfertaLocalidadeTurnoFilterSpecification>();
            spec.SetPageSetting(new SMCPageSetting() { PageSize = int.MaxValue });

            var include = IncludesCampanha.CiclosLetivos_CicloLetivo
                        | IncludesCampanha.EntidadeResponsavel_TipoEntidade;

            var campanha = CampanhaDomainService.SearchByKey(filtro.SeqCampanha.Value, include);

            ///Tipo Oferta
            var tipoOferta = TipoOfertaDomainService.SearchByKey(filtro.SeqTipoOferta.Value);
            filtro.TipoOfertaToken = tipoOferta?.Token;

            ///Tipo Entidade Responsável
            var tipoEntidadeResponsavel = campanha.EntidadeResponsavel.TipoEntidade;

            // Validar se o tipo de entidade responsável é do tipo grupo de programa
            if (tipoEntidadeResponsavel.Token != TOKEN_TIPO_ENTIDADE.GRUPO_PROGRAMA) { return null; }

            // Valido se é do tipo de oferta de turma ou se exige curso de oferta localidade turno
            if (tipoOferta == null || (filtro.TipoOfertaToken != TOKEN_TIPO_OFERTA.TURMA && !tipoOferta.ExigeCursoOfertaLocalidadeTurno)) { return null; }

            ///Menor ciclo letivo da campanha
            var cicloLetivo = BuscarCicloLetivoMaisAntigo(campanha.CiclosLetivos);
            filtro.SeqMenorCicloLetivoCampanha = cicloLetivo?.SeqCicloLetivo;
            if (tipoEntidadeResponsavel.Token != TOKEN_TIPO_OFERTA.ORIENTADOR
                                                    && filtro.Seqs.SMCAny()
                                                    && !filtro.Seqs.First().ToString().Contains(SEPARADOR_SEQS))
            {
                spec.Seqs = filtro.Seqs.Select(s => Int64.Parse(s.ToString())).ToArray();
            }
            // Lista para preenchimento dinâmico
            var ofertasCampanha = new List<SelecaoOfertaCampanhaLookupListaVO>();

            switch (filtro.TipoOfertaToken)
            {
                /*Caso o "Tipo de oferta" seja CURSO_OFERTA_LOCALIDE_TURNO, retornar todos os
                 cursos oferta localidade turno, de acordo com os filtros informados. Deverão ser
                 preenchidos e exibidos somente os campos "Curso oferta", "Localidade" e "Turno".*/
                case TOKEN_TIPO_OFERTA.CURSO_OFERTA_LOCALIDADE_TURNO:

                    ofertasCampanha = BuscarProjecaoCampanhaOfertaCompleta(spec, filtro);

                    break;

                /*Caso o "Tipo de oferta" seja AREA_CONCETRACAO, retornar todos os cursos oferta
                localidade turno e suas respectivas áreas de concentração de acordo com os
                filtros informados. Deverão ser preenchidos e exibidos somente os campos
                "Curso oferta", "Localidade", "Turno" e "Área de Concentração".*/
                case TOKEN_TIPO_OFERTA.AREA_CONCENTRACAO:

                    ofertasCampanha = BuscarProjecaoCampanhaOfertaCompleta(spec, filtro);

                    break;

                /*Caso o "Tipo de oferta" seja AREA_TEMATICA, retornar todos os cursos oferta
                localidade turno e suas respectivas áreas de concentração de acordo com os
                filtros informados. Deverão ser preenchidos e exibidos somente os campos
                "Curso oferta", "Localidade", "Turno" e "Área temática".*/
                case TOKEN_TIPO_OFERTA.AREA_TEMATICA:

                    ofertasCampanha = BuscarProjecaoCampanhaOfertaCompleta(spec, filtro);

                    break;

                /*Caso o "Tipo de oferta" seja LINHA_PESQUISA, retornar todos os cursos oferta
                localidade turno, com suas respectivas áreas de concentração e linhas de pesquisa,
                de acordo com os filtros informados. Deverão ser preenchidos e exibidas somente os
                campos "Curso oferta", "Localidade", "Turno", "Área de Concentração" e "Linha de Pesquisa".*/
                case TOKEN_TIPO_OFERTA.LINHA_PESQUISA:

                    ofertasCampanha = BuscarProjecaoCampanhaOfertaCompleta(spec, filtro);

                    break;

                /*Caso o "Tipo de oferta" seja EIXO_TEMATICO, retornar todos os cursos oferta
                localidade turno, com suas respectivas áreas de concentração, linhas de pesquisa
                e eixos temáticos, de acordo com os filtros informados. Deverão ser preenchidos e
                exibidos somente os campos "Curso oferta", "Localidade", "Turno", "Área de
                Concentração", "Linha de Pesquisa" e "Eixo Temático".*/
                case TOKEN_TIPO_OFERTA.EIXO_TEMATICO:

                    ofertasCampanha = BuscarProjecaoCampanhaOfertaCompleta(spec, filtro);

                    break;

                /*Caso o "Tipo de oferta" seja ORIENTADOR, retornar todos os cursos
                oferta localidade turno, de acordo com os filtros informados e, para cada
                um deles, retornar os colaboradores com atividade de orientação que possuem
                vínculo no respectivo curso oferta localidade e o vínculo de colaborador está ativo.
                Caso exista mais de um turno por curso oferta localide, os orientadores deverão se
                repetir em cada um dos turnos. Deverão ser preenchidos e exibidos somente os
                campos "Curso oferta", "Localidade", "Turno" e "Orientador.*/
                case TOKEN_TIPO_OFERTA.ORIENTADOR:

                    var ofertasCampanhaOrientador = BuscarProjecaoCampanhaOfertaCompleta(spec, filtro);

                    // Vincula os cursos e os orientadores, atribuindo os seqs duplos para cada oferta
                    // (seq de cursoOfertaLocalidadeTurno e seq de orientador)
                    var ofertasCampanhaOrientadorVinculado = VincularOrientadoresOfertaCampanha(ofertasCampanhaOrientador, cicloLetivo, filtro.SeqOrientador);

                    //Filtro dos Seqs dos itens na tela.
                    ofertasCampanha = FiltrarCampanhaOfertaOrientadores(ofertasCampanhaOrientadorVinculado, filtro);

                    break;

                /*Caso o "Tipo de oferta" seja TURMA, retornar todas as turmas de acordo com os
                filtros informados e cujos ciclos letivos da campanha coincidam com o
                período dos ciclos letivos de início e fim da turma. Deverá ser preenchido e
                exibido somente o campo "Turma".*/
                case TOKEN_TIPO_OFERTA.TURMA:

                    /*(NV03) Se a unidade responsável pela campanha informada por parâmetro for um grupo de programa:
                     * se o "Tipo de oferta" for TURMA, retornar as turmas cujo curso oferta localidade turno
                     * de alguma das suas ofertas de matriz está associado ao grupo de programa da unidade
                     * responsável da campanha. Considerar também os filtros abaixo:*/
                    ofertasCampanha = BuscarOfertasCampanhaTurma(filtro, cicloLetivo, campanha.SeqEntidadeResponsavel);

                    break;
            }

            /// Validação NV03
            /// Se o orientador tiver sido selecionado, se o "Tipo de oferta" exigir o curso oferta localidade
            /// turno, retornar os curso oferta localidade turno cujo colaborador possui vínculo ativo em seu
            /// curso oferta localidade, com atividade do tipo orientação.
            ofertasCampanha = BuscarOfertasCampanhaVinculoColaborador(ofertasCampanha, filtro, campanha.CiclosLetivos);

            ofertasCampanha = TratarRegistrosVazios(ofertasCampanha, filtro.TipoOfertaToken);

            ofertasCampanha?.ForEach(o => FormatarDescricaoOfertaCampanha(o, tipoOferta));

            // No resultado de pesquisa, retornar os dados ordenados alfabeticamente por
            //curso oferta, localidade, turno, área de concentração, linha de pesquisa, eixo temático, orientador e turma.
            ofertasCampanha = OrdenarCampanhaOfertaLookup(ofertasCampanha);

            int total = ofertasCampanha != null ? ofertasCampanha.Count : 0;
            return new SMCPagerData<SelecaoOfertaCampanhaLookupListaVO>(ofertasCampanha, total);
        }

        /// <summary>
        /// Efetua o filtro dos registros com seqs duplos selecionados (orientador e cursoOfertaLocalidadeTurno)
        /// </summary>
        /// <param name="ofertasCampanhaOrientadorVinculado"></param>
        /// <param name="filtro"></param>
        /// <returns>Lista de Ofertas da campanha por Orientador</returns>
        private List<SelecaoOfertaCampanhaLookupListaVO> FiltrarCampanhaOfertaOrientadores(List<SelecaoOfertaCampanhaLookupListaVO> ofertasCampanhaOrientadorVinculado, SelecaoOfertaCampanhaLookupFiltroVO filtro)
        {
            if (!ofertasCampanhaOrientadorVinculado.SMCAny() || !filtro.Seqs.SMCAny() || !filtro.Seqs.First().ToString().Contains(SEPARADOR_SEQS)) { return ofertasCampanhaOrientadorVinculado; }

            return ofertasCampanhaOrientadorVinculado.Where(o => filtro.Seqs.Contains(o.Seq)).ToList();
        }

        /// <summary>
        /// Validação NV03
        /// Se o orientador tiver sido selecionado, se o "Tipo de oferta" exigir o curso oferta localidade
        /// turno, retornar os curso oferta localidade turno cujo colaborador possui vínculo ativo em seu
        /// curso oferta localidade, com atividade do tipo orientação.
        /// </summary>
        /// <param name="ofertasCampanha"></param>
        /// <param name="filtro"></param>
        /// <param name="ciclosLetivos"></param>
        /// <returns></returns>
        private List<SelecaoOfertaCampanhaLookupListaVO> BuscarOfertasCampanhaVinculoColaborador(List<SelecaoOfertaCampanhaLookupListaVO> ofertasCampanha, SelecaoOfertaCampanhaLookupFiltroVO filtro, IList<CampanhaCicloLetivo> ciclosLetivos)
        {
            if (filtro.SeqOrientador.HasValue && filtro.TipoOfertaToken != TOKEN_TIPO_OFERTA.ORIENTADOR && filtro.TipoOfertaToken != TOKEN_TIPO_OFERTA.TURMA)
            {
                var ofertasColaborador = new List<SelecaoOfertaCampanhaLookupListaVO>();
                var cicloLetivo = BuscarCicloLetivoMaisAntigo(ciclosLetivos);

                /// Percorro as ofertas com o SeqCursoOfertaLocalidadeTurno filtrado para buscar os colaboradores
                /// com atividade de orientação que possuem vínculo no respectivo curso oferta localidade
                /// e o vínculo de colaborador está ativo.
                foreach (var ofertaItem in ofertasCampanha)
                {
                    //Faço o filtro para cada Curso oferta localidade turno, busco os  colaboradores que possuem vínculo ativo e que estão
                    //relacionados com os filtros da campanha oferta
                    var orientadores = BuscarOrientadoresComVinculoAtivo(cicloLetivo, ofertaItem.SeqCursoOfertaLocalidadeTurno.Value, filtro.SeqOrientador);

                    // se houver orientador vinculado, adiciono a oferta à lista de ofertas vinculadas ao colaborador
                    if (orientadores.SMCAny())
                    {
                        ofertasColaborador.Add(ofertaItem);
                    }
                }

                // Faço o vínculo do filtro dos curso-oferta-localidade-turno, que possuem vínculo com o colaborador selecionado
                ofertasCampanha = ofertasColaborador.ToList();
            }
            return ofertasCampanha;
        }

        private List<SelecaoOfertaCampanhaLookupListaVO> BuscarOfertasCampanhaTurma(SelecaoOfertaCampanhaLookupFiltroVO filtro, CampanhaCicloLetivo cicloLetivoTurma, long seqEntidadeResponsavel)
        {
            /*(NV03) Se a unidade responsável pela campanha informada por parâmetro for um grupo de programa:
                     * se o "Tipo de oferta" for TURMA, retornar as turmas cujo curso oferta localidade turno
                     * de alguma das suas ofertas de matriz está associado ao grupo de programa da unidade
                     * responsável da campanha. Considerar também os filtros abaixo:*/

            ///Crio o specification com os parâmetros que foram passados para o lookup
            var turmaSpec = new TurmaFilterSpecification()
            {
                SeqEntidadeGrupoPrograma = seqEntidadeResponsavel,
                SeqNivelEnsino = filtro.SeqNivelEnsino,
                SeqsEntidadeResponsavel = filtro.SeqsEntidadeResponsavel,
                SeqLocalidade = filtro.SeqLocalidade,
                SeqCursoOferta = filtro.SeqCursoOferta,
                SeqTurno = filtro.SeqTurno,
                DescricaoConfiguracao = filtro.Turma,
                TurmaComOrientacao = false,
                TurmaSituacaoNaoCancelada = true,
                NumeroCicloLetivo = cicloLetivoTurma.CicloLetivo.Numero,
                AnoCicloLetivo = cicloLetivoTurma.CicloLetivo.Ano,
                SeqsTurma = filtro.Seqs?.Select(s => Int64.Parse(s.ToString())).ToList()
            };

            return TurmaDomainService.SearchProjectionBySpecification(turmaSpec, x =>
                   new SelecaoOfertaCampanhaLookupListaVO()
                   {
                       Seq = x.Seq.ToString(),
                       SeqTurma = x.Seq,
                       TipoOfertaToken = filtro.TipoOfertaToken,
                       SeqCampanha = filtro.SeqCampanha,
                       SeqTipoOferta = filtro.SeqTipoOferta
                   }
                  ).ToList();
        }

        /// <summary>
        /// Método específico para a projeção de ofertas do lookup da campanha.
        /// Método que busca todos os campos para as buscas de ofertas do Curso-Oferta-Localidade-Turno
        /// E aplica o Filtro NV06
        /// </summary>
        /// <param name="spec"></param>
        /// <param name="filtro"></param>
        /// <returns>Lista de ofertas da campanha do Curso-Oferta-Localidade-Turno</returns>
        private List<SelecaoOfertaCampanhaLookupListaVO> BuscarProjecaoCampanhaOfertaCompleta(CursoOfertaLocalidadeTurnoFilterSpecification spec, SelecaoOfertaCampanhaLookupFiltroVO filtro)
        {
            var retorno = CursoOfertaLocalidadeTurnoDomainService.SearchProjectionBySpecification(spec, x => new SelecaoOfertaCampanhaLookupListaVO
            {
                Seq = x.Seq.ToString(),

                TipoOfertaToken = filtro.TipoOfertaToken,

                SeqCursoOfertaLocalidadeTurno = x.Seq,

                SeqCursoOferta = x.CursoOfertaLocalidade.SeqCursoOferta,

                /*Curso oferta*/
                CursoOferta = x.CursoOfertaLocalidade.CursoOferta.Descricao,

                SeqCursoOfertaLocalidade = x.CursoOfertaLocalidade.HierarquiasEntidades.FirstOrDefault().ItemSuperior.Entidade.Seq,

                /*Localidade*/
                Localidade = x.CursoOfertaLocalidade.HierarquiasEntidades.FirstOrDefault().ItemSuperior.Entidade.Nome,

                SeqFormacaoEspecifica = x.CursoOfertaLocalidade.CursoOferta.SeqFormacaoEspecifica,

                SeqTurno = x.SeqTurno,

                /*Turno*/
                Turno = x.Turno.Descricao,

                /*Área de Temática*/
                AreaTematica = x.CursoOfertaLocalidade.FormacoesEspecificas
                                                                .FirstOrDefault(f => f.FormacaoEspecifica
                                                                        .TipoFormacaoEspecifica.Token == TOKEN_TIPO_FORMACAO_ESPECIFICA.AREA_TEMATICA).FormacaoEspecifica.Descricao,

                /*Área de Concentração*/
                AreaConcentracao = x.CursoOfertaLocalidade.FormacoesEspecificas
                                                         .FirstOrDefault(f => f.FormacaoEspecifica.FormacaoEspecificaSuperior
                                                                 .TipoFormacaoEspecifica.Token == TOKEN_TIPO_FORMACAO_ESPECIFICA.AREA_CONCENTRACAO).FormacaoEspecifica.FormacaoEspecificaSuperior.Descricao,
                /*Linha de Pesquisa*/
                LinhaPesquisa = x.CursoOfertaLocalidade.FormacoesEspecificas
                                                                 .FirstOrDefault(f => f.FormacaoEspecifica
                                                                         .TipoFormacaoEspecifica.Token == TOKEN_TIPO_FORMACAO_ESPECIFICA.LINHA_PESQUISA).FormacaoEspecifica.Descricao,
                /*Eixo Temático*/
                EixoTematico = x.CursoOfertaLocalidade.FormacoesEspecificas
                                                                 .FirstOrDefault(f => f.FormacaoEspecifica
                                                                         .TipoFormacaoEspecifica.Token == TOKEN_TIPO_FORMACAO_ESPECIFICA.EIXO_TEMATICO).FormacaoEspecifica.Descricao,

                SeqCampanha = filtro.SeqCampanha,
                SeqTipoOferta = filtro.SeqTipoOferta
            }).ToList();

            //Aplico os filtros e validações - LK_CAM_003 - NV06
            return AplicarFiltrosOfertas_LK_CAM_003_NV06(spec, filtro, retorno);
        }

        /// <summary>
        /// LK_CAM_003 - NV06
        /// Se o tipo de oferta exigir curso-oferta-localidade-turno, não retornar a oferta se:
        ///  - O curso do curso-oferta-localidade-turno da oferta estiver com a categoria da situação "Em desativação" ou "Inativa"
        /// na data início do período letivo* do menor ciclo letivo da campanha informada por parâmetro.
        ///  - O curso-unidade do curso-oferta-localidade-turno estiver com a categoria da situação "Em desativação" ou "Inativa" na data
        /// início do período letivo* do menor ciclo letivo da campanha informada por parâmetro.
        ///  - O curso-oferta do curso-oferta-localidade-turno estiver desativado.
        ///  - O curso-oferta-localidade do curso-oferta-localidade-turno estiver com a categoria da situação “Em desativação” ou “Inativa”
        /// na data início do período letivo* do menor  ciclo letivo da campanha informada por parâmetro.
        ///  - A formação específica estiver desativada ou a formação específica por curso não estiver mais vigente na data·
        ///  início do período letivo* do menor ciclo letivo da campanha informada por parâmetro.
        ///  - O turno do curso-oferta-localidade-turno estiver desativado para o curso-oferta-localidade do curso-oferta-localidade-turno.
        ///
        /// Se o tipo de oferta não exigir curso-oferta-localidade-turno e for do tipo TURMA, não retornar a oferta se:
        ///  - A turma estiver cancelada.
        ///  - A turma tiver divisão de componente cujo tipo gere orientação.
        ///
        /// * O período letivo deverá ser retornado com base na regra RN_CAM_030 - Retorna período do evento letivo, informado
        /// como parâmetro: o menor ciclo letivo da campanha informada por parâmetro, o tipo de evento PERIODO_LETIVO,
        /// o curso-oferta-localidade-turno e o tipo de aluno “Calouro”.
        /// </summary>
        /// <param name="spec"></param>
        /// <param name="filtro"></param>
        /// <param name="retorno"></param>
        /// <returns>Lista de ofertas da campanha, válidas no período</returns>
        private List<SelecaoOfertaCampanhaLookupListaVO> AplicarFiltrosOfertas_LK_CAM_003_NV06(CursoOfertaLocalidadeTurnoFilterSpecification spec, SelecaoOfertaCampanhaLookupFiltroVO filtro, List<SelecaoOfertaCampanhaLookupListaVO> ofertasCampanha)
        {
            var specValidacao = spec.Transform<CursoOfertaLocalidadeTurnoFilterSpecification>();

            List<SelecaoOfertaCampanhaLookupListaVO> campanhaOfertasFiltradas = new List<SelecaoOfertaCampanhaLookupListaVO>();
            // Ativo as validaçções, a serem feitas pelo specification
            specValidacao.CursoAtivoDataInicioCicloLetivo =
            specValidacao.CursoUnidadeAtivoDataInicioCicloLetivo =
            specValidacao.CursoOfertaAtivo =
            specValidacao.CursoOfertaLocalidadeAtivoDataInicioCicloLetivo =
            specValidacao.FormacaoEspecificaAtivoDataInicioCicloLetivo =
            specValidacao.TurnoAtivo = true;

            specValidacao.Seqs = null;

            foreach (var ofertaCampanha in ofertasCampanha)
            {
                try
                {
                    //De cada Curso oferta localidade turno, retornado na busca

                    /* O período letivo deverá ser retornado com base na regra RN_CAM_030 -
                     * Retorna período do evento letivo, informado como parâmetro: o ciclo letivo
                     * informado, o tipo de evento PERIODO_LETIVO, o curso-oferta-localidade-turno e o tipo de aluno “Calouro”.*/
                    var periodoLetivo = ConfiguracaoEventoLetivoDomainService.BuscarDatasEventoLetivo(filtro.SeqMenorCicloLetivoCampanha.Value,
                                                                           ofertaCampanha.SeqCursoOfertaLocalidadeTurno.Value,
                                                                           TipoAluno.Calouro,
                                                                           TOKEN_TIPO_EVENTO.PERIODO_LETIVO);
                    if (periodoLetivo != null)
                    {
                        //Atribuo a data início do período letivo
                        specValidacao.DataInicioPeriodoLetivo = periodoLetivo.DataInicio;

                        // Atribuo o Seq do CursoOfertaLocalidadeTurno atual.
                        specValidacao.Seq = ofertaCampanha.SeqCursoOfertaLocalidadeTurno;

                        var seqCursoOfertaLocalidadeTurno = CursoOfertaLocalidadeTurnoDomainService.Count(specValidacao);

                        // Se for válido irá retornar o Seq do CursoOfertaLocalidadeTurno
                        if (seqCursoOfertaLocalidadeTurno > 0) { campanhaOfertasFiltradas.Add(ofertaCampanha); }
                    }
                }
                catch (Exception ex)
                {
                    // Permitir que continue com as validações, caso não exista período;
                    //Obs.: Méto de busca do periodo retorna um throw exception, caso tenha que retornar null;
                    continue;
                }
            }

            return campanhaOfertasFiltradas;
        }


        /// <summary>
        /// Percorro as ofertas com o SeqCursoOfertaLocalidadeTurno filtrado para buscar os colaboradores
        /// com atividade de orientação que possuem vínculo no respectivo curso oferta localidade
        /// e o vínculo de colaborador está ativo.
        /// </summary>
        /// <param name="ofertasCampanhaOrientador"></param>
        /// <param name="cicloLetivo"></param>
        /// <param name="seqOrientador"></param>
        /// <returns></returns>
        private List<SelecaoOfertaCampanhaLookupListaVO> VincularOrientadoresOfertaCampanha(List<SelecaoOfertaCampanhaLookupListaVO> ofertasCampanhaOrientador, CampanhaCicloLetivo cicloLetivo, long? seqOrientador)
        {
            var ofertasCampanha = new List<SelecaoOfertaCampanhaLookupListaVO>();
            /// Percorro as ofertas com o SeqCursoOfertaLocalidadeTurno filtrado para buscar os colaboradores
            /// com atividade de orientação que possuem vínculo no respectivo curso oferta localidade
            /// e o vínculo de colaborador está ativo.
            foreach (var ofertaItem in ofertasCampanhaOrientador)
            {
                //Faço o filtro para cada Curso oferta localidade turno, busco os  colaboradores que possuem vínculo ativo e que estão
                //relacionados com os filtros da campanha oferta
                var orientadores = BuscarOrientadoresComVinculoAtivo(cicloLetivo, ofertaItem.SeqCursoOfertaLocalidadeTurno.Value, seqOrientador);

                // Faço a repetição para cada turno, crio a oferta e vinculo todos os colaboradores
                foreach (var orientador in orientadores)
                {
                    var orientadorOferta = ofertaItem.Transform<SelecaoOfertaCampanhaLookupListaVO>();

                    orientadorOferta.SeqOrientador = orientador.Seq;

                    orientadorOferta.Seq = $"{ofertaItem.SeqCursoOfertaLocalidadeTurno}{SEPARADOR_SEQS}{orientador.Seq}";

                    //Nome
                    orientadorOferta.Orientador = orientador.Descricao;
                    ofertasCampanha.Add(orientadorOferta);
                }
            }

            return ofertasCampanha.Where(o => o.Orientador != string.Empty).ToList();
        }

        private List<SelecaoOfertaCampanhaLookupListaVO> TratarRegistrosVazios(List<SelecaoOfertaCampanhaLookupListaVO> ofertasCampanha, string tipoOfertaToken)
        {
            /*Tratamento de registros em branco*/
            if (tipoOfertaToken != TOKEN_TIPO_OFERTA.TURMA)
            {
                ofertasCampanha = ofertasCampanha?.Where(s => !string.IsNullOrEmpty(s.CursoOferta)
                                                         && !string.IsNullOrEmpty(s.Localidade)
                                                         && (!string.IsNullOrEmpty(s.Turno)
                                                            || (s.Turnos != null && s.Turnos.Any()))
                ).ToList();
            }
            else
            {
                ofertasCampanha = ofertasCampanha?.Where(s => s.SeqTurma.HasValue).ToList();
            }

            return ofertasCampanha;
        }

        /// <summary>
        /// Busco o(s) colaborador(es) do tipo Orientador, que possuem vínculo ativo no CursoOfertaLocalidadeTurno
        /// </summary>
        /// <param name="cicloLetivo"></param>
        /// <param name="seqCursoOfertaLocalidadeTurno"></param>
        /// <param name="seqOrientador"></param>
        /// <returns></returns>
        private List<SMCDatasourceItem> BuscarOrientadoresComVinculoAtivo(CampanhaCicloLetivo cicloLetivo, long seqCursoOfertaLocalidadeTurno, long? seqOrientador = null)
        {
            try
            {
                //De cada Curso oferta localidade turno, retornado na busca
                DatasEventoLetivoVO periodoLetivo = new DatasEventoLetivoVO();

                /* O período letivo deverá ser retornado com base na regra RN_CAM_030 -
                 * Retorna período do evento letivo, informado como parâmetro: o ciclo letivo
                 * informado, o tipo de evento PERIODO_LETIVO, o curso-oferta-localidade-turno e o tipo de aluno “Calouro”.*/
                periodoLetivo = ConfiguracaoEventoLetivoDomainService.BuscarDatasEventoLetivo(cicloLetivo.SeqCicloLetivo,
                                                                       seqCursoOfertaLocalidadeTurno,
                                                                       TipoAluno.Calouro,
                                                                       TOKEN_TIPO_EVENTO.PERIODO_LETIVO);

                var cursoOfertaLocalidadeTurnoValido = ValidarCursoOfertaLocalidadeTurno(seqCursoOfertaLocalidadeTurno, periodoLetivo);

                if (cursoOfertaLocalidadeTurnoValido != null)
                {
                    /*retornar os colaboradores com atividade de orientação que possuem
                     * vínculo no respectivo curso oferta localidade e o vínculo de colaborador está ativo.
                     * Caso exista mais de um turno por curso oferta localide, os orientadores deverão se
                     * repetir em cada um dos turnos.*/

                    ///Specification do colaborador
                    var colaboradorFiltroVO = new ColaboradorFiltroVO()
                    {
                        Seq = seqOrientador,
                        TipoAtividade = TipoAtividadeColaborador.Orientacao,
                        DataInicio = periodoLetivo.DataInicio,
                        DataFim = periodoLetivo.DataFim,
                        SeqCursoOfertaLocalidade = cursoOfertaLocalidadeTurnoValido.SeqCursoOfertaLocalidade
                    };
                    var specColoaborador = new ColaboradorFilterSpecification()
                    {
                        SeqsColaboradorVinculo = ColaboradorVinculoDomainService.FiltroVinculoColaboradores(ref colaboradorFiltroVO)
                    };

                    //Faço o filtro para cada Curso oferta localidade turno, busco os  colaboradores que possuem vínculo ativo e que estão
                    //relacionados com os filtros da campanha oferta
                    var orientadores = ColaboradorDomainService.SearchProjectionBySpecification(specColoaborador,
                                                 x => new SMCDatasourceItem()
                                                 {
                                                     Seq = x.Seq,
                                                     Descricao = x.DadosPessoais.Nome
                                                 }
                                             ).ToList();

                    return orientadores;
                }

                return new List<SMCDatasourceItem>();
            }
            catch (System.Exception ex)
            {
                //Erro de retorno null
                return new List<SMCDatasourceItem>();
            }
        }

        private CursoOfertaLocalidadeTurnoVO ValidarCursoOfertaLocalidadeTurno(long? seqCursoOfertaLocalidadeTurno, DatasEventoLetivoVO periodoLetivo)
        {
            var specValidaCursoOLT = new CursoOfertaLocalidadeTurnoFilterSpecification()
            {
                /// Complementos das validações LK_CAM_003 - Seleção de Oferta para Campanha - NV06
                Seq = seqCursoOfertaLocalidadeTurno,
                DataInicioPeriodoLetivo = periodoLetivo.DataInicio,
                CursoAtivoDataInicioCicloLetivo = true,
                CursoUnidadeAtivoDataInicioCicloLetivo = true,
                CursoOfertaAtivo = true,
                CursoOfertaLocalidadeAtivoDataInicioCicloLetivo = true,
                FormacaoEspecificaAtivoDataInicioCicloLetivo = true,
                TurnoAtivo = true
            };

            var cursoOfertaLocalidadeTurno = CursoOfertaLocalidadeTurnoDomainService.SearchByKey(specValidaCursoOLT);

            return cursoOfertaLocalidadeTurno.Transform<CursoOfertaLocalidadeTurnoVO>();
        }

        /// <summary>
        /// Retorna o ciclo letivo mais antigo (Menor ciclo letivo)
        /// </summary>
        /// <param name="ciclosLetivos"></param>
        /// <returns></returns>
        private CampanhaCicloLetivo BuscarCicloLetivoMaisAntigo(IList<CampanhaCicloLetivo> ciclosLetivos)
        {
            //Retorna o ciclo letivo mais antigo (Menor ciclo letivo)
            return ciclosLetivos.OrderBy(c => c.CicloLetivo.Ano).ThenBy(o => o.CicloLetivo.Numero).FirstOrDefault();
        }

        /// <summary>
        /// No resultado de pesquisa, retornar os dados ordenados alfabeticamente por
        /// curso oferta, localidade, turno, área de concentração, linha de pesquisa, eixo temático, orientador e turma.
        /// </summary>
        private List<SelecaoOfertaCampanhaLookupListaVO> OrdenarCampanhaOfertaLookup(List<SelecaoOfertaCampanhaLookupListaVO> ofertasCampanha)
        {
            return ofertasCampanha
                    //curso oferta
                    ?.OrderBy(o => o.CursoOferta)
                    //localidade
                    ?.ThenBy(o => o.Localidade)
                    //turno
                    ?.ThenBy(o => o.Turno)
                    //área de concentração
                    ?.ThenBy(o => o.AreaConcentracao)
                    //linha de pesquisa
                    ?.ThenBy(o => o.LinhaPesquisa)
                    //eixo temático
                    ?.ThenBy(o => o.EixoTematico)
                    //orientador
                    ?.ThenBy(o => o.Orientador)
                    //turma.
                    ?.ThenBy(o => o.Turma)
                ?.ToList();
        }

        /// <summary>
        /// Formatar a descrição da oferta da campanha, conforme a regra RN_CAM_072
        /// RN_CAM_072 Descrição da oferta da campanha
        /// </summary>
        /// <param name="spec"></param>
        /// <param name="ofertaCampanha"></param>
        /// <param name="tipoOferta"></param>
        /// <returns></returns>
        public string FormatarDescricaoOfertaCampanha(SelecaoOfertaCampanhaLookupListaVO ofertaCampanha, TipoOferta tipoOferta)
        {
            string descricaoOfertaFormatado = string.Empty;

            string nomeCurso = string.IsNullOrEmpty(ofertaCampanha.CursoOferta) ? "" : ofertaCampanha.CursoOferta.Trim();
            string descricaoLocalidade = string.IsNullOrEmpty(ofertaCampanha.Localidade) ? "" : $" - {ofertaCampanha.Localidade.Trim()}";
            string descricaoTurno = string.IsNullOrEmpty(ofertaCampanha.Turno) ? "" : $" - Turno {ofertaCampanha.Turno.Trim()}";
            string descricaoAreaConcentracao = string.IsNullOrEmpty(ofertaCampanha.AreaConcentracao) ? "" : $" - {ofertaCampanha.AreaConcentracao.Trim()}";
            string descricaoLinhaPesquisa = string.IsNullOrEmpty(ofertaCampanha.LinhaPesquisa) ? "" : $" - {ofertaCampanha.LinhaPesquisa.Trim()}";
            string descricaoEixoTematico = string.IsNullOrEmpty(ofertaCampanha.EixoTematico) ? "" : $" - {ofertaCampanha.EixoTematico.Trim()}";
            string descricaoAreaTematica = string.IsNullOrEmpty(ofertaCampanha.AreaTematica) ? "" : $" - {ofertaCampanha.AreaTematica.Trim()}";
            string nomeOrientador = string.IsNullOrEmpty(ofertaCampanha.Orientador) ? "" : $" - {ofertaCampanha.Orientador}";

            /*1. Se o tipo de oferta exigir curso-oferta-localidade-turno, montar a descrição da oferta no
              formato abaixo, com base no curso-oferta-localidade-turno, formação específica e orientador: */
            if (tipoOferta.ExigeCursoOfertaLocalidadeTurno)
            {
                //<nome do curso> + “ – “ + <nome da localidade> + “ - Turno “ + <descrição do turno> +
                descricaoOfertaFormatado = $"{nomeCurso}{descricaoLocalidade}{descricaoTurno}";

                switch (tipoOferta.Token)
                {
                    //Se o tipo de oferta for AREA_CONCENTRACAO: " - " + <nome da área de concentração>
                    case TOKEN_TIPO_OFERTA.AREA_CONCENTRACAO:
                        descricaoOfertaFormatado += $"{descricaoAreaConcentracao}";
                        break;

                    //Se o tipo de oferta for LINHA_PESQUISA: " - " + <descricao da área de concentração> + " - " + <nome da linha de pesquisa>
                    case TOKEN_TIPO_OFERTA.LINHA_PESQUISA:
                        descricaoOfertaFormatado += $"{descricaoAreaConcentracao}{descricaoLinhaPesquisa}";
                        break;

                    //Se o tipo de oferta for EIXO_TEMATICO: " - " + <nome da área de concentração>
                    //+ " - " + <nome da linha de pesquisa> + " - " + <nome do eixo temático>
                    case TOKEN_TIPO_OFERTA.EIXO_TEMATICO:
                        descricaoOfertaFormatado += $"{descricaoAreaConcentracao}{descricaoLinhaPesquisa}{descricaoEixoTematico}";
                        break;

                    //Se o tipo de oferta for AREA_TEMATICA: " - " + <nome da área temática>
                    case TOKEN_TIPO_OFERTA.AREA_TEMATICA:
                        descricaoOfertaFormatado += $"{descricaoAreaTematica}";
                        break;

                    //Se o tipo de oferta for ORIENTADOR: " - " + <nome do orientador>
                    case TOKEN_TIPO_OFERTA.ORIENTADOR:
                        descricaoOfertaFormatado += $"{nomeOrientador}";
                        break;
                }
            }
            // 2. Se o tipo de oferta for TURMA, a descrição da oferta deverá ser conforme a regra
            // RN_TUR_025 - Exibição Descrição Turma.
            else if (tipoOferta.Token == TOKEN_TIPO_OFERTA.TURMA)
            {
                ofertaCampanha.Turma = descricaoOfertaFormatado = TurmaDomainService.BuscarDescricaoTurmaConcatenado(ofertaCampanha.SeqTurma.Value);
            }

            return ofertaCampanha.Descricao = descricaoOfertaFormatado.SMCToPascalCaseName();
        }

        #endregion Lookup Ofertas

        #region [Associar campanha ofertas]

        /// <summary>
        /// Associa as ofertas, passadas como parâmetro, a uma campanha
        /// </summary>
        /// <param name="campanhaOfertaVO"></param>
        /// <returns>Seq da campanha</returns>
        public long AssociarCampanhaOferta(CampanhaOfertaAssociacaoVO campanhaOfertaVO)
        {
            ///Validações das RN_CAM_063 Consistência da seleção de oferta
            /// e RN_CAM_016 Consistência Associação de Oferta da Campanha
            ValidarAssociarCampanhaOferta(campanhaOfertaVO);

            using (var unitOfWork = SMCUnitOfWork.Begin())
            {
                foreach (var ofertaCampanha in campanhaOfertaVO.OfertasCampanha)
                {
                    var tipoOferta = TipoOfertaDomainService.SearchByKey(new TipoOfertaFilterSpecification() { Seq = ofertaCampanha.SeqTipoOferta });

                    /* RN_CAM_015 Associação de Oferta da Campanha
                     2. Ao associar uma oferta à campanha, criar o registro de oferta da campanha, gravando o tipo de oferta
                       selecionado, a descrição da oferta conforme regra RN_CAM_072 - Descrição da oferta da campanha e a quantidade de vagas "0".*/
                    //RN_CAM_072 Descrição da oferta da campanha
                    FormatarDescricaoOfertaCampanha(ofertaCampanha.Transform<SelecaoOfertaCampanhaLookupListaVO>(), tipoOferta);

                    CampanhaOferta entityOferta = CriarRegistroOfertaCampanha(campanhaOfertaVO.SeqCampanha, ofertaCampanha);

                    // 3. Criar o respectivo item de oferta, com os seguintes dados:
                    SalvarCampanhaOfertaItem(tipoOferta, entityOferta.Seq, ofertaCampanha);

                    /* 4.1. Incluir a(s) nova(s) oferta(s) no(s) processo(s) seletivo(s) e na(s) convocação(ões)
                     selecionado(s) pelo usuário. Caso ele tenha selecionado algum.*/
                    ///Inclusão da oferta para os Processos Seletivos
                    InclusaoOfertasProcessoSeletivoConvocacao(campanhaOfertaVO, entityOferta.Seq);
                }
                unitOfWork.Commit();
            }
            return campanhaOfertaVO.SeqCampanha;
        }

        /// <summary>
        /// Criar o registro de oferta da campanha, gravando o tipo de oferta selecionado
        /// </summary>
        /// <param name="ofertaCampanha"></param>
        /// <returns></returns>
        public CampanhaOferta CriarRegistroOfertaCampanha(long seqCampanha, CampanhaOfertaVO ofertaCampanha)
        {
            CampanhaOferta entityOferta = ofertaCampanha.Transform<CampanhaOferta>();
            entityOferta.Seq = 0;
            entityOferta.SeqCampanha = seqCampanha;
            entityOferta.QuantidadeVagas = 0;

            SaveEntity(entityOferta);

            return entityOferta;
        }

        /// <summary>
        /// 4.1. Incluir a(s) nova(s) oferta(s) no(s) processo(s) seletivo(s) e na(s) convocação(ões)
        /// selecionado(s) pelo usuário.Caso ele tenha selecionado algum.
        /// </summary>
        /// <param name="campanhaOfertaVO"></param>
        /// <param name="campanhaOfertaSeq"></param>
        private void InclusaoOfertasProcessoSeletivoConvocacao(CampanhaOfertaAssociacaoVO campanhaOfertaVO, long campanhaOfertaSeq)
        {
            if (campanhaOfertaVO == null || !campanhaOfertaVO.SeqsProcessosSeletivos.SMCAny()) { return; }

            ///Inclusão da oferta para os Processos Seletivos
            foreach (var seqProcessoSeletivo in campanhaOfertaVO.SeqsProcessosSeletivos)
            {
                ProcessoSeletivoOfertaVO ofertaProcessoSeletivo = new ProcessoSeletivoOfertaVO()
                {
                    SeqCampanhaOferta = campanhaOfertaSeq,
                    SeqProcessoSeletivo = seqProcessoSeletivo,
                    QuantidadeVagas = 0,
                    QuantidadeVagasOcupadas = 0,
                    Ofertas = new List<long>() { campanhaOfertaSeq }
                };

                ProcessoSeletivoOfertaDomainService.SalvarOfertaProcessoSeletivo(ofertaProcessoSeletivo);

                var specProcessoSeletivo = new ProcessoSeletivoFilterSpecification() { Seq = seqProcessoSeletivo };

                ///Busco todos os seqs das convocações do Processo Seletivo
                var seqsConvocacoesExistentes = ProcessoSeletivoDomainService.SearchProjectionByKey(specProcessoSeletivo
                                                , x => x.Convocacoes.Select(c => c.Seq)).ToList();

                // Se não houver convocação selecionada ou para o processo seletivo, passo para o próximo Processo Seletivo
                if (!seqsConvocacoesExistentes.SMCAny() || !campanhaOfertaVO.SeqsConvocacoes.SMCAny()) { continue; }

                ///Busco apenas os seqs das convocações que fazem parte do processo seletivo atual,
                ///das que foram selecionadas pelo usuário.
                var seqsConvocacaoSelecionada = seqsConvocacoesExistentes.Where(c => campanhaOfertaVO.SeqsConvocacoes.Contains(c));

                // Se não houver convocação selecionada, para este processo seletivo, passo para o próximo Processo Seletivo
                if (!seqsConvocacaoSelecionada.SMCAny()) { continue; }

                ///Inclusão da oferta para as convocações
                ///Convocações que pertencem ao processo seletivo em questão
                foreach (var seqConvocacao in seqsConvocacaoSelecionada)
                {
                    ConvocacaoOferta ofertaConvocacao = new ConvocacaoOferta()
                    {
                        SeqConvocacao = seqConvocacao,
                        SeqProcessoSeletivoOferta = ofertaProcessoSeletivo.Seq,
                        QuantidadeVagas = 0
                    };

                    ConvocacaoOfertaDomainService.SaveEntity(ofertaConvocacao);
                }
            }
        }

        /// <summary>
        /// 3. Criar o item de oferta
        /// </summary>
        /// <param name="spec"></param>
        /// <param name="tipoOferta"></param>
        /// <param name="entityOferta"></param>
        /// <param name="ofertaCampanha"></param>
        private void SalvarCampanhaOfertaItem(TipoOferta tipoOferta, long seqNovaCampanhaOferta, CampanhaOfertaVO ofertaCampanha)
        {
            // 3. Criar o respectivo item de oferta, com os seguintes dados:
            CampanhaOfertaItem item = new CampanhaOfertaItem();

            //- Oferta da campanha: referência da oferta da campanha associada.
            item.SeqCampanhaOferta = seqNovaCampanhaOferta;

            /*- Oferta de curso por localidade e turno: se o tipo de oferta da oferta associada
              exigir curso oferta localidade turno, retornar o curso oferta localidade
              turno da oferta selecionada, caso contrário, retornar nulo.*/
            if (tipoOferta.ExigeCursoOfertaLocalidadeTurno)
            {
                item.SeqCursoOfertaLocalidadeTurno = ofertaCampanha.SeqCursoOfertaLocalidadeTurno;
            }

            //- Orientador (colaborador): se o tipo de oferta for ORIENTADOR, retornar o orientador selecionado,
            //  caso contrário, retornar nulo.
            if (tipoOferta.Token == TOKEN_TIPO_OFERTA.ORIENTADOR) { item.SeqColaborador = ofertaCampanha.SeqOrientador; }

            //- Turma: se o tipo de oferta for TURMA, retornar a turma selecionada, caso contrário, retornar nulo.
            if (tipoOferta.Token == TOKEN_TIPO_OFERTA.TURMA) { item.SeqTurma = ofertaCampanha.SeqTurma; }

            /*- Formação específica: se o tipo de oferta for AREA_CONCETRACAO, LINHA_PESQUISA ou EIXO_TEMATICO,
              retornar a formação específica selecionada; caso contrário, se a oferta de curso do curso oferta
              localidade turno da oferta tiver uma formação especifica associada, retornar essa formação; caso contrário, retornar nulo.*/
            switch (tipoOferta.Token)
            {
                case TOKEN_TIPO_OFERTA.AREA_CONCENTRACAO:
                case TOKEN_TIPO_OFERTA.LINHA_PESQUISA:
                case TOKEN_TIPO_OFERTA.EIXO_TEMATICO:

                    if (ofertaCampanha.SeqFormacaoEspecifica.HasValue)
                    {
                        item.SeqFormacaoEspecifica = ofertaCampanha.SeqFormacaoEspecifica;
                    }
                    else
                    {
                        var spec = new CursoOfertaLocalidadeTurnoFilterSpecification() { Seq = item.SeqCursoOfertaLocalidadeTurno };

                        item.SeqFormacaoEspecifica = CursoOfertaLocalidadeTurnoDomainService.SearchProjectionByKey(spec,
                                                        x => x.CursoOfertaLocalidade.CursoOferta.SeqFormacaoEspecifica);
                    }
                    break;
            }
            CampanhaOfertaItemDomainService.SaveEntity(item);
        }

        /// <summary>
        /// Validações das RN_CAM_063 Consistência da seleção de oferta
        /// e RN_CAM_016 Consistência Associação de Oferta da Campanha
        /// </summary>
        /// <param name="campanhaOfertaVO"></param>
        /// <returns></returns>
        private void ValidarAssociarCampanhaOferta(CampanhaOfertaAssociacaoVO campanhaOfertaVO)
        {
            /*RN_CAM_063 Consistência da seleção de oferta
                Parâmetro: - Ciclo letivo. - Lista de ofertas*/
            foreach (var ofertaCampanha in campanhaOfertaVO.OfertasCampanha)
            {
                string descricaoCampanha = string.Empty;
                var tipoOferta = TipoOfertaDomainService.SearchByKey(ofertaCampanha.SeqTipoOferta.Value);

                ofertaCampanha.SeqCampanha = campanhaOfertaVO.SeqCampanha;

                /// N_CAM_063 Consistência da seleção de oferta
                ValidarConsistenciaOfertaCampanha(ofertaCampanha, tipoOferta);

                /// RN_CAM_016 Consistência Associação de Oferta da Campanha
                ValidarConsistenciaAssociacaoOferta(ofertaCampanha, campanhaOfertaVO.OfertasCampanha);
            }
        }

        /// <summary>
        ///  RN_CAM_016 Consistência Associação de Oferta da Campanha
        /// </summary>
        /// <param name="spec"></param>
        /// <param name="ofertasCampanha"></param>
        /// <param name="tipoOfertaToken"></param>
        /// <param name="descricaoCampanhaOferta"></param>
        private void ValidarConsistenciaAssociacaoOferta(CampanhaOfertaVO ofertaCampanha, List<CampanhaOfertaVO> ofertasCampanha)
        {
            /*Ao associar uma oferta, verificar se já existe uma oferta cadastrada, com um campanha-oferta-item, com o
             * mesmo curso-oferta-localidade-turno, a mesma turma, o mesmo colaborador e a mesma formação específica.
             * Considerar que os campos podem estar nulos. Caso exista, abortar a operação e emitir a mensagem de erro:*/

            //Valido se existe duplicado na lista da tela do usuário
            var countCampanhaOferta = ofertasCampanha.Count(o => o.SeqCursoOfertaLocalidadeTurno == ofertaCampanha.SeqCursoOfertaLocalidadeTurno
                                                                  && o.SeqTurma == ofertaCampanha.SeqTurma
                                                                  && o.SeqOrientador == ofertaCampanha.SeqOrientador
                                                                  && o.SeqFormacaoEspecifica == ofertaCampanha.SeqFormacaoEspecifica);

            var specValidation = new CampanhaOfertaFilterSpecification()
            {
                SeqCampanha = ofertaCampanha.SeqCampanha,
                SeqCursoOfertaLocalidadeTurno = ofertaCampanha.SeqCursoOfertaLocalidadeTurno,
                SeqTurma = ofertaCampanha.SeqTurma,
                SeqColaborador = ofertaCampanha.SeqOrientador,
                SeqFormacaoEspecifica = ofertaCampanha.SeqFormacaoEspecifica
            };

            //Valido se existe duplicado na base de dados
            var campanhaOfertaDuplicada = SearchBySpecification(specValidation).FirstOrDefault();

            if (countCampanhaOferta > 1 || campanhaOfertaDuplicada != null)
            {
                throw new CampanhaOfertaAssociacaoDuplicadaExtintoException(ofertaCampanha.Descricao);
            }

            /*Se o tipo de oferta da oferta associada for TURMA, caso a turma selecionada tenha divisão de
            componente cujo tipo gere orientação, abortar a operação e emitir a mensage de erro:
            "Associação não permitida. A turma {0} gera orientação. Turmas que geram orientação não podem ser ofertadas como disciplina isolada." */

            //if (ofertaCampanha.TipoOfertaToken == TOKEN_TIPO_OFERTA.TURMA)
            //{
            //    var specTurma = new TurmaFilterSpecification() { Seq = ofertaCampanha.SeqTurma, TurmaComOrientacao = true };
            //    var turma = TurmaDomainService.SearchByKey(specTurma);

            //    if (turma != null)
            //    {
            //        throw new CampanhaOfertaAssociacaoTurmaGeraOrientacaoException(ofertaCampanha.Descricao);
            //    }
            //}
        }

        /// <summary>
        /// RN_CAM_063 Consistência da seleção de oferta
        /// </summary>
        /// <param name="CampanhaOfertaVO"></param>
        /// <param name="tipoOferta"></param>
        public void ValidarConsistenciaOfertaCampanha(CampanhaOfertaVO campanhaOferta, TipoOferta tipoOferta)
        {
            /*Regras:
            1. Para lista de ofertas informada por parâmetro:
               1.1. Se o tipo de oferta exigir curso-oferta-localidade-turno:*/

            if (tipoOferta.ExigeCursoOfertaLocalidadeTurno && campanhaOferta.SeqCursoOfertaLocalidadeTurno.HasValue)
            {
                var include = IncludesCampanha.CiclosLetivos_CicloLetivo;
                var campanha = CampanhaDomainService.SearchByKey(new CampanhaFilterSpecification() { Seq = campanhaOferta.SeqCampanha }, include);
                var cicloLetivo = BuscarCicloLetivoMaisAntigo(campanha.CiclosLetivos);

                /* O período letivo deverá ser retornado com base na regra RN_CAM_030 -
                     * Retorna período do evento letivo, informado como parâmetro: o ciclo letivo
                     * informado, o tipo de evento PERIODO_LETIVO, o curso-oferta-localidade-turno e o tipo de aluno “Calouro”.*/
                var periodoLetivo = ConfiguracaoEventoLetivoDomainService.BuscarDatasEventoLetivo(cicloLetivo.SeqCicloLetivo,
                                                                       campanhaOferta.SeqCursoOfertaLocalidadeTurno.GetValueOrDefault(),
                                                                       TipoAluno.Calouro,
                                                                       TOKEN_TIPO_EVENTO.PERIODO_LETIVO);

                /*1.1.1. */
                /*Verificar se o curso do curso-oferta-localidade-turno da oferta está com a categoria da situação "Em desativação" ou
                 * * "Inativa" na data início do período letivo do ciclo letivo informado por parâmetro*. Em caso afirmativo, abortar a
                 * operação e emitir a mensagem de erro abaixo:
                 * “A oferta <descrição da oferta> não pode ser selecionada. Seu curso estará em extinção ou extinto no início do período letivo.”*/

                var curso = CursoOfertaLocalidadeTurnoDomainService.Count(new CursoOfertaLocalidadeTurnoFilterSpecification()
                {
                    Seq = campanhaOferta.SeqCursoOfertaLocalidadeTurno,
                    DataInicioPeriodoLetivo = periodoLetivo.DataInicio,
                    CursoAtivoDataInicioCicloLetivo = true
                });

                if (curso == 0) { throw new CampanhaOfertaAssociacaoCursoExtintoException(campanhaOferta.Descricao); }

                /*1.1.2. Verificar se o curso-unidade do curso-oferta-localidade-turno está
                com a categoria da situação "Em desativação" ou "Inativa" na data início do
                período letivo do ciclo letivo informado por parâmetro*. Em caso afirmativo,
                abortar a operação e emitir a mensagem de erro abaixo:
                “A oferta {0} não pode ser selecionada. Seu curso x unidade estará em extinção ou extinto no início do período letivo.”*/
                var cursoUnidade = CursoOfertaLocalidadeTurnoDomainService.Count(new CursoOfertaLocalidadeTurnoFilterSpecification()
                {
                    Seq = campanhaOferta.SeqCursoOfertaLocalidadeTurno,
                    DataInicioPeriodoLetivo = periodoLetivo.DataInicio,
                    CursoUnidadeAtivoDataInicioCicloLetivo = true
                });

                if (cursoUnidade == 0) { throw new CampanhaOfertaAssociacaoCursoUnidadeExtintoException(campanhaOferta.Descricao); }

                /*1.1.3. Verificar se o curso-oferta do curso-oferta-localidade-turno está desativado.
                Em caso afirmativo, abortar a operação e emitir a mensagem de erro abaixo:
                “A oferta {0} não pode ser selecionada. Sua oferta de curso está desativada.”*/
                var cursoOferta = CursoOfertaLocalidadeTurnoDomainService.Count(new CursoOfertaLocalidadeTurnoFilterSpecification()
                {
                    Seq = campanhaOferta.SeqCursoOfertaLocalidadeTurno,
                    DataInicioPeriodoLetivo = periodoLetivo.DataInicio,
                    CursoOfertaAtivo = true
                });

                if (cursoOferta == 0) { throw new CampanhaOfertaAssociacaoOfertaCursoDesativadoException(campanhaOferta.Descricao); }

                /*1.1.4. Verificar se o curso-oferta-localidade do curso-oferta-localidade-turno está
                com a categoria da situação “Em desativação” ou “Inativa” na data início do
                período letivo do ciclo letivo informado por parâmetro*. Em caso afirmativo,
                abortar a operação e emitir a mensagem de erro abaixo:
                “A oferta {0} não pode ser selecionada. Sua oferta de curso por localidade estará em extinção ou extinta no início do período letivo.”*/
                var cursoOfertaLocalidade = CursoOfertaLocalidadeTurnoDomainService.Count(new CursoOfertaLocalidadeTurnoFilterSpecification()
                {
                    Seq = campanhaOferta.SeqCursoOfertaLocalidadeTurno,
                    DataInicioPeriodoLetivo = periodoLetivo.DataInicio,
                    CursoOfertaLocalidadeAtivoDataInicioCicloLetivo = true
                });

                if (cursoOfertaLocalidade == 0) { throw new CampanhaOfertaAssociacaoOfertaCursoLocalidadeExtintoException(campanhaOferta.Descricao); }

                /*1.1.5. Verificar se a formação específica está desativada ou se a formação específica
                por curso não está mais vigente na data início do período letivo do ciclo letivo informado
                por parâmetro*. Em caso afirmativo, abortar a operação e emitir a mensagem de erro abaixo:
                “A oferta {0} não pode ser selecionada. Sua formação específica está desativada ou não estará mais vigente na data início do período letivo.”*/
                //Flag que Valido a vigência de cada curso da formação específica com o período do ciclo letivo
                if (campanhaOferta.SeqFormacaoEspecifica.HasValue)
                {
                    var formacaoEspecifica = FormacaoEspecificaDomainService.SearchProjectionByKey(campanhaOferta.SeqFormacaoEspecifica.Value, f => new
                    {
                        f.Ativo,
                        Curso = f.Cursos.Select(c => new { c.DataInicioVigencia, c.DataFimVigencia }).FirstOrDefault()
                    });

                    if (formacaoEspecifica != null)
                    {
                        if (!formacaoEspecifica.Ativo.HasValue || !formacaoEspecifica.Ativo.Value)
                        {
                            throw new CampanhaOfertaAssociacaoFormacaoEspecificaDesativadaException(campanhaOferta.Descricao);
                        }

                        if (formacaoEspecifica.Curso != null)
                        {
                            if (periodoLetivo.DataInicio < formacaoEspecifica.Curso.DataInicioVigencia ||
                             (formacaoEspecifica.Curso.DataFimVigencia.HasValue && periodoLetivo.DataInicio > formacaoEspecifica.Curso.DataFimVigencia.Value))

                            {
                                throw new CampanhaOfertaAssociacaoFormacaoEspecificaDesativadaException(campanhaOferta.Descricao);
                            }
                        }
                    }
                }

                /*1.1.6. Verificar se o turno do curso-oferta-localidade-turno está desativado
                para o curso-oferta-localidade do curso-oferta-localidade-turno. Em caso afirmativo,
                abortar a operação e emitir a mensagem de erro abaixo:
                “A oferta {0} não pode ser selecionada. Seu turno está desativado para a oferta de curso por localidade.”*/
                var turno = CursoOfertaLocalidadeTurnoDomainService.Count(new CursoOfertaLocalidadeTurnoFilterSpecification()
                {
                    Seq = campanhaOferta.SeqCursoOfertaLocalidadeTurno,
                    DataInicioPeriodoLetivo = periodoLetivo.DataInicio,
                    TurnoAtivo = true,
                });

                if (turno == 0) { throw new CampanhaOfertaAssociacaoTurnoDesativadoException(campanhaOferta.Descricao); }
            }

            /*1.2. Se o tipo de oferta não exigir curso-oferta-localidade-turno e for do tipo TURMA:*/
            else if (tipoOferta.Token == TOKEN_TIPO_OFERTA.TURMA)
            {
                /*1.2.1. Verificar se a turma está cancelada. Em caso afirmativo, abortar
                a operação e emitir a mensagem de erro abaixo:
                “A oferta <descrição da oferta> não pode ser selecionada. A turma está cancelada.”*/
                var turmaHistoricoSituacao = TurmaDomainService.BuscarTurmaSituacaoAtual(campanhaOferta.SeqTurma.Value);
                var situacaoTurma = turmaHistoricoSituacao != null ? turmaHistoricoSituacao.SituacaoTurma : SituacaoTurma.Nenhum;
                if (situacaoTurma == SituacaoTurma.Cancelada)
                {
                    throw new CampanhaOfertaAssociacaoTurmaCanceladaException(campanhaOferta.Descricao);
                }
            }
        }

        /// <summary>
        /// RN_CAM_014 - Exclusão de Oferta da Campanha
        /// Antes de excluir ofertas da campanha, validar se não possuem associação a algum processo seletivo
        /// </summary>
        /// <param name="seq"></param>o
        /// <returns>True para exclusão; False caso possue restrição</returns>
        public bool ValidarExclusao(long seq)
        {
            var spec = new CampanhaOfertaFilterSpecification() { Seq = seq };

            var processoSeletivo = SearchProjectionByKey(spec, x => x.Campanha.ProcessosSeletivos.SelectMany(p => p.Ofertas)).ToList();

            var valido = !processoSeletivo.SMCAny() || !processoSeletivo.Any(p => p.SeqCampanhaOferta == seq);

            return valido;
        }

        /// <summary>
        /// RN_CAM_014 - Exclusão de Oferta da Campanha
        /// 1. Ao excluir ofertas da campanha, se a elas estiverem associadas a algum processo seletivo
        /// 1.1. Em caso afirmativo, excluir as ofertas das convocações e em seguida dos processos seletivos em
        /// que estiverem associadas.
        /// 1.2. Em caso negativo, abortar a operação.
        /// 2. Excluir o item de oferta e em seguida a oferta da campanha.
        /// </summary>
        /// <param name="seq"></param>
        public long Excluir(long seq)
        {
            long seqCampanha = 0;
            // Início da transação
            using (var unitOfWork = SMCUnitOfWork.Begin())
            {
                var spec = new CampanhaOfertaFilterSpecification() { Seq = seq };

                var ofertasProcessoSeletivo = SearchProjectionByKey(spec, x => x.Campanha.ProcessosSeletivos.SelectMany(p => p.Ofertas)).ToList();

                var ofertasConvocacao = SearchProjectionByKey(spec, x => x.Campanha.ProcessosSeletivos.SelectMany(p => p.Convocacoes.SelectMany(o => o.Ofertas))).ToList();

                var OfertasProcessoSeletivoVinculadas = ofertasProcessoSeletivo.Where(p => p.SeqCampanhaOferta == seq);

                if (ofertasConvocacao.SMCAny() && OfertasProcessoSeletivoVinculadas.SMCAny())
                {
                    var OfertasConvocacaoVinculadas = ofertasConvocacao.Where(p => OfertasProcessoSeletivoVinculadas.Any(a => a.Seq == p.SeqProcessoSeletivoOferta));

                    // Excluir ofertas da convocação
                    OfertasConvocacaoVinculadas.Where(x => x != null)?.SMCForEach(oferta => ConvocacaoOfertaDomainService.DeleteEntity(oferta));
                }

                if (OfertasProcessoSeletivoVinculadas.SMCAny())
                {
                    // Excluir ofertas do processo seletivo
                    OfertasProcessoSeletivoVinculadas.Where(x => x != null)?.SMCForEach(oferta => ProcessoSeletivoOfertaDomainService.DeleteEntity(oferta));
                }

                // 2. Excluir o item de oferta e em seguida a oferta da campanha.
                var itensOferta = SearchProjectionByKey(spec, x => x.Itens).ToList();

                if (itensOferta.SMCAny())
                {
                    //Excluir o item de oferta
                    itensOferta.Where(x => x != null)?.SMCForEach(item => CampanhaOfertaItemDomainService.DeleteEntity(item));
                }

                var OfertaCampanha = SearchByKey(spec);

                seqCampanha = OfertaCampanha.SeqCampanha;

                //Excluir oferta da campanha
                DeleteEntity(OfertaCampanha);

                unitOfWork.Commit();
            }
            return seqCampanha;
        }

        #endregion [Associar campanha ofertas]
    }
}