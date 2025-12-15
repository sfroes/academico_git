using SMC.Academico.Common.Areas.CUR.Enums;
using SMC.Academico.Common.Areas.CUR.Exceptions;
using SMC.Academico.Common.Areas.CUR.Includes;
using SMC.Academico.Common.Areas.Shared.Helpers;
using SMC.Academico.Domain.Areas.CUR.Models;
using SMC.Academico.Domain.Areas.CUR.Specifications;
using SMC.Academico.Domain.Areas.CUR.ValueObjects;
using SMC.Academico.Domain.Areas.SRC.DomainServices;
using SMC.Academico.Domain.Areas.SRC.Specifications;
using SMC.Framework;
using SMC.Framework.Extensions;
using SMC.Framework.Model;
using SMC.Framework.Specification;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SMC.Academico.Domain.Areas.CUR.DomainServices
{
    public class DispensaDomainService : AcademicoContextDomain<Dispensa>
    {
        #region [ DomainService ]

        private SolicitacaoDispensaGrupoDomainService SolicitacaoDispensaGrupoDomainService => Create<SolicitacaoDispensaGrupoDomainService>();

        #endregion [ DomainService ]

        /// <summary>
        /// Busca uma dispensa com seus respectivos grupos de componentes
        /// Estruturados de acordo com as definições de tela
        /// </summary>
        /// <param name="seq">Sequencial da dispensa</param>
        /// <returns>Objeto dispensa com seus detalhes</returns>
        public DispensaVO BuscarDispensa(long seq)
        {
            var includes = IncludesDispensa.GrupoOrigem_Componentes_ComponenteCurricular |
                           IncludesDispensa.GrupoDispensado_Componentes_ComponenteCurricular |
                           IncludesDispensa.HistoricosVigencia |
                           IncludesDispensa.MatrizesExcecao;

            Dispensa registro = this.SearchByKey(new SMCSeqSpecification<Dispensa>(seq), includes);

            DispensaVO retorno = registro.Transform<DispensaVO>();

            retorno.GrupoOrigens = registro.GrupoOrigem.Componentes.Select(s => s.ComponenteCurricular).TransformList<DispensaComponenteVO>();
            retorno.GrupoDispensados = registro.GrupoDispensado.Componentes.Select(s => s.ComponenteCurricular).TransformList<DispensaComponenteVO>();
            retorno.HistoricosVigencia = registro.HistoricosVigencia.TransformList<DispensaHistoricoVigenciaVO>();
            retorno.MatrizesExcecao = registro.MatrizesExcecao.TransformList<DispensaMatrizExcecaoVO>();

            return retorno;
        }

        /// <summary>
        /// Busca as dispensas que atendam os filtros informados e com a paginação correta
        /// </summary>
        /// <param name="filtros">Filtros da listagem de dispensas</param>
        /// <returns>SMCPagerData com a lista de dispensas</returns>
        public SMCPagerData<DispensaVO> BuscarDispensas(DispensaFilterSpecification filtros)
        {
            int total;
            var includes = IncludesDispensa.GrupoOrigem_Componentes_ComponenteCurricular |
                           IncludesDispensa.GrupoDispensado_Componentes_ComponenteCurricular |
                           IncludesDispensa.HistoricosVigencia |
                           IncludesDispensa.MatrizesExcecao;

            var registros = this.SearchBySpecification(filtros, out total, includes);

            var dispensasVO = new List<DispensaVO>();
            foreach (var item in registros)
            {
                DispensaVO itemVO = item.Transform<DispensaVO>();

                itemVO.GrupoOrigens = item.GrupoOrigem.Componentes.Select(s => s.ComponenteCurricular).TransformList<DispensaComponenteVO>();
                itemVO.GrupoDispensados = item.GrupoDispensado.Componentes.Select(s => s.ComponenteCurricular).TransformList<DispensaComponenteVO>();
                itemVO.HistoricosVigencia = item.HistoricosVigencia.TransformList<DispensaHistoricoVigenciaVO>();
                itemVO.MatrizesExcecao = item.MatrizesExcecao.TransformList<DispensaMatrizExcecaoVO>();
                itemVO.Associado = itemVO.MatrizesExcecao.Count > 0 ? MatrizExcecaoDispensa.MatrizAssociado : MatrizExcecaoDispensa.SemMatrizAssociado;
                dispensasVO.Add(itemVO);
            }

            return new SMCPagerData<DispensaVO>(dispensasVO, total);
        }

        /// <summary>
        /// Grava uma dispensa com seus respectivos grupos de componentes
        /// </summary>
        /// <param name="dispensa">Dados da dispensa a ser gravado</param>
        /// <returns>Sequencial da dispensa gravado</returns>
        public long SalvarDispensa(DispensaVO dispensa)
        {
            Dispensa registro = new Dispensa();

            //Validação de listas vazias
            if (!dispensa.GrupoOrigens.SMCAny() || !dispensa.GrupoDispensados.SMCAny())
                throw new DispensaListaVaziaException();

            //Validação de mesmo registro nas duas listas
            if (dispensa.GrupoOrigens.Any(a => dispensa.GrupoDispensados.Select(s => s.Seq).Contains(a.Seq)))
                throw new DispensaListaDuplicadoException();

            if (!ValidarDispensaItensGrupos(dispensa))
                throw new DispensaListaItensDuplicadosException();

            if (!ValidacaoData.ValidarSobreposicaoPeriodos<DispensaHistoricoVigenciaVO>(dispensa.HistoricosVigencia, nameof(DispensaHistoricoVigenciaVO.DataInicioVigencia), nameof(DispensaHistoricoVigenciaVO.DataFimVigencia)))
                throw new DispensaListaDataPeriodoException();

            //Se for inclusão associa a matriz curricular do parâmetro
            if (dispensa.Seq == 0)
            {
                registro.GrupoOrigem = new DispensaGrupo();
                registro.GrupoDispensado = new DispensaGrupo();
                registro.GrupoOrigem.Componentes = new List<DispensaGrupoComponente>();
                dispensa.GrupoOrigens.ForEach(f => registro.GrupoOrigem.Componentes.Add(new DispensaGrupoComponente() { SeqComponenteCurricular = f.Seq }));
                registro.GrupoDispensado.Componentes = new List<DispensaGrupoComponente>();
                dispensa.GrupoDispensados.ForEach(f => registro.GrupoDispensado.Componentes.Add(new DispensaGrupoComponente() { SeqComponenteCurricular = f.Seq }));
                registro.HistoricosVigencia = dispensa.HistoricosVigencia.TransformList<DispensaHistoricoVigencia>();
                registro.ModoExibicaoHistoricoEscolar = dispensa.ModoExibicaoHistoricoEscolar;
            }
            else
            {
                registro = this.SearchByKey(new SMCSeqSpecification<Dispensa>(dispensa.Seq), IncludesDispensa.GrupoOrigem_Componentes_ComponenteCurricular |
                                                                                             IncludesDispensa.GrupoDispensado_Componentes_ComponenteCurricular |
                                                                                             IncludesDispensa.HistoricosVigencia | IncludesDispensa.MatrizesExcecao);
                var alterado = false;

                //Lista de componentes selecionado na origem
                var idsBancoOrigem = registro.GrupoOrigem.Componentes.Select(s => s.SeqComponenteCurricular);

                //Incluir os registros novos na lista
                dispensa.GrupoOrigens.Where(w => !idsBancoOrigem.Contains(w.Seq))
                                     .ToList()
                                     .ForEach(f => { registro.GrupoOrigem.Componentes.Add(new DispensaGrupoComponente() { SeqComponenteCurricular = f.Seq }); alterado = true; });

                //Remove os registros na lista
                registro.GrupoOrigem.Componentes.Where(w => !dispensa.GrupoOrigens.Any(a => a.Seq == w.SeqComponenteCurricular))
                                    .ToList()
                                    .ForEach(f => { registro.GrupoOrigem.Componentes.Remove(f); alterado = true; });

                //Lista de componentes selecionado na dispensa
                var idsBancoDispensa = registro.GrupoDispensado.Componentes.Select(s => s.SeqComponenteCurricular);

                //Incluir os registros novos na lista
                dispensa.GrupoDispensados.Where(w => !idsBancoDispensa.Contains(w.Seq))
                                     .ToList()
                                     .ForEach(f => { registro.GrupoDispensado.Componentes.Add(new DispensaGrupoComponente() { SeqComponenteCurricular = f.Seq }); alterado = true; });

                //Remove os registros na lista
                registro.GrupoDispensado.Componentes.Where(w => !dispensa.GrupoDispensados.Any(a => a.Seq == w.SeqComponenteCurricular))
                                    .ToList()
                                    .ForEach(f => { registro.GrupoDispensado.Componentes.Remove(f); alterado = true; });

                registro.HistoricosVigencia = dispensa.HistoricosVigencia.TransformList<DispensaHistoricoVigencia>();
                alterado |= registro.ModoExibicaoHistoricoEscolar != dispensa.ModoExibicaoHistoricoEscolar;
                registro.ModoExibicaoHistoricoEscolar = dispensa.ModoExibicaoHistoricoEscolar;

                if (!ValidarAssociacaoSolicitacaoServico(dispensa.Seq))
                {
                    if (alterado)
                        throw new DispensaSolicitacaoServicoAssociadaException();

                    if (!dispensa.HistoricosVigencia.SMCAny(a => a.DataFimVigencia is null || a.DataFimVigencia > DateTime.Today))
                        throw new DispensaDataFimInvalidaException();
                }
            }

            this.SaveEntity(registro);

            return registro.Seq;
        }

        /// <summary>
        /// Grava uma dispensa com suas matrizes de exceção
        /// </summary>
        /// <param name="dispensa">Dados da dispensa a ser gravado</param>
        /// <returns>Sequencial da dispensa gravado</returns>
        public long SalvarDispensaMatriz(DispensaVO dispensa)
        {
            Dispensa registro = new Dispensa();

            registro = this.SearchByKey(new SMCSeqSpecification<Dispensa>(dispensa.Seq), IncludesDispensa.GrupoOrigem_Componentes_ComponenteCurricular |
                                                                                         IncludesDispensa.GrupoDispensado_Componentes_ComponenteCurricular |
                                                                                         IncludesDispensa.HistoricosVigencia | IncludesDispensa.MatrizesExcecao);

            var dataInicioRegistro = registro.HistoricosVigencia.Min(m => m.DataInicioVigencia);
            var dataFimRegistro = registro.HistoricosVigencia.Max(m => m.DataFimVigencia);

            if (dispensa.MatrizesExcecao.Count(c => c.DataInicioExcecao < dataInicioRegistro || (dataFimRegistro != null && c.DataFimExcecao > dataFimRegistro)) > 0)
                throw new DispensaListaDataMatrizExcecaoException();

            registro.MatrizesExcecao = dispensa.MatrizesExcecao.TransformList<DispensaMatrizExcecao>();

            this.SaveEntity(registro);

            return registro.Seq;
        }

        /// <summary>
        /// Valida se ja existe uma dispensa com todos os itens iguais tanto na origem quanto nos dispensados
        /// </summary>
        /// <param name="dispensaVO">Dados da dispensa a ser gravado</param>
        /// <returns>Retorna se o registro esta valido</returns>
        public bool ValidarDispensaItensGrupos(DispensaVO dispensaVO)
        {
            DispensaFilterSpecification specQuantidade = new DispensaFilterSpecification()
            {
                QuantidadeGrupoOrigem = dispensaVO.GrupoOrigens.Count,
                QuantidadeGrupoDispensado = dispensaVO.GrupoDispensados.Count
            };

            SMCAndSpecification<Dispensa> specFinal = new SMCAndSpecification<Dispensa>(new DispensaFilterSpecification(), specQuantidade);

            foreach (var item in dispensaVO.GrupoOrigens)
            {
                DispensaFilterSpecification specItem = new DispensaFilterSpecification() { SeqComponenteCurricularOrigem = item.Seq };

                specFinal = new SMCAndSpecification<Dispensa>(specFinal, specItem);
            }

            foreach (var item in dispensaVO.GrupoDispensados)
            {
                DispensaFilterSpecification specItem = new DispensaFilterSpecification() { SeqComponenteCurricularDispensado = item.Seq };

                specFinal = new SMCAndSpecification<Dispensa>(specFinal, specItem);
            }

            var dispensaBanco = this.SearchProjectionBySpecification(specFinal, p => p.Seq);

            List<long> retornoValidacao = dispensaBanco.Where(w => w != dispensaVO.Seq).ToList();

            return retornoValidacao.Count() == 0;
        }

        /// <summary>
        /// (RN_SRC_088)
        /// Recupera uma dispensa (equivalencia) que tenha os mesmos componentes das listas informadas (cursados e dispensados),
        /// que não tenha a matriz informada como exceção e que esteja válida (período de vigencia ativo)
        /// </summary>
        /// <param name="seqsComponentesCursados">Sequencial dos componentes cursados</param>
        /// <param name="seqsComponentesDispensa">Sequencial dos componentes a serem dispensados</param>
        /// <param name="seqMatrizCurricularOferta">Sequencial da matriz curricular que não pode estar na exceção</param>
        /// <returns>Informações da dispensa equivalente</returns>
        public (long? SeqDispensa, ModoExibicaoHistoricoEscolar? ModoExibicao) BuscarDispensaEquivalente(List<long> seqsComponentesCursados, List<long> seqsComponentesDispensa, long seqMatrizCurricularOferta)
        {
            // Se não informou itens cursados, retorna NULL
            if (seqsComponentesCursados == null || seqsComponentesCursados.Count == 0)
                return (null, null);

            // Se não informou itens dispensados, retorna NULL
            if (seqsComponentesDispensa == null || seqsComponentesDispensa.Count == 0)
                return (null, null);

            // Busca todas as dispensas (equivalencias) que possuem os seqs componentes cursados e dispensados em seus grupos,
            // que não tem a matriz como exceção e que estão ativos na data de hoje
            var spec = new DispensaFilterSpecification
            {
                SeqMatrizCurricularOfertaExcecao = seqMatrizCurricularOferta,
                SeqsComponentesCurricularesDispensado = seqsComponentesDispensa,
                SeqsComponentesCurricularesOrigem = seqsComponentesCursados,
                Ativo = true
            };
            var dispensas = SearchProjectionBySpecification(spec, x => new
            {
                Seq = x.Seq,
                ModoExibicao = x.ModoExibicaoHistoricoEscolar,
                SeqsComponentesOrigem = x.GrupoOrigem.Componentes.Select(a => a.SeqComponenteCurricular),
                SeqsComponentesDispensa = x.GrupoDispensado.Componentes.Select(a => a.SeqComponenteCurricular),
            });

            // Para cada dispensa encontrada, verifica se todos os itens informados estão na dispensa
            foreach (var dispensa in dispensas)
            {
                // Valida se todos os componentes do grupo de origem foram informados
                if (dispensa.SeqsComponentesOrigem.Except(seqsComponentesCursados).Count() == 0)
                {
                    if (seqsComponentesCursados.Except(dispensa.SeqsComponentesOrigem).Count() == 0)
                    {
                        // Valida se todos os componentes do grupo de dispensa foram informados
                        if (dispensa.SeqsComponentesDispensa.Except(seqsComponentesDispensa).Count() == 0)
                        {
                            if (seqsComponentesDispensa.Except(dispensa.SeqsComponentesDispensa).Count() == 0)
                                return (dispensa.Seq, dispensa.ModoExibicao);
                        }
                    }
                }
            }

            return (null, null);
        }

        /// <summary>
        /// Exclui uma dispensa
        /// </summary>
        /// <param name="seq">Sequencial da dispensa</param>
        public void ExcluirDispensa(long seq)
        {
            var includes = IncludesDispensa.GrupoOrigem_Componentes_ComponenteCurricular |
                           IncludesDispensa.GrupoDispensado_Componentes_ComponenteCurricular |
                           IncludesDispensa.HistoricosVigencia |
                           IncludesDispensa.MatrizesExcecao;

            Dispensa registro = this.SearchByKey(new SMCSeqSpecification<Dispensa>(seq), includes);

            if (registro.MatrizesExcecao.Count > 0)
                throw new DispensaExcluirMatrizExcecaoException();

            if (!ValidarAssociacaoSolicitacaoServico(seq))
                throw new DispensaSolicitacaoServicoAssociadaException();

            this.DeleteEntity(registro);
        }

        /// <summary>
        /// Valida se a dispensa informada não foi associada a nenhuma solicitação de serviço
        /// </summary>
        /// <param name="seq">Sequencial da dispensa</param>
        /// <returns>Verdadeiro caso nenhuma solicitação utilize essa dispensa</returns>
        private bool ValidarAssociacaoSolicitacaoServico(long seq)
        {
            var specSolicitacaoServico = new SolicitacaoDispensaGrupoFilterSpecification() { SeqDispensa = seq };
            return SolicitacaoDispensaGrupoDomainService.Count(specSolicitacaoServico) == 0;
        }
    }
}