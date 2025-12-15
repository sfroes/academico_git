using SMC.Academico.Common.Areas.CSO.Exceptions;
using SMC.Academico.Common.Areas.Shared.Helpers;
using SMC.Academico.Domain.Areas.CSO.Models;
using SMC.Academico.Domain.Areas.CSO.Specifications;
using SMC.Academico.Domain.Areas.CSO.ValueObjects;
using SMC.Framework;
using System.Collections.Generic;
using System.Linq;

namespace SMC.Academico.Domain.Areas.CSO.DomainServices
{
    public class CursoFormacaoEspecificaTitulacaoDomainService : AcademicoContextDomain<CursoFormacaoEspecificaTitulacao>
    {
        /// <summary>
        /// Valida se não ocorre uma colisão de períodos para titulações do mesmo tipo
        /// </summary>
        /// <param name="titulacoesCursoFormacaoEspecifica">Titulações de uma formação específica</param>
        /*
        public void ValidarSobreposicaoPeriodosTitulacao(IEnumerable<CursoFormacaoEspecificaTitulacao> titulacoesCursoFormacaoEspecifica)
        {
            if (!titulacoesCursoFormacaoEspecifica.SMCAny())
                return;
            var grupoTipo = titulacoesCursoFormacaoEspecifica.GroupBy(g => g.SeqTitulacao);
            foreach (var grupo in grupoTipo)
            {
                // Valida se não ocorem sopreposições de períodos
                if (!ValidacaoData.ValidarSobreposicaoPeriodos(grupo.ToList(), nameof(CursoFormacaoEspecificaTitulacao.DataInicio), nameof(CursoFormacaoEspecificaTitulacao.DataFim)))
                {
                    throw new CursoFormacaoEspecificaTitulacaoDuplicadaException();
                }
            }
        }
        */

        /// <summary>
        /// Buscar a titulação de todas as formações específicas para associar na criação do aluno
        /// </summary>
        /// <param name="seqCurso">Sequencial do curso</param>   
        /// <param name="seqsFormacoesEspecificas">lista de sequenciais da formação especifica</param>
        public List<CursoFormacaoEspecificaTitulacaoVO> BuscarTitulacaoFormacaoPorCursoFormacao(long seqCurso, List<long> seqsFormacoesEspecificas)
        {
            var spec = new CursoFormacaoEspecificaTitulacaoFilterSpecification()
            {
                SeqCurso = seqCurso,
                SeqsFormacoesEspecificas = seqsFormacoesEspecificas
            };

            var retorno = this.SearchProjectionBySpecification(spec, s => new CursoFormacaoEspecificaTitulacaoVO
            {
               SeqTitulacao = s.SeqTitulacao,
               SeqFormacaoEspecifica = s.CursoFormacaoEspecifica.SeqFormacaoEspecifica,
            }).ToList();

            return retorno;
        }

    }
}