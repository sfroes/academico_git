using SMC.Academico.Common.Areas.SRC.Enums;
using SMC.Academico.Domain.Areas.ALN.DomainServices;
using SMC.Academico.Domain.Areas.ALN.Models;
using SMC.Academico.Domain.Areas.ALN.ValueObjects;
using SMC.Academico.Domain.Areas.SRC.Models;
using SMC.Academico.Domain.Areas.SRC.Specifications;
using SMC.Academico.Domain.Areas.SRC.ValueObjects;
using SMC.Framework.Extensions;
using SMC.Framework.Model;
using SMC.Framework.Specification;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SMC.Academico.Domain.Areas.SRC.DomainServices
{
    public class TipoServicoDomainService : AcademicoContextDomain<TipoServico>
    {
        #region [Domain Service]

        private ServicoDomainService ServicoDomainService => this.Create<ServicoDomainService>();

        private AlunoDomainService AlunoDomainService => this.Create<AlunoDomainService>();

        private AlunoHistoricoSituacaoDomainService AlunoHistoricoSituacaoDomainService => this.Create<AlunoHistoricoSituacaoDomainService>();

        private InstituicaoNivelTipoVinculoAlunoDomainService InstituicaoNivelTipoVinculoAlunoDomainService => this.Create<InstituicaoNivelTipoVinculoAlunoDomainService>();

        #endregion [Domain Service]

        public TipoServicoVO BuscarTipoServico(long seqTipoServico)
        {
            var tipoServico = this.SearchByKey(new SMCSeqSpecification<TipoServico>(seqTipoServico));

            return tipoServico.Transform<TipoServicoVO>();
        }

        public List<SMCDatasourceItem> BuscarTiposServicosPorAlunoSelect(ServicoPorAlunoFiltroVO filtro)
        {
            SituacaoAlunoVO situacaoAtualMatricula = null;

            // Recupera qual a instituição nível tipo vínculo do aluno
            var instituicaoNivelTipoVinculoAluno = InstituicaoNivelTipoVinculoAlunoDomainService.BuscarInstituicaoNivelTipoVinculoAlunoPorPessoaAtuacao(filtro.SeqAluno);

            // Recupera a entidade responsável do aluno
            var seqEntidadeResponsavelAluno = AlunoDomainService.SearchProjectionByKey(new SMCSeqSpecification<Aluno>(filtro.SeqAluno), x => x.Historicos.FirstOrDefault(h => h.Atual).SeqEntidadeVinculo);

            // Filtra os serviços
            var spec = new ServicoFilterSpecification
            {
                OrigemSolicitacaoServico = filtro.OrigemSolicitacaoServico,
                TipoAtuacao = filtro.TipoAtuacao,
                SeqInstituicaoNivelTipoVinculoAluno = instituicaoNivelTipoVinculoAluno.Seq,
                Com1EtapaAtiva = filtro.Com1EtapaAtiva,
                SeqEntidadeResponsavelProcesso = seqEntidadeResponsavelAluno,
                TipoUnidadeResponsavel = filtro.TipoUnidadeResponsavel
            };
            var tipos = ServicoDomainService.SearchProjectionBySpecification(spec, x => new
            {
                SeqServico = x.Seq,
                Seq = x.TipoServico.Seq,
                Descricao = x.TipoServico.Descricao,
                SeqsCiclosLetivos = x.Processos.Where(p => p.SeqCicloLetivo.HasValue &&
                                                           DateTime.Now >= p.DataInicio &&
                                                           (!p.DataFim.HasValue || DateTime.Now <= p.DataFim) &&
                                                             p.Etapas.Any(e => e.Ordem == 1 &&
                                                                            e.SituacaoEtapa == SituacaoEtapa.Liberada &&
                                                                            (!e.DataInicio.HasValue || e.DataInicio.Value <= DateTime.Now) &&
                                                                            (!e.DataFim.HasValue || e.DataFim.Value >= DateTime.Now))
                                                        ).Select(p => p.SeqCicloLetivo.Value).Distinct(),
                SeqsSituacoesMatricula = x.SituacoesAluno.Where(s => s.PermissaoServico == filtro.PermissaoServico).Select(s => s.SeqSituacaoMatricula).Distinct()
            }).ToList();

            // Cria o retorno
            var ret = new List<SMCDatasourceItem>();

            // Caso tenha algum tipo no resultado da consulta
            if (tipos != null && tipos.Any())
            {
                // Busca a situação atual do aluno
                if (filtro.ConsiderarSituacaoAluno)
                    situacaoAtualMatricula = AlunoHistoricoSituacaoDomainService.BuscarSituacaoAtualAluno(filtro.SeqAluno);

                // Cria o dicionário para adicionar os ciclos buscados
                var dicSituacoes = new Dictionary<long, SituacaoAlunoVO>();
                if (situacaoAtualMatricula != null)
                    dicSituacoes.Add(situacaoAtualMatricula.SeqCiclo, situacaoAtualMatricula);

                // Para cada tipo
                foreach (var tipo in tipos)
                {
                    // Caso seja para considerar a situação do aluno...
                    if (filtro.ConsiderarSituacaoAluno)
                    {
                        // Caso tenha ciclo letivo no processo, verifica a situação do aluno no ciclo letivo em questão
                        if (tipo.SeqsCiclosLetivos?.Any() ?? false)
                        {
                            bool existeSituacaoAlgumCiclo = false;
                            foreach (var seqCicloLetivo in tipo.SeqsCiclosLetivos)
                            {
                                if (!dicSituacoes.ContainsKey(seqCicloLetivo))
                                {
                                    var situacaoDic = AlunoHistoricoSituacaoDomainService.BuscarSituacaoAtualAlunoNoCicloLetivo(filtro.SeqAluno, seqCicloLetivo);
                                    if (situacaoDic != null)
                                        dicSituacoes.Add(seqCicloLetivo, situacaoDic);
                                }

                                if (dicSituacoes.ContainsKey(seqCicloLetivo) && tipo.SeqsSituacoesMatricula.Contains(dicSituacoes[seqCicloLetivo].SeqSituacao.GetValueOrDefault()))
                                {
                                    existeSituacaoAlgumCiclo = true;
                                    break;
                                }
                            }

                            if (!existeSituacaoAlgumCiclo)
                                continue;
                        }
                        // Caso contrário, verifica a situação do aluno atual
                        else if (situacaoAtualMatricula == null || !tipo.SeqsSituacoesMatricula.Contains(situacaoAtualMatricula.SeqSituacao.GetValueOrDefault()))
                            continue;
                    }

                    ret.Add(new SMCDatasourceItem
                    {
                        Seq = tipo.Seq,
                        Descricao = tipo.Descricao
                    });
                }
            }

            return ret.GroupBy(x => x.Seq).Select(x => x.First()).OrderBy(x => x.Descricao).ToList();
        }

        public TipoServicoVO BuscarTipoServicoPorToken(string token)
        {
            var spec = new TipoServicoFilterSpecification()
            {
                Token = token
            };

            var tipoServico = this.SearchBySpecification(spec).FirstOrDefault();

            return tipoServico.Transform<TipoServicoVO>();
        }
    }
}