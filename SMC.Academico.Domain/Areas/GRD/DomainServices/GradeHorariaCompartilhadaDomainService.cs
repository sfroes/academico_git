using SMC.Academico.Common.Areas.GRD.Exceptions;
using SMC.Academico.Domain.Areas.GRD.Models;
using SMC.Academico.Domain.Areas.GRD.Specifications;
using SMC.Academico.Domain.Areas.GRD.ValueObjects;
using SMC.Academico.Domain.Areas.TUR.DomainServices;
using SMC.Framework;
using SMC.Framework.Domain;
using SMC.Framework.Extensions;
using SMC.Framework.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SMC.Academico.Domain.Areas.GRD.DomainServices
{
    public class GradeHorariaCompartilhadaDomainService : AcademicoContextDomain<GradeHorariaCompartilhada>
    {
        #region [ DomainService ]

        TurmaDomainService TurmaDomainService => Create<TurmaDomainService>();
        EventoAulaDomainService EventoAulaDomainService => Create<EventoAulaDomainService>();
        DivisaoTurmaDomainService DivisaoTurmaDomainService => Create<DivisaoTurmaDomainService>();

        #endregion [ DomainService ]

        /// <summary>
        /// Buscar Grades de horarias compartilhadas
        /// </summary>
        /// <param name="filtro">Filtros da pesquisa</param>
        /// <returns>Grades comprtilhadas filtradas</returns>
        public SMCPagerData<GradeHorariaCompartilhadaListarVO> BuscarGradesHorariasCompartilhada(GradeHorariaCompartilhadaFilterSpecification filtro)
        {
            var modelsGrade = SearchProjectionBySpecification(filtro, p => new
            {
                p.Seq,
                CicloLetivoDescricao = p.CicloLetivo.Descricao,
                EntidadeResponsavel = p.EntidadeResponsavel.Nome,
                p.Descricao,
                Divisoes = p.Itens.Select(s => s.SeqDivisaoTurma).ToList()
            }, out int total).OrderByDescending(o => o.CicloLetivoDescricao).ThenBy(t => t.EntidadeResponsavel).ThenBy(t => t.Descricao).ToList();

            var retorno = new List<GradeHorariaCompartilhadaListarVO>();

            modelsGrade.ForEach(modelGrade => {
                var modelo = new GradeHorariaCompartilhadaListarVO();
                modelo.Seq = modelGrade.Seq;
                modelo.CicloLetivo = modelGrade.CicloLetivoDescricao;
                modelo.EntidadeResponsavel = modelGrade.EntidadeResponsavel;
                modelo.Descricao = modelGrade.Descricao;
                //TODO: Revisar modelo, deveria ser obrigatório o SeqDivisaoTurma
                modelo.Divisoes = modelGrade.Divisoes.Select(sd => DivisaoTurmaDomainService.MontarDescricaoDivisaoTurmaRegraGRD020(sd.Value)).ToList();

                /*Somente habilitar a opção de exclusão do compartilhamento se todas as divisões de turma do comportilhamento não
                  possuir evento de aula associado ou caso possuam evento aula, não poderão ter nenhum professor ou local
                  associado a eles.*/
                modelo.PerimiteExclusao = true;
                foreach (var divisao in modelGrade.Divisoes)
                {
                    if (!EventoAulaDomainService.ValidarEventosSemColaboradorELocal(divisao.Value))
                    {
                        modelo.PerimiteExclusao = false;
                        break;
                    }
                }
                retorno.Add(modelo);
            });

            return new SMCPagerData<GradeHorariaCompartilhadaListarVO>(retorno, total);
        }

        /// <summary>
        /// Grava um compartilhamento de grade
        /// </summary>
        /// <param name="gradeHorariaCompartilhadaVO">Modelo VO</param>
        /// <returns>Sequencial do compartilhamento criado</returns>
        public long SalvarGradeHorariaCompartilhada(GradeHorariaCompartilhadaVO gradeHorariaCompartilhadaVO)
        {
            var modelBanco = this.SearchByKey(gradeHorariaCompartilhadaVO.Seq, i => i.Itens) ?? new GradeHorariaCompartilhada();
            var model = gradeHorariaCompartilhadaVO.Transform<GradeHorariaCompartilhada>();
            //model.Itens = gradeHorariaCompartilhadaVO
            //    .Itens
            //    .SelectMany(s => s.SeqsDivisaoTurma.Select(ss => new GradeHorariaCompartilhadaItem() { SeqDivisaoTurma = ss }))
            //    .ToList();

            ValidarRemocaoDivisao(gradeHorariaCompartilhadaVO, modelBanco);

            SaveEntity(model);
            return model.Seq;
        }

        /// <summary>
        /// Busca um compartilhamento de grade
        /// </summary>
        /// <param name="seq">Sequencial do compartilhamento</param>
        /// <returns>Dados do compartilhamento</returns>
        public GradeHorariaCompartilhadaVO BuscarGradeHorariaCompartilhada(long seq)
        {
            var model = this.SearchProjectionByKey(seq, p => new GradeHorariaCompartilhadaVO()
            {
                Seq = p.Seq,
                SeqCicloLetivo = p.SeqCicloLetivo,
                SeqEntidadeResponsavel = p.SeqEntidadeResponsavel,
                Descricao = p.Descricao,
                Itens = p
                    .Itens
                    .Select(s => new GradeHorariaCompartilhadaItemVO()
                    {
                        SeqTurma = s.DivisaoTurma.SeqTurma,
                        //TODO: Revisar modelo, deveria ser obrigatório o SeqDivisaoTurma
                        SeqDivisaoTurma = s.SeqDivisaoTurma
                    }).ToList()
            });
            foreach (var item in model.Itens)
            {
                item.DivisoesTurma = DivisaoTurmaDomainService.BuscarDivisoesPorTurmaParaGradeCompartilhadaSelect(item.SeqTurma, new List<long>() { item.SeqDivisaoTurma.GetValueOrDefault() });
            }
            return model;
        }

        /// <summary>
        /// Recupera todas as divisões que fazem para do compartilhamento da divisão informada
        /// </summary>
        /// <param name="seqDivisaoTurma">Sequencial da divisão de turma</param>
        /// <param name="retornarDivisaoInformada">Retorna também a divisão informada, por padrão false</param>
        /// <returns>Sequenciais das divisões que participam do compartilhamento</returns>
        public virtual List<long> BuscarDivisoesGradeHorariaCompartilhada(long seqDivisaoTurma, bool retornarDivisaoInformada = false)
        {
            var spec = new GradeHorariaCompartilhadaFilterSpecification() { SeqDivisaoTurma = seqDivisaoTurma };
            var divisoes = SearchProjectionBySpecification(spec, p => p.Itens.Select(s => s.SeqDivisaoTurma.Value))
                .SelectMany(sm => sm)
                .Where(w => retornarDivisaoInformada || w != seqDivisaoTurma)
                .ToList();
            return divisoes;
        }

        private void ValidarRemocaoDivisao(GradeHorariaCompartilhadaVO gradeHorariaCompartilhadaVO, GradeHorariaCompartilhada model)
        {
            var seqsDivisaoVo = gradeHorariaCompartilhadaVO.Itens.Select(s => s.SeqDivisaoTurma).ToList();
            //TODO: Revisar modelo, deveria ser obrigatório o SeqDivisaoTurma
            var seqsDivisaoModel = model.Itens?.Select(s => s.SeqDivisaoTurma).ToList() ?? new List<long?>();
            var seqsDivisaoRemovido = seqsDivisaoModel.Except(seqsDivisaoVo).ToList();

            if (seqsDivisaoRemovido.SMCAny())
            {
                foreach (var seqDivisao in seqsDivisaoRemovido)
                {
                    if (TurmaDomainService.ValidarPeriodoTurma(seqDivisao.GetValueOrDefault(), encerrado: true))
                    {
                        throw new ExclusaoCompartilhamentoDivisaoNaoPermitidoTurmaEncerradaException();
                    }

                    if (!EventoAulaDomainService.ValidarEventosSemColaboradorELocal(seqDivisao.GetValueOrDefault()))
                    {
                        throw new ExclusaoCompartilhamentoDivisaoNaoPermitidoEventoVinculadoException();
                    }
                }
            }
        }
    }
}