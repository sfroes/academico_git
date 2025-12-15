using Newtonsoft.Json;
using SMC.Academico.Common.Areas.CNC.Constants;
using SMC.Academico.Common.Areas.CNC.Exceptions;
using SMC.Academico.Common.Areas.CNC.Exceptions.SolicitacaoDocumentoConclusao;
using SMC.Academico.Common.Areas.CSO.Includes;
using SMC.Academico.Common.Areas.CUR.Exceptions;
using SMC.Academico.Common.Areas.CUR.Includes;
using SMC.Academico.Common.Areas.Shared.Helpers;
using SMC.Academico.Common.Constants;
using SMC.Academico.Domain.Areas.CNC.DomainServices;
using SMC.Academico.Domain.Areas.CNC.Specifications;
using SMC.Academico.Domain.Areas.CNC.ValueObjects;
using SMC.Academico.Domain.Areas.CSO.DomainServices;
using SMC.Academico.Domain.Areas.CSO.Models;
using SMC.Academico.Domain.Areas.CUR.Models;
using SMC.Academico.Domain.Areas.CUR.Resources;
using SMC.Academico.Domain.Areas.CUR.Specifications;
using SMC.Academico.Domain.Areas.CUR.ValueObjects;
using SMC.Academico.Domain.Areas.ORG.DomainServices;
using SMC.Academico.Domain.Areas.ORG.Specifications;
using SMC.Academico.Domain.Areas.ORG.ValueObjects;
using SMC.Framework;
using SMC.Framework.Model;
using SMC.Framework.Repository;
using SMC.Framework.Rest;
using SMC.Framework.Specification;
using System;
using System.Collections.Generic;
using System.Linq;
using MantenedoraVO = SMC.Academico.Domain.Areas.CNC.ValueObjects.MantenedoraVO;

namespace SMC.Academico.Domain.Areas.CUR.DomainServices
{
    public class CurriculoDomainService : AcademicoContextDomain<Curriculo>
    {
        #region [ Serviços ]

        private CursoDomainService CursoDomainService => this.Create<CursoDomainService>();

        private CursoOfertaDomainService CursoOfertaDomainService => this.Create<CursoOfertaDomainService>();

        private InstituicaoNivelDomainService InstituicaoNivelDomainService => this.Create<InstituicaoNivelDomainService>();

        private DocumentoAcademicoCurriculoDomainService DocumentoAcademicoCurriculoDomainService => this.Create<DocumentoAcademicoCurriculoDomainService>();

        private DocumentoAcademicoHistoricoSituacaoDomainService DocumentoAcademicoHistoricoSituacaoDomainService => this.Create<DocumentoAcademicoHistoricoSituacaoDomainService>();

        private TipoDocumentoAcademicoDomainService TipoDocumentoAcademicoDomainService => Create<TipoDocumentoAcademicoDomainService>();

        private InstituicaoNivelTipoDocumentoAcademicoDomainService InstituicaoNivelTipoDocumentoAcademicoDomainService => Create<InstituicaoNivelTipoDocumentoAcademicoDomainService>();

        private InstituicaoEnsinoDomainService InstituicaoEnsinoDomainService => Create<InstituicaoEnsinoDomainService>();

        private MantenedoraDomainService MantenedoraDomainService => Create<MantenedoraDomainService>();

        private AtoNormativoEntidadeDomainService AtoNormativoEntidadeDomainService => Create<AtoNormativoEntidadeDomainService>();

        private CursoOfertaLocalidadeDomainService CursoOfertaLocalidadeDomainService => Create<CursoOfertaLocalidadeDomainService>();

        private SolicitacaoDocumentoConclusaoDomainService SolicitacaoDocumentoConclusaoDomainService => Create<SolicitacaoDocumentoConclusaoDomainService>();

        #endregion [ Serviços ]

        #region [ Querys ]

        private const string QUERY_BUSCAR_DADOS_CURSO_OFERTA_LOCALIDADE =
           @" SELECT 
              primeiraConsulta.SeqCursoOfertaLocalidade,
			  primeiraConsulta.CodigoCurriculoMigracao,
              primeiraConsulta.DataReferencia,
              (
                  SELECT TOP 1 seq_grau_academico
                  FROM (
                      -- Primeira prioridade (1)
                      SELECT TOP 1
                          cfe.seq_grau_academico,
                          1 AS prioridade
                      FROM cso.curso_oferta_localidade col
                      INNER JOIN cso.curso_oferta co 
                          ON col.seq_curso_oferta = co.seq_curso_oferta
                      INNER JOIN cso.formacao_especifica fe 
                          ON co.seq_formacao_especifica = fe.seq_formacao_especifica
                      INNER JOIN cso.curso_formacao_especifica cfe 
                          ON fe.seq_formacao_especifica = cfe.seq_formacao_especifica
                      WHERE col.seq_entidade = primeiraConsulta.SeqCursoOfertaLocalidade
          
                      UNION ALL
          
                      -- Segunda prioridade (2)
                      SELECT TOP 1
                          cfe.seq_grau_academico,
                          2 AS prioridade
                      FROM cso.curso_oferta_localidade col
                      INNER JOIN cso.curso_oferta_localidade_formacao colf 
                          ON col.seq_entidade = colf.seq_entidade_curso_oferta_localidade
                      INNER JOIN cso.formacao_especifica fe 
                          ON colf.seq_formacao_especifica = fe.seq_formacao_especifica
                      INNER JOIN cso.curso_formacao_especifica cfe 
                          ON fe.seq_formacao_especifica = cfe.seq_formacao_especifica
                      WHERE col.seq_entidade = primeiraConsulta.SeqCursoOfertaLocalidade
          			ORDER BY fe.dat_inclusao, cfe.seq_grau_academico
                  ) AS grau_academico
                  ORDER BY prioridade
              ) AS SeqGrauAcademico
          FROM (
              SELECT TOP 1 
                  col.seq_entidade AS SeqCursoOfertaLocalidade, 
				  c.cod_curriculo_migracao AS CodigoCurriculoMigracao, 
                  hsmco.dat_inicio AS DataReferencia
              FROM cur.curriculo c   
              INNER JOIN cur.curriculo_curso_oferta cco 
                  ON c.seq_curriculo = cco.seq_curriculo
              INNER JOIN CUR.matriz_curricular mc 
                  ON cco.seq_curriculo_curso_oferta = mc.seq_curriculo_curso_oferta
              INNER JOIN cur.matriz_curricular_oferta mco 
                  ON mc.seq_matriz_curricular = mco.seq_matriz_curricular
              INNER JOIN cur.historico_situacao_matriz_curricular_oferta hsmco 
                  ON mco.seq_matriz_curricular_oferta = hsmco.seq_matriz_curricular_oferta
              INNER JOIN cso.curso_oferta_localidade_turno colt 
                  ON mco.seq_curso_oferta_localidade_turno = colt.seq_curso_oferta_localidade_turno
              INNER JOIN cso.curso_oferta_localidade col 
                  ON colt.seq_entidade_curso_oferta_localidade = col.seq_entidade
              WHERE c.seq_curriculo = @SEQ_CURRICULO
              ORDER BY hsmco.dat_inicio, col.seq_entidade
          ) AS primeiraConsulta";

        #endregion [ Querys ]

        #region APIS

        public SMCApiClient APICurriculoGAD => SMCApiClient.Create("CurriculoGAD");

        #endregion APIS

        public Curriculo BuscarConfiguracoesCurriculo(long seqCurso)
        {
            var curriculo = new Curriculo() { SeqCurso = seqCurso };
            this.PreencherConfiguracoes(ref curriculo, IncludesCurso.CursosOferta);

            //// Caso tenha apenas uma oferta de curso, esta já virá no detalhe
            //if (curriculo.Curso.CursosOferta.Count(p => p.Ativo) == 1)
            //{
            //    curriculo.CursosOferta = curriculo
            //        .Curso
            //        .CursosOferta
            //        .Where(w => w.Ativo)
            //        .Select(s => new CurriculoCursoOferta() { SeqCursoOferta = s.Seq })
            //        .ToList();
            //}

            return curriculo;
        }

        public Curriculo BuscarCurriculo(long seq)
        {
            var includesCurriculo = IncludesCurriculo.Curso
                                  | IncludesCurriculo.CursosOferta;
            var curriculo = this.SearchByKey(new SMCSeqSpecification<Curriculo>(seq), includesCurriculo);
            curriculo.PermiteCreditoComponenteCurricular = this.CreditoComponenteCurricularPermitido(curriculo.Curso.SeqNivelEnsino);
            return curriculo;
        }

        /// <summary>
        /// Busca todos Currículos com suas Ofertas de Cursos e Grupos Curriculares
        /// </summary>
        /// <param name="filtros">Filtros dos Currículos</param>
        /// <returns>Lista paginada dos Currículos com Ofertas de Cursos e Grupos Curriculares que atendam aos filtros informados</returns>
        public SMCPagerData<CurriculoListaVO> BuscarCurriculos(CurriculoFilterSpecification filtros)
        {
            int total;

            var curriculosVO = this.SearchProjectionBySpecification(filtros, curriculo => new CurriculoListaVO()
            {
                Seq = curriculo.Seq,
                Codigo = curriculo.Codigo,
                Descricao = curriculo.Descricao,
                DescricaoComplementar = curriculo.DescricaoComplementar,
                Ativo = curriculo.Ativo,
                CursosOferta = curriculo.CursosOferta.Select(cursoOferta => new CurriculoCursoOfertaListaVO
                {
                    Seq = cursoOferta.Seq,
                    Descricao = cursoOferta.CursoOferta.Descricao,
                    QuantidadeCreditoObrigatorioTotal = cursoOferta.QuantidadeCreditoObrigatorio ?? 0,
                    QuantidadeHoraAulaObrigatoriaTotal = cursoOferta.QuantidadeHoraAulaObrigatoria ?? 0,
                    QuantidadeCreditoOptativoTotal = cursoOferta.QuantidadeCreditoOptativo ?? 0,
                    QuantidadeHoraAulaOptativaTotal = cursoOferta.QuantidadeHoraAulaOptativa ?? 0,
                    GruposCurriculares = cursoOferta.GruposCurriculares.Select(grupoCurricular => new CurriculoCursoOfertaGrupoListaVO()
                    {
                        Seq = grupoCurricular.Seq,
                        Descricao = grupoCurricular.GrupoCurricular.Descricao
                    })
                })
            }, out total);

            return new SMCPagerData<CurriculoListaVO>(curriculosVO, total);
        }

        /// <summary>
        /// Busca a lista de currículos de acordo com o curso para popular um Select
        /// </summary>
        /// <param name="seqCurso">Sequencial do curso</param>
        /// <returns>Lista de curriculos</returns>
        public List<SMCDatasourceItem> BuscarCurriculoPorCursoSelect(long seqCurso)
        {
            if (seqCurso == 0)
                return new List<SMCDatasourceItem>();

            CurriculoFilterSpecification spec = new CurriculoFilterSpecification();
            spec.SeqCurso = seqCurso;
            spec.Ativo = true;

            var lista = this.SearchProjectionBySpecification(spec, p => new SMCDatasourceItem() { Seq = p.Seq, Descricao = p.Descricao + " - " + p.DescricaoComplementar }).Distinct().ToList();

            return lista;
        }

        /// <summary>
        /// Grava um currículo e seus vínculos com ofertas
        /// </summary>
        /// <param name="curriculo">Dados do currículo a ser gravado</param>
        /// <returns>Sequencial do currículo gravado</returns>
        public long SalvarCurriculo(Curriculo curriculo)
        {
            //FIX: Remover quando o lookup hidden para edit retornar valor
            if (curriculo.SeqCurso == 0)
                curriculo.SeqCurso = SearchProjectionByKey(new SMCSeqSpecification<Curriculo>(curriculo.Seq), p => p.SeqCurso);

            // Valida se as ofertas de curso informadas são do curso vinculado ao currículo
            var seqsOferta = this.CursoOfertaDomainService.BuscarCursoOfertasAtivasSelect(curriculo.SeqCurso).Select(s => s.Seq);
            if (curriculo.CursosOferta.Any(a => !seqsOferta.Contains(a.SeqCursoOferta)))
                throw new OfertaCursoInvalidaException();


            // Preenche o sequencial no momento da criação do registro
            if (curriculo.Seq == 0)
            {
                var specCurriculoCurso = new CurriculoFilterSpecification { Codigo = curriculo.Codigo.Substring(0, curriculo.Codigo.Length - 2) };
                specCurriculoCurso.SetOrderByDescending(o => o.NumeroSequencial);
                var ultimoSequencial = this.SearchProjectionBySpecification(specCurriculoCurso, p => p.NumeroSequencial)
                    .FirstOrDefault();
                curriculo.NumeroSequencial = ultimoSequencial + 1;
            }

            this.PreencherConfiguracoes(ref curriculo);

            this.SaveEntity(curriculo);

            return curriculo.Seq;
        }

        /// <summary>
        /// Verifica se no nível de ensino informado da instituição atual o Crédio de Componente Curricular é permitido
        /// </summary>
        /// <param name="seqNivelEnsino">Sequencial do nível de ensino</param>
        /// <returns>Setado se o Crédito de Componente Curricular for permitido no nível de ensino informado na instituição atual</returns>
        private bool CreditoComponenteCurricularPermitido(long seqNivelEnsino)
        {
            var specInstituicaoNivel = new InstituicaoNivelFilterSpecification() { SeqNivelEnsino = seqNivelEnsino };
            var instituicaoNivel = this.InstituicaoNivelDomainService.SearchByKey(specInstituicaoNivel);
            return instituicaoNivel.PermiteCreditoComponenteCurricular;
        }

        private void PreencherConfiguracoes(ref Curriculo curriculo, IncludesCurso includes = IncludesCurso.Nenhum)
        {
            // Recupera configuração da instituição para o nível de ensino do curso
            var curso = this.CursoDomainService.SearchByKey(new SMCSeqSpecification<Curso>(curriculo.SeqCurso), includes);

            if (string.IsNullOrEmpty(curso.Sigla))
            {
                throw new CurriculoSiglaCursoObrigatoriaException();
            }

            curriculo.PermiteCreditoComponenteCurricular =
                this.CreditoComponenteCurricularPermitido(curso.SeqNivelEnsino);
            // Sempre atualiza os campos automáticos somente leitura
            curriculo.Curso = curso;
            curriculo.Codigo = $"{curriculo.Curso.Sigla.Trim()}{curriculo.NumeroSequencial.ToString("00")}";
            curriculo.Descricao = $"{MessagesResource.TIPO_ENTIDADE_Curriculo} {curriculo.Curso.Nome}";

            curriculo.CursosOferta.SMCForEach(oferta =>
            {
                oferta.QuantidadeHoraRelogioObrigatoria = this.ConverterHorasAulaParaHorasRelogio(oferta.QuantidadeHoraAulaObrigatoria);
                oferta.QuantidadeHoraRelogioOptativa = this.ConverterHorasAulaParaHorasRelogio(oferta.QuantidadeHoraAulaOptativa);
            });
        }

        private short? ConverterHorasAulaParaHorasRelogio(short? horasAula)
        {
            if (!horasAula.HasValue)
                return null;
            float horasRelogio = horasAula.Value * 50F / 60F;
            return Convert.ToInt16(horasRelogio);
        }

        /// <summary>
        /// Busca a lista de currículos para emissão do documento acadêmico digital em formato XML.
        /// </summary>
        /// <returns>Lista de objetos CurriculoDigitalVO contendo os seq's currículos para emissão digital.</returns>
        public List<CurriculoDigitalVO> BuscarCurriculosParaEmissaoDigital()
        {
            var spec = new CurriculoFilterSpecification { GerarDocumentoDigital = true };
            spec.SetOrderBy(o => o.Curso.Nome);
            spec.SetOrderBy(o => o.CodigoCurriculoMigracao);
            var listaCurriculo = this.SearchProjectionBySpecification(spec, s => new CurriculoDigitalVO()
            {
                SeqCurriculo = s.Seq,
                TemDocumentoAcademico = s.DocumentosAcademico.Any()
            }).ToList();

            return listaCurriculo;
        }

        public CurriculoDigitalVO BuscarDadosCurriculo(long? seq, int? codigoCurriculoMigracao)
        {
            var curriculo = this.SearchProjectionByKey(new CurriculoFilterSpecification() { Seq = seq, CodigoCurriculoMigracao = codigoCurriculoMigracao }, s => new CurriculoDigitalVO()
            {
                SeqCurriculo = s.Seq,
                TemDocumentoAcademico = s.DocumentosAcademico.Any()
            });
            return curriculo;
        }

        /// <summary>
        /// Atualiza o campo GerarDocumentoDigital conforme o sequencial do currículo
        /// </summary>
        private void GerarDocumentoDigital(long seq)
        {
            this.UpdateFields<Curriculo>(new Curriculo { Seq = seq, GerarDocumentoDigital = false }, x => x.GerarDocumentoDigital);
        }

        public void ConstruirCurriculoDigital(EmissaoCurriculoDigitalSATVO filtro)
        {
            var servico = Create<Jobs.EmissaoCurriculoDigitalWebJob>();
            servico.Execute(filtro);
        }

        public void EmitirCurriculo(CurriculoDigitalVO item)
        {
            //Determina se é a primeira via do documento acadêmico
            if (!item.TemDocumentoAcademico)
            {
                var dados = BuscarNivelEnsinoEInstituicao(item.SeqCurriculo.Value);
                if (dados.TokenNivelEnsino == TOKEN_NIVEL_ENSINO.GRADUACAO)
                {
                    var dadosOrigem = RecuperarInfoCursoOfertaLocalidade(item.SeqCurriculo.Value);

                    var documentoCurriculoGAD = GerarCurriculoGad(dados, dadosOrigem);
                    CriarDocumentoAcademicoCurriculo(item.SeqCurriculo, documentoCurriculoGAD.SeqDocumentoCurriculo, dados.SeqTipoDocumentoAcademico);

                    GerarDocumentoDigital(item.SeqCurriculo.Value);
                }
            }
        }

        private DadosCurriculoNivelEnsinoEInstituicaoVO BuscarNivelEnsinoEInstituicao(long seqCurriculo)
        {
            var curriculo = this.SearchProjectionByKey(new CurriculoFilterSpecification() { Seq = seqCurriculo }, s => new DadosCurriculoNivelEnsinoEInstituicaoVO
            {
                SeqCurriculo = s.Seq,
                SeqNivelEnsino = s.Curso.NivelEnsino.Seq,
                TokenNivelEnsino = s.Curso.NivelEnsino.Token,
                SeqInstituicaoEnsino = s.Curso.InstituicaoEnsino.Seq
            });

            curriculo.SeqInstituicaoNivel = InstituicaoNivelDomainService.SearchProjectionByKey(new InstituicaoNivelFilterSpecification
            {
                SeqInstituicaoEnsino = curriculo.SeqInstituicaoEnsino,
                SeqNivelEnsino = curriculo.SeqNivelEnsino,
            }, x => x.Seq);

            var spec = new TipoDocumentoAcademicoFilterSpecification() { Token = TOKEN_TIPO_DOCUMENTO_ACADEMICO.CURRICULO_ESCOLAR };
            var tipoDocumentoAcademico = TipoDocumentoAcademicoDomainService.SearchByKey(spec);

            if (tipoDocumentoAcademico == null)
                throw new TipoDocumentoAcademicoNaoLocalizadoException();

            curriculo.SeqTipoDocumentoAcademico = tipoDocumentoAcademico.Seq;

            return curriculo;
        }

        /// <summary>
        ///  Criação do documento acadêmico currículo -  RN_CNC_041.2
        /// </summary>
        private void CriarDocumentoAcademicoCurriculo(long? seqCurriculo, long seqDocumentoCurriculo, long seqTipoDocumentoAcademico)
        {
            var documentoAcademicoCurriculo = new DocumentoAcademicoCurriculoVO()
            {
                SeqCurriculo = seqCurriculo,
                NumeroViaDocumento = 1,
                SeqTipoDocumentoAcademico = seqTipoDocumentoAcademico,
                SeqDocumentoGAD = seqDocumentoCurriculo
            };
            var seqDocumentoAcademicoCurriculo = DocumentoAcademicoCurriculoDomainService.SalvarDocumentoAcademicoCurriculo(documentoAcademicoCurriculo);
            DocumentoAcademicoHistoricoSituacaoDomainService.InserirHistoricoAguardandoAssinatura(seqDocumentoAcademicoCurriculo);
        }

        /// <summary>
        /// Busca dados Código + Grau-Acadêmico + Data-Referência -  RN_CUR_101
        /// </summary>
        private DadosCurriculoCursoOfertaLocalidadeVO RecuperarInfoCursoOfertaLocalidade(long seqCurriculo)
        {
            var dados = this.RawQuery<DadosCurriculoCursoOfertaLocalidadeVO>(QUERY_BUSCAR_DADOS_CURSO_OFERTA_LOCALIDADE, new SMCFuncParameter("SEQ_CURRICULO", seqCurriculo));
            var retorno = dados.FirstOrDefault();

            if (retorno == null || retorno.SeqCursoOfertaLocalidade == 0 || retorno.SeqGrauAcademico == 0)
                throw new DadosCurriculoNaoLocalizadoException();

            return retorno;
        }

        public RetornoCriarCurriculoVO GerarCurriculoGad(DadosCurriculoNivelEnsinoEInstituicaoVO dados, DadosCurriculoCursoOfertaLocalidadeVO dadosOrigem)
        {
            var usuarioInclusao = UsuarioLogado.RetornaUsuarioLogado();
            var modeloCurriculo = new CriarCurriculoVO()
            {
                SeqConfiguracaoCurriculo = BuscarConfiguracaoGAD(dados.SeqInstituicaoEnsino, dados.SeqTipoDocumentoAcademico, dados.SeqInstituicaoNivel),
                Curriculum = PreencherDadosCurriculo(dados, dadosOrigem),
                UsuarioInclusao = usuarioInclusao,
            };

            //string requisicaoJson = JsonConvert.SerializeObject(modeloCurriculo, Formatting.Indented);

            var retornoGAD = APICurriculoGAD.Execute<object>("Criar", modeloCurriculo);
            var retornoCriarCurriculo = JsonConvert.DeserializeObject<RetornoCriarCurriculoVO>(retornoGAD.ToString());

            if (retornoCriarCurriculo.SeqDocumentoCurriculo == 0)
                throw new Exception(retornoCriarCurriculo.ErrorMessage);

            return retornoCriarCurriculo;
        }

        private long BuscarConfiguracaoGAD(long seqInstituicaoEnsino, long seqTipoDocumentoAcademico, long seqInstituicaoNivel)
        {
            var configuracao = InstituicaoNivelTipoDocumentoAcademicoDomainService.BuscarConfiguracaoInstituicaoNivelTipoDocumentoAcademico(seqInstituicaoEnsino,
                                                                                                                                            seqTipoDocumentoAcademico,
                                                                                                                                            seqInstituicaoNivel);

            if (!configuracao.SeqConfiguracaoGad.HasValue)
                throw new SolicitacaoDocumentoConclusaoParametrizacaoTipoDocumentoNaoEncontradaException();

            return configuracao.SeqConfiguracaoGad.Value;
        }

        private CurriculumVO PreencherDadosCurriculo(DadosCurriculoNivelEnsinoEInstituicaoVO dados, DadosCurriculoCursoOfertaLocalidadeVO dadosOrigem)
        {
            var curriculo = new CurriculumVO()
            {
                Codigo = dadosOrigem.CodigoCurriculoMigracao.Trim(),
                DataCurriculo = dadosOrigem.DataReferencia,
                MinutosRelogioDaHoraAula = 50,
                NomeParaAreas = null,
                DadosCurso = PreencherDadosCurso(dados, dadosOrigem),
                IesEmissora = PreencherDadosIesEmissora(dados, dadosOrigem),
                Etiquetas = PreencherDadosEtiquetas_Fixo(),
                EstruturaCurricular = PreencherDadosEstruturaCurricular_Fixo(),
                EstruturaAtividadeComplementar = PreencherDadosEstruturaAtividadeComplementar_Fixo(),
                CriteriosIntegralizacao = PreencherDadosCriteriosIntegralizacao_Fixo()
            };

            return curriculo;
        }

        private IesEmissoraVO PreencherDadosIesEmissora(DadosCurriculoNivelEnsinoEInstituicaoVO dados, DadosCurriculoCursoOfertaLocalidadeVO dadosOrigem)
        {
            var dadosInstituicaoEnsino = InstituicaoEnsinoDomainService.BuscarDadosInstituicaoEnsinoParaDocumentoAcademico(dados.SeqInstituicaoEnsino);

            var iesEmissora = new IesEmissoraVO()
            {
                Nome = dadosInstituicaoEnsino.Nome,
                CodigoMEC = dadosInstituicaoEnsino.CodigoMEC,
                Cnpj = dadosInstituicaoEnsino.Cnpj,
                Endereco = new EnderecoVO()
                {
                    Logradouro = dadosInstituicaoEnsino.Endereco.Logradouro,
                    Bairro = dadosInstituicaoEnsino.Endereco.Bairro,
                    Cep = dadosInstituicaoEnsino.Endereco.Cep,
                    Uf = dadosInstituicaoEnsino.Endereco.Uf,
                    NomeMunicipio = dadosInstituicaoEnsino.Endereco.NomeMunicipio,
                    CodigoMunicipio = dadosInstituicaoEnsino.Endereco.CodigoMunicipio

                },
                Mantenedora = PreencherDadosMantenedora(dadosInstituicaoEnsino.SeqMantenedora),
            };

            if (!string.IsNullOrEmpty(dadosInstituicaoEnsino.Endereco.Numero))
                iesEmissora.Endereco.Numero = dadosInstituicaoEnsino.Endereco.Numero;

            if (!string.IsNullOrEmpty(dadosInstituicaoEnsino.Endereco.Complemento))
                iesEmissora.Endereco.Complemento = dadosInstituicaoEnsino.Endereco.Complemento;

            var atoNormativos = PreencherDadosAtoNormativo(dados.SeqInstituicaoEnsino, dadosOrigem.DataReferencia);

            iesEmissora.Credenciamento = atoNormativos.Credenciamento;
            iesEmissora.Credenciamento = LimparAtoRegulatorio(iesEmissora.Credenciamento);

            iesEmissora.Recredenciamento = atoNormativos.Recredenciamento;
            iesEmissora.Recredenciamento = LimparAtoRegulatorio(iesEmissora.Recredenciamento);

            iesEmissora.RenovacaoDeRecredenciamento = atoNormativos.RenovacaoDeRecredenciamento;
            iesEmissora.RenovacaoDeRecredenciamento = LimparAtoRegulatorio(iesEmissora.RenovacaoDeRecredenciamento);

            return iesEmissora;
        }

        private MantenedoraVO PreencherDadosMantenedora(long seqMantenedora)
        {
            var dadosMantenedora = MantenedoraDomainService.BuscarMantenedoraParaDocumentoAcademico(seqMantenedora);

            var mantenedora = new MantenedoraVO()
            {
                Cnpj = dadosMantenedora.Cnpj,
                RazaoSocial = dadosMantenedora.RazaoSocial,
                Endereco = new EnderecoVO()
                {
                    Logradouro = dadosMantenedora.Endereco.Logradouro,
                    Bairro = dadosMantenedora.Endereco.Bairro,
                    Cep = dadosMantenedora.Endereco.Cep,
                    Uf = dadosMantenedora.Endereco.Uf,
                    NomeMunicipio = dadosMantenedora.Endereco.NomeMunicipio,
                    CodigoMunicipio = dadosMantenedora.Endereco.CodigoMunicipio
                }
            };

            if (!string.IsNullOrEmpty(dadosMantenedora.Endereco.Numero))
                mantenedora.Endereco.Numero = dadosMantenedora.Endereco.Numero;

            if (!string.IsNullOrEmpty(dadosMantenedora.Endereco.Complemento))
                mantenedora.Endereco.Complemento = dadosMantenedora.Endereco.Complemento;

            return mantenedora;
        }

        private (AtoRegulatorioVO Credenciamento, AtoRegulatorioVO Recredenciamento, AtoRegulatorioVO RenovacaoDeRecredenciamento) PreencherDadosAtoNormativo(long seqInstituicaoEnsino, DateTime dataReferencia)
        {
            var atosNormativosInstituicao = AtoNormativoEntidadeDomainService.BuscarAtoNormativoEntidadeInstituicao(seqInstituicaoEnsino, dataReferencia);

            var credenciamento = MapearAtoNormativo(atosNormativosInstituicao.Credenciamento);
            var recredenciamento = MapearAtoNormativo(atosNormativosInstituicao.Recredenciamento);
            var renovacaoDeRecredenciamento = MapearAtoNormativo(atosNormativosInstituicao.RenovacaoDeRecredenciamento);

            return (credenciamento, recredenciamento, renovacaoDeRecredenciamento);
        }

        private AtoRegulatorioVO MapearAtoNormativo(DadosAtoNormativoVO origem)
        {
            if (origem.Numero == null)
                return new AtoRegulatorioVO();

            var atoNormativo = new AtoRegulatorioVO
            {
                Numero = origem.Numero,
                Data = origem.Data,
                Tipo = origem.Tipo,
            };

            if (!string.IsNullOrEmpty(origem.VeiculoPublicacao))
                atoNormativo.VeiculoPublicacao = origem.VeiculoPublicacao;

            if (origem.DataPublicacao.HasValue)
                atoNormativo.DataPublicacao = origem.DataPublicacao.Value.Date;

            if (origem.SecaoPublicacao.HasValue)
                atoNormativo.SecaoPublicacao = origem.SecaoPublicacao;

            if (origem.PaginaPublicacao.HasValue)
                atoNormativo.PaginaPublicacao = origem.PaginaPublicacao;

            if (origem.NumeroDOU.HasValue)
                atoNormativo.NumeroDOU = origem.NumeroDOU;

            return atoNormativo;
        }

        private DadosMinimosCursoVO PreencherDadosCurso(DadosCurriculoNivelEnsinoEInstituicaoVO dados, DadosCurriculoCursoOfertaLocalidadeVO dadosOrigem)
        {
            var curso = CursoOfertaLocalidadeDomainService.BuscarIdentificacaoEmecComEnderecoParaDocumentoAcademico(dadosOrigem.SeqCursoOfertaLocalidade);

            var dadosCurso = new DadosMinimosCursoVO()
            {
                NomeCurso = curso.NomeCursoCurriculo,
                CodigoCursoEMEC = curso.CodigoOrgaoRegulador,
                Habilitacoes = null,
            };

            var atoNormativos = PreencherDadosAtoNormativoCurso(dadosOrigem.SeqCursoOfertaLocalidade, dadosOrigem.SeqGrauAcademico, dadosOrigem.DataReferencia);

            dadosCurso.Autorizacao = atoNormativos.Autorizacao;
            dadosCurso.Autorizacao = LimparAtoRegulatorio(dadosCurso.Autorizacao);

            dadosCurso.Reconhecimento = atoNormativos.Reconhecimento;
            dadosCurso.Reconhecimento = LimparAtoRegulatorio(dadosCurso.Reconhecimento);

            dadosCurso.RenovacaoReconhecimento = atoNormativos.RenovacaoReconhecimento;
            dadosCurso.RenovacaoReconhecimento = LimparAtoRegulatorio(dadosCurso.RenovacaoReconhecimento);

            return dadosCurso;
        }

        private (AtoRegulatorioVO Autorizacao, AtoRegulatorioVO Reconhecimento, AtoRegulatorioVO RenovacaoReconhecimento) PreencherDadosAtoNormativoCurso(long seqCursoOfertaLocalidade, long seqGrauAcademico, DateTime dataReferencia)
        {
            var atosNormativosCurso = SolicitacaoDocumentoConclusaoDomainService.BuscarAtosNormativosCurso(seqCursoOfertaLocalidade, seqGrauAcademico, dataReferencia, true);

            var autorizacao = MapearAtoNormativo(atosNormativosCurso.Autorizacao);
            var reconhecimento = MapearAtoNormativo(atosNormativosCurso.Reconhecimento);
            var renovacaoReconhecimento = MapearAtoNormativo(atosNormativosCurso.RenovacaoReconhecimento);

            return (autorizacao, reconhecimento, renovacaoReconhecimento);
        }

        private AtoRegulatorioVO LimparAtoRegulatorio(AtoRegulatorioVO ato)
        {
            if (ato == null)
                return null;

            bool tipoENumeroVazios = string.IsNullOrEmpty(ato.Tipo) && string.IsNullOrEmpty(ato.Numero);
            if (tipoENumeroVazios)
                return null;

            if (ato.InformacoesTramitacaoEmec != null && string.IsNullOrEmpty(ato.InformacoesTramitacaoEmec.TipoProcesso))
                ato.InformacoesTramitacaoEmec = null;

            return ato;
        }

        private List<DadosEtiquetaVO> PreencherDadosEtiquetas_Fixo()
        {
            var retorno = new List<DadosEtiquetaVO>();

            var etiquetaUm = new DadosEtiquetaVO()
            {
                Codigo = "FAKE - Extracurricular",
                Nome = "FAKE - Extracurricular",
                AplicadoAutomaticamenteUnidadesNaoPertencentesAoCurriculo = false
            };
            retorno.Add(etiquetaUm);
            var etiquetaDois = new DadosEtiquetaVO()
            {
                Codigo = "FAKE - Base: Bacharel - Obrigatória",
                Nome = "FAKE - Base: Bacharel - Obrigatória",
                AplicadoAutomaticamenteUnidadesNaoPertencentesAoCurriculo = false
            };
            retorno.Add(etiquetaDois);

            return retorno;
        }

        private EstruturaCurricularVO PreencherDadosEstruturaCurricular_Fixo()
        {
            var retorno = new EstruturaCurricularVO()
            {
                UnidadesCurriculares = new List<UnidadeCurricularVO>()
                {
                       new UnidadeCurricularVO()
                       {
                           Tipo ="Disciplina",
                           Codigo ="48802",
                           Nome="FAKE - ANÁLISE COMPORTAMENTAL APLICADA",
                           CargaHorariaEmHoraAula=60,
                           CargaHorariaEmHoraRelogio=50.0,
                           Ementa = new EmentaVO()
                           {
                               Itens = new List<ItemEmentaVO>()
                               {
                                   new ItemEmentaVO()
                                   {
                                       Descricao = "FAKE - O comportamento verbal e o controle do comportamento humano: características do comportamento controlado por regras; o processo de análise funcional."
                                   }
                               }
                           },
                           Fase = "FAKE - 1° Período",
                           Equivalencias = new List<UnidadeCurricularEquivalenteVO>()
                           {
                               new UnidadeCurricularEquivalenteVO()
                               {
                                   CodigosUnidadesEquivalentes = new List<string>(){ "FAKE-Codigo1" }
                               }
                           },
                           PreRequisitos = new List<string>(){ "FAKE-PreRequisitos1" },
                           Areas = new List<string>(){ "FAKE - Areas" },
                           Etiquetas = new List<EtiquetaVO>()
                           {
                               new EtiquetaVO()
                               {
                                   Codigo = "FAKE - Base: Bacharel - Obrigatória",
                                   CargaHorariaEmHoraAula = 60,
                                   CargaHorariaEmHoraRelogio = 50.0
                               }
                           }
                       }
                }
            };

            return retorno;
        }

        private EstruturaAtividadeComplementarVO PreencherDadosEstruturaAtividadeComplementar_Fixo()
        {
            var retorno = new EstruturaAtividadeComplementarVO()
            {
                Categorias = new List<CategoriaAtividadeComplementarVO>()
                {
                    new CategoriaAtividadeComplementarVO()
                    {
                        Codigo = "FAKE - ACG",
                        Nome = "FAKE - Atividade Complementar",
                        LimiteCargaHorariaEmHoraRelogio = 30.0,
                        Atividades = new List<AtividadeComplementarCurriculoVO>()
                        {
                            new AtividadeComplementarCurriculoVO()
                            {
                                Codigo = "FAKE - ACG",
                                Nome = "FAKE - Atividade Complementar",
                                Descricao = "FAKE - Atividade Complementar",
                                LimiteCargaHorariaEmHoraRelogio = 30.0
                            }
                        }
                    }
                }
            };

            return retorno;
        }

        private List<CriterioIntegralizacaoVO> PreencherDadosCriteriosIntegralizacao_Fixo()
        {
            var retorno = new List<CriterioIntegralizacaoVO>() { };

            var criterioIntegralizacao = new CriterioIntegralizacaoVO()
            {
                Tipo = "Expressao",
                Expressao = new ExpressaoIntegralizacaoVO()
                {
                    Codigo = "FAKE - Code",
                    LimitesCargaHoraria = new LimitesCargaHorariaVO()
                    {
                        CargaHorariaMinima = 0.0,
                        CargaHorariaMaxima = 0.0,
                        CargaHorariaParaTotal = 0.0
                    },
                    SomatorioCodigos = new List<string>() { "FAKE - SomatorioCodigos" },
                },
                Rotulos = new RotulosIntegralizacaoVO()
                {
                    TipoUnidadeCurricular = "Disciplina",
                    Etiquetas = new List<string>() { "FAKE - Etiqueta" },
                    Codigo = "FAKE - Codigo",
                    LimitesCargaHoraria = new LimitesCargaHorariaVO()
                    {
                        CargaHorariaMinima = 0.0,
                        CargaHorariaMaxima = 0.0,
                        CargaHorariaParaTotal = 0.0
                    }
                }
            };
            retorno.Add(criterioIntegralizacao);

            return retorno;
        }
    }
}