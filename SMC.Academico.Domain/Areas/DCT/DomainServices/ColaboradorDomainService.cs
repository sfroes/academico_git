using SMC.Academico.Common.Areas.ALN.Enums;
using SMC.Academico.Common.Areas.CSO.Constants;
using SMC.Academico.Common.Areas.DCT.Constants;
using SMC.Academico.Common.Areas.DCT.Enums;
using SMC.Academico.Common.Areas.DCT.Exceptions;
using SMC.Academico.Common.Areas.DCT.Includes;
using SMC.Academico.Common.Areas.PES.Enums;
using SMC.Academico.Common.Areas.PES.Exceptions;
using SMC.Academico.Common.Areas.PES.Includes;
using SMC.Academico.Common.Constants;
using SMC.Academico.Domain.Areas.ALN.DomainServices;
using SMC.Academico.Domain.Areas.ALN.Models;
using SMC.Academico.Domain.Areas.CAM.DomainServices;
using SMC.Academico.Domain.Areas.CAM.Models;
using SMC.Academico.Domain.Areas.CSO.DomainServices;
using SMC.Academico.Domain.Areas.CSO.Specifications;
using SMC.Academico.Domain.Areas.DCT.Models;
using SMC.Academico.Domain.Areas.DCT.Resources;
using SMC.Academico.Domain.Areas.DCT.Specifications;
using SMC.Academico.Domain.Areas.DCT.ValueObjects;
using SMC.Academico.Domain.Areas.ORG.DomainServices;
using SMC.Academico.Domain.Areas.ORG.Models;
using SMC.Academico.Domain.Areas.ORG.Specifications;
using SMC.Academico.Domain.Areas.PES.DomainServices;
using SMC.Academico.Domain.Areas.PES.Models;
using SMC.Academico.Domain.Areas.PES.ValueObjects;
using SMC.Academico.Domain.Areas.TUR.DomainServices;
using SMC.Academico.Domain.Helpers;
using SMC.DadosMestres.Common;
using SMC.DadosMestres.Common.Areas.PES.Enums;
using SMC.DadosMestres.ServiceContract.Areas.PES.Data;
using SMC.DadosMestres.ServiceContract.Areas.PES.Interfaces;
using SMC.Framework;
using SMC.Framework.Domain;
using SMC.Framework.Exceptions;
using SMC.Framework.Extensions;
using SMC.Framework.Model;
using SMC.Framework.Security;
using SMC.Framework.Security.Util;
using SMC.Framework.Specification;
using SMC.Framework.UnitOfWork;
using SMC.Localidades.ServiceContract.Areas.LOC.Interfaces;
using SMC.Pessoas.ServiceContract.Areas.PES.Data;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SMC.Academico.Domain.Areas.DCT.DomainServices
{
    public class ColaboradorDomainService : AcademicoContextDomain<Colaborador>
    {
        #region [ Service ]

        private ILocalidadeService LocalidadeService => Create<ILocalidadeService>();

        private IIntegracaoDadoMestreService IntegracaoDadoMestreService => Create<IIntegracaoDadoMestreService>();

        private Pessoas.ServiceContract.Areas.PES.Interfaces.IPessoaService PessoaService => Create<Pessoas.ServiceContract.Areas.PES.Interfaces.IPessoaService>();

        #endregion [ Service ]

        #region [ DomainService ]

        private AlunoDomainService AlunoDomainService => Create<AlunoDomainService>();
        private CampanhaOfertaDomainService CampanhaOfertaDomainService => Create<CampanhaOfertaDomainService>();
        private ColaboradorVinculoDomainService ColaboradorVinculoDomainService => Create<ColaboradorVinculoDomainService>();
        private ConfiguracaoEventoLetivoDomainService ConfiguracaoEventoLetivoDomainService => Create<ConfiguracaoEventoLetivoDomainService>();
        private EntidadeDomainService EntidadeDomainService => Create<EntidadeDomainService>();
        private HierarquiaClassificacaoDomainService HierarquiaClassificacaoDomainService => Create<HierarquiaClassificacaoDomainService>();
        private IngressanteDomainService IngressanteDomainService => Create<IngressanteDomainService>();
        private InstituicaoExternaDomainService InstituicaoExternaDomainService => Create<InstituicaoExternaDomainService>();
        private InstituicaoNivelDomainService InstituicaoNivelDomainService => Create<InstituicaoNivelDomainService>();
        private InstituicaoTipoEntidadeVinculoColaboradorDomainService InstituicaoTipoEntidadeVinculoColaboradorDomainService => Create<InstituicaoTipoEntidadeVinculoColaboradorDomainService>();
        private NivelEnsinoDomainService NivelEnsinoDomainService => Create<NivelEnsinoDomainService>();
        private PessoaAtuacaoDomainService PessoaAtuacaoDomainService => Create<PessoaAtuacaoDomainService>();
        private PessoaDomainService PessoaDomainService => Create<PessoaDomainService>();
        private TipoFormacaoEspecificaDomainService TipoFormacaoEspecificaDomainService => Create<TipoFormacaoEspecificaDomainService>();
        private TipoVinculoColaboradorDomainService TipoVinculoColaboradorDomainService => Create<TipoVinculoColaboradorDomainService>();
        private TurmaDomainService TurmaDomainService => Create<TurmaDomainService>();

        #endregion [ DomainService ]

        /// <summary>
        /// Busca colaboradores com seus dados pessoais
        /// </summary>
        /// <param name="filtros">Filtros para busca</param>
        /// <returns>Dados paginados dos colaboradores</returns>
        public SMCPagerData<ColaboradorListaVO> BuscarColaboradores(ColaboradorFiltroVO colaboradorFiltroVO)
        {
            // (Alteração Task 50998) Quando o tipo de vinculo, a data de inicio e a data fim do filtro de pesquisa não forem informados, 
            // exibir todos os vínculos do professor de acordo com os demais filtros informados, listando os vínculos ativos em primeiro lugar.
            bool exibirTodosVinculosProfessor = !colaboradorFiltroVO.SeqTipoVinculoColaborador.HasValue || !colaboradorFiltroVO.DataInicio.HasValue || !colaboradorFiltroVO.DataFim.HasValue;

            var filtros = colaboradorFiltroVO.Transform<ColaboradorFilterSpecification>();

            PrepararFiltro(ref colaboradorFiltroVO, ref filtros);

            if (colaboradorFiltroVO.IgnorarFiltros)
            {
                FilterHelper.AtivarApenasFiltros(this, FILTER.INSTITUICAO_ENSINO);
            }

            var dataAtual = DateTime.Today;
            var colaboradores = SearchProjectionBySpecification(filtros, p => new ColaboradorListaVO()
            {
                Seq = p.Seq,
                Cpf = p.Pessoa.Cpf,
                NumeroPassaporte = p.Pessoa.NumeroPassaporte,
                Falecido = p.Pessoa.Falecido,
                DataNascimento = p.Pessoa.DataNascimento,
                Nome = p.DadosPessoais.Nome,
                NomeSocial = p.DadosPessoais.NomeSocial,
                Sexo = p.DadosPessoais.Sexo,
                FormacaoAcademica = p.DadosPessoais.Sexo == Sexo.Masculino ? p.FormacoesAcademicas.FirstOrDefault(f => f.TitulacaoMaxima).Titulacao.DescricaoMasculino :
                                                                             p.FormacoesAcademicas.FirstOrDefault(f => f.TitulacaoMaxima).Titulacao.DescricaoFeminino,
                VinculosAtivos = p.Vinculos
                    .Where(vinculo => !exibirTodosVinculosProfessor ? vinculo.DataInicio <= dataAtual && (!vinculo.DataFim.HasValue || vinculo.DataFim >= dataAtual) : vinculo.DataInicio <= DateTime.MaxValue)
                    .OrderByDescending(o => o.DataInicio)
                    .ThenByDescending(o => o.DataFim)
                    .ThenBy(o => o.EntidadeVinculo.Nome)
                    .Select(s => new ColaboradorVinculoListaVO()
                    {
                        Seq = s.Seq,
                        SeqColaborador = s.SeqColaborador,
                        SeqEntidadeVinculo = s.SeqEntidadeVinculo,
                        DataInicio = s.DataInicio,
                        DataFim = s.DataFim,
                        InseridoPorCarga = s.InseridoPorCarga,
                        NomeEntidadeVinculo = s.EntidadeVinculo.Nome,
                        DescricaoTipoVinculoColaborador = s.TipoVinculoColaborador.Descricao,
                        SeqTipoVinculoColaborador = s.SeqTipoVinculoColaborador,
                        Cursos = s.Cursos.Select(c => new ColaboradorVinculoCursoVO()
                        {
                            SeqCursoOfertaLocalidade = c.SeqCursoOfertaLocalidade,
                            TipoAtividadeColaborador = c.Atividades.Select(a => a.TipoAtividadeColaborador).ToList()
                        }).ToList()
                    }).ToList()
            }, out int total).ToList();

            // Aplicando filtros nos vinculos depois de pesquisar, pois alguns itens dependem de regras de negócio (Vinculo, data inicio e data fim)
            // e o ColaboradorFilterSpecification tem muitas referências, o que pode acarretar bugs em outros lugares caso inclua esses campos lá
            if (filtros.SeqTipoVinculoColaborador.HasValue)
            {
                foreach (var colaborador in colaboradores)
                {
                    colaborador.VinculosAtivos = colaborador.VinculosAtivos.Where(c => c.SeqTipoVinculoColaborador.Equals(filtros.SeqTipoVinculoColaborador)).ToList();
                }
            }

            if (colaboradorFiltroVO.IgnorarFiltros)
            {
                FilterHelper.AtivarFiltros(this);
            }

            var colaboradoresPagerData = new SMCPagerData<ColaboradorListaVO>(colaboradores, total);

            return colaboradoresPagerData;
        }

        /// <summary>
        /// Busca colaboradores com seus dados pessoais para o lookup de colaboradores
        /// </summary>
        /// <param name="seq">Sequencial do colaborador</param>
        /// <returns>Dados do colaborador</returns>
        public ColaboradorLookupVO BuscarColaboradorLookup(long seq)
        {
            try
            {
                FilterHelper.AtivarApenasFiltros(this, FILTER.INSTITUICAO_ENSINO);

                var spec = new SMCSeqSpecification<Colaborador>(seq);

                var colaborador = this.SearchProjectionByKey(spec, c => new ColaboradorLookupVO()
                {
                    Seq = c.Seq,
                    Nome = c.DadosPessoais.Nome,
                    Cpf = c.Pessoa.Cpf,
                    Sexo = c.DadosPessoais.Sexo,
                    DataNascimento = c.Pessoa.DataNascimento,
                    NomeSocial = c.DadosPessoais.NomeSocial,
                    NumeroPassaporte = c.Pessoa.NumeroPassaporte
                });

                return colaborador;
            }
            finally
            {
                FilterHelper.AtivarFiltros(this);
            }
        }

        /// <summary>
        /// Buscar todos os tipos de vinculo baseado na turma.
        /// </summary>
        /// <param name="seqTurma">Sequencial da turma para filtrar apenas os vinculos relacionados a ela</param>
        /// <returns>Dados dos vínculos relacionados a Turma</returns>
        public List<SMCDatasourceItem> BuscarEntidadeVinculoColaboradorPorTurmaSelect(long? seqTurma = null)
        {
            var filtros = new ColaboradorFiltroVO();
            filtros.SeqTurma = seqTurma;

            filtros.SeqsTiposEntidadesVinculo = this.InstituicaoTipoEntidadeVinculoColaboradorDomainService
               .SearchProjectionAll(p => p.InstituicaoTipoEntidade.SeqTipoEntidade, o => o.InstituicaoTipoEntidade.SeqTipoEntidade, isDistinct: true)
               .ToArray();

            /*RN_TUR_027 - Filtro Atividade Colaborador*/
            FiltroAtividadeColaboradorRN_TUR_027(filtros);

            var specColaboradores = new ColaboradorFilterSpecification()
            {
                SeqsColaboradorVinculo = ColaboradorVinculoDomainService.FiltroVinculoColaboradores(ref filtros)
            };
            specColaboradores.MaxResults = Int32.MaxValue;

            // Filtro todos os vínculos dos colaboradores
            var vinculos = this.SearchProjectionBySpecification(specColaboradores, p =>
            new
            {
                vinculos = p.Vinculos.Select(s => new
                {
                    entidade = s.EntidadeVinculo,
                    cursos = s.Cursos.Select(c => c.SeqCursoOfertaLocalidade)
                })
            }).ToList();

            List<SMCDatasourceItem> retorno = new List<SMCDatasourceItem>();

            foreach (var item in vinculos)
            {
                // Percorro os vínculos e filtro pelo CursoOfertaLocalidade, vinculado a Turma
                var itemVinculo = item.vinculos.Where(w => (filtros.SeqCursoOfertaLocalidade.HasValue && w.cursos.Contains(filtros.SeqCursoOfertaLocalidade.Value))
                                                        || (filtros.SeqsCursoOfertaLocalidade != null && w.cursos.Any(a => filtros.SeqsCursoOfertaLocalidade.Contains(a)))
                                                      ).ToList();

                var entidadesVinculo = itemVinculo.Select(s => new SMCDatasourceItem() { Seq = s.entidade.Seq, Descricao = s.entidade.Nome }).ToList();

                retorno.AddRange(entidadesVinculo);
            }

            retorno = retorno.SMCDistinct(d => d.Seq).ToList();

            return retorno;
        }

        /// <summary>
        /// RN_TUR_027 - Filtro Atividade Colaborador
        /// Para listar os colaboradores, levar em consideração:
        ///     * Quando o tipo de turma for diferente de "Livre", identificar os cursos-ofertas-· localidades das
        ///       matrizes e listar os colaboradores ativos associados a estes cursos-ofertas-localidades, que
        ///       possuem tipo de atividade "Aula".
        ///     * Quando o tipo de turma for "Livre", identificar todas as ofertas de matriz que possuem a
        ///       configuração principal da turma e os cursos-ofertas-localidades dessas matrizes.Listar os
        ///       colaboradores ativos associados aos cursos-ofertas-localidades(identificados no filtro anterior), que
        ///      possuem tipo de atividade "Aula".
        /// </summary>
        /// <param name="filtros"></param>
        private void FiltroAtividadeColaboradorRN_TUR_027(ColaboradorFiltroVO filtros)
        {
            if (filtros.SeqTurma.HasValue)
            {
                var turma = TurmaDomainService.SearchProjectionByKey(filtros.SeqTurma.Value, p => new
                {
                    p.SeqCicloLetivoInicio,
                    TurmaLivre = p.TipoTurma.AssociacaoOfertaMatriz == Common.Areas.TUR.Enums.AssociacaoOfertaMatriz.NaoPermite
                });
                if (turma.TurmaLivre)
                {
                    var localidadeTurma = TurmaDomainService.SearchProjectionByKey(filtros.SeqTurma.Value, p => new
                    {
                        p.ConfiguracoesComponente.FirstOrDefault(f => f.Principal).RestricoesTurmaMatriz.FirstOrDefault(f => f.OfertaMatrizPrincipal).MatrizCurricularOferta.SeqCursoOfertaLocalidadeTurno,
                        p.ConfiguracoesComponente.FirstOrDefault(f => f.Principal).RestricoesTurmaMatriz.FirstOrDefault(f => f.OfertaMatrizPrincipal).MatrizCurricularOferta.CursoOfertaLocalidadeTurno.SeqCursoOfertaLocalidade,
                    });
                    filtros.SeqCursoOfertaLocalidade = localidadeTurma.SeqCursoOfertaLocalidade;

                    var periodoLetivo = BuscarDatasEventoLetivo(turma.SeqCicloLetivoInicio, localidadeTurma.SeqCursoOfertaLocalidadeTurno);
                    filtros.DataInicio = periodoLetivo.DataInicio;
                    filtros.DataFim = periodoLetivo.DataFim;
                }
                else
                {
                    var localidadesTurma = TurmaDomainService.SearchProjectionByKey(filtros.SeqTurma.Value, p =>
                        p.ConfiguracoesComponente.SelectMany(s => s.RestricoesTurmaMatriz.Select(sr => new
                        {
                            sr.SeqCursoOfertaLocalidadeTurno,
                            sr.CursoOfertaLocalidadeTurno.SeqCursoOfertaLocalidade
                        }))).ToArray();
                    filtros.SeqsCursoOfertaLocalidade = localidadesTurma.Select(s => s.SeqCursoOfertaLocalidade).ToArray();

                    var periodos = localidadesTurma.Select(s => BuscarDatasEventoLetivo(turma.SeqCicloLetivoInicio, s.SeqCursoOfertaLocalidadeTurno)).ToList();
                    filtros.DataInicio = periodos.Min(m => m.DataInicio);
                    filtros.DataFim = periodos.Max(m => m.DataFim);
                }

                //Assume que se o tipo atividade não vier prenchido assume como valor padrão aula, adaptação feita para atender todos que
                //usarem a função, mantendo uma retrocompatibilidade
                filtros.TipoAtividade = filtros.TipoAtividade.HasValue ? filtros.TipoAtividade.Value : TipoAtividadeColaborador.Aula;
            }
        }

        /// <summary>
        /// Verifica se as quantidades de filiação do tipo de atuação colaborador estão configuradas na instituição
        /// </summary>
        /// <returns>Novo objeto de colaborador caso o tipo de vínculo esteja configurado</returns>
        /// <exception cref="InstituicaoTipoAtuacaoNaoConfiguradaException">Caso o tipo de atuação não esteja configurado na instituição</exception>
        public ColaboradorVO BuscarConfiguracaoColaborador()
        {
            PessoaDomainService.ValidarTipoAtuacaoConfiguradoNaInstituicao(TipoAtuacao.Colaborador);
            var retorno = new ColaboradorVO();
            PessoaAtuacaoDomainService.AplicarValidacaoPermiteCadastrarNomeSocial(ref retorno);
            var specFormacao = new TipoFormacaoEspecificaFilterSpecification() { Token = TOKEN_TIPO_FORMACAO_ESPECIFICA.LINHA_PESQUISA };
            retorno.SeqTipoFormacaoEspecifica = TipoFormacaoEspecificaDomainService.SearchProjectionByKey(specFormacao, p => p.Seq);
            var specHierarquia = new HierarquiaClassificacaoFilterSpecification() { TokenTipoHierarquiaClassificacao = TOKEN_TIPO_HIERARQUIA_CLASSIFICACAO.HIERARQUIA_CNPQ };
            retorno.SeqHierarquiaClassificacao = HierarquiaClassificacaoDomainService.SearchProjectionBySpecification(specHierarquia, p => p.Seq).FirstOrDefault();
            retorno.TitulacaoMaxima = true;
            return retorno;
        }

        /// <summary>
        /// Recupera um colaborador com seus dados pessoais e de contato
        /// </summary>
        /// <param name="seq">Sequencial do colaborado</param>
        /// <returns>Dados do colaborador com suas dependências</returns>
        public ColaboradorVO BuscarColaborador(long seq)
        {
            PessoaDomainService.ValidarTipoAtuacaoConfiguradoNaInstituicao(TipoAtuacao.Colaborador);

            var includesColaborador = IncludesColaborador.DadosPessoais_ArquivoFoto
                                    | IncludesColaborador.Enderecos_PessoaEndereco_Endereco
                                    | IncludesColaborador.EnderecosEletronicos_EnderecoEletronico
                                    | IncludesColaborador.InstituicoesExternas_InstituicaoExterna
                                    | IncludesColaborador.Pessoa_Filiacao
                                    | IncludesColaborador.Telefones_Telefone;

            var colaborador = SearchByKey(new SMCSeqSpecification<Colaborador>(seq), includesColaborador);

            var colaboradorVo = colaborador.Transform<ColaboradorVO>();

            if (colaborador.DadosPessoais.SeqArquivoFoto.HasValue)
                colaboradorVo.ArquivoFoto.GuidFile = colaborador.DadosPessoais.ArquivoFoto.UidArquivo.ToString();

            if (colaboradorVo.Enderecos.SMCCount() > 0)
            {
                // Preenche as descrições dos países
                var paises = LocalidadeService.BuscarPaisesValidosCorreios();
                colaboradorVo.Enderecos.SMCForEach(f => f.DescPais = paises.SingleOrDefault(s => s.Codigo == f.CodigoPais)?.Nome);
            }

            PessoaAtuacaoDomainService.AplicarValidacaoPermiteAlterarCpf(ref colaboradorVo);

            return colaboradorVo;
        }

        /// <summary>
        /// Grava um colaborador com seus dados pessoais e dados de contato
        /// </summary>
        /// <param name="colaboradorVo">Dados do colaborador a ser gravado</param>
        /// <returns>Sequencial do colaborador</returns>
        public long SalvarColaborador(ColaboradorVO colaboradorVo)
        {
            // Valida as datas ao salvar.
            var validarVinculo = colaboradorVo.Transform<ColaboradorVinculoVO>();
            validarVinculo.DataInicio = colaboradorVo.DataInicioVinculo;
            validarVinculo.DataFim = colaboradorVo.DataFimVinculo;
            ColaboradorVinculoDomainService.ValidarDatasVinculo(validarVinculo);

            PessoaAtuacaoDomainService.RestaurarCamposReadonlyCpf(ref colaboradorVo);
            PessoaDomainService.FormatarNomesPessoaVo(ref colaboradorVo);
            Pessoa pessoa = RecuperarPessoaComDependencias(colaboradorVo.SeqPessoa);
            Colaborador colaborador;

            //Atualização dos campos de pessoa
            pessoa.SeqInstituicaoEnsino = colaboradorVo.SeqInstituicaoEnsino;
            pessoa.SeqUsuarioSAS = colaboradorVo.SeqUsuarioSAS;
            pessoa.Cpf = colaboradorVo.Cpf;
            pessoa.NumeroPassaporte = colaboradorVo.NumeroPassaporte;
            pessoa.CodigoPaisEmissaoPassaporte = colaboradorVo.CodigoPaisEmissaoPassaporte;
            pessoa.DataValidadePassaporte = colaboradorVo.DataValidadePassaporte;
            pessoa.DataNascimento = colaboradorVo.DataNascimento;
            pessoa.Falecido = colaboradorVo.Falecido;
            pessoa.TipoNacionalidade = colaboradorVo.TipoNacionalidade;
            pessoa.CodigoPaisNacionalidade = colaboradorVo.CodigoPaisNacionalidade;
            pessoa.Filiacao = colaboradorVo.Filiacao.TransformList<PessoaFiliacao>();

            var dadosPessoais = colaboradorVo.Transform<PessoaDadosPessoais>();
            dadosPessoais.Seq = 0;
            dadosPessoais.Atuacoes = pessoa.Atuacoes.Where(w => w.TipoAtuacao != TipoAtuacao.Colaborador).ToList();

            // Inclui os dados pessoais no histórico
            pessoa.DadosPessoais.Add(dadosPessoais);

            //Se as fotos não foram atualizadas, atualiza com o conteúdo do banco
            pessoa.DadosPessoais.SMCForEach(f => this.EnsureFileIntegrity(f, x => x.SeqArquivoFoto, x => x.ArquivoFoto));

            this.PessoaDomainService.ValidarQuantidadesFiliacao(pessoa, TipoAtuacao.Colaborador);

            if (colaboradorVo.Seq != 0)
            {
                var professor = SearchProjectionByKey(new SMCSeqSpecification<Colaborador>(colaboradorVo.Seq),
                    p => p.Professores.Any(a => a.SituacaoProfessor == SituacaoProfessor.Normal || a.SituacaoProfessor == SituacaoProfessor.Afastado));
                if (professor)
                {
                    var instituicoesExternas = colaboradorVo.InstituicoesExternas?.Where(w => w.Ativo).Select(s => s.SeqInstituicaoExterna).ToArray();
                    var specInstituicoes = new InstituicaoExternaFilterSpecification()
                    {
                        Seqs = instituicoesExternas,
                        SeqInstituicaoEnsino = colaboradorVo.SeqInstituicaoEnsino
                    };
                    if (!instituicoesExternas.SMCAny() || InstituicaoExternaDomainService.Count(specInstituicoes) == 0)
                    {
                        throw new ColaboradorVinculoObrigatorioException();
                    }
                }
            }

            using (ISMCUnitOfWork transacao = SMCUnitOfWork.Begin())
            {
                //Grava a pessoa com suas dependências
                this.PessoaDomainService.SalvarPessoa(pessoa);

                // Recupera os dados do colaborador do vo
                colaborador = colaboradorVo.Transform<Colaborador>();
                // Campo utilizado pela formação acadêmica
                colaborador.Descricao = null;
                colaborador.TipoAtuacao = TipoAtuacao.Colaborador;
                // Remove o último nível para evitar duplicidade
                colaborador.Telefones.SMCForEach(f => f.Telefone = null);
                colaborador.Enderecos.SMCForEach(f => f.PessoaEndereco = null);
                colaborador.EnderecosEletronicos.SMCForEach(f => f.EnderecoEletronico = null);

                if (colaborador.Seq == 0)
                {
                    // Recupera os dados do vinculo do vo
                    var vinculo = colaboradorVo.Transform<ColaboradorVinculo>();
                    vinculo.DataInicio = colaboradorVo.DataInicioVinculo;
                    vinculo.DataFim = colaboradorVo.DataFimVinculo;

                    ColaboradorVinculoDomainService.ValidarDatasVinculo(vinculo.Transform<ColaboradorVinculoVO>());
                    // Limpa a referência para evitar falhas com o tratamento de grafo do orm
                    vinculo.Cursos.SMCForEach(f => f.CursoOfertaLocalidade = null);

                    var vinculoRequerAcompanhamentoSupervisor = TipoVinculoColaboradorDomainService.SearchProjectionByKey(vinculo.SeqTipoVinculoColaborador, p => p.RequerAcompanhamentoSupervisor);
                    if (!vinculoRequerAcompanhamentoSupervisor)
                    {
                        vinculo.TituloPesquisa = null;
                        vinculo.Observacao = null;
                        vinculo.ColaboradoresResponsaveis = null;
                    }

                    colaborador.Vinculos = new List<ColaboradorVinculo>()
                    {
                        vinculo
                    };

                    if (colaboradorVo.SeqTitulacao.HasValue)
                    {
                        var formacaoAcademica = colaboradorVo.Transform<FormacaoAcademica>();
                        formacaoAcademica.DocumentosApresentados = colaboradorVo.SeqDocumentoApresentado
                            .Select(s => new DocumentoApresentadoFormacao()
                            {
                                SeqTitulacaoDocumentoComprobatorio = s
                            }).ToList();
                        colaborador.FormacoesAcademicas = new List<FormacaoAcademica>()
                        {
                            formacaoAcademica
                        };
                    }

                    // Task 51461 - Se não tiver CPF ou Passaporte, não inserir uma pessoa na integração de Dados Mestres.
                    // Task 57733 - Se não informou a data de nascimento, também não deve integrar com os Dados Mestres.
                    if (colaboradorVo.DataNascimento.HasValue && (!string.IsNullOrEmpty(colaboradorVo.Cpf) || !string.IsNullOrEmpty(colaboradorVo.NumeroPassaporte)))
                    {
                        //caso seja inclusão, insere uma pessoa física nos Dados Mestres
                        var pessoaFisicaDadosMetres = colaboradorVo.Transform<InserePessoaFisicaData>();

                        pessoaFisicaDadosMetres.NomeBanco = TOKEN_DADOSMESTRES.BANCO_ACADEMICO;
                        pessoaFisicaDadosMetres.NomeTabela = TOKEN_DADOSMESTRES.BANCO_ACADEMICO_PESSOA;
                        pessoaFisicaDadosMetres.NomeAtributoChave = TOKEN_DADOSMESTRES.BANCO_ACADEMICO_PESSOA_SEQ;
                        pessoaFisicaDadosMetres.SeqAtributoChaveIntegracao = colaboradorVo.SeqPessoa;
                        if (!string.IsNullOrEmpty(pessoaFisicaDadosMetres.Cpf))
                            pessoaFisicaDadosMetres.Cpf = pessoaFisicaDadosMetres.Cpf.SMCRemoveNonDigits();

                        pessoaFisicaDadosMetres.Filiacao = new List<InserePessoaFisicaFiliacaoData>();

                        foreach (var filiacao in colaboradorVo.Filiacao)
                        {
                            pessoaFisicaDadosMetres.Filiacao.Add(new InserePessoaFisicaFiliacaoData()
                            {
                                TipoParentesco = filiacao.TipoParentesco,
                                NomePessoaParentesco = filiacao.Nome
                            });
                        }

                        string msgErroIntegracao = IntegracaoDadoMestreService.InserePessoaFisica(pessoaFisicaDadosMetres);
                        if (!string.IsNullOrEmpty(msgErroIntegracao))
                            throw new SMCApplicationException(msgErroIntegracao);
                    }
                }

                colaborador.SeqPessoaDadosPessoais = dadosPessoais.Seq;
                this.SaveEntity(colaborador);

                // Para vínculos cujo tipo esteja configurado “Criar vínculo institucional” e o vínculo estaja ativo,
                // inserir no CAD uma atuação ativa com o papel = 9 para o código de pessoa do colaborador,
                // caso esta atuação não exista. Se existir torna-la ativa, caso esteja desativada.
                if (colaboradorVo.Seq == 0 &&
                    colaboradorVo.DataInicioVinculo <= DateTime.Today && (!colaboradorVo.DataFimVinculo.HasValue || DateTime.Today <= colaboradorVo.DataFimVinculo) &&
                    TipoVinculoColaboradorDomainService.SearchProjectionByKey(colaboradorVo.SeqTipoVinculoColaborador, p => p.CriaVinculoInstitucional))
                {
                    // Task 51461 - Se não tiver CPF ou Passaporte, nesse ponto não foi inserido uma pessoa em Dados Mestres.
                    // Task 57733 - Se não informou a data de nascimento, também não deve integrar com os Dados Mestres.
                    // Se não integra com DadosMestres, também não integra com o CAD
                    if (colaboradorVo.DataNascimento.HasValue && (!string.IsNullOrEmpty(colaboradorVo.Cpf) || !string.IsNullOrEmpty(colaboradorVo.NumeroPassaporte)))
                    {
                        int seqPessoaCad = PessoaDomainService.BuscarCodigoDePessoaNosDadosMestres(pessoa.Seq, TipoPessoa.Fisica, null, true);
                        var result = PessoaService.IncluirAtuacao(new AtuacaoData()
                        {
                            CodigoPessoa = seqPessoaCad,
                            CodigoPapel = 9,
                            CodigoSistema = "SGA",
                            CodigoConexao = 24
                        });
                        if (!result)
                            throw new SMCApplicationException($"Falha ao criar uma atuação para o colaborador {colaboradorVo.Seq} no CAD");
                    }
                }

                transacao.Commit();
            }

            return colaborador.Seq;
        }

        public List<SMCDatasourceItem> BuscarColaboradoresPorTipoAtividadeSelect(TipoAtividadeColaborador tipoAtividadeColaborador)
        {
            var ret = new List<SMCDatasourceItem>();

            var spec = new ColaboradorFilterSpecification() { SeqsColaboradorVinculo = ColaboradorVinculoDomainService.BuscarColaboradorVinculos(new ColaboradorFiltroVO() { TipoAtividade = tipoAtividadeColaborador }) };

            var result = this.SearchBySpecification(spec, IncludesColaborador.Pessoa | IncludesColaborador.DadosPessoais);

            foreach (var item in result)
            {
                ret.Add(new SMCDatasourceItem() { Seq = item.Seq, Descricao = item.DadosPessoais.Nome });
            }

            return ret;
        }

        public List<SMCDatasourceItem> BuscarColaboradoresPorIngressanteSelect(long seqIngressante)
        {
            var ingressante = this.IngressanteDomainService.SearchProjectionByKey(new SMCSeqSpecification<Ingressante>(seqIngressante),
                i => new
                {
                    SeqIngressante = i.Seq,
                    SeqEntidadeResponsavel = i.SeqEntidadeResponsavel,
                    SeqCursoOfertaLocalidade = i.Ofertas.FirstOrDefault().CampanhaOferta.Itens.FirstOrDefault().CursoOfertaLocalidadeTurno.SeqCursoOfertaLocalidade,
                });

            var colaboradorFiltroVO = new ColaboradorFiltroVO()
            {
                SeqEntidadeVinculo = ingressante.SeqEntidadeResponsavel,
                SeqCursoOfertaLocalidade = ingressante.SeqCursoOfertaLocalidade,
                VinculoAtivo = true,
                TipoAtividade = TipoAtividadeColaborador.Orientacao
            };
            var specColaborador = new ColaboradorFilterSpecification()
            {
                SeqsColaboradorVinculo = ColaboradorVinculoDomainService.FiltroVinculoColaboradores(ref colaboradorFiltroVO)
            };

            specColaborador.SetOrderBy(w => w.DadosPessoais.Nome);

            return this.SearchProjectionBySpecification(specColaborador,
                c => new SMCDatasourceItem()
                {
                    Seq = c.Seq,
                    Descricao = c.DadosPessoais.Nome
                }).ToList();
        }

        /// <summary>
        /// Buscar colaboradores seguindo a RN_ORT_013 - Filtro Orientador do caso de uso Orientação
        /// </summary>
        /// <param name="colaboradorFiltroVO">Dados de filtro</param>
        /// <returns>Sequencial e nome dos orientadores</returns>
        public List<SMCDatasourceItem> BuscarColaboradoresOrientacaoSelect(ColaboradorFiltroVO colaboradorFiltroVO)
        {
            //FIX: Remover ao corrigir o SetParams do SMCDynamic remover esta pog
            colaboradorFiltroVO.Seq = null;

            var spec = colaboradorFiltroVO.Transform<ColaboradorFilterSpecification>();

            var nivelEnsino = NivelEnsinoDomainService.SearchByKey(new SMCSeqSpecification<NivelEnsino>(colaboradorFiltroVO.SeqNivelEnsino.GetValueOrDefault()));
            var niveisVinculoGrupo = new[]
            {
                TOKEN_NIVEL_ENSINO.DOUTORADO_ACADEMICO,
                TOKEN_NIVEL_ENSINO.DOUTORADO_PROFISSIONAL,
                TOKEN_NIVEL_ENSINO.MESTRADO_ACADEMICO,
                TOKEN_NIVEL_ENSINO.MESTRADO_PROFISSIONAL,
            };

            if (nivelEnsino != null && niveisVinculoGrupo.Contains(nivelEnsino.Token))
            {
                colaboradorFiltroVO.TokenEntidadeVinculo = TOKEN_TIPO_ENTIDADE.GRUPO_PROGRAMA;
            }

            colaboradorFiltroVO.TipoAtividade = TipoAtividadeColaborador.Orientacao;

            // Recupera os curso oferta localidade dos alunos selecionados
            // Validando o aluno que é disciplina isolada(DI) ou curso regular(CR)
            List<long> alunosCR = new List<long>();
            List<long> alunosDI = new List<long>();

            //Validar o filtro separando os alunos de DI e CR
            if (colaboradorFiltroVO.SeqsAlunos.SMCAny())
            {
                foreach (var seq in colaboradorFiltroVO.SeqsAlunos)
                {
                    if (AlunoDomainService.SearchProjectionByKey(new SMCSeqSpecification<Aluno>(seq), p => p.Historicos.FirstOrDefault(f => f.Atual).SeqCursoOfertaLocalidadeTurno.HasValue))
                    {
                        alunosCR.Add(seq);
                    }
                    else
                    {
                        //Ajuste para se por ventura tenha vindo alguns aluno invalido
                        if (seq != 0)
                        {
                            alunosDI.Add(seq);
                        }
                    }
                }

                //Preencherá o filtro com os alunos validados
                colaboradorFiltroVO.SeqsAlunos = alunosCR.SMCAny() ? alunosCR.ToArray() : null;
            }

            //Buscará as localidades dos alunos alunos CR caso existam
            var specAlunos = new SMCContainsSpecification<Aluno, long>(p => p.Seq, colaboradorFiltroVO.SeqsAlunos ?? new long[] { });
            var cursoOfertaLocalidadeAlunos = AlunoDomainService.SearchProjectionBySpecification(specAlunos, p =>
                p.Historicos.FirstOrDefault(f => f.Atual).CursoOfertaLocalidadeTurno.SeqCursoOfertaLocalidade
            , isDistinct: true).ToList();

            //Buscará as localidade dos alunos DI caso existam
            if (alunosDI.SMCAny())
            {
                foreach (var aluno in alunosDI)
                {
                    cursoOfertaLocalidadeAlunos.Add(this.PessoaAtuacaoDomainService.RecuperaDadosOrigem(aluno).SeqCursoOfertaLocalidade);
                }
            }

            colaboradorFiltroVO.SeqsCursoOfertaLocalidade = cursoOfertaLocalidadeAlunos.Distinct().ToArray();
            //colaboradorFiltroVO.VinculoAtivo = true;

            ///Lista de Orientadores do(s) alunos
            spec.SeqsColaboradorVinculo = ColaboradorVinculoDomainService.FiltroVinculoColaboradores(ref colaboradorFiltroVO);
            List<long> seqsColaboradoresAlunos = this.SearchProjectionBySpecification(spec, c => c.Seq).ToList();

            ///Considera valido somente os colaboradores que orientem todos os cursos dos alunos selecionados
            seqsColaboradoresAlunos = seqsColaboradoresAlunos.GroupBy(x => x).Where(w => w.Count() == cursoOfertaLocalidadeAlunos.Distinct().Count()).Select(s => s.Key).ToList();

            var contains = new SMCContainsSpecification<Colaborador, long>(p => p.Seq, seqsColaboradoresAlunos.ToArray());
            contains.SetOrderBy(s => s.DadosPessoais.Nome);

            var retorno = this.SearchProjectionBySpecification(contains,
                c => new SMCDatasourceItem()
                {
                    Seq = c.Seq,
                    Descricao = c.DadosPessoais.Nome
                }).ToList();

            return retorno;
        }

        private Pessoa RecuperarPessoaComDependencias(long seqPessoa)
        {
            Pessoa pessoa;
            var specPessoa = new SMCSeqSpecification<Pessoa>(seqPessoa);
            var includesPessoa = IncludesPessoa.DadosPessoais_ArquivoFoto
                               | IncludesPessoa.Atuacoes;

            pessoa = this.PessoaDomainService.SearchByKey(specPessoa, includesPessoa);
            return pessoa;
        }

        private bool ValidarVinculoAtivo(ColaboradorVinculo vinculo)
        {
            var dataAtual = DateTime.Today;
            return vinculo.DataInicio <= dataAtual && (!vinculo.DataFim.HasValue || vinculo.DataFim >= dataAtual);
        }

        /// <summary>
        /// Verifica se o usuário logado é um colaborador para realizar os filtros de acesso a solicitações de matrícula
        /// </summary>
        public bool ValidarColaboradorLogado()
        {
            var spec = new ColaboradorFilterSpecification() { SeqUsuarioSAS = SMCContext.User.SMCGetSequencialUsuario().Value };
            return this.Count(spec) > 0;
        }

        /// <summary>
        /// Busca os colaboradoes que atendam aos filtros informados
        /// </summary>
        /// <param name="filtros">Dados dos filtros</param>
        /// <returns>Lista de colaboradores ordenados por nome</returns>
        public List<SMCDatasourceItem> BuscarColaboradoresSelect(ColaboradorFiltroVO filtros)
        {
            var spec = filtros.Transform<ColaboradorFilterSpecification>();

            PrepararFiltro(ref filtros, ref spec);

            if (filtros.AptoLecionarComponenteTurma.GetValueOrDefault())
            {
                var config = InstituicaoNivelDomainService.BuscarInstituicaoNivelPorTurma(filtros.SeqTurma.Value);
                if (config.VerificaComponenteAptoLecionar)
                {
                    var seqCompomente = TurmaDomainService.SearchProjectionByKey(filtros.SeqTurma.Value, p => p
                        .ConfiguracoesComponente.FirstOrDefault(f => f.Principal)
                        .ConfiguracaoComponente
                        .SeqComponenteCurricular);
                    spec.SeqCompomenteAptoLecionar = seqCompomente;
                }
            }

            return this.SearchProjectionBySpecification(spec, p => new
            {
                p.Seq,
                p.DadosPessoais.Nome,
                p.DadosPessoais.NomeSocial
            }, true)
            .ToList()
            .Select(s => new SMCDatasourceItem(s.Seq, string.IsNullOrEmpty(s.NomeSocial) ? s.Nome : $"{s.NomeSocial} ({s.Nome})"))
            .OrderBy(o => o.Descricao)
            .ToList();
        }

        /// <summary>
        /// Recupera um professor com a instituição selecionada no portal
        /// </summary>
        /// <param name="seqUsuarioSAS">Sequencial do usuario SAS</param>
        /// <param name="seqInstituicaoEnsino">Sequencial da instituição de ensino</param>
        /// <returns>Dados do colaborador</returns>
        public ColaboradorVO BuscarProfessorLogin(long seqUsuarioSAS, long seqInstituicaoEnsino)
        {
            var spec = new ColaboradorFilterSpecification() { SeqUsuarioSAS = seqUsuarioSAS, SeqInstituicaoEnsino = seqInstituicaoEnsino };

            var includesColaborador = IncludesColaborador.DadosPessoais_ArquivoFoto
                                    | IncludesColaborador.Pessoa_Filiacao;
            var colaborador = this.SearchByKey(spec, includesColaborador);
            var colaboradorVo = colaborador.Transform<ColaboradorVO>();

            if (colaborador.DadosPessoais.SeqArquivoFoto.HasValue)
                colaboradorVo.ArquivoFoto.GuidFile = colaborador.DadosPessoais.ArquivoFoto.UidArquivo.ToString();

            return colaboradorVo;
        }

        /// <summary>
        /// Busca as identidades acadêmicas dos colaboradores informados
        /// </summary>
        /// <param name="seqs">Sequenciais dos colaboradores a serem recuperados</param>
        /// <returns>Dados das identidades acadêmcias dos colaboradores</returns>
        public List<IdentidadeEstudantilVO> BuscarColaboradoresIdentidadeAcademica(List<long> seqs)
        {
            var spec = new SMCContainsSpecification<Colaborador, long>(p => p.Seq, seqs.ToArray());
            var colaboradores = SearchProjectionBySpecification(spec, p => new IdentidadeEstudantilVO()
            {
                SeqPessoa = p.SeqPessoa,
                SeqColaborador = p.Seq,
                Nome = p.DadosPessoais.Nome,
                RegistroDV = p.Seq.ToString(),
                DescricaoTipoEntidadeResponsavel = MessagesResource.Label_Programa,
                NumeroVia = 1,
                // Dados do vínculo institucional mais recente
                ColaboradorVinculo = p.Vinculos
                    .Where(w => w.TipoVinculoColaborador.CriaVinculoInstitucional && (w.DataFim == null || w.DataFim >= DateTime.Now))
                    .OrderByDescending(o => o.DataInicio)
                    .Select(s => new ColaboradorVinculoIdentidadeAcademicaVO()
                    {
                        DataValidade = s.DataFim,
                        SeqPrograma = s.SeqEntidadeVinculo,
                        DescricaoEntidadeResponsavel = s.EntidadeVinculo.Nome
                    })
                    .FirstOrDefault()
            }).ToList();

            var identidadesVencidas = colaboradores
                .Where(w => w.ColaboradorVinculo == null || (w.ColaboradorVinculo?.DataValidade ?? DateTime.MaxValue) < DateTime.Now)
                .OrderBy(o => o.Nome)
                .Select(s => s.Nome)
                .ToList();
            if (identidadesVencidas.SMCAny())
            {
                throw new IdentidadeAcademicaVencidaException(identidadesVencidas);
            }

            foreach (var colaborador in colaboradores)
            {
                colaborador.Codigo = $"02{PessoaDomainService.BuscarCodigoDePessoaNosDadosMestres(colaborador.SeqPessoa, TipoPessoa.Fisica, null).ToString("d8")}{colaborador.NumeroVia.ToString("d2")}";
                colaborador.DataValidade = colaborador.ColaboradorVinculo.DataValidade;
                colaborador.SeqPrograma = colaborador.ColaboradorVinculo.SeqPrograma;
                colaborador.DescricaoEntidadeResponsavel = colaborador.ColaboradorVinculo.DescricaoEntidadeResponsavel;
            }
            return colaboradores;
        }

        /// <summary>
        /// Busca os Colaboradores que tenham a atividade do tipo Orientação
        /// </summary>
        /// <param name="colaboradorFiltroVO">Filtro realizado na tela</param>
        /// <returns>Lista de colaboradores Orientadores ordenados por nome</returns>
        public List<SMCDatasourceItem> BuscarColaboradoresOrientadores(ColaboradorOrientadorFiltroVO colaboradorFiltroVO)
        {
            var filtros = colaboradorFiltroVO.Transform<ColaboradorFilterSpecification>();
            var filtroVinculo = colaboradorFiltroVO.Transform<ColaboradorFiltroVO>();

            ///Atribui ao filtro o tipo de atividade de colaborador Orientador
            filtroVinculo.TipoAtividade = TipoAtividadeColaborador.Orientacao;

            filtroVinculo.SeqCursoOfertaLocalidade = colaboradorFiltroVO.SeqLocalidade;

            if (colaboradorFiltroVO.SeqEntidadeResponsavel != null && colaboradorFiltroVO.SeqEntidadeResponsavel.Any())
            {
                var specEntidade = new EntidadeFilterSpecification() { Seqs = colaboradorFiltroVO.SeqEntidadeResponsavel.ToArray() };

                //Busco os tipos de entidades, das entidades selecionadas
                var tiposEntidade = EntidadeDomainService.SearchProjectionBySpecification(specEntidade, x => x.TipoEntidade);
                filtroVinculo.TokensEntidadeVinculo = new List<string>();
                foreach (var tipo in tiposEntidade)
                {
                    //Verifico se o tipo de entidade respeita a regra
                    //NV06 - Filtra colaboradores com vínculo no Departamento ou programa
                    if (tipo.Token == TOKEN_TIPO_ENTIDADE.GRUPO_PROGRAMA
                        || tipo.Token == TOKEN_TIPO_ENTIDADE.DEPARTAMENTO)
                    {
                        //Adiciono apenas um tipo de token a lista
                        if (!filtroVinculo.TokensEntidadeVinculo.Contains(tipo.Token))
                        {
                            filtroVinculo.TokensEntidadeVinculo.Add(tipo.Token);
                        }
                    }
                }
            }

            filtros.SeqsColaboradorVinculo = ColaboradorVinculoDomainService.FiltroVinculoColaboradores(ref filtroVinculo);
            return this.SearchProjectionBySpecification(filtros,
               c => new SMCDatasourceItem()
               {
                   Seq = c.Seq,
                   Descricao = c.DadosPessoais.Nome
               }).ToList();
        }

        /// <summary>
        /// Buscar colaboradores para turma
        /// </summary>
        /// <param name="seqTurma">Sequencial da turma</param>
        /// <param name="tipoAtividadeColaborador">Atividade do colaborador</param>
        /// <returns>Lista de colaboradores em formato select</returns>
        public List<SMCDatasourceItem> BuscarColaboradoresPorTurmaSelect(long seqTurma, TipoAtividadeColaborador tipoAtividadeColaborador)
        {
            // Usa a busca completa de colaborador para aplicar as regras de turma
            var listaColaboradores = BuscarColaboradores(new ColaboradorFiltroVO() { SeqTurma = seqTurma, TipoAtividade = tipoAtividadeColaborador });

            var retorno = listaColaboradores.Select(s => new SMCDatasourceItem()
            {
                Seq = s.Seq,
                Descricao = s.Nome
            }).ToList();

            return retorno;
        }

        /// <summary>
        /// Buscar professores aptos a lecionar na grade com seu vinculo ativo mais longo
        /// </summary>
        /// <param name="colaboradorFiltroVO">Filtro do colaborador</param>
        /// <returns>Dados dos professores com seus vinculos</returns>
        public List<ColaboradorGradeVO> BuscarColaboradoresAptoLecionarGrade(ColaboradorFiltroVO colaboradorFiltroVO)
        {
            var filtros = colaboradorFiltroVO.Transform<ColaboradorFilterSpecification>();
            List<ColaboradorGradeVO> retorno = new List<ColaboradorGradeVO>();

            //Valida se a turma é compartilhada
            var turma = TurmaDomainService.SearchProjectionByKey(colaboradorFiltroVO.SeqTurma.Value, p => new
            {
                p.SeqCicloLetivoInicio,
                p.DataInicioPeriodoLetivo,
                p.DataFimPeriodoLetivo,
                TurmaCompartilhada = p.TipoTurma.AssociacaoOfertaMatriz == Common.Areas.TUR.Enums.AssociacaoOfertaMatriz.ExigeMaisDeUma,
                RestricoesMartriz = p.ConfiguracoesComponente.SelectMany(s => s.RestricoesTurmaMatriz.Select(sm => sm.CursoOfertaLocalidadeTurno.SeqCursoOfertaLocalidade))

            });

            PrepararFiltro(ref colaboradorFiltroVO, ref filtros);

            if (colaboradorFiltroVO.IgnorarFiltros)
            {
                FilterHelper.AtivarApenasFiltros(this, FILTER.INSTITUICAO_ENSINO);
            }

            //Conforme demanda 49686 passamos a trazer todos os colaboradores com vinculo no periodo letivo
            //var dataAtual = DateTime.Today;
            var colaboradores = SearchProjectionBySpecification(filtros, p => new ColaboradorListaVO()
            {
                Seq = p.Seq,
                Nome = p.DadosPessoais.Nome,
                NomeSocial = p.DadosPessoais.NomeSocial,
                VinculosAtivos = p.Vinculos
                    //.Where(vinculo => vinculo.DataInicio <= dataAtual && (!vinculo.DataFim.HasValue || vinculo.DataFim >= dataAtual))
                    .OrderByDescending(o => o.DataInicio)
                    .ThenByDescending(o => o.DataFim)
                    .ThenBy(o => o.EntidadeVinculo.Nome)
                    .Select(s => new ColaboradorVinculoListaVO()
                    {
                        Seq = s.Seq,
                        SeqColaborador = s.SeqColaborador,
                        SeqEntidadeVinculo = s.SeqEntidadeVinculo,
                        DataInicio = s.DataInicio,
                        DataFim = s.DataFim,
                        InseridoPorCarga = s.InseridoPorCarga,
                        NomeEntidadeVinculo = s.EntidadeVinculo.Nome,
                        DescricaoTipoVinculoColaborador = s.TipoVinculoColaborador.Descricao,
                        Cursos = s.Cursos.Select(c => new ColaboradorVinculoCursoVO()
                        {
                            SeqCursoOfertaLocalidade = c.SeqCursoOfertaLocalidade,
                            TipoAtividadeColaborador = c.Atividades.Select(a => a.TipoAtividadeColaborador).ToList()
                        }).ToList()
                    }).ToList()
            }).ToList();

            if (colaboradorFiltroVO.IgnorarFiltros)
            {
                FilterHelper.AtivarFiltros(this);
            }

            colaboradores.ForEach(f =>
            {
                List<ColaboradorVinculoListaVO> vinculosColaborador = new List<ColaboradorVinculoListaVO>();
                //Conforme bug 57416 os vinculos dos professores serão baseados em nas restrições das matrizes das quais são cadastrados
                //não mais levando em consideração se a turma é compartilhada.
                foreach (var item in turma.RestricoesMartriz)
                {
                    vinculosColaborador.AddRange(f.VinculosAtivos.Where(w => w.Cursos.Select(s => s.SeqCursoOfertaLocalidade)
                                                                                         .Contains(item)).ToList());
                }

                //Busca remover os vinculos duplicados
                vinculosColaborador = vinculosColaborador.Distinct().ToList();

                if (vinculosColaborador.SMCAny())
                {
                    string nome = string.IsNullOrEmpty(f.NomeSocial) ? f.Nome : $"{f.NomeSocial} ({f.Nome})";
                    //var viculo = BuscarVinculoLongo(vinuclosColaborador);
                    var vinculos = BuscarVinculoAtivosPeriodoAula(vinculosColaborador, turma.DataInicioPeriodoLetivo, turma.DataFimPeriodoLetivo);
                    int numVinculo = 1;
                    foreach (var vinculo in vinculos)
                    {
                        retorno.Add(new ColaboradorGradeVO
                        {
                            Seq = f.Seq,
                            Nome = nome,
                            NomeFormatado = vinculo.DataFim.HasValue ? $"{numVinculo}º) {vinculo.DataInicio.SMCDataAbreviada()} à  {vinculo.DataFim.SMCDataAbreviada()}"
                                                         : $"{numVinculo}º) {vinculo.DataInicio.SMCDataAbreviada()} à não informado",
                            Vinculos = vinculos.TransformList<ColaboradorGradeVinculosVO>()
                        });
                        numVinculo++;
                    }

                    var vinculosFormatar = retorno.Where(w => w.Seq == f.Seq).ToList();
                    if (vinculosFormatar.SMCAny())
                    {
                        var nomeFormatado = vinculosFormatar.FirstOrDefault().Nome + " - Vínculos: " + string.Join(" - ",
                                                                                                        retorno.Where(w => w.Seq == f.Seq)
                                                                                                        .Select(s => s.NomeFormatado).ToList());
                        foreach (var item in vinculosFormatar)
                        {
                            item.NomeFormatado = nomeFormatado;
                        }
                    }
                }
            });

            return retorno;
        }

        /// <summary>
        /// Buscar o vinculo mais longo
        /// </summary>
        /// <param name="vinculos">Lista de vinculos</param>
        private ColaboradorVinculoListaVO BuscarVinculoLongo(List<ColaboradorVinculoListaVO> vinculos)
        {
            ColaboradorVinculoListaVO vinculosSemDataFim = vinculos.Where(w => !w.DataFim.HasValue).OrderBy(o => o.DataInicio).FirstOrDefault();

            if (vinculosSemDataFim != null)
            {
                return vinculosSemDataFim;
            }
            else
            {
                var dicVinculos = new Dictionary<ColaboradorVinculoListaVO, int>();

                vinculos.ForEach(vinculo =>
                {
                    var diasVinculo = vinculo.DataFim.Value.Subtract(vinculo.DataInicio).Days;
                    dicVinculos.Add(vinculo, diasVinculo);
                });

                return dicVinculos.OrderByDescending(o => o.Value).FirstOrDefault().Key;
            }
        }

        /// <summary>
        /// Recupera as datas do evento letivo conforme a regra RN_CAM_030
        /// </summary>
        /// <returns>Datas do período letivo</returns>
        private (DateTime DataInicio, DateTime DataFim) BuscarDatasEventoLetivo(long seqCicloLetivoInicio, long seqCursoOfertaLocalidadeTurno)
        {
            var periodoLetivo = ConfiguracaoEventoLetivoDomainService.BuscarDatasEventoLetivo(seqCicloLetivoInicio,
                seqCursoOfertaLocalidadeTurno,
                TipoAluno.Veterano,
                TOKEN_TIPO_EVENTO.PERIODO_LETIVO);
            return (periodoLetivo.DataInicio, periodoLetivo.DataFim);
        }

        /// <summary>
        /// Aplica as regras de filtro de colaborador e trata tambem os filtros por vinculo.
        /// </summary>
        /// <param name="filtroVO">Dados originais do filtro</param>
        /// <param name="specification">Filtro transformado em spec que será atualizado caso necessário</param>
        private void PrepararFiltro(ref ColaboradorFiltroVO filtroVO, ref ColaboradorFilterSpecification specification)
        {
            // Caso apenas uma das datas seja preenchida
            if (filtroVO.DataInicio.HasValue ^ filtroVO.DataFim.HasValue)
            {
                throw new ColaboradorIntervaloDatasIncompletoException();
            }

            if (filtroVO.SeqInstituicaoExterna.HasValue)
            {
                var specInstituicao = new SMCSeqSpecification<InstituicaoExterna>(filtroVO.SeqInstituicaoExterna.GetValueOrDefault());
                specification.SeqEntidadeInstituicaoExterna = this.InstituicaoExternaDomainService.SearchProjectionByKey(specInstituicao, p => p.SeqInstituicaoEnsino);
            }

            if (filtroVO.Oritentador == true)
            {
                filtroVO.TipoAtividade = TipoAtividadeColaborador.Orientacao;
                filtroVO.PermiteInclusaoManualVinculo = true;
            }

            /*RN_TUR_027 - Filtro Atividade Colaborador*/
            FiltroAtividadeColaboradorRN_TUR_027(filtroVO);

            specification.SeqsColaboradorVinculo = ColaboradorVinculoDomainService.FiltroVinculoColaboradores(ref filtroVO);

            if (filtroVO.SeqCampanhaOferta.HasValue)
            {
                var campanha = CampanhaOfertaDomainService
                    .SearchProjectionByKey(new SMCSeqSpecification<CampanhaOferta>(filtroVO.SeqCampanhaOferta.Value), p => new
                    {
                        p.Campanha.SeqEntidadeResponsavel,
                        p.Itens.FirstOrDefault().CursoOfertaLocalidadeTurno.SeqCursoOfertaLocalidade
                    });
                filtroVO.SeqEntidadeVinculo = campanha.SeqEntidadeResponsavel;
                filtroVO.SeqCursoOfertaLocalidade = campanha.SeqCursoOfertaLocalidade;
                specification.SeqsColaboradorVinculo = ColaboradorVinculoDomainService.FiltroVinculoColaboradores(ref filtroVO);
            }

            if (filtroVO.SeqTipoVinculoColaborador.HasValue)
            {
                specification.SeqTipoVinculoColaborador = filtroVO.SeqTipoVinculoColaborador;
            }
        }

        /// <summary>
        /// Buscar vinculos ativos em um periodo de aula da turma
        /// </summary>
        /// <param name="vinculos">Vinculos do professor</param>
        /// <param name="DataFimPeriodoAula">Data periodo de aula</param>
        /// <param name="DataIncioPeriodoAula">Data inicio de aula</param>
        /// <returns></returns>
        private List<ColaboradorVinculoListaVO> BuscarVinculoAtivosPeriodoAula(List<ColaboradorVinculoListaVO> vinculos, DateTime DataIncioPeriodoAula, DateTime DataFimPeriodoAula)
        {
            List<ColaboradorVinculoListaVO> retorno = new List<ColaboradorVinculoListaVO>();

            foreach (var vinculo in vinculos.OrderBy(o => o.DataInicio))
            {
                //O método CompareTo() retorna um valor negativo se a data atual for anterior à data fornecida,
                //zero se forem iguais e um valor positivo se a data atual for posterior à data fornecida.
                if (!vinculo.DataFim.HasValue)
                {
                    if (vinculo.DataInicio.CompareTo(DataFimPeriodoAula) <= 0)
                        retorno.Add(vinculo);
                }
                else
                {
                    if (vinculo.DataInicio.CompareTo(DataIncioPeriodoAula) >= 0 && vinculo.DataInicio.CompareTo(DataFimPeriodoAula) < 0)
                    {
                        retorno.Add(vinculo);
                    }
                    else if (vinculo.DataFim.Value.CompareTo(DataIncioPeriodoAula) >= 0 && vinculo.DataFim.Value.CompareTo(DataFimPeriodoAula) < 0)
                    {
                        retorno.Add(vinculo);
                    }
                    else if (DataIncioPeriodoAula.CompareTo(vinculo.DataInicio) < 0 && DataFimPeriodoAula.CompareTo(vinculo.DataFim.Value) >= 0)
                    {
                        retorno.Add(vinculo);
                    }
                    else if (vinculo.DataInicio.CompareTo(DataIncioPeriodoAula) < 0 && vinculo.DataFim.Value.CompareTo(DataFimPeriodoAula) >= 0)
                    {
                        retorno.Add(vinculo);
                    }
                }
            };

            return retorno;
        }




        /// <summary>
        /// Retorna a situação do colaborador
        /// </summary>
        /// <param name="seqColaborador"></param>
        /// <returns></returns>
        public bool BuscarSituacaoColaborador(long seqColaborador, out bool colaboradorAtivo)
        {
            /* Task 52812
             * Permitir que o usuário informe a data fim do vinculo para todos os colaboradores  que estejam com a situação 'normal'. 
             * Caso o colaborador esteja com a situação 'afastado' ou 'demitido' somente os usuários que possuem permissão no token Permitir Alterar Data Fim Vinculo 
               poderão alterar este campo.
            */
            bool permiteAlterarDataFim = false;
            bool colaboradorAtivoFinal = false;

            var professores = SearchProjectionByKey(new SMCSeqSpecification<Colaborador>(seqColaborador),
                      p => p.Professores.OrderByDescending(c => c.DataInclusao).ThenByDescending(c => c.DataAlteracao));

            if (professores.Count() > 0)
            {
                if (professores.Any(c => c.SituacaoProfessor == SituacaoProfessor.Normal))
                {
                    permiteAlterarDataFim = true;
                    colaboradorAtivoFinal = true;
                }
                else if (professores.Any(c => c.SituacaoProfessor == SituacaoProfessor.Afastado || c.SituacaoProfessor == SituacaoProfessor.Demitido))
                {
                    var usuarioPossuiTokenAlterarDataFim = SMCSecurityHelper.Authorize(UC_DCT_001_06_04.PERMITIR_ALTERAR_DATA_FIM_VINCULO);

                    if (usuarioPossuiTokenAlterarDataFim)
                    {
                        permiteAlterarDataFim = true;
                        colaboradorAtivoFinal = true;
                    }
                }
            }

            colaboradorAtivo = colaboradorAtivoFinal;
            return permiteAlterarDataFim;
        }


        public SMCPagerData<EnvioNotificacaoPessoasListarVO> BuscarColaboradoresEnvioNotificacao(ColaboradorFiltroVO colaboradorFiltroVO)
        {
            var lista = BuscarColaboradoresNotificacaoListarVO(colaboradorFiltroVO, out int total);
            var listaOrdenada = lista.OrderBy(x => x.Nome);
            return new SMCPagerData<EnvioNotificacaoPessoasListarVO>(listaOrdenada, total);
        }
        
        public IEnumerable<EnvioNotificacaoPessoasListarVO> BuscarColaboradoresNotificacaoListarVO(ColaboradorFiltroVO colaboradorFiltroVO, out int total)
        {
            var filtros = colaboradorFiltroVO.Transform<ColaboradorFilterSpecification>();
            filtros.SetOrderBy(p => p.DadosPessoais.Nome);

            PrepararFiltro(ref colaboradorFiltroVO, ref filtros);

            if (colaboradorFiltroVO.IgnorarFiltros)
            {
                FilterHelper.AtivarApenasFiltros(this, FILTER.INSTITUICAO_ENSINO);
            }
            var retornoColaboradores = new List<EnvioNotificacaoPessoasListarVO>();
            var colaboradores = SearchProjectionBySpecification(filtros, p => new
            {
                Seq = p.Seq,
                Nome = p.DadosPessoais.Nome,
                Email = p.EnderecosEletronicos.Where(e => e.EnderecoEletronico.TipoEnderecoEletronico == TipoEnderecoEletronico.Email).Select(e => e.EnderecoEletronico.Descricao).ToList()

            }, out total).ToList();
            foreach (var colaborador in colaboradores)
            {
                retornoColaboradores.Add(new EnvioNotificacaoPessoasListarVO()
                {
                    Seq = colaborador.Seq,
                    Nome = colaborador.Nome,
                    Email = string.Join(";", colaborador.Email)
                });
            }

            if (colaboradorFiltroVO.IgnorarFiltros)
            {
                FilterHelper.AtivarFiltros(this);
            }

            return retornoColaboradores;
        }
       
    }
}