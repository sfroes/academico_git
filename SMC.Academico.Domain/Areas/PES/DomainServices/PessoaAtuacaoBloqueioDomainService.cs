using SMC.Academico.Common.Areas.PES.Constants;
using SMC.Academico.Common.Areas.PES.Enums;
using SMC.Academico.Common.Areas.PES.Exceptions;
using SMC.Academico.Common.Areas.PES.Includes;
using SMC.Academico.Common.Areas.SRC.Enums;
using SMC.Academico.Common.Constants;
using SMC.Academico.Domain.Areas.ALN.DomainServices;
using SMC.Academico.Domain.Areas.ALN.Models;
using SMC.Academico.Domain.Areas.ALN.Specifications;
using SMC.Academico.Domain.Areas.ALN.ValueObjects;
using SMC.Academico.Domain.Areas.PES.Models;
using SMC.Academico.Domain.Areas.PES.Specifications;
using SMC.Academico.Domain.Areas.PES.ValueObjects;
using SMC.Academico.Domain.Areas.SRC.DomainServices;
using SMC.Academico.Domain.Areas.SRC.Models;
using SMC.DadosMestres.Common.Areas.PES.Enums;
using SMC.Framework;
using SMC.Framework.Extensions;
using SMC.Framework.Specification;
using SMC.IntegracaoAcademico.ServiceContract.Areas.IAC.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace SMC.Academico.Domain.Areas.PES.DomainServices
{
    public class PessoaAtuacaoBloqueioDomainService : AcademicoContextDomain<PessoaAtuacaoBloqueio>
    {
        #region [Servicos]

        private ConfiguracaoEtapaDomainService ConfiguracaoEtapaDomainService => this.Create<ConfiguracaoEtapaDomainService>();

        private InstituicaoNivelTipoTermoIntercambioDomainService InstituicaoNivelTipoTermoIntercambioDomainService => this.Create<InstituicaoNivelTipoTermoIntercambioDomainService>();

        private InstituicaoNivelTipoVinculoAlunoDomainService InstituicaoNivelTipoVinculoAlunoDomainService => this.Create<InstituicaoNivelTipoVinculoAlunoDomainService>();

        private MotivoBloqueioDomainService MotivoBloqueioDomainService => this.Create<MotivoBloqueioDomainService>();

        private PessoaAtuacaoDomainService PessoaAtuacaoDomainService => this.Create<PessoaAtuacaoDomainService>();

        private PessoaDomainService PessoaDomainService => Create<PessoaDomainService>();

        private IIntegracaoAcademicoService IntegracaoAcademicoService => Create<IIntegracaoAcademicoService>();

        #endregion

        public bool PessoaAtuacaoPossuiBloqueios(long seqPessoaAtuacao, long seqConfiguracaoEtapa, bool bloqueioFimEtapa)
        {
            // Cria o retorno
            bool pessoaPossuiBloqueios = false;
            bool pessoaAtuacaoPossuiBloqueios = false;

            // Busca todos os bloqueios que impedem iniciar uma etapa
            var bloqueiosEtapa = ConfiguracaoEtapaDomainService.SearchProjectionByKey(new SMCSeqSpecification<ConfiguracaoEtapa>(seqConfiguracaoEtapa), x => x.ConfiguracoesBloqueio.Where(b => bloqueioFimEtapa ? b.ImpedeFimEtapa : b.ImpedeInicioEtapa));

            // Verifica se a etapa possui configuração de algum bloqueio
            if (bloqueiosEtapa.Count() > 0)
            {
                long? seqPessoa = null;

                // Verifica quais bloqueios são feitos por pessoa e quais por pessoa atuação (ambito)
                var bloqueiosConfiguradosPessoa = bloqueiosEtapa.Where(b => b.AmbitoBloqueio == AmbitoBloqueio.Pessoa);
                var bloqueiosConfiguradosPessoaAtuacao = bloqueiosEtapa.Where(b => b.AmbitoBloqueio == AmbitoBloqueio.PessoaAtuacao);

                // Recupera o sequencial da pessoa associada ao pessoa atuação caso tenha algum tipo de bloqueio configurado por pessoa
                if (bloqueiosConfiguradosPessoa != null && bloqueiosConfiguradosPessoa.Count() > 0)
                    seqPessoa = PessoaAtuacaoDomainService.SearchProjectionByKey(new SMCSeqSpecification<PessoaAtuacao>(seqPessoaAtuacao), p => p.SeqPessoa);

                // Busca todos os bloqueios configurados para Pessoa (motivos que ambito = pessoa)
                if (bloqueiosConfiguradosPessoa != null && bloqueiosConfiguradosPessoa.Count() > 0)
                {
                    // Cria o specification
                    var spec = new PessoaAtuacaoBloqueioFilterSpecification
                    {
                        SeqMotivoBloqueio = bloqueiosConfiguradosPessoa.Select(b => b.SeqMotivoBloqueio).ToList(),
                        SeqPessoa = seqPessoa,
                        SituacaoBloqueio = SituacaoBloqueio.Bloqueado,
                        DataBloqueioMenorOuIgualA = DateTime.Now
                    };

                    if (spec?.SeqMotivoBloqueio?.Count == 0)
                        spec.SeqMotivoBloqueio = null;

                    if (spec?.SeqTipoBloqueio?.Count == 0)
                        spec.SeqTipoBloqueio = null;

                    // Busca os bloqueios configurados para pessoa e pessoa atuação
                    pessoaPossuiBloqueios = this.Count(spec) > 0;
                }

                // Busca todos os bloqueios configurados para PessoaAtuacao (motivos que ambito = pessoa)
                if (bloqueiosConfiguradosPessoaAtuacao != null && bloqueiosConfiguradosPessoaAtuacao.Count() > 0)
                {
                    // Cria o specification
                    var spec = new PessoaAtuacaoBloqueioFilterSpecification
                    {
                        SeqMotivoBloqueio = bloqueiosConfiguradosPessoaAtuacao.Select(b => b.SeqMotivoBloqueio).ToList(),
                        SeqPessoaAtuacao = seqPessoaAtuacao,
                        SituacaoBloqueio = SituacaoBloqueio.Bloqueado,
                        DataBloqueioMenorOuIgualA = DateTime.Now
                    };

                    if (spec?.SeqMotivoBloqueio?.Count == 0)
                        spec.SeqMotivoBloqueio = null;

                    if (spec?.SeqTipoBloqueio?.Count == 0)
                        spec.SeqTipoBloqueio = null;

                    // Busca os bloqueios configurados para pessoa e pessoa atuação
                    var bloqueios = this.SearchBySpecification(spec).ToList();

                    pessoaAtuacaoPossuiBloqueios = bloqueios.Count > 0;
                }
            }
            return pessoaPossuiBloqueios || pessoaAtuacaoPossuiBloqueios;
        }

        public List<PessoaAtuacaoBloqueioVO> BuscarPessoaAtuacaoBloqueios(long seqPessoaAtuacao, IEnumerable<ConfiguracaoEtapaBloqueio> bloqueiosEtapa, bool bloqueioFimEtapa)
        {
            // Cria o retorno
            List<PessoaAtuacaoBloqueioVO> ret = new List<PessoaAtuacaoBloqueioVO>();

            // Busca todos os bloqueios que impedem iniciar uma etapa
            var tipoAtuacao = PessoaAtuacaoDomainService.SearchProjectionByKey(new SMCSeqSpecification<PessoaAtuacao>(seqPessoaAtuacao), x => x.TipoAtuacao);

            // Verifica se a etapa possui configuração de algum bloqueio
            if (bloqueiosEtapa.Count() > 0)
            {
                long? seqPessoa = null;

                // Verifica quais bloqueios são feitos por pessoa e quais por pessoa atuação (ambito)
                var bloqueiosConfiguradosPessoa = bloqueiosEtapa.Where(b => b.AmbitoBloqueio == AmbitoBloqueio.Pessoa);
                var bloqueiosConfiguradosPessoaAtuacao = bloqueiosEtapa.Where(b => b.AmbitoBloqueio == AmbitoBloqueio.PessoaAtuacao);

                // Recupera o sequencial da pessoa associada ao pessoa atuação caso tenha algum tipo de bloqueio configurado por pessoa
                if (bloqueiosConfiguradosPessoa != null && bloqueiosConfiguradosPessoa.Count() > 0)
                    seqPessoa = PessoaAtuacaoDomainService.SearchProjectionByKey(new SMCSeqSpecification<PessoaAtuacao>(seqPessoaAtuacao), p => p.SeqPessoa);

                /*Se o termo de intercâmbio associado à pessoa-atuação for de um tipo de termo de intercâmbio
                     parametrizado para conceder formação de acordo com a Instituição de Ensino logada,
                     Nível de Ensino e Vínculo da pessoa-atuação em questão.
                     Ou
                     Se o vínculo da pessoa-atuação for parametrizado para exigir parceria no ingresso de acordo
                     com a Instituição de Ensino logada e Nível de Ensino da pessoa-atuação em questão.*/

                bool vinculoIntercambio = false;
                long? seqTipoTermoConcedeFormacao = null;
                // Recupera os tipos de termo vinculados à pessoa atuação
                var seqsTiposTermoIntercambio = PessoaAtuacaoDomainService.SearchProjectionByKey(new SMCSeqSpecification<PessoaAtuacao>(seqPessoaAtuacao), x => x.TermosIntercambio.Select(t => t.TermoIntercambio.ParceriaIntercambioTipoTermo.SeqTipoTermoIntercambio));

                if (seqsTiposTermoIntercambio != null && seqsTiposTermoIntercambio.Any())
                {
                    //Recupera dados do vinculo de ingressante
                    InstituicaoNivelTipoVinculoAlunoVO dadosVinculo = InstituicaoNivelTipoVinculoAlunoDomainService.BuscarInstituicaoNivelTipoVinculoAlunoPorPessoaAtuacao(seqPessoaAtuacao);

                    if (dadosVinculo != null)
                    {
                        var dadosIntercambio = InstituicaoNivelTipoTermoIntercambioDomainService.BuscarInstituicoesNivelTipoVinculoAluno(new InstituicaoNivelTipoTermoIntercambioFilterSpecification() { SeqInstituicaoNivelTipoVinculoAluno = dadosVinculo.Seq });
                        if ((dadosIntercambio != null && dadosIntercambio.Count > 0 && dadosIntercambio.Any(c => c.ConcedeFormacao)))
                        {
                            var dadosConcede = dadosIntercambio.FirstOrDefault(c => c.ConcedeFormacao);
                            if (seqsTiposTermoIntercambio.Contains(dadosConcede.SeqTipoTermoIntercambio))
                            {
                                vinculoIntercambio = true;
                                seqTipoTermoConcedeFormacao = dadosConcede.SeqTipoTermoIntercambio;
                            }
                        }

                        // faz a segunda validação caso ainda não seja vinculoIntercambio = true
                        if (!vinculoIntercambio && dadosVinculo.ExigeParceriaIntercambioIngresso.GetValueOrDefault())
                        {
                            vinculoIntercambio = true;
                        }
                    }
                }

                // Cria a expression que vai ser usada para ambas consultas na projeção
                Expression<Func<PessoaAtuacaoBloqueio, PessoaAtuacaoBloqueioVO>> expression = (x) => new PessoaAtuacaoBloqueioVO
                {
                    Comprovantes = x.Comprovantes,
                    DataAlteracao = x.DataAlteracao,
                    DataDesbloqueioEfetivo = x.DataDesbloqueioEfetivo,
                    DataDesbloqueioTemporario = x.DataDesbloqueioTemporario,
                    DataInclusao = x.DataInclusao,
                    Descricao = x.Descricao,
                    DataBloqueio = x.DataBloqueio,
                    JustificativaDesbloqueio = x.JustificativaDesbloqueio,
                    MotivoBloqueio = x.MotivoBloqueio,
                    Observacao = x.Observacao,
                    PessoaAtuacao = x.PessoaAtuacao,
                    Seq = x.Seq,
                    SeqMotivoBloqueio = x.SeqMotivoBloqueio,
                    SeqPessoaAtuacao = x.SeqPessoaAtuacao,
                    SituacaoBloqueio = x.SituacaoBloqueio,
                    TipoDesbloqueio = x.TipoDesbloqueio,
                    UsuarioAlteracao = x.UsuarioAlteracao,
                    UsuarioDesbloqueioEfetivo = x.UsuarioDesbloqueioEfetivo,
                    UsuarioDesbloqueioTemporario = x.UsuarioDesbloqueioTemporario,
                    UsuarioInclusao = x.UsuarioInclusao,
                    DescricaoVinculo = vinculoIntercambio ? ((x.PessoaAtuacao is Aluno) ? (x.PessoaAtuacao as Aluno).TipoVinculoAluno.Descricao : (x.PessoaAtuacao as Ingressante).TipoVinculoAluno.Descricao) + " - " + x.PessoaAtuacao.TermosIntercambio.FirstOrDefault(a => !seqTipoTermoConcedeFormacao.HasValue || a.TermoIntercambio.ParceriaIntercambioTipoTermo.SeqTipoTermoIntercambio == seqTipoTermoConcedeFormacao).TermoIntercambio.Descricao : (x.PessoaAtuacao is Aluno) ? (x.PessoaAtuacao as Aluno).TipoVinculoAluno.Descricao : (x.PessoaAtuacao as Ingressante).TipoVinculoAluno.Descricao,
                    NumeroIngressante = (x.PessoaAtuacao as Ingressante).Seq,
                    RegistroAcademico = (x.PessoaAtuacao as Aluno).NumeroRegistroAcademico,
                    DescricaoTipoBloqueio = x.MotivoBloqueio.TipoBloqueio.Descricao,
                    //DescricaoReferenciaAtuacao = x.PessoaAtuacao.Descricao, // De acordo com a Ellen deve exibir a propria descrição de referencia do bloqueio e não a da pessoa atuação
                    DescricaoReferenciaAtuacao = x.DescricaoReferenciaAtuacao,
                    PermiteDesbloqueioTemporarioEfetivacao = x.MotivoBloqueio.Token == TOKEN_TIPO_PESSOAATUACAOBLOQUEIO.PARCELA_MATRICULA_PENDENTE || x.MotivoBloqueio.Token == TOKEN_TIPO_PESSOAATUACAOBLOQUEIO.PARCELA_PRE_MATRICULA_PENDENTE || x.MotivoBloqueio.Token == TOKEN_TIPO_PESSOAATUACAOBLOQUEIO.PARCELA_SERVICO_ADICIONAL_PENDENTE,
                    Itens = x.Itens
                };

                // Busca todos os bloqueios configurados para Pessoa (motivos que ambito = pessoa)
                if (bloqueiosConfiguradosPessoa != null && bloqueiosConfiguradosPessoa.Count() > 0)
                {
                    // Cria o specification
                    var spec = new PessoaAtuacaoBloqueioFilterSpecification
                    {
                        SeqMotivoBloqueio = bloqueiosConfiguradosPessoa.Select(b => b.SeqMotivoBloqueio).ToList(),
                        SeqPessoa = seqPessoa,
                        SituacaoBloqueio = SituacaoBloqueio.Bloqueado,
                        DataBloqueioMenorOuIgualA = DateTime.Now
                    };

                    if (spec?.SeqMotivoBloqueio?.Count == 0)
                        spec.SeqMotivoBloqueio = null;

                    if (spec?.SeqTipoBloqueio?.Count == 0)
                        spec.SeqTipoBloqueio = null;

                    // Busca os bloqueios configurados para pessoa e pessoa atuação
                    ret.AddRange(this.SearchProjectionBySpecification(spec, expression));

                    // Verificar bloqueios do legado
                    /*
                        * SGA legado - identificar os alunos do legado através dos dados da pessoa atuação: cpf ou passaporte, prenome, primeiro sobrenome e data de nascimento. Verificar se algum dos alunos encontrados possua o respectivo bloqueio. Se existir, exibir suas informações.
                        * CAD legado - Se o bloqueio parametrizado for "Sanção disciplinar de de desligamento", identificar os respectivos códigos de pessoa do CAD e verificar se a pessoa do CAD existe esse bloqueio. Se existir, exibir suas informações.
                    */
                }

                // Busca todos os bloqueios configurados para PessoaAtuacao (motivos que ambito = pessoa)
                if (bloqueiosConfiguradosPessoaAtuacao != null && bloqueiosConfiguradosPessoaAtuacao.Count() > 0)
                {
                    // Cria o specification
                    var spec = new PessoaAtuacaoBloqueioFilterSpecification
                    {
                        SeqMotivoBloqueio = bloqueiosConfiguradosPessoaAtuacao.Select(b => b.SeqMotivoBloqueio).ToList(),
                        SeqPessoaAtuacao = seqPessoaAtuacao,
                        SituacaoBloqueio = SituacaoBloqueio.Bloqueado,
                        DataBloqueioMenorOuIgualA = DateTime.Now
                    };

                    if (spec?.SeqMotivoBloqueio?.Count == 0)
                        spec.SeqMotivoBloqueio = null;

                    if (spec?.SeqTipoBloqueio?.Count == 0)
                        spec.SeqTipoBloqueio = null;

                    // Busca os bloqueios configurados para pessoa e pessoa atuação
                    ret.AddRange(this.SearchProjectionBySpecification(spec, expression));
                }
            }

            return ret;
        }

        public List<PessoaAtuacaoBloqueioVO> BuscarPessoaAtuacaoBloqueios(long seqPessoaAtuacao, long seqConfiguracaoEtapa, bool bloqueioFimEtapa)
        {
            // Busca todos os bloqueios que impedem iniciar uma etapa
            var bloqueiosEtapa = ConfiguracaoEtapaDomainService.SearchProjectionByKey(new SMCSeqSpecification<ConfiguracaoEtapa>(seqConfiguracaoEtapa), x => x.ConfiguracoesBloqueio.Where(b => bloqueioFimEtapa ? b.ImpedeFimEtapa : b.ImpedeInicioEtapa));
            return BuscarPessoaAtuacaoBloqueios(seqPessoaAtuacao, bloqueiosEtapa, bloqueioFimEtapa);
        }

        public long SalvarPessoaAtuacaoBloqueio(PessoaAtuacaoBloqueioVO modelo)
        {
            modelo.SeqMotivoBloqueio = modelo.SeqMotivoBloqueioAuxiliarSalvar;

            var motivoBloqueio = MotivoBloqueioDomainService.SearchByKey(new SMCSeqSpecification<MotivoBloqueio>(modelo.SeqMotivoBloqueio));

            // Caso seja obrigatório exibir os itens de bloqueio, deve ser informado ao menos um item.
            if (motivoBloqueio != null && motivoBloqueio.PermiteItem)
            {
                if (modelo.Itens == null || modelo.Itens.Count == 0)
                    throw new PessoaAtuacaoBloqueioItemRequeridoException();
            }

            var dominio = modelo.Transform<PessoaAtuacaoBloqueio>();

            //Caso o motivo de bloqueio esteja configurado para permitir itens de bloqueio, e não exista descricao
            //cadastrada, concatenar no campo descrição, as descrições dos itens que foram informados, uma vez que
            //o campo descrição é obrigatório no banco de dados
            if (string.IsNullOrEmpty(dominio.Descricao) && motivoBloqueio.PermiteItem)
            {
                dominio.Descricao = string.Join(" | ", dominio.Itens.Select(i => i.Descricao));
            }

            this.SaveEntity(dominio);

            return dominio.Seq;
        }

        /// <summary>
        /// Verifica se a pessoa CAD possui pendencia na biblioteca.
        /// Caso possua pendência e não existe bloqueio referente a pendência, cria o bloqueio.
        /// Caso não possua pendência e existe bloqueio referente a pendência, libera o bloqueio.
        /// </summary>
        /// <param name="seqPessoaAtuacao">Pessoa-atuação a ser verificado</param>
        public void VerificaBloqueioPendenciaBiblioteca(long seqPessoaAtuacao)
        {
            // Busca os dados da pessoa-atuacao
            var specPessoaAtuacao = new PessoaAtuacaoFilterSpecification() { Seq = seqPessoaAtuacao };
            var pessoaAtuacao = PessoaAtuacaoDomainService.SearchProjectionByKey(specPessoaAtuacao, x => new
            {
                SeqPessoa = x.SeqPessoa,
                Descricao = x.Descricao
            });

            // Busca os bloqueio de pendencia de biblioteca da pessoa-atuação
            var specPABloqueio = new PessoaAtuacaoBloqueioFilterSpecification()
            {
                SeqPessoaAtuacao = seqPessoaAtuacao,
                TokenMotivoBloqueio = TOKEN_MOTIVO_BLOQUEIO.MATERIAL_PENDENTE_BIBLIOTECA,
                BloqueadoOuDesbloqueadoTemporariamente = true
            };
            var bloqueio = this.SearchByKey(specPABloqueio, IncludesPessoaAtuacaoBloqueio.Itens);

            // Descobre o código de pessoa CAD da pessoa atuação
            int codigoPessoaCAD = this.PessoaDomainService.BuscarCodigoDePessoaNosDadosMestres(pessoaAtuacao.SeqPessoa, TipoPessoa.Fisica, null);

            // Chama rotina do IntegracaoAcademico para verificar as pendencias de biblioteca
            var listaPendencia = IntegracaoAcademicoService.ConsultaPendenciaBiblioteca(codigoPessoaCAD);

            // Caso tenha pendencia na biblioteca... 
            if (listaPendencia != null && listaPendencia.Count > 0)
            {
                // Caso não exista bloqueio, cria
                if (bloqueio == null)
                {
                    PessoaAtuacaoBloqueio novoBloqueio = new PessoaAtuacaoBloqueio()
                    {
                        SeqPessoaAtuacao = seqPessoaAtuacao,
                        SeqMotivoBloqueio = this.MotivoBloqueioDomainService.BuscarSeqMotivoBloqueioPorToken(TOKEN_MOTIVO_BLOQUEIO.MATERIAL_PENDENTE_BIBLIOTECA),
                        Descricao = "Material pendente na biblioteca",
                        SituacaoBloqueio = SituacaoBloqueio.Bloqueado,
                        DataBloqueio = DateTime.Now,
                        DescricaoReferenciaAtuacao = pessoaAtuacao.Descricao,
                        CadastroIntegracao = true,
                        Itens = new List<PessoaAtuacaoBloqueioItem>()
                    };
                    foreach (var pendencia in listaPendencia)
                    {
                        PessoaAtuacaoBloqueioItem item = new PessoaAtuacaoBloqueioItem()
                        {
                            Descricao = pendencia.Descricao,
                            SituacaoBloqueio = SituacaoBloqueio.Bloqueado,
                            CodigoIntegracaoSistemaOrigem = pendencia.CodigoExemplar.ToString()
                        };
                        novoBloqueio.Itens.Add(item);
                    }
                    this.SaveEntity(novoBloqueio);
                }
                // Já existe o bloqueio, confere se deve incluir ou desbloquear algum item
                else
                {
                    bool alterouBloqueio = false;

                    // Percorre os itens do bloqueio verificando se tem algum item a ser desbloqueado
                    foreach (var item in bloqueio.Itens)
                    {
                        if ((item.SituacaoBloqueio == SituacaoBloqueio.Bloqueado) &&
                            (!listaPendencia.Any(p => p.CodigoExemplar.ToString().Equals(item.CodigoIntegracaoSistemaOrigem))))
                        {
                            item.SituacaoBloqueio = SituacaoBloqueio.Desbloqueado;
                            item.DataDesbloqueio = DateTime.Now;
                            item.UsuarioDesbloqueio = SMCContext.User.Identity.Name;
                            alterouBloqueio = true;
                        }
                    }
                    // Percorre os itens da pendencia verificando se existe novas pendencias
                    foreach (var pendencia in listaPendencia)
                    {
                        if (!bloqueio.Itens.Any(i => i.CodigoIntegracaoSistemaOrigem.Equals(pendencia.CodigoExemplar.ToString())))
                        {
                            PessoaAtuacaoBloqueioItem novoItem = new PessoaAtuacaoBloqueioItem()
                            {
                                SeqPessoaAtuacaoBloqueio = bloqueio.Seq,
                                Descricao = pendencia.Descricao,
                                SituacaoBloqueio = SituacaoBloqueio.Bloqueado,
                                CodigoIntegracaoSistemaOrigem = pendencia.CodigoExemplar.ToString()
                            };
                            bloqueio.Itens.Add(novoItem);
                            alterouBloqueio = true;
                        }
                    }
                    if (alterouBloqueio)
                        this.SaveEntity(bloqueio);
                }
            }
            // Caso tenha bloqueio e não tem pendência na biblioteca, libera o bloqueio.
            else
            {
                if (bloqueio != null)
                {
                    foreach (var item in bloqueio.Itens.Where(i => i.SituacaoBloqueio == SituacaoBloqueio.Bloqueado))
                    {
                        item.SituacaoBloqueio = SituacaoBloqueio.Desbloqueado;
                        item.DataDesbloqueio = DateTime.Now;
                        item.UsuarioDesbloqueio = SMCContext.User.Identity.Name;
                    }
                    bloqueio.SituacaoBloqueio = SituacaoBloqueio.Desbloqueado;
                    bloqueio.TipoDesbloqueio = TipoDesbloqueio.Efetivo;
                    bloqueio.JustificativaDesbloqueio = "Desbloqueado por entrega de material pendente na biblioteca";
                    bloqueio.DataDesbloqueioEfetivo = DateTime.Now;
                    bloqueio.UsuarioDesbloqueioEfetivo = SMCContext.User.Identity.Name;
                    this.SaveEntity(bloqueio);
                }
            }
        }

        /// <summary>
        /// Verifica as pendências de material da biblioteca de forma automática
        /// </summary>
        /// <param name="filtro">Filtros para verificação</param>
        public void VerificaBloqueioPendenciaBibliotecaAutomatica(VerificarPendenciaBibliotecaSATVO filtro)
        {
            var servico = Create<Jobs.VerificarBloqueioPendenciaBibliotecaWebJob>();
            servico.Execute(filtro);
        }

        /// <summary>
        /// Bloquear o bloqueio do aluno 
        /// </summary>
        /// <param name="model">VO de Pessoa Atuação Bloqueio recebendo alguns campos de acordo com a regra definida</param>
        /// <param name="motivoBloqueio">string contida no enum TOKEN_MOTIVO_BLOQUEIO</param>
        /// <param name="cadastroIntegracao">Se é cadastro integração ou não</param>
        public void BloquearBloqueioAluno(PessoaAtuacaoBloqueioVO model, string motivoBloqueio, bool? cadastroIntegracao = null, DateTime? dataBloqueio = null)
        {
            var specBloqueio = new PessoaAtuacaoBloqueioFilterSpecification()
            {
                SeqPessoaAtuacao = model.SeqPessoaAtuacao,
                TokenMotivoBloqueio = motivoBloqueio
            };

            var bloqueio = this.SearchByKey(specBloqueio);

            if (bloqueio != null)
            {
                bloqueio.SituacaoBloqueio = model.SituacaoBloqueio;
                bloqueio.DataDesbloqueioEfetivo = null;
                bloqueio.UsuarioDesbloqueioEfetivo = null;
                bloqueio.JustificativaDesbloqueio = null;
                bloqueio.TipoDesbloqueio = null;
                bloqueio.Observacao = model.Observacao;

                if (cadastroIntegracao.HasValue)
                    bloqueio.CadastroIntegracao = cadastroIntegracao.Value;

                if(dataBloqueio.HasValue)
                    bloqueio.DataBloqueio = dataBloqueio.Value;

                this.UpdateFields(bloqueio, b => b.Observacao,
                    b => b.DataDesbloqueioEfetivo,
                    b => b.UsuarioDesbloqueioEfetivo,
                    b => b.JustificativaDesbloqueio,
                    b => b.TipoDesbloqueio,
                    b => b.SituacaoBloqueio,
                    b => b.DataBloqueio,
                    b => b.CadastroIntegracao);
            };
        }


        //1.2.1.2.Caso não existir, associar à pessoa-atuação o bloqueio que possui o token
        //IMPEDIMENTO_RENOVACAO_PRAZO_CONCLUSAO, de acordo com os valores:
        //-Pessoa-atuação: sequencial da pessoa - atuação em questão
        //-Motivo de bloqueio: IMPEDIMENTO_RENOVACAO_PRAZO_CONCLUSAO
        //- Descrição: “Impedimento de matrícula - Prazo de conclusão de curso encerrado - Segundo depósito”
        //-Situação: bloqueado
        //- Data de bloqueio: data corrente do sistema
        //- Descrição de referência da atuação: descrição da pessoa atuação em questão
        //-Cadastro por integração: sim
        //- Solicitação de serviço: nulo
        public void CriarBloqueioAluno(PessoaAtuacaoBloqueioVO model, string motivoBloqueio, bool? cadastroIntegracao = null, DateTime? dataBloqueio = null)
        {
            var motivoBloqueioSpec = new MotivoBloqueioFilterSpecification { Token = motivoBloqueio };
            var motivo = MotivoBloqueioDomainService.SearchByKey(motivoBloqueioSpec);

            var pessoaAtuacaoBloqueio = new PessoaAtuacaoBloqueio
            {
                CadastroIntegracao = cadastroIntegracao.HasValue ? cadastroIntegracao.Value : false,
                SeqPessoaAtuacao = model.SeqPessoaAtuacao,
                Descricao = "Impedimento de matrícula - Prazo de conclusão de curso encerrado - Segundo depósito",
                DataBloqueio = model.DataBloqueio,
                SituacaoBloqueio = SituacaoBloqueio.Bloqueado,
                SeqMotivoBloqueio = motivo.Seq
            };

            this.SaveEntity(pessoaAtuacaoBloqueio);
        }
    }
}