using SMC.Academico.Common.Areas.ALN.Enums;
using SMC.Academico.Common.Areas.MAT.Constants;
using SMC.Academico.Domain.Areas.ALN.Models;
using SMC.Academico.Domain.Areas.ALN.Specifications;
using SMC.Academico.Domain.Areas.ALN.ValueObjects;
using SMC.Academico.Domain.Areas.CAM.DomainServices;
using SMC.Academico.Domain.Areas.MAT.DomainServices;
using SMC.DadosMestres.Common.Areas.PES.Enums;
using SMC.Financeiro.Common.Areas.GRA.Enums;
using SMC.Framework;
using SMC.Framework.Domain;
using SMC.Framework.Extensions;
using SMC.Framework.Model;
using System.Collections.Generic;
using System.Linq;

namespace SMC.Academico.Domain.Areas.ALN.DomainServices
{
    public class ViewAlunoDomainService : AcademicoContextDomain<ViewAluno>
    {
        #region [ DomainService ]

        private AlunoDomainService AlunoDomainService => Create<AlunoDomainService>();

        private InstituicaoNivelTipoVinculoAlunoDomainService InstituicaoNivelTipoVinculoAlunoDomainService => Create<InstituicaoNivelTipoVinculoAlunoDomainService>();

        private SituacaoMatriculaDomainService SituacaoMatriculaDomainService => Create<SituacaoMatriculaDomainService>();

        private CicloLetivoDomainService CicloLetivoDomainService => Create<CicloLetivoDomainService>();

        #endregion [ DomainService ]

        /// <summary>
        /// Busca os alunos com as depêndencias apresentadas na listagem do seu cadastro
        /// </summary>
        /// <param name="filtro">Filtros do ingressante</param>
        /// <returns>Dados paginados de ingressante</returns>
        public SMCPagerData<AlunoListaVO> BuscarAlunos(ViewAlunoFilterSpecification filtros)
        {
            filtros.SetOrderBy(o => o.Nome);
            filtros.SetOrderByDescending(o => o.AnoNumeroCicloLetivo);

            var lista = this.SearchProjectionBySpecification(filtros, p => new AlunoListaVO()
            {
                Seq = p.SeqPessoaAtuacao,
                SeqNivelEnsino = p.SeqNivelEnsino,
                SeqTipoVinculoAluno = p.SeqTipoVinculoAluno,
                SeqTipoTermoIntercambio = p.SeqTipoTermoIntercambio,
                NumeroRegistroAcademico = p.NumeroRegistroAcademico,
                Nome = p.Nome,
                Cpf = p.Cpf,
                NumeroPassaporte = p.NumeroPassaporte,
                DataNascimento = p.DataNascimento,
                Falecido = p.Falecido,
                DescricaoVinculo = p.DescricaoTipoVinculoAluno,
                DadosVinculo = p.DescricaoPessoaAtuacao,
                DataInicioTermoIntercambio = p.DataInicioTermoIntercambioVigencia,
                DataFimTermoIntercambio = p.DataFimTermoIntercambioVigencia,
                DescricaoInstituicaoExterna = p.NomeInstituicaoExterna,
                DescricaoCursoOferta = p.DescricaoCursoOferta,
                DescricaoLocalidade = p.NomeLocalidade,
                DescricaoTurno = p.DescricaoTurno,
                DescricaoTipoTermoIntercambio = p.DescricaoTipoTermoIntercambio,
                TipoVinculoAluno = p.DescricaoTipoVinculoAluno,
                PermiteFormacaoEspecifica = p.TipoVinculoAlunoFinanceiro != TipoVinculoAlunoFinanceiro.RegimeDisciplinaIsolada,
                VinculoAlunoAtivo = p.VinculoAlunoAtivo,
                DescricaoSituacaoMatricula = p.DescricaoCicloLetivoSituacaoMatricula,
                SeqSituacaoMatricula = p.SeqSituacaoMatricula
            }, out int total).ToList();

            var configuracoesVinculos = this.InstituicaoNivelTipoVinculoAlunoDomainService
                .BuscarConfiguracoesVinculos(lista.Select(s => s.SeqNivelEnsino.GetValueOrDefault()).Distinct().ToArray(),
                                             lista.Select(s => s.SeqTipoVinculoAluno).Distinct().ToArray());

            var emailsAlunos = AlunoDomainService.SearchProjectionBySpecification(new AlunoFilterSpecification() { Seqs = lista.Select(s => s.Seq).ToArray() }, p => new
            {
                p.Seq,
                Emails = p.EnderecosEletronicos.Where(w => w.EnderecoEletronico.TipoEnderecoEletronico == TipoEnderecoEletronico.Email)
                          .OrderBy(o => o.EnderecoEletronico.Descricao)
                          .Select(s => s.EnderecoEletronico.Descricao).ToList()
            }).ToList();

            foreach (var aluno in lista)
            {
                var configuracaoVinculo = configuracoesVinculos
                    .FirstOrDefault(f => f.InstituicaoNivel.SeqNivelEnsino == aluno.SeqNivelEnsino.GetValueOrDefault()
                                      && f.SeqTipoVinculoAluno == aluno.SeqTipoVinculoAluno);
                // RN_ALN_029 - Descrição Vínculo Tipo Termo Intercâmbio
                aluno.DescricaoVinculo = AlunoDomainService.RecuperarVinculoAluno(configuracaoVinculo, aluno.SeqTipoTermoIntercambio,
                                                                                                       aluno.DescricaoVinculo,
                                                                                                       aluno.DescricaoTipoTermoIntercambio);
                aluno.ExigeParceriaIntercambioIngresso = configuracaoVinculo.ExigeParceriaIntercambioIngresso;
                aluno.ExigePeriodoIntercambioTermo = configuracaoVinculo.TiposTermoIntercambio.FirstOrDefault(f => f.SeqTipoTermoIntercambio == aluno.SeqTipoTermoIntercambio)?.ExigePeriodoIntercambioTermo ?? false;
                aluno.Emails = emailsAlunos.FirstOrDefault(f => f.Seq == aluno.Seq)?.Emails;

                aluno.PermiteCancelarMatricula = SituacaoMatriculaDomainService.SearchProjectionByKey(aluno.SeqSituacaoMatricula,
                                                    x => x.TipoSituacaoMatricula.Token != TOKENS_TIPO_SITUACAO_MATRICULA.FORMADO &&
                                                         x.TipoSituacaoMatricula.Token != TOKENS_TIPO_SITUACAO_MATRICULA.CANCELADO &&
                                                         x.TipoSituacaoMatricula.Token != TOKENS_TIPO_SITUACAO_MATRICULA.TRANSFERIDO);
            }

            return new SMCPagerData<AlunoListaVO>(lista, total);
        }

        /// <summary>
        /// Busca os alunos para emissão de relatórios
        /// </summary>
        /// <param name="filtro">Filtro a ser considerado na pesquisa</param>
        /// <returns>Lista de alunos para emissão de relatórios</returns>
        public SMCPagerData<RelatorioListarVO> BuscarAlunosRelatorio(RelatorioFiltroVO filtro, bool buscarDescricaoCicloLetivoIngresso = false)
        {
            var total = 0;

            var seqCicloLetivoSituacao = filtro.SeqCicloLetivo;
            // Ignora o ciclo letivo para consulta original
            filtro.SeqCicloLetivo = null;

            // Monta o specification para buscar os dados dos alunos
            var specVwAluno = filtro.Transform<ViewAlunoFilterSpecification>();
            specVwAluno.SeqEntidadesResponsaveis = filtro.SeqsEntidadesResponsaveis.HasValue ? new List<long>() { filtro.SeqsEntidadesResponsaveis.GetValueOrDefault() } : null;

            // Informa que não tem paginação
            specVwAluno.MaxResults = int.MaxValue;

            // Informa a ordenação
            specVwAluno.SetOrderBy(o => o.Nome);
            specVwAluno.SetOrderBy(o => o.NumeroRegistroAcademico);

            // Realiza a pesquisa
            var result = this.SearchProjectionBySpecification(specVwAluno, a => new RelatorioListarVO()
            {
                Seq = a.SeqPessoaAtuacao,
                NumeroRegistroAcademico = a.NumeroRegistroAcademico,
                Nome = a.Nome,
                Cpf = a.Cpf,
                NumeroPassaporte = a.NumeroPassaporte,
                DescricaoSituacaoMatricula = a.DescricaoCicloLetivoSituacaoMatricula,
                TipoVinculoAlunoFinanceiro = a.TipoVinculoAlunoFinanceiro,
                DescricaoPessoaAtuacao = a.DescricaoPessoaAtuacao,
                DescricaoCursoOferta = a.DescricaoCursoOferta,
                SeqCicloLetivoIngresso = a.SeqCicloLetivoIngresso
            }, out total).ToList();

            // Remove os alunos que não tenham situação do tipo matriculado ou formado no ciclo letivo informado
            if (filtro.TipoRelatorio == TipoRelatorio.DeclaracaoMatricula && result.SMCAny())
            {
                var specAlunosMatriculados = new AlunoFilterSpecification()
                {
                    Seqs = result.Select(s => s.Seq).ToArray(),
                    SeqCicloLetivoSituacaoMatricula = seqCicloLetivoSituacao,
                    TokensTipoSituacaoMatricula = new[] { TOKENS_TIPO_SITUACAO_MATRICULA.FORMADO, TOKENS_TIPO_SITUACAO_MATRICULA.MATRICULADO }
                };
                var seqsValidos = AlunoDomainService.SearchProjectionBySpecification(specAlunosMatriculados, p => p.Seq).ToList();
                result = result.Where(w => seqsValidos.Contains(w.Seq)).ToList();
            }

            if (buscarDescricaoCicloLetivoIngresso)
            {
                foreach (var dadosAluno in result)
                {
                    var cicloLetivoDesc = CicloLetivoDomainService.BuscarDescricaoFormatadaCicloLetivo(dadosAluno.SeqCicloLetivoIngresso);
                    dadosAluno.DescricaoCicloLetivo = cicloLetivoDesc;
                }
            }

            return new SMCPagerData<RelatorioListarVO>(result, total);
        }
    }
}