using SMC.Academico.Domain.Areas.CUR.Models;
using SMC.Academico.Domain.Areas.CUR.Specifications;
using SMC.Academico.Domain.Areas.CUR.ValueObjects;
using System.Collections.Generic;
using System.Linq;

namespace SMC.Academico.Domain.Areas.CUR.DomainServices
{
    public class MatrizCurricularOfertaExcecaoLocalidadeDomainService : AcademicoContextDomain<MatrizCurricularOfertaExcecaoLocalidade>
    {
        /// <summary>
        /// Busca uma lista de Localidades de Exceções conforme as ofertas de matriz curricular passadas como parâmetro
        /// </summary>
        /// <param name="seqsMatrizCurricularOferta">Lista Sequencial das ofertas de matriz curricular oferta</param>
        /// <returns>Lista de Exceção de localidade de matriz curricular oferta</returns>
        public List<MatrizCurricularOfertaExcecaoLocalidadeVO> BuscarMatrizesCurricularExcecoesLocalidades(List<long> seqsMatrizCurricularOferta)
        {
            var filtros = new MatrizCurricularOfertaExcecaoLocalidadeFilterSpecification() { SeqsMatrizCurricularOferta = seqsMatrizCurricularOferta };

            var retorno = this.SearchProjectionBySpecification(filtros,
                                                                p => new MatrizCurricularOfertaExcecaoLocalidadeVO()
                                                                {
                                                                    Seq = p.Seq,
                                                                    SeqEntidadeLocalidade = p.SeqEntidadeLocalidade,
                                                                    SeqMatrizCurricularOferta = p.SeqMatrizCurricularOferta,
                                                                    DescricaoLocalidade = p.EntidadeLocalidade.Nome
                                                                }).ToList();

            return retorno;
        }
    }
}