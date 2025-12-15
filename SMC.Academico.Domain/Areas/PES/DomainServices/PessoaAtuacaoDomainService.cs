using SMC.Academico.Common.Areas.ALN.Constants;
using SMC.Academico.Common.Areas.PES.Constants;
using SMC.Academico.Common.Areas.PES.Enums;
using SMC.Academico.Common.Areas.PES.Exceptions;
using SMC.Academico.Common.Areas.PES.Includes;
using SMC.Academico.Common.Areas.SRC.Constants;
using SMC.Academico.Common.Areas.SRC.Exceptions;
using SMC.Academico.Common.Constants;
using SMC.Academico.Domain.Areas.ALN.DomainServices;
using SMC.Academico.Domain.Areas.ALN.Models;
using SMC.Academico.Domain.Areas.ALN.ValueObjects;
using SMC.Academico.Domain.Areas.CSO.DomainServices;
using SMC.Academico.Domain.Areas.DCT.DomainServices;
using SMC.Academico.Domain.Areas.ORG.DomainServices;
using SMC.Academico.Domain.Areas.PES.Models;
using SMC.Academico.Domain.Areas.PES.Specifications;
using SMC.Academico.Domain.Areas.PES.ValueObjects;
using SMC.Academico.Domain.Areas.SRC.DomainServices;
using SMC.Academico.Domain.Areas.SRC.Models;
using SMC.Academico.Domain.Areas.SRC.Specifications;
using SMC.Academico.Domain.Areas.SRC.ValueObjects;
using SMC.DadosMestres.Common;
using SMC.DadosMestres.Common.Areas.PES.Enums;
using SMC.DadosMestres.ServiceContract.Areas.PES.Data;
using SMC.DadosMestres.ServiceContract.Areas.GED.Interfaces;
using SMC.Framework;
using SMC.Framework.Barcode;
using SMC.Framework.Exceptions;
using SMC.Framework.Extensions;
using SMC.Framework.Mapper;
using SMC.Framework.Model;
using SMC.Framework.Repository;
using SMC.Framework.Security.Util;
using SMC.Framework.Specification;
using SMC.Framework.UnitOfWork;
using SMC.Framework.Util;
using SMC.IntegracaoAcademico.ServiceContract.Areas.IAC.Interfaces;
using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using SMC.DadosMestres.ServiceContract.Areas.PES.Interfaces;

namespace SMC.Academico.Domain.Areas.PES.DomainServices
{
    public class PessoaAtuacaoDomainService : AcademicoContextDomain<PessoaAtuacao>
    {
        #region Services

        private IIntegracaoAcademicoService IntegracaoAcademicoService
        {
            get { return Create<IIntegracaoAcademicoService>(); }
        }

        private IIntegracaoDadoMestreService IntegracaoDadoMestreService
        {
            get { return Create<IIntegracaoDadoMestreService>(); }
        }

        private ITipoDocumentoService TipoDocumentoService
        {
            get { return Create<ITipoDocumentoService>(); }
        }

        #endregion Services

        #region DomainService

        private ViewDadosOrigemPessoaAtuacaoDomainService ViewDadosOrigemPessoaAtuacaoDomainService => Create<ViewDadosOrigemPessoaAtuacaoDomainService>();

        private InstituicaoNivelDomainService InstituicaoNivelDomainService => Create<InstituicaoNivelDomainService>();

        private InstituicaoNivelTipoVinculoAlunoDomainService InstituicaoNivelTipoVinculoAlunoDomainService => Create<InstituicaoNivelTipoVinculoAlunoDomainService>();

        private HierarquiaEntidadeItemDomainService HierarquiaEntidadeItemDomainService => Create<HierarquiaEntidadeItemDomainService>();

        private DocumentoRequeridoDomainService DocumentoRequeridoDomainService => Create<DocumentoRequeridoDomainService>();

        private SolicitacaoDocumentoRequeridoDomainService SolicitacaoDocumentoRequeridoDomainService => Create<SolicitacaoDocumentoRequeridoDomainService>();

        private SolicitacaoServicoDomainService SolicitacaoServicoDomainService => Create<SolicitacaoServicoDomainService>();

        private CursoDomainService CursoDomainService => Create<CursoDomainService>();

        private IngressanteDomainService IngressanteDomainService => Create<IngressanteDomainService>();

        private AlunoDomainService AlunoDomainService => Create<AlunoDomainService>();

        private AlunoHistoricoSituacaoDomainService AlunoHistoricoSituacaoDomainService => Create<AlunoHistoricoSituacaoDomainService>();

        private PessoaAtuacaoBloqueioDomainService PessoaAtuacaoBloqueioDomainService => Create<PessoaAtuacaoBloqueioDomainService>();
        private RegistroDocumentoDomainService RegistroDocumentoDomainService => Create<RegistroDocumentoDomainService>();

        private MotivoBloqueioDomainService MotivoBloqueioDomainService => Create<MotivoBloqueioDomainService>();

        private ColaboradorDomainService ColaboradorDomainService => Create<ColaboradorDomainService>();

        #endregion DomainService

        #region Querys

        private const string _queryDadosSimulados = "select seq_curso_oferta_localidade_turno as SeqCursoOfertaLocalidadeTurno, seq_curso_oferta_localidade as SeqCursoOfertaLocalidade, seq_origem_financeira as SeqOrigemFinanceira, seq_entidade_curso as SeqCurso from CSO.fn_simular_curso_aluno_di(@SEQ_ENTIDADE_VINCULO, @SEQ_NIVEL_ENSINO)";
        private const string _queryDadosOrigem = "select seq_pessoa_atuacao as SeqPessoaAtuacao, idt_dom_tipo_atuacao as TipoAtuacao, cod_aluno_migracao as CodigoAlunoMigracao, seq_tipo_vinculo_aluno as SeqTipoVinculoAluno, seq_entidade_instituicao as SeqInstituicaoEnsino, seq_aluno_historico as SeqAlunoHistorico, seq_nivel_ensino as SeqNivelEnsino, seq_curso_oferta_localidade_turno as SeqCursoOfertaLocalidadeTurno, seq_ciclo_letivo as SeqCicloLetivo, seq_entidade_vinculo as SeqEntidadeVinculo, seq_instituicao_nivel as SeqInstituicaoNivel, seq_entidade_curso_oferta_localidade as SeqCursoOfertaLocalidade, seq_origem_financeira as SeqOrigemFinanceira, seq_matriz_curricular_oferta as SeqMatrizCurricularOferta, seq_matriz_curricular as SeqMatrizCurricular, seq_entidade_curso as SeqCurso from PES.vw_dados_origem_pessoa_atuacao where seq_pessoa_atuacao = @SeqPessoaAtuacao";

        #endregion Querys

        /// <summary>
        /// Busca sequenciais de instituição nível e tipo vínculo de acordo com o ingressante
        /// </summary>
        /// <param name="seq">Sequencial do ingressante</param>
        /// <param name="desativarFiltroDados">Desabilita o filtro de dados de hierarquia_entidade_organizacional para atender o caso de entidades compartilhadas da tela de consulta de solicitações de serviço </param>
        /// <returns>Objeto com os sequenciais de instituição nível e tipo vínculo</returns>
        public (long SeqInstituicao, long SeqNivelEnsino, long SeqTipoVinculoAluno, long SeqInstituicaoNivelEnsino) BuscarInstituicaoNivelEnsinoESequenciaisPorPessoaAtuacao(long seq, bool desativarFiltroDados = false)
        {
            if (desativarFiltroDados)
            {
                this.DisableFilter(FILTER.HIERARQUIA_ENTIDADE_ORGANIZACIONAL);
                InstituicaoNivelDomainService.DisableFilter(FILTER.HIERARQUIA_ENTIDADE_ORGANIZACIONAL);
            }

            //Recupera os sequenciais do ingressante para recuperar os parâmetros de acordo com o vínculo
            var sequenciais = this.SearchProjectionByKey(new SMCSeqSpecification<PessoaAtuacao>(seq),
                                                         p => new
                                                         {
                                                             SeqNivelEnsino = (long?)(p as Ingressante).SeqNivelEnsino ?? (p as Aluno).Historicos.FirstOrDefault(h => h.Atual).SeqNivelEnsino,
                                                             SeqInstituicaoEnsino = p.Pessoa.SeqInstituicaoEnsino,
                                                             SeqTipoVinculoAluno = (long?)(p as Ingressante).SeqTipoVinculoAluno ?? (p as Aluno).SeqTipoVinculoAluno
                                                         });

            var seqInstituicaoNivelEnsino = InstituicaoNivelDomainService.BuscarSequencialInstituicaoNivelEnsino(sequenciais.SeqNivelEnsino, sequenciais.SeqInstituicaoEnsino);

            if (desativarFiltroDados)
            {
                InstituicaoNivelDomainService.EnableFilter(FILTER.HIERARQUIA_ENTIDADE_ORGANIZACIONAL);
                this.EnableFilter(FILTER.HIERARQUIA_ENTIDADE_ORGANIZACIONAL);
            }

            return (sequenciais.SeqInstituicaoEnsino, sequenciais.SeqNivelEnsino, sequenciais.SeqTipoVinculoAluno, seqInstituicaoNivelEnsino);
        }

        /// <summary>
        /// Recupera uma pessoa atuação com endereços eletrônicos e telefones.
        /// </summary>
        /// <param name="seqPessoaAtuacao">Sequencial de pessoa atuação</param>
        /// <returns>Pessoa atuação com os endereços eletrônicos e telefones associados</returns>
        public PessoaAtuacaoVO BuscarPessoaAtuacao(long seqPessoaAtuacao)
        {
            var includes = IncludesPessoaAtuacao.EnderecosEletronicos_EnderecoEletronico
                         | IncludesPessoaAtuacao.Telefones_Telefone;

            var pessoaAtuacao = this.SearchByKey(new SMCSeqSpecification<PessoaAtuacao>(seqPessoaAtuacao), includes);

            return pessoaAtuacao.Transform<PessoaAtuacaoVO>();
        }

        public void SalvarIngressanteDadosMestres(InformacoesPessoaVO pessoa)
        {
            IntegracaoDadoMestreData dadosOrigem = new IntegracaoDadoMestreData()
            {
                NomeBanco = TOKEN_DADOSMESTRES.BANCO_ACADEMICO,
                NomeTabela = TOKEN_DADOSMESTRES.BANCO_ACADEMICO_PESSOA,
                NomeAtributoChave = TOKEN_DADOSMESTRES.BANCO_ACADEMICO_PESSOA_SEQ,
                SeqAtributoChaveIntegracao = pessoa.SeqPessoa
            };

            var dataDadosMestres = SMCMapperHelper.Create<InserePessoaFisicaData>(pessoa, dadosOrigem);

            dataDadosMestres.Filiacao = new List<InserePessoaFisicaFiliacaoData>();
            foreach (var filiacao in pessoa.Filiacao)
            {
                dataDadosMestres.Filiacao.Add(new InserePessoaFisicaFiliacaoData()
                {
                    TipoParentesco = filiacao.TipoParentesco,
                    NomePessoaParentesco = filiacao.Nome
                });
            }
            var msgErro = IntegracaoDadoMestreService.InserePessoaFisica(dataDadosMestres);
            if (!string.IsNullOrWhiteSpace(msgErro))
            {
                throw new SMCApplicationException(msgErro);
            }
        }

        /// <summary>
        /// Recupera os dados de origem de uma pessoa atuação, considerando a simulação em caso de não possuir curso.
        /// </summary>
        /// <param name="seqPessoaAtuacao">Sequencial da pessoa atuação</param>
        /// <returns>Sequencial da origem e do código de serviço de origem</returns>
        public PessoaAtuacaoDadosOrigemVO RecuperaDadosOrigem(long seqPessoaAtuacao, bool desativarFiltroDados = false)
        {
            //DateTime dataInicioExecucao = DateTime.Now;

            if (desativarFiltroDados)
            {
                AlunoDomainService.DisableFilter(FILTER.HIERARQUIA_ENTIDADE_ORGANIZACIONAL);
                IngressanteDomainService.DisableFilter(FILTER.HIERARQUIA_ENTIDADE_ORGANIZACIONAL);
                this.DisableFilter(FILTER.HIERARQUIA_ENTIDADE_ORGANIZACIONAL);
            }

            // Busca os dados na view
            var dados = RawQuery<ViewDadosOrigemPessoaAtuacao>(_queryDadosOrigem, new SMCFuncParameter("@SeqPessoaAtuacao", seqPessoaAtuacao)).FirstOrDefault();

            long? CodServicoOrigem = null;
            long? SeqOrigem = null;
            long? SeqCursoOfertaLocalidadeTurno = dados.SeqCursoOfertaLocalidadeTurno;
            long? SeqCursoOfertaLocalidade = dados.SeqCursoOfertaLocalidade;
            long? SeqMatrizCurricularOferta = dados.SeqMatrizCurricularOferta;
            long? SeqMatrizCurricular = dados.SeqMatrizCurricular;
            long? SeqAlunoHistoricoAtual = dados.SeqAlunoHistorico;
            long? SeqCicloLetivo = dados.SeqCicloLetivo;
            long SeqEntidadeResponsavel = dados.SeqEntidadeVinculo.GetValueOrDefault();
            long SeqNivelEnsino = dados.SeqNivelEnsino;
            long SeqTipoVinculoAluno = dados.SeqTipoVinculoAluno;
            long SeqInstituicaoEnsino = dados.SeqInstituicaoEnsino;
            long? CodigoAlunoMigracao = dados.CodigoAlunoMigracao;
            long? SeqCurso = dados.SeqCurso;

            if (dados.TipoAtuacao == TipoAtuacao.Aluno)
            {
                // FIX: Verificar como vai ficar esta questão abaixo pois estamos mandando dados fixos para o GRA
                //CodServicoOrigem = dadosAluno.CodServicoOrigem;
                //SeqOrigem = dadosAluno.SeqOrigem;
                CodServicoOrigem = dados.CodigoAlunoMigracao.GetValueOrDefault() > 0 ? IntegracaoAcademicoService.BuscarCursoOfertaAlunoSGP(dados.CodigoAlunoMigracao.GetValueOrDefault()) : (long?)null;
                SeqOrigem = 1;
            }
            else if (dados.TipoAtuacao == TipoAtuacao.Ingressante)
            {
                CodServicoOrigem = dados.SeqCursoOfertaLocalidade;
                SeqOrigem = dados.SeqOrigemFinanceira;
            }

            /*- Origem:
                * É o seq_origem_financeira do curso oferta localidade do ingressante, quando este possuir curso.
                * Caso não possua, simular qual seria o seu respectivo curso, conforme o nível de ensino e entidade responsável que ele possui e, tipo de oferta de curso cujo token seja “INTERNO”.*/

            /*- Código do serviço origem:
			 * - Se o ingressante possuir curso associado, enviar como parâmetro o curso-oferta-localidade da oferta do ingressante
			 * - Se o ingressante não possuir curso associado, simular qual seria o seu respectivo curso, conforme o nível de ensino e entidade responsável que ele possui e, tipo de oferta de curso cujo token seja “INTERNO”. */
            if (SeqOrigem.GetValueOrDefault() == 0 || CodServicoOrigem.GetValueOrDefault() == 0 || SeqCursoOfertaLocalidadeTurno.GetValueOrDefault() == 0 || SeqCursoOfertaLocalidade.GetValueOrDefault() == 0)
            {
                var dadosSimulados = RawQuery<DadosSimuladosVO>(_queryDadosSimulados, new SMCFuncParameter("@SEQ_ENTIDADE_VINCULO", dados.SeqEntidadeVinculo),
                                                                    new SMCFuncParameter("@SEQ_NIVEL_ENSINO", dados.SeqNivelEnsino)).FirstOrDefault();

                if (SeqOrigem.GetValueOrDefault() == 0)
                    SeqOrigem = Convert.ToInt32(dadosSimulados?.SeqOrigemFinanceira ?? 0);

                if (CodServicoOrigem.GetValueOrDefault() == 0)
                    CodServicoOrigem = dadosSimulados?.SeqCursoOfertaLocalidade;

                if (SeqCursoOfertaLocalidadeTurno.GetValueOrDefault() == 0)
                    SeqCursoOfertaLocalidadeTurno = dadosSimulados?.SeqCursoOfertaLocalidadeTurno;

                if (SeqCursoOfertaLocalidade.GetValueOrDefault() == 0)
                    SeqCursoOfertaLocalidade = dadosSimulados?.SeqCursoOfertaLocalidade;

                if (SeqCurso.GetValueOrDefault() == 0)
                    SeqCurso = dadosSimulados?.SeqCurso;
            }

            PessoaAtuacaoDadosOrigemVO dadosOrigem = new PessoaAtuacaoDadosOrigemVO()
            {
                SeqOrigem = SeqOrigem.GetValueOrDefault(),
                CodigoServicoOrigem = CodServicoOrigem.GetValueOrDefault(),
                SeqCursoOfertaLocalidadeTurno = SeqCursoOfertaLocalidadeTurno.GetValueOrDefault(),
                SeqNivelEnsino = SeqNivelEnsino,
                SeqTipoVinculoAluno = SeqTipoVinculoAluno,
                SeqMatrizCurricularOferta = SeqMatrizCurricularOferta.GetValueOrDefault(),
                TipoAtuacao = dados.TipoAtuacao,
                SeqAlunoHistoricoAtual = SeqAlunoHistoricoAtual.GetValueOrDefault(),
                SeqCicloLetivo = SeqCicloLetivo,
                SeqInstituicaoEnsino = SeqInstituicaoEnsino,
                SeqCursoOfertaLocalidade = SeqCursoOfertaLocalidade.GetValueOrDefault(),
                CodigoAlunoMigracao = CodigoAlunoMigracao,
                SeqMatrizCurricular = SeqMatrizCurricular,
                SeqCurso = SeqCurso.GetValueOrDefault(),
                SeqEntidadeResponsavel = SeqEntidadeResponsavel
            };

            if (desativarFiltroDados)
            {
                AlunoDomainService.EnableFilter(FILTER.HIERARQUIA_ENTIDADE_ORGANIZACIONAL);
                IngressanteDomainService.EnableFilter(FILTER.HIERARQUIA_ENTIDADE_ORGANIZACIONAL);
                this.EnableFilter(FILTER.HIERARQUIA_ENTIDADE_ORGANIZACIONAL);
            }

            //DateTime dataFimExecucao = DateTime.Now;
            //TimeSpan span = dataFimExecucao - dataInicioExecucao;
            //File.AppendAllText(@"C:\temp\tempo.txt", $"Usuário {SMCContext.User.Identity.Name}: Tempo de execução: " + span.TotalMilliseconds + "\n");

            return dadosOrigem;
        }

        public string RecuperarDescricaoVinculo(long seqPessoaAtuacao, IEnumerable<long> seqsTipoTermoIntercambioAssociados, string descricaoVinculo)
        {
            /*RN_ALN_029 Descrição Vínculo Tipo Termo Intercâmbio
               Caso a regra RN_ALN_031  - Consistência Vínculo Tipo Termo Intercâmbio ocorra, exibir a descrição do vínculo:  "Vínculo" + "-" + "Tipo de Termo de Intercâmbio"

              RN_ALN_031  Consistência Vínculo Tipo Termo Intercâmbio
               Se o termo de intercâmbio associado à pessoa-atuação for de um tipo de termo de intercâmbio parametrizado para conceder formação de acordo com a Instituição de Ensino logada, Nível de Ensino e Vínculo da pessoa-atuação em questão.
               Ou
               Se o vínculo da pessoa-atuação for parametrizado para exigir parceria no ingresso de acordo com a Instituição de Ensino logada e Nível de Ensino da pessoa-atuação em questão.*/

            string ret = descricaoVinculo;
            if (seqsTipoTermoIntercambioAssociados != null && seqsTipoTermoIntercambioAssociados.Any())
            {
                var instituicoesNivelTipoVinculoAluno = InstituicaoNivelTipoVinculoAlunoDomainService.BuscarInstituicaoNivelTipoVinculoAlunoPorPessoaAtuacao(seqPessoaAtuacao);
                var tipoTermo = instituicoesNivelTipoVinculoAluno.TiposTermoIntercambio.Where(t => seqsTipoTermoIntercambioAssociados.Contains(t.SeqTipoTermoIntercambio)).FirstOrDefault(t => t.ConcedeFormacao);

                if (tipoTermo != null)
                    ret += " - " + tipoTermo.TipoTermoIntercambio.Descricao;
                else if (instituicoesNivelTipoVinculoAluno.ExigeParceriaIntercambioIngresso.GetValueOrDefault())
                {
                    var dscRert = instituicoesNivelTipoVinculoAluno.TiposTermoIntercambio.FirstOrDefault(t => seqsTipoTermoIntercambioAssociados.Contains(t.SeqTipoTermoIntercambio))?.TipoTermoIntercambio.Descricao;
                    if (string.IsNullOrEmpty(dscRert))
                        dscRert = instituicoesNivelTipoVinculoAluno.TiposTermoIntercambio.FirstOrDefault().TipoTermoIntercambio.Descricao;
                    ret += " - " + dscRert;
                }
            }
            return ret;
        }

        public string RecuperarDescricaoVinculo(InstituicaoNivelTipoVinculoAlunoVO instituicaoNivelTipoVinculoAluno, IEnumerable<long> seqsTipoTermoIntercambioAssociados, string descricaoVinculo)
        {
            /*RN_ALN_029 Descrição Vínculo Tipo Termo Intercâmbio
               Caso a regra RN_ALN_031  - Consistência Vínculo Tipo Termo Intercâmbio ocorra, exibir a descrição do vínculo:  "Vínculo" + "-" + "Tipo de Termo de Intercâmbio"

              RN_ALN_031  Consistência Vínculo Tipo Termo Intercâmbio
               Se o termo de intercâmbio associado à pessoa-atuação for de um tipo de termo de intercâmbio parametrizado para conceder formação de acordo com a Instituição de Ensino logada, Nível de Ensino e Vínculo da pessoa-atuação em questão.
               Ou
               Se o vínculo da pessoa-atuação for parametrizado para exigir parceria no ingresso de acordo com a Instituição de Ensino logada e Nível de Ensino da pessoa-atuação em questão.*/

            string ret = descricaoVinculo;
            if (seqsTipoTermoIntercambioAssociados != null && seqsTipoTermoIntercambioAssociados.Any())
            {
                var tipoTermo = instituicaoNivelTipoVinculoAluno.TiposTermoIntercambio.Where(t => seqsTipoTermoIntercambioAssociados.Contains(t.SeqTipoTermoIntercambio)).FirstOrDefault(t => t.ConcedeFormacao);

                if (tipoTermo != null)
                    ret += " - " + tipoTermo.TipoTermoIntercambio.Descricao;
                else if (instituicaoNivelTipoVinculoAluno.ExigeParceriaIntercambioIngresso.GetValueOrDefault())
                    ret += " - " + instituicaoNivelTipoVinculoAluno.TiposTermoIntercambio.FirstOrDefault().TipoTermoIntercambio.Descricao;
            }
            return ret;
        }

        public string RecuperarDescricaoVinculo(InstituicaoNivelTipoVinculoAluno instituicaoNivelTipoVinculoAluno, IEnumerable<long> seqsTipoTermoIntercambioAssociados, string descricaoVinculo)
        {
            /*RN_ALN_029 Descrição Vínculo Tipo Termo Intercâmbio
               Caso a regra RN_ALN_031  - Consistência Vínculo Tipo Termo Intercâmbio ocorra, exibir a descrição do vínculo:  "Vínculo" + "-" + "Tipo de Termo de Intercâmbio"

              RN_ALN_031  Consistência Vínculo Tipo Termo Intercâmbio
               Se o termo de intercâmbio associado à pessoa-atuação for de um tipo de termo de intercâmbio parametrizado para conceder formação de acordo com a Instituição de Ensino logada, Nível de Ensino e Vínculo da pessoa-atuação em questão.
               Ou
               Se o vínculo da pessoa-atuação for parametrizado para exigir parceria no ingresso de acordo com a Instituição de Ensino logada e Nível de Ensino da pessoa-atuação em questão.*/

            string ret = descricaoVinculo;
            if (seqsTipoTermoIntercambioAssociados != null && seqsTipoTermoIntercambioAssociados.Any())
            {
                var tipoTermo = instituicaoNivelTipoVinculoAluno.TiposTermoIntercambio.Where(t => seqsTipoTermoIntercambioAssociados.Contains(t.SeqTipoTermoIntercambio)).FirstOrDefault(t => t.ConcedeFormacao);

                if (tipoTermo != null)
                    ret += " - " + tipoTermo.TipoTermoIntercambio.Descricao;
                else if (instituicaoNivelTipoVinculoAluno.ExigeParceriaIntercambioIngresso)
                    ret += " - " + instituicaoNivelTipoVinculoAluno.TiposTermoIntercambio.FirstOrDefault().TipoTermoIntercambio.Descricao;
            }
            return ret;
        }

        /// <summary>
        /// Buscar o tipo de atuação da pessoa atuação para validações de aluno e ingressante
        /// </summary>
        /// <param name="seqPessoaAtuacao">Sequencial da pessoa atuação</param>
        /// <param name="desativarFiltroDados">Desabilita o filtro de dados de hierarquia_entidade_organizacional para atender o caso de entidades compartilhadas da tela de consulta de solicitações de serviço </param>
        /// <returns>O tipo de atuação da pessoa</returns>
        public TipoAtuacao BuscarTipoAtuacaoPessoaAtuacao(long seqPessoaAtuacao, bool desativarFiltroDados = false)
        {
            if (desativarFiltroDados)
                this.DisableFilter(FILTER.HIERARQUIA_ENTIDADE_ORGANIZACIONAL);

            var tipoAtuacao = this.SearchProjectionByKey(new SMCSeqSpecification<PessoaAtuacao>(seqPessoaAtuacao), x => x.TipoAtuacao);

            if (desativarFiltroDados)
                this.EnableFilter(FILTER.HIERARQUIA_ENTIDADE_ORGANIZACIONAL);

            return tipoAtuacao;
        }

        public PessoaAtuacaoVisualizacaoDocumentoVO BuscarDocumentosPessoaAtuacao(long seqPessoaAtuacao)
        {
            var tiposDocumentos = TipoDocumentoService.BuscarTiposDocumentos();

            var ret = new PessoaAtuacaoVisualizacaoDocumentoVO() { TiposDocumento = new List<PessoaAtuacaoTipoDocumentoVO>() };

            var spec = new SMCSeqSpecification<PessoaAtuacao>(seqPessoaAtuacao);

            var resultPessoaAtuacao = this.SearchProjectionByKey(spec, p => p.SolicitacoesServico.SelectMany(s => s.DocumentosRequeridos).Select(d => new
            {
                SeqArquivoAnexado = d.SeqArquivoAnexado,
                UidArquivoAnexado = d.SeqArquivoAnexado.HasValue ? d.ArquivoAnexado.UidArquivo : null,
                SeqSolicitacaoServico = (long?)d.SolicitacaoServico.Seq,
                SeqPessoaAtuacao = (long?)d.SolicitacaoServico.SeqPessoaAtuacao,
                SeqTipoDocumento = (long?)d.DocumentoRequerido.SeqTipoDocumento,
                SituacaoEntregaDocumento = (SituacaoEntregaDocumento?)d.SituacaoEntregaDocumento,
                DataPrazoEntrega = d.DataPrazoEntrega,
                DataEntrega = d.DataEntrega,
                VersaoDocumento = d.VersaoDocumento,
                FormaEntregaDocumento = d.FormaEntregaDocumento,
                Observacao = d.Observacao,
                SolicitacaoServico = new
                {
                    SeqPessoaAtuacao = (long?)d.SolicitacaoServico.SeqPessoaAtuacao,
                    SeqSolicitacaoServico = (long?)d.SeqSolicitacaoServico,
                    DescricaoSolicitacaoServico = d.SolicitacaoServico.NumeroProtocolo + " - " + d.SolicitacaoServico.ConfiguracaoProcesso.Processo.Descricao,
                    SeqTipoSocumento = (long?)d.DocumentoRequerido.SeqTipoDocumento,
                    TipoDocumento = new
                    {
                        SeqPessoaAtuacao = (long?)d.SolicitacaoServico.SeqPessoaAtuacao,
                        SeqTipoDocumento = (long?)d.DocumentoRequerido.SeqTipoDocumento,
                    }
                },
            })).ToList();

            //Apos recuperar os dados da pessoa atuação, descobre qual é o ingressante do histórico mais antigo do aluno referente a pessoa atuação
            //e unifica os dados, caso existam solicitações do o ingressante encontrado

            //Cria o spec do aluno, usando o seq da pessoa atuação
            var specAluno = new SMCSeqSpecification<Aluno>(seqPessoaAtuacao);

            //Recupera o seq do ingressante do historico mais antigo do aluno, da pessao atuação
            var seqIngressante = this.AlunoDomainService.SearchProjectionByKey(specAluno, p => p.Historicos.OrderBy(d => d.DataInclusao).FirstOrDefault().SeqIngressante);

            //Se encontrado ingressante
            if (seqIngressante.HasValue)
            {
                //Atualiza o spec passando o ingressante encontrado
                spec = new SMCSeqSpecification<PessoaAtuacao>(seqIngressante.GetValueOrDefault());

                //Recupera os dados do ingressante
                var resultIngressante = this.SearchProjectionByKey(spec, p => p.SolicitacoesServico.SelectMany(s => s.DocumentosRequeridos).Select(d => new
                {
                    SeqArquivoAnexado = d.SeqArquivoAnexado,
                    UidArquivoAnexado = d.SeqArquivoAnexado.HasValue ? d.ArquivoAnexado.UidArquivo : null,
                    SeqSolicitacaoServico = (long?)d.SolicitacaoServico.Seq,
                    SeqPessoaAtuacao = (long?)d.SolicitacaoServico.SeqPessoaAtuacao,
                    SeqTipoDocumento = (long?)d.DocumentoRequerido.SeqTipoDocumento,
                    SituacaoEntregaDocumento = (SituacaoEntregaDocumento?)d.SituacaoEntregaDocumento,
                    DataPrazoEntrega = d.DataPrazoEntrega,
                    DataEntrega = d.DataEntrega,
                    VersaoDocumento = d.VersaoDocumento,
                    FormaEntregaDocumento = d.FormaEntregaDocumento,
                    Observacao = d.Observacao,
                    SolicitacaoServico = new
                    {
                        SeqPessoaAtuacao = (long?)d.SolicitacaoServico.SeqPessoaAtuacao,
                        SeqSolicitacaoServico = (long?)d.SeqSolicitacaoServico,
                        DescricaoSolicitacaoServico = d.SolicitacaoServico.NumeroProtocolo + " - " + d.SolicitacaoServico.ConfiguracaoProcesso.Processo.Descricao,
                        SeqTipoSocumento = (long?)d.DocumentoRequerido.SeqTipoDocumento,
                        TipoDocumento = new
                        {
                            SeqPessoaAtuacao = (long?)d.SolicitacaoServico.SeqPessoaAtuacao,
                            SeqTipoDocumento = (long?)d.DocumentoRequerido.SeqTipoDocumento,
                        }
                    },
                }));

                //Caso encontre algum registro, adiciona os mesmos ao resultado
                if (resultIngressante != null && resultIngressante.Any())
                    resultPessoaAtuacao.AddRange(resultIngressante);
            }

            ret.SeqPessoaAtuacao = seqPessoaAtuacao;

            ret.TiposDocumento = resultPessoaAtuacao.GroupBy(g => g.SeqTipoDocumento).Select(x => new PessoaAtuacaoTipoDocumentoVO
            {
                SeqTipoDocumento = x.Key.GetValueOrDefault(),
                Solicitacoes = x.Select(a => new PessoaAtuacaoSolicitacaoServicoVO
                {
                    SeqSolicitacaoServico = a.SeqSolicitacaoServico.GetValueOrDefault(),
                    Anexos = resultPessoaAtuacao.Where(d => d.SeqTipoDocumento == x.Key && d.SeqSolicitacaoServico == a.SeqSolicitacaoServico).TransformList<PessoaAtuacaoAnexoVO>(),
                    DescricaoSolicitacaoServico = a.SolicitacaoServico.DescricaoSolicitacaoServico,
                    SeqPessoaAtuacao = a.SeqPessoaAtuacao.GetValueOrDefault(),
                    SeqTipoDocumento = a.SeqTipoDocumento.GetValueOrDefault()
                }).ToList(),
                DescricaoTipoDocumento = tiposDocumentos.FirstOrDefault(s => s.Seq == x.Key).Descricao,
                SeqPessoaAtuacao = seqPessoaAtuacao
            }).OrderBy(o => o.DescricaoTipoDocumento).ToList();

            return ret;
        }

        public PessoaAtuacaoRegistroDocumentoVO PrepararModeloRegistroDocumento(long seqPessoaAtuacao, long seqTipoDocumento, List<long> seqsSolicitacoesServico)
        {
            var spec = new SolicitacaoServicoFilterSpecification()
            {
                SeqsSolicitacoesServico = seqsSolicitacoesServico,
                SeqPessoaAtuacao = seqPessoaAtuacao
            };

            var resultPessoaAtuacao = this.SolicitacaoServicoDomainService.SearchProjectionBySpecification(spec, s => new
            {
                SolicitacaoServico = s,
                SolicitacaoDocumentosRequeridos = s.DocumentosRequeridos.Where(d => d.DocumentoRequerido.SeqTipoDocumento == seqTipoDocumento).Select(d => d).ToList(),
                DocumentosRequeridos = s.DocumentosRequeridos.Where(d => d.DocumentoRequerido.SeqTipoDocumento == seqTipoDocumento).Select(d => d.DocumentoRequerido).ToList(),
                ConfiguracoesDocumentosRequeridos = s.ConfiguracaoProcesso.ConfiguracoesEtapa.SelectMany(c => c.DocumentosRequeridos.Where(d => d.SeqTipoDocumento == seqTipoDocumento)).ToList()
            }).ToList();

            ValidarPermissaoParaRealizarOperacao(resultPessoaAtuacao.SelectMany(d => d.ConfiguracoesDocumentosRequeridos).ToList());

            //Apos recuperar os dados da pessoa atuação, descobre qual é o ingressante do histórico mais antigo do aluno referente a pessoa atuação
            //e unifica os dados, caso existam solicitações do o ingressante encontrado

            //Cria o spec do aluno, usando o seq da pessoa atuação
            var specAluno = new SMCSeqSpecification<Aluno>(seqPessoaAtuacao);

            //Recupera o seq do ingressante do historico mais antigo do aluno, da pessao atuação
            var seqIngressante = this.AlunoDomainService.SearchProjectionByKey(specAluno, p => p.Historicos.OrderBy(d => d.DataInclusao).FirstOrDefault().SeqIngressante);

            if (seqIngressante.HasValue)
            {
                //Atualiza o spec passando o ingressante encontrado
                spec = new SolicitacaoServicoFilterSpecification()
                {
                    SeqsSolicitacoesServico = seqsSolicitacoesServico,
                    SeqPessoaAtuacao = seqIngressante.GetValueOrDefault()
                };

                var resultIngressante = this.SolicitacaoServicoDomainService.SearchProjectionBySpecification(spec, s => new
                {
                    SolicitacaoServico = s,
                    SolicitacaoDocumentosRequeridos = s.DocumentosRequeridos.Where(d => d.DocumentoRequerido.SeqTipoDocumento == seqTipoDocumento).Select(d => d).ToList(),
                    DocumentosRequeridos = s.DocumentosRequeridos.Where(d => d.DocumentoRequerido.SeqTipoDocumento == seqTipoDocumento).Select(d => d.DocumentoRequerido).ToList(),
                    ConfiguracoesDocumentosRequeridos = s.ConfiguracaoProcesso.ConfiguracoesEtapa.SelectMany(c => c.DocumentosRequeridos.Where(d => d.SeqTipoDocumento == seqTipoDocumento)).ToList()
                }).ToList();

                ValidarPermissaoParaRealizarOperacao(resultIngressante.SelectMany(d => d.ConfiguracoesDocumentosRequeridos).ToList());

                //Caso encontre algum registro, adiciona os mesmos ao resultado
                if (resultIngressante != null && resultIngressante.Any())
                    resultPessoaAtuacao.AddRange(resultIngressante);
            }

            var tiposDocumentos = TipoDocumentoService.BuscarTiposDocumentos();

            var ret = new PessoaAtuacaoRegistroDocumentoVO()
            {
                DescricaoTipoDocumento = tiposDocumentos.FirstOrDefault(t => t.Seq == seqTipoDocumento).Descricao,
                PermiteVarios = resultPessoaAtuacao.All(s => s.DocumentosRequeridos.All(d => d.PermiteVarios)),
                SituacoesEntregaDocumento = BuscarSituacoesEntregaDocumento(resultPessoaAtuacao.SelectMany(s => s.DocumentosRequeridos).ToList()),
                Documentos = new List<PessoaAtuacaoRegistroDocumentoItemVO>()
                {
                    new PessoaAtuacaoRegistroDocumentoItemVO()
                    {
                        SeqTipoDocumento = tiposDocumentos.FirstOrDefault(t => t.Seq == seqTipoDocumento).Seq,
                        VersaoExigida  = BuscarVersaoExigida(resultPessoaAtuacao.SelectMany(s => s.ConfiguracoesDocumentosRequeridos).ToList())
                    }
                }
            };

            return ret;
        }

        private VersaoDocumento BuscarVersaoExigida(List<DocumentoRequerido> configuracoesDocumentosRequeridos)
        {
            if (configuracoesDocumentosRequeridos.Any(c => c.VersaoDocumento == VersaoDocumento.Original))
                return VersaoDocumento.Original;
            else if (configuracoesDocumentosRequeridos.Any(c => c.VersaoDocumento == VersaoDocumento.CopiaAutenticada))
                return VersaoDocumento.CopiaAutenticada;
            else if (configuracoesDocumentosRequeridos.Select(c => c.VersaoDocumento).Distinct().Count() == 1)
                return configuracoesDocumentosRequeridos.FirstOrDefault().VersaoDocumento;

            return VersaoDocumento.Nenhum;
        }

        private List<SMCDatasourceItem> BuscarSituacoesEntregaDocumento(List<DocumentoRequerido> documentosRequeridos)
        {
            var lista = new List<SMCDatasourceItem>();

            if (!SMCSecurityHelper.Authorize(UC_SRC_004_02_01.VALIDACAO_CRA) && !SMCSecurityHelper.Authorize(UC_SRC_004_02_01.VALIDACAO_SECRETARIA))
                return lista;

            if (SMCSecurityHelper.Authorize(UC_SRC_004_02_01.VALIDACAO_CRA) && SMCSecurityHelper.Authorize(UC_SRC_004_02_01.VALIDACAO_SECRETARIA))
            {
                foreach (var situacaoEntregaDocumento in SMCEnumHelper.GenerateKeyValuePair<SituacaoEntregaDocumento>())
                {
                    switch (situacaoEntregaDocumento.Key)
                    {
                        case SituacaoEntregaDocumento.AguardandoEntrega:
                        //case SituacaoEntregaDocumento.AguardandoValidacao:
                        case SituacaoEntregaDocumento.Deferido:
                        case SituacaoEntregaDocumento.Indeferido:
                        case SituacaoEntregaDocumento.AguardandoAnaliseSetorResponsavel:
                            lista.Add(new SMCDatasourceItem() { Seq = (long)situacaoEntregaDocumento.Key, Descricao = situacaoEntregaDocumento.Value });
                            break;

                        case SituacaoEntregaDocumento.Pendente:
                            if (documentosRequeridos.All(d => d.PermiteEntregaPosterior))
                                lista.Add(new SMCDatasourceItem() { Seq = (long)situacaoEntregaDocumento.Key, Descricao = situacaoEntregaDocumento.Value });
                            break;
                    }
                }

                return lista;
            }

            if (SMCSecurityHelper.Authorize(UC_SRC_004_02_01.VALIDACAO_CRA))
            {
                foreach (var situacaoEntregaDocumento in SMCEnumHelper.GenerateKeyValuePair<SituacaoEntregaDocumento>())
                {
                    if (documentosRequeridos.Any(d => d.ValidacaoOutroSetor))
                    {
                        switch (situacaoEntregaDocumento.Key)
                        {
                            case SituacaoEntregaDocumento.Deferido:
                            case SituacaoEntregaDocumento.Indeferido:
                            case SituacaoEntregaDocumento.AguardandoEntrega:

                                lista.Add(new SMCDatasourceItem() { Seq = (long)situacaoEntregaDocumento.Key, Descricao = situacaoEntregaDocumento.Value });
                                break;

                            case SituacaoEntregaDocumento.Pendente:

                                if (documentosRequeridos.All(d => d.PermiteEntregaPosterior))
                                    lista.Add(new SMCDatasourceItem() { Seq = (long)situacaoEntregaDocumento.Key, Descricao = situacaoEntregaDocumento.Value });
                                break;
                        }
                    }
                    else
                    {
                        throw new RegistroDocumentoPessoaAtuacaoUsuarioSemPermissaoException();
                    }
                }

                return lista;
            }

            if (SMCSecurityHelper.Authorize(UC_SRC_004_02_01.VALIDACAO_SECRETARIA))
            {
                foreach (var situacaoEntregaDocumento in SMCEnumHelper.GenerateKeyValuePair<SituacaoEntregaDocumento>())
                {
                    if (!documentosRequeridos.Any(d => d.ValidacaoOutroSetor))
                    {
                        switch (situacaoEntregaDocumento.Key)
                        {
                            case SituacaoEntregaDocumento.Deferido:
                            case SituacaoEntregaDocumento.Indeferido:
                            case SituacaoEntregaDocumento.AguardandoEntrega:

                                lista.Add(new SMCDatasourceItem() { Seq = (long)situacaoEntregaDocumento.Key, Descricao = situacaoEntregaDocumento.Value });
                                break;

                            case SituacaoEntregaDocumento.Pendente:

                                if (documentosRequeridos.All(d => d.PermiteEntregaPosterior))
                                    lista.Add(new SMCDatasourceItem() { Seq = (long)situacaoEntregaDocumento.Key, Descricao = situacaoEntregaDocumento.Value });
                                break;
                        }
                    }
                    else
                    {
                        switch (situacaoEntregaDocumento.Key)
                        {
                            case SituacaoEntregaDocumento.AguardandoEntrega:
                            case SituacaoEntregaDocumento.AguardandoAnaliseSetorResponsavel:

                                lista.Add(new SMCDatasourceItem() { Seq = (long)situacaoEntregaDocumento.Key, Descricao = situacaoEntregaDocumento.Value });
                                break;

                            case SituacaoEntregaDocumento.Pendente:

                                if (documentosRequeridos.All(d => d.PermiteEntregaPosterior))
                                    lista.Add(new SMCDatasourceItem() { Seq = (long)situacaoEntregaDocumento.Key, Descricao = situacaoEntregaDocumento.Value });
                                break;
                        }
                    }
                }

                return lista;
            }

            return lista;
        }

        /// <summary>
        /// Busca os dados de cabeçalho de uma pessoa atuação
        /// </summary>
        /// <param name="seq"></param>
        /// <returns></returns>
        public PessoaAtuacaoCabecalhoVO BuscarPessoaAtuacaoCabecalho(long seq)
        {
            var spec = new SMCSeqSpecification<PessoaAtuacao>(seq);
            return SearchProjectionByKey(spec, i => new PessoaAtuacaoCabecalhoVO()
            {
                Nome = i.DadosPessoais.Nome,
                NomeSocial = i.DadosPessoais.NomeSocial,
                Cpf = i.Pessoa.Cpf,
                NumeroPassaporte = i.Pessoa.NumeroPassaporte,
                Falecido = i.Pessoa.Falecido,
                TipoAtuacao = i.TipoAtuacao,
                SeqIngressante = (i as Ingressante).Seq,
                CodigoMigracaoAluno = (i as Aluno).CodigoAlunoMigracao,
                RA = (i as Aluno).NumeroRegistroAcademico
            });
        }

        /// <summary>
        /// Recupera os parâmetros mais comuns de uma pessoa atuação
        /// </summary>
        /// <param name="seq">Sequencial da pessoa atuação</param>
        /// <returns>Parâmetros da pessoa atuação</returns>
        public PessoaAtuacaoConfiguracaoVO BuscarPessoaAtuacaoConfiguracao(long seq)
        {
            var pessoaAtuacao = SearchByKey(new SMCSeqSpecification<PessoaAtuacao>(seq));

            if (pessoaAtuacao is Ingressante)
            {
                return IngressanteDomainService.SearchProjectionByKey(new SMCSeqSpecification<Ingressante>(seq), p => new PessoaAtuacaoConfiguracaoVO()
                {
                    Seq = p.Seq,
                    SeqIngressante = p.Seq,
                    SeqMatrizCurricularOferta = p.SeqMatrizCurricularOferta,
                    SeqNivelEnsino = p.SeqNivelEnsino,
                });
            }
            else if (pessoaAtuacao is Aluno)
            {
                var seqCicloletivoAtual = AlunoDomainService.BuscarCicloLetivoAtual(seq);
                return AlunoDomainService.SearchProjectionByKey(new SMCSeqSpecification<Aluno>(seq), p => new PessoaAtuacaoConfiguracaoVO()
                {
                    Seq = p.Seq,
                    SeqAluno = p.Seq,
                    SeqMatrizCurricularOferta = p.Historicos.FirstOrDefault(f => f.Atual)
                        .HistoricosCicloLetivo.FirstOrDefault(f => !f.DataExclusao.HasValue && f.SeqCicloLetivo == seqCicloletivoAtual)
                        .PlanosEstudo.FirstOrDefault(f => f.Atual)
                        .SeqMatrizCurricularOferta,
                    SeqNivelEnsino = p.Historicos.FirstOrDefault(f => f.Atual)
                        .SeqNivelEnsino
                });
            }

            return new PessoaAtuacaoConfiguracaoVO();
        }

        public void SalvarRegistroDocumento(PessoaAtuacaoRegistroDocumentoVO model)
        {
            using (var unityOfWork = SMCUnitOfWork.Begin())
            {
                try
                {
                    var specSolicitacaoServico = new SMCContainsSpecification<SolicitacaoServico, long>(s => s.Seq, model.SeqsSolicitacoesServico.ToArray());

                    var dadosSolicitacoesServico = this.SolicitacaoServicoDomainService.SearchProjectionBySpecification(specSolicitacaoServico, s => new
                    {
                        SeqSolicitacaoServico = s.Seq,
                        DescricaoSolicitacaoServico = s.NumeroProtocolo + " - " + s.ConfiguracaoProcesso.Processo.Descricao,
                        SolicitacaoDocumentosRequeridos = s.DocumentosRequeridos.Where(d => d.DocumentoRequerido.SeqTipoDocumento == model.SeqTipoDocumento).Select(d => d).ToList(),
                        DocumentosRequeridos = s.DocumentosRequeridos.Where(d => d.DocumentoRequerido.SeqTipoDocumento == model.SeqTipoDocumento).Select(d => d.DocumentoRequerido).ToList(),
                        ConfiguracoesDocumentosRequeridos = s.ConfiguracaoProcesso.ConfiguracoesEtapa.SelectMany(c => c.DocumentosRequeridos.Where(d => d.SeqTipoDocumento == model.SeqTipoDocumento)).ToList()
                    }).ToList();

                    //Valida se é possivel realizar as alterações necessárias
                    ValidarPermissaoParaRealizarOperacao(dadosSolicitacoesServico.SelectMany(s => s.ConfiguracoesDocumentosRequeridos).ToList());

                    //Deleta os registros existentes do tipo que está sendo editado
                    var seqsSolicitacoesDocumentosRequeridosExclusao = dadosSolicitacoesServico.SelectMany(s => s.SolicitacaoDocumentosRequeridos).Select(s => s.Seq);
                    seqsSolicitacoesDocumentosRequeridosExclusao.SMCForEach(s => this.SolicitacaoDocumentoRequeridoDomainService.DeleteEntity(s));

                    var solicitacoesDocumentosRequeridosInsert = model.Documentos.Select(d => d.Transform<SolicitacaoDocumentoRequerido>()).ToList();

                    solicitacoesDocumentosRequeridosInsert.SMCForEach(d =>
                    {
                        EnsureFileIntegrity(d, x => x.SeqArquivoAnexado, x => x.ArquivoAnexado);
                    });

                    foreach (var seqSolicitacaoServico in model.SeqsSolicitacoesServico)
                    {
                        var dadoSolicitacaoServico = dadosSolicitacoesServico.FirstOrDefault(d => d.SeqSolicitacaoServico == seqSolicitacaoServico);

                        var seqDocumentoRequerido = dadoSolicitacaoServico.SolicitacaoDocumentosRequeridos.FirstOrDefault().SeqDocumentoRequerido;

                        if (!dadoSolicitacaoServico.DocumentosRequeridos.Any())
                        {
                            throw new PessoaAtuacaoRegistroDocumentoRequeridoNaoExisteException(dadoSolicitacaoServico.DescricaoSolicitacaoServico);
                        }
                        else
                        {
                            InserirBloqueiosPessoaAtuacao(model.SeqPessoaAtuacao, seqSolicitacaoServico, model.Documentos.Select(d => d.Transform<DocumentoItemVO>()).ToList());

                            InserirDesbloqueiosPessoaAtuacao(model.SeqPessoaAtuacao, seqSolicitacaoServico, model.Documentos.Select(d => d.Transform<DocumentoItemVO>()).ToList());

                            //Prepara os registros para inserir
                            solicitacoesDocumentosRequeridosInsert.SMCForEach(d =>
                            {
                                d.Seq = 0;
                                d.SeqDocumentoRequerido = seqDocumentoRequerido;
                                d.SeqSolicitacaoServico = seqSolicitacaoServico;
                                this.SolicitacaoDocumentoRequeridoDomainService.SaveEntity(d);
                            });

                            RegistroDocumentoDomainService.ValidarDocumentoObrigatorio(seqSolicitacaoServico);
                        }
                    }

                    unityOfWork.Commit();
                }
                catch (Exception ex)
                {
                    unityOfWork.Rollback();
                    throw;
                }
            }
        }

        /// <summary>
        /// Busca os alunos e colaboradores para emissão da identidade estudantil pelos seqs informados
        /// </summary>
        /// <param name="seqsAlunos">Seqs dos alunos para pesquisa</param>
        /// <param name="seqsColaboradores">Seqs dos colaboradores para pesquisa</param>
        /// <returns>Lista de alunos e colaboradores para emissão da identidade estudantil</returns>
        public List<RelatorioIdentidadeEstudantilVO> BuscarPessoaAtuacaoIdentidadeEstudantil(List<long> seqsAlunos, List<long> seqsColaboradores)
        {
            var pessoasAtuacao = AlunoDomainService.BuscarAlunosIdentidadeEstudantil(seqsAlunos);
            pessoasAtuacao.AddRange(ColaboradorDomainService.BuscarColaboradoresIdentidadeAcademica(seqsColaboradores));
            return PaginarIdentidadesEstudantis(pessoasAtuacao);
        }

        /// <summary>
        /// Pagina uma lista de identidades estudantis com 8 itens por página e o verso espelhado orizontalmente
        /// </summary>
        /// <param name="identidadesEstudantis">Identidades estudantis</param>
        /// <returns>Lista paginada das identidades estudantis</returns>
        public List<RelatorioIdentidadeEstudantilVO> PaginarIdentidadesEstudantis(List<IdentidadeEstudantilVO> identidadesEstudantis)
        {
            var result = new List<RelatorioIdentidadeEstudantilVO>();
            // Calcula o número de páginas com 8 itens de frente e 8 itens de verso
            var paginas = Math.Ceiling(identidadesEstudantis.Count / 8d);
            List<IdentidadeEstudantilVO> itensPagina = null;
            for (int pagina = 0; pagina < paginas; pagina++)
            {
                // Frente
                itensPagina = identidadesEstudantis.Skip(pagina * 8).Take(8).ToList();
                // Caso tenha menos de 8 itens na pagina atual, completa a pagina com itens em branco
                if (itensPagina.Count() < 8)
                {
                    itensPagina.AddRange(Enumerable.Range(0, 8 - itensPagina.Count()).Select(s => new IdentidadeEstudantilVO() { Frente = true }));
                }

                // Adiciona os itens nas posições normais no retorno
                //result.AddRange(itensPagina);
                RelatorioIdentidadeEstudantilVO linha = null;
                foreach (var item in itensPagina)
                {
                    if (linha == null)
                    {
                        linha = new RelatorioIdentidadeEstudantilVO()
                        {
                            SeqAlunoEsquerda = item.SeqAluno,
                            SeqColaboradorEsquerda = item.SeqColaborador,
                            SeqProgramaEsquerda = item.SeqPrograma,
                            NomeEsquerda = item.Nome,
                            RegistroDVEsquerda = item.RegistroDV,
                            DataValidadeEsquerda = item.DataValidade?.ToString("MM/yyyy"),
                            DescricaoTipoEntidadeResponsavelEsquerda = item.DescricaoTipoEntidadeResponsavel,
                            DescricaoEntidadeResponsavelEsquerda = item.DescricaoEntidadeResponsavel,
                            DescricaoUnidadeEsquerda = item.DescricaoUnidade,
                            CodigoEsquerda = item.Codigo,
                            Frente = true
                        };
                    }
                    else
                    {
                        linha.SeqAlunoDireita = item.SeqAluno;
                        linha.SeqColaboradorDireita = item.SeqColaborador;
                        linha.SeqProgramaDireita = item.SeqPrograma;
                        linha.NomeDireita = item.Nome;
                        linha.RegistroDVDireita = item.RegistroDV;
                        linha.DataValidadeDireita = item.DataValidade?.ToString("MM/yyyy");
                        linha.DescricaoTipoEntidadeResponsavelDireita = item.DescricaoTipoEntidadeResponsavel;
                        linha.DescricaoEntidadeResponsavelDireita = item.DescricaoEntidadeResponsavel;
                        linha.DescricaoUnidadeDireita = item.DescricaoUnidade;
                        linha.CodigoDireita = item.Codigo;
                        result.Add(linha);
                        linha = null;
                    }
                }

                // Verso
                // Adiciona os itens nas posições espelhandas horizontalmente no verso
                linha = null;
                foreach (var item in itensPagina)
                {
                    string codigoBarra = null;
                    if (item != null && !string.IsNullOrEmpty(item.Codigo))
                    {
                        var imgCodigoBarra = SMCBarcodeInter25.Generate(item.Codigo, 470, 10);
                        using (var memory = new MemoryStream())
                        {
                            imgCodigoBarra.Save(memory, ImageFormat.Bmp);
                            codigoBarra = Convert.ToBase64String(memory.GetBuffer());
                        }
                    }

                    if (linha == null)
                    {
                        linha = new RelatorioIdentidadeEstudantilVO()
                        {
                            SeqAlunoDireita = item.SeqAluno,
                            SeqColaboradorDireita = item.SeqColaborador,
                            CodigoDireita = item.Codigo,
                            CodigoBarraDireita = codigoBarra
                        };
                    }
                    else
                    {
                        linha.SeqAlunoEsquerda = item.SeqAluno;
                        linha.SeqColaboradorEsquerda = item.SeqColaborador;
                        linha.CodigoEsquerda = item.Codigo;
                        linha.CodigoBarraEsquerda = codigoBarra;
                        result.Add(linha);
                        linha = null;
                    }
                }
            }

            return result;
        }

        /// <summary>
        /// Configura a permissão para cadastrar o nome social conforme o token de autorização
        /// </summary>
        /// <typeparam name="T">Tipo da atuação</typeparam>
        /// <param name="pessoaVO">Dados da pessoa atuação</param>
        public void AplicarValidacaoPermiteCadastrarNomeSocial<T>(ref T pessoaVO) where T : InformacoesPessoaVO
        {
            ///Permite a alterção do Nome social conforme regra
            pessoaVO.PermitirAlterarDadosPessoaAtuacaoNomeSocial = SMCSecurityHelper.Authorize(UC_ALN_001_01_02.PERMITIR_ALTERAR_CPF);

            ///Na inclusão sempre será permitido a alteração dos dados pessoais
            pessoaVO.PermitirAlterarDadosPessoaAtuacao = true;
        }

        /// <summary>
        /// Define se o usuário tem o token PermiteAlterarCpf e preenche os campos somente leitura
        /// </summary>
        /// <typeparam name="T">Tipo da atuação</typeparam>
        /// <param name="pessoaVO">Dados da pessoa atuação</param>
        public void AplicarValidacaoPermiteAlterarCpf<T>(ref T pessoaVO) where T : InformacoesPessoaVO
        {
            ///Verifica autorização de usuario logado conforme token
            bool permiteAlterarCpf = SMCSecurityHelper.Authorize(UC_ALN_001_01_02.PERMITIR_ALTERAR_CPF);
            ///Permite a alterção do Nome social conforme regra
            pessoaVO.PermitirAlterarDadosPessoaAtuacaoNomeSocial = permiteAlterarCpf;
            pessoaVO.PermitirAlterarDadosPessoaAtuacao = permiteAlterarCpf;

            ///Se o for permitido a alteração de dados montará a lista filiação somente para readonly
            if (!pessoaVO.PermitirAlterarDadosPessoaAtuacao)
            {
                pessoaVO.FiliacaoReadOnly = new List<PessoaFiliacaoReadOnlyVO>();
                foreach (var item in pessoaVO.Filiacao)
                {
                    PessoaFiliacaoReadOnlyVO filiacao = new PessoaFiliacaoReadOnlyVO();
                    filiacao.Seq = item.Seq;
                    filiacao.SeqPessoa = item.SeqPessoa;
                    filiacao.NomeFiliacaoReadOnly = item.Nome;
                    filiacao.TipoParentescoReadOnly = item.TipoParentesco;
                    pessoaVO.FiliacaoReadOnly.Add(filiacao);
                }

                pessoaVO.CpfReadOnly = pessoaVO.Cpf;
                pessoaVO.DataNascimentoReadOnly = pessoaVO.DataNascimento;
                pessoaVO.CodigoPaisNacionalidadeReadOnly = pessoaVO.CodigoPaisNacionalidade;
                pessoaVO.NomeSocialReadOnly = pessoaVO.NomeSocial;
                pessoaVO.TipoNacionalidadeReadOnly = pessoaVO.TipoNacionalidade;
            }
        }

        /// <summary>
        /// Restaura os campos originais com os valores dos campos readonly
        /// </summary>
        /// <typeparam name="T">Tipo da atuação</typeparam>
        /// <param name="pessoaVO">Dados da pessoa atuação</param>
        public void RestaurarCamposReadonlyCpf<T>(ref T pessoaVO) where T : InformacoesPessoaVO
        {
            ///Se o não for permitido a alteração de dados cria-se mapea-se os objetos readoly para os objetos VO
            if (!pessoaVO.PermitirAlterarDadosPessoaAtuacao)
            {
                pessoaVO.Filiacao = new List<PessoaFiliacaoVO>();
                foreach (var item in pessoaVO.FiliacaoReadOnly)
                {
                    PessoaFiliacaoVO filiacao = new PessoaFiliacaoVO();
                    filiacao.Seq = item.Seq;
                    filiacao.SeqPessoa = item.SeqPessoa;
                    filiacao.TipoParentesco = item.TipoParentescoReadOnly;
                    filiacao.Nome = item.NomeFiliacaoReadOnly;
                    pessoaVO.Filiacao.Add(filiacao);
                }

                pessoaVO.DataNascimento = pessoaVO.DataNascimentoReadOnly;
                pessoaVO.TipoNacionalidade = pessoaVO.TipoNacionalidadeReadOnly;
                pessoaVO.CodigoPaisNacionalidade = pessoaVO.CodigoPaisNacionalidadeReadOnly;
                pessoaVO.Cpf = pessoaVO.CpfReadOnly;
                pessoaVO.NomeSocial = pessoaVO.NomeSocialReadOnly;
            }
        }

        /// <summary>
        /// Busca os dados da pessoa atuação para header da tela de mensagem
        /// </summary>
        /// <param name="seq">Sequencial da pessoa atuação</param>
        /// <returns>Dados da pessoa atuação</returns>
        public PessoaAtuacaoMensagemHeaderVO BuscarPessoaAtuacaoHeaderMensagem(long seq)
        {
            var model = SearchProjectionByKey(new SMCSeqSpecification<PessoaAtuacao>(seq), p => new PessoaAtuacaoMensagemHeaderVO()
            {
                Nome = p.DadosPessoais.Nome,
                NomeSocial = p.DadosPessoais.NomeSocial,
                TipoAtuacao = p.TipoAtuacao
            });

            if (model.TipoAtuacao == TipoAtuacao.Aluno)
            {
                var situacao = AlunoHistoricoSituacaoDomainService.BuscarSituacaoAtualAluno(seq);
                model.DescricaoSituacaoMatricula = $"{situacao.DescricaoCicloLetivo} - {situacao.Descricao}";
            }

            return model;
        }

        private void InserirBloqueiosPessoaAtuacao(long seqPessoaAtuacao, long seqSolicitacaoServico, List<DocumentoItemVO> documentosRequeridos)
        {
            //UC_SRC_004_02_01 - NV13

            //Recupera os documentos com situacao pendente, aguardadno entrega, aguardando validação ou indeferido
            var documentosPendentes = documentosRequeridos
                                            .Where(w => w.SituacaoEntregaDocumento == SituacaoEntregaDocumento.Pendente ||
                                                        w.SituacaoEntregaDocumento == SituacaoEntregaDocumento.AguardandoEntrega ||
                                                        w.SituacaoEntregaDocumento == SituacaoEntregaDocumento.AguardandoValidacao ||
                                                        w.SituacaoEntregaDocumento == SituacaoEntregaDocumento.Indeferido)
                                            .ToList();

            //Caso existe documentos com situação pendente
            if (documentosPendentes.Count > 0)
            {
                //Recupera o seq do motivo de bloqueio pelo token
                var seqMotivoBloqueio = this.MotivoBloqueioDomainService.BuscarSeqMotivoBloqueioPorToken(TOKEN_MOTIVO_BLOQUEIO.ENTREGA_DOCUMENTACAO);

                //Caso não exista motivo de bloqueio cadastrado para gravar os bloqueios, dispara mensagem mde erro.
                if (seqMotivoBloqueio == null || seqMotivoBloqueio == 0)
                    throw new RegistroDocumentoMotivoBloqueioNaoCadastradoException();

                //Cria o spec para recuperar os bloqueios da pessoa atuação
                var spec = new PessoaAtuacaoBloqueioFilterSpecification()
                {
                    SeqSolicitacaoServico = seqSolicitacaoServico,
                    SeqPessoaAtuacao = seqPessoaAtuacao,
                    SeqMotivoBloqueio = new List<long>() { seqMotivoBloqueio },
                };

                //Recupera os bloqueios da pessoa atuação
                var pessoaAtuacaoBloqueios = this.PessoaAtuacaoBloqueioDomainService.SearchBySpecification(spec, IncludesPessoaAtuacaoBloqueio.Itens).ToList();

                //Apos recuperar os dados da pessoa atuação, descobre qual é o ingressante do histórico mais antigo do aluno referente a pessoa atuação
                //e unifica os dados, caso existam bloqueios do o ingressante encontrado

                //Cria o spec do aluno, usando o seq da pessoa atuação
                var specAluno = new SMCSeqSpecification<Aluno>(seqPessoaAtuacao);

                //Recupera o seq do ingressante do historico mais antigo do aluno, da pessao atuação
                var seqIngressante = this.AlunoDomainService.SearchProjectionByKey(specAluno, p => p.Historicos.OrderBy(h => h.DataInclusao).FirstOrDefault().SeqIngressante);

                //Se encontrado ingressante
                if (seqIngressante.HasValue)
                {
                    //Atualiza o spec passando o ingressante encontrado
                    spec = new PessoaAtuacaoBloqueioFilterSpecification()
                    {
                        SeqSolicitacaoServico = seqSolicitacaoServico,
                        SeqPessoaAtuacao = seqIngressante.Value,
                        SeqMotivoBloqueio = new List<long>() { seqMotivoBloqueio },
                    };

                    //Recupera os bloqueios do ingressante
                    var ingressanteBloqueios = this.PessoaAtuacaoBloqueioDomainService.SearchBySpecification(spec, IncludesPessoaAtuacaoBloqueio.Itens);

                    //Caso encontre algum registro, adiciona os mesmos ao resultado
                    if (ingressanteBloqueios.Any())
                        pessoaAtuacaoBloqueios.AddRange(ingressanteBloqueios);
                }

                //Para cada documento pendente (do modelo)
                foreach (var documentoPendente in documentosPendentes)
                {
                    //Verificar se pelo menos um bloqueio associado possui o tipo de documento em questão como item do bloqueio
                    var bloqueiosDoDocumento = pessoaAtuacaoBloqueios.Where(i => i.Itens.Any(ii => ii.CodigoIntegracaoSistemaOrigem == documentoPendente.SeqTipoDocumento.ToString())).ToList();

                    // Regra 1
                    var documentosBloqueadosAtual = bloqueiosDoDocumento.Where(i =>
                                (i.SituacaoBloqueio == SituacaoBloqueio.Bloqueado)).ToList();

                    // Regra 2
                    var documentosDesbloqueadoEfetivo = bloqueiosDoDocumento.Where(i =>
                                (i.SituacaoBloqueio == SituacaoBloqueio.Desbloqueado)).ToList();

                    // Criar o bloqueio para o caso de so existir bloqueio desbloqueado efetivamente ou não existir bloqueio
                    if ((documentosBloqueadosAtual == null || !documentosBloqueadosAtual.Any()) ||
                        (documentosDesbloqueadoEfetivo.Any() && (documentosBloqueadosAtual == null || !documentosBloqueadosAtual.Any())))
                    {
                        //Recupera o tipo de documento pendente
                        var tipoDocumento = this.TipoDocumentoService.BuscarTipoDocumento(documentoPendente.SeqTipoDocumento);

                        //Recupera a pessoa atuação
                        var pessoaAtuacao = this.SearchProjectionByKey(
                            new SMCSeqSpecification<PessoaAtuacao>(seqPessoaAtuacao),
                            p => new
                            {
                                SeqPessoaAtuacao = p.Seq,
                                Descricao = p.Descricao
                            });

                        //Cria o bloqueio
                        var novaPessoaAtuacaoBloqueio = new PessoaAtuacaoBloqueio()
                        {
                            SeqSolicitacaoServico = seqSolicitacaoServico,
                            SeqPessoaAtuacao = pessoaAtuacao.SeqPessoaAtuacao,
                            SeqMotivoBloqueio = seqMotivoBloqueio,
                            Descricao = "Pendência na entrega da documentação.",
                            SituacaoBloqueio = SituacaoBloqueio.Bloqueado,
                            DescricaoReferenciaAtuacao = pessoaAtuacao.Descricao,
                            CadastroIntegracao = true,
                            DataBloqueio = documentoPendente.DataPrazoEntrega.HasValue ? documentoPendente.DataPrazoEntrega.Value : DateTime.Now,
                            Itens = new List<PessoaAtuacaoBloqueioItem>()
                            {
                                 new PessoaAtuacaoBloqueioItem()
                                 {
                                    Descricao = tipoDocumento.Descricao,
                                    SituacaoBloqueio = SituacaoBloqueio.Bloqueado,
                                    CodigoIntegracaoSistemaOrigem = documentoPendente.SeqTipoDocumento.ToString()
                                 }
                            },
                        };

                        //Salva o bloqueio
                        this.PessoaAtuacaoBloqueioDomainService.SaveEntity(novaPessoaAtuacaoBloqueio);
                    }
                    else
                    {
                        //recupera o bloqueio atual
                        var documentoBloqueadosAtual = documentosBloqueadosAtual.FirstOrDefault();

                        //Se a data foi alterada pelo usuário, realiza a atualização
                        if (documentoBloqueadosAtual != null && (documentoBloqueadosAtual.DataBloqueio != (documentoPendente.DataPrazoEntrega.HasValue ? documentoPendente.DataPrazoEntrega.Value : DateTime.Now)))
                        {
                            documentoBloqueadosAtual.DataBloqueio = documentoPendente.DataPrazoEntrega.HasValue ? documentoPendente.DataPrazoEntrega.Value : DateTime.Now;

                            //Salva o bloqueio atual
                            this.PessoaAtuacaoBloqueioDomainService.UpdateFields(documentoBloqueadosAtual, x => x.DataBloqueio);
                        }
                    }
                }
            }
        }

        private void InserirDesbloqueiosPessoaAtuacao(long seqPessoaAtuacao, long seqSolicitacaoServico, List<DocumentoItemVO> documentosRequeridos)
        {
            //Recupera os documentos com situacao pendente ou aguardando análise do setor responsável
            var documentosDesbloqueio = documentosRequeridos
                                        .Where(w => w.SituacaoEntregaDocumento == SituacaoEntregaDocumento.Deferido ||
                                                    w.SituacaoEntregaDocumento == SituacaoEntregaDocumento.AguardandoAnaliseSetorResponsavel)
                                        .ToList();

            //Recupera o seq do motivo de bloqueio pelo token
            var seqMotivoBloqueio = this.MotivoBloqueioDomainService.BuscarSeqMotivoBloqueioPorToken(TOKEN_MOTIVO_BLOQUEIO.ENTREGA_DOCUMENTACAO);

            //Cria o spec para recuperar os bloqueios da pessoa atuação
            var spec = new PessoaAtuacaoBloqueioFilterSpecification()
            {
                SeqSolicitacaoServico = seqSolicitacaoServico,
                SeqPessoaAtuacao = seqPessoaAtuacao,
                SeqMotivoBloqueio = new List<long>() { seqMotivoBloqueio },
            };

            //Recupera os bloqueios da pessoa atuação
            var pessoaAtuacaoBloqueios = this.PessoaAtuacaoBloqueioDomainService.SearchBySpecification(spec, IncludesPessoaAtuacaoBloqueio.Itens).ToList();

            //Apos recuperar os dados da pessoa atuação, descobre qual é o ingressante do histórico mais antigo do aluno referente a pessoa atuação
            //e unifica os dados, caso existam bloqueios do o ingressante encontrado

            //Cria o spec do aluno, usando o seq da pessoa atuação
            var specAluno = new SMCSeqSpecification<Aluno>(seqPessoaAtuacao);

            //Recupera o seq do ingressante do historico mais antigo do aluno, da pessao atuação
            var seqIngressante = this.AlunoDomainService.SearchProjectionByKey(specAluno, p => p.Historicos.OrderBy(h => h.DataInclusao).FirstOrDefault().SeqIngressante);

            //Se encontrado ingressante
            if (seqIngressante.HasValue)
            {
                //Atualiza o spec passando o ingressante encontrado
                spec = new PessoaAtuacaoBloqueioFilterSpecification()
                {
                    SeqSolicitacaoServico = seqSolicitacaoServico,
                    SeqPessoaAtuacao = seqIngressante.Value,
                    SeqMotivoBloqueio = new List<long>() { seqMotivoBloqueio },
                };

                //Recupera os bloqueios do ingressante
                var ingressanteBloqueios = this.PessoaAtuacaoBloqueioDomainService.SearchBySpecification(spec, IncludesPessoaAtuacaoBloqueio.Itens);

                //Caso encontre algum registro, adiciona os mesmos ao resultado
                if (ingressanteBloqueios.Any())
                    pessoaAtuacaoBloqueios.AddRange(ingressanteBloqueios);
            }

            foreach (var documentoDesbloqueio in documentosDesbloqueio)
            {
                //Verificar se pelo menos um bloqueio associado possui o tipo de documento em questão como item do bloqueio
                var bloqueiosDoDocumento = pessoaAtuacaoBloqueios.Where(i => i.Itens.Any(ii => ii.CodigoIntegracaoSistemaOrigem == documentoDesbloqueio.SeqTipoDocumento.ToString())).ToList();

                // Regra 1
                var documentosBloqueadosAtual = bloqueiosDoDocumento.Where(i =>
                            (i.SituacaoBloqueio == SituacaoBloqueio.Bloqueado)).ToList();

                //Para cada bloqueio
                foreach (var documentoBloqueado in documentosBloqueadosAtual)
                {
                    //Para cada item de bloqueio, proceder com o desbloqueio
                    foreach (var itemBloqueio in documentoBloqueado.Itens)
                    {
                        itemBloqueio.SituacaoBloqueio = SituacaoBloqueio.Desbloqueado;
                        itemBloqueio.UsuarioDesbloqueio = SMCContext.User.Identity.Name;
                        itemBloqueio.DataDesbloqueio = DateTime.Now;
                    }
                    //Caso todos os itens de um bloqueio estejam desbloqueados, realizar o desbloqueio
                    if (documentoBloqueado.Itens.All(i => i.SituacaoBloqueio == SituacaoBloqueio.Desbloqueado))
                    {
                        documentoBloqueado.SituacaoBloqueio = SituacaoBloqueio.Desbloqueado;
                        documentoBloqueado.TipoDesbloqueio = TipoDesbloqueio.Efetivo;
                        documentoBloqueado.UsuarioDesbloqueioEfetivo = SMCContext.User.Identity.Name;
                        documentoBloqueado.DataDesbloqueioEfetivo = DateTime.Now;
                        documentoBloqueado.JustificativaDesbloqueio = "Entrega de documentação.";
                    }

                    //Salvar o desbloqueio
                    this.PessoaAtuacaoBloqueioDomainService.SaveEntity(documentoBloqueado);
                }
            }
        }

        private void ValidarPermissaoParaRealizarOperacao(List<DocumentoRequerido> configuracoesDocumentosRequeridos)
        {
            //Se o documento foi parametrizado em TODAS as solicitações, nas quais ele é requerido, para NÃO ser validado
            //por outro setor, e a pessoa possuir APENAS o token Validação CRA, abortar a operação e exibir mensagem de erro
            if (configuracoesDocumentosRequeridos.All(c => !c.ValidacaoOutroSetor) && (SMCSecurityHelper.Authorize(UC_SRC_004_02_01.VALIDACAO_CRA) && !SMCSecurityHelper.Authorize(UC_SRC_004_02_01.VALIDACAO_SECRETARIA)))
                throw new PessoaAtuacaoRegistroDocumentoPelaSecretariaException();

            //Se o documento foi parametrizado em TODAS as solicitações, nas quais ele é requerido, para ser validado por
            //outro setor, e a pessoa possuir APENAS o token Validação Secretaria, abortar a operação e exibir mensagem de erro
            //if (configuracoesDocumentosRequeridos.All(c => c.ValidacaoOutroSetor) && (SMCSecurityHelper.Authorize(UC_SRC_004_02_01.VALIDACAO_SECRETARIA) && !SMCSecurityHelper.Authorize(UC_SRC_004_02_01.VALIDACAO_CRA)))
            //throw new PessoaAtuacaoRegistroDocumentoPeloCRAException();
        }
    }
}