using SMC.Academico.Domain.Areas.SRC.Models;
using SMC.Framework.Specification;

namespace SMC.Academico.Domain.Areas.SRC.DomainServices
{
    public class SolicitacaoTrabalhoAcademicoDomainService : AcademicoContextDomain<SolicitacaoTrabalhoAcademico>
    {
#region [ Query ]
        #region [ _inserirSolicitacaoTrabalhoAcademicoPorSolicitacaoServico ]

        private string _inserirSolicitacaoTrabalhoAcademicoPorSolicitacaoServico =
                        @" INSERT INTO SRC.solicitacao_trabalho_academico (seq_solicitacao_servico) VALUES ({0})";

        #endregion [ _inserirSolicitacaoIntercambioPorSolicitacaoServico ]

#endregion Query


        /// <summary>
        /// </summary>
        /// <param name="seqSolicitacaoServico">Sequencial da solicitação de serviço</param>
        public void CriarSolicitacaoTrabalhoAcademicoPorSolicitacaoServico(long seqSolicitacaoServico)
        {
            var registro = this.SearchByKey(new SMCSeqSpecification<SolicitacaoTrabalhoAcademico>(seqSolicitacaoServico));

            if (registro == null)
                ExecuteSqlCommand(string.Format(_inserirSolicitacaoTrabalhoAcademicoPorSolicitacaoServico, seqSolicitacaoServico));
        }

        public long BuscarSeqDivisaoComponentePorSolicitacao(long seqSolicitacaoServico)
        {
            var solicitacaoTrabalhoAcademico = this.SearchByKey(new SMCSeqSpecification<SolicitacaoTrabalhoAcademico>(seqSolicitacaoServico));
            if (solicitacaoTrabalhoAcademico == null || !solicitacaoTrabalhoAcademico.SeqDivisaoComponente.HasValue )
                return 0;

            return solicitacaoTrabalhoAcademico.SeqDivisaoComponente.Value;
        }
    }
}