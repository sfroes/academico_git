using SMC.Academico.Common.Areas.APR.Enums;
using SMC.Academico.Domain.Areas.APR.DomainServices;
using SMC.Academico.Domain.Areas.ORG.Models;
using SMC.Academico.Domain.Areas.ORG.Specifications;
using SMC.Academico.Domain.Areas.TUR.DomainServices;
using SMC.Academico.Domain.Areas.TUR.Specifications;
using SMC.Framework.Domain;
using System.Linq;

namespace SMC.Academico.Domain.Areas.ORG.DomainServices
{
    public class InstituicaoNivelTipoEventoDomainService : AcademicoContextDomain<InstituicaoNivelTipoEvento>
    {
        #region [Domain]

        private DivisaoTurmaDomainService DivisaoTurmaDomainService => Create<DivisaoTurmaDomainService>();
        private TurmaDomainService TurmaDomainService => Create<TurmaDomainService>();
        private OrigemAvaliacaoDomainService OrigemAvaliacaoDomainService => Create<OrigemAvaliacaoDomainService>();

        #endregion [Domain]

        public virtual long BuscarSeqTipoEventoAgdPorTokenDivisao(string tokenTipoEvento, long seqDivisaoTurma)
        {
            var seqNivelEnsino = DivisaoTurmaDomainService.SearchProjectionByKey(seqDivisaoTurma, p => p
                .Turma
                .ConfiguracoesComponente.FirstOrDefault(f => f.Principal)
                .ConfiguracaoComponente
                .ComponenteCurricular
                .NiveisEnsino.FirstOrDefault(f => f.Responsavel)
                .SeqNivelEnsino);
            var spec = new InstituicaoNivelTipoEventoFilterSpecification()
            {
                SeqNivelEnsino = seqNivelEnsino,
                TokenTipoEventoAgd = tokenTipoEvento
            };

            return SearchProjectionBySpecification(spec, p => p.SeqTipoEventoAgd).FirstOrDefault();
        }

        public long BuscarSeqTipoEventoAgdPorTipoAvaliacao(TipoAvaliacao tipoAvaliacao, long seqOrigemAvaliacao)
        {
            var turma = OrigemAvaliacaoDomainService.SearchProjectionByKey(seqOrigemAvaliacao, x => x.TipoOrigemAvaliacao == TipoOrigemAvaliacao.Turma);
            var spec = new InstituicaoNivelTipoEventoFilterSpecification()
            {
                TipoAvaliacao = tipoAvaliacao
            };

            // Recupera os niveis de ensino
            if (turma)
            {
                var dadosTurma = TurmaDomainService.SearchProjectionByKey(new TurmaFilterSpecification { SeqOrigemAvaliacao = seqOrigemAvaliacao }, x => new
                {
                    SeqNivelEnsino = x.ConfiguracoesComponente.FirstOrDefault(f => f.Principal)
                     .ConfiguracaoComponente
                     .ComponenteCurricular
                     .NiveisEnsino.FirstOrDefault(f => f.Responsavel)
                     .SeqNivelEnsino,
                    SeqInstituicaoEnsino = x.CicloLetivoInicio.SeqInstituicaoEnsino
                });

                spec.SeqNivelEnsino = dadosTurma.SeqNivelEnsino;
                spec.SeqInstituicaoEnsino = dadosTurma.SeqInstituicaoEnsino;
            }
            else
            {
                // Recupera para saber se é turma ou divisão
                var dadosDivisaoTurma = DivisaoTurmaDomainService.SearchProjectionByKey(new DivisaoTurmaFilterSpecification { SeqOrigemAvaliacao = seqOrigemAvaliacao }, p => new
                {
                    SeqNivelEnsino = p.Turma
                                      .ConfiguracoesComponente.FirstOrDefault(f => f.Principal)
                                      .ConfiguracaoComponente
                                      .ComponenteCurricular
                                      .NiveisEnsino.FirstOrDefault(f => f.Responsavel)
                                      .SeqNivelEnsino,
                    SeqInstituicaoEnsino = p.Turma.CicloLetivoInicio.SeqInstituicaoEnsino
                });

                spec.SeqNivelEnsino = dadosDivisaoTurma.SeqNivelEnsino;
                spec.SeqInstituicaoEnsino = dadosDivisaoTurma.SeqInstituicaoEnsino;
            }

            return SearchProjectionBySpecification(spec, p => p.SeqTipoEventoAgd).FirstOrDefault();
        }
    }
}