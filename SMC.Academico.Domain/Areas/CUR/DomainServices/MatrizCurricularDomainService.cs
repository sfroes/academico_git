using SMC.Academico.Common.Areas.CUR.Enums;
using SMC.Academico.Common.Areas.CUR.Exceptions;
using SMC.Academico.Common.Areas.CUR.Includes;
using SMC.Academico.Common.Constants;
using SMC.Academico.Domain.Areas.ALN.DomainServices;
using SMC.Academico.Domain.Areas.ALN.Models;
using SMC.Academico.Domain.Areas.CAM.DomainServices;
using SMC.Academico.Domain.Areas.CSO.DomainServices;
using SMC.Academico.Domain.Areas.CSO.Models;
using SMC.Academico.Domain.Areas.CSO.Specifications;
using SMC.Academico.Domain.Areas.CUR.Models;
using SMC.Academico.Domain.Areas.CUR.Specifications;
using SMC.Academico.Domain.Areas.CUR.ValueObjects;
using SMC.Framework;
using SMC.Framework.Domain;
using SMC.Framework.Extensions;
using SMC.Framework.Model;
using SMC.Framework.Specification;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SMC.Academico.Domain.Areas.CUR.DomainServices
{
    public class MatrizCurricularDomainService : AcademicoContextDomain<MatrizCurricular>
    {
        #region [ DomainService ]
        private CursoDomainService CursoDomainService => Create<CursoDomainService>();
        private CurriculoCursoOfertaDomainService CurriculoCursoOfertaDomainService => Create<CurriculoCursoOfertaDomainService>();

        private CursoOfertaLocalidadeDomainService CursoOfertaLocalidadeDomainService => Create<CursoOfertaLocalidadeDomainService>();

        private CursoOfertaLocalidadeTurnoDomainService CursoOfertaLocalidadeTurnoDomainService => Create<CursoOfertaLocalidadeTurnoDomainService>();

        private DispensaDomainService DispensaDomainService => Create<DispensaDomainService>();

        private DivisaoCurricularDomainService DivisaoCurricularDomainService => Create<DivisaoCurricularDomainService>();

        private DivisaoCurricularItemDomainService DivisaoCurricularItemDomainService => Create<DivisaoCurricularItemDomainService>();

        private InstituicaoNivelTipoComponenteCurricularDomainService InstituicaoNivelTipoComponenteCurricularDomainService => Create<InstituicaoNivelTipoComponenteCurricularDomainService>();

        private GrupoCurricularDomainService GrupoCurricularDomainService => Create<GrupoCurricularDomainService>();

        private MatrizCurricularOfertaDomainService MatrizCurricularOfertaDomainService => Create<MatrizCurricularOfertaDomainService>();

        private ModalidadeDomainService ModalidadeDomainService => Create<ModalidadeDomainService>();

        private TurnoDomainService TurnoDomainService => Create<TurnoDomainService>();

        private CurriculoCursoOfertaGrupoDomainService CurriculoCursoOfertaGrupoDomainService => Create<CurriculoCursoOfertaGrupoDomainService>();

        private AlunoDomainService AlunoDomainService => Create<AlunoDomainService>();

        private ConfiguracaoEventoLetivoDomainService ConfiguracaoEventoLetivoDomainService => Create<ConfiguracaoEventoLetivoDomainService>();

        private ComponenteCurricularDomainService ComponenteCurricularDomainService => Create<ComponenteCurricularDomainService>();

        #endregion [ DomainService ]

        /// <summary>
        /// Busca maior sequencial de acordo com o curriculo curso oferta
        /// </summary>
        /// <param name="seqCurriculoCursoOferta">Sequencial do curriculo curso oferta</param>
        /// <returns>Dados para inserir uma matriz curricular</returns>
        public MatrizCurricularVO BuscarConfiguracaoMatrizCurricular(long seqCurriculoCursoOferta)
        {
            //Validação de modalidades associadas para o curriculo curso oferta
            var modalidades = ModalidadeDomainService.BuscarModalidadesPorCurriculoCursoOfertaSelect(seqCurriculoCursoOferta);

            if (modalidades.Count == 0)
                throw new MatrizCurricularModalidadeNaoAssociadaException();

            //Recupera o sequencial do curriculo para verificar todos os sequencias do curso oferta
            var cursoOfertas = CurriculoCursoOfertaDomainService.SearchProjectionByKey(new SMCSeqSpecification<CurriculoCursoOferta>(seqCurriculoCursoOferta),
                s => new
                {
                    CodigoCurriculo = s.Curriculo.Codigo,
                    SeqOfertas = s.Curriculo.CursosOferta.Select(o => o.Seq).ToList(),
                    Descricao = s.CursoOferta.Descricao
                });

            var spec = new MatrizCurricularFilterSpecification() { SeqsCurriculoCursoOfertas = cursoOfertas.SeqOfertas };

            var registroSequencial = this.SearchProjectionBySpecification(spec, s => s.NumeroSequencial).ToList();
            int sequencial = registroSequencial.Count > 0 ? registroSequencial.Max() : 0;

            string codigoCurriculo = cursoOfertas.CodigoCurriculo;

            MatrizCurricularVO matrizCurricularVO = new MatrizCurricularVO();
            matrizCurricularVO.SeqCurriculoCursoOferta = seqCurriculoCursoOferta;
            matrizCurricularVO.NumeroSequencial = sequencial + 1;
            matrizCurricularVO.Codigo = $"{codigoCurriculo}.{matrizCurricularVO.NumeroSequencial.ToString().PadLeft(2, '0')}";
            matrizCurricularVO.Descricao = $"{matrizCurricularVO.Codigo} - {cursoOfertas.Descricao}";
            return matrizCurricularVO;
        }

        /// <summary>
        /// Busca a descrição da matriz curricular de acordo com os valores selecionado
        /// </summary>
        /// <param name="seqCurriculoCursoOferta">Sequencial do curriculo curso oferta</param>
        /// <param name="codigo">codigo da matriz curricular</param>
        /// <param name="seqDivisaoCurricular">Sequencial da divisao curricular selecionada</param>
        /// <param name="seqModalidade">Sequencial da modalidade selecionada</param>
        /// <returns>Dados para inserir uma matriz curricular</returns>
        public string BuscarConfiguracaoDescricaoMatrizCurricular(long seqDivisaoCurricular, long seqModalidade)
        {
            //string descricaoCursoOferta = this.CurriculoCursoOfertaDomainService.SearchProjectionByKey(new SMCSeqSpecification<CurriculoCursoOferta>(seqCurriculoCursoOferta), s => s.CursoOferta.Descricao);

            string descricaoDivisao = DivisaoCurricularDomainService.SearchProjectionByKey(new SMCSeqSpecification<DivisaoCurricular>(seqDivisaoCurricular), s => s.Descricao);

            string descricaoModalidade = ModalidadeDomainService.SearchProjectionByKey(new SMCSeqSpecification<Modalidade>(seqModalidade), s => s.Descricao);

            string descricaoMatriz = $"{descricaoDivisao} - {descricaoModalidade}";

            return descricaoMatriz;
        }

        /// <summary>
        /// Busca o codigo do item da matriz curricular de acordo com os valores selecionado
        /// </summary>
        /// <param name="seqCursoOfertaLocalidade">Sequencial do curso oferta localidade</param>
        /// <param name="codigo">codigo da matriz curricular</param>
        /// <returns>Dados para inserir uma matriz curricular</returns>
        public string BuscarConfiguracaoLocalidadeMatrizCurricular(long seqCursoOfertaLocalidade, string codigo)
        {
            string descricaoLocalidade = CursoOfertaLocalidadeDomainService.SearchProjectionByKey(new SMCSeqSpecification<CursoOfertaLocalidade>(seqCursoOfertaLocalidade), s => s.HierarquiasEntidades.FirstOrDefault().ItemSuperior.Entidade.Sigla);

            string descricaoCodigo = $"{descricaoLocalidade}{codigo}";

            return descricaoCodigo;
        }

        /// <summary>
        /// Busca a matriz curricular com dados para selecionar o datasource de unidade/localidade
        /// </summary>
        /// <param name="seq">Sequencial da matriz curricular</param>
        /// <returns>Dados da matriz curricular</returns>
        public MatrizCurricularVO BuscarMatrizCurricular(long seq)
        {
            var matrizCurricular = this.SearchByKey(new SMCSeqSpecification<MatrizCurricular>(seq), IncludesMatrizCurricular.Ofertas_CursoOfertaLocalidadeTurno
                                                                                                  | IncludesMatrizCurricular.Ofertas_HistoricosSituacao
                                                                                                  | IncludesMatrizCurricular.Ofertas_ExcecoesLocalidade
                                                                                                  | IncludesMatrizCurricular.CurriculoCursoOferta_Curriculo);
            var matrizCurricularVo = matrizCurricular.Transform<MatrizCurricularVO>();

            foreach (var item in matrizCurricularVo.Ofertas)
            {
                item.Turnos = this.TurnoDomainService.BuscarTurnosPorCursoOfertaLocalidadeSelect(item.SeqCursoOfertaLocalidade);
                item.DescricaoTurno = item.Turnos.Where(w => w.Seq == item.SeqCursoOfertaTurno).Select(s => s.Descricao).SingleOrDefault();
                item.DataInicioVigencia = matrizCurricular.Ofertas
                    .Single(s => s.Seq == item.Seq)
                    .HistoricosSituacao
                    .Where(w => w.SituacaoMatrizCurricularOferta == SituacaoMatrizCurricularOferta.Ativa)
                    .Select(x => x.DataInicio)
                    .DefaultIfEmpty()
                    .Max();
            }

            return matrizCurricularVo;
        }

        public List<long> BuscarSeqsMatrizCurricularUsuarioLogado()
        {
            return CursoDomainService.SearchProjectionAll(x => x.Curriculos.SelectMany(c => c.CursosOferta.SelectMany(co => co.MatrizesCurriculares.Select(m => m.Seq)))).SelectMany(x => x).ToList();
        }

        /// <summary>
        /// Busca as matrizes curricular que atendam os filtros informados e com a paginação correta
        /// </summary>
        /// <param name="filtros">Sequencial do curriculo curso oferta</param>
        /// <returns>SMCPagerData com a lista de matrizes curricular</returns>
        public SMCPagerData<MatrizCurricularVO> BuscarMatrizesCurricular(MatrizCurricularFilterSpecification filtros)
        {
            DateTime dataAtual = DateTime.Now.Date;
            int total = 0;

            var matrizes = this.SearchProjectionBySpecification(filtros,
                                                                p => new MatrizCurricularVO()
                                                                {
                                                                    Seq = p.Seq,
                                                                    Codigo = p.Codigo,
                                                                    SeqCurriculoCursoOferta = p.SeqCurriculoCursoOferta,
                                                                    Descricao = p.Descricao,
                                                                    DescricaoComplementar = p.DescricaoComplementar,
                                                                    NumeroSequencial = p.NumeroSequencial,
                                                                    QuantidadeMesesLimiteConclusao = p.QuantidadeMesesLimiteConclusao,
                                                                    QuantidadeMesesPrevistoConclusao = p.QuantidadeMesesPrevistoConclusao,
                                                                    SeqDivisaoCurricular = p.SeqDivisaoCurricular,
                                                                    SeqModalidade = p.SeqModalidade,
                                                                    Ofertas = p.Ofertas.Select(s => new MatrizCurricularOfertaVO()
                                                                    {
                                                                        Seq = s.Seq,
                                                                        SeqCurriculoCursoOferta = p.SeqCurriculoCursoOferta,
                                                                        Codigo = s.Codigo,
                                                                        DescricaoUnidade = s.CursoOfertaLocalidadeTurno.CursoOfertaLocalidade.CursoUnidade.HierarquiasEntidades.FirstOrDefault().ItemSuperior.Entidade.Nome,
                                                                        DescricaoLocalidade = s.CursoOfertaLocalidadeTurno.CursoOfertaLocalidade.HierarquiasEntidades.FirstOrDefault().ItemSuperior.Entidade.Nome,
                                                                        DescricaoTurno = s.CursoOfertaLocalidadeTurno.Turno.Descricao,
                                                                        DescricaoMatrizCurricular = s.MatrizCurricular.Descricao,
                                                                        DescricaoComplementarMatrizCurricular = s.MatrizCurricular.DescricaoComplementar,
                                                                        NumeroPeriodoAtivo = s.NumeroPeriodoAtivo,
                                                                        DataInicioVigencia = s.HistoricosSituacao.Where(w => w.SituacaoMatrizCurricularOferta == SituacaoMatrizCurricularOferta.Ativa).Max(m => m.DataInicio),
                                                                        DataFinalVigencia = s.HistoricosSituacao.Where(w => w.SituacaoMatrizCurricularOferta == SituacaoMatrizCurricularOferta.Extinta).Max(m => m.DataInicio),
                                                                        HistoricoSituacaoAtual = s.HistoricosSituacao.Count(w => w.DataInicio <= dataAtual && (w.DataFim == null || w.DataFim >= dataAtual)) > 0 ?
                                                                                                 s.HistoricosSituacao.Where(w => w.DataInicio <= dataAtual && (w.DataFim == null || w.DataFim >= dataAtual)).FirstOrDefault().SituacaoMatrizCurricularOferta :
                                                                                                 s.HistoricosSituacao.Where(w => w.DataFim == null).FirstOrDefault().SituacaoMatrizCurricularOferta,
                                                                    }).ToList(),
                                                                    ContemOfertaAtiva = p.Ofertas.Any(anyOferta => anyOferta.HistoricosSituacao.Any(anyHistorico => anyHistorico.SituacaoMatrizCurricularOferta == SituacaoMatrizCurricularOferta.Ativa && anyHistorico.DataInicio <= DateTime.Today))
                                                                }, out total);

            return new SMCPagerData<MatrizCurricularVO>(matrizes, total);
        }

        /// <summary>
        /// Busca as matrizes curricular que atendam os filtros informados e com a paginação correta
        /// </summary>
        /// <param name="filtros">Sequencial do curriculo curso oferta</param>
        /// <returns>SMCPagerData com a lista de matrizes curricular</returns>
        public SMCPagerData<MatrizCurricularVO> BuscarMatrizesCurricularLookupOferta(MatrizCurricularOfertaFiltroVO filtros)
        {
            var spec = filtros.Transform<MatrizCurricularFilterSpecification>();
            //recurso temporário
            var pageSettings = spec.GetPageSetting();
            pageSettings.PageSize = int.MaxValue;

            spec.SetPageSetting(pageSettings);
            spec.MaxResults = int.MaxValue;
            if (filtros.SeqDispensa.HasValue && filtros.SeqDispensa > 0)
            {
                var dispensa = DispensaDomainService.BuscarDispensa(filtros.SeqDispensa.Value);

                //Filtro de matriz curriculares que possui seu grupo de componentes, os componentes listados como origem
                spec.GrupoOrigemSeqComponentesCurriculares = dispensa.GrupoOrigens.Select(s => s.Seq).ToList();

                //Filtro de matriz curriculares que NÃO possui no seu grupo de componentes, os componentes listados como dispensado
                spec.GrupoDispensadoSeqComponentesCurriculares = dispensa.GrupoDispensados.Select(s => s.Seq).ToList();
            }

            var matrizes = BuscarMatrizesCurricularOfertasHistoricosSituacaoAtual(spec);
            if (matrizes.SMCAny())
            {
                /// Se houver Ciclo letivo é feito o filtro da Situação, conforme o período do ciclo letivo,
                /// caso contrário, deixa o filtro existente, com a data atua.
                AtualizarSituacaoHistoricoOfertasMatrizCicloLetivo(matrizes, filtros.SeqCicloLetivo);
                matrizes = matrizes.Where(m => m.Ofertas.SMCAny()).ToList();
            }

            return new SMCPagerData<MatrizCurricularVO>(matrizes);
        }

        #region [ Métodos Privados - Buscar Matrizes Curricular Lookup Oferta ]

        /// <summary>
        /// Busca as matrizes e suas ofertas, com a situação atual e todo seu histórico de situações
        /// </summary>
        /// <param name="spec"></param>
        /// <returns></returns>
        private List<MatrizCurricularVO> BuscarMatrizesCurricularOfertasHistoricosSituacaoAtual(MatrizCurricularFilterSpecification spec)
        {
            DateTime dataAtual = DateTime.Now.Date;
            return this.SearchProjectionBySpecification(spec,
                p => new MatrizCurricularVO()
                {
                    Seq = p.Seq,
                    SeqCurriculoCursoOferta = p.SeqCurriculoCursoOferta,
                    Descricao = p.Descricao,
                    DescricaoComplementar = p.DescricaoComplementar,
                    Codigo = p.Ofertas.FirstOrDefault().Codigo,
                    DescricaoUnidade = p.Ofertas.FirstOrDefault().CursoOfertaLocalidadeTurno.CursoOfertaLocalidade.CursoUnidade.HierarquiasEntidades.FirstOrDefault().ItemSuperior.Entidade.Nome,
                    DescricaoLocalidade = p.Ofertas.FirstOrDefault().CursoOfertaLocalidadeTurno.CursoOfertaLocalidade.HierarquiasEntidades.FirstOrDefault().ItemSuperior.Entidade.Nome,
                    DescricaoTurno = p.Ofertas.FirstOrDefault().CursoOfertaLocalidadeTurno.Turno.Descricao,
                    DescricaoMatrizCurricular = p.Descricao,
                    DescricaoComplementarMatrizCurricular = p.DescricaoComplementar,
                    SeqMatrizCurricular = p.Ofertas.FirstOrDefault().Seq,
                    SeqEntidadeLocalidade = p.Ofertas.FirstOrDefault().CursoOfertaLocalidadeTurno.CursoOfertaLocalidade.HierarquiasEntidades.FirstOrDefault().ItemSuperior.Entidade.Seq,

                    Ofertas = p.Ofertas.Select(s => new MatrizCurricularOfertaVO()
                    {
                        Seq = s.Seq,
                        SeqCurriculoCursoOferta = p.SeqCurriculoCursoOferta,
                        Codigo = s.Codigo,
                        DescricaoUnidade = s.CursoOfertaLocalidadeTurno.CursoOfertaLocalidade.CursoUnidade.HierarquiasEntidades.FirstOrDefault().ItemSuperior.Entidade.Nome,
                        DescricaoLocalidade = s.CursoOfertaLocalidadeTurno.CursoOfertaLocalidade.HierarquiasEntidades.FirstOrDefault().ItemSuperior.Entidade.Nome,
                        DescricaoTurno = s.CursoOfertaLocalidadeTurno.Turno.Descricao,
                        DescricaoMatrizCurricular = s.MatrizCurricular.Descricao,
                        DescricaoComplementarMatrizCurricular = s.MatrizCurricular.DescricaoComplementar,
                        NumeroPeriodoAtivo = s.NumeroPeriodoAtivo,
                        SeqCursoOfertaLocalidadeTurno = s.SeqCursoOfertaLocalidadeTurno,

                        DataInicioVigencia = s.HistoricosSituacao.Count(w => w.DataInicio <= dataAtual && (w.DataFim == null || w.DataFim >= dataAtual)) > 0 ?
                                                s.HistoricosSituacao.Where(w => w.DataInicio <= dataAtual && (w.DataFim == null || w.DataFim >= dataAtual)).FirstOrDefault().DataInicio :
                                                s.HistoricosSituacao.Where(w => w.DataFim == null).FirstOrDefault().DataInicio,

                        DataFinalVigencia = s.HistoricosSituacao.Count(w => w.DataInicio <= dataAtual && (w.DataFim == null || w.DataFim >= dataAtual)) > 0 ?
                                                s.HistoricosSituacao.Where(w => w.DataInicio <= dataAtual && (w.DataFim == null || w.DataFim >= dataAtual)).FirstOrDefault().DataFim :
                                                s.HistoricosSituacao.Where(w => w.DataFim == null).FirstOrDefault().DataFim,

                        HistoricoSituacaoAtual = s.HistoricosSituacao.Count(w => w.DataInicio <= dataAtual && (w.DataFim == null || w.DataFim >= dataAtual)) > 0 ?
                                                s.HistoricosSituacao.Where(w => w.DataInicio <= dataAtual && (w.DataFim == null || w.DataFim >= dataAtual)).FirstOrDefault().SituacaoMatrizCurricularOferta :
                                                s.HistoricosSituacao.Where(w => w.DataFim == null).FirstOrDefault().SituacaoMatrizCurricularOferta,

                        HistoricosSituacao = s.HistoricosSituacao.Select(h => new HistoricoSituacaoMatrizCurricularOfertaVO()
                        {
                            Seq = h.Seq,
                            SeqMatrizCurricularOferta = h.SeqMatrizCurricularOferta,
                            DataInicio = h.DataInicio,
                            DataFim = h.DataFim,
                            SituacaoMatrizCurricularOferta = h.SituacaoMatrizCurricularOferta
                        }).ToList(),
                    }).ToList(),
                    ContemOfertaAtiva = p.Ofertas.Any(anyOferta => anyOferta.HistoricosSituacao.Any(anyHistorico => anyHistorico.SituacaoMatrizCurricularOferta == SituacaoMatrizCurricularOferta.Ativa && anyHistorico.DataInicio <= DateTime.Today))
                }).ToList();
        }

        /// <summary>
        /// O filtro deverá listar somente as Ofertas de Matriz Curricular, cuja Matriz possui as situações
        /// "Em extinção" e "Ativa" com vigência coincidente com o período letivo do ciclo letivo* passado
        /// como parâmetro, de acordo com cada oferta de matriz:
        /// </summary>
        /// <param name="matrizes"></param>
        /// <param name="seqCicloLetivo"></param>
        private void AtualizarSituacaoHistoricoOfertasMatrizCicloLetivo(IEnumerable<MatrizCurricularVO> matrizes, long? seqCicloLetivo)
        {
            if (seqCicloLetivo.HasValue)
            {
                foreach (var matriz in matrizes)
                {
                    foreach (var oferta in matriz.Ofertas)
                    {
                        AtualizarSituacaoHistoricoOfertaCicloLetivo(oferta, seqCicloLetivo.Value);
                    }
                    // somentes as Ofertas Ativas e em extinção
                    matriz.Ofertas = matriz?.Ofertas.Where(o => o.HistoricoSituacaoAtual != null
                                                            && (o.HistoricoSituacaoAtual == SituacaoMatrizCurricularOferta.Ativa
                                                             || o.HistoricoSituacaoAtual == SituacaoMatrizCurricularOferta.EmExtincao)).ToList();
                }
            }
        }

        /// <summary>
        /// A data início da situação da oferta de matriz deve ser menor ou igual a data início do ciclo letivo
        /// e a data fim da situação da oferta deve ser nula, ou ser maior ou igual a data fim do ciclo letivo.
        /// </summary>
        /// <param name="oferta"></param>
        /// <param name="seqCicloLetivo"></param>
        private void AtualizarSituacaoHistoricoOfertaCicloLetivo(MatrizCurricularOfertaVO oferta, long seqCicloLetivo)
        {
            try
            {
                var periodoLetivo = ConfiguracaoEventoLetivoDomainService.BuscarDatasEventoLetivo(seqCicloLetivo,
                                                                       oferta.SeqCursoOfertaLocalidadeTurno,
                                                                       null,
                                                                       TOKEN_TIPO_EVENTO.PERIODO_LETIVO);
                if (periodoLetivo == null) { return; }

                var situacaoHistorico = oferta.HistoricosSituacao.FirstOrDefault(x => x.DataInicio <= periodoLetivo.DataInicio
                                                                             && (x.DataFim == null || x.DataFim >= periodoLetivo.DataFim));
                if (situacaoHistorico == null)
                {
                    var situacaoAtiva = oferta.HistoricosSituacao.FirstOrDefault(x => x.SituacaoMatrizCurricularOferta == SituacaoMatrizCurricularOferta.Ativa);
                    var situacaoEmExtincao = oferta.HistoricosSituacao.FirstOrDefault(x => x.SituacaoMatrizCurricularOferta == SituacaoMatrizCurricularOferta.EmExtincao);

                    if (situacaoAtiva != null && situacaoEmExtincao != null)
                    {
                        if (situacaoAtiva.DataInicio <= periodoLetivo.DataInicio && (situacaoEmExtincao.DataFim == null || situacaoEmExtincao.DataFim >= periodoLetivo.DataFim))
                        {
                            situacaoHistorico = situacaoEmExtincao;
                        }
                    }
                }

                oferta.DataInicioVigencia = situacaoHistorico?.DataInicio;
                oferta.DataFinalVigencia = situacaoHistorico?.DataFim;
                oferta.HistoricoSituacaoAtual = situacaoHistorico?.SituacaoMatrizCurricularOferta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion [ Métodos Privados - Buscar Matrizes Curricular Lookup Oferta ]

        /// <summary>
        /// Busca a matrize curricular com a oferta selecionada para retorno do lookup
        /// </summary>
        /// <param name="SeqMatrizCurricularOferta">Sequencial da matriz curricular oferta</param>
        /// <returns>Objeto matriz curricular oferta</returns>
        public MatrizCurricularOfertaVO BuscarMatrizesCurricularLookupOfertaSelecionado(long seqMatrizCurricularOferta)
        {
            DateTime dataAtual = DateTime.Now.Date;
            MatrizCurricularFilterSpecification filtros = new MatrizCurricularFilterSpecification() { SeqMatrizCurricularOferta = seqMatrizCurricularOferta };

            DisableFilter(FILTER.HIERARQUIA_ENTIDADE_ORGANIZACIONAL);

            var retorno = this.SearchProjectionBySpecification(filtros,
                                                                p => p.Ofertas.Where(w => w.Seq == seqMatrizCurricularOferta)
                                                                    .Select(s => new MatrizCurricularOfertaVO()
                                                                    {
                                                                        Seq = s.Seq,
                                                                        SeqCurriculoCursoOferta = p.SeqCurriculoCursoOferta,
                                                                        SeqMatrizCurricular = p.Seq,
                                                                        Codigo = s.Codigo,
                                                                        Descricao = s.MatrizCurricular.Descricao,
                                                                        DescricaoUnidade = s.CursoOfertaLocalidadeTurno.CursoOfertaLocalidade.CursoUnidade.HierarquiasEntidades.FirstOrDefault().ItemSuperior.Entidade.Nome,
                                                                        DescricaoLocalidade = s.CursoOfertaLocalidadeTurno.CursoOfertaLocalidade.HierarquiasEntidades.FirstOrDefault().ItemSuperior.Entidade.Nome,
                                                                        SeqEntidadeLocalidade = s.CursoOfertaLocalidadeTurno.CursoOfertaLocalidade.HierarquiasEntidades.FirstOrDefault().ItemSuperior.Entidade.Seq,
                                                                        DescricaoTurno = s.CursoOfertaLocalidadeTurno.Turno.Descricao,
                                                                        DescricaoMatrizCurricular = s.MatrizCurricular.Descricao,
                                                                        DescricaoComplementarMatrizCurricular = s.MatrizCurricular.DescricaoComplementar,
                                                                        NumeroPeriodoAtivo = s.NumeroPeriodoAtivo,

                                                                        DataInicioVigencia = s.HistoricosSituacao.Count(w => w.DataInicio <= dataAtual && (w.DataFim == null || w.DataFim >= dataAtual)) > 0 ?
                                                                                             s.HistoricosSituacao.Where(w => w.DataInicio <= dataAtual && (w.DataFim == null || w.DataFim >= dataAtual)).FirstOrDefault().DataInicio :
                                                                                             s.HistoricosSituacao.Where(w => w.DataFim == null).FirstOrDefault().DataInicio,

                                                                        DataFinalVigencia = s.HistoricosSituacao.Count(w => w.DataInicio <= dataAtual && (w.DataFim == null || w.DataFim >= dataAtual)) > 0 ?
                                                                                            s.HistoricosSituacao.Where(w => w.DataInicio <= dataAtual && (w.DataFim == null || w.DataFim >= dataAtual)).FirstOrDefault().DataFim :
                                                                                            s.HistoricosSituacao.Where(w => w.DataFim == null).FirstOrDefault().DataFim,

                                                                        HistoricoSituacaoAtual = s.HistoricosSituacao.Count(w => w.DataInicio <= dataAtual && (w.DataFim == null || w.DataFim >= dataAtual)) > 0 ?
                                                                                                 s.HistoricosSituacao.Where(w => w.DataInicio <= dataAtual && (w.DataFim == null || w.DataFim >= dataAtual)).FirstOrDefault().SituacaoMatrizCurricularOferta :
                                                                                                 s.HistoricosSituacao.Where(w => w.DataFim == null).FirstOrDefault().SituacaoMatrizCurricularOferta,
                                                                        ExcecoesLocalidade = s.ExcecoesLocalidade.Select(e => new MatrizCurricularOfertaExcecaoLocalidadeVO()
                                                                        {
                                                                            Seq = e.Seq,
                                                                            SeqEntidadeLocalidade = e.SeqEntidadeLocalidade,
                                                                            SeqMatrizCurricularOferta = e.SeqMatrizCurricularOferta,
                                                                            DescricaoLocalidade = e.EntidadeLocalidade.Nome
                                                                        }).ToList(),
                                                                    }).FirstOrDefault()).Single();

            EnableFilter(FILTER.HIERARQUIA_ENTIDADE_ORGANIZACIONAL);

            return retorno;
        }

        /// <summary>
        /// Busca os dados do curso, curriculo, oferta e matriz para o cabeçalho da consulta de matriz curricular
        /// </summary>
        /// <param name="seq">Sequencial da matriz curricular</param>
        /// <returns>Dados do cabeçalho</returns>
        public MatrizCurricularCabecalhoVO BuscarMatrizCurricularCabecalho(long seq)
        {
            return this.SearchProjectionByKey(new SMCSeqSpecification<MatrizCurricular>(seq), p =>
                new MatrizCurricularCabecalhoVO()
                {
                    // Curso
                    SeqCurso = p.CurriculoCursoOferta.CursoOferta.Curso.Seq,
                    DescricaoCurso = p.CurriculoCursoOferta.CursoOferta.Curso.Nome,
                    // Currículo
                    CodigoCurriculo = p.CurriculoCursoOferta.Curriculo.Codigo,
                    DescricaoCurriculo = p.CurriculoCursoOferta.Curriculo.Descricao,
                    DescricaoComplementarCurriculo = p.CurriculoCursoOferta.Curriculo.DescricaoComplementar,
                    SituacaoCurriculo = p.CurriculoCursoOferta.Curriculo.Ativo,
                    // Oferta Curso
                    SeqCursoOferta = p.CurriculoCursoOferta.CursoOferta.Seq,
                    DescricaoOfertaCurso = p.CurriculoCursoOferta.CursoOferta.Descricao,
                    // Matriz Curricular
                    CodigoMatrizCurricular = p.Codigo,
                    DescricaoMatrizCurricular = p.Descricao,
                    DescricaoComplementarMatrizCurricular = p.DescricaoComplementar
                });
        }

        /// <summary>
        /// Busca a matriz curricular para com todas as configurações e divisões curriculares para o relatório
        /// </summary>
        /// <param name="seq">Sequencial da matriz curricular</param>
        /// <returns>Dados da matriz curricular para o relatório</returns>
        public MatrizCurricularRelatorioVO BuscarMatrizCurricularRelatorio(long seq)
        {
            var relatorio = new MatrizCurricularRelatorioVO();

            var relatorioCabecalho = BuscarRelatorioCabecalho(seq);

            var parametros = this.InstituicaoNivelTipoComponenteCurricularDomainService.BuscarInstituicaoNivelTipoComponenteCurricularConfiguracao(relatorioCabecalho[0].SeqNivelEnsino, relatorioCabecalho[0].SeqTipoComponenteCurricular);

            var relatorioDados = BuscarRelatorioDadosConfigurados(seq, parametros.FormatoCargaHoraria);

            var seqsGruposCompletos = relatorioDados.Select(s => s.GruposSeq).SMCDistinct(d => d).ToList();

            relatorio.MatrizCurricularCabecalho = relatorioCabecalho;

            if (relatorioDados.Count > 0)
                relatorio.MatrizCurricularDados = relatorioDados;
            else
                relatorio.MatrizCurricularDados = new List<MatrizCurricularRelatorioDadosVO>();

            var relatorioGrupos = BuscarRelatorioDadosGrupos(seq, relatorioCabecalho[0].SeqCurriculoCursoOferta, parametros.FormatoCargaHoraria, relatorio.MatrizCurricularDados);

            if (relatorioGrupos.Count > 0)
                relatorio.MatrizCurricularGrupos = relatorioGrupos;
            else
                relatorio.MatrizCurricularGrupos = new List<MatrizCurricularRelatorioGruposVO>();

            return relatorio;
        }

        /// <summary>
        /// Busca os dados do cabeçalho de matriz curricular para o relatório
        /// </summary>
        /// <param name="seq">Sequencial da matriz curricular</param>
        /// <returns>Lista com objeto cabeçalho de matriz curricular</returns>
        private List<MatrizCurricularRelatorioCabecalhoVO> BuscarRelatorioCabecalho(long seq)
        {
            DateTime dataAtual = DateTime.Now.Date;

            //Recuperar os dados do cabeçalho
            var relatorioCabecalho = this.SearchProjectionByKey(new SMCSeqSpecification<MatrizCurricular>(seq), p => p.Ofertas.Select(f => new MatrizCurricularRelatorioCabecalhoVO()
            {
                SeqCurriculoCursoOferta = p.SeqCurriculoCursoOferta,
                SeqMatrizCurricular = p.Seq,
                DescricaoMatrizCurricular = p.Codigo + " - " + p.Descricao,
                DescricaoSituacao = f.HistoricosSituacao.Count(w => w.DataInicio <= dataAtual && (w.DataFim == null || w.DataFim >= dataAtual)) > 0 ?
                                           f.HistoricosSituacao.Where(w => w.DataInicio <= dataAtual && (w.DataFim == null || w.DataFim >= dataAtual)).FirstOrDefault().SituacaoMatrizCurricularOferta.ToString() :
                                           f.HistoricosSituacao.Where(w => w.DataFim == null).FirstOrDefault().SituacaoMatrizCurricularOferta.ToString(),
                SeqNivelEnsino = p.DivisaoCurricular.SeqNivelEnsino,
                DescricaoNivelEnsino = p.DivisaoCurricular.NivelEnsino.Descricao,
                DescricaoLocalidade = f.CursoOfertaLocalidadeTurno.CursoOfertaLocalidade.HierarquiasEntidades.FirstOrDefault().ItemSuperior.Entidade.Nome,
                DescricaoTurno = f.CursoOfertaLocalidadeTurno.Turno.Descricao,
            })).ToList();

            //Caso tenha cadastro quebrado sem ofertas trata o erro para exibir um relatório vazio
            if (relatorioCabecalho.Count == 0)
            {
                var relatorioVazio = this.SearchProjectionBySpecification(new SMCSeqSpecification<MatrizCurricular>(seq), p => new MatrizCurricularRelatorioCabecalhoVO()
                {
                    SeqCurriculoCursoOferta = p.SeqCurriculoCursoOferta,
                    SeqMatrizCurricular = p.Seq,
                    DescricaoMatrizCurricular = p.Codigo + " - " + p.Descricao,
                    SeqNivelEnsino = p.DivisaoCurricular.SeqNivelEnsino,
                    DescricaoNivelEnsino = p.DivisaoCurricular.NivelEnsino.Descricao,
                }).ToList();

                relatorioCabecalho = relatorioVazio;
            }

            return relatorioCabecalho;
        }

        /// <summary>
        /// Busca os dados das configurações e grupos de matriz curricular agrupados por divisão para o relatório
        /// </summary>
        /// <param name="seq">Sequencial da matriz curricular</param>
        /// <param name="formato">Formato da carga horária definido em consulta de parâmetros</param>
        /// <returns>Lista com objeto de dados configurados de matriz curricular</returns>
        private List<MatrizCurricularRelatorioDadosVO> BuscarRelatorioDadosConfigurados(long seq, FormatoCargaHoraria? formato)
        {
            string formatoCargaHoraria = formato == FormatoCargaHoraria.HoraAula ? "h/a" : "h";

            //Recupera os dados da configuração do componente por divisão
            var relatorioDados = this.SearchProjectionByKey(new SMCSeqSpecification<MatrizCurricular>(seq),
                p => p.ConfiguracoesComponente.SelectMany(f => f.DivisoesComponente.Select(d => new MatrizCurricularRelatorioDadosVO()
                {
                    SeqMatrizCurricular = p.Seq,
                    FormatoCargaHoraria = formatoCargaHoraria,
                    NumeroDivisaoCurricular = f.DivisaoMatrizCurricular.DivisaoCurricularItem.Numero,
                    DescricaoDivisaoCurricular = f.DivisaoMatrizCurricular.DivisaoCurricularItem.Descricao,
                    SeqConfiguracaoComponente = f.ConfiguracaoComponente.Seq,
                    CodigoConfiguracaoComponente = f.ConfiguracaoComponente.Codigo,
                    DescricaoConfiguracaoComponente = f.ConfiguracaoComponente.Descricao,
                    DescricaoComplementarConfiguracaoComponente = f.ConfiguracaoComponente.DescricaoComplementar,
                    CargaHorariaConfiguracaoComponente = f.ConfiguracaoComponente.ComponenteCurricular.CargaHoraria,
                    CreditosConfiguracaoComponente = f.ConfiguracaoComponente.ComponenteCurricular.Credito,
                    VagasConfiguracaoComponente = f.QuantidadeVagas,
                    SeqTipoDivisaoComponente = d.DivisaoComponente.SeqTipoDivisaoComponente,
                    DescricaoTipoDivisaoComponente = d.DivisaoComponente.TipoDivisaoComponente.Descricao,
                    SeqModalidadeDivisaoComponente = d.DivisaoComponente.TipoDivisaoComponente.SeqModalidade,
                    ModalidadeDivisaoComponente = d.DivisaoComponente.TipoDivisaoComponente.Modalidade.Descricao,
                    CargaHorariaDivisaoComponente = d.DivisaoComponente.CargaHoraria,
                    ProfessoresDivisaoComponente = d.QuantidadeProfessores,
                    GruposDivisaoComponente = d.QuantidadeGrupos,
                    //GruposSeq = f.GrupoCurricularComponente.SeqGrupoCurricular,
                    //GrupoSeqPai = f.GrupoCurricularComponente.GrupoCurricular.SeqGrupoCurricularSuperior,
                    Configuracao = true,
                }))).ToList();

            //Recupera os dados do grupo curricular por divisão
            var relatorioGrupos = this.SearchProjectionByKey(new SMCSeqSpecification<MatrizCurricular>(seq),
                p => p.DivisoesMatrizCurricular
                .SelectMany(f => f.ConfiguracoesGrupos.Select(g => new MatrizCurricularRelatorioDadosVO()
                {
                    SeqMatrizCurricular = p.Seq,
                    FormatoCargaHoraria = formatoCargaHoraria,
                    NumeroDivisaoCurricular = f.DivisaoCurricularItem.Numero,
                    DescricaoDivisaoCurricular = f.DivisaoCurricularItem.Descricao,
                    GruposSeq = g.CurriculoCursoOfertaGrupo.GrupoCurricular.Seq,
                    GrupoSeqPai = g.CurriculoCursoOfertaGrupo.GrupoCurricular.SeqGrupoCurricularSuperior,
                    GruposDescricao = g.CurriculoCursoOfertaGrupo.GrupoCurricular.Descricao,
                    GruposCargaHoraria = 0,
                    GruposCredito = 0,
                    GruposTipoConfiguracao = g.CurriculoCursoOfertaGrupo.GrupoCurricular.SeqTipoConfiguracaoGrupoCurricular,
                    GruposTipoConfiguracaoDescricao = g.CurriculoCursoOfertaGrupo.GrupoCurricular.TipoConfiguracaoGrupoCurricular.Descricao,
                    GruposFormatoConfiguracao = g.CurriculoCursoOfertaGrupo.GrupoCurricular.FormatoConfiguracaoGrupo,
                    GruposQuantidadeHorariaAula = g.CurriculoCursoOfertaGrupo.GrupoCurricular.QuantidadeHoraAula,
                    GruposQuantidadeHorariaRelogio = g.CurriculoCursoOfertaGrupo.GrupoCurricular.QuantidadeHoraRelogio,
                    GruposQuantidadeCredito = g.CurriculoCursoOfertaGrupo.GrupoCurricular.QuantidadeCreditos,
                    GruposQuantidadeItens = g.CurriculoCursoOfertaGrupo.GrupoCurricular.QuantidadeItens,
                    Configuracao = false,
                }))).ToList();

            //Recupera os detalhes de cada grupo para exibir na listagem final do relatório
            relatorioGrupos = CalcularCargaHorariaGrupoPorTipoConfiguracao(relatorioGrupos, formato);

            relatorioDados.AddRange(relatorioGrupos);

            relatorioDados.Where(w => w.NumeroDivisaoCurricular == null).SMCForEach(f => { f.NumeroDivisaoCurricular = 99; f.DescricaoDivisaoCurricular = "Demais componentes"; });

            return relatorioDados;
        }

        /// <summary>
        /// Busca os dados das configurações e grupos de matriz curricular agrupados por divisão para o relatório
        /// </summary>
        /// <param name="seq">Sequencial da matriz curricular</param>
        /// <param name="formato">Formato da carga horária definido em consulta de parâmetros</param>
        /// <returns>Lista com objeto de dados configurados de matriz curricular</returns>
        private List<MatrizCurricularRelatorioGruposVO> BuscarRelatorioDadosGrupos(long seq, long seqCurriculoCursoOferta, FormatoCargaHoraria? formato, List<MatrizCurricularRelatorioDadosVO> dadosListados)
        {
            string formatoCargaHoraria = formato == FormatoCargaHoraria.HoraAula ? "h/a" : "h";

            var associadosOferta = GrupoCurricularDomainService.BuscarGruposCurricularesTreeCurriculoCursoOferta(seqCurriculoCursoOferta);

            var retorno = new List<MatrizCurricularRelatorioGruposVO>();
            var contador = 0;
            foreach (var item in associadosOferta)
            {
                MatrizCurricularRelatorioGruposVO registro = new MatrizCurricularRelatorioGruposVO();
                if (item.Folha)
                {
                    var configuracao = item.ConfiguracoesComponentes.FirstOrDefault();
                    if (configuracao != null)
                    {
                        var configuracaoRealizada = dadosListados.Where(w => w.CodigoConfiguracaoComponente == configuracao.Codigo).ToList();

                        if (configuracaoRealizada.Count > 0)
                            foreach (var divisaoRealizada in configuracaoRealizada)
                            {
                                var registroDivisao = divisaoRealizada.Transform<MatrizCurricularRelatorioGruposVO>();
                                registroDivisao.GruposSeq = item.SeqGrupoCurricularSuperior;
                                registroDivisao.GruposSeqPai = item.SeqGrupoCurricularSuperior;
                                retorno.Add(registroDivisao);
                            }
                        else
                        {
                            registro.SeqMatrizCurricular = seq;
                            registro.FormatoCargaHoraria = formatoCargaHoraria;
                            registro.CodigoConfiguracaoComponente = configuracao.Codigo;
                            registro.DescricaoConfiguracaoComponente = configuracao.Descricao;
                            registro.DescricaoComplementarConfiguracaoComponente = configuracao.DescricaoComplementar;
                            registro.CargaHorariaConfiguracaoComponente = item.CargaHoraria;
                            registro.CreditosConfiguracaoComponente = item.Credito;
                            registro.VagasConfiguracaoComponente = 0;
                            registro.CargaHorariaDivisaoComponente = 0;
                            registro.ProfessoresDivisaoComponente = 0;
                            registro.GruposSeq = item.SeqGrupoCurricularSuperior;
                            registro.GruposSeqPai = item.SeqGrupoCurricularSuperior;
                            registro.Configuracao = true;

                            if (configuracao.DivisoesComponente.Count > 0)
                            {
                                foreach (var divisao in configuracao.DivisoesComponente)
                                {
                                    registro.SeqTipoDivisaoComponente = divisao.SeqTipoDivisaoComponente;
                                    registro.DescricaoTipoDivisaoComponente = divisao?.TipoDivisaoComponente?.Descricao;
                                    registro.SeqModalidadeDivisaoComponente = divisao?.TipoDivisaoComponente?.SeqModalidade;
                                    registro.ModalidadeDivisaoComponente = divisao?.TipoDivisaoComponente?.Modalidade?.Descricao;

                                    retorno.Add(registro);
                                }
                            }
                            else
                            {
                                retorno.Add(registro);
                            }
                        }
                    }
                }
                else
                {
                    //Grupo Curricular
                    registro.SeqMatrizCurricular = seq;
                    registro.FormatoCargaHoraria = formatoCargaHoraria;
                    registro.GruposSeq = item.Seq;
                    registro.CodigoConfiguracaoComponente = contador.ToString().PadLeft(3, '0');
                    registro.GruposSeqPai = item.SeqPai;
                    registro.GruposDescricao = item.Descricao;
                    registro.GruposCargaHoraria = item.CargaHoraria;
                    registro.GruposCredito = item.Credito;
                    registro.GruposTipoConfiguracao = item.SeqTipoConfiguracao;
                    registro.GruposTipoConfiguracaoDescricao = item.TipoConfiguracaoDescricao;
                    registro.GruposFormatoConfiguracao = item.FormatoConfiguracaoGrupo;
                    registro.GruposQuantidadeHorariaAula = item.QuantidadeHoraAula;
                    registro.GruposQuantidadeHorariaRelogio = item.QuantidadeHoraRelogio;
                    registro.GruposQuantidadeCredito = item.QuantidadeCreditos;
                    registro.GruposQuantidadeItens = item.QuantidadeItens;
                    registro.Configuracao = false;
                    retorno.Add(registro);
                    contador++;
                }
            }

            return retorno;
        }

        private List<MatrizCurricularRelatorioDadosVO> CalcularCargaHorariaGrupoPorTipoConfiguracao(List<MatrizCurricularRelatorioDadosVO> grupo, FormatoCargaHoraria? formatoCargaHoraria)
        {
            foreach (var item in grupo)
            {
                switch (item.GruposFormatoConfiguracao)
                {
                    case FormatoConfiguracaoGrupo.CargaHoraria:
                        item.GruposCargaHoraria = formatoCargaHoraria == FormatoCargaHoraria.HoraAula ? item.GruposQuantidadeHorariaAula : item.GruposQuantidadeHorariaRelogio;
                        item.GruposCredito = 0;
                        break;

                    case FormatoConfiguracaoGrupo.Credito:
                        item.GruposCargaHoraria = 0;
                        item.GruposCredito = item.GruposQuantidadeCredito;
                        break;

                    case FormatoConfiguracaoGrupo.Itens:
                        item.GruposCargaHoraria = formatoCargaHoraria == FormatoCargaHoraria.HoraAula ?
                            (short?)(item.GruposQuantidadeHorariaAula * item.GruposQuantidadeItens) :
                            (short?)(item.GruposQuantidadeHorariaRelogio * item.GruposQuantidadeItens);
                        item.GruposCredito = (short?)(item.GruposQuantidadeCredito * item.GruposQuantidadeItens);
                        break;

                    default:
                        short totalCargaHoraria = 0;
                        short totalCredito = 0;
                        CalcularCargaHorariaGrupoSemTipoConfiguracao(item.GruposSeq, formatoCargaHoraria, ref totalCargaHoraria, ref totalCredito);
                        item.GruposCargaHoraria = totalCargaHoraria;
                        item.GruposCredito = totalCredito;
                        break;
                }
            }

            return grupo;
        }

        private void CalcularCargaHorariaGrupoSemTipoConfiguracao(long seqGrupoCurricular, FormatoCargaHoraria? formatoCargaHoraria, ref short totalCargaHoraria, ref short totalCredito)
        {
            var includes = IncludesGrupoCurricular.ComponentesCurriculares_ComponenteCurricular | IncludesGrupoCurricular.GruposCurricularesFilhos;
            var grupoCurricular = this.GrupoCurricularDomainService.SearchByKey(new SMCSeqSpecification<GrupoCurricular>(seqGrupoCurricular), includes);

            short valorCargaHorariaGrupo = formatoCargaHoraria == FormatoCargaHoraria.HoraAula ? grupoCurricular.QuantidadeHoraAula.GetValueOrDefault() : grupoCurricular.QuantidadeHoraRelogio.GetValueOrDefault();
            short valorCreditoGrupo = grupoCurricular.QuantidadeCreditos.GetValueOrDefault();
            if (valorCargaHorariaGrupo != 0 || valorCreditoGrupo != 0)
            {
                totalCargaHoraria += valorCargaHorariaGrupo;
                totalCredito += valorCreditoGrupo;
            }
            else
            {
                foreach (var componenteCurricular in grupoCurricular.ComponentesCurriculares)
                {
                    totalCargaHoraria += componenteCurricular.ComponenteCurricular.CargaHoraria.GetValueOrDefault();
                    totalCredito += componenteCurricular.ComponenteCurricular.Credito.GetValueOrDefault();
                }
            }

            foreach (var filhos in grupoCurricular.GruposCurricularesFilhos)
            {
                CalcularCargaHorariaGrupoSemTipoConfiguracao(filhos.Seq, formatoCargaHoraria, ref totalCargaHoraria, ref totalCredito);
            }
        }

        /// <summary>
        /// Busca os dados das divisões da mariz curricular com seus grupos e componentes
        /// </summary>
        /// <param name="seq">Sequencial da matriz curricular</param>
        /// <returns>Dados das divisões da matriz curricular</returns>
        public ConsultaDivisoesMatrizCurricularVO BuscarConsultaDivisoesMatrizCurricular(long seq)
        {
            var consultaMatriz = this.SearchProjectionByKey(new SMCSeqSpecification<MatrizCurricular>(seq), matriz =>
                new
                {
                    matriz.CurriculoCursoOferta.SeqCurriculo,
                    matriz.SeqCurriculoCursoOferta,
                    Divisoes = matriz.DivisoesMatrizCurricular.Select(divisao => new ConsultaDivisaoMatrizCurricularItemVO()
                    {
                        Seq = divisao.Seq,
                        NumeroDivisaoCurricularItem = divisao.DivisaoCurricularItem.Numero,
                        DescricaoDivisaoCurricularItem = divisao.DivisaoCurricularItem.Descricao,
                        ConfiguracoesComponentes = divisao.ConfiguracoesComponentes.Select(componente => new ConsultaDivisaoMatrizCurricularComponenteItemVO()
                        {
                            Seq = componente.ConfiguracaoComponente.SeqComponenteCurricular,
                            SeqGrupoCurricular = componente.GrupoCurricularComponente.SeqGrupoCurricular,
                            SeqGrupoCurricularComponente = componente.SeqGrupoCurricularComponente,
                            CodigoConfiguracao = componente.ConfiguracaoComponente.Codigo,
                            DescricaoConfiguracao = componente.ConfiguracaoComponente.Descricao,
                            DescricaoComplementarConfiguracao = componente.ConfiguracaoComponente.DescricaoComplementar,
                            CargaHorariaComponente = componente.ConfiguracaoComponente.ComponenteCurricular.CargaHoraria,
                            CreditosComponente = componente.ConfiguracaoComponente.ComponenteCurricular.Credito,
                            ExigeAssuntoComponente = componente.ConfiguracaoComponente.ComponenteCurricular.ExigeAssuntoComponente,
                            ContemComponenteSubstituto = componente.ComponentesCurricularSubstitutos.Any(),
                            ContemRequisitos = componente.ConfiguracaoComponente.ComponenteCurricular.Requisitos.Any(),
                            SiglasEntidadesResponsaveisComponente = componente.ConfiguracaoComponente
                                .ComponenteCurricular
                                .EntidadesResponsaveis
                                .Select(s => s.Entidade.Sigla)
                        }),
                        ConfiguracoesGrupos = divisao.ConfiguracoesGrupos.Select(s => new GrupoCurricularListaVO()
                        {
                            SeqGrupoCurricular = s.CurriculoCursoOfertaGrupo.SeqGrupoCurricular,
                            QuantidadeHoraRelogio = s.QuantidadeHoraRelogio,
                            QuantidadeHoraAula = s.QuantidadeHoraAula,
                            QuantidadeCreditos = s.QuantidadeCreditos
                        })
                    }),
                    GruposCurriculares = matriz.CurriculoCursoOferta.GruposCurriculares.Select(s => new { s.SeqGrupoCurricular, s.DivisoesMatrizCurriculares.Count }),
                    ComponentesCurriculares = matriz.ConfiguracoesComponente.Where(w => w.SeqDivisaoMatrizCurricular != null).Select(s => s.ConfiguracaoComponente.SeqComponenteCurricular),
                });

            var seqsGruposCurriculares = consultaMatriz.GruposCurriculares.Select(s => s.SeqGrupoCurricular);

            // Grupos com componentes do currículo
            var gruposTreeCurriculo = this.GrupoCurricularDomainService
                .BuscarGruposCurricularesTree(consultaMatriz.SeqCurriculo)
                .Where(w => seqsGruposCurriculares.Contains(w.SeqGrupoCurricular ?? 0)
                         || seqsGruposCurriculares.Contains(w.SeqGrupoCurricularSuperior ?? 0))
                .ToList();

            // Grupos com componentes à cursar
            var gruposTreeCurriculoACursar = gruposTreeCurriculo
                .Where(w => (consultaMatriz.GruposCurriculares.Count(c => c.SeqGrupoCurricular == w.SeqGrupoCurricular && c.Count == 0) > 0
                             || consultaMatriz.GruposCurriculares.Count(c => c.SeqGrupoCurricular == w.SeqGrupoCurricularSuperior && c.Count == 0) > 0)
                        && !consultaMatriz.ComponentesCurriculares.Contains(w.SeqComponenteCurricular ?? 0))
                .ToList();

            //*Seguindo a regra de exibir sometente os grupos com componentes*/
            //Lista somente os componentes a cursar
            var componentesACursar = gruposTreeCurriculoACursar.Where(w => w.SeqComponenteCurricular.HasValue).ToList();
            //Recupera arvore buscando os pais dos componentes a cursar
            gruposTreeCurriculoACursar = this.RecuperarPais(gruposTreeCurriculoACursar, componentesACursar).ToList();

            //Validar grupos a cursar sem pai
            var seqGruposTreeCurriculoACursar = gruposTreeCurriculoACursar.Select(s => s.Seq);
            var nodulosACursarSemPai = gruposTreeCurriculoACursar.Where(w => w.SeqPai.HasValue && !seqGruposTreeCurriculoACursar.Contains(w.SeqPai.GetValueOrDefault()));
            foreach (var nodulo in nodulosACursarSemPai)
            {
                nodulo.SeqPai = null;
            }

            // Transforma a projeção de divisões numa lista para possibilidar modificações
            var divisoes = consultaMatriz.Divisoes.ToList();

            // Atualiza os grupos das divisões com as hierarquias de grupos
            foreach (var divisao in divisoes)
            {
                var gruposTreeCurriculoDivisao = gruposTreeCurriculo.SMCClone();

                // Recupera os sequenciais retornados na projeção
                List<long> seqsGruposDivisao = new List<long>();

                divisao.ConfiguracoesGrupos.SMCForEach(f =>
                {
                    seqsGruposDivisao.Add(f.SeqGrupoCurricular.Value);
                    var atualiza = gruposTreeCurriculoDivisao.Single(s => s.SeqGrupoCurricular == f.SeqGrupoCurricular);
                    atualiza.QuantidadeCreditos = f.QuantidadeCreditos;
                    atualiza.QuantidadeHoraAula = f.QuantidadeHoraAula;
                    atualiza.QuantidadeHoraRelogio = f.QuantidadeHoraRelogio;
                    atualiza.QuantidadeItens = f.QuantidadeItens;
                });

                // Substitui a lista com os sequenciais pela projeção da hierarquia de grupos
                divisao.ConfiguracoesGrupos = this.RecuperarHierarquiaGrupoDivisao(gruposTreeCurriculoDivisao, seqsGruposDivisao);

                var seqsConfiguracoesGrupos = divisao.ConfiguracoesGrupos.Select(s => s.Seq).ToList();

                divisao.ConfiguracoesGrupos.Where(w => !seqsConfiguracoesGrupos.Contains(w.SeqPai.GetValueOrDefault())).SMCForEach(f => f.SeqPai = null);

                // Monta a hierarquia só com os componentes daquela divisão
                var seqsComponentesDivisao = divisao.ConfiguracoesComponentes.Select(s => s.Seq).Distinct().ToList();
                var seqsGruposComponentesDivisao = divisao.ConfiguracoesComponentes.Select(s => s.SeqGrupoCurricular).Distinct().ToList();
                //Arvore daquela divisão baseado nos componentes e grupos da divisão
                var componentesTree = gruposTreeCurriculo.Where(w => seqsComponentesDivisao.Contains(w.SeqComponenteCurricular ?? 0) || seqsGruposComponentesDivisao.Contains(w.SeqGrupoCurricular ?? 0)).ToList();
                //Listar somente os sequenciais da arvore para buscar os galhos da arvoré sem nodulo superior
                var seqscomponentesTree = componentesTree.Select(s => s.Seq).ToList();
                //Buscar os nodulos que estão sem nodulos superiores
                var nodulosSemPai = componentesTree.Where(w => w.SeqPai.HasValue && !seqscomponentesTree.Contains(w.SeqPai.GetValueOrDefault())).ToList();
                //Adicionar a arvore da divisão o grupo de grupos que está sem nodulo superior
                foreach (var item in nodulosSemPai)
                {
                    if (item.SeqComponenteCurricular.HasValue)
                    {
                        componentesTree.Remove(item);
                    }
                    else
                    {
                        //Valida se nodulo sem pai esta nas configurações de grupo
                        //Caso esteja ele adiciona este pai que já esta devidamente configurado conforme a associação
                        if (seqsConfiguracoesGrupos.Contains((long)item.SeqPai) &&
                            !componentesTree.Select(s => s.Seq).Contains((long)item.SeqPai))
                        {
                            componentesTree.Add(divisao.ConfiguracoesGrupos.FirstOrDefault(f => f.Seq == item.SeqPai));
                        }
                        else
                        {
                            //Verifica algum grupo ficou sem pai esta na arvore de associação e não conste na arvore que esta sendo montada
                            if (gruposTreeCurriculo.Select(s => s.Seq).Contains((long)item.SeqPai) &&
                                !componentesTree.Select(s => s.Seq).Contains((long)item.SeqPai))
                            {
                                componentesTree.Add(gruposTreeCurriculo.FirstOrDefault(f => f.Seq == item.SeqPai));
                            }
                            //Valida se o pai não foi adicionado ou e se mesmo assim o pai irá ser removido
                            if (!componentesTree.Select(s => s.Seq).Contains((long)item.SeqPai))
                            {
                                item.SeqPai = null;
                            }
                        }
                    }
                }

                //Monta devidamente a arvoré ordenando os grupos por DescricaoFormatada e os componentes por Descricao
                divisao.ComponentesGrupos = componentesTree.OrderBy(o => o.SeqComponenteCurricular is null ? o.DescricaoFormatada : o.Descricao);
                // Atualiza a situação dos componentes da divisão
                foreach (var configuracaoComponente in divisao.ConfiguracoesComponentes)
                {
                    var componente = divisao.ComponentesGrupos.FirstOrDefault(s => s.SeqGrupoComponenteCurricular == configuracaoComponente.SeqGrupoCurricularComponente);
                    ConfigurarSituacaoComponente(configuracaoComponente, componente);
                    var componenteGrupo = divisao.ConfiguracoesGrupos.FirstOrDefault(f => f.SeqComponenteCurricular == configuracaoComponente.Seq);
                    ConfigurarSituacaoComponente(configuracaoComponente, componenteGrupo);
                }
            }

            // Atualiza a situação dos componentes sem divisão
            var seqsComponentesSemDivisao = gruposTreeCurriculoACursar
                .Where(w => w.SeqComponenteCurricular.HasValue)
                .Select(s => s.SeqComponenteCurricular.Value)
                .ToArray();
            var specComponentesSemDivisao = new ComponenteCurricularFilterSpecification() { SeqComponentesCurriculares = seqsComponentesSemDivisao };
            var configuracaoComponentesSemDivisao = ComponenteCurricularDomainService.SearchProjectionBySpecification(specComponentesSemDivisao, p => new ConsultaDivisaoMatrizCurricularComponenteItemVO()
            {
                Seq = p.Seq,
                ExigeAssuntoComponente = p.ExigeAssuntoComponente,
                ContemComponenteSubstituto = false, // Configurado na divisão
                ContemRequisitos = p.Requisitos.Any()
            });
            foreach (var componente in gruposTreeCurriculoACursar)
            {
                var configuracaoComponente = configuracaoComponentesSemDivisao.FirstOrDefault(f => f.Seq == componente.SeqComponenteCurricular);
                ConfigurarSituacaoComponente(configuracaoComponente, componente);
            }

            return new ConsultaDivisoesMatrizCurricularVO()
            {
                SeqMatrizCurricular = seq,
                SeqCurriculoCursoOferta = consultaMatriz.SeqCurriculoCursoOferta,
                ComponentesACursar = gruposTreeCurriculoACursar.OrderBy(o => o.Descricao).ToArray(),
                DivisoesMatrizCurricular = divisoes
            };
        }

        /// <summary>
        /// Grava uma matriz curricular e suas divisões
        /// </summary>
        /// <param name="matrizCurricular">Dados da matriz curricular a ser gravada</param>
        /// <returns>Sequencial da matriz curricular gravada</returns>
        /// <exception cref="HistoricoSituacaoMatrizCurricularOfertaAtivaException">Caso já exista uma matriz curricular como "Ativa" para a mesma oferta de curso-localidade-turno RN_CUR_057</exception>
        /// <exception cref="MatrizCurricularEdicaoNaoPermitidaException">Caso seja para atualizar as divisões e tenha dependências cadastrada</exception>
        public long SalvarMatrizCurricular(MatrizCurricularVO matrizCurricularVo)
        {
            var matrizCurricular = matrizCurricularVo.Transform<MatrizCurricular>();
            bool atualizarDivisoes = true;
            bool matrizAtiva = false;

            // Caso seja uma atualização
            if (matrizCurricular.Seq != 0)
            {
                // Recupera a divisão e modalidade atuais e verifica se têm dependências
                var estadoAtualMatriz = this.SearchProjectionByKey(new SMCSeqSpecification<MatrizCurricular>(matrizCurricular.Seq), p => new
                {
                    p.SeqDivisaoCurricular,
                    ContemConfiguracaoComponente = p.ConfiguracoesComponente.Count() > 0,
                    ContemGrupos = p.DivisoesMatrizCurricular.Sum(s => s.ConfiguracoesGrupos.Count()) > 0,
                    p.QuantidadeMesesSolicitacaoProrrogacao,
                    p.CurriculoCursoOferta.Curriculo.Ativo
                });

                matrizAtiva = estadoAtualMatriz.Ativo;

                // Verifica se deve atualizar também as divisões
                atualizarDivisoes = matrizCurricular.SeqDivisaoCurricular != estadoAtualMatriz.SeqDivisaoCurricular;

                // Caso seja para atualizar as divisões e tenha dependências cadastradas, aborta o processo de atualização
                if (atualizarDivisoes && (estadoAtualMatriz.ContemConfiguracaoComponente || estadoAtualMatriz.ContemGrupos))
                    throw new MatrizCurricularEdicaoNaoPermitidaException();
            }

            if (atualizarDivisoes)
            {
                var specDivisaoCurricularItem = new DivisaoCurricularItemFilterSpecification() { SeqDivisaoCurricular = matrizCurricular.SeqDivisaoCurricular };
                var dividoesCurriculares = this.DivisaoCurricularItemDomainService.SearchBySpecification(specDivisaoCurricularItem);
                matrizCurricular.DivisoesMatrizCurricular = dividoesCurriculares.Select(s => new DivisaoMatrizCurricular()
                {
                    SeqMatrizCurricular = matrizCurricular.Seq,
                    SeqDivisaoCurricularItem = s.Seq
                }).ToList();
            }

            // Task 50128
            // Permitir alterar o prazo de abertura de prorrogação mesmo que exista matriz ativa.
            if (!matrizAtiva)
            {
                var cursoOfertaLocalidadeTurnoDomainService = this.CursoOfertaLocalidadeTurnoDomainService;
                foreach (var oferta in matrizCurricular.Ofertas)
                {
                    // Cria as situações apenas na primeira gravação
                    if (oferta.Seq == 0)
                    {
                        var dataInicioVigencia = matrizCurricularVo
                            .Ofertas
                            .First(s => s.SeqCursoOfertaLocalidade == oferta.CursoOfertaLocalidadeTurno.SeqCursoOfertaLocalidade
                                     && s.SeqCursoOfertaTurno == oferta.CursoOfertaLocalidadeTurno.SeqTurno)
                            .DataInicioVigencia;
                     
                        // Regra RN_CUR_057 não permite cadastrar situação ativa para mais de um curso oferta localidade turno
                        ValidarSituacaoAtiva(oferta.CursoOfertaLocalidadeTurno.SeqCursoOfertaLocalidade, oferta.CursoOfertaLocalidadeTurno.SeqTurno, dataInicioVigencia.Value);
                        
                        oferta.HistoricosSituacao = this.GerarSituacoesOferta(dataInicioVigencia.Value);
                    }

                    // Atualiza as referências para curso oferta localidade enquanto a atualização for permitida
                    oferta.SeqCursoOfertaLocalidadeTurno = cursoOfertaLocalidadeTurnoDomainService
                        .SearchProjectionByKey(new CursoOfertaLocalidadeTurnoFilterSpecification()
                        {
                            SeqCursoOfertaLocalidade = oferta.CursoOfertaLocalidadeTurno.SeqCursoOfertaLocalidade,
                            SeqTurno = oferta.CursoOfertaLocalidadeTurno.SeqTurno
                        },
                        p => p.Seq);
                }
            }
            else
            {
                // Se apenas prazo de abertura de prorrogação estiver sendo alterado, as ofertas não precisam ser atualizadas
                // Tirando ofertas do objeto para evitar erros em banco
                matrizCurricular.Ofertas = null;
            }

            this.SaveEntity(matrizCurricular);

            return matrizCurricular.Seq;
        }

        /// <summary>
        /// Gera o histórico de situações da matriz segundo a rega RN_CUR_017 e a data de início da vigência
        /// </summary>
        /// <param name="dataInicioVigenciaOferta">Data de início da vigência da oferta de matriz</param>
        /// <returns>Lista de histórico de situações de matriz curricular oferta</returns>
        public List<HistoricoSituacaoMatrizCurricularOferta> GerarSituacoesOferta(DateTime dataInicioVigenciaOferta)
        {
            var situiacoes = new List<HistoricoSituacaoMatrizCurricularOferta>();

            if (dataInicioVigenciaOferta > DateTime.Today)
            {
                situiacoes.Add(new HistoricoSituacaoMatrizCurricularOferta()
                {
                    DataInicio = DateTime.Today,
                    DataFim = dataInicioVigenciaOferta.AddDays(-1),
                    SituacaoMatrizCurricularOferta = SituacaoMatrizCurricularOferta.EmAtivacao
                });
            }

            situiacoes.Add(new HistoricoSituacaoMatrizCurricularOferta()
            {
                DataInicio = dataInicioVigenciaOferta,
                SituacaoMatrizCurricularOferta = SituacaoMatrizCurricularOferta.Ativa
            });

            return situiacoes;
        }

        /// <summary>
        /// Verifica se já existe situação de uma matriz curricular como "Ativa", para a mesma oferta de curso-localidade-turno atendendo a regra RN_CUR_057
        /// </summary>
        /// <param name="seqCursoOfertaLocalidade">Sequencial do curso oferta localidade</param>
        /// <param name="seqTurno">Sequencial do turno</param>
        /// <param name="dataInicio">Data em que ocorre a ativação da oferta</param>
        /// <exception cref="HistoricoSituacaoMatrizCurricularOfertaAtivaException">Caso já exista uma matriz curricular como "Ativa" para a mesma oferta de curso-localidade-turno RN_CUR_057</exception>
        public void ValidarSituacaoAtiva(long seqCursoOfertaLocalidade, long seqTurno, DateTime dataInicio)
        {
            var filtroOferta = new MatrizCurricularOfertaFiltroVO() { SeqCursoOfertaLocalidade = seqCursoOfertaLocalidade, SeqTurno = seqTurno, DataAtivacaoMatriz = dataInicio };

            var outrasOfertas = MatrizCurricularOfertaDomainService.BuscarMatrizesCurricularesOfertas(filtroOferta);

            if (outrasOfertas.Count > 0)
            {
                throw new HistoricoSituacaoMatrizCurricularOfertaAtivaException();
            }
        }

        /// <summary>
        /// Calcula os valores em créditos e horas aula das divisões de uma matriz curricular
        /// </summary>
        /// <param name="seq">Sequencial da matriz curricular</param>
        /// <returns>Lista com os valores das divisões da matriz</returns>
        public List<DivisaoMatrizCurricularValorVO> BuscarValoresDivisaoMatrizCurricular(long seq)
        {
            var spec = new SMCSeqSpecification<MatrizCurricular>(seq);
            var matriz = SearchProjectionByKey(spec, p => new
            {
                // Configuração de divisão dos grupos
                DivisoesGrupo = p.DivisoesMatrizCurricular.SelectMany(d => d.ConfiguracoesGrupos.Select(g => new
                {
                    g.DivisaoMatrizCurricular.DivisaoCurricularItem.Numero,
                    g.QuantidadeCreditos,
                    g.QuantidadeHoraAula
                })),
                // Configuração de divisão dos componentes (utilizado caso o grupo não seja configurado diretamente)
                DivisoesComponente = p.DivisoesMatrizCurricular.SelectMany(d => d.ConfiguracoesComponentes.Select(c => new
                {
                    c.DivisaoMatrizCurricular.DivisaoCurricularItem.Numero,
                    c.ConfiguracaoComponente.ComponenteCurricular.Credito,
                    c.ConfiguracaoComponente.ComponenteCurricular.CargaHoraria,

                    c.GrupoCurricularComponente.SeqGrupoCurricular,
                    c.GrupoCurricularComponente.GrupoCurricular.TipoConfiguracaoGrupoCurricular.ExigeFormato,
                    c.GrupoCurricularComponente.GrupoCurricular.QuantidadeCreditos,
                    c.GrupoCurricularComponente.GrupoCurricular.QuantidadeHoraAula,
                })),
            });

            // Soma os valores configurados para divisões de grupos
            var valoresDivisoes = new Dictionary<short, DivisaoMatrizCurricularValorVO>();
            foreach (var divisaoGrupo in matriz.DivisoesGrupo)
            {
                if (!valoresDivisoes.ContainsKey(divisaoGrupo.Numero))
                    valoresDivisoes.Add(divisaoGrupo.Numero, new DivisaoMatrizCurricularValorVO());
                valoresDivisoes[divisaoGrupo.Numero].HorasAula += divisaoGrupo.QuantidadeHoraAula.GetValueOrDefault();
                valoresDivisoes[divisaoGrupo.Numero].Creditos += divisaoGrupo.QuantidadeCreditos.GetValueOrDefault();
            }

            // Soma os valores configurados para os componentes associadoas às divisões
            // Soma os valores configurados nos componentes, onde o tipo do grupo não exige formato
            foreach (var divisaoComponente in matriz.DivisoesComponente.Where(w => !w.ExigeFormato))
            {
                if (!valoresDivisoes.ContainsKey(divisaoComponente.Numero))
                    valoresDivisoes.Add(divisaoComponente.Numero, new DivisaoMatrizCurricularValorVO());
                valoresDivisoes[divisaoComponente.Numero].HorasAula += divisaoComponente.CargaHoraria.GetValueOrDefault();
                valoresDivisoes[divisaoComponente.Numero].Creditos += divisaoComponente.Credito.GetValueOrDefault();
            }

            // Soma os valores configurados nos grupos dos componentes, onde os grupos exigem formato e os componentes foram associados diretamente na divisão
            // Considerando apenas uma vez cada grupo
            foreach (var grupoDivisaoComponente in matriz.DivisoesComponente.Where(w => w.ExigeFormato).GroupBy(g => g.SeqGrupoCurricular))
            {
                var divisaoComponente = grupoDivisaoComponente.First();
                if (!valoresDivisoes.ContainsKey(divisaoComponente.Numero))
                    valoresDivisoes.Add(divisaoComponente.Numero, new DivisaoMatrizCurricularValorVO());
                valoresDivisoes[divisaoComponente.Numero].HorasAula += divisaoComponente.QuantidadeHoraAula.GetValueOrDefault();
                valoresDivisoes[divisaoComponente.Numero].Creditos += divisaoComponente.QuantidadeCreditos.GetValueOrDefault();
            }
            return valoresDivisoes.Values.ToList();
        }

        /// <summary>
        /// Recupera a matriz curricular de um aluno num ciclo letivo
        /// </summary>
        /// <param name="seqAluno">Sequencial do aluno</param>
        /// <param name="seqCicloLetivo">Sequencial do ciclo letivo</param>
        /// <param name="considerarMatriz">Considerar a matriz do aluno</param>
        /// <returns>Retorna a matriz do aluno ou nulo caso não seja encontrada uma matriz para o ciclo e aluno informados ou seja informado para não considerar a matriz</returns>
        public MatrizCurricular BuscarMatrizCurricularAluno(long seqAluno, long seqCicloLetivo, bool considerarMatriz)
        {
            if (!considerarMatriz)
                return null;

            return AlunoDomainService.SearchProjectionByKey(new SMCSeqSpecification<Aluno>(seqAluno), p => p
                .Historicos.FirstOrDefault(f => f.Atual)
                .HistoricosCicloLetivo.FirstOrDefault(f => f.SeqCicloLetivo == seqCicloLetivo)
                .PlanosEstudo.OrderByDescending(o => o.Seq).FirstOrDefault()
                .MatrizCurricularOferta.MatrizCurricular);
        }

        /// <summary>
        /// Soma todos os grupos curriculares configurados nas divisões da matriz
        /// </summary>
        /// <param name="seq">Sequencial da matriz curricualr oferta</param>
        /// <returns>Valor total dos grupos configurados na matriz</returns>
        public CurriculoCursoOfertaGrupoValorVO BuscarValoresGruposMatriz(long seq, bool considerarCargaHoraria = false)
        {
            // Recupera das divisões da matriz os grupos configurados e grupos dos componentes configurados
            var matriz = this.SearchProjectionByKey(seq, p => new
            {
                p.SeqCurriculoCursoOferta,
                Divisoes = p.DivisoesMatrizCurricular.Select(d => new
                {
                    SeqsGruposDiretos = d.ConfiguracoesGrupos.Select(s => s.CurriculoCursoOfertaGrupo.SeqGrupoCurricular),
                    SeqsGruposComponentes = d.ConfiguracoesComponentes.Select(s => s.GrupoCurricularComponente.SeqGrupoCurricular)
                })
            });
            var seqsGruposCurricularesMatriz = matriz.Divisoes.SelectMany(d => d.SeqsGruposDiretos.Union(d.SeqsGruposComponentes)).Distinct().ToList();

            // Recupera todos grupos curriculares e componentes associados ao currículo
            var gruposCurricularesComponentes = GrupoCurricularDomainService.BuscarGruposCurricularesTreeCurriculoCursoOferta(matriz.SeqCurriculoCursoOferta).ToList();

            // Recupera os sequenciais dos grupos associados à matriz e seus superiores
            var gruposMatriz = new List<GrupoCurricularListaVO>();
            var seqGruposNivelAtual = seqsGruposCurricularesMatriz;
            do
            {
                var seqsGruposSuperiores = gruposCurricularesComponentes.Where(w =>
                    w.SeqGrupoCurricularSuperior.HasValue && seqGruposNivelAtual.Contains(w.SeqGrupoCurricular.GetValueOrDefault()))
                    .Select(s => s.SeqGrupoCurricularSuperior.Value)
                    .Distinct()
                    .ToList();
                seqsGruposCurricularesMatriz.AddRange(seqsGruposSuperiores);
                seqGruposNivelAtual = seqsGruposSuperiores;
            } while (seqGruposNivelAtual.Any());

            // Filtra os grupos associados e seus superiores
            gruposCurricularesComponentes = gruposCurricularesComponentes.Where(w => seqsGruposCurricularesMatriz.Contains(w.SeqGrupoCurricular.GetValueOrDefault()) || seqsGruposCurricularesMatriz.Contains(w.SeqGrupoCurricularSuperior.GetValueOrDefault())).ToList();

            // Soma os valores dos grupos e componenetes
            CurriculoCursoOfertaGrupoValorVO quantidadeTotal = CurriculoCursoOfertaGrupoDomainService
                .SomarQuantidadesCurriculoCursoOfertaGrupo(gruposCurricularesComponentes, considerarCargaHoraria);

            return quantidadeTotal;
        }

        /// <summary>
        /// Recupera a hierarquia de grupos de componentes com todos grupos pais, grupos filhos e componentes dos grupos filhos dos sequenciais informados
        /// </summary>
        /// <param name="gruposCurriculo">Hierarquia de grupos e componentes do currículo</param>
        /// <param name="seqsGruposDivisao">Sequenciais dos grupos associados à divisão</param>
        /// <returns>Grupos com os sequenciais informados, hierarquia de grupos pais dos grupos informados e grupos filhos dos informados com seus componentes</returns>
        private GrupoCurricularListaVO[] RecuperarHierarquiaGrupoDivisao(IEnumerable<GrupoCurricularListaVO> gruposCurriculo, IEnumerable<long> seqsGruposDivisao)
        {
            var gruposDivisao = gruposCurriculo.Where(w => seqsGruposDivisao.Contains(w.SeqGrupoCurricular ?? 0));
            var gruposFilhos = RecuperarFilhos(gruposCurriculo, gruposDivisao);
            var gruposPais = RecuperarPais(gruposCurriculo, gruposDivisao).Except(gruposDivisao);

            return gruposPais.Union(gruposFilhos).ToArray();
        }

        private IEnumerable<T> RecuperarFilhos<T>(IEnumerable<T> hierarquia, IEnumerable<T> pais) where T : ISMCTreeNode
        {
            if (!pais.Any())
                return Enumerable.Empty<T>();
            var nodesFilhos = hierarquia.Where(w => pais.Any(a => a.Seq == w.SeqPai));
            return RecuperarFilhos(hierarquia, nodesFilhos).Union(pais);
        }

        private IEnumerable<T> RecuperarPais<T>(IEnumerable<T> hierarquia, IEnumerable<T> filhos) where T : ISMCTreeNode
        {
            if (!filhos.Any())
                return Enumerable.Empty<T>();
            var nodesPais = hierarquia.Where(w => filhos.Any(a => a.SeqPai == w.Seq));
            return RecuperarPais(hierarquia, nodesPais).Union(filhos);
        }

        /// <summary>
        /// Configura a situação de um componente curricular
        /// </summary>
        /// <param name="configuracaoComponete">Configuração do componente na divisão de matriz</param>
        /// <param name="componente">Componente a ser atualizado</param>
        private static void ConfigurarSituacaoComponente(ConsultaDivisaoMatrizCurricularComponenteItemVO configuracaoComponete, GrupoCurricularListaVO componente)
        {
            if (configuracaoComponete != null && componente != null)
            {
                if (configuracaoComponete.ContemRequisitos)
                    componente.SituacaoComponente = SituacaoConfiguracaoComponenteCurricular.RequisitoCadastrado;
                else if (configuracaoComponete.ContemComponenteSubstituto)
                    componente.SituacaoComponente = SituacaoConfiguracaoComponenteCurricular.AssuntoCadastrado;
                else if (configuracaoComponete.ExigeAssuntoComponente.HasValue && configuracaoComponete.ExigeAssuntoComponente.Value)
                    componente.SituacaoComponente = SituacaoConfiguracaoComponenteCurricular.ExigeAssunto;
                else
                    componente.SituacaoComponente = SituacaoConfiguracaoComponenteCurricular.Nenhum;
            }
        }
    }
}