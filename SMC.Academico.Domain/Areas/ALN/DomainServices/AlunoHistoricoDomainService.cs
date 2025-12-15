using SMC.Academico.Common.Areas.ALN.Includes;
using SMC.Academico.Common.Areas.PES.Enums;
using SMC.Academico.Domain.Areas.ALN.Models;
using SMC.Academico.Domain.Areas.ALN.Specifications;
using SMC.Academico.Domain.Areas.PES.DomainServices;
using SMC.Academico.Domain.Areas.PES.Models;
using SMC.Framework.Domain;
using SMC.Framework.Specification;
using System;
using System.Linq;

namespace SMC.Academico.Domain.Areas.ALN.DomainServices
{
    public class AlunoHistoricoDomainService : AcademicoContextDomain<AlunoHistorico>
    {
        #region [ DomainService ]

        private PessoaAtuacaoDomainService PessoaAtuacaoDomainService => Create<PessoaAtuacaoDomainService>();

        private AlunoDomainService AlunoDomainService => Create<AlunoDomainService>();

        #endregion

        /// <summary>
        /// Busca o código de aluno de migração de acordo com o tipo de pessoa atuação
        /// Se for ingressante, busca o código do aluno que foi gerado a partir do ingressante.
        /// Se não tiver código de migração (não veio por migração) retorna o próprio seqPessoaAtuacao.
        /// </summary>
        /// <param name="seqPessoaAtuacao">Sequencial da pessoa atuação</param>
        /// <param name="seqSolicitacaoMatricula">Sequencial da solicitação de matrícula</param>
        /// <returns>Registro acadêmico</returns>
        public long BuscarCodigoAlunoMigracao(long seqPessoaAtuacao, long seqSolicitacaoMatricula)
        {
            // Busca o tipo de pessoa atuação
            var specPA = new SMCSeqSpecification<PessoaAtuacao>(seqPessoaAtuacao);
            var tipoAtuacao = PessoaAtuacaoDomainService.SearchProjectionByKey(specPA, x => x.TipoAtuacao);

            long? codigoAlunoMigracao = null;

            // Se atuação aluno
            if (tipoAtuacao == TipoAtuacao.Aluno)
            {
                var specAluno = new SMCDynamicSeqSpecification<Aluno>(seqPessoaAtuacao);
                codigoAlunoMigracao = AlunoDomainService.SearchProjectionByKey(specAluno, x => x.CodigoAlunoMigracao);
            }
            // Se atuação ingressante
            else if (tipoAtuacao == TipoAtuacao.Ingressante)
            {
                var specIngressante = new AlunoHistoricoFilterSpecification() 
                    { 
                        SeqIngressante = seqPessoaAtuacao, 
                        SeqSolicitacaoServico = seqSolicitacaoMatricula, 
                        Atual = true 
                    };
                codigoAlunoMigracao = this.SearchProjectionByKey(specIngressante, p => p.Aluno.CodigoAlunoMigracao);
            }

            if (codigoAlunoMigracao == null)
                return seqPessoaAtuacao;
            else
                return codigoAlunoMigracao.GetValueOrDefault();
        }

        /// <summary>
        /// Busca o sequencial da entidade de vínculo atual do aluno e a instituição de ensino
        /// </summary>
        /// <param name="seq">Sequencial do aluno</param>
        /// <returns>Sequencial da entidade de vínculo atual do aluno e Sequencial da instituição de ensino</returns>
        public Tuple<long, long?> BuscarEntidadeVinculoAluno(long seq)
        {
            var filtro = new AlunoHistoricoFilterSpecification() { SeqAluno = seq, Atual = true };

            var registro = this.SearchProjectionByKey(filtro, p => new { p.SeqEntidadeVinculo, p.EntidadeVinculo.SeqInstituicaoEnsino });
            return new Tuple<long, long?>(registro.SeqEntidadeVinculo, registro.SeqInstituicaoEnsino);
        }

        /// <summary>
        /// Buscar o registro de histório atual do aluno.
        /// </summary>
        /// <param name="seqPessoaAtuacao">Sequencial do aluno.</param>
        /// <param name="includes">Includes.</param>
        /// <returns>Registro atual do histórico do aluno.</returns>
        public AlunoHistorico BuscarHistoricoAtualAluno(long seqPessoaAtuacao, IncludesAlunoHistorico includes)
        {
            AlunoHistoricoFilterSpecification spec = new AlunoHistoricoFilterSpecification() { SeqAluno = seqPessoaAtuacao, Atual = true };
            return SearchBySpecification(spec, includes).FirstOrDefault();
        }

        /// <summary>
        /// Buscar o nível de ensino do histório atual do aluno.
        /// </summary>
        /// <param name="seqPessoaAtuacao">Sequencial do aluno.</param>
        /// <returns>Sequencial do nível de ensino do histórico atual do aluno.</returns>
        public long BuscarNivelEnsinoHistoricoAtualAluno(long seqPessoaAtuacao)
        {
            AlunoHistoricoFilterSpecification spec = new AlunoHistoricoFilterSpecification() { SeqAluno = seqPessoaAtuacao, Atual = true };
            return SearchProjectionByKey(spec, x => x.SeqNivelEnsino);
        }

        /// <summary>
        /// Buscar o sequencial do aluno historico atual
        /// </summary>
        /// <param name="seqAluno">Sequencial do Aluno</param>
        /// <returns>Sequencial do historico aluno</returns>
        public long BuscarSequencialAlunoHistoricoAtual(long seqAluno)
        {
            AlunoHistoricoFilterSpecification spec = new AlunoHistoricoFilterSpecification() { SeqAluno = seqAluno, Atual = true };
            return SearchProjectionByKey(spec, x => x.Seq);
        }

        /// <summary>
        ///  Buscar sequencial do aluno pelo aluno historico
        /// </summary>
        /// <param name="seqAlunoHistorico">Sequacial do aluno historico</param>
        /// <returns>Sequencial do aluno</returns>
        public long BuscarSeqAlunoPorAlunoHistorico(long seqAlunoHistorico)
        {
            return this.SearchProjectionByKey(seqAlunoHistorico, p => p.SeqAluno);
        }

        /// <summary>
        /// Buscar o sequencial do curso pelo historico atual
        /// </summary>
        /// <param name="seqAluno">Sequencial do Aluno</param>
        /// <returns>Sequencial do historico aluno</returns>
        public long BuscarSequencialCursoAluno(long seqAluno)
        {
            var spec = new AlunoHistoricoFilterSpecification() { SeqAluno = seqAluno, Atual = true };
            return SearchProjectionByKey(spec, x => x.CursoOfertaLocalidadeTurno.CursoOfertaLocalidade.CursoOferta.SeqCurso);
        }
    }
}