using SMC.Academico.Common.Areas.FIN.Constants;
using SMC.Academico.Common.Areas.FIN.Exceptions;
using SMC.Academico.Domain.Areas.ALN.DomainServices;
using SMC.Academico.Domain.Areas.ALN.Specifications;
using SMC.Academico.Domain.Areas.FIN.DomainServices;
using SMC.Academico.Domain.Areas.FIN.Specifications;
using SMC.Academico.Domain.Areas.FIN.ValueObjects;
using SMC.Academico.Domain.Areas.PES.Specifications;
using SMC.Financeiro.Service.FIN;
using SMC.Framework.Domain;
using SMC.Framework.Jobs;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SMC.Academico.Domain.Areas.FIN.Jobs
{
    public class AtualizacaoTerminoBeneficioWebJob : SMCWebJobBase<ISMCWebJobFilterModel, PessoaAtuacaoBeneficioVO>
    {
        #region [ DomainService ]

        private PessoaAtuacaoBeneficioDomainService PessoaAtuacaoBeneficioDomainService => new PessoaAtuacaoBeneficioDomainService();
        private AlunoDomainService AlunoDomainService => new AlunoDomainService();
        private MotivoAlteracaoBeneficioDomainService MotivoAlteracaoBeneficioDomainService => new MotivoAlteracaoBeneficioDomainService();

        #endregion [ DomainService ]

        #region [ Services ]

        private IFinanceiroService FinanceiroService => Create<IFinanceiroService>();

        #endregion [ Services ]

        #region [ RawQuery ]

        private const string BUSCAR_DADOS_ALUNO = @"

select
    Seq = pab.seq_pessoa_atuacao_beneficio,
    SeqPessoaAtuacao = a.seq_pessoa_atuacao,
    SeqBeneficio = b.seq_beneficio,
    SeqConfiguracaoBeneficio = pab.seq_configuracao_beneficio,
	CodigoAlunoMigracao = a.cod_aluno_migracao,
    Nome = pdp.nom_pessoa,
    IncideParcelaMatricula = pab.ind_incide_parcela_matricula,
    DataInicioVigencia = bhv.dat_inicio_vigencia,
    DataFimVigencia = bhv.dat_fim_vigencia
from aln.aluno a
join pes.pessoa_atuacao pa on a.seq_pessoa_atuacao = pa.seq_pessoa_atuacao
join pes.pessoa_dados_pessoais pdp on pa.seq_pessoa_dados_pessoais = pdp.seq_pessoa_dados_pessoais
join fin.pessoa_atuacao_beneficio pab on a.seq_pessoa_atuacao = pab.seq_pessoa_atuacao
join fin.beneficio b on pab.seq_beneficio = b.seq_beneficio
join fin.beneficio_historico_vigencia bhv on pab.seq_pessoa_atuacao_beneficio = bhv.seq_pessoa_atuacao_beneficio and bhv.ind_atual = 1
where a.cod_aluno_migracao in ({0})

";

        #endregion [ RawQuery ]

        #region [ Constantes ] 

        private const string JUSTIFICATIVA = "Atualização pelo JOB ATUALIZAÇÃO TERMINO BENEFÍCIO";

        #endregion [ Constantes ]

        /// <summary>
        /// Recupera todos beneficios que devem ter sua data de término atualizada no SGA
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        public override ICollection<PessoaAtuacaoBeneficioVO> GetItems(ISMCWebJobFilterModel filter)
        {
            Scheduler.LogSucess($"Recuperando alunos com conecessão até o fim do curso e vinculo ativo");
            var specAlunoMigracao = new AlunoFilterSpecification() { ConcessaoAteFinalCurso = true, VinculoAlunoAtivo = true, CodigoAlunoMigracaoPreenchido = true };
            var codigosAlunoMigracao = this.AlunoDomainService.SearchProjectionBySpecification(specAlunoMigracao, p => p.CodigoAlunoMigracao.Value).ToArray();
            Scheduler.LogSucess($"{codigosAlunoMigracao.Length} alunos encontrados");

            Scheduler.LogSucess($"Recuperando dados dos alunos no financeiro");
            var dataVencimentoPorAluno = FinanceiroService.BuscarDataVencimentoBeneficios(codigosAlunoMigracao);
            Scheduler.LogSucess($"{codigosAlunoMigracao.Length} dados encontrados");

            Scheduler.LogSucess($"Recuperando beneficios dos alunos no SGA");
            var codigosAlunoMiracao = string.Join(",", dataVencimentoPorAluno.Select(s => s.CodigoAluno));
            var query = string.Format(BUSCAR_DADOS_ALUNO, codigosAlunoMiracao);
            var dadosPessoaAtuacaoBeneficio = PessoaAtuacaoBeneficioDomainService.RawQuery<PessoaAtuacaoBeneficioVO>(query);
            Scheduler.LogSucess($"{dadosPessoaAtuacaoBeneficio.Count} dados de alunos no SGA");

            Scheduler.LogSucess($"Validando alunos a serem atualizados");
            var lote = new List<PessoaAtuacaoBeneficioVO>();
            Scheduler.LogSucess($"{dadosPessoaAtuacaoBeneficio.Count} dados de alunos no SGA");
            long seqMotivoAlteracaoBeneficio = this.MotivoAlteracaoBeneficioDomainService.BuscarMotivoAlteracaoBeneficioPorToken(TOKEN_MOTIVO_ALTERACAO_BENEFICIO.ALTERACAO_DAS_PARCELAS_NO_FINANCEIRO);
            if (seqMotivoAlteracaoBeneficio <= 0)
                throw new MotivoAlteracaoBeneficioNaoCadastradoException(TOKEN_MOTIVO_ALTERACAO_BENEFICIO.ALTERACAO_DAS_PARCELAS_NO_FINANCEIRO);
            foreach (var pessoaAtuacaoBeneficio in dadosPessoaAtuacaoBeneficio)
            {
                var dataGRA = dataVencimentoPorAluno.FirstOrDefault(f => f.CodigoAluno == pessoaAtuacaoBeneficio.CodigoAlunoMigracao);
                if (dataGRA != null && dataGRA.DataVencimento != pessoaAtuacaoBeneficio.DataFimVigencia)
                {
                    pessoaAtuacaoBeneficio.DataFimVigencia = dataGRA.DataVencimento;
                    pessoaAtuacaoBeneficio.SeqMotivoAlteracaoBeneficio = seqMotivoAlteracaoBeneficio;
                    pessoaAtuacaoBeneficio.Justificativa = JUSTIFICATIVA;
                    lote.Add(pessoaAtuacaoBeneficio);
                }
            }
            Scheduler.LogSucess($"Foram encontrados {lote.Count} beneficios para serem atualizados");

            return lote;
        }

        /// <summary>
        /// Processa a atualização da data de término de um beneficio
        /// </summary>
        /// <param name="item">Dados do beneficio</param>
        /// <returns>True caso seja atualizado com sucesso</returns>
        public override bool ProcessItem(PessoaAtuacaoBeneficioVO item)
        {
            try
            {
                PessoaAtuacaoBeneficioDomainService.SalvarAlterarVigenciaBeneficio(item);
            }
            catch (Exception ex)
            {
                Scheduler.LogError($"Ocorreu um erro ao atualizar a data do benficio {item.Seq}. Mensagem: {ex.Message}", item.SeqPessoaAtuacao, item.Nome);
                return false;
            }
            return true;
        }
    }
}