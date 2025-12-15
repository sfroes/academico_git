using SMC.Academico.Common.Areas.ALN.Enums;
using SMC.Academico.Common.Areas.CAM.Exceptions.CilcoLetivo;
using SMC.Academico.Common.Constants;
using SMC.Academico.Domain.Areas.ALN.DomainServices;
using SMC.Academico.Domain.Areas.ALN.Models;
using SMC.Academico.Domain.Areas.ALN.Specifications;
using SMC.Academico.Domain.Areas.ALN.ValueObjects;
using SMC.Academico.Domain.Areas.CAM.DomainServices;
using SMC.Academico.Domain.Areas.MAT.ValueObjects;
using SMC.Academico.Domain.Areas.ORG.DomainServices;
using SMC.Academico.Domain.Areas.PES.DomainServices;
using SMC.Academico.ServiceContract.Areas.ALN.Data;
using SMC.Academico.ServiceContract.Areas.ALN.Data.Aluno;
using SMC.Academico.ServiceContract.Areas.ALN.Interfaces;
using SMC.Academico.ServiceContract.Areas.MAT.Data;
using SMC.Academico.ServiceContract.Areas.ORG.Data;
using SMC.Framework;
using SMC.Framework.Extensions;
using SMC.Framework.Model;
using SMC.Framework.Service;
using SMC.Framework.Specification;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SMC.Academico.Service.Areas.ALN.Services
{
    public class AlunoService : SMCServiceBase, IAlunoService
    {
        #region [ DomainService ]

        private AlunoDomainService AlunoDomainService => Create<AlunoDomainService>();

        private AlunoHistoricoSituacaoDomainService AlunoHistoricoSituacaoDomainService => Create<AlunoHistoricoSituacaoDomainService>();

        private NivelEnsinoDomainService NivelEnsinoDomainService => Create<NivelEnsinoDomainService>();

        private ViewAlunoDomainService ViewAlunoDomainService => Create<ViewAlunoDomainService>();

        private ConfiguracaoEventoLetivoDomainService ConfiguracaoEventoLetivoDomainService => Create<ConfiguracaoEventoLetivoDomainService>();

        private PessoaAtuacaoDomainService PessoaAtuacaoDomainService => Create<PessoaAtuacaoDomainService>();

        #endregion [ DomainService ]

        /// <summary>
        /// Busca os alunos com as depêndencias apresentadas na listagem do seu cadastro
        /// </summary>
        /// <param name="filtros">Filtros do ingressante</param>
        /// <returns>Dados paginados de ingressante</returns>
        public SMCPagerData<AlunoListaData> BuscarAlunos(AlunoFiltroData filtros)
        {
            return AlunoDomainService.BuscarAlunos(filtros.Transform<AlunoFilterSpecification>()).Transform<SMCPagerData<AlunoListaData>>();
        }

        /// <summary>
        /// Busca os alunos com as depêndencias apresentadas na consulta detalhada
        /// </summary>
        /// <param name="seq">Sequencial do aluno</param>
        /// <returns>Dados do aluno</returns>
        public AlunoListaData BuscarAlunoVisualizacaoDados(long seq)
        {
            return AlunoDomainService.BuscarAlunoVisualizacaoDados(seq, true).Transform<AlunoListaData>();
        }

        /// <summary>
        /// Grava alterações dos dados pessoais de um aluno
        /// </summary>
        /// <param name="aluno">Dados pessoais do aluno a ser atualizado</param>
        /// <returns>Sequencial do aluno atualizado</returns>
        /// <exception cref="AtuacaoSemTelefoneException">Caso não seja informado nenhum telefone</exception>
        public long SalvarAluno(AlunoData aluno)
        {
            return AlunoDomainService.SalvarAluno(aluno.Transform<AlunoVO>());
        }

        /// <summary>
        /// Busca um aluno com seus dados pessoais
        /// </summary>
        /// <param name="seq">Sequencial do aluno</param>
        /// <returns>Dados do aluno</returns>
        public AlunoData BuscarAluno(long seq)
        {
            return AlunoDomainService.BuscarAluno(seq).Transform<AlunoData>();
        }

        /// <summary>
        /// Busca um aluno com seus dados pessoais para mobile
        /// </summary>
        /// <param name="seq">Sequencial do aluno</param>
        /// <returns>Dados do aluno</returns>
        public AlunoData BuscarAlunoMobile(long seq)
        {
            return AlunoDomainService.BuscarAlunoMobile(seq).Transform<AlunoData>();
        }

        /// <summary>
        /// Retorna o nível de ensino atual do aluno.
        /// </summary>
        /// <param name="seqPessoaAtuacao">Sequencial do aluno.</param>
        /// <returns>Nível de ensino do aluno informado.</returns>
        public NivelEnsinoData BuscarNivelEnsinoAluno(long seqPessoaAtuacao)
        {
            return NivelEnsinoDomainService.BuscarNivelEnsinoAluno(seqPessoaAtuacao).Transform<NivelEnsinoData>();
        }

        /// <summary>
        /// Busca os alunos para emissão de relatórios
        /// </summary>
        /// <param name="filtro">Filtro a ser considerado na pesquisa</param>
        /// <returns>Lista de alunos para emissão de relatórios</returns>
        public SMCPagerData<RelatorioListarData> BuscarAlunosRelatorio(RelatorioFiltroData filtro, bool buscarDescricaoCicloLetivoIngresso = false)
        {
            return this.ViewAlunoDomainService.BuscarAlunosRelatorio(filtro.Transform<RelatorioFiltroVO>(), buscarDescricaoCicloLetivoIngresso).Transform<SMCPagerData<RelatorioListarData>>();
        }

        /// <summary>
        /// Busca os alunos para emissão da identidade estudantil pelos seqs informados
        /// </summary>
        /// <param name="filtro">Seqs dos alunos para pesquisa</param>
        /// <returns>Lista de alunos para emissão da identidade estudantil</returns>
        public List<RelatorioIdentidadeEstudantilData> BuscarAlunosIdentidadeEstudantil(List<long> seqsAlunos)
        {
            return this.AlunoDomainService.BuscarAlunosIdentidadeEstudantilPaginados(seqsAlunos).TransformList<RelatorioIdentidadeEstudantilData>();
        }

        /// <summary>
        /// Buscar dados da matricula do aluno
        /// </summary>
        /// <param name="seq">Sequencial do aluno</param>
        public ConsultaDadosAlunoData BuscarDadosMatriculaAluno(long seq)
        {
            return AlunoDomainService.BuscarDadosMatriculaAluno(seq).Transform<ConsultaDadosAlunoData>();
        }

        /// <summary>
        /// Busca os dados relativos a matricula dos alunos passados no parametro. Obs.: utilizado para gerar
        /// relatório de declaração de matrícula
        /// </summary>
        /// <param name="seqPessoaAtuacao"></param>
        /// <returns></returns>
        public List<ItemDeclaracaoMatriculaData> BuscarItemsDeclaracaoMatricula(long[] seqPessoaAtuacao, long seqCicloLetivo)
        {
            return AlunoDomainService.BuscarItemsDeclaracaoMatricula(seqPessoaAtuacao, seqCicloLetivo).TransformList<ItemDeclaracaoMatriculaData>();
        }

        /// <summary>
        /// Retorna a lista de alunos como select para ser usado em combo
        /// </summary>
        /// <param name="alunoFiltroData">Filtros a serem utilizados</param>
        /// <returns>Lista de alunos</returns>
        public List<SMCDatasourceItem> BuscarAlunosSelect(AlunoFiltroData filtros, bool exibirVinculo = false, bool carregarVinculoAtivo = false)
        {
            return AlunoDomainService.BuscarAlunosSelect(filtros.Transform<AlunoFilterSpecification>(), exibirVinculo, carregarVinculoAtivo);
        }

        /// <summary>
        /// Retorna os dados de integração para fazer chamadas ao sistema antigo
        /// </summary>
        /// <param name="seq">Sequencial do aluno</param>
        /// <returns>Dados de integração</returns>
        public AlunoDadosIntegracaoData BuscarDadosIntegracaoSGAAntigo(long seq)
        {
            var dadosRet = AlunoDomainService.BuscarDadosIntegracaoSGAAntigo(seq);
            return new AlunoDadosIntegracaoData { CodigoAlunoMigracao = dadosRet.CodigoAlunoMigracao.GetValueOrDefault(), SeqOrigem = dadosRet.SeqOrigem.GetValueOrDefault() };
        }

        /// <summary>
        /// Retorna os dados do ingressante que deu origem ao aluno
        /// </summary>
        /// <param name="seqAluno">Sequencial do aluno</param>
        /// <returns>Dados do ingressante</returns>
        public AlunoDadosIngressanteData BuscarDadosIngressanteAluno(long seqAluno)
        {
            var dadosRet = AlunoDomainService.BuscarDadosIngressanteAluno(seqAluno);
            return new AlunoDadosIngressanteData { SeqIngressante = dadosRet.SeqIngressante, SeqSolicitacaoMatricula = dadosRet.SeqSolicitacaoMatricula };
        }

        /// <summary>
        /// Buscar os dados das parcelas em aberto do aluno
        /// </summary>
        /// <param name="seqPessoaAtuacao">Sequencial do aluno</param>
        /// <returns>Dados de parcela em aberto</returns>
        public List<EmitirBoletoAbertoData> BuscarParcelasPagamentoEmAberto(long seqPessoaAtuacao)
        {
            var dadosRet = AlunoDomainService.BuscarParcelasPagamentoEmAberto(seqPessoaAtuacao);
            return dadosRet.TransformList<EmitirBoletoAbertoData>();
        }

        /// <summary>
        /// Gerar o boleto de acordo com o título e o serviço no portal do aluno
        /// </summary>
        /// <param name="seqTitulo">Sequencial do titulo do boleto</param>
        /// <param name="seqServico">Sequencial do serviço</param>
        /// <param name="seqPessoaAtuacao">Sequencial do aluno solicitante</param>
        /// <returns></returns>
        public AlunoEmitirBoletoData GerarBoletoAluno(long seqTitulo, long seqServico, long seqPessoaAtuacao)
        {
            var dadosRet = AlunoDomainService.GerarBoletoAluno(seqTitulo, seqServico, seqPessoaAtuacao);
            return dadosRet.Transform<AlunoEmitirBoletoData>();
        }

        /// <summary>
        /// Busca as disciplinas cursadas para cada seq aluno passado como parametro
        /// </summary>
        /// <param name="seqAlunos"></param>
        /// <returns></returns>
        public List<RelatorioDisciplinasCursadasData> RelatorioDisciplinasCursadas(RelatorioDisciplinasCursadasFiltroData filtro)
        {
            var filtroData = SMCMapperExtensions.Transform<RelatorioDisciplinasCursadasFiltroVO>(filtro);
            var dadosRet = AlunoDomainService.RelatorioDisciplinasCursadas(filtroData).Alunos;
            return dadosRet.TransformList<RelatorioDisciplinasCursadasData>();
        }

        /// <summary>
        /// Emisão de relatório de histórico escolar.
        /// </summary>
        /// <param name="seqsAlunos">Lista de sequenciais dos alunos (PessoaAtuacao).</param>
        /// <returns>Data com dados dos alunos informados para montar os relatórios de cada aluno.</returns>
        public List<RelatorioHistoricoEscolarData> RelatorioHistoricoEscolar(List<long> seqsAlunos, bool? compCurriculaSemCreditos, bool? exibirMediaNotas)
        {
            return AlunoDomainService.RelatorioHistoricoEscolar(seqsAlunos, compCurriculaSemCreditos, exibirMediaNotas).Alunos.TransformList<RelatorioHistoricoEscolarData>();
        }

        /// <summary>
        /// Emisão de relatório de histórico escolar interno.
        /// </summary>
        /// <param name="seqsAlunos">Lista de sequenciais dos alunos (PessoaAtuacao).</param>
        /// <returns>Data com dados dos alunos informados para montar os relatórios de cada aluno.</returns>
        public List<RelatorioHistoricoEscolarData> RelatorioHistoricoEscolarInterno(List<long> seqsAlunos)
        {
            return AlunoDomainService.RelatorioHistoricoEscolarInterno(seqsAlunos).Alunos.TransformList<RelatorioHistoricoEscolarData>();
        }

        /// <summary>
        /// Busca o registro acadêmico para comprovante de processo
        /// </summary>
        /// <param name="seqPessoaAtuacao">Sequencial do aluno</param>
        /// <returns>Número do registro acadêmico</returns>
        public long BuscarRegistroAcademicoAluno(long seqPessoaAtuacao)
        {
            return AlunoDomainService.BuscarRegistroAcademicoAluno(seqPessoaAtuacao);
        }

        /// <summary>
        /// Busca os dados do aluno para o header BI_ALN_001 - Aluno - Cabeçalho
        /// </summary>
        /// <param name="seq">Sequencial do aluno</param>
        /// <returns>Dados do aluno para o header</returns>
        public AlunoCabecalhoData BuscarAlunoCabecalho(long seq)
        {
            return AlunoDomainService.BuscarAlunoCabecalho(seq).Transform<AlunoCabecalhoData>();
        }

        /// <summary>
        /// Busca o ciclo letivo atual de um aluno
        /// </summary>
        /// <param name="seqAluno">Sequencial do aluno</param>
        /// <returns>Ciclo letivo atual</returns>
        public long BuscarCicloLetivoAtual(long seqAluno)
        {
            return this.AlunoDomainService.BuscarCicloLetivoAtual(seqAluno);
        }

        /// <summary>
        /// Busca o cliclos letivos do aluno histórico de um aluno
        /// </summary>
        /// <param name="seqAluno">Sequencial do aluno</param>
        /// <returns>Ciclos letivos encontrados</returns>
        public List<SMCDatasourceItem> BuscarCiclosLetivosAlunoHistoricoSelect(long seqAluno)
        {
            return this.AlunoDomainService.BuscarCiclosLetivosAlunoHistoricoSelect(seqAluno);
        }

        public long BuscarCodigoMigracaoAluno(long seqPessoaAtuacao)
        {
            return this.AlunoDomainService.SearchProjectionByKey(new SMCSeqSpecification<Aluno>(seqPessoaAtuacao), x => x.CodigoAlunoMigracao) ?? 0;
        }

        /// <summary>
        /// Realizar o filtro para relatório de alunos por situação de matrícula.
        /// </summary>
        /// <param name="filtros"></param>
        /// <returns></returns>
        public List<RelatorioAlunosPorSituacaoData> BuscarDadosRelatorioAlunosPorSituacao(RelatorioAlunosPorSituacaoFiltroData filtros)
        {
            return AlunoDomainService.BuscarDadosRelatorioAlunosPorSituacao(filtros.Transform<RelatorioAlunosPorSituacaoFiltroVO>()).TransformList<RelatorioAlunosPorSituacaoData>();
        }

        public List<int> BuscarDadosArquivoSMDAlunosPorSituacao(RelatorioAlunosPorSituacaoFiltroData filtros)
        {
            var filtroVO = filtros.Transform<RelatorioAlunosPorSituacaoFiltroVO>();
            return AlunoDomainService.BuscarDadosArquivoSMDAlunosPorSituacao(filtroVO);
        }

        public string CancelarMatricula(CancelarMatriculaAlunoData data)
        {
            // Transforma o data em VO
            var vo = data.Transform<CancelarMatriculaVO>();

            //Se informado o ciclo letivo, utilizar este ciclo como referência
            if (data.SeqCicloLetivo.HasValue)
            {
                //Seta o ciclo letivo referência para cancelamento
                vo.SeqCicloLetivoReferencia = data.SeqCicloLetivo.GetValueOrDefault();
            }
            else
            {
                // Busca os dados de origem do aluno
                var dadosOrigem = PessoaAtuacaoDomainService.RecuperaDadosOrigem(data.SeqPessoaAtuacao);

                // Busca o ciclo letivo na data de referencia
                long seqCicloReferencia = ConfiguracaoEventoLetivoDomainService.BuscarSeqCicloLetivo(data.DataReferencia, dadosOrigem.SeqCursoOfertaLocalidadeTurno, TipoAluno.Veterano, TOKEN_TIPO_EVENTO.PERIODO_CICLO_LETIVO);
                if (seqCicloReferencia <= 0)
                    throw new CicloLetivoInvalidoException();

                // Atualiza o VO com o ciclo na data de referencia
                vo.SeqCicloLetivoReferencia = seqCicloReferencia;
            }

            // Chama rotina de cancelamento de matrícula
            return AlunoDomainService.CancelarMatricula(vo);
        }

        /// <summary>
        /// Verifica nada consta financeiro
        /// </summary>
        /// <param name="codigoAlunoMigracao">Código do aluno de migração</param>
        /// <param name="dataVerificacao">Data da verificação</param>
        /// <returns></returns>
        public bool ValidarNadaConstaFinanceiro(int codigoAlunoMigracao, DateTime dataVerificacao)
        {
            return AlunoDomainService.ValidarNadaConstaFinanceiro(codigoAlunoMigracao, dataVerificacao);
        }

        public SituacaoAlunoData BuscarSituacaoAtual(long seqAluno, bool desativarFiltroDados = false)
        {
            return AlunoHistoricoSituacaoDomainService.BuscarSituacaoAtualAluno(seqAluno, desativarFiltroDados).Transform<SituacaoAlunoData>();
        }

        /// <summary>
        /// Realizar o filtro para relatório de alunos por componente.
        /// </summary>
        /// <param name="filtros"></param>
        /// <returns></returns>
        public List<RelatorioAlunosPorComponenteListaData> BuscarDadosRelatorioAlunosPorComponente(RelatorioAlunosPorComponenteFiltroData filtros)
        {
            return AlunoDomainService.BuscarDadosRelatorioAlunosPorComponente(filtros.Transform<RelatorioAlunosPorComponenteFiltroVO>()).TransformList<RelatorioAlunosPorComponenteListaData>();
        }

        /// <summary>
        /// Busca aluno para emissão da identidade estudantil pelos seqs informados, sem paginação
        /// </summary>
        /// <param name="seqAluno"></param>
        /// <returns></returns>
        public IdentidadeEstudantilData BuscarIdentidadeEstudantil(long seqAluno)
        {
            var seqAlunoList = new List<long> { seqAluno };
            var identidadeEstudantilVO = AlunoDomainService.BuscarAlunosIdentidadeEstudantil(seqAlunoList).FirstOrDefault();
            return identidadeEstudantilVO.Transform<IdentidadeEstudantilData>();
        }

        /// <summary>
        /// Buscar os dados do aluno para abertura de chamado de suporte técnico
        /// </summary>
        /// <param name="seqAluno">Sequencial da pessoa atuação do aluno</param>
        /// <returns>Dados para abertura de chamados de suporte tecnico</returns>
        public AlunoDadosSuporteTecnicoData BuscarDadosSuporteTecnico(long seqAluno)
        {
            return AlunoDomainService.BuscarDadosSuporteTecnico(seqAluno).Transform<AlunoDadosSuporteTecnicoData>();
        }

        /// <summary>
        /// Recupera o código de estabelecimento de um aluno 
        /// </summary>
        /// <param name="seqAluno">Sequencial da pessoa atuação do aluno</param>
        /// <returns>Recupera o código de estabelecimento de um aluno</returns>
        public string BuscarCodigoEstabelecimentoAluno(long seqAluno)
        {
            return AlunoDomainService.BuscarCodigoEstabelecimentoAluno(seqAluno);
        }

        /// <summary>
        /// Chama a procedure ALN.st_carga_sincronismo_aluno_graduacao para sincronizar os dados do aluno
        /// </summary>
        /// <param name="codigoAlunoMigracao">Código do aluno migração a ser sincronizado</param>
        /// <param name="seqPessoaAtuacao">Sequencial da pessoa atuacao</param>
        /// <returns>Sequencial da pessoa atuação que foi sincronizada</returns>
        public long SincronizarDadosAluno(long codigoAlunoMigracao, long seqPessoaAtuacao)
        {
            return AlunoDomainService.SincronizarDadosAluno(codigoAlunoMigracao, seqPessoaAtuacao);
        }

        public long SincronizarDadosALunoSomenteComCodigoMigracao(int codigoAlunoMigracao)
        {
            return AlunoDomainService.SincronizarDadosALunoSomenteComCodigoMigracao(codigoAlunoMigracao);
        }

        public List<PrevisaoConclusaoOrientacaoRelatorioData> BuscarPrevisaoConclusaoOrientacaoRelatorio(RelatorioPrevisaoConclusaoOrientacaoFiltroData filtro)
        {
            var retorno = this.AlunoDomainService.BuscarPrevisaoConclusaoOrientacaoRelatorio(filtro.Transform<AlunoFilterSpecification>()).TransformList<PrevisaoConclusaoOrientacaoRelatorioData>();
            return retorno;
        }

        public void EnviarParaAssinatura(ConfigurarRelatorioDeclaracaoGenericaData dados)
        {
            this.AlunoDomainService.EnviarDeclaracaoGenericaParaAssinatura(dados.Transform<ConfigurarRelatorioDeclaracaoGenericaVO>());
        }

        public ConfigurarRelatorioDeclaracaoGenericaData ConfigurarRelatorioDeclaracaoGenerica(long seqNivelEnsinoPorGrupoDocumentoAcademico, long seqTipoDocumentoAcademico, SMCLanguage idiomaDocumentoAcademico, long seqAluno)
        {
            return this.AlunoDomainService.ConfigurarRelatorioDeclaracaoGenerica(seqNivelEnsinoPorGrupoDocumentoAcademico, seqTipoDocumentoAcademico, idiomaDocumentoAcademico, seqAluno).Transform<ConfigurarRelatorioDeclaracaoGenericaData>();
        }

        public byte[] BuscarRelatorioGenerico(ConfigurarRelatorioDeclaracaoGenericaData dados)
        {
            return this.AlunoDomainService.BuscarRelatorioGenerico(dados.Transform<ConfigurarRelatorioDeclaracaoGenericaVO>());
        }
    }
}