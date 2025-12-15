using SMC.Academico.Common.Areas.CUR.Enums;
using SMC.Academico.Common.Areas.CUR.Exceptions;
using SMC.Academico.Domain.Areas.CUR.Models;
using SMC.Academico.Domain.Areas.CUR.Specifications;
using SMC.Framework.Domain;
using SMC.Framework.Model;
using SMC.Framework.Specification;
using SMC.Framework.Util;
using SMC.Framework.UnitOfWork;
using System.Collections.Generic;
using System.Linq;
using SMC.Academico.Domain.Areas.CUR.ValueObjects;
using SMC.Framework.Extensions;

namespace SMC.Academico.Domain.Areas.CUR.DomainServices
{
    public class HistoricoSituacaoMatrizCurricularOfertaDomainService : AcademicoContextDomain<HistoricoSituacaoMatrizCurricularOferta>
    {

        #region [ DomainService ]

        private MatrizCurricularOfertaDomainService MatrizCurricularOfertaDomainService
        {
            get { return this.Create<MatrizCurricularOfertaDomainService>(); }
        }

        #endregion

        /// <summary>
        /// Lista as opções de situação da matriz curricular de acordo com a regra RN_CUR_057 Cadastro/alteração situação matriz curricular
        /// Em ativação <-> Ativa <-> Em extinção <-> Extinta
        /// </summary>
        /// <param name="seqMatrizCurricularOferta">Sequencial da matriz curricular oferta</param>
        /// <returns>Lista de opções da situação</returns>
        public List<SMCDatasourceItem> SituacoesMatrizCurricularOferta(long seqMatrizCurricularOferta)
        {
            var filtro = new HistoricoSituacaoMatrizCurricularOfertaFilterSpecification() { SeqMatrizCurricularOferta = seqMatrizCurricularOferta };
            var registros = this.SearchBySpecification(filtro).ToList();
            var retorno = new List<SMCDatasourceItem>();
            var item = new SMCDatasourceItem();

            if (registros.Count == 0)
            {
                item.Seq = (long)SituacaoMatrizCurricularOferta.EmAtivacao;
                item.Descricao = SMCEnumHelper.GetDescription(SituacaoMatrizCurricularOferta.EmAtivacao);
                retorno.Add(item);
            }
            else
            {
                var ultimoRegistro = registros.Where(w => w.DataFim == null).FirstOrDefault();
                if (ultimoRegistro == null)
                    ultimoRegistro = registros.First();

                switch (ultimoRegistro.SituacaoMatrizCurricularOferta)
                {
                    case SituacaoMatrizCurricularOferta.Nenhum:
                        item.Seq = (long)SituacaoMatrizCurricularOferta.EmAtivacao;
                        item.Descricao = SMCEnumHelper.GetDescription(SituacaoMatrizCurricularOferta.EmAtivacao);
                        retorno.Add(item);
                        break;
                    case SituacaoMatrizCurricularOferta.EmAtivacao:
                        item.Seq = (long)SituacaoMatrizCurricularOferta.Ativa;
                        item.Descricao = SMCEnumHelper.GetDescription(SituacaoMatrizCurricularOferta.Ativa);
                        retorno.Add(item);
                        break;
                    case SituacaoMatrizCurricularOferta.Ativa:
                        item.Seq = (long)SituacaoMatrizCurricularOferta.EmAtivacao;
                        item.Descricao = SMCEnumHelper.GetDescription(SituacaoMatrizCurricularOferta.EmAtivacao);
                        retorno.Add(item);
                        item = new SMCDatasourceItem();
                        item.Seq = (long)SituacaoMatrizCurricularOferta.EmExtincao;
                        item.Descricao = SMCEnumHelper.GetDescription(SituacaoMatrizCurricularOferta.EmExtincao);
                        retorno.Add(item);
                        break;
                    case SituacaoMatrizCurricularOferta.EmExtincao:
                        item.Seq = (long)SituacaoMatrizCurricularOferta.Ativa;
                        item.Descricao = SMCEnumHelper.GetDescription(SituacaoMatrizCurricularOferta.Ativa);
                        retorno.Add(item);
                        item = new SMCDatasourceItem();
                        item.Seq = (long)SituacaoMatrizCurricularOferta.Extinta;
                        item.Descricao = SMCEnumHelper.GetDescription(SituacaoMatrizCurricularOferta.Extinta);
                        retorno.Add(item);
                        break;
                    case SituacaoMatrizCurricularOferta.Extinta:
                        item.Seq = (long)SituacaoMatrizCurricularOferta.EmExtincao;
                        item.Descricao = SMCEnumHelper.GetDescription(SituacaoMatrizCurricularOferta.EmExtincao);
                        retorno.Add(item);
                        break;
                    default:
                        break;
                }
            }
            return retorno;
        }

        /// <summary>
        /// Busca os históricos de situações matriz curricular oferta acordo com os filtros
        /// </summary>
        /// <param name="filtros">Objeto históricos filtro</param>
        /// <returns>SMCPagerData com a lista de históricos</returns>
        public SMCPagerData<HistoricoSituacaoMatrizCurricularOfertaVO> BuscarHistoricosSituacoesMatrizCurricularOferta(HistoricoSituacaoMatrizCurricularOfertaFilterSpecification filtros)
        {
            int total = 0;

            var historicos = this.SearchBySpecification(filtros,out total).TransformList<HistoricoSituacaoMatrizCurricularOfertaVO>();

            if (historicos.Count > 0)
                historicos.First().PrimeiroRegistro = true;

            return new SMCPagerData<HistoricoSituacaoMatrizCurricularOfertaVO>(historicos, total);
        }

        /// <summary>
        /// Salva uma nova situação da matriz curricular e edita a anterior preenchendo a data final
        /// </summary>
        /// <param name="historicoSituacaoMatrizCurricularOferta">Dados do registro a ser gravado</param>
        /// <returns>Sequencial do registro gravado</returns>
        public long SalvarHistoricoSituacaoMatrizCurricularOferta(HistoricoSituacaoMatrizCurricularOferta historicoSituacaoMatrizCurricularOferta)
        {
            var specMatriz = new HistoricoSituacaoMatrizCurricularOfertaFilterSpecification() { SeqMatrizCurricularOferta = historicoSituacaoMatrizCurricularOferta.SeqMatrizCurricularOferta };
            specMatriz.SetOrderByDescending(o => o.DataInicio);

            var ultimoRegistro = this.SearchBySpecification(specMatriz).ToList();

            if (ultimoRegistro.Count > 0)
            {
                switch (ultimoRegistro[0].SituacaoMatrizCurricularOferta)
                {
                    case SituacaoMatrizCurricularOferta.Nenhum:
                        if (historicoSituacaoMatrizCurricularOferta.SituacaoMatrizCurricularOferta != SituacaoMatrizCurricularOferta.EmAtivacao)
                            throw new HistoricoSituacaoMatrizCurricularOfertaSituacaoException();
                        break;
                    case SituacaoMatrizCurricularOferta.EmAtivacao:
                        if (historicoSituacaoMatrizCurricularOferta.SituacaoMatrizCurricularOferta != SituacaoMatrizCurricularOferta.Ativa)
                            throw new HistoricoSituacaoMatrizCurricularOfertaSituacaoException();
                        break;
                    case SituacaoMatrizCurricularOferta.Ativa:
                        if (historicoSituacaoMatrizCurricularOferta.SituacaoMatrizCurricularOferta != SituacaoMatrizCurricularOferta.EmAtivacao
                          && historicoSituacaoMatrizCurricularOferta.SituacaoMatrizCurricularOferta != SituacaoMatrizCurricularOferta.EmExtincao)
                            throw new HistoricoSituacaoMatrizCurricularOfertaSituacaoException();
                        break;
                    case SituacaoMatrizCurricularOferta.EmExtincao:
                        if (historicoSituacaoMatrizCurricularOferta.SituacaoMatrizCurricularOferta != SituacaoMatrizCurricularOferta.Ativa
                         && historicoSituacaoMatrizCurricularOferta.SituacaoMatrizCurricularOferta != SituacaoMatrizCurricularOferta.Extinta)
                            throw new HistoricoSituacaoMatrizCurricularOfertaSituacaoException();
                        break;
                    case SituacaoMatrizCurricularOferta.Extinta:
                        if (historicoSituacaoMatrizCurricularOferta.SituacaoMatrizCurricularOferta != SituacaoMatrizCurricularOferta.EmExtincao)
                            throw new HistoricoSituacaoMatrizCurricularOfertaSituacaoException();
                        break;
                    default:
                        break;
                }

                if (ultimoRegistro[0].DataInicio <= historicoSituacaoMatrizCurricularOferta.DataInicio.AddDays(-1))
                {
                    ultimoRegistro[0].DataFim = historicoSituacaoMatrizCurricularOferta.DataInicio.AddDays(-1);

                    this.SaveEntity(ultimoRegistro[0]);
                }
                else
                {
                    throw new HistoricoSituacaoMatrizCurricularOfertaDataFimException();
                }
            }

            if (historicoSituacaoMatrizCurricularOferta.SituacaoMatrizCurricularOferta == SituacaoMatrizCurricularOferta.Ativa)
                ValidarSituacaoAtiva(historicoSituacaoMatrizCurricularOferta);

            this.SaveEntity(historicoSituacaoMatrizCurricularOferta);

            return historicoSituacaoMatrizCurricularOferta.Seq;
        }

        /// <summary>
        /// Exclui ó último histórico situação da matriz curricular e edita a anterior preenchendo a data final como null
        /// </summary>
        /// <param name="seq">Sequencial do registro</param>
        public void ExcluirHistoricoSituacaoMatrizCurricularOferta(long seq)
        {
            // Realiza a atualização dentro de uma transação
            using (var tran = SMCUnitOfWork.Begin())
            {

                var registro = this.SearchByKey(new SMCSeqSpecification<HistoricoSituacaoMatrizCurricularOferta>(seq));

                HistoricoSituacaoMatrizCurricularOfertaFilterSpecification filtro = new HistoricoSituacaoMatrizCurricularOfertaFilterSpecification() { SeqMatrizCurricularOferta = registro.SeqMatrizCurricularOferta };

                this.DeleteEntity(registro);

                var registrosRestante = this.SearchBySpecification(filtro).ToList();

                if (registrosRestante.Count > 0)
                {
                    var registroMaiorDataFim = registrosRestante.OrderByDescending(o => o.DataFim).First();
                    registroMaiorDataFim.DataFim = null;

                    if (registroMaiorDataFim.SituacaoMatrizCurricularOferta == SituacaoMatrizCurricularOferta.Ativa)
                        ValidarSituacaoAtiva(registroMaiorDataFim);

                    this.SaveEntity(registroMaiorDataFim);
                }

                // Fecha a transação
                tran.Commit();
            }
        }

        /// <summary>
        /// Verifica se já existe situação de uma matriz curricular como "Ativa", para a mesma oferta de curso-localidade-turno
        /// </summary>
        /// <param name="historicoSituacaoMatrizCurricularOferta">Registro com a situação para gravar no histórico</param>
        public void ValidarSituacaoAtiva(HistoricoSituacaoMatrizCurricularOferta historicoSituacaoMatrizCurricularOferta)
        {
            var registroOferta = MatrizCurricularOfertaDomainService.BuscarMatrizCurricularOferta(historicoSituacaoMatrizCurricularOferta.SeqMatrizCurricularOferta);

            var filtroOferta = new MatrizCurricularOfertaFiltroVO() { SeqCursoOfertaLocalidadeTurno = registroOferta.SeqCursoOfertaLocalidadeTurno, DataHistorico = historicoSituacaoMatrizCurricularOferta.DataInicio };

            var outrasOfertas = MatrizCurricularOfertaDomainService.BuscarMatrizesCurricularesOfertas(filtroOferta);

            if (outrasOfertas.Count > 1)
            {
                if (outrasOfertas.Where(w => w.Seq != registroOferta.Seq && w.HistoricoSituacaoAtual == SituacaoMatrizCurricularOferta.Ativa).Count() > 0)
                {
                    throw new HistoricoSituacaoMatrizCurricularOfertaAtivaException();
                }
            }
        }

    }
}
