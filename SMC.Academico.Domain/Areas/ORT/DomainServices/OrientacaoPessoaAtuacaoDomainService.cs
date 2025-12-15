using SMC.Academico.Domain.Areas.ALN.DomainServices;
using SMC.Academico.Domain.Areas.ORT.Models;
using SMC.Academico.Domain.Areas.ORT.Specifications;
using SMC.Academico.Domain.Areas.ORT.ValueObjects;
using SMC.Framework.Domain;
using SMC.Framework.Extensions;
using System.Collections.Generic;
using System.Linq;

namespace SMC.Academico.Domain.Areas.ORT.DomainServices
{
    public class OrientacaoPessoaAtuacaoDomainService : AcademicoContextDomain<OrientacaoPessoaAtuacao>
    {
        #region [ DomainService ]

        private TipoTermoIntercambioDomainService TipoTermoIntercambioDomainService
        {
            get { return this.Create<TipoTermoIntercambioDomainService>(); }
        }

        #endregion [ DomainService ]

        /// <summary>
        /// Validar orientação do ingressante de acordo com a seguinte regra:
        /// Deve existir uma orientação para o ingressante que não seja orientação do tipo turma (ind_orientacao_turma). 
        /// Caso a orientação seja uma orientação associada à parceria de intercambio, esta deve ser de um tipo de termo que "CONCEDE FORMAÇÃO"
        /// </summary>
        /// <param name="seqPessoaAtuacao">Sequencial da pessoa atuação</param>
        /// <param name="seqInstituicaoNivelTipoVinculoAluno">Sequencial da instituição nível tipo vinculo do aluno</param>
        /// <returns>Lista com o sequencial de todos os tipos de orientação</returns>
        public bool ValidarOrientacoesPessoaAtuacao(long seqPessoaAtuacao, long seqInstituicaoNivelTipoVinculoAluno)
        {
            var spec = new OrientacaoPessoaAtuacaoFilterSpecification() { SeqPessoaAtuacao = seqPessoaAtuacao, OrientacaoTurma = false };
            spec.MaxResults = int.MaxValue;

            var registros = this.SearchProjectionBySpecification(spec, p => p.Orientacao).TransformList<OrientacaoVO>();

            if (registros.Count == 0)
                return true;
            else
            {
                if (registros.Any(a => a.SeqTipoTermoIntercambio.HasValue))
                {
                    foreach (var item in registros.Where(w => w.SeqTipoTermoIntercambio.HasValue).ToList())
                    {
                        var concedeFormacao = TipoTermoIntercambioDomainService.TipoTermoIntercambioConcedeFormacao(item.SeqTipoTermoIntercambio.Value, seqInstituicaoNivelTipoVinculoAluno);
                        if (concedeFormacao)
                            return false;
                    }

                    return true;
                }
                else return false;
            }            
        }

        /// <summary>
        /// Validar orientação do ingressante de acordo com a seguinte regra:
        /// Deve existir uma orientação para o ingressante do mesmo tipo que foi parametrizado utilizado na convocação
        /// </summary>
        /// <param name="seqPessoaAtuacao">Sequencial da pessoa atuação</param>
        /// <param name="seqsTipoOrientacao">Sequencial dos tipos de orientação</param>
        /// <returns>Verdadeiro quando o ingressante possui todas as orientações do mesmo tipo parametrizado</returns>
        public bool ValidarOrientacoesPessoaAtuacaoConvocacao(long seqPessoaAtuacao, long[] seqsTipoOrientacao)
        {
            var spec = new OrientacaoPessoaAtuacaoFilterSpecification() { SeqPessoaAtuacao = seqPessoaAtuacao, SeqsTipoOrientacao = seqsTipoOrientacao };
            spec.MaxResults = int.MaxValue;

            var registros = this.SearchProjectionBySpecification(spec, p => p.Orientacao).TransformList<OrientacaoVO>();

            if (registros.Count == 0)
                return false;
            else
            {
                if (registros.Any(a => a.SeqTipoOrientacao.HasValue && !seqsTipoOrientacao.Contains(a.SeqTipoOrientacao.Value)))
                    return false;
                else
                    return true;                
            }
        }
    }
}