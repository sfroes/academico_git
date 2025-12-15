using SMC.Academico.Domain.Areas.TUR.Models;
using SMC.Academico.Domain.Areas.TUR.Specifications;
using System.Linq;

namespace SMC.Academico.Domain.Areas.TUR.DomainServices
{
    public class RestricaoTurmaMatrizDomainService : AcademicoContextDomain<RestricaoTurmaMatriz>
    {
        /// <summary>
        /// Busca a restrição de turma matriz para realizar a verificação de vagas nos processos de matrícula
        /// </summary>
        /// <param name="seqTurmaConfiguracaoComponente">Sequencial da turma configuração componente</param>
        /// <param name="seqMatrizCurricularOferta">Sequencial da matriz curricular oferta</param>
        /// <returns>Retorna o primeiro objeto de restrição de turma matriz</returns>
        public RestricaoTurmaMatriz BuscarRestricaoTurmaMatrizPorTurmaConfiguracaoMatriz(long? seqTurmaConfiguracaoComponente, long? seqMatrizCurricularOferta)
        {
            RestricaoTurmaMatrizFilterSpecification filtro = new RestricaoTurmaMatrizFilterSpecification()
            {
                SeqTurmaConfiguracaoComponente = seqTurmaConfiguracaoComponente,
                SeqMatrizCurricularOferta = seqMatrizCurricularOferta
            };

            var registro = this.SearchBySpecification(filtro).FirstOrDefault();

            return registro;
        }

        /// <summary>
        /// Atualizar os dados quantidade de vagas oculpadas 
        /// </summary>
        /// <param name="seq">Sequencial da restrição turma matriz</param>
        /// <param name="quantidadeVagasOcupadas">Quantidade de vagas oculpadas</param>
        public void AtualizarQuantidadeVagasOculpadas(long seq, short quantidadeVagasOcupadas)
        {
            var restricaoTurmaMatriz = new RestricaoTurmaMatriz()
            {
                Seq = seq,
                QuantidadeVagasOcupadas = quantidadeVagasOcupadas
            };

            UpdateFields(restricaoTurmaMatriz, f => f.QuantidadeVagasOcupadas);
        }
    }
}
