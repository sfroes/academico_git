using SMC.Academico.Common.Areas.CUR.Enums;
using SMC.Academico.Common.Areas.CUR.Exceptions;
using SMC.Academico.Common.Areas.CUR.Includes;
using SMC.Academico.Common.Areas.ORG.Enums;
using SMC.Academico.Common.Areas.PES.Enums;
using SMC.Academico.Common.Constants;
using SMC.Academico.Domain.Areas.ALN.DomainServices;
using SMC.Academico.Domain.Areas.ALN.Specifications;
using SMC.Academico.Domain.Areas.ALN.ValueObjects;
using SMC.Academico.Domain.Areas.CAM.DomainServices;
using SMC.Academico.Domain.Areas.CAM.Models;
using SMC.Academico.Domain.Areas.CAM.Specifications;
using SMC.Academico.Domain.Areas.CSO.DomainServices;
using SMC.Academico.Domain.Areas.CSO.Models;
using SMC.Academico.Domain.Areas.CSO.Specifications;
using SMC.Academico.Domain.Areas.CUR.Models;
using SMC.Academico.Domain.Areas.CUR.Resources;
using SMC.Academico.Domain.Areas.CUR.Specifications;
using SMC.Academico.Domain.Areas.CUR.ValueObjects;
using SMC.Academico.Domain.Areas.MAT.DomainServices;
using SMC.Academico.Domain.Areas.MAT.ValueObjects;
using SMC.Academico.Domain.Areas.ORG.DomainServices;
using SMC.Academico.Domain.Areas.ORG.Models;
using SMC.Academico.Domain.Areas.ORG.Specifications;
using SMC.Academico.Domain.Areas.PES.DomainServices;
using SMC.Academico.Domain.Areas.PES.ValueObjects;
using SMC.Academico.Domain.Areas.TUR.DomainServices;
using SMC.Academico.Domain.Areas.TUR.ValueObjects;
using SMC.Academico.Domain.Helpers;
using SMC.Framework;
using SMC.Framework.Domain;
using SMC.Framework.Extensions;
using SMC.Framework.Model;
using SMC.Framework.Repository;
using SMC.Framework.Specification;
using SMC.Framework.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SMC.Academico.Domain.Areas.CUR.DomainServices
{
    public class ConfiguracaoComponenteDomainService : AcademicoContextDomain<ConfiguracaoComponente>
    {
        #region [ DomainService ]

        private ComponenteCurricularDomainService ComponenteCurricularDomainService => Create<ComponenteCurricularDomainService>();
        private DivisaoComponenteDomainService DivisaoComponenteDomainService => Create<DivisaoComponenteDomainService>();
        private DivisaoMatrizCurricularComponenteDomainService DivisaoMatrizCurricularComponenteDomainService => Create<DivisaoMatrizCurricularComponenteDomainService>();
        private EntidadeDomainService EntidadeDomainService => Create<EntidadeDomainService>();
        private GrupoCurricularComponenteDomainService GrupoCurricularComponenteDomainService => Create<GrupoCurricularComponenteDomainService>();
        private HierarquiaEntidadeDomainService HierarquiaEntidadeDomainService => Create<HierarquiaEntidadeDomainService>();
        private HierarquiaEntidadeItemDomainService HierarquiaEntidadeItemDomainService => Create<HierarquiaEntidadeItemDomainService>();
        private InstituicaoNivelDomainService InstituicaoNivelDomainService => Create<InstituicaoNivelDomainService>();
        private InstituicaoNivelTipoComponenteCurricularDomainService InstituicaoNivelTipoComponenteCurricularDomainService => Create<InstituicaoNivelTipoComponenteCurricularDomainService>();
        private InstituicaoNivelTipoTermoIntercambioDomainService InstituicaoNivelTipoTermoIntercambioDomainService => Create<InstituicaoNivelTipoTermoIntercambioDomainService>();
        private InstituicaoNivelTipoVinculoAlunoDomainService InstituicaoNivelTipoVinculoAlunoDomainService => Create<InstituicaoNivelTipoVinculoAlunoDomainService>();
        private MatrizCurricularOfertaDomainService MatrizCurricularOfertaDomainService => Create<MatrizCurricularOfertaDomainService>();
        private PessoaAtuacaoDomainService PessoaAtuacaoDomainService => Create<PessoaAtuacaoDomainService>();
        private RequisitoDomainService RequisitoDomainService => Create<RequisitoDomainService>();
        private RestricaoTurmaMatrizDomainService RestricaoTurmaMatrizDomainService => Create<RestricaoTurmaMatrizDomainService>();
        private SolicitacaoMatriculaItemDomainService SolicitacaoMatriculaItemDomainService => Create<SolicitacaoMatriculaItemDomainService>();
        private TipoDivisaoComponenteDomainService TipoDivisaoComponenteDomainService => Create<TipoDivisaoComponenteDomainService>();
        private CursoOfertaLocalidadeTurnoDomainService CursoOfertaLocalidadeTurnoDomainService => Create<CursoOfertaLocalidadeTurnoDomainService>();
        private CursoOfertaLocalidadeDomainService CursoOfertaLocalidadeDomainService => Create<CursoOfertaLocalidadeDomainService>();
        private MatrizCurricularDomainService MatrizCurricularDomainService => Create<MatrizCurricularDomainService>();
        private ConfiguracaoEventoLetivoDomainService ConfiguracaoEventoLetivoDomainService => Create<ConfiguracaoEventoLetivoDomainService>();
        private InstituicaoNivelTipoDivisaoComponenteDomainService InstituicaoNivelTipoDivisaoComponenteDomainService => Create<InstituicaoNivelTipoDivisaoComponenteDomainService>();
        private TurmaConfiguracaoComponenteDomainService TurmaConfiguracaoComponenteDomainService => Create<TurmaConfiguracaoComponenteDomainService>();
        #endregion [ DomainService ]

        #region [ Querys ]

        private const string BUSCAR_SEQ_ENTIDADE_SUPERIOR = "select seq_entidade from ORG.fn_buscar_entidade_superior_hierarquia(@seq_grupo_programa, @tipo_visao, @seq_tipo_entidade_destino)";
        private const string BUSCAR_SIGLA_ENTIDADE_SUPERIOR = "select sgl_entidade from ORG.fn_buscar_entidade_superior_hierarquia(@seq_grupo_programa, @tipo_visao, @seq_tipo_entidade_destino)";

        #endregion [ Querys ]

        /// <summary>
        /// Verifica se exige assunto para a configuração de componente
        /// </summary>
        /// <param name="seq">Sequencial da configuração do componente</param>
        /// <returns>Valor do campo exige assunto</returns>
        public bool VerificaConfiguracaoComponenteExigeAssunto(long seq)
        {
            var configuracao = this.SearchProjectionByKey(new SMCSeqSpecification<ConfiguracaoComponente>(seq), p => p.ComponenteCurricular.ExigeAssuntoComponente);

            return configuracao == true;
        }

        /// <summary>
        /// Buscar os dados de um componente curricular
        /// </summary>
        /// <param name="seq">Sequencial da configuração do componente</param>
        /// <returns>Registro do componente curricular</returns>
        public ConfiguracaoComponenteVO BuscarConfiguracaoComponente(long seq)
        {
            FilterHelper.DesativarFiltros(this);
            var spec = new SMCSeqSpecification<ConfiguracaoComponente>(seq);
            var includes = IncludesConfiguracaoComponente.ComponenteCurricular_NiveisEnsino_NivelEnsino
                         | IncludesConfiguracaoComponente.ComponenteCurricular_EntidadesResponsaveis_Entidade
                         | IncludesConfiguracaoComponente.DivisoesComponente_TipoDivisaoComponente;
            var configuracao = this.SearchByKey(spec, includes);

            var turmaConfiguracaoComponente = TurmaConfiguracaoComponenteDomainService.BuscarConfiguracaoPrincipalTurma(0, configuracao.Seq);

            var configuracaoVo = configuracao.Transform<ConfiguracaoComponenteVO>();
            if(turmaConfiguracaoComponente != null)
                configuracaoVo.Principal = turmaConfiguracaoComponente.Principal;

            // Propriedades para descrição da Configuracao do Componente da Turma
            configuracaoVo.ComponenteCurricularCargaHoraria = configuracao?.ComponenteCurricular?.CargaHoraria;
            configuracaoVo.ComponenteCurricularCredito = configuracao?.ComponenteCurricular?.Credito;

            configuracaoVo.SeqNivelEnsino = configuracao.ComponenteCurricular.NiveisEnsino.Where(s => s.Responsavel == true).Select(s => s.SeqNivelEnsino).FirstOrDefault();
            configuracaoVo.FormatoCargaHoraria = BuscarFormatoCargaHoraria(configuracaoVo.SeqNivelEnsino, configuracao.ComponenteCurricular.SeqTipoComponenteCurricular);
            configuracaoVo.ComponenteCurricularEntidadesSigla = configuracao.ComponenteCurricular.EntidadesResponsaveis.Select(s => s.Entidade.Sigla);

            configuracaoVo.DivisoesComponente = configuracaoVo.DivisoesComponente.OrderBy(o => o.Numero).ToList();
            configuracaoVo.ExibirItensOrganizacao = ComponenteCurricularDomainService.SearchByKey<ComponenteCurricular, ComponenteCurricularVO>(configuracaoVo.SeqComponenteCurricular, IncludesComponenteCurricular.Organizacoes).Organizacoes.Count > 0;

            foreach (var divisaoVo in configuracaoVo.DivisoesComponente)
            {
                var tipoDivisao = configuracao.DivisoesComponente.Single(s => s.Seq == divisaoVo.Seq).TipoDivisaoComponente;

                divisaoVo.PermiteGrupoSomenteLeitura = tipoDivisao.TipoGestaoDivisaoComponente != TipoGestaoDivisaoComponente.Turma;
                divisaoVo.ExibirArtigo = tipoDivisao.Artigo ?? false;
                //divisaoVo.PossuiTipoOrganizacao = divisaoVo.Organizacoes.SMCCount() > 0;
                divisaoVo.PermiteCargaHorariaGrade = InstituicaoNivelTipoDivisaoComponenteDomainService.VerificarPermissaoCargaHorariaGrade(divisaoVo.SeqTipoDivisaoComponente, configuracaoVo.SeqComponenteCurricular).PermiteCargaHorariaGrade;
                divisaoVo.QuantidadeSemanasComponentePreenchida = configuracao.ComponenteCurricular.QuantidadeSemanas.HasValue && configuracao.ComponenteCurricular.QuantidadeSemanas > 0;
            }
            FilterHelper.AtivarFiltros(this);
            return configuracaoVo;
        }

        /// <summary>
        /// Buscar a descrição de uma configuração do componente, conforme a regra
        ///
        /// [Código da configuração] + "-" + [Descrição] + "-" + [Descrição complementar] + "-" + [Carga horária do componente curricular] 
        /// + [label parametrizado*] + "-" + [Créditos do componente curricular] + "Créditos" + "-"  
        /// + [Lista de siglas das entidades responsáveis do componente separadas por "/", ordenadas alfabeticamente]. 
        /// 
        /// </summary>
        /// <param name="seq">Sequencial da configuração do componente</param>
        /// <returns>descrição de uma configuração do componente</returns>
        public string BuscarDescricaoConfiguracaoComponente(long seq)
        {
            FilterHelper.DesativarFiltros(this);
            var spec = new SMCSeqSpecification<ConfiguracaoComponente>(seq);
            var includes = IncludesConfiguracaoComponente.ComponenteCurricular_NiveisEnsino_NivelEnsino
                         | IncludesConfiguracaoComponente.ComponenteCurricular_EntidadesResponsaveis_Entidade;
            var configuracao = this.SearchByKey(spec, includes);

            var configuracaoVo = configuracao.Transform<ConfiguracaoComponenteVO>();

            // Propriedades para descrição da Configuracao do Componente da Turma
            configuracaoVo.ComponenteCurricularCargaHoraria = configuracao?.ComponenteCurricular?.CargaHoraria;
            configuracaoVo.ComponenteCurricularCredito = configuracao?.ComponenteCurricular?.Credito;

            configuracaoVo.SeqNivelEnsino = configuracao.ComponenteCurricular.NiveisEnsino.Where(s => s.Responsavel == true).Select(s => s.SeqNivelEnsino).FirstOrDefault();
            configuracaoVo.FormatoCargaHoraria = BuscarFormatoCargaHoraria(configuracaoVo.SeqNivelEnsino, configuracao.ComponenteCurricular.SeqTipoComponenteCurricular);
            configuracaoVo.ComponenteCurricularEntidadesSigla = configuracao.ComponenteCurricular.EntidadesResponsaveis.Select(s => s.Entidade.Sigla);

            FilterHelper.AtivarFiltros(this);
            return configuracaoVo.ConfiguracaoComponenteDescricaoCompleta;
        }

        public string GerarDescricaoConfiguracaoComponenteTurma(long seqConfiguracaoComponente, string descricaoComponenteCurricularAssunto, List<string> siglasEntidadesResponsavelOfertasMatriz)
        {
            FilterHelper.DesativarFiltros(this);

            var spec = new SMCSeqSpecification<ConfiguracaoComponente>(seqConfiguracaoComponente);
            var includes = IncludesConfiguracaoComponente.ComponenteCurricular_NiveisEnsino_NivelEnsino;
            var configuracao = this.SearchByKey(spec, includes);

            var configuracaoVo = configuracao.Transform<ConfiguracaoComponenteVO>();

            // Propriedades para descrição da Configuracao do Componente da Turma
            configuracaoVo.ComponenteCurricularCargaHoraria = configuracao?.ComponenteCurricular?.CargaHoraria;
            configuracaoVo.ComponenteCurricularCredito = configuracao?.ComponenteCurricular?.Credito;
            configuracaoVo.DescricaoAssuntoComponente = descricaoComponenteCurricularAssunto;
            configuracaoVo.SeqNivelEnsino = configuracao.ComponenteCurricular.NiveisEnsino.Where(s => s.Responsavel == true).Select(s => s.SeqNivelEnsino).FirstOrDefault();
            configuracaoVo.FormatoCargaHoraria = BuscarFormatoCargaHoraria(configuracaoVo.SeqNivelEnsino, configuracao.ComponenteCurricular.SeqTipoComponenteCurricular);
            configuracaoVo.EntidadeResponsavelOfertaMatrizSigla = siglasEntidadesResponsavelOfertasMatriz;

            FilterHelper.AtivarFiltros(this);

            return FormatarDescricaoConfiguracaoTurma(configuracaoVo);
        }

        /// <summary>
        /// Buscar as Siglas da entidade responsável pelas ofertas de matriz associadas a turma
        /// </summary>
        /// <param name="seq">Sequencial da configuração do componente</param>
        /// <param name="seqsCursoOfertaLocalidadeTurno">Sequencial dos CursoOfertaLocalidadeTurno, das ofertas de matriz da turma</param>
        /// <returns>Siglas das entidades responsáveis, por cada curso, das ofertas de matriz da turma</returns>
        public List<string> BuscarSiglasEntidadeResponsavelOfertasMatrizTurma(TurmaVO turma)
        {
            FilterHelper.DesativarFiltros(this);

            var seqsCursoOfertaLocalidadeTurno = BuscarMatrizCurricularOfertasSeqsCursoOfertaLocalidadeTurno(turma);

            /// Sigla da entidade responsável pelas ofertas de matriz associadas a turma
            /// Para buscar a entidade responsável pela oferta de matriz é necessário:
            ///     1.Buscar o tipo de entidade parametrizada para o tipo de componente e o nível de ensino responsável pelo componente 
            /// referente à configuração principal da turma;
            var siglas = new List<string>();

            if (turma.ConfiguracaoComponente == 0 || !seqsCursoOfertaLocalidadeTurno.SMCAny()) { return siglas; }

            var spec = new SMCSeqSpecification<ConfiguracaoComponente>(turma.ConfiguracaoComponente);
            var includes = IncludesConfiguracaoComponente.ComponenteCurricular_NiveisEnsino_NivelEnsino;
            var configuracao = this.SearchByKey(spec, includes);

            var configuracaoVo = configuracao.Transform<ConfiguracaoComponenteVO>();
            configuracaoVo.SeqNivelEnsino = configuracao.ComponenteCurricular.NiveisEnsino.Where(s => s.Responsavel == true).Select(s => s.SeqNivelEnsino).FirstOrDefault();

            var parametros = InstituicaoNivelTipoComponenteCurricularDomainService.BuscarInstituicaoNivelTipoComponenteCurricularConfiguracao(configuracaoVo.SeqNivelEnsino, configuracao.ComponenteCurricular.SeqTipoComponenteCurricular);

            ///     2.Buscar na hierarquia de entidade qual a entidade responsável pela oferta de curso por localidade e turno de 
            /// todas as ofertas de matriz associadas à turma Esta busca deve retornar uma entidade do tipo de entidade encontrado no item 1;
            foreach (var seqCursoOfertaLocalidadeTurno in seqsCursoOfertaLocalidadeTurno.Distinct().ToList())
            {
                var seqEntidadeCurso = CursoOfertaLocalidadeTurnoDomainService.SearchProjectionByKey(new SMCSeqSpecification<CursoOfertaLocalidadeTurno>(seqCursoOfertaLocalidadeTurno),
                     x => x.CursoOfertaLocalidade.CursoOferta.Curso.Seq);

                if (seqEntidadeCurso != 0)
                {
                    var siglaEntidadeSuperior = BuscarSiglaEntidadeSuperior(seqEntidadeCurso, parametros.TipoEntidadeResponsavel.Value);
                    if (!string.IsNullOrEmpty(siglaEntidadeSuperior) && !siglas.Any(x => x == siglaEntidadeSuperior))
                    {
                        siglas.Add(siglaEntidadeSuperior);
                    }
                }
            }

            ///     3.Concatenar as siglas das entidades encontradas no item 2, sem repetições. Caso exista mais de uma entidade 
            /// responsável, concatenar a sigla de todas elas, separando por "/", em ordem alfabética.
            FilterHelper.AtivarFiltros(this);
            return siglas.OrderBy(o => o).ToList();
        }

        /// <summary>
        /// Busca os Seqs do CursoOfertaLocalidadeTurno, das ofertas de matriz da turma
        /// </summary>
        /// <param name="turma"></param>
        /// <returns></returns>
        private List<long> BuscarMatrizCurricularOfertasSeqsCursoOfertaLocalidadeTurno(TurmaVO turma)
        {
            var seqsCursoOfertaLocalidadeTurno = new List<long>();

            foreach (var oferta in turma.TurmaOfertasMatriz.SelectMany(x => x.OfertasMatriz).ToList())
            {
                if (oferta.SeqCursoOfertaLocalidadeTurno == 0)
                {
                    oferta.SeqCursoOfertaLocalidadeTurno = MatrizCurricularOfertaDomainService.BuscarMatrizCurricularOfertaSeqCursoOfertaLocalidadeTurno(oferta.SeqMatrizCurricularOferta.Value, true);
                }
                seqsCursoOfertaLocalidadeTurno.Add(oferta.SeqCursoOfertaLocalidadeTurno);
            }

            return seqsCursoOfertaLocalidadeTurno;
        }

        #region [ Buscar dados da entidade superior ]

        /// <summary>
        /// Buscar a Sigla do pai da entidade de um determinado tipo
        /// </summary>
        /// <param name="seqEntidadeOrigem"></param>
        /// <param name="seqTipoEntidadeDestino"></param>
        /// <param name="tipoVisao"></param>
        /// <returns>Seq da entidade superior</returns>
        public string BuscarSiglaEntidadeSuperior(long seqEntidadeOrigem, long seqTipoEntidadeDestino, TipoVisao tipoVisao = TipoVisao.VisaoOrganizacional)
        {
            var siglaEntidadeSuperior = this.RawQuery<string>(BUSCAR_SIGLA_ENTIDADE_SUPERIOR,
                                                  new SMCFuncParameter("seq_grupo_programa"/*SEQ_ENTIDADE_ORIGEM*/, seqEntidadeOrigem),
                                                  new SMCFuncParameter("tipo_visao", tipoVisao),
                                                  new SMCFuncParameter("seq_tipo_entidade_destino", seqTipoEntidadeDestino)).FirstOrDefault();

            return siglaEntidadeSuperior;
        }

        /// <summary>
        /// Buscar o Seq do pai da entidade de um determinado tipo
        /// </summary>
        /// <param name="seqEntidadeOrigem"></param>
        /// <param name="seqTipoEntidadeDestino"></param>
        /// <param name="tipoVisao"></param>
        /// <returns>Seq da entidade superior</returns>
        public long BuscarSeqEntidadeSuperior(long seqEntidadeOrigem, long seqTipoEntidadeDestino, TipoVisao tipoVisao = TipoVisao.VisaoOrganizacional)
        {
            var seqEntidadeSuperior = BuscarSeqsEntidadesSuperiores(seqEntidadeOrigem, seqTipoEntidadeDestino, tipoVisao).FirstOrDefault();
            return seqEntidadeSuperior;
        }

        /// <summary>
        /// Buscar os Seqs dos pais da entidade de um determinado tipo
        /// </summary>
        /// <param name="seqEntidadeOrigem"></param>
        /// <param name="seqTipoEntidadeDestino"></param>
        /// <param name="tipoVisao"></param>
        /// <returns>Seqs das entidades superiores</returns>
        public long[] BuscarSeqsEntidadesSuperiores(long seqEntidadeOrigem, long seqTipoEntidadeDestino, TipoVisao tipoVisao = TipoVisao.VisaoOrganizacional)
        {
            var seqsEntidadesSuperiores = this.RawQuery<long>(BUSCAR_SEQ_ENTIDADE_SUPERIOR,
                                                  new SMCFuncParameter("seq_grupo_programa"/*SEQ_ENTIDADE_ORIGEM*/, seqEntidadeOrigem),
                                                  new SMCFuncParameter("tipo_visao", tipoVisao),
                                                  new SMCFuncParameter("seq_tipo_entidade_destino", seqTipoEntidadeDestino)).ToArray();

            return seqsEntidadesSuperiores;
        }

        #endregion [ Buscar dados da entidade superior ]

        public FormatoCargaHoraria? BuscarFormatoCargaHoraria(long seqNivelEnsino, long seqTipoComponenteCurricular)
        {
            return InstituicaoNivelTipoComponenteCurricularDomainService.SearchProjectionByKey(new InstituicaoNivelTipoComponenteCurricularFilterSpecification()
            {
                SeqNivelEnsino = seqNivelEnsino,
                SeqTipoComponenteCurricular = seqTipoComponenteCurricular
            }, i => i.FormatoCargaHoraria);
        }

        /// <summary>
        /// Buscar os dados de uma configuração do componente curricular somente com instituicao nivel
        /// </summary>
        /// <param name="seq">Sequencial da configuração do componente curricular</param>
        /// <returns>Seqeuncia da instiuição nível da configuração do componente</returns>
        public long BuscarConfiguracaoComponenteInstituicaoNivel(long seq)
        {
            var spec = new SMCSeqSpecification<ConfiguracaoComponente>(seq);
            var includes = IncludesConfiguracaoComponente.ComponenteCurricular_NiveisEnsino_NivelEnsino;
            var configuracao = this.SearchByKey(spec, includes);

            long seqNivelEnsino = configuracao.ComponenteCurricular.NiveisEnsino.Where(w => w.Responsavel == true).First().SeqNivelEnsino;
            var configuracaoNivel = InstituicaoNivelDomainService.SearchByKey(new InstituicaoNivelFilterSpecification() { SeqNivelEnsino = seqNivelEnsino });

            return configuracaoNivel.Seq;
        }

        /// <summary>
        /// Buscar os dados de uma configuração do componente curricular somente com instituicao nivel
        /// </summary>
        /// <param name="seq">Sequencial da configuração do componente curricular</param>
        /// <returns>Seqeuncia da nível ensino da configuração do componente</returns>
        public long BuscarConfiguracaoComponenteNivelEnsino(long seq)
        {
            var spec = new SMCSeqSpecification<ConfiguracaoComponente>(seq);
            var includes = IncludesConfiguracaoComponente.ComponenteCurricular_NiveisEnsino_NivelEnsino;
            var configuracao = this.SearchByKey(spec, includes);

            long seqNivelEnsino = configuracao.ComponenteCurricular.NiveisEnsino.Where(w => w.Responsavel == true).First().SeqNivelEnsino;

            return seqNivelEnsino;
        }

        /// <summary>
        /// Buscar os dados de uma configuração do componente curricular somente com instituicao nivel
        /// </summary>
        /// <param name="seq">Sequencial da configuração do componente curricular</param>
        /// <returns>Lista de seqeuncia da nível ensino da configuração do componente</returns>
        public List<long> BuscarConfiguracaoComponenteNiveisEnsino(long seq)
        {
            var spec = new SMCSeqSpecification<ConfiguracaoComponente>(seq);
            var includes = IncludesConfiguracaoComponente.ComponenteCurricular_NiveisEnsino_NivelEnsino;
            var configuracao = this.SearchByKey(spec, includes);

            List<long> seqsNivelEnsino = configuracao.ComponenteCurricular.NiveisEnsino.Select(s => s.SeqNivelEnsino).ToList();

            return seqsNivelEnsino;
        }

        /// <summary>
        /// Buscar os dados de configuração inicial de acordo com o componente curricular
        /// </summary>
        /// <param name="seqComponenteCurricular">Sequencial do componente curricular</param>
        /// <returns>Configuração inicial do componente curricular</returns>
        public ConfiguracaoComponenteVO BuscarConfiguracaoComponenteCurricular(long seqComponenteCurricular)
        {
            ConfiguracaoComponenteVO configuracao = new ConfiguracaoComponenteVO();

            var inicioConfiguracao = this.SearchBySpecification(new ConfiguracaoComponenteFilterSpecification() { SeqComponenteCurricular = seqComponenteCurricular }).OrderByDescending(o => o.Seq).ToList();
            var componente = ComponenteCurricularDomainService.SearchByKey<ComponenteCurricular, ComponenteCurricularVO>(seqComponenteCurricular, IncludesComponenteCurricular.TipoComponente);

            if (inicioConfiguracao.Count > 0)
            {
                var ultimoCodigo = inicioConfiguracao[0].Codigo.Split('.');
                var novoCodigo = Convert.ToInt64(ultimoCodigo[1]) + 1;
                configuracao.Codigo = $"{componente.Codigo}.{novoCodigo}";
            }
            else
            {
                configuracao.Codigo = $"{componente.Codigo}.1";
            }

            configuracao.SeqComponenteCurricular = componente.Seq;
            configuracao.Descricao = $"{componente.Descricao} - {MessagesResource.Label_Modalidade}";
            configuracao.ExibirItensOrganizacao = ComponenteCurricularDomainService.SearchByKey<ComponenteCurricular, ComponenteCurricularVO>(seqComponenteCurricular, IncludesComponenteCurricular.Organizacoes).Organizacoes.Count > 0;
            configuracao.Ativo = true;

            return configuracao;
        }

        /// <summary>
        /// Buscar as configurações de componente, necessario para preencher o combo de entidades descrição na listagem
        /// </summary>
        /// <param name="filtros">Filtros definidos para a configuração de componente</param>
        /// <param name="desabilitaFiltroDados">Desabilita o filtro de dados de hierarquia_entidade_organizacional para atender o caso de entidades compartilhadas da tela de consulta dados do aluno </param>
        /// <returns>SMCPagerData da lista de configuração de componente</returns>
        public SMCPagerData<ConfiguracaoComponenteListaVO> BuscarConfiguracoesComponentes(ConfiguracaoComponenteFilterSpecification filtros, bool desabilitaFiltroDados = false)
        {
            if (desabilitaFiltroDados)
            {
                this.DisableFilter(FILTER.HIERARQUIA_ENTIDADE_ORGANIZACIONAL);
            }

            int total = 0;

            var lista = this.SearchBySpecification(filtros, out total, IncludesConfiguracaoComponente.ComponenteCurricular_NiveisEnsino_NivelEnsino
                                                                     | IncludesConfiguracaoComponente.ComponenteCurricular_EntidadesResponsaveis_Entidade
                                                                     | IncludesConfiguracaoComponente.DivisoesComponente_TipoDivisaoComponente_Modalidade
                                                                     | IncludesConfiguracaoComponente.DivisoesComponente_ComponenteCurricularOrganizacao).ToList();

            var listResult = lista.TransformList<ConfiguracaoComponenteListaVO>();

            // Busca os tipos de componente da instituição
            var tiposComponenteNivel = this.InstituicaoNivelTipoComponenteCurricularDomainService
                .SearchProjectionAll(p => new
                {
                    p.InstituicaoNivel.SeqNivelEnsino,
                    p.SeqTipoComponenteCurricular,
                    p.FormatoCargaHoraria
                });

            foreach (var configuracaoComponente in listResult)
            {
                // Recupera o formato correto para o nível de ensino e tipo de componente na instituição
                configuracaoComponente.FormatoCargaHoraria = tiposComponenteNivel.FirstOrDefault(
                    f => f.SeqNivelEnsino == configuracaoComponente.SeqNivelEnsino
                      && f.SeqTipoComponenteCurricular == configuracaoComponente.SeqTipoComponenteCurricular)
                        ?.FormatoCargaHoraria;

                configuracaoComponente.DescricaoFormatada = GerarDescricaoConfiguracaoComponenteCurricular(
                    configuracaoComponente.Codigo,
                    configuracaoComponente.Descricao,
                    configuracaoComponente.DescricaoComplementar,
                    configuracaoComponente.ComponenteCurricularCredito,
                    configuracaoComponente.ComponenteCurricularCargaHoraria,
                    configuracaoComponente.FormatoCargaHoraria,
                    lista.Single(s => s.Seq == configuracaoComponente.Seq).ComponenteCurricular.EntidadesResponsaveis?.Select(s => s.Entidade.Sigla));

                // Replica o formato nas dependências
                foreach (var divisao in configuracaoComponente.DivisoesComponente)
                {
                    divisao.FormatoCargaHoraria = configuracaoComponente.FormatoCargaHoraria;
                }
            }

            if (desabilitaFiltroDados)
            {
                this.EnableFilter(FILTER.HIERARQUIA_ENTIDADE_ORGANIZACIONAL);
            }

            return new SMCPagerData<ConfiguracaoComponenteListaVO>(listResult, total);
        }

        /// <summary>
        /// Buscar as configurações de componente, necessario para preencher o combo de entidades descrição na listagem
        /// </summary>
        /// <param name="filtro">Filtros definidos para a configuração de componente</param>
        /// <returns>Lista de configuração de componente</returns>
        public SMCPagerData<ConfiguracaoComponenteVO> BuscarConfiguracoesComponentesLookup(ConfiguracaoComponenteFiltroVO filtrosVO)
        {
            var filtros = filtrosVO.Transform<ConfiguracaoComponenteFilterSpecification>();
            var seqsEntidadeHierarquiaOrganizacional = GetDataFilter(FILTER.HIERARQUIA_ENTIDADE_ORGANIZACIONAL);
            var utilizaFiltroDados = seqsEntidadeHierarquiaOrganizacional.SMCAny();
            if (filtrosVO.IgnorarFiltroDados || filtrosVO.Seq.HasValue || (filtrosVO.SeqsMatrizCurricular != null && filtrosVO.SeqsMatrizCurricular.Any()))
            {
                FilterHelper.DesativarFiltros(this);
            }
            else if (utilizaFiltroDados)
            {
                var specHierarquiaEntidadeItem = new SMCContainsSpecification<HierarquiaEntidadeItem, long>(p => p.Seq, seqsEntidadeHierarquiaOrganizacional);
                FilterHelper.DesativarFiltros(HierarquiaEntidadeItemDomainService);
                var seqsEntidade = HierarquiaEntidadeItemDomainService.SearchProjectionBySpecification(specHierarquiaEntidadeItem, p => p.SeqEntidade).ToList();
                FilterHelper.AtivarFiltros(HierarquiaEntidadeItemDomainService);

                filtros.SeqsEntidadesResponsaveis = HierarquiaEntidadeDomainService
                    .BuscarEntidadesSuperioresSelect(seqsEntidade, TipoVisao.VisaoOrganizacional)
                    .Select(s => s.Seq)
                    .ToList();
                filtros.SeqsEntidadesResponsaveis = filtros.SeqsEntidadesResponsaveis.Union(seqsEntidade).Distinct().ToList();
                FilterHelper.DesativarFiltros(this);
            }

            int total = 0;
            var lista = this.SearchBySpecification(filtros, out total, IncludesConfiguracaoComponente.ComponenteCurricular
                                                                     | IncludesConfiguracaoComponente.ComponenteCurricular_NiveisEnsino
                                                                     | IncludesConfiguracaoComponente.ComponenteCurricular_EntidadesResponsaveis_Entidade
                                                                     | IncludesConfiguracaoComponente.DivisoesComponente_TipoDivisaoComponente).ToList();

            List<ConfiguracaoComponenteVO> listResult = new List<ConfiguracaoComponenteVO>();
            foreach (var item in lista.ToList())
            {
                var configuracao = item.Transform<ConfiguracaoComponenteVO>();
                configuracao.ComponenteCurricularCargaHoraria = item.ComponenteCurricular.CargaHoraria;
                configuracao.ComponenteCurricularCredito = item.ComponenteCurricular.Credito;
                configuracao.SeqNivelEnsino = item.ComponenteCurricular.NiveisEnsino.Where(s => s.Responsavel == true).Select(s => s.SeqNivelEnsino).FirstOrDefault();
                configuracao.FormatoCargaHoraria = InstituicaoNivelTipoComponenteCurricularDomainService.SearchProjectionByKey(new InstituicaoNivelTipoComponenteCurricularFilterSpecification()
                {
                    SeqNivelEnsino = configuracao.SeqNivelEnsino,
                    SeqTipoComponenteCurricular = item.ComponenteCurricular.SeqTipoComponenteCurricular
                }, i => i.FormatoCargaHoraria);
                configuracao.ComponenteCurricularEntidadesSigla = item.ComponenteCurricular.EntidadesResponsaveis.Select(s => s.Entidade.Sigla);

                listResult.Add(configuracao);
            }

            if (utilizaFiltroDados || filtrosVO.IgnorarFiltroDados || filtrosVO.Seq.HasValue)
            {
                FilterHelper.AtivarFiltros(this);
            }

            return new SMCPagerData<ConfiguracaoComponenteVO>(listResult, total);
        }

        /// <summary>
        /// Buscar as configurações do componente curricular de um grupo curricular componente
        /// </summary>
        /// <param name="seqGrupoCurricularComponente">Sequencial do grupo curricular compoente</param>
        /// <returns>Lista com as organizações</returns>
        public List<SMCDatasourceItem> BuscarConfiguracaoComponentePorGrupoCurricularComponenteSelect(long seqGrupoCurricularComponente)
        {
            var registro = this.GrupoCurricularComponenteDomainService.SearchProjectionByKey(new SMCSeqSpecification<GrupoCurricularComponente>(seqGrupoCurricularComponente),
                p => p.ComponenteCurricular.Configuracoes.Select(s => new GrupoCurricularComponenteVO()
                {
                    Seq = s.Seq,
                    Codigo = s.Codigo,
                    Descricao = s.Descricao,
                    DescricaoComplementar = s.DescricaoComplementar,
                    CargaHoraria = s.ComponenteCurricular.CargaHoraria,
                    Credito = p.ComponenteCurricular.Credito,
                    SeqTipoComponenteCurricular = p.ComponenteCurricular.SeqTipoComponenteCurricular,
                    SeqNivelEnsino = p.ComponenteCurricular.NiveisEnsino.Where(w => w.Responsavel == true).FirstOrDefault().SeqNivelEnsino,
                    EntidadesSigla = p.ComponenteCurricular.EntidadesResponsaveis.Select(e => e.Entidade.Sigla).ToList(),
                }))
                .ToList();

            List<SMCDatasourceItem> result = new List<SMCDatasourceItem>();
            foreach (var item in registro.ToList())
            {
                item.Formato = InstituicaoNivelTipoComponenteCurricularDomainService.SearchProjectionByKey(new InstituicaoNivelTipoComponenteCurricularFilterSpecification()
                {
                    SeqNivelEnsino = item.SeqNivelEnsino,
                    SeqTipoComponenteCurricular = item.SeqTipoComponenteCurricular
                }, i => i.FormatoCargaHoraria);

                result.Add(new SMCDatasourceItem()
                {
                    Seq = item.Seq,
                    Descricao = item.DescricaoFormatada,
                });
            }

            return result;
        }

        public long ConfiguracaoComponenteSequencial(long seqComponenteCurricular)
        {
            ConfiguracaoComponenteFilterSpecification spec = new ConfiguracaoComponenteFilterSpecification();
            spec.SeqComponenteCurricular = seqComponenteCurricular;

            var listComponente = this.SearchBySpecification(spec).OrderByDescending(o => o.Seq);

            if (listComponente.Count() == 0)
                return 1;
            else
                return listComponente.ToList()[0].Seq + 1;
        }

        /// <summary>
        /// Salvar a configuração do componente
        /// </summary>
        /// <param name="configuracaoComponente"></param>
        /// <returns>Sequencial da configuração do componente</returns>
        /// <exception cref="ConfiguracaoComponenteCargaHorariaDivergenteException">RN_CUR_079 validação de carga horária dos itens de organização</exception>
        /// <exception cref="ConfiguracaoComponenteDivisaoAlteracaoProibidaException">Caso seja alterado algum campo não permitido para uma configuração já associada</exception>
        /// <exception cref="ConfiguracaoComponenteDivisaoCargaHorariaDivergenteException">Validação da carga horária das divisões</exception>
        /// <exception cref="ConfiguracaoComponenteInativoException">RN_CUR_083 - Consistência configuração componente</exception>
        /// <exception cref="ConfiguracaoComponenteOrganizacaoInvalidaException">Caso seja informado um tipo de organização inválido</exception>
        /// <exception cref="ConfiguracaoComponenteTipoDivisaoComponenteInvalidoException">Caso sera informado um tipo de divisão inválido</exception>
        public long SalvarConfiguracaoComponente(ConfiguracaoComponenteVO configuracaoComponente)
        {
            if (configuracaoComponente != null && configuracaoComponente.DivisoesComponente != null)
            {
                // Valida os dados dos datasources armazenados em session e utilizados no detalhe do mestre detalhe modal
                var seqsTiposDivisao = this.TipoDivisaoComponenteDomainService.BuscarTipoDivisaoComponentePorComponenteSelect(configuracaoComponente.SeqComponenteCurricular).Select(s => s.Seq);
                if (configuracaoComponente.DivisoesComponente.Any(a => !seqsTiposDivisao.Contains(a.SeqTipoDivisaoComponente)))
                {
                    throw new ConfiguracaoComponenteTipoDivisaoComponenteInvalidoException();
                }
            }

            // Recupera o componente curricular
            var componenteCurricular = ComponenteCurricularDomainService.BuscarComponenteCurricular(configuracaoComponente.SeqComponenteCurricular);

            // RN_CUR_083 - Consistência configuração componente
            if (!componenteCurricular.Ativo && configuracaoComponente.Ativo)
            {
                throw new ConfiguracaoComponenteInativoException();
            }

            var configuracaoAtual = componenteCurricular.Configuracoes.FirstOrDefault(c => c.Seq == configuracaoComponente.Seq);
            foreach (var divisao in configuracaoComponente.DivisoesComponente)
            {
                // Caso tenha sido associado a uma divisão de matriz curricular, não permite alteração das divisões
                if (configuracaoAtual?.DivisoesMatrizCurricularComponente?.Count > 0)
                {
                    var divisaoAtual = configuracaoAtual?.DivisoesComponente?.FirstOrDefault(d => d.Seq == divisao.Seq);

                    if (divisaoAtual == null ||
                        divisao.CargaHoraria != divisaoAtual.CargaHoraria ||
                        divisao.Numero != divisaoAtual.Numero ||
                        divisao.PermiteGrupo != divisaoAtual.PermiteGrupo ||
                        divisao.QualisCapes != divisaoAtual.QualisCapes ||
                        divisao.SeqConfiguracaoComponente != divisaoAtual.SeqConfiguracaoComponente ||
                        divisao.SeqTipoDivisaoComponente != divisaoAtual.SeqTipoDivisaoComponente ||
                        divisao.TipoEventoPublicacao != divisaoAtual.TipoEventoPublicacao ||
                        divisao.TipoPublicacao != divisaoAtual.TipoPublicacao)
                    {
                        throw new ConfiguracaoComponenteDivisaoAlteracaoProibidaException();
                    }
                }
            }
            // Valida a carga horária das divisões com o componente apenas se o componente tiver carga horária definida
            if (componenteCurricular.CargaHoraria.GetValueOrDefault() > 0 && componenteCurricular.CargaHoraria != configuracaoComponente.DivisoesComponente.Sum(s => s.CargaHoraria.GetValueOrDefault()))
            {
                throw new ConfiguracaoComponenteDivisaoCargaHorariaDivergenteException();
            }

            using (ISMCUnitOfWork transacao = SMCUnitOfWork.Begin())
            {
                // Caso seja uma atualização, seta todos os números para fora do range para evitar colisões da constraint de número no banco
                if (configuracaoComponente.Seq != 0)
                {
                    var specDivisoes = new DivisaoComponenteFilterSpecification() { SeqConfiguracaoComponente = configuracaoComponente.Seq };
                    var divisaoComponenteDomainService = this.DivisaoComponenteDomainService;
                    var divisoes = divisaoComponenteDomainService.SearchBySpecification(specDivisoes).ToList();
                    var numeroMax = divisoes.SMCCount() == 0 ? (short)1 : divisoes.Max(m => m.Numero);
                    divisoes.SMCForEach(f =>
                    {
                        f.Numero = ++numeroMax;
                        if (configuracaoComponente.DivisoesComponente.Select(s => s.Seq).Contains(f.Seq))
                        {
                            // Caso o item ainda exista no modelo atualiza o número para fora do range para evitar colisões da constraint
                            divisaoComponenteDomainService.SaveEntity(f);
                        }
                        else
                        {
                            // Caso contrário mata o item para contornar a falha do cascade ao gravar o grafo completo
                            divisaoComponenteDomainService.DeleteEntity(f);
                        }
                    });
                }

                // Ordena a lista de divisões pelo número e acerta com o indice da linha
                short numero = 1;
                configuracaoComponente.DivisoesComponente.SMCForEach(item => item.Numero = numero++);

                // Salva o objeto configuração componente
                var registro = configuracaoComponente.Transform<ConfiguracaoComponente>();

                registro.Descricao = this.GerarDescricaoComponenteCurricular(registro);

                this.SaveEntity(registro);
                transacao.Commit();

                return registro.Seq;
            }
        }

        /// <summary>
        /// Gera a descrição da configuração de componente segundo a regra RN_CUR_083
        /// </summary>
        /// <param name="configuracaoComponente"></param>
        /// <returns>Descrição da configuração de componente no formado [Descrição componente] + [Modalidade]</returns>
        private string GerarDescricaoComponenteCurricular(ConfiguracaoComponente configuracaoComponente)
        {
            // Recupera a descrição do componente
            var specComponente = new SMCSeqSpecification<ComponenteCurricular>(configuracaoComponente.SeqComponenteCurricular);
            var descricao = new StringBuilder()
                .Append(this.ComponenteCurricularDomainService.SearchProjectionByKey(specComponente, p => p.Descricao));

            // Atualiza o labem conforme as modalidades do item
            // Recupera as modalidades dos tipos de configuração das divisões do componente.
            var seqsTiposDivisaoComponete = configuracaoComponente.DivisoesComponente.Select(s => s.SeqTipoDivisaoComponente).ToArray();
            var specTiposDivisoes = new SMCContainsSpecification<TipoDivisaoComponente, long>(p => p.Seq, seqsTiposDivisaoComponete);
            var modalidades = this.TipoDivisaoComponenteDomainService
                .SearchProjectionBySpecification(
                    specTiposDivisoes,
                    p => new { p.SeqModalidade, p.Modalidade.Descricao }
                );

            // Caso a descrição de todas modalidades sejam iguais
            if (modalidades.GroupBy(g => g.SeqModalidade).Count() == 1)
            {
                // Caso estejam preenchidas
                if (!string.IsNullOrEmpty(modalidades.First().Descricao))
                {
                    descricao.Append($" - {modalidades.First().Descricao}");
                }
                // Caso nenhuma esteja preenchida não concatena label
            }
            else
            {
                // Concatena o label semipresencial caso contrário
                descricao.Append($" - {MessagesResource.Label_ModalidadeSemipresencial}");
            }

            return descricao.ToString();
        }

        /// <summary>
        /// Gera a descrição da configuração de um componente curricular segundo a regra RN_CUR_042
        /// </summary>
        /// <param name="codigo">Código da configuração</param>
        /// <param name="descricao">Descrição da configuração</param>
        /// <param name="descricaoComplementar">Descrição complementar da configuração</param>
        /// <param name="creditos">Créditos do componente</param>
        /// <param name="cargaHoraria">Carga horária do componente</param>
        /// <param name="formatoCargaHoraria">Formato de carga horária na instituição para o nível de ensino e tipo do componente</param>
        /// <param name="siglasEntidadesResponsaveis">Siglas das entidades responsáveis</param>
        /// <returns>Descrição da configuração do componente com a máscara [Código da configuração] + "-" + [Descrição] + "-" + [Descrição complementar] + "-" + [Carga horária do componente curricular] + [label parametrizado] + "-" + [Créditos do componente curricular] + "Créditos" + "-"  + [Lista de siglas das entidades responsáveis do componente separadas por "/"].</returns>
        public static string GerarDescricaoConfiguracaoComponenteCurricular(string codigo, string descricao, string descricaoComplementar, short? creditos, short? cargaHoraria, FormatoCargaHoraria? formatoCargaHoraria, IEnumerable<string> siglasEntidadesResponsaveis)
        {
            var partes = new List<string>();
            partes.Add(codigo);
            partes.Add(descricao);
            if (!string.IsNullOrEmpty(descricaoComplementar))
            {
                partes.Add(descricaoComplementar);
            }

            if (cargaHoraria.GetValueOrDefault() > 0)
            {
                string labelHoraAula = null;
                if (formatoCargaHoraria == FormatoCargaHoraria.Hora)
                {
                    labelHoraAula = cargaHoraria == 1 ? MessagesResource.Label_Hora : MessagesResource.Label_Horas;
                }
                else
                {
                    labelHoraAula = cargaHoraria == 1 ? MessagesResource.Label_HoraAula : MessagesResource.Label_HorasAula;
                }

                partes.Add($"{cargaHoraria} {labelHoraAula}");
            }

            if (creditos.GetValueOrDefault() > 0)
            {
                string labelCredito = creditos == 1 ? MessagesResource.Label_Credito : MessagesResource.Label_Creditos;

                partes.Add($"{creditos} {labelCredito}");
            }

            if (siglasEntidadesResponsaveis.SMCCount() > 0)
            {
                partes.Add(string.Join("/", siglasEntidadesResponsaveis.OrderBy(o => o)));
            }

            return string.Join(" - ", partes);
        }

        /// <summary>
        /// Busca as configurações de componente de acordo com a matriz curricular oferta da pesso atuação
        /// </summary>
        /// <param name="seqPessoaAtuacao">Sequencial pessoa atuação</param>
        /// <param name="seqSolicitacaoMatricula">Sequencial solicitação matricula</param>
        /// <returns>Lista configuração de componentes</returns>
        public SMCPagerData<ConfiguracaoComponenteListaVO> BuscarConfiguracaoComponentePessoaAtuacaoEntidade(long seqPessoaAtuacao, long seqSolicitacaoMatricula)
        {
            InstituicaoNivelTipoVinculoAlunoVO dadosVinculo = new InstituicaoNivelTipoVinculoAlunoVO();
            PessoaAtuacaoDadosOrigemVO dadosOrigem = new PessoaAtuacaoDadosOrigemVO();

            dadosVinculo = InstituicaoNivelTipoVinculoAlunoDomainService.BuscarInstituicaoNivelTipoVinculoAlunoPorPessoaAtuacao(seqPessoaAtuacao);
            dadosOrigem = PessoaAtuacaoDomainService.RecuperaDadosOrigem(seqPessoaAtuacao);

            //var seqTipoGruposVazios = this.MatrizCurricularOfertaDomainService.SearchProjectionByKey(
            //                                                                          new SMCSeqSpecification<MatrizCurricularOferta>(dadosIngressante.SeqMatrizCurricularOferta.Value),
            //                                                                          p => p.MatrizCurricular.CurriculoCursoOferta.Curriculo.GruposCurriculares.Select(s => new { SeqTipo = s.SeqTipoGrupoCurricular, TotalComponentes = s.ComponentesCurriculares.Count }));

            //seqTipoGruposVazios = seqTipoGruposVazios.Where(w => w.TotalComponentes == 0);

            if (dadosOrigem.SeqMatrizCurricularOferta == 0)
                throw new ConfiguracaoComponenteAtividadeSemMatrizException();

            //Recupera os sequenciais da configuração de componente associado a matriz curricular oferta
            var seqConfiguracoesComponente = this.MatrizCurricularOfertaDomainService.SearchProjectionByKey(
                                                                                      new SMCSeqSpecification<MatrizCurricularOferta>(dadosOrigem.SeqMatrizCurricularOferta),
                                                                                      p => p.MatrizCurricular.ConfiguracoesComponente.Select(s => s.SeqConfiguracaoComponente));

            if (!seqConfiguracoesComponente.Any())
                throw new ConfiguracaoComponenteAtividadeSemComponenteException();

            //Preenche os campos de filtro informando para não paginar
            var filtro = new ConfiguracaoComponenteFilterSpecification()
            {
                Ativo = true,
                SeqConfiguracoesComponentes = seqConfiguracoesComponente.ToArray()
            };
            filtro.MaxResults = int.MaxValue;
            filtro.SetOrderBy(s => s.Descricao);

            List<TipoGestaoDivisaoComponente?> tiposGestao = new List<TipoGestaoDivisaoComponente?>();
            tiposGestao.Add(TipoGestaoDivisaoComponente.Turma);
            tiposGestao.Add(TipoGestaoDivisaoComponente.Trabalho);
            tiposGestao.Add(TipoGestaoDivisaoComponente.AssuntoComponente);

            if (dadosVinculo.ConcedeFormacao == true)
                tiposGestao.Remove(TipoGestaoDivisaoComponente.Trabalho);
            else if (dadosVinculo != null)
            {
                var dadosIntercambio = InstituicaoNivelTipoTermoIntercambioDomainService.BuscarInstituicoesNivelTipoVinculoAluno(new InstituicaoNivelTipoTermoIntercambioFilterSpecification() { SeqInstituicaoNivelTipoVinculoAluno = dadosVinculo.Seq });
                if (dadosIntercambio != null && dadosIntercambio.FirstOrDefault().ConcedeFormacao == true)
                    tiposGestao.Remove(TipoGestaoDivisaoComponente.Trabalho);
            }

            filtro.VariosTipoGestaoDivisaoComponenteDiferente = tiposGestao.ToArray();

            var listaRegistro = BuscarConfiguracoesComponentes(filtro);

            foreach (var item in listaRegistro)
            {
                //Verifica se essa atividade já existe na tabela de solicitação item
                var filtroItem = new SolicitacaoMatriculaItemFiltroVO()
                {
                    SeqSolicitacaoMatricula = seqSolicitacaoMatricula,
                    ExibirTurma = false,
                    SeqConfiguracaoComponente = item.Seq
                };

                item.SeqSolicitacaoMatriculaItem = SolicitacaoMatriculaItemDomainService.BuscarSequencialSolicitacaoMatriculaItem(filtroItem);

                if (dadosOrigem.TipoAtuacao == TipoAtuacao.Ingressante)
                {
                    var validacaoPre = RequisitoDomainService.ValidarPreRequisitos(seqPessoaAtuacao, item.DivisoesComponente.Select(s => s.Seq), null, null, null);

                    item.PreRequisito = !validacaoPre.Valido;
                }
            }

            return listaRegistro;
        }

        /// <summary>
        /// Verifica se exige a configuração de componente pertence a um curriculo
        /// </summary>
        /// <param name="seq">Sequencial da configuração do componente</param>
        /// <returns>Valor Sim caso a configuração de componente esteja associado um grupo de componente com matriz curricular</returns>
        public bool VerificaConfiguracaoComponentePertenceCurriculo(long seq)
        {
            var filtro = new DivisaoMatrizCurricularComponenteFilterSpecification() { SeqConfiguracaoComponente = seq };
            var configuracao = DivisaoMatrizCurricularComponenteDomainService.Count(filtro);

            return configuracao > 0;
        }

        /// <summary>
        /// - A cada configuração de componente associada à turma, salvar uma descrição conforme concatenação dos campos:
        /// [Descrição da configuração do componente] + "-" + [Descrição complementar da configuração do componente*] 
        /// + ":**" + [Descrição do assunto de componente**] + "-" + [Carga horária do componente curricular referente à configuração] 
        /// + [label parametrizado***] + "-" + [Créditos do componente curricular referente à configuração] + "Créditos" + "-" 
        /// + [Sigla da entidade responsável pelas ofertas de matriz associadas a turma****]
        /// 
        /// * A descrição complementar da configuração de componente pode ser nula
        /// ** A turma pode ou não ter assunto associado.Em caso de não ter assunto, ":" não deve ser exibido.
        /// *** O label parametrizado é o conteúdo do campo Formato em Parâmetros por Instituição e Nível de Ensino, para o 
        /// tipo do componente referente à configuração.
        /// **** Para buscar a entidade responsável pela oferta de matriz é necessário:
        /// 1. Buscar o tipo de entidade parametrizada para o tipo de componente e o nível de ensino responsável pelo componente referente 
        /// à configuração principal da turma;
        /// 2. Buscar na hierarquia de entidade qual a entidade responsável pela oferta de curso por localidade e turno de todas as 
        /// ofertas de matriz associadas à turma Esta busca deve retornar uma entidade do tipo de entidade encontrado no item 1;
        /// 3. Concatenar as siglas das entidades encontradas no item 2, sem repetições.Caso exista mais de uma entidade responsável, 
        /// concatenar a sigla de todas elas, separando por "/", em ordem alfabética.
        /// 
        /// - Na ausência da carga horária ou do crédito, retirar os labels: [label parametrizado] e "Créditos". 
        /// </summary>
        /// <returns></returns>
        public string FormatarDescricaoConfiguracaoTurma(ConfiguracaoComponenteVO configuracao)
        {
            if (configuracao == null) { return string.Empty; }

            // Validações de textos
            var DescricaoComplementarConfiguracaoComponente = string.IsNullOrEmpty(configuracao.DescricaoComplementar) ? "" : $" - {configuracao.DescricaoComplementar.Trim()}: ";
            var LabelParametrizado = configuracao.FormatoCargaHoraria?.SMCGetDescription();
            var Credito = configuracao.ComponenteCurricularCredito.HasValue && configuracao.ComponenteCurricularCredito > 1 ? "Créditos" : "Crédito";
            var Siglas = configuracao.EntidadeResponsavelOfertaMatrizSigla.SMCAny(x => !string.IsNullOrEmpty(x)) ? $" - {string.Join(" / ", configuracao.EntidadeResponsavelOfertaMatrizSigla.OrderBy(x => x))}" : "";
            var DescricaoAssuntoComponenteTemp = string.IsNullOrEmpty(configuracao.DescricaoAssuntoComponente) ? " - " : $": {configuracao.DescricaoAssuntoComponente.Trim()} - ";

            var descricaoFormatada = $"{configuracao.Descricao.Trim()}{DescricaoComplementarConfiguracaoComponente}{DescricaoAssuntoComponenteTemp}{configuracao.ComponenteCurricularCargaHoraria} " +
                                     $"{LabelParametrizado} - {configuracao.ComponenteCurricularCredito} {Credito}{Siglas}";

            // Na ausência da carga horária ou do crédito, retirar os labels: [label parametrizado] e "Créditos".
            if (!configuracao.ComponenteCurricularCargaHoraria.HasValue || !configuracao.ComponenteCurricularCredito.HasValue)
            {
                descricaoFormatada = $"{configuracao.Descricao.Trim()}{DescricaoComplementarConfiguracaoComponente}{DescricaoAssuntoComponenteTemp}{configuracao.ComponenteCurricularCargaHoraria}{Siglas.Trim()}";
            }

            return descricaoFormatada;
        }

        public List<SMCDatasourceItem> BuscarConfiguracoesComponentePorCicloLetivoCursoOfertaLocalidadeSelect(long? seqCicloLetivo, long? seqCursoOfertaLocalidade, long? seqTurno)
        {
            //Cria a lista de componentes para ser retornada
            var listaConfiguracaoComponente = new List<SMCDatasourceItem>();

            //Caso não sejam informados os parametros, retorna uma lista vazia
            if (!seqCicloLetivo.HasValue || !seqCursoOfertaLocalidade.HasValue || !seqTurno.HasValue)
                return listaConfiguracaoComponente;

            //Cria o specification para recuperar o CursoOfertaLocalidadeTurno
            var specCursoOfertaLocalidadeTurno = new CursoOfertaLocalidadeTurnoFilterSpecification() { SeqCursoOfertaLocalidade = seqCursoOfertaLocalidade, SeqTurno = seqTurno };

            //Recupera o CursoOfertaLocalidadeTurno
            var seqCursoOfertaLocalidadeTurno = CursoOfertaLocalidadeTurnoDomainService.SearchProjectionBySpecification(specCursoOfertaLocalidadeTurno, c => c.Seq).FirstOrDefault();

            //Recupera as datas do período letivo
            var datasEventoLetivo = this.ConfiguracaoEventoLetivoDomainService.BuscarDatasEventoLetivo(seqCicloLetivo.GetValueOrDefault(), seqCursoOfertaLocalidadeTurno, null, TOKEN_TIPO_EVENTO.PERIODO_CICLO_LETIVO);

            //Cria o specification para recuperar as matrizes curriculares do CursoOfertaLocalidadeTurno
            var specMatrizCurricularOferta = new MatrizCurricularOfertaFilterSpecification()
            {
                SeqCursoOfertaLocalidadeTurno = seqCursoOfertaLocalidadeTurno,
            };

            //Cria uma lista de tipos de gestão de divisão componente que devem ser ignorados
            var tiposGestoesDivisaoComponenteDiferente = new TipoGestaoDivisaoComponente[] { TipoGestaoDivisaoComponente.Turma, TipoGestaoDivisaoComponente.AssuntoComponente };

            //Recupera as matrizes curriculares com seus historicos de situações cuja situação da patriz seja Ativa ou Em Extinção e
            //com os sequenciais das configurações de componente cujo o tipo de gestão seja diferente de Turma e AssuntoComponente
            var matrizes = this.MatrizCurricularOfertaDomainService.SearchProjectionBySpecification(specMatrizCurricularOferta, m => new
            {
                HistoricosSituacao = m.HistoricosSituacao.Where(h => h.SituacaoMatrizCurricularOferta == SituacaoMatrizCurricularOferta.Ativa || h.SituacaoMatrizCurricularOferta == SituacaoMatrizCurricularOferta.EmExtincao).ToList(),
                SeqsConfiguracoesComponente = m.MatrizCurricular.ConfiguracoesComponente.Where(c => m.HistoricosSituacao.Any(h => h.SituacaoMatrizCurricularOferta == SituacaoMatrizCurricularOferta.Ativa || h.SituacaoMatrizCurricularOferta == SituacaoMatrizCurricularOferta.EmExtincao) && !c.DivisoesComponente.Any(d => tiposGestoesDivisaoComponenteDiferente.Contains(d.DivisaoComponente.TipoDivisaoComponente.TipoGestaoDivisaoComponente))).Select(c => c.SeqConfiguracaoComponente).ToList()
            }).ToList();


            //Recupera os sequencias das configurações dos componentes curriculares de todas as matrizes
            var seqsConfiguracoesComponente = matrizes.SelectMany(m => m.SeqsConfiguracoesComponente).Distinct().ToArray();

            //Cria o specification para recuperar as configurações dos componentes curriculares das matrizes
            var specConfiguracaoComponente = new ConfiguracaoComponenteFilterSpecification() { SeqConfiguracoesComponentes = seqsConfiguracoesComponente };

            //Busca todas as configurações de componentes das matrizes
            var configuracoesComponentes = BuscarConfiguracoesComponentes(specConfiguracaoComponente);

            var selecaoConfiguracaoComponente = new List<ConfiguracaoComponenteSelecaoVO>();

            //Para cada matriz encontrada
            foreach (var matriz in matrizes)
            {
                //Para cada historico de situação da matriz
                foreach (var historico in matriz.HistoricosSituacao)
                {
                    //Verifica se o histórico da situação da matriz está dentro do periodo letivo.
                    //Caso esteja, adiciona as configurações de componente na lista de retorno.
                    if (historico.DataInicio <= datasEventoLetivo.DataInicio && (!historico.DataFim.HasValue || historico.DataFim >= datasEventoLetivo.DataFim))
                    {
                        //Novas configurações para serem adicionadas a lista de retorno
                        var novasConfiguracoesComponente = configuracoesComponentes.Where(c => matriz.SeqsConfiguracoesComponente.Contains(c.Seq)).Select(c => new ConfiguracaoComponenteSelecaoVO() { Seq = c.Seq, Descricao = c.Descricao, ConfiguracaoComponenteDescricaoCompleta = c.ConfiguracaoComponenteDescricaoCompleta }).ToList();

                        //Adiciona as configurações de componete na lista caso não existam
                        selecaoConfiguracaoComponente.AddRange(novasConfiguracoesComponente.Where(n => !selecaoConfiguracaoComponente.Any(a => a.Seq == n.Seq)));
                    }
                }
            }

            //prepara a lista de configurações em ordem alfabetica pela descricao
            listaConfiguracaoComponente = selecaoConfiguracaoComponente.OrderBy(w => w.Descricao).Select(s => new SMCDatasourceItem() { Seq = s.Seq, Descricao = s.ConfiguracaoComponenteDescricaoCompleta }).ToList();

            //Retorna a lista de componentes
            return listaConfiguracaoComponente;
        }

        /// <summary>
        /// Buscar dados do cabeçalho configuração do componente por matriz
        /// </summary>
        /// <param name="seqMatrizCurricular">Sequencial matriz curricular</param>
        public ConfiguracaoComponeteMatrizCabecalhoVO BuscarCabecalhoConfiguracaoComponentePorMatriz(long seqMatrizCurricular)
        {
            var consultaMatriz = this.MatrizCurricularDomainService
                .SearchProjectionByKey(seqMatrizCurricular, p => new
                {
                    SeqCurriculoCursoOferta = p.SeqCurriculoCursoOferta,
                    SeqsGruposMatriz = p.CurriculoCursoOferta
                        .GruposCurriculares
                        .Select(s => s.GrupoCurricular.Seq),
                    DescricaoMatrizCurricular = p.Descricao,
                    DescricaoComplementarMatrizCurricular = p.DescricaoComplementar
                });

            GrupoCurricularComponenteFilterSpecification specComponentes = new GrupoCurricularComponenteFilterSpecification();
            specComponentes.SeqGruposCurriculares = consultaMatriz.SeqsGruposMatriz.ToList();

            var componentesCurriculares = this.GrupoCurricularComponenteDomainService
                .SearchProjectionBySpecification(specComponentes, componente => new
                {
                    componente.SeqComponenteCurricular,
                    Conifguracoes = componente.DivisoesMatrizCurricularComponente.Select(s => s.SeqConfiguracaoComponente).ToList()
                }).ToList();

            ConfiguracaoComponeteMatrizCabecalhoVO retorno = new ConfiguracaoComponeteMatrizCabecalhoVO();

            retorno.DescricaoMatriz = consultaMatriz.DescricaoMatrizCurricular;
            if (!string.IsNullOrEmpty(consultaMatriz.DescricaoComplementarMatrizCurricular))
            {
                retorno.DescricaoMatriz = $"{retorno.DescricaoMatriz} - {consultaMatriz.DescricaoComplementarMatrizCurricular}";
            }
            retorno.TotalComponentes = componentesCurriculares.Select(s => s.SeqComponenteCurricular).Distinct().Count();
            retorno.TotalComponentesComConfiguracao = componentesCurriculares.Where(w => w.Conifguracoes.Count > 0)
                                                                             .Select(s => s.SeqComponenteCurricular)
                                                                             .Distinct().Count();
            return retorno;
        }
    }
}