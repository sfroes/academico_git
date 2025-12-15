using SMC.Academico.Common.Areas.ALN.Enums;
using SMC.Academico.Common.Areas.ALN.Exceptions;
using SMC.Academico.Common.Areas.ALN.Includes;
using SMC.Academico.Common.Areas.ALN.Resources;
using SMC.Academico.Common.Areas.CAM.Enums;
using SMC.Academico.Common.Areas.CAM.Exceptions;
using SMC.Academico.Common.Areas.CSO.Includes;
using SMC.Academico.Common.Areas.CUR.Enums;
using SMC.Academico.Common.Areas.CUR.Exceptions;
using SMC.Academico.Common.Areas.DCT.Enums;
using SMC.Academico.Common.Areas.FIN.Enums;
using SMC.Academico.Common.Areas.FIN.Includes;
using SMC.Academico.Common.Areas.MAT.Includes;
using SMC.Academico.Common.Areas.ORT.Enums;
using SMC.Academico.Common.Areas.ORT.Exceptions;
using SMC.Academico.Common.Areas.PES.Enums;
using SMC.Academico.Common.Areas.PES.Exceptions;
using SMC.Academico.Common.Areas.PES.Includes;
using SMC.Academico.Common.Areas.SRC.Exceptions;
using SMC.Academico.Common.Areas.SRC.Includes;
using SMC.Academico.Common.Areas.TUR.Exceptions;
using SMC.Academico.Common.Constants;
using SMC.Academico.Domain.Areas.ALN.Models;
using SMC.Academico.Domain.Areas.ALN.Resources;
using SMC.Academico.Domain.Areas.ALN.Specifications;
using SMC.Academico.Domain.Areas.ALN.ValueObjects;
using SMC.Academico.Domain.Areas.CAM.DomainServices;
using SMC.Academico.Domain.Areas.CAM.Models;
using SMC.Academico.Domain.Areas.CAM.Specifications;
using SMC.Academico.Domain.Areas.CAM.ValueObjects;
using SMC.Academico.Domain.Areas.CSO.DomainServices;
using SMC.Academico.Domain.Areas.CSO.Models;
using SMC.Academico.Domain.Areas.CSO.Specifications;
using SMC.Academico.Domain.Areas.CSO.ValueObjects;
using SMC.Academico.Domain.Areas.CUR.DomainServices;
using SMC.Academico.Domain.Areas.CUR.Models;
using SMC.Academico.Domain.Areas.DCT.DomainServices;
using SMC.Academico.Domain.Areas.DCT.Models;
using SMC.Academico.Domain.Areas.DCT.Specifications;
using SMC.Academico.Domain.Areas.DCT.ValueObjects;
using SMC.Academico.Domain.Areas.FIN.DomainServices;
using SMC.Academico.Domain.Areas.FIN.Specifications;
using SMC.Academico.Domain.Areas.FIN.ValueObjects;
using SMC.Academico.Domain.Areas.MAT.DomainServices;
using SMC.Academico.Domain.Areas.MAT.Models;
using SMC.Academico.Domain.Areas.MAT.ValueObjects;
using SMC.Academico.Domain.Areas.ORG.DomainServices;
using SMC.Academico.Domain.Areas.ORG.Models;
using SMC.Academico.Domain.Areas.ORG.Specifications;
using SMC.Academico.Domain.Areas.ORT.DomainServices;
using SMC.Academico.Domain.Areas.ORT.Models;
using SMC.Academico.Domain.Areas.ORT.Specifications;
using SMC.Academico.Domain.Areas.PES.DomainServices;
using SMC.Academico.Domain.Areas.PES.Models;
using SMC.Academico.Domain.Areas.PES.Specifications;
using SMC.Academico.Domain.Areas.PES.ValueObjects;
using SMC.Academico.Domain.Areas.SRC.DomainServices;
using SMC.Academico.Domain.Areas.SRC.Models;
using SMC.Academico.Domain.Areas.SRC.ValueObjects;
using SMC.Academico.Domain.Areas.TUR.DomainServices;
using SMC.Academico.Domain.Models;
using SMC.DadosMestres.Common.Areas.PES.Enums;
using SMC.DadosMestres.ServiceContract.Areas.PES.Interfaces;
using SMC.Financeiro.Common.Areas.GRA.Enums;
using SMC.Financeiro.Service.FIN;
using SMC.Financeiro.ServiceContract.Areas.FIN.Data;
using SMC.Financeiro.ServiceContract.Areas.GRA.Data;
using SMC.Financeiro.ServiceContract.BLT;
using SMC.Formularios.Common.Areas.TMP.Enums;
using SMC.Formularios.ServiceContract.Areas.FRM.Interfaces;
using SMC.Formularios.ServiceContract.Areas.TMP.Interfaces;
using SMC.Framework;
using SMC.Framework.Domain;
using SMC.Framework.Exceptions;
using SMC.Framework.Extensions;
using SMC.Framework.Mapper;
using SMC.Framework.Model;
using SMC.Framework.Specification;
using SMC.Framework.UnitOfWork;
using SMC.Framework.Util;
using SMC.Inscricoes.ServiceContract.Areas.INS.Data;
using SMC.Inscricoes.ServiceContract.Areas.INS.Interfaces;
using SMC.Localidades.Common.Constants;
using SMC.Localidades.ServiceContract.Areas.LOC.Interfaces;
using SMC.Pessoas.ServiceContract.Areas.PES.Data;
using SMC.Seguranca.ServiceContract.Areas.APL.Interfaces;
using SMC.Seguranca.ServiceContract.Areas.PER.Interfaces;
using SMC.Seguranca.ServiceContract.Areas.USU.Data;
using SMC.Seguranca.ServiceContract.Areas.USU.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SMC.Academico.Domain.Areas.ALN.DomainServices
{
    public class IngressanteDomainService : AcademicoContextDomain<Ingressante>
    {
        #region [ DomainServices ]

        private FormacaoEspecificaDomainService FormacaoEspecificaDomainService => Create<FormacaoEspecificaDomainService>();

        private PessoaAtuacaoBeneficioDomainService PessoaAtuacaoBeneficioDomainService => Create<PessoaAtuacaoBeneficioDomainService>();

        private InstituicaoNivelTipoVinculoAlunoDomainService InstituicaoNivelTipoVinculoAlunoDomainService => Create<InstituicaoNivelTipoVinculoAlunoDomainService>();

        private CursoDomainService CursoDomainService => Create<CursoDomainService>();

        private InstituicaoNivelTipoTermoIntercambioDomainService InstituicaoNivelTipoTermoIntercambioDomainService => Create<InstituicaoNivelTipoTermoIntercambioDomainService>();

        private SolicitacaoMatriculaDomainService SolicitacaoMatriculaDomainService => Create<SolicitacaoMatriculaDomainService>();

        private PessoaDomainService PessoaDomainService => Create<PessoaDomainService>();

        private ProcessoSeletivoDomainService ProcessoSeletivoDomainService => Create<ProcessoSeletivoDomainService>();

        private CampanhaCicloLetivoDomainService CampanhaCicloLetivoDomainService => Create<CampanhaCicloLetivoDomainService>();

        private CampanhaOfertaDomainService CampanhaOfertaDomainService => Create<CampanhaOfertaDomainService>();

        private MatrizCurricularOfertaDomainService MatrizCurricularOfertaDomainService => Create<MatrizCurricularOfertaDomainService>();

        private GrupoEscalonamentoDomainService GrupoEscalonamentoDomainService => Create<GrupoEscalonamentoDomainService>();

        private TermoIntercambioDomainService TermoIntercambioDomainService => Create<TermoIntercambioDomainService>();

        private OrientacaoDomainService OrientacaoDomainService => Create<OrientacaoDomainService>();

        private TipoOrientacaoDomainService TipoOrientacaoDomainService => Create<TipoOrientacaoDomainService>();

        private InstituicaoExternaDomainService InstituicaoExternaDomainService => Create<InstituicaoExternaDomainService>();

        private ConfiguracaoProcessoDomainService ConfiguracaoProcessoDomainService => Create<ConfiguracaoProcessoDomainService>();

        private InstituicaoTipoEntidadeFormacaoEspecificaDomainService InstituicaoTipoEntidadeFormacaoEspecificaDomainService => Create<InstituicaoTipoEntidadeFormacaoEspecificaDomainService>();

        private InstituicaoNivelTipoOrientacaoDomainService InstituicaoNivelTipoOrientacaoDomainService => Create<InstituicaoNivelTipoOrientacaoDomainService>();

        private InstituicaoNivelTipoOrientacaoParticipacaoDomainService InstituicaoNivelTipoOrientacaoParticipacaoDomainService => Create<InstituicaoNivelTipoOrientacaoParticipacaoDomainService>();

        private PessoaAtuacaoDomainService PessoaAtuacaoDomainService => Create<PessoaAtuacaoDomainService>();

        private ProcessoDomainService ProcessoDomainService => Create<ProcessoDomainService>();

        private ProcessoSeletivoOfertaDomainService ProcessoSeletivoOfertaDomainService => Create<ProcessoSeletivoOfertaDomainService>();

        private CursoOfertaLocalidadeTurnoDomainService CursoOfertaLocalidadeTurnoDomainService => Create<CursoOfertaLocalidadeTurnoDomainService>();

        private PessoaDadosPessoaisDomainService PessoaDadosPessoaisDomainService => Create<PessoaDadosPessoaisDomainService>();

        private NivelEnsinoDomainService NivelEnsinoDomainService => Create<NivelEnsinoDomainService>();

        private ColaboradorDomainService ColaboradorDomainService => Create<ColaboradorDomainService>();

        private ConvocacaoDomainService ConvocacaoDomainService => Create<ConvocacaoDomainService>();

        private IngressanteHistoricoSituacaoDomainService IngressanteHistoricoSituacaoDomainService => Create<IngressanteHistoricoSituacaoDomainService>();

        private DivisaoTurmaDomainService DivisaoTurmaDomainService => Create<DivisaoTurmaDomainService>();

        private ConfiguracaoEventoLetivoDomainService ConfiguracaoEventoLetivoDomainService => Create<ConfiguracaoEventoLetivoDomainService>();

        private PessoaAtuacaoEnderecoDomainService PessoaAtuacaoEnderecoDomainService => Create<PessoaAtuacaoEnderecoDomainService>();

        private OrientacaoPessoaAtuacaoDomainService OrientacaoPessoaAtuacaoDomainService => Create<OrientacaoPessoaAtuacaoDomainService>();

        private PessoaAtuacaoTermoIntercambioDomainService PessoaAtuacaoTermoIntercambioDomainService => Create<PessoaAtuacaoTermoIntercambioDomainService>();

        private SolicitacaoServicoDomainService SolicitacaoServicoDomainService => Create<SolicitacaoServicoDomainService>();

        private SolicitacaoServicoEtapaDomainService SolicitacaoServicoEtapaDomainService => Create<SolicitacaoServicoEtapaDomainService>();

        private ConvocadoDomainService ConvocadoDomainService => Create<ConvocadoDomainService>();

        private ChamadaDomainService ChamadaDomainService => Create<ChamadaDomainService>();

        private HierarquiaEntidadeItemDomainService HierarquiaEntidadeItemDomainService => Create<HierarquiaEntidadeItemDomainService>();

        private InstituicaoNivelBeneficioDomainService InstituicaoNivelBeneficioDomainService => Create<InstituicaoNivelBeneficioDomainService>();

        #endregion [ DomainServices ]

        #region [ Services ]

        private IPerfilService PerfilService
        {
            get { return this.Create<IPerfilService>(); }
        }

        private IAplicacaoService AplicacaoService
        {
            get { return this.Create<IAplicacaoService>(); }
        }

        private IUsuarioService UsuarioService
        {
            get { return Create<IUsuarioService>(); }
        }

        private IFormularioService FormularioService
        {
            get { return this.Create<IFormularioService>(); }
        }

        private IPaginaService PaginaService
        {
            get { return this.Create<IPaginaService>(); }
        }

        private ISituacaoService SituacaoService
        {
            get { return this.Create<ISituacaoService>(); }
        }

        private ITemplateProcessoService TemplateProcessoService
        {
            get { return this.Create<ITemplateProcessoService>(); }
        }

        private IIntegracaoFinanceiroService IntegracaoFinanceiroService
        {
            get { return this.Create<IIntegracaoFinanceiroService>(); }
        }

        private IIntegracaoDadoMestreService IntegracaoDadoMestreService
        {
            get { return Create<IIntegracaoDadoMestreService>(); }
        }

        private ILocalidadeService LocalidadeService { get => Create<ILocalidadeService>(); }

        private IIntegracaoService IntegracaoService => Create<IIntegracaoService>();

        private IEtapaService EtapaService => Create<IEtapaService>();

        private IFinanceiroService FinanceiroService => Create<IFinanceiroService>();

        private Pessoas.ServiceContract.Areas.PES.Interfaces.IPessoaService PessoaService => Create<Pessoas.ServiceContract.Areas.PES.Interfaces.IPessoaService>();

        #endregion [ Services ]

        /// <summary>
        /// Busca as atuações de ingressante de uma pessoa com os dados pessoais
        /// </summary>
        /// <param name="filter">Filtro com o seq da pessoa</param>
        /// <returns>Dados de ingressante da pessoa informada</returns>
        public Ingressante[] BuscarIngressantesPessoa(IngressanteFilterSpecification filter)
        {
            var includesIngressante = IncludesIngressante.DadosPessoais_ArquivoFoto
                                    | IncludesIngressante.Pessoa_Filiacao
                                    | IncludesIngressante.Pessoa;

            var ingressantes = this.SearchBySpecification(filter, includesIngressante).ToArray();

            return ingressantes;
        }

        public List<SMCDatasourceItem> BuscarIngressantesSelect(IngressanteFilterSpecification spec)
        {
            return this.SearchProjectionBySpecification(spec, x => new SMCDatasourceItem { Seq = x.Seq, Descricao = x.ProcessoSeletivo.Descricao }).ToList();
        }

        public List<IngressanteProcessoSeletivoListaVO> BuscarIngressantesLista(IngressanteFilterSpecification spec)
        {
            // Considera apenas os aptos para matrícula
            spec.ApenasAptosParaMatricula = true;

            // Retorna a lista de ingressantes para Home
            var dados = this.SearchProjectionBySpecification(spec, x => new
            {
                SeqSolicitacaoServico = x.SolicitacoesServico.FirstOrDefault().Seq,
                SeqIngressante = x.Seq,
                DescricaoFormaIngresso = x.FormaIngresso.Descricao,
                DescricoesOfertas = x.Ofertas.Select(y => y.CampanhaOferta.Descricao).ToList(),
                DescricaoProcesso = x.ProcessoSeletivo.Descricao,
                DescricaoVinculo = x.TipoVinculoAluno.Descricao,
                NomeInstituicaoEnsino = x.ProcessoSeletivo.Campanha.EntidadeResponsavel.InstituicaoEnsino.Nome,
                SeqsTipoTermoIntercambioAssociados = x.TermosIntercambio.Select(t => t.TermoIntercambio.ParceriaIntercambioTipoTermo.SeqTipoTermoIntercambio)
            }).ToList();

            List<IngressanteProcessoSeletivoListaVO> ret = new List<IngressanteProcessoSeletivoListaVO>();
            foreach (var item in dados)
            {
                var itemParsed = SMCMapperHelper.Create<IngressanteProcessoSeletivoListaVO>(item);
                itemParsed.DescricaoVinculo = PessoaAtuacaoDomainService.RecuperarDescricaoVinculo(item.SeqIngressante, item.SeqsTipoTermoIntercambioAssociados, item.DescricaoVinculo);
                ret.Add(itemParsed);
            }
            return ret;
        }

        public IngressanteCabecalhoVO BuscarCabecalhoIngressantes(long seqIngressante)
        {
            var ret = new IngressanteCabecalhoVO();

            // Retorna a lista de ingressantes para Home
            var dadosBasicos = this.SearchProjectionByKey(new SMCSeqSpecification<Ingressante>(seqIngressante), x => new
            {
                //DescricaoCicloLetivo = x.CampanhaCicloLetivo.CicloLetivo.Descricao,
                //Cursos = x.Ofertas.Select(o => o.CampanhaOfertaItem.CursoOfertaLocalidadeTurno.CursoOfertaLocalidade.CursoOferta.Curso).ToList(),
                DescricaoEntidadeResponsavel = x.EntidadeResponsavel.Nome,
                DescricaoProcesso = x.SolicitacoesServico.OfType<SolicitacaoMatricula>().FirstOrDefault().ConfiguracaoProcesso.Processo.Descricao,
                DescricaoProcessoSeletivo = x.ProcessoSeletivo.Descricao,
                DescricaoVinculo = x.TipoVinculoAluno.Descricao,
                SeqEntidadeResponsavel = x.EntidadeResponsavel.Seq,
                SeqNivelEnsino = x.SeqNivelEnsino,
                SeqTipoVinculo = x.SeqTipoVinculoAluno,
                SeqInstituicao = x.Pessoa.SeqInstituicaoEnsino,
                DescricaoTipoOrientacao = x.OrientacoesPessoaAtuacao.FirstOrDefault().Orientacao.TipoOrientacao.Descricao,
                DadosOrientadores = x.OrientacoesPessoaAtuacao.FirstOrDefault().Orientacao.OrientacoesColaborador.OrderBy(o => o.TipoParticipacaoOrientacao).Select(o => new { NomeSocial = o.Colaborador.DadosPessoais.NomeSocial, Nome = o.Colaborador.DadosPessoais.Nome, TipoParticipacao = o.TipoParticipacaoOrientacao }),
                SeqsTipoTermoIntercambio = x.TermosIntercambio.Select(t => t.TermoIntercambio.ParceriaIntercambioTipoTermo.SeqTipoTermoIntercambio),
            });

            ret.DescricaoVinculo = PessoaAtuacaoDomainService.RecuperarDescricaoVinculo(seqIngressante, dadosBasicos.SeqsTipoTermoIntercambio, dadosBasicos.DescricaoVinculo);

            // Popula o ret
            //ret.DescricaoCicloLetivo = dadosBasicos.DescricaoCicloLetivo;
            ret.DescricaoEntidadeResponsavel = dadosBasicos.DescricaoEntidadeResponsavel;
            ret.DescricaoProcesso = dadosBasicos.DescricaoProcesso;
            ret.DescricaoProcessoSeletivo = dadosBasicos.DescricaoProcessoSeletivo;
            ret.NomeOrientador = string.Empty;

            if (dadosBasicos.DadosOrientadores != null)
            {
                foreach (var item in dadosBasicos.DadosOrientadores)
                {
                    if (ret.NomeOrientador.Length > 0)
                        ret.NomeOrientador += ", ";

                    if (!string.IsNullOrWhiteSpace(item.NomeSocial))
                        ret.NomeOrientador += item.NomeSocial;
                    else
                        ret.NomeOrientador += item.Nome;

                    ret.NomeOrientador += " (" + SMCEnumHelper.GetDescription(item.TipoParticipacao) + ")";
                }
            }

            ret.DescricaoTipoOrientacao = dadosBasicos.DescricaoTipoOrientacao;

            // NV06 Exibir campos apenas quando o vínculo da pessoa-atuação exigir associação de curso, de acordo com a parametrização por instituição e nível de ensino.
            var instituicaoNivelTipoVinculoAluno = InstituicaoNivelTipoVinculoAlunoDomainService.SearchByKey(
                new InstituicaoNivelTipoVinculoAlunoFilterSpecification { SeqNivelEnsino = dadosBasicos.SeqNivelEnsino, SeqTipoVinculoAluno = dadosBasicos.SeqTipoVinculo, SeqInstituicao = dadosBasicos.SeqInstituicao },
                IncludesInstituicaoNivelTipoVinculoAluno.InstituicaoNivel_NivelEnsino);

            if (instituicaoNivelTipoVinculoAluno != null && instituicaoNivelTipoVinculoAluno.ExigeCurso)
            {
                // RN_ORG_049 Exibir campo apenas para os níveis de ensino, de acordo com os tokens:
                //- DOUTORADO_ACADEMICO
                //- DOUTORADO_PROFISSIONAL
                //- MESTRADO_ACADEMICO
                //- MESTRADO_PROFISSIONAL

                if ((instituicaoNivelTipoVinculoAluno?.InstituicaoNivel?.NivelEnsino?.Token == TOKEN_NIVEL_ENSINO.DOUTORADO_ACADEMICO) ||
                    (instituicaoNivelTipoVinculoAluno?.InstituicaoNivel?.NivelEnsino?.Token == TOKEN_NIVEL_ENSINO.DOUTORADO_PROFISSIONAL) ||
                    (instituicaoNivelTipoVinculoAluno?.InstituicaoNivel?.NivelEnsino?.Token == TOKEN_NIVEL_ENSINO.MESTRADO_ACADEMICO) ||
                    (instituicaoNivelTipoVinculoAluno?.InstituicaoNivel?.NivelEnsino?.Token == TOKEN_NIVEL_ENSINO.MESTRADO_PROFISSIONAL))
                {
                    ret.ExibeFormacoesEspecificas = true;

                    // Retorna a lista de ingressantes para Home
                    var dadosComplexos = this.SearchProjectionByKey(new SMCSeqSpecification<Ingressante>(seqIngressante), x => new
                    {
                        NomesLocalidades = x.Ofertas.Select(o => o.CampanhaOfertaItem.CursoOfertaLocalidadeTurno.CursoOfertaLocalidade.HierarquiasEntidades.FirstOrDefault().ItemSuperior.Entidade.Nome),
                        DescricoesModalidades = x.Ofertas.Select(o => o.CampanhaOfertaItem.CursoOfertaLocalidadeTurno.CursoOfertaLocalidade.Modalidade.Descricao).ToList(),
                        DescricoesOfertas = x.Ofertas.Select(o => o.CampanhaOfertaItem.CursoOfertaLocalidadeTurno.CursoOfertaLocalidade.CursoOferta.Descricao).ToList(),
                        DescricoesTurnos = x.Ofertas.Select(o => o.CampanhaOfertaItem.CursoOfertaLocalidadeTurno.Turno.Descricao).ToList(),
                        SeqsFormacoesEspecificas = x.FormacoesEspecificas.Select(f => f.SeqFormacaoEspecifica),
                        SeqEntidadeResponsavelFormacaoEspecifica = x.FormacoesEspecificas.Select(f => f.FormacaoEspecifica.SeqEntidadeResponsavel).FirstOrDefault(),
                    });

                    if (dadosComplexos != null)
                    {
                        List<FormacaoEspecificaNodeVO> formacoesHierarquia = new List<FormacaoEspecificaNodeVO>();

                        if (dadosComplexos.SeqEntidadeResponsavelFormacaoEspecifica.HasValue)
                        {
                            var listaSeqsEntidadesResponsaveis = new List<long>();
                            listaSeqsEntidadesResponsaveis.Add(dadosComplexos.SeqEntidadeResponsavelFormacaoEspecifica.Value);
                            var formacoes = FormacaoEspecificaDomainService.BuscarFormacoesEspecificas(new FormacaoEspecificaFiltroVO { SeqsEntidadesResponsaveis = listaSeqsEntidadesResponsaveis.ToArray() });

                            if (formacoes != null)
                                foreach (var formacao in dadosComplexos.SeqsFormacoesEspecificas)
                                    formacoesHierarquia.AddRange(FormacaoEspecificaDomainService.GerarHierarquiaFormacaoEspecifica(formacao, formacoes));
                        }

                        //ret.DescricaoCurso = string.Join(", ", dadosComplexos?.Cursos?.Select(c => c.Nome)?.Distinct()?.ToArray());
                        ret.DescricaoLocalidade = string.Join(", ", dadosComplexos?.NomesLocalidades?.ToArray());
                        ret.DescricaoModalidade = string.Join(", ", dadosComplexos?.DescricoesModalidades?.ToArray());
                        ret.DescricaoOferta = string.Join(", ", dadosComplexos?.DescricoesOfertas?.ToArray());
                        ret.DescricaoTurno = string.Join(", ", dadosComplexos?.DescricoesTurnos?.ToArray());
                        ret.ExigeCurso = true;
                        ret.FormacoesEspecificas = formacoesHierarquia.Select(f => new FormacoesEspecificasSolicitacaoMatriculaVO { DescricaoTipoFormacaoEspecifica = f.DescricaoTipoFormacaoEspecifica, DescricoesFormacoesEspecificas = f.Descricao }).ToList();
                    }
                }
            }

            return ret;
        }

        /// <summary>
        /// Busca os sequenciais de processo e matriz curricular do ingressante
        /// </summary>
        /// <param name="seqIngressante">Sequencial do ingressante</param>
        /// <param name="desabilitarFiltro">Desabilita o filtro de HIERARQUIA_ENTIDADE_ORGANIZACIONAL</param>
        /// <returns></returns>
        public IngressanteTurmaVO BuscarIngressanteMatrizOferta(long seqIngressante, bool desabilitarFiltro = false)
        {
            if (desabilitarFiltro)
            {
                this.DisableFilter(FILTER.HIERARQUIA_ENTIDADE_ORGANIZACIONAL);
            }

            var ingressanteMatrizOferta = SearchProjectionByKey(new SMCSeqSpecification<Ingressante>(seqIngressante), p => new IngressanteTurmaVO()
            {
                Seq = p.Seq,
                SeqProcessoSeletivo = p.SeqProcessoSeletivo,
                SeqMatrizCurricularOferta = p.SeqMatrizCurricularOferta,
                TipoAtuacao = p.TipoAtuacao
            });

            if (desabilitarFiltro)
            {
                this.EnableFilter(FILTER.HIERARQUIA_ENTIDADE_ORGANIZACIONAL);
            }

            return ingressanteMatrizOferta;
        }

        /// <summary>
        /// Busca os ingressantes com as depêndencias apresentadas na listagem do seu cadastro
        /// </summary>
        /// <param name="filtro">Filtros do ingressante</param>
        /// <returns>Dados paginados de ingressante</returns>
        public SMCPagerData<IngressanteListaVO> BuscarIngressantes(IngressanteFilterSpecification filtro)
        {
            int total = 0;
            filtro.SetOrderBy(o => (o.DadosPessoais.NomeSocial ?? "") + o.DadosPessoais.Nome);
            filtro.SetOrderByDescending(o => o.CampanhaCicloLetivo.CicloLetivo.AnoNumeroCicloLetivo);
            var ingressentes = SearchProjectionBySpecification(filtro, p => new IngressanteListaVO()
            {
                // Dados pessoais
                Seq = p.Seq,
                Nome = p.DadosPessoais.Nome,
                NomeSocial = p.DadosPessoais.NomeSocial,
                Cpf = p.Pessoa.Cpf,
                NumeroPassaporte = p.Pessoa.NumeroPassaporte,
                DataNascimento = p.Pessoa.DataNascimento,
                SituacaoIngressante = p.HistoricosSituacao.OrderByDescending(x => x.Seq).FirstOrDefault().SituacaoIngressante,
                OrigemIngressante = p.OrigemIngressante,
                Falecido = p.Pessoa.Falecido,

                // Dados academicos
                SeqCurso = p.Ofertas.FirstOrDefault().CampanhaOferta.Itens.FirstOrDefault().CursoOfertaLocalidadeTurno.CursoOfertaLocalidade.CursoOferta.SeqCurso,
                SeqFormacaoEspecifica = p.FormacoesEspecificas.FirstOrDefault().SeqFormacaoEspecificaOrigem,
                SeqNivelEnsino = p.SeqNivelEnsino,
                DescricaoNivelEnsino = p.NivelEnsino.Descricao,
                SeqTipoVinculoAluno = p.SeqTipoVinculoAluno,
                Vinculo = p.TipoVinculoAluno.Descricao,
                TermoIntercambio = p.TermosIntercambio.FirstOrDefault().TermoIntercambio,
                DescricaoTipoTermoIntercambio = p.TermosIntercambio.FirstOrDefault().TermoIntercambio.ParceriaIntercambioTipoTermo.TipoTermoIntercambio.Descricao,
                SeqTipoTermoIntercambio = p.TermosIntercambio.FirstOrDefault().TermoIntercambio.ParceriaIntercambioTipoTermo.SeqTipoTermoIntercambio,
                DataInicioTermoIntercambio = p.TermosIntercambio.Min(mt => mt.TermoIntercambio.Vigencias.Min(mv => mv.DataInicio)),
                DataFimTermoIntercambio = p.TermosIntercambio.Max(mt => mt.TermoIntercambio.Vigencias.Max(mv => mv.DataFim)),
                DescricaoInstituicaoExterna = p.TermosIntercambio.FirstOrDefault().TermoIntercambio.ParceriaIntercambioInstituicaoExterna.InstituicaoExterna.Nome,
                OfertasCampanha = p.Ofertas.Select(s => s.CampanhaOferta.Descricao),
            }, out total).ToList();

            // Recupera os cursos dos ingressantes selecionados
            var specCursosIngressantes = new SMCContainsSpecification<Curso, long>(p => p.Seq,
                ingressentes.Where(w => w.SeqCurso.HasValue).Select(s => s.SeqCurso.Value).ToArray());
            var cursosIngressantes = this.CursoDomainService.SearchProjectionBySpecification(specCursosIngressantes, p => new
            {
                SeqCurso = p.Seq,
                p.SeqNivelEnsino,
                SeqTipoEntidadeSuperior = p.HierarquiasEntidades.FirstOrDefault().ItemSuperior.Entidade.SeqTipoEntidade,
                FormacoesEspecificasPais = p.HierarquiasEntidades.FirstOrDefault().ItemSuperior.Entidade.FormacoesEspecificasEntidade,
                SeqsEntidadesSuperiores = p.HierarquiasEntidades.Select(h => h.ItemSuperior.SeqEntidade),
            }).ToList();

            var specConfigNivelEnsino = new SMCContainsSpecification<InstituicaoNivelTipoVinculoAluno, long>(p => p.InstituicaoNivel.SeqNivelEnsino, cursosIngressantes.Select(s => s.SeqNivelEnsino).Distinct().ToArray());
            var specConfigVinculo = new SMCContainsSpecification<InstituicaoNivelTipoVinculoAluno, long>(p => p.SeqTipoVinculoAluno, ingressentes.Select(s => s.SeqTipoVinculoAluno).Distinct().ToArray());
            var specConfigVinculos = new SMCAndSpecification<InstituicaoNivelTipoVinculoAluno>(specConfigNivelEnsino, specConfigVinculo);
            var configuracoesVinculos = this.InstituicaoNivelTipoVinculoAlunoDomainService.SearchBySpecification(specConfigVinculos, IncludesInstituicaoNivelTipoVinculoAluno.InstituicaoNivel_NivelEnsino | IncludesInstituicaoNivelTipoVinculoAluno.TiposTermoIntercambio_TipoTermoIntercambio).ToList();

            // Recupera as formações específicas dos cursos dos ingressantes com hierarquia
            var formacoesCursos = new Dictionary<long, List<FormacaoEspecificaListaVO>>();
            cursosIngressantes
                .SMCForEach(f => formacoesCursos.Add(f.SeqCurso, FormacaoEspecificaDomainService.BuscarFormacoesEspecificasCursoComHierarquia(f.SeqCurso)));

            // Recupera as hierarquias de tipos de formação para as entidades responsáveis pelos cursos dos ingressantes
            var hierarquiasTipoFormacao = new Dictionary<long, List<InstituicaoTipoEntidadeFormacaoEspecificaVO>>();
            cursosIngressantes
                .Select(s => s.SeqTipoEntidadeSuperior)
                .Distinct()
                .SMCForEach(f => hierarquiasTipoFormacao.Add(f, InstituicaoTipoEntidadeFormacaoEspecificaDomainService.BuscarTipoFormacaoPorTipoEntidade(f)));

            foreach (var ingressante in ingressentes)
            {
                // RN_PES_023 - Nome e Nome Social - Visão Administrativo
                ingressante.Nome = string.IsNullOrEmpty(ingressante.NomeSocial) ? ingressante.Nome : $"{ingressante.NomeSocial} ({ingressante.Nome})";

                ingressante.PossuiSituacaoImpeditivaIngressante = ValidarSituacaoImpeditivaIngressante(ingressante.Seq);
                ingressante.NaoExigeOfertaMatrizCurricular = ValidarExigenciaOfertaMatrizCurricular(ingressante.Seq);
                ingressante.NaoPossuiVinculoAssociacaoOrientador = ValidarVinculoAssociacaoOrientador(ingressante.Seq);
                ingressante.NaoPermiteAssociacaoOrientador = ValidarPermissaoAssociacaoOrientador(ingressante.Seq);
                ingressante.ExibirLiberacaoMaricula = ingressante.OrigemIngressante == OrigemIngressante.Manual;
                ingressante.PermitirLiberacaoMatricula = ingressante.SituacaoIngressante == SituacaoIngressante.AguardandoLiberacaMatricula;

                ingressante.NumeroPassaporte = string.IsNullOrEmpty(ingressante.NumeroPassaporte) ? "-" : ingressante.NumeroPassaporte;

                if (cursosIngressantes.Any(a => a.SeqCurso == ingressante.SeqCurso))
                {
                    var formacoesCurso = cursosIngressantes.First(c => c.SeqCurso == ingressante.SeqCurso.GetValueOrDefault()).FormacoesEspecificasPais;

                    var infoCursoIngressante = cursosIngressantes.First(f => f.SeqCurso == ingressante.SeqCurso.Value);
                    ingressante.SeqNivelEnsino = infoCursoIngressante.SeqNivelEnsino;

                    // RN_ALN_029 - Descrição Vínculo Tipo Termo Intercâmbio
                    var configuracaoVinculo = configuracoesVinculos.FirstOrDefault(f => f.InstituicaoNivel.SeqNivelEnsino == ingressante.SeqNivelEnsino && f.SeqTipoVinculoAluno == ingressante.SeqTipoVinculoAluno);
                    if (configuracaoVinculo != null)
                    {
                        var configuracaoTipoTermoIntercambio = configuracaoVinculo.TiposTermoIntercambio.FirstOrDefault(f => f.SeqTipoTermoIntercambio == (ingressante.SeqTipoTermoIntercambio ?? 0));
                        if (  // RN_ALN_031  Consistência Vínculo Tipo Termo Intercâmbio
                              (configuracaoVinculo.ExigeParceriaIntercambioIngresso || (configuracaoTipoTermoIntercambio?.ConcedeFormacao ?? false))
                              // E tenha dados de intercâmbio para evitar [Descricao] - []
                              && ingressante.DescricaoTipoTermoIntercambio != null
                           )
                        {
                            ingressante.Vinculo = $"{ingressante.Vinculo} - {ingressante.DescricaoTipoTermoIntercambio}";
                        }

                        ingressante.VinculoInstituicaoNivelEnsinoExigeCurso = configuracaoVinculo.ExigeCurso;
                        ingressante.TipoVinculoAlunoExigeCurso = configuracaoVinculo.ExigeCurso;
                        ingressante.ExigePeriodoIntercambioTermo = configuracaoTipoTermoIntercambio?.ExigePeriodoIntercambioTermo ?? false;
                    }
                }
            }

            return new SMCPagerData<IngressanteListaVO>(ingressentes, total);
        }

        /// <summary>
        /// Busca os dados acadêmicos de um ingressante
        /// </summary>
        /// <param name="seq">Sequencial do ingressante</param>
        /// <returns>Dados paginados de ingressante</returns>
        public IngressanteListaVO BuscarDadosAcademicosIngressante(long seq)
        {
            var ingressante = SearchProjectionByKey(new SMCSeqSpecification<Ingressante>(seq), p => new IngressanteListaVO()
            {
                // Dados pessoais
                Seq = p.Seq,
                Nome = p.DadosPessoais.Nome,
                NomeSocial = p.DadosPessoais.NomeSocial,
                Cpf = p.Pessoa.Cpf,
                NumeroPassaporte = p.Pessoa.NumeroPassaporte,
                DataNascimento = p.Pessoa.DataNascimento,
                SituacaoIngressante = p.HistoricosSituacao.OrderByDescending(x => x.Seq).FirstOrDefault().SituacaoIngressante,
                OrigemIngressante = p.OrigemIngressante,
                Falecido = p.Pessoa.Falecido,

                // Dados academicos
                SeqCurso = p.Ofertas.FirstOrDefault().CampanhaOferta.Itens.FirstOrDefault().CursoOfertaLocalidadeTurno.CursoOfertaLocalidade.CursoOferta.SeqCurso,
                SeqFormacaoEspecifica = p.FormacoesEspecificas.FirstOrDefault().SeqFormacaoEspecificaOrigem,
                SeqNivelEnsino = p.SeqNivelEnsino,
                Campanha = p.CampanhaCicloLetivo.Campanha.Descricao,
                ProcessoSeletivo = p.ProcessoSeletivo.Descricao,
                GrupoEscalonamento = p.SolicitacoesServico.FirstOrDefault().GrupoEscalonamento.Descricao,
                SeqTipoVinculoAluno = p.SeqTipoVinculoAluno,
                Vinculo = p.TipoVinculoAluno.Descricao,
                TermoIntercambio = p.TermosIntercambio.FirstOrDefault().TermoIntercambio,
                SeqTipoTermoIntercambio = p.TermosIntercambio.FirstOrDefault().TermoIntercambio.ParceriaIntercambioTipoTermo.SeqTipoTermoIntercambio,
                DescricaoTipoTermoIntercambio = p.TermosIntercambio.FirstOrDefault().TermoIntercambio.ParceriaIntercambioTipoTermo.TipoTermoIntercambio.Descricao,
                DataInicioTermoIntercambio = p.TermosIntercambio.FirstOrDefault().TermoIntercambio.Vigencias.Min(m => m.DataInicio),
                DataFimTermoIntercambio = p.TermosIntercambio.FirstOrDefault().TermoIntercambio.Vigencias.Max(m => m.DataFim),
                DescricaoInstituicaoExterna = p.TermosIntercambio.FirstOrDefault().TermoIntercambio.ParceriaIntercambioInstituicaoExterna.InstituicaoExterna.Nome,
                FormaIngresso = p.FormaIngresso.Descricao,
                OfertasCampanha = p.Ofertas.Select(s => s.CampanhaOferta.Descricao),
                MatrizCurricularOferta = p.MatrizCurricularOferta.MatrizCurricular.Descricao,
                FormacoesEspecificasIngressante = p.FormacoesEspecificas.Select(s => s.SeqFormacaoEspecifica),
                TipoOrientacao = p.OrientacoesPessoaAtuacao.FirstOrDefault().Orientacao.TipoOrientacao.Descricao,
                OrientadoresIngressante = p.OrientacoesPessoaAtuacao.SelectMany(s => s.Orientacao.OrientacoesColaborador.Select(o => new IngressanteOrientacaoVO()
                {
                    TipoParticipacaoOrientacao = o.TipoParticipacaoOrientacao,
                    NomeColaborador = o.Colaborador.DadosPessoais.Nome
                })),
                DescricaoInstituicaoTransferenciaExterna = p.InstituicaoTransferenciaExterna.Nome,
                CursoTransferenciaExterna = p.CursoTransferenciaExterna
            });

            var configuracaoVinculo = this.InstituicaoNivelTipoVinculoAlunoDomainService
                .BuscarConfiguracaoVinculo(ingressante.SeqNivelEnsino, ingressante.SeqTipoVinculoAluno);

            // RN_PES_023 - Nome e Nome Social - Visão Administrativo
            ingressante.Nome = string.IsNullOrEmpty(ingressante.NomeSocial) ? ingressante.Nome : $"{ingressante.NomeSocial} ({ingressante.Nome})";

            ingressante.NumeroPassaporte = string.IsNullOrEmpty(ingressante.NumeroPassaporte) ? "-" : ingressante.NumeroPassaporte;

            if (ingressante.SeqCurso.HasValue)
            {
                var specCursoIngressante = new SMCSeqSpecification<Curso>(ingressante.SeqCurso.GetValueOrDefault());
                var cursoIngressante = this.CursoDomainService.SearchProjectionByKey(specCursoIngressante, p => new
                {
                    SeqCurso = p.Seq,
                    SeqNivelEnsino = p.SeqNivelEnsino,
                    SeqTipoEntidadeSuperior = p.HierarquiasEntidades.FirstOrDefault().ItemSuperior.Entidade.SeqTipoEntidade,
                    FormacoesEspecificasPais = p.HierarquiasEntidades.FirstOrDefault().ItemSuperior.Entidade.FormacoesEspecificasEntidade,
                    SeqsEntidadesSuperiores = p.HierarquiasEntidades.Select(h => h.ItemSuperior.SeqEntidade),
                });

                var specFormacoes = new FormacaoEspecificaFilterSpecification() { SeqEntidades = cursoIngressante.SeqsEntidadesSuperiores.ToList() };
                var formacoesIngressante = this.FormacaoEspecificaDomainService
                    .SearchBySpecification(specFormacoes, IncludesFormacaoEspecifica.TipoFormacaoEspecifica)
                    .ToList();

                // RN_ALN_029 - Descrição Vínculo Tipo Termo Intercâmbio
                if (configuracaoVinculo != null)
                {
                    var configuracaoTipoTermoIntercambio = configuracaoVinculo.TiposTermoIntercambio.FirstOrDefault(f => f.SeqTipoTermoIntercambio == ingressante.SeqTipoTermoIntercambio);
                    if (  // RN_ALN_031  Consistência Vínculo Tipo Termo Intercâmbio
                          (configuracaoVinculo.ExigeParceriaIntercambioIngresso || (configuracaoTipoTermoIntercambio?.ConcedeFormacao ?? false))
                          // E tenha dados de intercâmbio para evitar [Descricao] - []
                          && ingressante.DescricaoTipoTermoIntercambio != null
                       )
                    {
                        ingressante.Vinculo = $"{ingressante.Vinculo} - {ingressante.DescricaoTipoTermoIntercambio}";
                    }

                    // NV7 (não deve exibir conforme a configuração de vínculo
                    if (!configuracaoVinculo.ExigeParceriaIntercambioIngresso)
                        ingressante.DescricaoInstituicaoExterna = null;

                    ingressante.TipoVinculoAlunoExigeCurso = configuracaoVinculo.ExigeCurso;
                    ingressante.ExigePeriodoIntercambioTermo = configuracaoTipoTermoIntercambio?.ExigePeriodoIntercambioTermo ?? false;
                }

                foreach (var formacaoIngressante in ingressante.FormacoesEspecificasIngressante)
                {
                    var hierarquiaFormacaoIngressante = this.FormacaoEspecificaDomainService.GerarHierarquiaFormacaoEspecifica(formacaoIngressante, formacoesIngressante);

                    if (ingressante.FormacoesEspecificas == null)
                        ingressante.FormacoesEspecificas = new List<string>();
                    hierarquiaFormacaoIngressante.SMCForEach(x => ingressante.FormacoesEspecificas.Add($"[{x.DescricaoTipoFormacaoEspecifica}] {x.Descricao}"));
                }
            }

            // RN_ALN_021 Exibição orientador pessoa-atuação
            ingressante.Orientadores = ingressante.OrientadoresIngressante.OrderBy(o => o.NomeColaborador).Select(s => $"{s.NomeColaborador} - {SMCEnumHelper.GetDescription(s.TipoParticipacaoOrientacao)}");

            return ingressante;
        }

        public long SalvarIngressante(IngressanteVO ingressanteVO)
        {
            PessoaDomainService.FormatarNomesPessoaVo(ref ingressanteVO);

            var configHelper = CriarVOHelper(ingressanteVO);

            // RN_CAM_054.3
            // RN_ALN_005.5
            if (configHelper.ExigeCurso)
            {
                ingressanteVO.FormacoesEspecificas = BuscarFormacoesEspecificasIngressante(configHelper, ingressanteVO);
            }

            // RN_CAM_054 (Apenas a validação)
            VerificarQuantidadeOfertas(ingressanteVO, configHelper);

            // RN_ALN_036
            VerificarQuantidadeDeVagasPorOferta(ingressanteVO, configHelper);

            var ingressante = TransformaIngressante(ingressanteVO);
            if (configHelper.ExigeCurso)
            {
                var seqCursoOfertaLocalidadeTurno = CampanhaOfertaDomainService.SearchProjectionByKey(new SMCSeqSpecification<CampanhaOferta>(ingressanteVO.Ofertas.First().SeqCampanhaOferta),
                                                        x => x.Itens.FirstOrDefault().SeqCursoOfertaLocalidadeTurno).Value;

                // Busca [Oferta de Curso]  + "-" + [Localidade] + "-" + [Turno]
                ingressante.Descricao = CursoOfertaLocalidadeTurnoDomainService.SearchProjectionByKey(new SMCSeqSpecification<CursoOfertaLocalidadeTurno>(seqCursoOfertaLocalidadeTurno),
                                    x => x.CursoOfertaLocalidade.CursoOferta.Descricao + " - " +
                                         x.CursoOfertaLocalidade.HierarquiasEntidades.FirstOrDefault().ItemSuperior.Entidade.Nome + " - " +
                                         x.Turno.Descricao + " - ");

                //[Oferta de Curso]  + "-" + [Localidade] + "-" + [Turno]  "-" + [Ciclo letivo ingresso]
                ingressante.Descricao += CampanhaCicloLetivoDomainService.SearchProjectionByKey(new SMCSeqSpecification<CampanhaCicloLetivo>(ingressante.SeqCampanhaCicloLetivo),
                                    x => x.CicloLetivo.Descricao);
            }
            else
            {
                //[Entidade Responsável] + "-" + [Nível de Ensino]
                ingressante.Descricao = CampanhaOfertaDomainService.SearchProjectionByKey(new SMCSeqSpecification<CampanhaOferta>(ingressanteVO.Ofertas.First().SeqCampanhaOferta),
                                                        x => x.Campanha.EntidadeResponsavel.Nome + " - ");

                ingressante.Descricao += NivelEnsinoDomainService.SearchProjectionByKey(new SMCSeqSpecification<NivelEnsino>(ingressante.SeqNivelEnsino),
                                                        x => x.Descricao + " - ");

                //[Entidade Responsável] + "-" + [Nível de Ensino] + "-" [Ciclo letivo ingresso]
                ingressante.Descricao += CampanhaCicloLetivoDomainService.SearchProjectionByKey(new SMCSeqSpecification<CampanhaCicloLetivo>(ingressante.SeqCampanhaCicloLetivo),
                                    x => x.CicloLetivo.Descricao);
            }
            ingressanteVO.Descricao = ingressante.Descricao;

            using (var unitOfWork = SMCUnitOfWork.Begin())
            {
                // Criação do usuário do SAS
                if (!ingressanteVO.SeqUsuarioSAS.HasValue)
                {
                    ingressante.Pessoa.SeqUsuarioSAS = CriarUsuarioSas(ingressante);
                    ingressanteVO.SeqUsuarioSAS = ingressante.Pessoa.SeqUsuarioSAS;
                    PessoaDomainService.UpdateFields(ingressante.Pessoa, f => f.SeqUsuarioSAS);
                }

                long seqDadosPessoais = 0;
                foreach (var dadospessoais in ingressante.Pessoa.DadosPessoais)
                {
                    if (SMCReflectionHelper.CompareExistingPrimitivePropertyValues(dadospessoais, ingressanteVO, false, IgnoredProperties)
                    && ingressanteVO.ArquivoFoto?.State == SMCUploadFileState.Unchanged)
                    {
                        seqDadosPessoais = dadospessoais.Seq;
                        break;
                    }
                }
                // Verifica se houve alterações nos dados da pessoa. Se sim, será gerado um novo registro.
                if (seqDadosPessoais == 0)
                {
                    var dadosPessoais = SMCMapperHelper.Create<PessoaDadosPessoais>(ingressanteVO);
                    dadosPessoais.Seq = 0;
                    dadosPessoais.SeqPessoa = ingressante.SeqPessoa;
                    // Para gravar corretamente o arquivo da foto
                    if (dadosPessoais.ArquivoFoto?.State == SMCUploadFileState.Unchanged)
                        dadosPessoais.ArquivoFoto = null;
                    else
                        this.EnsureFileIntegrity(dadosPessoais, i => i.SeqArquivoFoto, i => i.ArquivoFoto);
                    PessoaDadosPessoaisDomainService.SaveEntity(dadosPessoais);
                    seqDadosPessoais = dadosPessoais.Seq;
                }
                ingressante.SeqPessoaDadosPessoais = seqDadosPessoais;

                // Calcula as datas de admissão e conclusão. Como o seqMatrizCurricularOferta oferta precisa da data de admissão para ser gerado e
                // a data de conclusão pode precisar do seqMatrizCurricularOferta, ambos são gerados no mesmo método.
                var (dataAdmissao, dataTermino, seqMatrizCurricularOferta) = BuscarDatasEMatrizCurricularOferta(ingressanteVO, configHelper);
                ingressante.DataAdmissao = dataAdmissao;
                ingressante.DataPrevisaoConclusao = dataTermino;
                ingressante.SeqMatrizCurricularOferta = seqMatrizCurricularOferta;

                ingressante.OrientacoesPessoaAtuacao = new List<OrientacaoPessoaAtuacao>();
                // RN_CAM_051
                if (ingressanteVO.SeqOrientador.HasValue)
                {
                    ingressante.OrientacoesPessoaAtuacao.Add(new OrientacaoPessoaAtuacao
                    {
                        SeqOrientacao = CriaOrientacaoPessoaAtuacao(ingressanteVO)
                    });
                }

                // RN_ALN_046
                if (configHelper.ExigeParceriaIntercambioIngresso && ingressanteVO.TermosIntercambio.SMCCount() > 0)
                {
                    var specTermos = new SMCContainsSpecification<TermoIntercambio, long>(p => p.Seq, ingressanteVO.TermosIntercambio.Select(s => s.SeqTermoIntercambio).ToArray());
                    var tiposTermos = TermoIntercambioDomainService.SearchProjectionBySpecification(specTermos, p => new { p.Seq, p.ParceriaIntercambioTipoTermo.SeqTipoTermoIntercambio }).ToList();
                    foreach (var termoVO in ingressanteVO.TermosIntercambio)
                    {
                        var seqTipoTermo = tiposTermos.First(f => f.Seq == termoVO.SeqTermoIntercambio).SeqTipoTermoIntercambio;
                        var termo = ingressante.TermosIntercambio.First(f => f.SeqTermoIntercambio == termoVO.SeqTermoIntercambio);
                        // RN_ALN_005.13
                        termo.Ativo = true;
                        termo.TipoMobilidade = TipoMobilidade.IngressoEmNossaInstituicao;
                        if (termoVO.OrientacaoParticipacoesColaboradores != null && termoVO.OrientacaoParticipacoesColaboradores.Count > 0)
                        {
                            var seqOrientacao = CriaOrientacoesPessoaAtuacaoIntercambio(ingressanteVO, termoVO, seqTipoTermo);
                            ingressante.OrientacoesPessoaAtuacao.Add(new OrientacaoPessoaAtuacao()
                            {
                                SeqOrientacao = seqOrientacao
                            });
                            termo.SeqOrientacao = seqOrientacao;
                        }
                        if (configHelper.TiposTermoIntercambio.First(f => f.SeqTipoParceriaIntercambio == seqTipoTermo).ExigePeriodoIngresso)
                        {
                            if (termo.Periodos == null)
                            {
                                termo.Periodos = new List<PeriodoIntercambio>();
                            }
                            termo.Periodos.Add(new PeriodoIntercambio()
                            {
                                DataInicio = termoVO.DataInicio.GetValueOrDefault(),
                                DataFim = termoVO.DataFim.GetValueOrDefault()
                            });
                        }
                    }
                }

                if (ingressanteVO.OrientacaoParticipacoesColaboradores.SMCAny())
                {
                    ingressante.OrientacoesPessoaAtuacao.Add(new OrientacaoPessoaAtuacao
                    {
                        SeqOrientacao = CriaOrientacaoPessoaAtuacao(ingressanteVO, ingressanteVO.OrientacaoParticipacoesColaboradores, ingressanteVO.SeqTipoOrientacao.GetValueOrDefault())
                    });
                }

                List<long> seqsDivisaoTurmasAssociadas = null;
                // RN_ALN_045 - Ocupar/Desocupar vaga oferta
                if (!configHelper.ExigeCurso)
                {
                    seqsDivisaoTurmasAssociadas = AtualizarQuantidadeVagasPorOferta(ingressante);
                }

                // Remove referências para evitar conflitos com o *diff
                ingressante.EnderecosEletronicos.SMCForEach(f => f.EnderecoEletronico = null);
                ingressante.TermosIntercambio.SMCForEach(f =>
                {
                    f.TermoIntercambio = null;
                    f.Orientacao = null;
                });
                // Limpa o objeto de dados pessoais para evitar conflitos com o *diff.
                ingressante.DadosPessoais = null;
                ingressante.Pessoa = null;

                SaveEntity(ingressante);

                if (ingressanteVO.Seq == 0)
                {
                    // Atualiza o seq do ingressante
                    ingressanteVO.Seq = ingressante.Seq;

                    // RN_ALN_017
                    SalvarSolicitacaoMatricula(ingressanteVO, configHelper, seqsDivisaoTurmasAssociadas);

                    // Inclur o ingressante no papel correto no SAS
                    /*AplicacaoService.IncluirParticipantePapelAplicacaoToken(SIGLA_APLICACAO.SGA_ALUNO,
                                                                            TOKENS_PAPEIS_APLICACAO.OPERACIONAL_INGRESSANTE,
                                                                            ingressanteVO.SeqUsuarioSAS.Value);*/
                    // Inclui o ingressante no perfil de ingressantes do sas
                    PerfilService.IncluirParticipantePerfilAplicacaoToken(SIGLA_APLICACAO.SGA_ALUNO,
                                                                            TOKENS_PERFIS_APLICACAO.INGRESSANTE,
                                                                            ingressanteVO.SeqUsuarioSAS.Value);
                }
                else
                {
                    AtualizarSolicitacaoMatricula(ingressanteVO, configHelper, seqsDivisaoTurmasAssociadas);
                }

                AtualizarDadosPessoa(ingressanteVO);

                // Chama serviço de inserção dos dados mestres
                ingressanteVO.Cpf = ingressanteVO.Cpf.SMCRemoveNonDigits();
                PessoaAtuacaoDomainService.SalvarIngressanteDadosMestres(ingressanteVO);

                unitOfWork.Commit();

                return ingressante.Seq;
            }
        }

        private void AtualizarDadosPessoa(IngressanteVO ingressanteVO)
        {
            var pessoa = this.PessoaDomainService.SearchByKey(new SMCSeqSpecification<Pessoa>(ingressanteVO.SeqPessoa));

            //Atualização dos campos de pessoa
            pessoa.SeqInstituicaoEnsino = ingressanteVO.SeqInstituicaoEnsino;
            pessoa.SeqUsuarioSAS = ingressanteVO.SeqUsuarioSAS;
            pessoa.Cpf = ingressanteVO.Cpf;
            pessoa.NumeroPassaporte = ingressanteVO.NumeroPassaporte;
            pessoa.CodigoPaisEmissaoPassaporte = ingressanteVO.CodigoPaisEmissaoPassaporte;
            pessoa.DataValidadePassaporte = ingressanteVO.DataValidadePassaporte;
            pessoa.DataNascimento = ingressanteVO.DataNascimento;
            pessoa.Falecido = ingressanteVO.Falecido;
            pessoa.TipoNacionalidade = ingressanteVO.TipoNacionalidade;
            pessoa.CodigoPaisNacionalidade = ingressanteVO.CodigoPaisNacionalidade;
            pessoa.Filiacao = ingressanteVO.Filiacao.TransformList<PessoaFiliacao>();

            this.PessoaDomainService.SalvarPessoa(pessoa);
        }

        private long? BuscarMatricCurricularOferta(long seqCampanhaOferta, DateTime dataAdmissao, bool exigeCurso)
        {
            if (exigeCurso)
            {
                var campanhaOferta = CampanhaOfertaDomainService.SearchProjectionByKey(new SMCSeqSpecification<CampanhaOferta>(seqCampanhaOferta),
                                                    x => new
                                                    {
                                                        x.TipoOferta.ExigeCursoOfertaLocalidadeTurno,
                                                        Itens = x.Itens,
                                                        MatrizCurricularOferta = x.Itens.FirstOrDefault().CursoOfertaLocalidadeTurno.MatrizesCurriculares
                                                            .FirstOrDefault(f => f.HistoricosSituacao.Any(g => g.SituacaoMatrizCurricularOferta == SituacaoMatrizCurricularOferta.Ativa
                                                                            && g.DataInicio <= dataAdmissao
                                                                            && (!g.DataFim.HasValue || dataAdmissao <= g.DataFim.Value))),
                                                        DescricaoEntidade = x.Itens.FirstOrDefault().CursoOfertaLocalidadeTurno.CursoOfertaLocalidade.Nome
                                                    });
                if (campanhaOferta.ExigeCursoOfertaLocalidadeTurno && campanhaOferta.Itens.Count == 1 && campanhaOferta.MatrizCurricularOferta != null)
                {
                    return campanhaOferta.MatrizCurricularOferta.Seq;
                }
                throw new MatrizCurriculaOfertaInexistenteException(campanhaOferta.DescricaoEntidade, dataAdmissao);
            }

            return null;
        }

        public IngressanteConfigVO CriarVOHelper(IngressanteVO ingressanteVO)
        {
            var paramSpec = new InstituicaoNivelTipoVinculoAlunoFilterSpecification()
            {
                SeqTipoVinculoAluno = ingressanteVO.SeqTipoVinculoAluno,
                SeqNivelEnsino = ingressanteVO.SeqNivelEnsino,
                SeqInstituicao = ingressanteVO.SeqInstituicaoEnsino
            };
            var vo = InstituicaoNivelTipoVinculoAlunoDomainService.SearchProjectionByKey(paramSpec,
                                                    x => new IngressanteConfigVO()
                                                    {
                                                        QuantidadeOfertaCampanhaIngresso = x.QuantidadeOfertaCampanhaIngresso,
                                                        DescricaoVinculo = x.TipoVinculoAluno.Descricao,
                                                        ExigeCurso = x.ExigeCurso,
                                                        ExigeParceriaIntercambioIngresso = x.ExigeParceriaIntercambioIngresso,
                                                        TiposTermoIntercambio = x.TiposTermoIntercambio.Select(s => new IngressanteTiposTermoIntercambioConfigVO()
                                                        {
                                                            SeqTipoParceriaIntercambio = s.SeqTipoTermoIntercambio,
                                                            ExigePeriodoIngresso = s.ExigePeriodoIntercambioTermo
                                                        }).ToList()
                                                    });

            var specProcesso = new SMCSeqSpecification<Processo>(ingressanteVO.SeqProcesso);
            if (ingressanteVO.SeqProcesso == 0)
            {
                throw new SMCApplicationException("Não foi possível encontrar um processo para o Campanha Ciclo Letivo e Processo Seletivo informados.");
            }
            var processo = ProcessoDomainService.SearchProjectionByKey(specProcesso, x => new { x.Descricao });
            vo.DescricaoProcesso = processo.Descricao;

            if (vo.ExigeCurso)
            {
                vo.SeqCurso = CampanhaOfertaDomainService.SearchProjectionByKey(new SMCSeqSpecification<CampanhaOferta>(ingressanteVO.Ofertas.First().SeqCampanhaOferta),
                        p => p.Itens.FirstOrDefault()
                            .CursoOfertaLocalidadeTurno
                            .CursoOfertaLocalidade
                            .CursoOferta.SeqCurso);
            }

            return vo;
        }

        private long CriarUsuarioSas(Ingressante ingressante)
        {
            var usuarioSas = new UsuarioData()
            {
                Nome = ingressante.DadosPessoais.Nome,
                NomeSocial = ingressante.DadosPessoais.NomeSocial,
                NomeMae = ingressante.Pessoa.Filiacao?.FirstOrDefault(f => f.TipoParentesco == TipoParentesco.Mae)?.Nome,
                Cpf = ingressante.Pessoa.Cpf,
                DataNascimento = ingressante.Pessoa.DataNascimento,
                NumeroPassaporte = ingressante.Pessoa.NumeroPassaporte,
                TipoNacionalidade = ingressante.Pessoa.TipoNacionalidade,
                Emails = ingressante.EnderecosEletronicos.Where(f => f.EnderecoEletronico.TipoEnderecoEletronico == TipoEnderecoEletronico.Email)
                                                                             .Select(f => new UsuarioEmailData
                                                                             {
                                                                                 Ativo = true,
                                                                                 Email = f.EnderecoEletronico.Descricao
                                                                             }).ToList()
            };
            // Caso o usuário já exista no SAS, apenas retorna o seq do usuário criado anteriormente
            var seqUsuarioSas = UsuarioService.VerificaDuplicidadeUsuario(usuarioSas);
            if (seqUsuarioSas.HasValue)
                return seqUsuarioSas.Value;
            return UsuarioService.SalvarUsuario(usuarioSas);
        }

        private void SalvarSolicitacaoMatricula(IngressanteVO ingressanteVO, IngressanteConfigVO configHelper, List<long> seqsDivisaoTurmasAssociadas)
        {
            var seqCursoOfertaLocalidadeTurno = CampanhaOfertaDomainService.SearchProjectionByKey(
                                        new SMCSeqSpecification<CampanhaOferta>(ingressanteVO.Ofertas.First().SeqCampanhaOferta),
                                        x => x.Itens.FirstOrDefault().SeqCursoOfertaLocalidadeTurno);

            var configuracaoProcesso = ConfiguracaoProcessoDomainService.BuscarProjecaoPorParametros(ingressanteVO.SeqTipoVinculoAluno,
                                                                                                         ingressanteVO.SeqEntidadeResponsavel,
                                                                                                         ingressanteVO.SeqProcesso,
                                                                                                         seqCursoOfertaLocalidadeTurno,
                                                                                                         ingressanteVO.SeqNivelEnsino,
                                                                                                         x => new { x.Seq, x.Processo.Servico.OrigemSolicitacaoServico });

            var vo = new SolicitacaoMatriculaVO()
            {
                SeqConfiguracaoProcesso = configuracaoProcesso.Seq,
                SeqGrupoEscalonamento = ingressanteVO.SeqGrupoEscalonamento,
                SeqEntidadeResponsavel = ingressanteVO.SeqEntidadeResponsavel,
                SeqPessoaAtuacao = ingressanteVO.Seq,
                OrigemSolicitacaoServico = configuracaoProcesso.OrigemSolicitacaoServico,
                //NumeroProtocolo = ingressanteVO.Seq.ToString("D6"),
                Descricao = $"Solicitação de matrícula - {configHelper.DescricaoProcesso}",
                DescricaoPessoaAtuacao = ingressanteVO.Descricao,
                DataSolicitacao = DateTime.Now,
                TermoAdesaoVO = new TermoAdesaoSolicitacaoMatriculaVO()
                {
                    SeqInstituicaoEnsino = ingressanteVO.SeqInstituicaoEnsino,
                    SeqNivelEnsino = ingressanteVO.SeqNivelEnsino,
                    TipoOfertaExigeCurso = configHelper.ExigeCurso,
                    SeqTipoVinculoAluno = ingressanteVO.SeqTipoVinculoAluno,
                    SeqCurso = configHelper.SeqCurso
                },
                Documentos = ingressanteVO.Documentos?.TransformList<SolicitacaoDocumentoRequeridoVO>()
            };

            // Verifica se o vínculo da pessoa atuação foi parametrizado por instituição e nível de ensino para não exigir curso.
            if (!configHelper.ExigeCurso)
            {
                vo.SeqsDivisaoTurma = SearchProjectionByKey(new SMCSeqSpecification<Ingressante>(ingressanteVO.Seq),
                                                                x => x.Ofertas.SelectMany(y => y.CampanhaOferta.Itens.FirstOrDefault().Turma.DivisoesTurma.Where(w => seqsDivisaoTurmasAssociadas.Contains(w.Seq)).Select(f => new SolicitacaoMatriculaItemDivisoesVO
                                                                {
                                                                    Seq = f.Seq,
                                                                    SeqConfiguracaoComponente = f.DivisaoComponente.SeqConfiguracaoComponente
                                                                })));
            }
            SolicitacaoMatriculaDomainService.CriarNovaSolicitacaoMatricula(vo);
        }

        private void AtualizarSolicitacaoMatricula(IngressanteVO ingressanteVO, IngressanteConfigVO configHelper, List<long> seqsDivisaoTurmasAssociadas)
        {
            if (!configHelper.ExigeCurso)
            {
                var solicitacaoMatricular = SearchProjectionByKey(new SMCSeqSpecification<Ingressante>(ingressanteVO.Seq),
                                                                    x => x.SolicitacoesServico.FirstOrDefault(f => f is SolicitacaoMatricula));

                var vo = new SolicitacaoMatriculaVO()
                {
                    SeqsDivisaoTurma = SearchProjectionByKey(new SMCSeqSpecification<Ingressante>(ingressanteVO.Seq),
                                                                x => x.Ofertas.SelectMany(y => y.CampanhaOferta.Itens.FirstOrDefault().Turma.DivisoesTurma.Where(w => seqsDivisaoTurmasAssociadas.Contains(w.Seq)).Select(f => new SolicitacaoMatriculaItemDivisoesVO
                                                                {
                                                                    Seq = f.Seq,
                                                                    SeqConfiguracaoComponente = f.DivisaoComponente.SeqConfiguracaoComponente
                                                                })))
                };

                solicitacaoMatricular.SeqGrupoEscalonamento = ingressanteVO.SeqGrupoEscalonamento;
                SolicitacaoMatriculaDomainService.AtualizarItensSolicitacaoMatricula(vo, solicitacaoMatricular as SolicitacaoMatricula);
            }
        }

        public void VerificarQuantidadeOfertas(IngressanteVO ingressanteVO, IngressanteConfigVO vo)
        {
            if (ingressanteVO.Ofertas.Count > vo.QuantidadeOfertaCampanhaIngresso.Value)
            {
                throw new QuantidadeOfertasIngressanteException(vo.DescricaoVinculo);
            }
        }

        /// <summary>
        /// Validação da regra RN_ALN_036 Controle vaga oferta campanha - ingressante
        /// </summary>
        /// <param name="ingressanteVO">Dados do ingressante</param>
        public void VerificarQuantidadeDeVagasPorOferta(IngressanteVO ingressanteVO, IngressanteConfigVO vo)
        {
            if (!vo.ExigeCurso)
            {
                var falhas = new StringBuilder();

                var specProcesso = new SMCSeqSpecification<ProcessoSeletivo>(ingressanteVO.SeqProcessoSeletivo);
                var vagasOfertas = ProcessoSeletivoDomainService.SearchProjectionByKey(specProcesso, p => p.Ofertas.Select(s => new
                {
                    s.ProcessoSeletivo.ReservaVaga,
                    s.SeqCampanhaOferta,
                    s.QuantidadeVagas,
                    s.QuantidadeVagasOcupadas,
                    s.CampanhaOferta.Descricao,
                    QuantidadeVagasDivisoes = s.CampanhaOferta.Itens.FirstOrDefault().Turma.DivisoesTurma
                        .GroupBy(g => g.SeqDivisaoComponente)
                        .Select(sd => new
                        {
                            SeqDivisaoComponente = sd.Key,
                            QuantidadeVagas = sd.Sum(sm => sm.QuantidadeVagas),
                            QuantidadeVagasOcupadas = sd.Sum(sm => sm.QuantidadeVagasOcupadas ?? 0)
                        }).ToList(),
                })).ToList();

                // Recupera as ofertas já associadas ao ingressante ou assume como nenhuma caso este seja novo
                var ofertasBancoIngressante = ingressanteVO.Seq != 0 ?
                    SearchProjectionByKey(new SMCSeqSpecification<Ingressante>(ingressanteVO.Seq), p => p.Ofertas.Select(s => s.SeqCampanhaOferta).ToList()) :
                    new List<long>();

                foreach (var oferta in ingressanteVO.Ofertas)
                {
                    // Valida apenas ofertas que não estão associadas ao ingressante atualmente
                    if (!ofertasBancoIngressante.Contains(oferta.SeqCampanhaOferta))
                    {
                        var vagasOferta = vagasOfertas.FirstOrDefault(f => f.SeqCampanhaOferta == oferta.SeqCampanhaOferta);
                        if (vagasOferta == null)
                            throw new SMCApplicationException("Configuração de vínculo incorreta");
                        var divisaoTurmaSemVagas = vagasOferta.QuantidadeVagasDivisoes.SMCAny(a => a.QuantidadeVagasOcupadas >= a.QuantidadeVagas);
                        // Caso o processo controle vagas, valida a quantidade de vagas na ProcessoSeletivoOferta
                        // Caso contrário, valida a quantidade de vagas na divisão da turma
                        if ((vagasOferta.ReservaVaga && vagasOferta.QuantidadeVagasOcupadas >= vagasOferta.QuantidadeVagas) ||
                            (!vagasOferta.ReservaVaga && divisaoTurmaSemVagas))
                            falhas.AppendLine($"<br/>-{vagasOferta.Descricao}");
                    }
                }

                if (falhas.Length > 0)
                {
                    if (vagasOfertas.First().ReservaVaga)
                    {
                        throw new OfertasSemVagasException(falhas.ToString());
                    }
                    else
                    {
                        throw new TurmasSemVagasException(falhas.ToString());
                    }
                }
            }
        }

        private Ingressante TransformaIngressante(IngressanteVO ingressanteVO)
        {
            var ingressante = ingressanteVO.Transform<Ingressante>();
            ingressante.TipoAtuacao = TipoAtuacao.Ingressante;

            ingressante.Pessoa = PessoaDomainService.SearchByKey(new SMCSeqSpecification<Pessoa>(ingressanteVO.SeqPessoa),
                                                       IncludesPessoa.DadosPessoais
                                                       | IncludesPessoa.Filiacao
                                                       | IncludesPessoa.Telefones_Telefone
                                                       | IncludesPessoa.Enderecos_Endereco
                                                       | IncludesPessoa.EnderecosEletronicos_EnderecoEletronico);

            // Mantêm os cpf e passaporte da interface para realizar a busca de pessoa existente corretamente no SAS
            ingressante.Pessoa.Cpf = ingressanteVO.Cpf?.SMCRemoveNonDigits();
            ingressante.Pessoa.NumeroPassaporte = ingressanteVO.NumeroPassaporte;

            VerificaAlteracoesContatos(ingressanteVO, ingressante);

            if (ingressante.IsNew())
            {
                // Cria o primeiro registro no histórico de situações
                ingressante.HistoricosSituacao = new List<IngressanteHistoricoSituacao>()
                {
                    new IngressanteHistoricoSituacao()
                    {
                        SituacaoIngressante = SituacaoIngressante.AguardandoLiberacaMatricula
                    }
                };
            }

            return ingressante;
        }

        private void VerificaAlteracoesContatos(IngressanteVO ingressanteVO, Ingressante ingressante)
        {
            ingressante.Telefones = new List<PessoaTelefone>();
            // Verifica por alterações nos telefones
            if (ingressanteVO.Telefones != null)
            {
                foreach (var telefone in ingressanteVO.Telefones)
                {
                    if (telefone.SeqTelefone == 0)
                    {
                        // Veio da importação
                        var telefoneBanco = ingressante.Pessoa.Telefones.FirstOrDefault(f =>
                                                    SMCReflectionHelper.CompareExistingPrimitivePropertyValues(f.Telefone, telefone, IgnoredProperties));
                        telefone.Seq = telefoneBanco.Seq;
                        telefone.SeqTelefone = telefoneBanco.SeqTelefone;
                    }

                    ingressante.Telefones.Add(new PessoaTelefone()
                    {
                        Seq = telefone.Seq,
                        SeqPessoa = ingressante.SeqPessoa,
                        SeqTelefone = telefone.SeqTelefone
                    });
                }
            }

            ingressante.EnderecosEletronicos = new List<PessoaEnderecoEletronico>();
            // Verifica por alterações no endereços eletronicos
            if (ingressanteVO.EnderecosEletronicos != null)
            {
                foreach (var enderecoEletronico in ingressanteVO.EnderecosEletronicos)
                {
                    if (enderecoEletronico.SeqEnderecoEletronico == 0)
                    {
                        // Veio da importação
                        var endEletronicoBanco = ingressante.Pessoa.EnderecosEletronicos.FirstOrDefault(f =>
                                                            f.EnderecoEletronico.TipoEnderecoEletronico == enderecoEletronico.TipoEnderecoEletronico
                                                            && f.EnderecoEletronico.Descricao.ToLower().Trim() == enderecoEletronico.DescricaoEnderecoEletronico.ToLower().Trim());
                        enderecoEletronico.Seq = endEletronicoBanco.Seq;
                        enderecoEletronico.SeqEnderecoEletronico = endEletronicoBanco.SeqEnderecoEletronico;
                    }
                    ingressante.EnderecosEletronicos.Add(new PessoaEnderecoEletronico
                    {
                        Seq = enderecoEletronico.Seq,
                        SeqPessoa = ingressante.SeqPessoa,
                        SeqEnderecoEletronico = enderecoEletronico.SeqEnderecoEletronico,
                        EnderecoEletronico = enderecoEletronico.Transform<EnderecoEletronico>()
                    });
                }
            }

            ingressante.Enderecos = new List<PessoaAtuacaoEndereco>();
            // Verifica por alterações nos endereços
            if (ingressanteVO.Enderecos != null)
            {
                foreach (var endereco in ingressanteVO.Enderecos)
                {
                    if (endereco.SeqEndereco == 0)
                    {
                        // Veio da importação.
                        var pessoaEnderecoBanco = ingressante.Pessoa.Enderecos.FirstOrDefault(f =>
                                                    SMCReflectionHelper.CompareExistingPrimitivePropertyValues(f.Endereco, endereco, IgnoredProperties));
                        endereco.SeqPessoaEndereco = pessoaEnderecoBanco.Seq;
                        endereco.EnderecoCorrespondencia = (endereco.Correspondencia.GetValueOrDefault()) ?
                                                                EnderecoCorrespondencia.AcademicaFinanceira :
                                                                EnderecoCorrespondencia.Nao;
                    }

                    ingressante.Enderecos.Add(new PessoaAtuacaoEndereco()
                    {
                        SeqPessoaEndereco = endereco.SeqPessoaEndereco,
                        EnderecoCorrespondencia = endereco.EnderecoCorrespondencia.GetValueOrDefault()
                    });
                }
            }
        }

        /// <summary>
        /// RN_ALN_035 - Registro da data de admissão e data de término prevista do ingressante
        /// </summary>
        private (DateTime Admissao, DateTime Termino, long? seqMatrizCurricularOferta) BuscarDatasEMatrizCurricularOferta(IngressanteVO ingressanteVO, IngressanteConfigVO configVO)
        {
            DateTime dataAdmissao;
            DateTime dataTermino;
            long? seqMatrizCurricularOferta;

            var paramSpec = new InstituicaoNivelTipoVinculoAlunoFilterSpecification()
            {
                SeqTipoVinculoAluno = ingressanteVO.SeqTipoVinculoAluno,
                SeqNivelEnsino = ingressanteVO.SeqNivelEnsino,
                SeqInstituicao = ingressanteVO.SeqInstituicaoEnsino
            };
            var instituicaoNivelTipoVinculoAluno = InstituicaoNivelTipoVinculoAlunoDomainService.SearchProjectionByKey(paramSpec,
                                                        x => new { x.Seq, x.ConcedeFormacao });
            bool? termoIntercambioConcedeFormacao = null;

            // Verifica se o ingressante possui um termo de intercambio
            if (ingressanteVO.TermosIntercambio != null && ingressanteVO.TermosIntercambio.Count > 0)
            {
                var seqTipoTermoIntercambio = TermoIntercambioDomainService.SearchProjectionByKey(
                                                    new SMCSeqSpecification<TermoIntercambio>(ingressanteVO.TermosIntercambio.First().SeqTermoIntercambio),
                                                        x => x.ParceriaIntercambioTipoTermo.SeqTipoTermoIntercambio);
                var intercambioSpec = new InstituicaoNivelTipoTermoIntercambioFilterSpecification()
                {
                    SeqTipoTermoIntercambio = seqTipoTermoIntercambio,
                    SeqInstituicaoNivelTipoVinculoAluno = instituicaoNivelTipoVinculoAluno.Seq
                };
                termoIntercambioConcedeFormacao = InstituicaoNivelTipoTermoIntercambioDomainService.SearchProjectionByKey(intercambioSpec, x => x.ConcedeFormacao);
            }

            if (instituicaoNivelTipoVinculoAluno.ConcedeFormacao || termoIntercambioConcedeFormacao.GetValueOrDefault())
            {
                // 1.1 - Busca o tipo de calculo da data de admissão associada ao processo seletivo
                var tipoCalculoDataAdmissao = ProcessoSeletivoDomainService.SearchProjectionByKey(new SMCSeqSpecification<ProcessoSeletivo>(ingressanteVO.SeqProcessoSeletivo),
                                                                                x => x.TipoProcessoSeletivo.TipoCalculoDataAdmissao);

                if (tipoCalculoDataAdmissao == TipoCalculoDataAdmissao.DataInicioPeriodoLetivo)
                {
                    var seqCicloLetivo = CampanhaCicloLetivoDomainService.SearchProjectionByKey(new SMCSeqSpecification<CampanhaCicloLetivo>(ingressanteVO.SeqCampanhaCicloLetivo),
                                            x => x.SeqCicloLetivo);
                    var seqCursoOfertaLocalidadeTurno = CampanhaOfertaDomainService.SearchProjectionByKey(new SMCSeqSpecification<CampanhaOferta>(ingressanteVO.Ofertas.First().SeqCampanhaOferta),
                                                                                  x => x.Itens.FirstOrDefault().SeqCursoOfertaLocalidadeTurno);
                    var dataPeriodoLetivo = ConfiguracaoEventoLetivoDomainService.BuscarDatasEventoLetivo(ingressanteVO.SeqCicloLetivo,
                                                                                                          seqCursoOfertaLocalidadeTurno.GetValueOrDefault(),
                                                                                                          TipoAluno.Calouro,
                                                                                                          TOKEN_TIPO_EVENTO.PERIODO_LETIVO);
                    dataAdmissao = dataPeriodoLetivo.DataInicio;
                }
                else// if (tipoCalculoDataAdmissao == Common.Areas.CAM.Enums.TipoCalculoDataAdmissao.DataAtual)
                {
                    var itensGrupoEscalonamento = GrupoEscalonamentoDomainService.SearchProjectionByKey(new SMCSeqSpecification<GrupoEscalonamento>(ingressanteVO.SeqGrupoEscalonamento),
                                                                    x => x.Itens.Select(f => f.Escalonamento));
                    // Busca pelo escalonamento de maior data final. Existe uma regra no cadastro que não permite que uma etapa anterior possua data final
                    // maior do que as próximas etapas. Assim, a maior data fim será a data da última etapa.
                    dataAdmissao = itensGrupoEscalonamento.OrderByDescending(o => o.DataFim).First().DataFim;
                }

                // oferta de matriz curricular
                seqMatrizCurricularOferta = BuscarMatricCurricularOferta(ingressanteVO.Ofertas.First().SeqCampanhaOferta, dataAdmissao, configVO.ExigeCurso);
                var prazoConclusao = MatrizCurricularOfertaDomainService.SearchProjectionByKey(new SMCSeqSpecification<MatrizCurricularOferta>(seqMatrizCurricularOferta.GetValueOrDefault()),
                                                                            x => x.MatrizCurricular.QuantidadeMesesPrevistoConclusao);
                // último dia do mês anterior a data de adimissão mais o prazo de conclusao em meses
                dataTermino = dataAdmissao.AddMonths(prazoConclusao).AddDays(-dataAdmissao.Day);
            }
            else
            {
                if (termoIntercambioConcedeFormacao.HasValue && !termoIntercambioConcedeFormacao.Value)
                {
                    // 1.2 - Utiliza as datas de admissão e términos dos termos de intercâmbio associado ao ingressante.
                    var seqTermosIntercambio = ingressanteVO.TermosIntercambio.Select(s => s.SeqTermoIntercambio).ToArray();
                    var specTermos = new SMCContainsSpecification<TermoIntercambio, long>(p => p.Seq, seqTermosIntercambio);
                    var datasTermos = TermoIntercambioDomainService.SearchProjectionBySpecification(specTermos, p => new
                    {
                        DataInicio = p.Vigencias.Min(m => m.DataInicio),
                        DataFim = p.Vigencias.Max(m => m.DataFim)
                    }).ToList();
                    dataAdmissao = datasTermos.Min(m => m.DataInicio);
                    dataTermino = datasTermos.Max(m => m.DataFim);
                }
                else
                {
                    // 1.3 - Ingressante que possui vínculo-instituição-nível
                    (DateTime dataInicio, DateTime dataFim) datas = CalcularDatasDisciplinaIsolada(ingressanteVO);

                    dataAdmissao = datas.dataInicio;
                    dataTermino = datas.dataFim;
                }

                // oferta de matriz curricular
                seqMatrizCurricularOferta = BuscarMatricCurricularOferta(ingressanteVO.Ofertas.First().SeqCampanhaOferta, dataAdmissao, configVO.ExigeCurso);
            }

            return (dataAdmissao, dataTermino, seqMatrizCurricularOferta);
        }

        private (DateTime dataInicio, DateTime dataFim) CalcularDatasDisciplinaIsolada(IngressanteVO ingressanteVO)
        {
            var oferta = CampanhaOfertaDomainService.SearchProjectionByKey(new SMCSeqSpecification<CampanhaOferta>(ingressanteVO.Ofertas.First().SeqCampanhaOferta),
                                x => x.Itens.SelectMany(f => f.Turma.ConfiguracoesComponente.SelectMany(g => g.RestricoesTurmaMatriz)
                                    .Where(s => s.OfertaMatrizPrincipal).Select(h =>
                                        h.MatrizCurricularOferta.SeqCursoOfertaLocalidadeTurno
                                    )
                                ).FirstOrDefault()
                         );
            if (oferta == 0)
            {
                var nomeOfertaCampanha = CampanhaOfertaDomainService.SearchProjectionByKey(new SMCSeqSpecification<CampanhaOferta>(ingressanteVO.Ofertas.First().SeqCampanhaOferta),
                                x => x.Descricao);
                throw new TurmaSemMatrizPrincipalException(nomeOfertaCampanha);
            }

            var datas = ConfiguracaoEventoLetivoDomainService.BuscarDatasEventoLetivo(ingressanteVO.SeqCicloLetivo,
                                                                                 oferta,
                                                                                 TipoAluno.Calouro,
                                                                                 TOKEN_TIPO_EVENTO.PERIODO_CICLO_LETIVO);

            return (datas.DataInicio, datas.DataFim);
        }

        private long CriaOrientacaoAssociacaoOrientadorIngressante(AssociacaoOrientadorIngressanteVO associacaoOrientadorIngressanteVO, Ingressante ingressante)
        {
            var seqInstituicaoExterna = InstituicaoExternaDomainService.SearchProjectionByKey(new InstituicaoExternaFilterSpecification { SeqInstituicaoEnsino = associacaoOrientadorIngressanteVO.SeqInstituicaoEnsino },
                                                        x => x.Seq);

            var orientacao = new Orientacao
            {
                SeqNivelEnsino = ingressante.SeqNivelEnsino,
                SeqTipoOrientacao = associacaoOrientadorIngressanteVO.SeqTipoOrientacao.GetValueOrDefault(),
                SeqEntidadeInstituicao = associacaoOrientadorIngressanteVO.SeqInstituicaoEnsino,
                OrientacoesColaborador = new List<OrientacaoColaborador>(),
                SeqTipoVinculoAluno = ingressante.SeqTipoVinculoAluno
            };

            foreach (var item in associacaoOrientadorIngressanteVO.Orientacoes)
            {
                orientacao.OrientacoesColaborador.Add(new OrientacaoColaborador()
                {
                    TipoParticipacaoOrientacao = (TipoParticipacaoOrientacao)item.TipoParticipacaoOrientacao,
                    SeqInstituicaoExterna = seqInstituicaoExterna,
                    SeqColaborador = item.SeqColaborador.GetValueOrDefault()
                });
            }

            OrientacaoDomainService.SaveEntity(orientacao);
            return orientacao.Seq;
        }

        private long CriaOrientacaoPessoaAtuacao(IngressanteVO ingressanteVO)
        {
            var seqTipoOrientacao = TipoOrientacaoDomainService.SearchProjectionByKey(new TipoOrientacaoFilterSpecification { TrabalhoConclusaoCurso = true },
                                                                                    x => x.Seq);
            var seqInstituicaoExterna = InstituicaoExternaDomainService.SearchProjectionByKey(new InstituicaoExternaFilterSpecification { SeqInstituicaoEnsino = ingressanteVO.SeqInstituicaoEnsino },
                                                                    x => x.Seq);
            var orientacao = new Orientacao
            {
                SeqEntidadeInstituicao = ingressanteVO.SeqInstituicaoEnsino,
                SeqNivelEnsino = ingressanteVO.SeqNivelEnsino,
                SeqTipoOrientacao = seqTipoOrientacao,
                SeqTipoVinculoAluno = ingressanteVO.SeqTipoVinculoAluno,
                OrientacoesColaborador = new List<OrientacaoColaborador>
                    {
                        new OrientacaoColaborador()
                        {
                            TipoParticipacaoOrientacao = TipoParticipacaoOrientacao.Orientador,
                            SeqInstituicaoExterna = seqInstituicaoExterna,
                            SeqColaborador = ingressanteVO.SeqOrientador.Value
                        }
                    }
            };
            OrientacaoDomainService.SaveEntity(orientacao);
            return orientacao.Seq;
        }

        private long CriaOrientacoesPessoaAtuacaoIntercambio(IngressanteVO ingressanteVO, PessoaAtuacaoTermoIntercambioVO termoVO, long seqTipoTermoIntercambio)
        {
            var orientacao = new Orientacao
            {
                SeqEntidadeInstituicao = ingressanteVO.SeqInstituicaoEnsino,
                SeqNivelEnsino = ingressanteVO.SeqNivelEnsino,
                SeqTipoOrientacao = termoVO.SeqTipoOrientacao.GetValueOrDefault(),
                SeqTipoVinculoAluno = ingressanteVO.SeqTipoVinculoAluno,
                SeqTipoTermoIntercambio = seqTipoTermoIntercambio,
                OrientacoesColaborador = new List<OrientacaoColaborador>()
            };
            foreach (var orientador in termoVO.OrientacaoParticipacoesColaboradores)
            {
                orientacao.OrientacoesColaborador.Add(
                    new OrientacaoColaborador()
                    {
                        TipoParticipacaoOrientacao = orientador.TipoParticipacaoOrientacao,
                        SeqInstituicaoExterna = orientador.SeqInstituicaoExterna,
                        SeqColaborador = orientador.SeqColaborador
                    });
            }
            OrientacaoDomainService.SaveEntity(orientacao);
            return orientacao.Seq;
        }

        private long CriaOrientacaoPessoaAtuacao(IngressanteVO ingressanteVO, IEnumerable<IngressanteOrientacaoVO> orientacoesVO, long seqTipoOrientacao)
        {
            var orientacao = new Orientacao
            {
                SeqEntidadeInstituicao = ingressanteVO.SeqInstituicaoEnsino,
                SeqNivelEnsino = ingressanteVO.SeqNivelEnsino,
                SeqTipoOrientacao = seqTipoOrientacao,
                SeqTipoVinculoAluno = ingressanteVO.SeqTipoVinculoAluno,
                OrientacoesColaborador = new List<OrientacaoColaborador>()
            };
            foreach (var orientador in orientacoesVO)
            {
                orientacao.OrientacoesColaborador.Add(
                    new OrientacaoColaborador()
                    {
                        TipoParticipacaoOrientacao = orientador.TipoParticipacaoOrientacao,
                        SeqInstituicaoExterna = orientador.SeqInstituicaoExterna,
                        SeqColaborador = orientador.SeqColaborador
                    });
            }
            OrientacaoDomainService.SaveEntity(orientacao);
            return orientacao.Seq;
        }

        private string[] IgnoredProperties
        {
            get
            {
                return new List<string>
                {
                    nameof(PessoaDadosPessoais.Seq),
                    nameof(PessoaDadosPessoais.SeqPessoa),
                    nameof(PessoaDadosPessoais.UsuarioAlteracao),
                    nameof(PessoaDadosPessoais.UsuarioInclusao),
                    nameof(PessoaDadosPessoais.DataAlteracao),
                    nameof(PessoaDadosPessoais.DataInclusao)
                }.ToArray();
            }
        }

        public List<AssociacaoIngressanteLoteCabecalhoVO> BuscarCabecalhoAssociacaoIngressanteLote(long[] seqsIngressantes)
        {
            var spec = new SMCContainsSpecification<Ingressante, long>(p => p.Seq, seqsIngressantes);

            var result = this.SearchProjectionBySpecification(spec, i => new AssociacaoIngressanteLoteCabecalhoVO()
            {
                SeqIngressante = i.Seq,
                Nome = i.DadosPessoais.Nome,
                NomeSocial = i.DadosPessoais.NomeSocial,
                Cpf = string.IsNullOrEmpty(i.Pessoa.Cpf) ? "-" : i.Pessoa.Cpf,
                NumeroPassaporte = string.IsNullOrEmpty(i.Pessoa.NumeroPassaporte) ? "-" : i.Pessoa.NumeroPassaporte,
                Falecido = i.Pessoa.Falecido
            });

            return result.ToList();
        }

        public AssociacaoFormacaoEspecificaIngressanteVO BuscarAssociacaoFormacoesEspecificasIngressante(long seqIngressante)
        {
            ///RN_ALN_015 - Parâmetro LK formação específica ingressante
            var result = this.SearchProjectionByKey(new SMCSeqSpecification<Ingressante>(seqIngressante), i => new AssociacaoFormacaoEspecificaIngressanteVO()

            {
                SeqIngressante = i.Seq,
                SeqCurso = i.MatrizCurricularOferta.CursoOfertaLocalidadeTurno.CursoOfertaLocalidade.CursoOferta.SeqCurso,
                SeqCursoOfertaLocalidade = i.MatrizCurricularOferta.CursoOfertaLocalidadeTurno.SeqCursoOfertaLocalidade,
                SeqFormacaoEspecifica = i.MatrizCurricularOferta.CursoOfertaLocalidadeTurno.CursoOfertaLocalidade.CursoOferta.SeqFormacaoEspecifica,
                SeqFormacaoEspecificaOrigem = i.FormacoesEspecificas.FirstOrDefault().SeqFormacaoEspecificaOrigem,
                FormacoesEspecificas = i.FormacoesEspecificas.Select(f => new FormacaoEspecificaHierarquiaVO() { Seq = f.SeqFormacaoEspecifica }),
            });
            result.FormacoesEspecificas = this.FormacaoEspecificaDomainService.BuscarFormacoesEspecificasHierarquia(result.FormacoesEspecificas.Select(f => f.Seq).ToArray());

            if (result.SeqFormacaoEspecificaOrigem.HasValue)
            {
                result.SeqFormacaoEspecifica = result.SeqFormacaoEspecificaOrigem;
            }

            return result;
        }

        public long SalvarAssociacaoFormacaoEspecificaIngressante(long seqInstituicao, AssociacaoFormacaoEspecificaIngressanteVO model)
        {
            //Recupera o ingressante com suas formações específicas e cursos
            var ingressante = this.SearchByKey(new SMCSeqSpecification<Ingressante>(model.SeqIngressante),
                                               IncludesIngressante.FormacoesEspecificas_FormacaoEspecifica |
                                               IncludesIngressante.Ofertas_CampanhaOfertaItem_CursoOfertaLocalidadeTurno_CursoOfertaLocalidade_CursoOferta_Curso_TipoEntidade |
                                               IncludesIngressante.HistoricosSituacao);

            //Fistra os itens que podem ser gravados
            model.FormacoesEspecificas = FiltrarItensHirarquia(model.FormacoesEspecificas);

            //RN_ALN_019
            var seqCurso = ingressante.Ofertas.FirstOrDefault().CampanhaOfertaItem.CursoOfertaLocalidadeTurno.CursoOfertaLocalidade.CursoOferta.SeqCurso;
            var tiposFormacaoInvalidos = InstituicaoTipoEntidadeFormacaoEspecificaDomainService
                                        .BuscarObrigatoriedadeFormacoesNaoAtendidasPorCurso(seqCurso,
                                                                                            model.FormacoesEspecificas.Select(s => s.Seq),
                                                                                            true, false)
                                        .Select(s => string.Format(MessagesResource.AssociacaoFormacaoEspecificaIngressanteQtdTipoFormacaoEspecifica,
                                                s.QuantidadePermitidaAssociacaoIngressante,
                                                s.TipoFormacaoEspecifica.Descricao));
            if (tiposFormacaoInvalidos.Any())
                throw new AssociacaoFormacaoEspecificaIngressanteException(string.Join("", tiposFormacaoInvalidos));

            //Recupera a formação específica origem. Como esse Seq é sempre o mesmo para todas as formações
            //específicas associadas ao ingressante, pode ser utilizado o Seq do primeiro registro (first).
            var seqFormacaoEspecificaOrigem = ingressante.FormacoesEspecificas != null && ingressante.FormacoesEspecificas.Count > 0 ? ingressante.FormacoesEspecificas.First().SeqFormacaoEspecificaOrigem : null;

            //Limpa a lista de formações específicas do ingressante ou instancia caso esteja nula
            if (ingressante.FormacoesEspecificas == null)
                ingressante.FormacoesEspecificas = new List<IngressanteFormacaoEspecifica>();
            else
                ingressante.FormacoesEspecificas.Clear();

            //Adiciona as formações específicas ao ingressante. Mantendo a formação específica origem
            foreach (var item in model.FormacoesEspecificas)
            {
                ingressante.FormacoesEspecificas.Add(new IngressanteFormacaoEspecifica() { SeqFormacaoEspecifica = item.Seq, SeqFormacaoEspecificaOrigem = seqFormacaoEspecificaOrigem });
            }

            //Salva o ingressante com suas formações específicas associadas
            this.SaveEntity(ingressante);

            return ingressante.Seq;
        }

        /// <summary>
        /// REmove itens redundantes da hierarquia
        /// </summary>
        /// <param name="formacoesEspecificas">Lista de itens completa</param>
        /// <returns>Lista de itens filtrados</returns>
        public List<FormacaoEspecificaHierarquiaVO> FiltrarItensHirarquia(IEnumerable<FormacaoEspecificaHierarquiaVO> formacoesEspecificas)
        {
            if (!formacoesEspecificas.SMCAny())
            {
                return new List<FormacaoEspecificaHierarquiaVO>();
            }

            //Instancia uma lista de itens a remover
            var seqsItensRemover = new List<long>();

            //Verifica os itens a remover e adiciona a lista
            foreach (var item in formacoesEspecificas)
            {
                if (item.Hierarquia != null)
                    seqsItensRemover.AddRange(item.Hierarquia.Where(x => x.Seq != item.Seq).Select(x => x.Seq));
            }

            //Resotna a lista de formações específicas a serem gravadas
            return formacoesEspecificas.Where(w => !seqsItensRemover.Contains(w.Seq)).ToList();
        }

        public AssociacaoOrientadorIngressanteVO BuscarAssociacaoOrientadorIngressante(long seqIngressante, long? seqTipoOrientacao = null)
        {
            var result = this.SearchProjectionByKey(
                new SMCSeqSpecification<Ingressante>(seqIngressante),
                i => new AssociacaoOrientadorIngressanteVO()
                {
                    SeqIngressante = i.Seq,
                    SeqInstituicaoEnsino = i.Pessoa.SeqInstituicaoEnsino,
                    SeqTipoOrientacao = i.OrientacoesPessoaAtuacao
                        .FirstOrDefault(f => !seqTipoOrientacao.HasValue || seqTipoOrientacao == f.Orientacao.SeqTipoOrientacao)
                        .Orientacao.SeqTipoOrientacao,
                    SeqNivelEnsino = i.SeqNivelEnsino,
                    Cpf = i.Pessoa.Cpf,
                    NumeroPassaporte = i.Pessoa.NumeroPassaporte,
                    Orientacoes = i.OrientacoesPessoaAtuacao
                        .FirstOrDefault(f => !seqTipoOrientacao.HasValue || seqTipoOrientacao == f.Orientacao.SeqTipoOrientacao)
                        .Orientacao.OrientacoesColaborador.Select(o =>
                            new AssociacaoOrientadorIngressanteItemVO()
                            {
                                SeqColaborador = o.SeqColaborador,
                                TipoParticipacaoOrientacao = (long)o.TipoParticipacaoOrientacao,
                                NomeColaborador = o.Colaborador.DadosPessoais.Nome
                            }).ToList()
                });

            result.SeqTipoIntercambio = TermoIntercambioDomainService.SearchProjectionByKey(
                new TermoIntercambioFilterSpecification() { Cpf = result.Cpf, NumeroPassaporte = result.NumeroPassaporte },
                p => (long?)p.ParceriaIntercambioTipoTermo.SeqTipoTermoIntercambio);

            result.Orientacoes.SMCForEach(o => o.DescricaoTipoParticipacaoOrientacao = SMCEnumHelper.GetDescription((TipoParticipacaoOrientacao)o.TipoParticipacaoOrientacao));

            return result;
        }

        public long SalvarAssociacaoOrientadorIngressante(AssociacaoOrientadorIngressanteVO model)
        {
            ValidarParametrosAssociacaoOrientador(model);

            //Recupera o ingressante
            var ingressante = this.SearchByKey(new SMCSeqSpecification<Ingressante>(model.SeqIngressante), IncludesIngressante.HistoricosSituacao);

            using (var unityOfWork = SMCUnitOfWork.Begin())
            {
                try
                {
                    ingressante.OrientacoesPessoaAtuacao = new List<OrientacaoPessoaAtuacao>()
                    {
                        new OrientacaoPessoaAtuacao()
                        {
                            SeqOrientacao = CriaOrientacaoAssociacaoOrientadorIngressante(model, ingressante)
                        }
                    };

                    this.SaveEntity(ingressante);

                    unityOfWork.Commit();
                }
                catch (Exception)
                {
                    unityOfWork.Rollback();
                    throw;
                }
            };

            return ingressante.Seq;
        }

        private void ValidarParametrosAssociacaoOrientador(AssociacaoOrientadorIngressanteVO model)
        {
            //Recupera os dados so ingressante para a validação
            var ingressante = SearchProjectionByKey(new SMCSeqSpecification<Ingressante>(model.SeqIngressante), i =>
            new
            {
                SeqIngressante = i.Seq,
                SeqNivelEnsino = i.SeqNivelEnsino,
                SeqTipoVinculoAluno = i.SeqTipoVinculoAluno
            });

            //Cria o spec para filtro dos dados
            var spec = new InstituicaoNivelTipoOrientacaoFilterSpecification()
            {
                SeqNivelEnsino = ingressante.SeqNivelEnsino,
                SeqTipoVinculoAluno = ingressante.SeqTipoVinculoAluno,
                SeqTipoOrientacao = model.SeqTipoOrientacao
            };

            // Caso tenha termo associado, ignora o tipo de vínculo e busca apenas
            if (model.SeqTipoIntercambio.HasValue)
            {
                spec.SeqTipoVinculoAluno = null;
                spec.SeqTipoIntercambio = model.SeqTipoIntercambio;
            }

            //Retorna os parametros
            var parametros = this.InstituicaoNivelTipoOrientacaoDomainService.SearchByKey(spec, IncludesInstituicaoNivelTipoOrientacao.TiposParticipacao);

            //Verifica se as orientações obrigatórias foram associadas ao colaborador e se
            //foi associado um colaborador para ela
            foreach (var tipoParticipacao in parametros.TiposParticipacao.Where(t => t.ObrigatorioOrientacao))
            {
                //Caso
                var orientacaoIngressante = model.Orientacoes.FirstOrDefault(o => o.TipoParticipacaoOrientacao == (long)tipoParticipacao.TipoParticipacaoOrientacao);

                if (orientacaoIngressante == null || !orientacaoIngressante.SeqColaborador.HasValue)
                    throw new AssociacaoObrigatoriaOrientadorIngressanteException(tipoParticipacao.TipoParticipacaoOrientacao);
            }

            //Verifica se foi selecionado im tipo de participação que não foi parametrizado para o tipo de orientação
            foreach (var tipoParticipacao in model.Orientacoes.Select(o => o.TipoParticipacaoOrientacao))
            {
                if (!parametros.TiposParticipacao.Any(t => (long)t.TipoParticipacaoOrientacao == tipoParticipacao))
                    throw new AssociacaoOrientadorIngressanteSemParametroException(SMCEnumHelper.GetEnum<TipoParticipacaoOrientacao>(tipoParticipacao.ToString()));
            }
        }

        private bool ValidarVinculoAssociacaoOrientador(long seqIngressante)
        {
            try
            {
                //RN_ALN_025

                //Recupera os dados so ingressante para a validação
                var ingressante = SearchProjectionByKey(new SMCSeqSpecification<Ingressante>(seqIngressante), i =>
                new
                {
                    SeqIngressante = i.Seq,
                    SeqNivelEnsino = i.SeqNivelEnsino,
                    SeqTipoVinculoAluno = i.SeqTipoVinculoAluno,
                    SeqTipoTermoIntercambio = (long?)i.TermosIntercambio.FirstOrDefault().TermoIntercambio.ParceriaIntercambioTipoTermo.SeqTipoTermoIntercambio
                });

                //Cria o spec para filtro dos dados InstituicaoNivelTipoOrientacao
                var specInstituicaoNivelTipoOrientacao = new InstituicaoNivelTipoOrientacaoFilterSpecification()
                {
                    SeqNivelEnsino = ingressante.SeqNivelEnsino,
                    SeqTipoVinculoAluno = ingressante.SeqTipoVinculoAluno
                };

                //Retorna os registros para validação de InstituicaoNivelTipoOrientacao
                var instituicaoNivelTipoOrientacao = this.InstituicaoNivelTipoOrientacaoDomainService.SearchByKey(specInstituicaoNivelTipoOrientacao);

                //Cria um objeto para validação de InstituicaoNivelTipoTermoIntercambio
                InstituicaoNivelTipoTermoIntercambio instituicaoNivelTipoTermoIntercambio = null;

                //Caso o ingressante temoa termo de intercâmbio parametrizado
                if (ingressante.SeqTipoTermoIntercambio.HasValue)
                {
                    //Cria o spec e filtro dos dados InstituicaoNivelTipoTermoIntercambio
                    var specInstituicaoNivelTipoTermoIntercambio = new InstituicaoNivelTipoTermoIntercambioFilterSpecification()
                    {
                        SeqTipoTermoIntercambio = ingressante.SeqTipoTermoIntercambio.GetValueOrDefault()
                    };

                    //Recupera os parametros de InstituicaoNivelTipoTermoIntercambio
                    instituicaoNivelTipoTermoIntercambio = this.InstituicaoNivelTipoTermoIntercambioDomainService.SearchByKey(specInstituicaoNivelTipoTermoIntercambio);
                }

                //Retorna a validação
                return instituicaoNivelTipoOrientacao != null &&
                       (instituicaoNivelTipoOrientacao.CadastroOrientacaoIngressante == CadastroOrientacao.NaoPermite ||
                        instituicaoNivelTipoTermoIntercambio != null);
            }
            catch
            {
                // Caso exista inconsistência nos dados, assume como impeditivo
                return true;
            }
        }

        private bool ValidarPermissaoAssociacaoOrientador(long seqIngressante)
        {
            try
            {
                //RN_ALN_025

                //Recupera os dados so ingressante para a validação
                var ingressante = SearchProjectionByKey(new SMCSeqSpecification<Ingressante>(seqIngressante), i =>
                new
                {
                    SeqIngressante = i.Seq,
                    SeqNivelEnsino = i.SeqNivelEnsino,
                    SeqTipoVinculoAluno = i.SeqTipoVinculoAluno
                });

                //Cria o spec para filtro dos dados
                var spec = new InstituicaoNivelTipoOrientacaoFilterSpecification()
                {
                    SeqNivelEnsino = ingressante.SeqNivelEnsino,
                    SeqTipoVinculoAluno = ingressante.SeqTipoVinculoAluno
                };

                //Retorna a validação
                return this.InstituicaoNivelTipoOrientacaoDomainService.SearchByKey(spec).CadastroOrientacaoIngressante == CadastroOrientacao.Exige;
            }
            catch
            {
                // Caso exista inconsistência nos dados, assume como impeditivo
                return true;
            }
        }

        /// <summary>
        /// Valida se o ingressante têm impedimento segundo a regra RN_ALN_003 - Consistência situação ingressante
        /// </summary>
        /// <param name="seqIngressante">Sequencial do ingressante</param>
        /// <returns>Verdadeiro caso a situação do ingressante não permita alterações</returns>
        public bool ValidarSituacaoImpeditivaIngressante(long seqIngressante)
        {
            try
            {
                //RN_ALN_003

                var ingressante = this.SearchByKey(new SMCSeqSpecification<Ingressante>(seqIngressante), IncludesIngressante.HistoricosSituacao);

                var situacaoAtualIngressante = ingressante.HistoricosSituacao.OrderByDescending(h => h.Seq).Select(h => h.SituacaoIngressante).First();

                return (situacaoAtualIngressante == SituacaoIngressante.Matriculado ||
                        situacaoAtualIngressante == SituacaoIngressante.Desistente ||
                        situacaoAtualIngressante == SituacaoIngressante.Cancelado);
            }
            catch
            {
                // Caso exista inconsistência nos dados, assume como impeditivo
                return true;
            }
        }

        private bool ValidarExigenciaOfertaMatrizCurricular(long seqIngressante)
        {
            try
            {
                //RN_ALN_023

                var instituicaoTipoVinculoAluno = this.InstituicaoNivelTipoVinculoAlunoDomainService.BuscarInstituicaoNivelTipoVinculoAlunoPorPessoaAtuacao(seqIngressante);

                return (instituicaoTipoVinculoAluno.ExigeOfertaMatrizCurricular.HasValue &&
                    !instituicaoTipoVinculoAluno.ExigeOfertaMatrizCurricular.Value);
            }
            catch
            {
                // Caso exista inconsistência nos dados, assume como impeditivo
                return true;
            }
        }

        private string PrepararMensagemErro(List<InstituicaoTipoEntidadeFormacaoEspecificaVO> configuracoes)
        {
            //Variável para mensagem de erro
            var mensagemErro = string.Empty;

            //Para cada registro encontrado, prepara a formatação da string da mensagem de erro caso ocorra
            foreach (var item in configuracoes)
            {
                mensagemErro += string.Format(MessagesResource.AssociacaoFormacaoEspecificaIngressanteQtdTipoFormacaoEspecifica, item.QuantidadePermitidaAssociacaoIngressante, item.DescricaoTipoFormacaoEspecifica);
            }

            return mensagemErro;
        }

        /// <summary>
        /// Chama a procedure de criação do aluno no GRA
        /// </summary>
        /// <param name="seqSolicitacaoMatricula">Código da solicitação de matrícula</param>
        /// <returns></returns>
        public long CriaAlunoNoFinanceiro(long seqSolicitacaoMatricula, long seqIngressante)
        {
            // TODO: Fazer para quando for aluno e não ingressante
            // Rever regra para aluno
            var seqPessoaAtuacao = SolicitacaoMatriculaDomainService.SearchProjectionByKey(new SMCSeqSpecification<SolicitacaoMatricula>(seqSolicitacaoMatricula), x =>
                x.SeqPessoaAtuacao
            );

            var instituicaoNiveltipoVinculoAluno = InstituicaoNivelTipoVinculoAlunoDomainService.BuscarInstituicaoNivelTipoVinculoAlunoPorPessoaAtuacao(seqPessoaAtuacao);
            var exigeCurso = instituicaoNiveltipoVinculoAluno?.ExigeCurso.Value ?? true;

            var dadosIngressante = SolicitacaoMatriculaDomainService.SearchProjectionByKey(new SMCSeqSpecification<SolicitacaoMatricula>(seqSolicitacaoMatricula), x => new
            {
                AnoCicloLetivo = x.ConfiguracaoProcesso.Processo.CicloLetivo.Ano, // Validar.. não ta na documentação. chamar?
                NumeroCicloLetivo = x.ConfiguracaoProcesso.Processo.CicloLetivo.Numero, // Validar.. não ta na documentação. chamar?
                QuantidadeCreditos = !exigeCurso ? x.Itens
                    .Where(y => y.SeqDivisaoTurma.HasValue &&
                           y.HistoricosSituacao.OrderByDescending(s => s.Seq).FirstOrDefault().SituacaoItemMatricula.ClassificacaoSituacaoFinal == ClassificacaoSituacaoFinal.FinalizadoComSucesso)
                    .Select(d => new { d.DivisaoTurma.SeqTurma, d.DivisaoTurma.DivisaoComponente.ConfiguracaoComponente.ComponenteCurricular.Credito })
                    .GroupBy(d => d.SeqTurma)
                    .Select(d => d.FirstOrDefault())
                    .Sum(y => y.Credito) ?? 0 : 0,
                SeqCondicaoPagamento = x.SeqCondicaoPagamentoGra,
                DataInicio = x.GrupoEscalonamento.Itens.OrderBy(i => i.Escalonamento.DataInicio).Select(i => i.Escalonamento.DataInicio).FirstOrDefault(),
                DataFim = x.GrupoEscalonamento.Itens.OrderByDescending(i => i.Escalonamento.DataFim).Select(i => i.Escalonamento.DataFim).FirstOrDefault(),
                TipoVinculoAlunoFinanceiro = (x.PessoaAtuacao as Ingressante).TipoVinculoAluno.TipoVinculoAlunoFinanceiro,
                NumeroDivisaoParcelas = x.GrupoEscalonamento.NumeroDivisaoParcelas,
                SeqPessoa = x.PessoaAtuacao.SeqPessoa,
                Parcelas = x.GrupoEscalonamento.Itens.SelectMany(i => i.Parcelas),
                TipoPessoaAtuacao = x.PessoaAtuacao.TipoAtuacao,
            });

            // Recupera os dados de origem
            var dadosOrigem = PessoaAtuacaoDomainService.RecuperaDadosOrigem(seqPessoaAtuacao);

            // Carrega os benefícios da pessoa atuação
            var beneficio = PessoaAtuacaoBeneficioDomainService.SearchBySpecification(new PessoaAtuacaoBeneficioFilterSpecification
            {
                SeqPessoaAtuacao = seqPessoaAtuacao,
                // Agora deve mandar o beneficio mesmo que nao incida na parcela de matricula.
                //IncideParcelaMatricula = true,
                SituacaoChancelaBeneficio = SituacaoChancelaBeneficio.Deferido,
            }, IncludesPessoaAtuacaoBeneficio.ConfiguracaoBeneficio | IncludesPessoaAtuacaoBeneficio.Beneficio).ToList();

            var obj = new MatriculaParametroData();

            /*- Código do aluno: sequencial da pessoa-atuação do ingressante */
            obj.CodigoAluno = null; // PROCEDURE NAO ACEITA PASSAR CODIGO ALUNO PARA CRIACAO DE INGRESSANTE

            /*- Tipo de aluno: Valor correspondente ao domínio “Ingressante” */
            obj.TipoAluno = null; // TipoAluno.Ingressante; (Não tem tipo aluno ingressante. Enviar null? Validar)

            /*- Código da pessoa: código de pessoa do CAD associado ao ingressante. (A pessoa do CAD está nos Dados Mestres) */
            obj.CodigoPessoa = PessoaDomainService.BuscarCodigoDePessoaNosDadosMestres(dadosIngressante.SeqPessoa, TipoPessoa.Fisica, seqPessoaAtuacao);

            /*- Origem:
             * É o seq_origem_financeira do curso oferta localidade do ingressante, quando este possuir curso.
             * Caso não possua, simular qual seria o seu respectivo curso, conforme o nível de ensino e entidade responsável que ele possui e, tipo de oferta de curso cujo token seja “INTERNO”.*/
            obj.SeqOrigem = (int)dadosOrigem.SeqOrigem;

            /*- Código do serviço origem:
             * - Se o ingressante possuir curso associado, enviar como parâmetro o curso-oferta-localidade da oferta do ingressante
             * - Se o ingressante não possuir curso associado, simular qual seria o seu respectivo curso, conforme o nível de ensino e entidade responsável que ele possui e, tipo de oferta de curso cujo token seja “INTERNO”. */
            obj.CodigoServicoOrigem = dadosOrigem.CodigoServicoOrigem;

            /*- Data da solicitação da transação: data corrente do sistema*/
            obj.DataSolicitacaoTransacao = DateTime.Now;

            /*- Data início do período letivo: data início do escalonamento associado para a primeira etapa do processo de matrícula no grupo de escalonamento do ingressante */
            obj.DataInicioPeriodoLetivo = dadosIngressante.DataInicio;

            /*- Data fim do período letivo: data fim do escalonamento associado para a última etapa do processo de matrícula no grupo de escalonamento do ingressante */
            obj.DataFimPeriodoLetivo = dadosIngressante.DataFim;

            /*- Condição de pagamento: passar o valor selecionado pela pessoa-atuação*/
            obj.SeqCondicaoPagamento = dadosIngressante.SeqCondicaoPagamento; // (Validar... Carol disse que era pra passar null)

            /*- Tipo de Transação: passar o seq_tipo_transacao_financeira correspondente à inclusão do ingressante parametrizado nos parâmetros gerais da instituição */
            obj.CodigoTipoTransacao = 44; // (Validar.. fixo 44?)

            /*- Quantidade de Créditos: preencher apenas quando o ingressante não possuir curso. Somar os créditos de todas as disciplinas isoladas existentes na oferta do ingressante */
            obj.QuantidadeCreditos = dadosIngressante.QuantidadeCreditos;

            /*- Tipo de Vínculo: enviar o valor do tipo de vínculo financeiro correspondente ao vínculo do ingressante */
            obj.SeqTipoVinculoAluno = Convert.ToInt32(dadosIngressante.TipoVinculoAlunoFinanceiro);

            /*- Seq_ingressante: sequencial da pessoa-atuação do ingressante */
            obj.SeqIngressante = seqPessoaAtuacao;

            /*- num_divisao_parcelas: num_divisao_parcelas do grupo de escalonamento do ingressante*/
            obj.NumeroDivisaoParcelas = Convert.ToInt32(dadosIngressante.NumeroDivisaoParcelas.GetValueOrDefault());

            /*Lista de Valores:
             * Enviar uma lista com todos os valores associados ao grupo de escalonamento do ingressante
             *  - Número da Parcela
             *  - Data Vencimento Parcela
             *  - Percentual Parcela
            */
            // VALIDAR. Não é só da etapa atual?
            obj.Parcelas = new List<MatriculaParcelaData>();
            foreach (var parcela in dadosIngressante.Parcelas.Where(p => p.NumeroParcela.HasValue))
            {
                obj.Parcelas.Add(new MatriculaParcelaData()
                {
                    NumeroParcela = parcela.NumeroParcela.Value,
                    DataVencimentoParcela = parcela.DataVencimentoParcela,
                    ValorPercentualParcela = parcela.ValorPercentualParcela
                });
            }

            /* CAMPOS QUE NÃO ESTÃO NA ESPECIFICAÇÃO */
            //- Ano/Número do ciclo letivo
            obj.AnoCicloLetivo = dadosIngressante.AnoCicloLetivo;// ingressante.CampanhaCicloLetivo.CicloLetivo.Ano;
            obj.NumeroCicloLetivo = dadosIngressante.NumeroCicloLetivo;// ingressante.CampanhaCicloLetivo.CicloLetivo.Numero;

            /* NOVOS CAMPOS 06/12/2017 */
            // Se ingressante possui benefício, busca os parametros do benefício para recebimento de cobrança e observação.
            if (dadosIngressante.TipoPessoaAtuacao == TipoAtuacao.Ingressante && beneficio != null && beneficio.SMCAny(s => s.IncideParcelaMatricula))
            {
                //obj.RecebeCobranca = beneficio.Benefici.RecebeCobranca;
                //obj.ObservacaoCobranca = beneficio.Beneficio.JustificativaNaoRecebeCobranca;
                obj.RecebeCobranca = beneficio.FirstOrDefault(f => f.IncideParcelaMatricula).Beneficio.RecebeCobranca;
                obj.ObservacaoCobranca = beneficio.FirstOrDefault(f => f.IncideParcelaMatricula).Beneficio.JustificativaNaoRecebeCobranca;
            }
            else
            {
                obj.RecebeCobranca = true;
                obj.ObservacaoCobranca = null;
            }

            // Faz a chamada ao integração financeiro para incluir a matrícula
            var seqMatricula = IntegracaoFinanceiroService.IncluirMatriculaAcademico(obj);
            if (seqMatricula == 0)
                throw new InclusoaMatriculaAcademicoException();
            else
            {
                // Se ingressante possui benefício, chama rotina do benefício
                if (dadosIngressante.TipoPessoaAtuacao == TipoAtuacao.Ingressante && beneficio != null)
                {
                    //Caso os beneficios sejam deferido e não possui beneficio financeiro irá acionar a rotina de inclusão ST_INCLUI_CONTRATO_BENEFICIO_ACADEMICO
                    //passando para efetuar a verificação de envio de cobrança.
                    var pessoasAtuacoesBeneficioSemBenficioFinanceiro = beneficio.Where(w => !w.Beneficio.SeqBeneficioFinanceiro.HasValue);
                    foreach (var item in pessoasAtuacoesBeneficioSemBenficioFinanceiro)
                    {
                        PessoaAtuacaoBeneficioDomainService.AtualizarContratoPessoaAtuacaoBeneficio(item.Seq, true);
                    }

                    //Valida as parcelas e faz os devidos tratamentos conforme a regra RN_FIN_006, retornando somente os beneficios a serem enviados para o contrato conforme ordenação pre-determinada
                    var pessoasAtuacoesBeneficio = this.PessoaAtuacaoBeneficioDomainService.ValidarChancelaBeneficiosPortal(seqPessoaAtuacao, dadosOrigem);
                    /*Ao acionar a rotina de inclusão ST_INCLUI_CONTRATO_BENEFICIO_ACADEMICO individualmente e,
                    houver retorno de mensadem de erro. Os seguintes procedimentos deverão ser realizados:
                    · Inserir no histórico a situação atual igual a Aguardando Chancela, a data de início igual a data
                    corrente(hoje) e a observação deverá ser preenchida no seguinte formato: "Durante a adesão no
                    portal do ingressante foi identificado erro durante a inclusão do contrato de benefício, conforme
                    retorno do sistema financeiro. Motivo: [Descrição da mensagem de erro do respectivo benefício].*/
                    string mensagemErro = "Durante a adesão no portal do ingressante foi identificado erro durante a inclusão do contrato de benefício, conforme retorno do sistema financeiro. Motivo: {0}.";
                    long seqPessoaAtuacaoComRestricao = 0;
                    foreach (var item in pessoasAtuacoesBeneficio)
                    {
                        try
                        {
                            seqPessoaAtuacaoComRestricao = item;
                            PessoaAtuacaoBeneficioDomainService.AtualizarContratoPessoaAtuacaoBeneficio(item, true);
                        }
                        catch (Exception ex)
                        {
                            this.PessoaAtuacaoBeneficioDomainService.SalvarBeneficioHistoricoSituacaoChancela(seqPessoaAtuacaoComRestricao,
                                                                                                              (int)SituacaoChancelaBeneficio.AguardandoChancela,
                                                                                                              string.Format(mensagemErro, ex.Message));
                        }
                    }
                }
                // Cria os bloqueios para a solicitação em questão
                SolicitacaoMatriculaDomainService.CriaBloqueiosFinanceirosMatricula(seqSolicitacaoMatricula, seqPessoaAtuacao, TipoAtuacao.Ingressante);
            }

            return seqMatricula;
        }

        private int BuscarQuantidadeDeCreditosDasDisciplinasIsoladasDoIngressante(IList<SolicitacaoServico> solicitacoesServico)
        {
            int quantidadeCreditos = 0;

            foreach (var solServico in solicitacoesServico)
            {
                if (solServico is SolicitacaoMatricula)
                {
                    var solMatricula = SolicitacaoMatriculaDomainService.SearchByKey(new SMCSeqSpecification<SolicitacaoMatricula>(solServico.Seq), IncludesSolicitacaoMatricula.Itens
                        | IncludesSolicitacaoMatricula.Itens_ConfiguracaoComponente
                        | IncludesSolicitacaoMatricula.Itens_ConfiguracaoComponente_ComponenteCurricular);

                    foreach (var item in solMatricula.Itens)
                    {
                        //Se não tiver divisaão de turma é disciplina isolada
                        if (!item.SeqDivisaoTurma.HasValue)
                        {
                            quantidadeCreditos += (int)item.ConfiguracaoComponente.ComponenteCurricular.Credito;
                        }
                    }
                }
            }

            return quantidadeCreditos;
        }

        /// <summary>
        /// Aplica a validação da regra RN_ALN_031  Consistência Vínculo Tipo Termo Intercâmbio
        /// </summary>
        /// <param name="ingressante">Dados do ingressante com nível de ensino, tipo de vínculo e termo de intercâmbio</param>
        /// <returns>Verdaderio caso a regra 31 ocorra</returns>
        public bool ConsistenciaVinculoTipoTermoIntercambio(IngressanteVO ingressante)
        {
            var configVinculo = InstituicaoNivelTipoVinculoAlunoDomainService.BuscarInstituicaoNivelTipoVinculoAlunoPorNivelTipo(ingressante.SeqNivelEnsino, ingressante.SeqTipoVinculoAluno);

            // Tipo de termo que concendem formação na configuração do vínculo do ingressante
            var tiposTermoFormacao = configVinculo.TiposTermoIntercambio.Where(w => w.ConcedeFormacao).Select(s => s.Seq).ToArray();
            var specTermos = new SMCContainsSpecification<TermoIntercambio, long>(p => p.ParceriaIntercambioTipoTermo.SeqTipoTermoIntercambio, tiposTermoFormacao);
            var seqsTermos = TermoIntercambioDomainService.SearchProjectionBySpecification(specTermos, p => p.Seq).ToList();

            // Verdadeiro se o tipo de vínculo exigir parceria ou o tipo do termo for configurado para conceder formação
            return configVinculo.ExigeParceriaIntercambioIngresso || ingressante.TermosIntercambio.SMCCount(c => seqsTermos.Contains(c.SeqTermoIntercambio)) > 0;
        }

        /// <summary>
        /// Adapta o lookup
        /// </summary>
        /// <param name="ingressante"></param>
        /// <returns></returns>
        public long PrepararIngressanteGravacao(IngressanteVO ingressante)
        {
            ValidarContatosIngressante(ingressante);

            if (!ingressante.Ofertas.SMCAny())
            {
                throw new IngressanteSemOfertasException();
            }

            PessoaAtuacaoDomainService.RestaurarCamposReadonlyCpf(ref ingressante);

            if (ingressante.Seq == 0)
            {
                //RN_ALN_005.3
                ingressante.HistoricosSituacao = new List<IngressanteHistoricoSituacaoVO>() {
                    new IngressanteHistoricoSituacaoVO()
                    {
                        SituacaoIngressante = SituacaoIngressante.AguardandoLiberacaMatricula
                    }
                };
                //RN_ALN_005.4
                ingressante.OrigemIngressante = OrigemIngressante.Manual;
            }
            else
            {
                //UC_ALN_002_01_01.NV12
                var infoIngressante = this.SearchProjectionByKey(ingressante.Seq, p => new
                {
                    p.OrigemIngressante,
                    p.HistoricosSituacao.OrderByDescending(o => o.DataInclusao).FirstOrDefault().SituacaoIngressante
                });
                var edicaoPermitida =
                    !(
                        infoIngressante.SituacaoIngressante == SituacaoIngressante.Matriculado ||
                        infoIngressante.SituacaoIngressante == SituacaoIngressante.Desistente ||
                        infoIngressante.SituacaoIngressante == SituacaoIngressante.Cancelado
                    );

                if (!edicaoPermitida)
                    throw new AlteracaoDadosNaoPermitidaException();

                ingressante.OrigemIngressante = infoIngressante.OrigemIngressante;
            }

            // RN_ALN_005 1.1 Validação de duplicitade do ingressante
            if (ingressante.SeqPessoa != 0)
            {
                var spec = new IngressanteFilterSpecification() { SeqPessoa = ingressante.SeqPessoa };
                var ingressantesBanco = this.SearchProjectionBySpecification(spec, p => new
                {
                    p.Seq,
                    p.CampanhaCicloLetivo.SeqCicloLetivo,
                    p.SeqTipoVinculoAluno,
                    p.SeqFormaIngresso,
                    SeqsOfertas = p.Ofertas.Select(s => s.SeqCampanhaOferta),
                    p.HistoricosSituacao.OrderByDescending(o => o.Seq).FirstOrDefault().SituacaoIngressante,
                    SeqsGruposEscalonamento = p.SolicitacoesServico.Select(s => s.SeqGrupoEscalonamento)
                });

                foreach (var ingressanteBanco in ingressantesBanco)
                {
                    if (ingressante.Seq == ingressanteBanco.Seq)
                        continue;
                    var mesmaOferta = ingressante.SeqCicloLetivo == ingressanteBanco.SeqCicloLetivo
                                   && ingressante.SeqTipoVinculoAluno == ingressanteBanco.SeqTipoVinculoAluno
                                   && ingressante.SeqFormaIngresso == ingressanteBanco.SeqFormaIngresso
                                   && ingressante.Ofertas.Any(novo => ingressanteBanco.SeqsOfertas.Any(banco => novo.SeqCampanhaOferta == banco));

                    // Caso a atuação de ingressante esteja cancelada, valida também o escalonamento
                    if (ingressanteBanco.SituacaoIngressante == SituacaoIngressante.Cancelado || ingressanteBanco.SituacaoIngressante == SituacaoIngressante.Desistente)
                        mesmaOferta &= ingressanteBanco.SeqsGruposEscalonamento.Any(a => a.GetValueOrDefault() == ingressante.SeqGrupoEscalonamento);

                    if (mesmaOferta)
                        throw new IngressanteDuplicadoException(ingressante.Seq == 0 ? ExceptionsResource.Inclusao : ExceptionsResource.Alteracao);
                }
            }

            // RN_PES_022 - Consistência pessoa existente
            if (ingressante.CadastrarNovaPessoa)
            {
                var specPessoa = new PessoaFilterSpecification()
                {
                    NomeAtuacaoInicio = string.Join(" ", ingressante.Nome.Split(' ').Take(2)),
                    NomesFiliacaoInicio = ingressante.Filiacao?.Select(s => s.Nome.Split(' ').First()).ToArray(),
                    DataNascimento = ingressante.DataNascimento
                };
                if (PessoaDomainService.Count(specPessoa) > 0)
                    throw new PessoaDuplicadaException(ExceptionsResource.Inclusao);
            }

            // RN_ALN_005.5.1 Validação do item de oferta
            if (ingressante.Ofertas != null && ingressante.Ofertas.Count > 0)
            {
                // Recupera os itens das ofertas selecionadas
                var specItens = new SMCContainsSpecification<CampanhaOferta, long>(p => p.Seq, ingressante.Ofertas.Select(s => s.SeqCampanhaOferta).ToArray());
                var configItens = CampanhaOfertaDomainService.SearchProjectionBySpecification(specItens, p => new
                {
                    p.Seq,
                    SeqsItens = p.Itens.Select(s => s.Seq),
                }).ToList();
                foreach (var oferta in ingressante.Ofertas)
                {
                    var configItem = configItens.FirstOrDefault(f => f.Seq == oferta.SeqCampanhaOferta);
                    // Caso a oferta selecionada tenha apenas um item, amarra o item ao ingressante
                    if (configItem != null && configItem.SeqsItens.Count() == 1)
                        oferta.SeqCampanhaOfertaItem = configItem.SeqsItens.First();
                }
            }

            //FIX: Remover ao corrigir o lookup de endereço
            ingressante.Enderecos = ingressante.Enderecos.Select(s => new PessoaAtuacaoEnderecoVO()
            {
                SeqEndereco = s.SeqEndereco,
                SeqPessoaEndereco = s.SeqPessoaEndereco,
                EnderecoCorrespondencia = s.EnderecoCorrespondencia
            }).ToList();

            var filtroCampanha = new ProcessoFiltroVO() { SeqCampanhaCicloLetivo = ingressante.SeqCampanhaCicloLetivo, SeqProcessoSeletivo = ingressante.SeqProcessoSeletivo };
            ingressante.SeqProcesso = ProcessoDomainService.BuscarProcesso(filtroCampanha)?.Seq ?? 0;

            ValidarTermosIntercambioIngressante(ingressante);
            ValidarOrientacaoIngressante(ingressante);
            ValidarGrupoEscalonamentoIngressante(ingressante.SeqGrupoEscalonamento);

            return SalvarIngressante(ingressante);
        }

        /// <summary>
        /// Recupera o ingressante com todas suas dependências
        /// </summary>
        /// <param name="seq">Sequencial do ingressante</param>
        /// <returns>Dados do ingressante com suas dependências</returns>
        public IngressanteVO BuscarIngressante(long seq)
        {
            // Recupera os dados do ingressante

            // Dados únicos do ingressante
            var includesSemMultiplicidade = IncludesIngressante.CampanhaCicloLetivo_Campanha
                                          | IncludesIngressante.CampanhaCicloLetivo_CicloLetivo
                                          | IncludesIngressante.DadosPessoais_ArquivoFoto
                                          | IncludesIngressante.EntidadeResponsavel
                                          | IncludesIngressante.FormaIngresso
                                          | IncludesIngressante.MatrizCurricularOferta
                                          | IncludesIngressante.NivelEnsino
                                          | IncludesIngressante.ProcessoSeletivo
                                          | IncludesIngressante.SolicitacoesServico
                                          | IncludesIngressante.TipoVinculoAluno;
            var ingressante = SearchByKey(new SMCSeqSpecification<Ingressante>(seq), includesSemMultiplicidade);

            // Dados que podem ter vários registros

            // Endereços do ingressante
            ingressante.Enderecos = PessoaAtuacaoEnderecoDomainService.SearchBySpecification(
                new PessoaAtuacaoEnderecoFilterSpecification() { SeqPessoaAtuacao = ingressante.Seq },
                IncludesPessoaAtuacaoEndereco.PessoaEndereco_Endereco).ToList();

            // Endereços eletrônicos e telefones
            var contatosVo = PessoaAtuacaoDomainService.SearchProjectionByKey(new SMCSeqSpecification<PessoaAtuacao>(ingressante.Seq), p => new
            {
                EnderecosEletronicos = p.EnderecosEletronicos.Select(s => new PessoaEnderecoEletronicoVO()
                {
                    Seq = s.Seq,
                    SeqPessoa = s.SeqPessoa,
                    SeqEnderecoEletronico = s.SeqEnderecoEletronico,
                    DescricaoEnderecoEletronico = s.EnderecoEletronico.Descricao,
                    TipoEnderecoEletronico = s.EnderecoEletronico.TipoEnderecoEletronico
                }).ToList(),
                Telefones = p.Telefones.Select(s => new PessoaTelefoneVO()
                {
                    Seq = s.Seq,
                    SeqPessoa = s.SeqPessoa,
                    SeqTelefone = s.SeqTelefone,
                    CodigoPais = s.Telefone.CodigoPais,
                    CodigoArea = s.Telefone.CodigoArea,
                    Numero = s.Telefone.Numero,
                    TipoTelefone = s.Telefone.TipoTelefone
                }).ToList()
            });

            var pessoaFiliacao = PessoaDomainService.SearchByKey(new SMCSeqSpecification<Pessoa>(ingressante.SeqPessoa), IncludesPessoa.Filiacao);
            ingressante.Pessoa = pessoaFiliacao;

            // Configuração do vínculo para definir se deve ou não recuperar orientação e intercâmbio
            var specVinculo = new InstituicaoNivelTipoVinculoAlunoFilterSpecification()
            {
                SeqNivelEnsino = ingressante.SeqNivelEnsino,
                SeqTipoVinculoAluno = ingressante.SeqTipoVinculoAluno
            };
            var configVinculo = InstituicaoNivelTipoVinculoAlunoDomainService.SearchProjectionByKey(specVinculo, p => new
            {
                ContemOrientacaoIngressante = p.TiposOrientacao.Any(a => a.CadastroOrientacaoIngressante == CadastroOrientacao.Permite || a.CadastroOrientacaoIngressante == CadastroOrientacao.Exige),
                p.ExigeParceriaIntercambioIngresso
            });

            if (configVinculo.ContemOrientacaoIngressante)
            {
                var ingressanteOrientacao = SearchByKey(new SMCSeqSpecification<Ingressante>(seq), IncludesIngressante.OrientacoesPessoaAtuacao_Orientacao_OrientacoesColaborador);
                ingressante.OrientacoesPessoaAtuacao = ingressanteOrientacao.OrientacoesPessoaAtuacao;
            }

            if (configVinculo.ExigeParceriaIntercambioIngresso)
            {
                var ingressanteTermo = SearchByKey(new SMCSeqSpecification<Ingressante>(seq),
                                                        IncludesIngressante.TermosIntercambio_Orientacao_OrientacoesColaborador |
                                                        IncludesIngressante.TermosIntercambio_Orientacao_TipoOrientacao |
                                                        IncludesIngressante.TermosIntercambio_TermoIntercambio_ParceriaIntercambioTipoTermo_TipoTermoIntercambio |
                                                        IncludesIngressante.TermosIntercambio_TermoIntercambio_Vigencias |
                                                        IncludesIngressante.TermosIntercambio_TermoIntercambio_ParceriaIntercambioInstituicaoExterna);
                ingressante.TermosIntercambio = ingressanteTermo.TermosIntercambio;
            }

            var ofertas = SearchByKey(new SMCSeqSpecification<Ingressante>(seq), IncludesIngressante.Ofertas_CampanhaOferta);
            ingressante.Ofertas = ofertas.Ofertas;

            // Tranformação do ingressante em vo
            var ingressanteVo = ingressante.Transform<IngressanteVO>();
            ingressanteVo.EnderecosEletronicos = contatosVo.EnderecosEletronicos;
            ingressanteVo.Telefones = contatosVo.Telefones;

            if (ingressante.DadosPessoais.SeqArquivoFoto.HasValue)
                ingressanteVo.ArquivoFoto.GuidFile = ingressante.DadosPessoais.ArquivoFoto.UidArquivo.ToString();

            var seqsOrientacoesIntercambio = ingressante.TermosIntercambio?.Select(s => s.Orientacao.Seq) ?? new List<long>();
            var orientacoesDiretas = ingressante
                ?.OrientacoesPessoaAtuacao
                ?.Where(w => !seqsOrientacoesIntercambio.Contains(w.SeqOrientacao))
                .ToList() ?? new List<OrientacaoPessoaAtuacao>();
            var colaboradores = new List<SMCDatasourceItem>();

            // Caso tenha alguma orientação
            if (orientacoesDiretas.Any() || ingressante.TermosIntercambio.SMCAny())
            {
                // Carrega os colaboradores disponíveis para a campanha do ingressante
                colaboradores = ColaboradorDomainService.BuscarColaboradoresSelect(new ColaboradorFiltroVO()
                {
                    SeqCampanhaOferta = ingressanteVo.Ofertas.First().SeqCampanhaOferta,
                    VinculoAtivo = true
                });
            }

            // Caso tenha orientações "diretas" de ingressante
            if (orientacoesDiretas.Any())
            {
                ingressanteVo.SeqTipoOrientacao = orientacoesDiretas.First().Orientacao.SeqTipoOrientacao;

                // Recupera as instituições externas dos colaboradores
                var instituicoesColaboradores = orientacoesDiretas
                    .SelectMany(opa => opa.Orientacao.OrientacoesColaborador.Select(oc => oc.SeqColaborador))
                    .Distinct()
                    .Select(s => new KeyValuePair<long, List<SMCDatasourceItem>>(s,
                        InstituicaoExternaDomainService.BuscarInstituicaoExternaPorColaboradorSelect(new InstituicaoExternaFiltroVO() { SeqColaborador = s })))
                    .ToDictionary(k => k.Key, v => v.Value);

                // Recupera os tipos de participação para os tipos de orientação
                var tiposOrientacaoParticiapacoes = orientacoesDiretas
                    .Select(s => s.Orientacao.SeqTipoOrientacao)
                    .Distinct()
                    .Select(s => new KeyValuePair<long, List<SMCDatasourceItem>>(s,
                        InstituicaoNivelTipoOrientacaoParticipacaoDomainService
                            .BuscarInstituicaoNivelTipoOrientacaoParticipacaoSelect(
                                new InstituicaoNivelTipoOrientacaoParticipacaoFiltroVO() { SeqTipoOrientacao = s })))
                    .ToDictionary(k => k.Key, v => v.Value);

                var orientacoesColaboradorDiretas = orientacoesDiretas.SelectMany(sm => sm.Orientacao.OrientacoesColaborador);
                // Recupera os colaboradores relacionados para o NameDescriptionField
                var specColaboradoresRelacionados = new SMCContainsSpecification<Colaborador, long>(p => p.Seq, orientacoesColaboradorDiretas.Select(s => s.SeqColaborador).ToArray());
                var colaboradoesRelacionados = ColaboradorDomainService.SearchProjectionBySpecification(specColaboradoresRelacionados, p => new { p.Seq, p.DadosPessoais.Nome }).ToList();

                // Recupera as instiuições relacionadas para o NameDescriptionField
                var specInstituicoesRelacionadas = new SMCContainsSpecification<InstituicaoExterna, long>(p => p.Seq, orientacoesColaboradorDiretas.Select(s => s.SeqInstituicaoExterna).ToArray());
                var instituicoesExternasRelacionadas = InstituicaoExternaDomainService.SearchProjectionBySpecification(specInstituicoesRelacionadas, p => new { p.Seq, p.Nome }).ToList();

                // Transforma a hierarquia de orientação para a estrutura esperada na tela
                ingressanteVo.OrientacaoParticipacoesColaboradores = orientacoesDiretas
                    .SelectMany(opa => opa.Orientacao.OrientacoesColaborador.Select(oc => new IngressanteOrientacaoVO()
                    {
                        Seq = oc.Seq,
                        SeqColaborador = oc.SeqColaborador,
                        SeqInstituicaoExterna = oc.SeqInstituicaoExterna,
                        TipoParticipacaoOrientacao = oc.TipoParticipacaoOrientacao,
                        NomeColaborador = colaboradoesRelacionados.FirstOrDefault(f => f.Seq == oc.SeqColaborador)?.Nome,
                        ColaboradorParticipacaoConfirmacao = colaboradoesRelacionados.FirstOrDefault(f => f.Seq == oc.SeqColaborador)?.Nome,
                        DescricaoTipoParticipacaoOrientacao = oc.TipoParticipacaoOrientacao.SMCGetDescription(),
                        NomeInstituicaoExterna = instituicoesExternasRelacionadas.FirstOrDefault(f => f.Seq == oc.SeqInstituicaoExterna)?.Nome,
                        Colaboradores = colaboradores,
                        InstituicoesExternas = instituicoesColaboradores[oc.SeqColaborador],
                        TiposParticipacaoOrientacao = tiposOrientacaoParticiapacoes[opa.Orientacao.SeqTipoOrientacao]
                    })).ToList();
            }

            if (ingressante.TermosIntercambio.SMCAny())
            {
                // Orientações por nível
                var orientacoesPorNivel = InstituicaoNivelTipoOrientacaoParticipacaoDomainService.BuscarInstituicaoNivelTipoOrientacaoParticipacoes(new InstituicaoNivelTipoOrientacaoParticipacaoFilterSpecification() { SeqNivelEnsino = ingressante.SeqNivelEnsino });
                // Recupera as instituições externas para todos colaboradores únicos utilizados
                var instituicoesPorColaborador = ingressante
                    .TermosIntercambio
                    .SelectMany(s => s.Orientacao
                        .OrientacoesColaborador
                        .Select(so => so.SeqColaborador))
                    .Distinct()
                    .ToDictionary(
                        k => k,
                        v => InstituicaoExternaDomainService
                            .BuscarInstituicaoExternaPorColaboradorSelect(new InstituicaoExternaFiltroVO()
                            {
                                SeqColaborador = v,
                                Ativo = true
                            }));

                var orientacoesColaborador = ingressante.TermosIntercambio.SelectMany(sm => sm.Orientacao?.OrientacoesColaborador);
                // Recupera os colaboradores relacionados para o NameDescriptionField
                var specColaboradoresAssociados = new SMCContainsSpecification<Colaborador, long>(p => p.Seq, orientacoesColaborador.Select(s => s.SeqColaborador).ToArray());
                var nomesColaboradores = ColaboradorDomainService.SearchProjectionBySpecification(specColaboradoresAssociados, p => new { p.Seq, p.DadosPessoais.Nome }).ToList();
                // Recupera as instituições externas relacionadas para o NameDescriptionField
                var seqsInstituicoesExternas = ingressante
                    .TermosIntercambio.Select(s => s.TermoIntercambio.ParceriaIntercambioInstituicaoExterna.SeqInstituicaoExterna)
                    .Union(orientacoesColaborador.Select(s => s.SeqInstituicaoExterna))
                    .ToArray();
                var specInstituicoesExternasAssociadas = new SMCContainsSpecification<InstituicaoExterna, long>(p => p.Seq, seqsInstituicoesExternas);
                var nomesInstituicoesExternas = InstituicaoExternaDomainService.SearchProjectionBySpecification(specInstituicoesExternasAssociadas, p => new { p.Seq, p.Nome }).ToList();

                // Configura os tipos e descrições das participações de orientação nos termos de intercâmbio
                ingressanteVo.TermosIntercambio.SMCForEach(termo =>
                {
                    // Recupera os tipo de orientação para o nível de ensino e tipo de orientação do termo
                    var tiposParticipacao = InstituicaoNivelTipoOrientacaoParticipacaoDomainService.BuscarInstituicaoNivelTipoOrientacaoParticipacaoSelect(new InstituicaoNivelTipoOrientacaoParticipacaoFiltroVO()
                    {
                        SeqTipoOrientacao = termo.SeqTipoOrientacao,
                        SeqNivelEnsino = ingressanteVo.SeqNivelEnsino,
                        SeqTermoIntercambio = termo.SeqTermoIntercambio,
                        SeqTipoVinculo = ingressanteVo.SeqTipoVinculoAluno
                    });

                    var requerOrientacao = InstituicaoNivelTipoOrientacaoDomainService.BuscarTiposOrientacaoSelect(new InstituicaoNivelTipoOrientacaoFiltroVO()
                    {
                        SeqNivelEnsino = ingressanteVo.SeqNivelEnsino,
                        SeqTermoIntercambio = termo.SeqTermoIntercambio,
                        SeqTipoVinculoAluno = ingressanteVo.SeqTipoVinculoAluno,
                        CadastroOrientacaoIngressante = CadastroOrientacao.Exige
                    }).SMCAny();

                    var termoBanco = ingressante.TermosIntercambio.First(f => f.Seq == termo.Seq);

                    termo.SeqInstituicaoEnsinoExterna = termoBanco.TermoIntercambio.ParceriaIntercambioInstituicaoExterna.SeqInstituicaoExterna;
                    termo.InstituicaoExterna = nomesInstituicoesExternas.FirstOrDefault(f => f.Seq == termo.SeqInstituicaoEnsinoExterna)?.Nome;
                    termo.PermiteOrientacao = termo.OrientacaoParticipacoesColaboradores.SMCCount() > 0;
                    termo.RequerOrientacao = requerOrientacao;
                    termo.ExistePeriodo = termoBanco.TermoIntercambio.Vigencias.SMCAny();
                    if (termo.ExistePeriodo)
                    {
                        termo.DataInicio = termoBanco.TermoIntercambio.Vigencias.Min(m => m.DataInicio);
                        termo.DataFim = termoBanco.TermoIntercambio.Vigencias.Max(m => m.DataFim);
                    }
                    termo.DescricaoTipoIntercambio = termoBanco.TermoIntercambio.ParceriaIntercambioTipoTermo.TipoTermoIntercambio.Descricao;
                    termo.DescricaoTermoIntercambio = termoBanco.TermoIntercambio.Descricao;

                    termo.OrientacaoParticipacoesColaboradores.SMCForEach(orientacao =>
                    {
                        // Replica o datasource de tipos de participação para o mestre detalhe na edição
                        orientacao.TiposParticipacaoOrientacao = tiposParticipacao;

                        // recupera o tipo de participação parametrizado
                        var orientacaoNivel = orientacoesPorNivel.FirstOrDefault(f => f.TipoParticipacaoOrientacao == orientacao.TipoParticipacaoOrientacao);

                        orientacao.Colaboradores = colaboradores;
                        orientacao.InstituicoesExternas = instituicoesPorColaborador[orientacao.SeqColaborador];
                    });

                    termo.OrientacaoParticipacoesColaboradores.SMCForEach(participacao =>
                    {
                        participacao.NomeColaborador = nomesColaboradores.FirstOrDefault(f => f.Seq == participacao.SeqColaborador)?.Nome;
                        participacao.ColaboradorParticipacaoConfirmacao = nomesColaboradores.FirstOrDefault(f => f.Seq == participacao.SeqColaborador)?.Nome;
                        participacao.DescricaoTipoParticipacaoOrientacao = participacao.TipoParticipacaoOrientacao.SMCGetDescription();
                        participacao.NomeInstituicaoExterna = nomesInstituicoesExternas.FirstOrDefault(f => f.Seq == participacao.SeqInstituicaoExterna)?.Nome;
                    });
                });
            }

            var paises = LocalidadeService.BuscarPaisesValidosCorreios();
            // Formata cep dos endereços brasileiros e preenche as descrições de países
            ingressanteVo.Enderecos.SMCForEach(f =>
            {
                f.DescPais = paises.FirstOrDefault(fd => fd.Codigo == f.CodigoPais)?.Nome;
                if (f.CodigoPais == LocalidadesDefaultValues.SEQ_PAIS_BRASIL)
                    f.CEP = SMCMask.ApplyMaskCEP(f.CEP);
            });

            // Recupera o grupo de escalonamento da solicitação de serviço do ingressante
            ingressanteVo.SeqGrupoEscalonamento = ingressante.SolicitacoesServico?.FirstOrDefault()?.SeqGrupoEscalonamento ?? 0;

            // Recupera o processo referente a campanha ciclo e processo seletivo, utilizado como parâmetro no lookup de grupo de escalonamento
            var filtroCampanha = new ProcessoFiltroVO() { SeqCampanhaCicloLetivo = ingressanteVo.SeqCampanhaCicloLetivo, SeqProcessoSeletivo = ingressanteVo.SeqProcessoSeletivo };
            ingressanteVo.SeqProcesso = ProcessoDomainService.BuscarProcesso(filtroCampanha)?.Seq ?? 0;

            ingressanteVo.ExigeParceriaIntercambioIngresso = configVinculo.ExigeParceriaIntercambioIngresso;

            ingressanteVo.SituacaoIngressante = this.SearchProjectionByKey(seq, p => p.HistoricosSituacao.OrderByDescending(x => x.Seq).FirstOrDefault().SituacaoIngressante);

            ingressanteVo.Enderecos.SMCForEach(f => f.Seq = f.SeqPessoaEndereco);

            PessoaAtuacaoDomainService.AplicarValidacaoPermiteAlterarCpf(ref ingressanteVo);

            return ingressanteVo;
        }

        /// <summary>
        /// Valida a configuração da atuação de ingressante na instituição e retorna um novo ingressante caso a atuação esteja configurada
        /// </summary>
        /// <returns>Novo ingressante caso esteja configurado</returns>
        /// <exception cref="InstituicaoTipoAtuacaoNaoConfiguradaException">Caso o tipo de atuação não esteja configurado na instituição</exception>
        public IngressanteVO BuscarConfiguracaoIngressante()
        {
            IngressanteVO retorno = new IngressanteVO() { TipoAtuacao = TipoAtuacao.Ingressante };

            PessoaAtuacaoDomainService.AplicarValidacaoPermiteCadastrarNomeSocial(ref retorno);

            PessoaDomainService.ValidarTipoAtuacaoConfiguradoNaInstituicao(TipoAtuacao.Ingressante);
            return retorno;
        }

        /// <summary>
        /// Atualiza a quantidade de vagas ocupadas nas ofertas de processo seletivo vinculadas às ofertas de campanha do ingressante
        /// RN_ALN_045 - Ocupar/ desocupar vaga oferta DI
        /// </summary>
        /// <param name="ingressante">Ingressante a ser processado</param>
        /// <returns>Sequenciais das divisoes vinculadas ao ingressante</returns>
        private List<long> AtualizarQuantidadeVagasPorOferta(Ingressante ingressante)
        {
            var divisoesAssociadas = new List<long>();
            var ofertasBanco = new List<long>();
            var dicOfertasDivisoesBanco = new Dictionary<long, List<long>>();
            var specProcesso = new SMCSeqSpecification<ProcessoSeletivo>(ingressante.SeqProcessoSeletivo);
            var processoSeletivoOfertas = ProcessoSeletivoDomainService.SearchProjectionByKey(specProcesso, p => p.Ofertas.Select(s => new
            {
                s.Seq,
                s.ProcessoSeletivo.ReservaVaga,
                s.QuantidadeVagas,
                s.QuantidadeVagasOcupadas,
                DivisoesTurma = s.CampanhaOferta.Itens.FirstOrDefault().Turma.DivisoesTurma
                    .Select(sd => new
                    {
                        s.SeqCampanhaOferta,
                        sd.Seq,
                        sd.SeqDivisaoComponente,
                        sd.QuantidadeVagas,
                        QuantidadeVagasOcupadas = sd.QuantidadeVagasOcupadas ?? 0
                    }).ToList(),
            })).ToList();
            var reservaVaga = processoSeletivoOfertas.Any(a => a.ReservaVaga);
            var divisoesOfertas = processoSeletivoOfertas.SelectMany(s => s.DivisoesTurma).ToList();

            if (ingressante.Seq != 0)
            {
                if (reservaVaga)
                {
                    ofertasBanco = SearchProjectionByKey(new SMCSeqSpecification<Ingressante>(ingressante.Seq), p => p.Ofertas.Select(s => s.SeqCampanhaOferta)).ToList();
                }
                else
                {
                    var divisoesTurmaSolicitacaoMatriculaIngressante = SearchProjectionByKey(new SMCSeqSpecification<Ingressante>(ingressante.Seq), p => p
                        .SolicitacoesServico.OfType<SolicitacaoMatricula>().FirstOrDefault()
                        .Itens
                        .Where(w => w.SeqDivisaoTurma.HasValue)
                        .Select(s => s.SeqDivisaoTurma.Value).ToList()
                    );
                    foreach (var seqDivisao in divisoesTurmaSolicitacaoMatriculaIngressante)
                    {
                        var seqCampanhaOferta = divisoesOfertas.First(f => f.Seq == seqDivisao).SeqCampanhaOferta;
                        if (!dicOfertasDivisoesBanco.ContainsKey(seqCampanhaOferta))
                        {
                            dicOfertasDivisoesBanco.Add(seqCampanhaOferta, new List<long>());
                            ofertasBanco.Add(seqCampanhaOferta);
                        }
                        dicOfertasDivisoesBanco[seqCampanhaOferta].Add(seqDivisao);
                    }
                }
            }

            var ofertasSelecionadas = ingressante.Ofertas?.Select(s => s.SeqCampanhaOferta).ToList() ?? new List<long>();
            var ofertasAdicionadas = ofertasSelecionadas.Except(ofertasBanco).ToArray();
            var ofertasRemovidas = ofertasBanco.Except(ofertasSelecionadas).ToArray();

            if (reservaVaga)
            {
                AtualizarQuantidadesVagasOfertas(ofertasAdicionadas, ingressante.SeqProcessoSeletivo, 1);
                AtualizarQuantidadesVagasOfertas(ofertasRemovidas, ingressante.SeqProcessoSeletivo, -1);
            }
            else
            {
                var ofertasMantidas = ofertasBanco.Except(ofertasAdicionadas).Except(ofertasRemovidas).ToArray();
                // Inclui no retorno as divisões das ofertas mantidas para atualização da solicitação de matrícula
                divisoesAssociadas.AddRange(ofertasMantidas.SelectMany(s => dicOfertasDivisoesBanco[s]));

                // Associa o ingressante à divisão com mais vagas de cada grupo das ofertas adicionadas
                foreach (var seqOfertaAdicionada in ofertasAdicionadas)
                {
                    var grupos = divisoesOfertas
                        .Where(w => w.SeqCampanhaOferta == seqOfertaAdicionada)
                        .GroupBy(g => g.SeqDivisaoComponente);
                    foreach (var grupo in grupos)
                    {
                        var divisao = grupo
                            .OrderByDescending(o => o.QuantidadeVagas - o.QuantidadeVagasOcupadas)
                            .First();
                        divisoesAssociadas.Add(divisao.Seq);
                        DivisaoTurmaDomainService.AtualizarQuantidadeVagasOcupadas(divisao.Seq, divisao.QuantidadeVagasOcupadas + 1);
                    }
                }
                // Desassocia o ingressante de todas divisões das ofertas removidas
                foreach (var seqOfertaRemovida in ofertasRemovidas)
                {
                    foreach (var seqDivisao in dicOfertasDivisoesBanco[seqOfertaRemovida])
                    {
                        var divisao = divisoesOfertas.First(f => f.Seq == seqDivisao);
                        DivisaoTurmaDomainService.AtualizarQuantidadeVagasOcupadas(divisao.Seq, divisao.QuantidadeVagasOcupadas - 1);
                    }
                }
            }

            return divisoesAssociadas;
        }

        /// <summary>
        /// Atualiza as quantidades de vagas ocupadas das ofertas de processo seletivo vinculadas às ofertas de campanha selecionadas
        /// </summary>
        /// <param name="seqsCampanhaOfertas">Sequenciais das ofertas de campanha selecionadas</param>
        /// <param name="seqProcessoSeletivo"></param>
        /// <param name="reservaVaga">Define se o processo seletivo controla vagas</param>
        /// <param name="deltaQuantidade">Quantidade a ser somada à quantidade atual</param>
        private void AtualizarQuantidadesVagasOfertas(long[] seqsCampanhaOfertas, long seqProcessoSeletivo, short deltaQuantidade)
        {
            var specIn = new SMCContainsSpecification<ProcessoSeletivoOferta, long>(p => p.SeqCampanhaOferta, seqsCampanhaOfertas);
            var specProcesso = new ProcessoSeletivoOfertaFilterSpecification() { SeqProcessoSeletivo = seqProcessoSeletivo };
            var specAnd = new SMCAndSpecification<ProcessoSeletivoOferta>(specIn, specProcesso);

            var ofertas = ProcessoSeletivoOfertaDomainService.SearchBySpecification(specAnd);
            foreach (var oferta in ofertas)
            {
                oferta.QuantidadeVagasOcupadas += deltaQuantidade;
                ProcessoSeletivoOfertaDomainService.UpdateFields(oferta, p => p.QuantidadeVagasOcupadas);
            }
        }

        /// <summary>
        /// Buscar os ingressantes convocados em que o tipo de vinculo exige curso para validar as formações específicas para liberar matricula
        /// </summary>
        /// <param name="seqsConvocados">Sequenciais dos convocados</param>
        /// <param name="seqInstituicao">Sequencial da instituição</param>
        /// <returns>Lista de sequenciais de convocados em que o tipo de vinculo exige curso</returns>
        public List<IngressanteConvocadoVinculoVO> BuscarIngressantesQueExigeCursoPorConvocados(long[] seqsConvocados, long seqInstituicao)
        {
            string query =
            @"select	ing.seq_pessoa_atuacao as SeqPessoaAtuacao,
		                ing.seq_convocado as SeqConvocado,
		                tva.ind_exige_curso as ExigeCurso,
		                tva.ind_exige_oferta_matriz_curricular as ExigeOfertaMatrizCurricular
		        from	ALN.ingressante ing
                join    ALN.instituicao_nivel_tipo_vinculo_aluno tva
                        on ing.seq_tipo_vinculo_aluno = tva.seq_tipo_vinculo_aluno
                join    ORG.instituicao_nivel itn
                        on tva.seq_instituicao_nivel = itn.seq_instituicao_nivel
                        and ing.seq_nivel_ensino = itn.seq_nivel_ensino
                where	ing.seq_convocado in ({0})
                        and itn.seq_entidade_instituicao  = {1}
                        and tva.ind_exige_curso = 1";

            return this.RawQuery<IngressanteConvocadoVinculoVO>(string.Format(query, string.Join(",", seqsConvocados), seqInstituicao));
        }

        /// <summary>
        /// Validação de contatos segundo as regras:
        /// RN_ALN_006 Consistência endereço
        /// RN_ALN_008 Consistência endereço eletrônico
        /// </summary>
        /// <param name="ingressanteVO">Ingressante com os contatos</param>
        /// <exception cref="AtuacaoSemEnderecoException">Caso a regra RN_ALN_006 não seja atendida</exception>
        /// <exception cref="AtuacaoSemEmailException">Caso a regra RN_ALN_008 não seja atendida</exception>
        public void ValidarContatosIngressante(IngressanteVO ingressanteVO)
        {
            ValidarContatosAtuacao(ingressanteVO.Enderecos, ingressanteVO.EnderecosEletronicos);
        }

        /// <summary>
        /// Validação de contatos segundo as regras:
        /// RN_ALN_006 Consistência endereço
        /// RN_ALN_008 Consistência endereço eletrônico
        /// </summary>
        /// <param name="ingressanteVO">Ingressante com os contatos</param>
        /// <exception cref="AtuacaoSemEnderecoException">Caso a regra RN_ALN_006 não seja atendida</exception>
        /// <exception cref="AtuacaoSemEmailException">Caso a regra RN_ALN_008 não seja atendida</exception>
        public void ValidarContatosAtuacao(List<PessoaAtuacaoEnderecoVO> enderecos, List<PessoaEnderecoEletronicoVO> enderecosEletronicos)
        {
            // Valida se o ingressante têm endreço acadêmico e endreço financeiro
            var quantidadeEnderecosAcademicos = enderecos.SMCCount(c => c.EnderecoCorrespondencia == EnderecoCorrespondencia.Academica || c.EnderecoCorrespondencia == EnderecoCorrespondencia.AcademicaFinanceira);
            var quantidadeEnderecosFinanceiros = enderecos.SMCCount(c => c.EnderecoCorrespondencia == EnderecoCorrespondencia.Financeira || c.EnderecoCorrespondencia == EnderecoCorrespondencia.AcademicaFinanceira);
            if (quantidadeEnderecosAcademicos != 1 || quantidadeEnderecosFinanceiros != 1)
                throw new AtuacaoSemEnderecoException();

            // Valida se o ingressante têm e-mail
            if (enderecosEletronicos.SMCCount(c => c.TipoEnderecoEletronico == TipoEnderecoEletronico.Email) == 0)
                throw new AtuacaoSemEmailException();
        }

        /// <summary>
        /// Valida se se nenhum item do grupo de escalonamento já expirou
        /// </summary>
        /// <param name="seqGrupoEscalonamento">Sequencial do grupo de escalonamento</param>
        /// <exception cref="GrupoEscalonamentoExpiradoException">Caso o grupo de escalonamento tenha um item expirado</exception>
        public void ValidarGrupoEscalonamentoIngressante(long seqGrupoEscalonamento)
        {
            var specEscalonamento = new SMCSeqSpecification<GrupoEscalonamento>(seqGrupoEscalonamento);
            var menoDataTerminoEscalonamento = GrupoEscalonamentoDomainService.SearchProjectionByKey(specEscalonamento, p => p.Itens.Min(m => m.Escalonamento.DataFim));
            if (menoDataTerminoEscalonamento < DateTime.Today)
                throw new GrupoEscalonamentoExpiradoException();
        }

        /// <summary>
        /// Valida se os termos associados ao ingressante são compatíveis
        /// RN_ALN_030 - Consistência termos e período de intercâmbio
        /// </summary>
        /// <param name="ingressanteVO">Dados do ingressante</param>
        /// <exception cref="IngressanteTermoIntercambioTipoDiferenteException">Caso os termos tenham mais de um tipo</exception>
        /// <exception cref="IngressanteTermoIntercambioParceriaDiferenteException">Caso os termos tenham mais de uma parceria</exception>
        /// <exception cref="IngressanteTermoIntercambioIntervaloVigenciaException">Caso exista algum intervalo sem vigência entre os termos</exception>
        public void ValidarTermosIntercambioIngressante(IngressanteVO ingressanteVO)
        {
            if (ingressanteVO.TermosIntercambio.SMCAny())
            {
                var specTermos = new SMCContainsSpecification<TermoIntercambio, long>(p => p.Seq, ingressanteVO.TermosIntercambio.Select(s => s.SeqTermoIntercambio).ToArray());
                var termos = TermoIntercambioDomainService.SearchProjectionBySpecification(specTermos, p => new
                {
                    p.Seq,
                    p.ParceriaIntercambioTipoTermo.SeqTipoTermoIntercambio,
                    p.SeqParceriaIntercambioInstituicaoExterna,
                    DataInicio = (DateTime?)p.Vigencias.OrderByDescending(o => o.DataInicio).FirstOrDefault().DataInicio,
                    DataFim = (DateTime?)p.Vigencias.OrderByDescending(o => o.DataInicio).FirstOrDefault().DataFim,
                    TiposMobilidade = p.TiposMobilidade.Select(s => new
                    {
                        s.TipoMobilidade,
                        s.QuantidadeVagas
                    })
                }).ToList();

                var specConfigVinculo = new InstituicaoNivelTipoVinculoAlunoFilterSpecification()
                {
                    SeqNivelEnsino = ingressanteVO.SeqNivelEnsino,
                    SeqTipoVinculoAluno = ingressanteVO.SeqTipoVinculoAluno
                };
                var configVinculo = InstituicaoNivelTipoVinculoAlunoDomainService.SearchProjectionByKey(specConfigVinculo, p => new
                {
                    TiposTermoIntercambio = p.TiposTermoIntercambio.Select(s => new
                    {
                        s.SeqTipoTermoIntercambio,
                        s.ExigePeriodoIntercambioTermo
                    })
                });

                var specPessoasTermo = new PessoaAtuacaoTermoIntercambioFilterSpecification()
                {
                    SeqsTermoIntercambio = ingressanteVO.TermosIntercambio.Select(s => s.SeqTermoIntercambio).ToArray(),
                    Ativo = true
                };
                var pessoasTermo = PessoaAtuacaoTermoIntercambioDomainService.SearchProjectionBySpecification(specPessoasTermo, p => new
                {
                    p.SeqPessoaAtuacao,
                    p.SeqTermoIntercambio,
                    p.TipoMobilidade
                }).ToList();

                foreach (var termo in termos)
                {
                    if (!termo.TiposMobilidade.SMCAny())
                        continue;
                    foreach (var tipoMobilidade in termo.TiposMobilidade)
                    {
                        if (tipoMobilidade.QuantidadeVagas <= pessoasTermo.Count(c => c.SeqTermoIntercambio == termo.Seq
                                                                                   && c.TipoMobilidade == tipoMobilidade.TipoMobilidade
                                                                                   && c.SeqPessoaAtuacao != ingressanteVO.Seq))
                        {
                            throw new IngressanteTermoIntercambioVagaExcedidaException();
                        }
                    }
                }

                if (termos.Select(s => s.SeqTipoTermoIntercambio).Distinct().Count() > 1)
                    throw new IngressanteTermoIntercambioTipoDiferenteException();

                if (termos.Select(s => s.SeqParceriaIntercambioInstituicaoExterna).Distinct().Count() > 1)
                    throw new IngressanteTermoIntercambioParceriaDiferenteException();

                if (ingressanteVO.TermosIntercambio.GroupBy(g => g.SeqTermoIntercambio).Any(a => a.Count() > 1))
                    throw new IngressanteTermoIntercambioDuplicadoException();

                // Valida se existe algum intervalo entre as vigências dos termos
                var ultimaDataFim = DateTime.MaxValue;
                foreach (var termo in termos.OrderBy(o => o.DataInicio))
                {
                    var configVinculoTermo = configVinculo.TiposTermoIntercambio.FirstOrDefault(f => f.SeqTipoTermoIntercambio == termo.SeqTipoTermoIntercambio);
                    if (configVinculoTermo == null)
                        throw new SMCApplicationException("Tipo de intercambio não configurado");
                    if (!configVinculoTermo.ExigePeriodoIntercambioTermo)
                        continue;
                    if (!termo.DataInicio.HasValue || !termo.DataFim.HasValue)
                        throw new IngressanteTermoIntercambioIntervaloVigenciaException();
                    if (termo.DataInicio.Value.AddDays(-1) > ultimaDataFim)
                        throw new IngressanteTermoIntercambioIntervaloVigenciaException();
                    ultimaDataFim = termo.DataFim.Value;
                }
            }
        }

        /// <summary>
        /// Realiza as validações de acordo com RN_ALN_043 e libera o ingressante para realizar a matrícula
        /// </summary>
        /// <param name="seq">Sequencial do ingressante</param>
        /// <exception cref="LiberacaoIngressaneInvalidaException">Caso o ingressante tenha origem ou situação inválida</exception>
        /// <exception cref="OrientacaoConvocadosException">Caso alguma orientação requerida não seja configurada</exception>
        /// <exception cref="SMCApplicationException">Caso o grupo de escalonamento do ingressante esteja expirado ou uma formação específica requerida não seja associada</exception>
        public void LiberarIngressanteMatricula(long seq)
        {
            var specIngressante = new SMCSeqSpecification<Ingressante>(seq);
            var ingressante = this.SearchProjectionByKey(specIngressante, p => new
            {
                p.Seq,
                p.Pessoa.SeqInstituicaoEnsino,
                p.OrigemIngressante,
                p.FormaIngresso.TipoVinculoAluno.TipoVinculoAlunoFinanceiro,
                p.SeqNivelEnsino,
                TokenNivelEnsino = p.NivelEnsino.Token,
                p.SolicitacoesServico.FirstOrDefault()
                    .GrupoEscalonamento
                    .Itens.OrderBy(o => o.Escalonamento.ProcessoEtapa.Ordem)
                    .FirstOrDefault().GrupoEscalonamento.NumeroDivisaoParcelas,
                SeqsBeneficioDeferido = p.Beneficios.Where(w => w.HistoricoSituacoes
                    .FirstOrDefault(f => f.Atual).SituacaoChancelaBeneficio == SituacaoChancelaBeneficio.Deferido)
                    .Select(s => s.SeqBeneficio)
                    .ToList(),
                AnoCicloLetivo = p.CampanhaCicloLetivo.CicloLetivo.Ano,
                NumeroCicloLetivo = p.CampanhaCicloLetivo.CicloLetivo.Numero,
                p.HistoricosSituacao.OrderByDescending(o => o.Seq).FirstOrDefault().SituacaoIngressante,
                p.SolicitacoesServico.OfType<SolicitacaoMatricula>().FirstOrDefault().SeqGrupoEscalonamento,
                SeqsTipoFormacaoEspecifica = p.FormacoesEspecificas.Select(s => s.FormacaoEspecifica.SeqTipoFormacaoEspecifica).ToList(),
            });

            if (ingressante.OrigemIngressante != OrigemIngressante.Manual || ingressante.SituacaoIngressante != SituacaoIngressante.AguardandoLiberacaMatricula)
                throw new LiberacaoIngressaneInvalidaException();

            /*
             * RN_ALN_043 (olhar também RN_ALN_019)
             * Passo 2.1
             * Todos os ingressantes devem possuir um grupo de escalonamento válido, ou seja,
             * todos os itens de escalonamento do grupo devem possuir uma data fim não expirada (maior ou igual a data atual).
             * Caso tenha ingressante com um ticket cadastrado para algum item do grupo, considerar este ticket como a data fim do item em questão.
             */
            //TODO: Validar ticket
            var grupoEscalonamento = this.GrupoEscalonamentoDomainService.SearchByKey(new SMCSeqSpecification<GrupoEscalonamento>(ingressante.SeqGrupoEscalonamento.GetValueOrDefault()),
                IncludesGrupoEscalonamento.Itens | IncludesGrupoEscalonamento.Itens_Escalonamento_ProcessoEtapa);
            if (grupoEscalonamento.Itens.Any(a => a.Escalonamento.DataFim < DateTime.Today))
                throw new SMCApplicationException(ImpedimentoLiberacaoMatricula.GrupoEscalonamentoComItemExpirado.SMCGetDescription());

            /*
             * Verifica se todos os ingressantes convocados atendem ao critério de tipo orientação
             */
            if (!ConvocacaoDomainService.ValidarConvocadosTipoOrientacao(ingressante.Seq))
                throw new OrientacaoConvocadosException();

            var dadosOrigem = PessoaAtuacaoDomainService.RecuperaDadosOrigem(ingressante.Seq);
            var escalonamento = grupoEscalonamento.Itens.OrderBy(o => o.Escalonamento.ProcessoEtapa.Ordem).Select(s => s.Escalonamento).First();

            /*
             * RN_ALN_043 (olhar também RN_ALN_019)
             * Passo 2.2.2
             * Todos os ingressantes, cujo vínculo foi parametrizado por instituição nível para exigir oferta de matriz,
             * devem ter formações específicas de seus cursos associadas, cujos tipos e quantidades foram parametrizados por instituição e tipo de entidade
             * para serem obrigatórios na associação do ingressante.
             */

            // Caso exija curso, verifica a formação específica.
            var configuracaoViculo = InstituicaoNivelTipoVinculoAlunoDomainService.BuscarInstituicaoNivelTipoVinculoAlunoPorPessoaAtuacao(ingressante.Seq);
            if (configuracaoViculo.ExigeCurso.GetValueOrDefault())
            {
                //Recupera as formações específicas de acordo com a instituição e o tipo entidade externada programa
                var tiposFormacaoEspecifica = InstituicaoTipoEntidadeFormacaoEspecificaDomainService.BuscarTiposObrigatorioComFilhos(ingressante.SeqInstituicaoEnsino);

                if (!tiposFormacaoEspecifica.All(a => ingressante.SeqsTipoFormacaoEspecifica.Any(n => a.SeqsFilhos.Contains(n))))
                    throw new SMCApplicationException(ImpedimentoLiberacaoMatricula.FormacaoEspecificaNaoAssociada.SMCGetDescription());
            }

            // RN_ALN_043 2.2.5
            var dadosIngressaneValidacaoFinanceiro = new IngressantesLiberacaoMatriculaData()
            {
                AnoCicloLetivo = ingressante.AnoCicloLetivo,
                NumeroCicloLetivo = ingressante.NumeroCicloLetivo,
                SeqOrigem = dadosOrigem.SeqOrigem,
                CodigoServicoOrigem = new List<long>() { dadosOrigem.CodigoServicoOrigem },
                SeqTipoVinculoAluno = dadosOrigem.SeqTipoVinculoAluno,
                DataInicioPeriodo = escalonamento.DataInicio,
                DataFimPeriodo = escalonamento.DataFim
            };
            ValidarLiberacaoMatriculaIngressanteBeneficio(
                dadosIngressaneValidacaoFinanceiro,
                ingressante.TipoVinculoAlunoFinanceiro,
                ingressante.NumeroDivisaoParcelas,
                ingressante.SeqNivelEnsino,
                ingressante.TokenNivelEnsino,
                ingressante.SeqsBeneficioDeferido);

            //Libera o ingressante para matrícula
            var ingressanteLiberado = BuscarInformacoesNotificacoesIngressanteLiberado(specIngressante);

            using (var unit = SMCUnitOfWork.Begin())
            {
                IngressanteHistoricoSituacaoDomainService.AtualizarSituacaoNaLiberacaoMatriculaIngressante(new List<long>() { ingressante.Seq });
                ConvocacaoDomainService.EnviarNotificacoesLiberacao(ingressanteLiberado);
                unit.Commit();
            }
        }

        /// <summary>
        /// Valida a liberação de matrícula de um ingressante no financeiro
        /// </summary>
        /// <param name="seqIngressante">Sequencial do ingressante</param>
        /// <param name="dataInicioPeriodo">Data de início do grupo de escalonamento</param>
        /// <param name="dataFimPeriodo">Data fim do grupo de escalonamento</param>
        public void ValidarLiberacaoMatriculaIngressanteBeneficio(long seqIngressante, DateTime dataInicioPeriodo, DateTime dataFimPeriodo, short? numeroDivisaoParcelasGrupoASerAssociado = null)
        {
            var dadosIngressante = SearchProjectionByKey(new SMCSeqSpecification<Ingressante>(seqIngressante), p => new
            {
                p.Seq,
                AnoCicloLetivo = p.CampanhaCicloLetivo.CicloLetivo.Ano,
                NumeroCicloLetivo = p.CampanhaCicloLetivo.CicloLetivo.Numero,
                p.FormaIngresso.TipoVinculoAluno.TipoVinculoAlunoFinanceiro,
                p.SeqNivelEnsino,
                TokenNivelEnsino = p.NivelEnsino.Token,
                p.SolicitacoesServico.FirstOrDefault()
                    .GrupoEscalonamento
                    .Itens.OrderBy(o => o.Escalonamento.ProcessoEtapa.Ordem)
                    .FirstOrDefault().GrupoEscalonamento.NumeroDivisaoParcelas,
                SeqsBeneficioDeferido = p.Beneficios.Where(w => w.HistoricoSituacoes
                    .FirstOrDefault(f => f.Atual).SituacaoChancelaBeneficio == SituacaoChancelaBeneficio.Deferido)
                    .Select(s => s.SeqBeneficio)
                    .ToList(),

            });

            var dadosOrigem = PessoaAtuacaoDomainService.RecuperaDadosOrigem(dadosIngressante.Seq);

            var dadosIngressaneValidacaoFinanceiro = new IngressantesLiberacaoMatriculaData()
            {
                AnoCicloLetivo = dadosIngressante.AnoCicloLetivo,
                NumeroCicloLetivo = dadosIngressante.NumeroCicloLetivo,
                SeqOrigem = dadosOrigem.SeqOrigem,
                CodigoServicoOrigem = new List<long>() { dadosOrigem.CodigoServicoOrigem },
                SeqTipoVinculoAluno = dadosOrigem.SeqTipoVinculoAluno,
                DataInicioPeriodo = dataInicioPeriodo,
                DataFimPeriodo = dataFimPeriodo
            };

            short? numeroDivisaoParcelas = dadosIngressante.NumeroDivisaoParcelas;
            if (numeroDivisaoParcelasGrupoASerAssociado.HasValue)
            {
                numeroDivisaoParcelas = numeroDivisaoParcelasGrupoASerAssociado.Value;
            }

            ValidarLiberacaoMatriculaIngressanteBeneficio(
                dadosIngressaneValidacaoFinanceiro,
                dadosIngressante.TipoVinculoAlunoFinanceiro,
                numeroDivisaoParcelas,
                dadosIngressante.SeqNivelEnsino,
                dadosIngressante.TokenNivelEnsino,
                dadosIngressante.SeqsBeneficioDeferido);
        }

        public List<NotificacaoConvocacaoVO> BuscarInformacoesNotificacoesIngressanteLiberado(SMCSpecification<Ingressante> spec)
        {
            return SearchProjectionBySpecification(spec, p => new NotificacaoConvocacaoVO
            {
                SeqIngressante = p.Seq,
                SeqSolicitacaoServico = p.SolicitacoesServico.FirstOrDefault().Seq,
                // Dados para merge de notificação
                Nome = p.DadosPessoais.Nome,
                NomeSocial = p.DadosPessoais.NomeSocial,
                DescricaoOfertaCampanha = p.Ofertas.Select(f => f.CampanhaOferta.Descricao).ToList(),
            }).ToList();
        }

        /// <summary>
        /// Valida as orientações exigidas e participações de orientações obrigatórias segundo o nível de ensino e tipo de vínculo do ingressante
        /// </summary>
        /// <param name="ingressanteVO">Ingressante com o nível de ensino, tipo de vínculo e orintações</param>
        /// <exception cref="OrientacoesExigidasPorNivelEnsinoException">Caso algum tipo de orientação exigido não seja informado</exception>
        /// <exception cref="TiposParticipacaoOrientacaoObrigatoriosException">Caso algum tipo de orientação obrigatória não seja informada</exception>
        /// <exception cref="OrigemParticipacaoOrientcaoInvalidaException">Caso alguma participação de orientação esteja associada ao tipo errado de instituição</exception>
        public void ValidarOrientacaoIngressante(IngressanteVO ingressanteVO)
        {
            var configuracaoOrientacao = InstituicaoNivelTipoOrientacaoDomainService.BuscarTiposOritencoes(new InstituicaoNivelTipoOrientacaoFilterSpecification()
            {
                SeqNivelEnsino = ingressanteVO.SeqNivelEnsino,
                SeqTipoVinculoAluno = ingressanteVO.SeqTipoVinculoAluno
            });
            var seqInstituicaoExternaLogada = InstituicaoExternaDomainService.SearchProjectionByKey(new InstituicaoExternaFilterSpecification()
            {
                SeqInstituicaoEnsino = ingressanteVO.SeqInstituicaoEnsino
            }, p => p.Seq);
            // Validação de orientações diretas
            if (configuracaoOrientacao.Any(a => !a.SeqInstituicaoNivelTipoTermoIntercambio.HasValue))
            {
                ValidarOrientacoesExigidasIngressante(
                    configuracaoOrientacao,
                    new[] { ingressanteVO.SeqTipoOrientacao.GetValueOrDefault() },
                    null);
                ValidarParticipacoesObrigatoriasOrientacaoIngressante(
                    configuracaoOrientacao,
                    ingressanteVO.OrientacaoParticipacoesColaboradores,
                    ingressanteVO.SeqTipoOrientacao.GetValueOrDefault(),
                    seqInstituicaoExternaLogada,
                    null);
                ValidarParticipacoesOrigemIngressante(
                    configuracaoOrientacao,
                    ingressanteVO.OrientacaoParticipacoesColaboradores,
                    seqInstituicaoExternaLogada,
                    ingressanteVO.SeqTipoOrientacao.GetValueOrDefault(),
                    null);
            }
            // Validação de orientações de intercâmbio
            if (configuracaoOrientacao.Any(a => a.SeqInstituicaoNivelTipoTermoIntercambio.HasValue) && ingressanteVO.TermosIntercambio.SMCAny())
            {
                var dicTipoTermoIntercambio = TermoIntercambioDomainService
                    .SearchProjectionBySpecification(
                        new SMCContainsSpecification<TermoIntercambio, long>(
                            p => p.Seq,
                            ingressanteVO.TermosIntercambio.Select(s => s.SeqTermoIntercambio).ToArray()),
                        p => new
                        {
                            p.Seq,
                            p.ParceriaIntercambioTipoTermo.SeqTipoTermoIntercambio
                        })
                    .ToDictionary(
                        k => k.Seq,
                        v => v.SeqTipoTermoIntercambio);

                foreach (var seqTipoTermo in ingressanteVO.TermosIntercambio.Select(s => s.SeqTermoIntercambio).Distinct())
                {
                    ValidarOrientacoesExigidasIngressante(
                        configuracaoOrientacao,
                        ingressanteVO.TermosIntercambio.Select(s => s.SeqTipoOrientacao.GetValueOrDefault()),
                        dicTipoTermoIntercambio[seqTipoTermo]);
                }

                ingressanteVO.TermosIntercambio.SMCForEach(termoIntercambio =>
                {
                    ValidarParticipacoesObrigatoriasOrientacaoIngressante(
                        configuracaoOrientacao,
                        termoIntercambio.OrientacaoParticipacoesColaboradores,
                        termoIntercambio.SeqTipoOrientacao.GetValueOrDefault(),
                        seqInstituicaoExternaLogada,
                        dicTipoTermoIntercambio[termoIntercambio.SeqTermoIntercambio]);
                    ValidarParticipacoesOrigemIngressante(
                        configuracaoOrientacao,
                        termoIntercambio.OrientacaoParticipacoesColaboradores,
                        seqInstituicaoExternaLogada,
                        termoIntercambio.SeqTipoOrientacao.GetValueOrDefault(),
                        dicTipoTermoIntercambio[termoIntercambio.SeqTermoIntercambio]);
                });
            }
        }

        /// <summary>
        /// Valida a quantidade de vagas por oferta para o ingressante caso o vínculo não exija curso
        /// </summary>
        /// <param name="ingressanteVO">Dados do ingressante</param>
        /// <exception cref="OfertasSemVagasException">Caso alguma das ofertas validadas não tenha vagas</exception>
        public void ValidarVagasOfertasDisciplinaIsoladaIngressante(IngressanteVO ingressanteVO)
        {
            var configHelper = CriarVOHelper(ingressanteVO);

            // RN_ALN_036
            VerificarQuantidadeDeVagasPorOferta(ingressanteVO, configHelper);
        }

        /// <summary>
        /// Exclui um ingressante e suas dependências
        /// </summary>
        /// <param name="seq">Sequencial do ingressante</param>
        public void ExcluirIngressante(long seq)
        {
            using (var unitOfWork = SMCUnitOfWork.Begin())
            {
                // As orientações não são mapeadas como owned para não conflitar com a configuração da tela de orientação
                var dadosIngressante = this.SearchProjectionByKey(seq, p => new
                {
                    SolicitacoesServico = p.SolicitacoesServico.Select(s => new
                    {
                        s.Seq,
                        s.SeqSolicitacaoHistoricoSituacaoAtual,
                        s.SeqSolicitacaoHistoricoUsuarioResponsavelAtual,
                        Etapas = s.Etapas.Select(se => new
                        {
                            se.Seq,
                            se.SeqSolicitacaoHistoricoNavegacaoAtual,
                            se.SeqSolicitacaoHistoricoSituacaoAtual
                        })
                    }),
                    p.HistoricosSituacao.OrderByDescending(x => x.Seq).FirstOrDefault().SituacaoIngressante,
                    p.OrigemIngressante,
                    Orientacoes = p.OrientacoesPessoaAtuacao.Select(s => new
                    {
                        s.Seq,
                        s.SeqOrientacao,
                        s.Orientacao.TermosIntercambio
                    }).ToList(),
                    p.SeqProcessoSeletivo,
                    p.SeqTipoVinculoAluno,
                    p.SeqNivelEnsino,
                    p.Pessoa.SeqInstituicaoEnsino,
                    p.ProcessoSeletivo.ReservaVaga
                });

                if (dadosIngressante.OrigemIngressante == OrigemIngressante.Convocacao
                || dadosIngressante.OrigemIngressante == OrigemIngressante.SelecionadoGPI)
                {
                    throw new IngressanteExclusaoNaoPermitidaOrigemException();
                }

                if (dadosIngressante.SituacaoIngressante == SituacaoIngressante.AptoMatricula
                || dadosIngressante.SituacaoIngressante == SituacaoIngressante.Matriculado
                || dadosIngressante.SituacaoIngressante == SituacaoIngressante.Desistente
                || dadosIngressante.SituacaoIngressante == SituacaoIngressante.Cancelado)
                {
                    throw new IngressanteExclusaoNaoPermitidaException();
                }

                // Caso seja DI, atualizar vagas
                var paramSpec = new InstituicaoNivelTipoVinculoAlunoFilterSpecification()
                {
                    SeqTipoVinculoAluno = dadosIngressante.SeqTipoVinculoAluno,
                    SeqNivelEnsino = dadosIngressante.SeqNivelEnsino,
                    SeqInstituicao = dadosIngressante.SeqInstituicaoEnsino
                };
                var exigeCurso = InstituicaoNivelTipoVinculoAlunoDomainService.SearchProjectionByKey(paramSpec, x => x.ExigeCurso);
                if (!exigeCurso)
                {
                    AtualizarQuantidadeVagasPorOferta(new Ingressante()
                    {
                        Seq = seq,
                        SeqProcessoSeletivo = dadosIngressante.SeqProcessoSeletivo
                    });
                }

                if (dadosIngressante.SolicitacoesServico.SMCAny())
                {
                    foreach (var solicitacao in dadosIngressante.SolicitacoesServico)
                    {
                        foreach (var etapa in solicitacao.Etapas)
                        {
                            var regEtapa = new SolicitacaoServicoEtapa()
                            {
                                Seq = etapa.Seq
                            };
                            SolicitacaoServicoEtapaDomainService.UpdateFields(regEtapa, p => p.SeqSolicitacaoHistoricoNavegacaoAtual,
                                                                                        p => p.SeqSolicitacaoHistoricoSituacaoAtual);
                        }

                        var regSolicitacao = new SolicitacaoServico()
                        {
                            Seq = solicitacao.Seq
                        };
                        SolicitacaoServicoDomainService.UpdateFields(regSolicitacao, p => p.SeqSolicitacaoHistoricoSituacaoAtual,
                                                                                     p => p.SeqSolicitacaoHistoricoUsuarioResponsavelAtual);

                        // Aparentemente como a solicitação de serviço estava em memória por conta do update anterior,
                        // o entity parou de validar se essa solicitação é uma solicitação de matrícula e deu um erro de fk
                        if (SolicitacaoMatriculaDomainService.Count(new SMCSeqSpecification<SolicitacaoMatricula>(solicitacao.Seq)) > 0)
                        {
                            SolicitacaoMatriculaDomainService.DeleteEntity(solicitacao.Seq);
                        }
                        else
                        {
                            SolicitacaoServicoDomainService.DeleteEntity(solicitacao.Seq);
                        }
                    }
                }
                if (dadosIngressante.Orientacoes.SMCAny())
                {
                    foreach (var orientacao in dadosIngressante.Orientacoes)
                    {
                        if (orientacao.TermosIntercambio.SMCAny())
                        {
                            foreach (var termo in orientacao.TermosIntercambio)
                            {
                                PessoaAtuacaoTermoIntercambioDomainService.DeleteEntity(termo);
                            }
                        }
                        OrientacaoPessoaAtuacaoDomainService.DeleteEntity(orientacao.Seq);
                        OrientacaoDomainService.DeleteEntity(orientacao.SeqOrientacao);
                    }
                }
                this.DeleteEntity(seq);
                unitOfWork.Commit();
            }
        }

        /// <summary>
        /// Valida se todos os tipos de orientação exigidas foram vinculadas ao ingressante
        /// </summary>
        /// <param name="configuracaoOrientacoes">Configurações de orientação para o vínculo do ingressante</param>
        /// <param name="seqsTipoOrientacao">Sequenciais dos tipos de orientação vinculados ao ingressante</param>
        /// <param name="seqTipoTermoIntercambio">Considera as orientações do tipo de intercambio quando setado ou "diretas" caso contrário</param>
        /// <exception cref="OrientacoesExigidasPorNivelEnsinoException">Caso algum tipo de orientação exigido não seja informado</exception>
        private void ValidarOrientacoesExigidasIngressante(IEnumerable<InstituicaoNivelTipoOrientacao> configuracaoOrientacoes, IEnumerable<long> seqsTipoOrientacao, long? seqTipoTermoIntercambio)
        {
            var tiposOrientacaoExigidos = configuracaoOrientacoes
                .Where(w => w.CadastroOrientacaoIngressante == CadastroOrientacao.Exige && w.SeqInstituicaoNivelTipoTermoIntercambio == seqTipoTermoIntercambio);
            var tiposOrientacaoFaltantes = tiposOrientacaoExigidos.Where(w => !seqsTipoOrientacao.Contains(w.SeqTipoOrientacao));
            if (tiposOrientacaoFaltantes.Any())
            {
                var specInfo = new SMCSeqSpecification<InstituicaoNivelTipoOrientacao>(configuracaoOrientacoes.First().Seq);
                var info = InstituicaoNivelTipoOrientacaoDomainService.SearchProjectionByKey(specInfo, p => new
                {
                    NivelEnsino = p.InstituicaoNivelTipoVinculoAluno.InstituicaoNivel.NivelEnsino.Descricao,
                    TipoVinculo = p.InstituicaoNivelTipoVinculoAluno.TipoVinculoAluno.Descricao
                });
                throw new OrientacoesExigidasPorNivelEnsinoException(info.NivelEnsino, info.TipoVinculo, tiposOrientacaoFaltantes.Select(s => s.TipoOrientacao.Descricao));
            }
        }

        /// <summary>
        /// Valida se todos os tipos de participação obrigatórios foram informados numa orientação de um ingressante
        /// </summary>
        /// <param name="configuracaoOrientacoes">Configurações de orientação para o vínculo do ingressante</param>
        /// <param name="orientacaoes">Orientações de um tipo</param>
        /// <param name="seqTipoOrientacao">Tipo das orientações</param>
        /// <param name="seqInstituicaoExternaLogada">Sequencial da instuição externa que repensanta a instuição logada</param>
        /// <param name="seqTipoTermoIntercambio">Sequencial do tipo do termo de intercambio (null para orientação direta)</param>
        /// <exception cref="TiposParticipacaoOrientacaoObrigatoriosException">Caso algum tipo de orientação obrigatória não seja informada</exception>
        private void ValidarParticipacoesObrigatoriasOrientacaoIngressante(IEnumerable<InstituicaoNivelTipoOrientacao> configuracaoOrientacoes, IEnumerable<IngressanteOrientacaoVO> orientacaoes, long seqTipoOrientacao, long seqInstituicaoExternaLogada, long? seqTipoTermoIntercambio)
        {
            var tiposParticipacaoObrigatorios = configuracaoOrientacoes
                .FirstOrDefault(f => f.SeqTipoOrientacao == seqTipoOrientacao && f.InstituicaoNivelTipoTermoIntercambio?.SeqTipoTermoIntercambio == seqTipoTermoIntercambio)
                ?.TiposParticipacao.Where(w => w.ObrigatorioOrientacao);
            var participacoesObrigatoriasFaltantes = tiposParticipacaoObrigatorios
                ?.Where(w => !orientacaoes
                    .SMCAny(c => c.TipoParticipacaoOrientacao == w.TipoParticipacaoOrientacao));
            if (participacoesObrigatoriasFaltantes.SMCAny())
            {
                throw new TiposParticipacaoOrientacaoObrigatoriosException(
                    tiposParticipacaoObrigatorios.Select(s => string.Format(MessagesResource.ValidacaoFormacaoEspecificaObrigatoria, s.TipoParticipacaoOrientacao.SMCGetDescription(), s.OrigemColaborador.SMCGetDescription())));
            }
        }

        /// <summary>
        /// Valida se todos os tipos de participação obrigatórios foram informados numa orientação de um ingressante
        /// </summary>
        /// <param name="configuracaoOrientacoes">Configurações de orientação para o vínculo do ingressante</param>
        /// <param name="orientacoes">Grupo de orientações do ingressante</param>
        /// <param name="seqInstituicaoExternaLogada">Sequencial da instiuição externa que representa a instiuição logada</param>
        /// <param name="seqTipoOrientacao">Sequencial do tipo da orientação do ingressante ou do termo de intercambio</param>
        /// <param name="seqTipoTermoIntercambio">Sequencial do tipo do tipo de termo de intercâmbio (ou null caso seja uma orientação direta)</param>
        /// <exception cref="OrigemParticipacaoOrientcaoInvalidaException">Caso alguma participação de orientação esteja associada ao tipo errado de instituição</exception>
        private void ValidarParticipacoesOrigemIngressante(IEnumerable<InstituicaoNivelTipoOrientacao> configuracaoOrientacoes, IEnumerable<IngressanteOrientacaoVO> orientacoes, long seqInstituicaoExternaLogada, long seqTipoOrientacao, long? seqTipoTermoIntercambio)
        {
            if (!orientacoes.SMCAny())
                return;
            foreach (var orientacao in orientacoes)
            {
                var configuracaoOrientacao = configuracaoOrientacoes
                    .FirstOrDefault(f => f.SeqTipoOrientacao == seqTipoOrientacao && f.InstituicaoNivelTipoTermoIntercambio?.SeqTipoTermoIntercambio == seqTipoTermoIntercambio);

                var origem = RecuperarOrigemColaborador(seqInstituicaoExternaLogada, orientacao);

                var origensPermitidas = configuracaoOrientacao
                    ?.TiposParticipacao
                    .Where(w => w.TipoParticipacaoOrientacao == orientacao.TipoParticipacaoOrientacao)
                    .Select(s => s.OrigemColaborador) ?? new OrigemColaborador[] { OrigemColaborador.Nenhum };

                if (!(origensPermitidas.Contains(OrigemColaborador.InternoExterno) || origensPermitidas.Contains(origem)))
                    throw new OrigemParticipacaoOrientcaoInvalidaException(orientacao.TipoParticipacaoOrientacao, origensPermitidas.FirstOrDefault());
            }
        }

        /// <summary>
        /// Recupera as formações específicas do ingressante conforme as regras RN_ALN_005.5 e RN_CAM_054.3
        /// </summary>
        /// <param name="configHelper">Configurações do tipo de vínculo do ingressante</param>
        /// <param name="ingressanteVO">Dados do ingressante</param>
        /// <returns>Formações específicas que podem ser associadas ao ingressante</returns>
        private List<IngressanteFormacaoEspecificaVO> BuscarFormacoesEspecificasIngressante(IngressanteConfigVO configHelper, IngressanteVO ingressanteVO)
        {
            // Caso o tipo de vínculo exija curso
            if (configHelper.ExigeCurso && ingressanteVO.Ofertas.SMCCount() == 1)
            {
                // Tenta recuperar a formação específica da campanha oferta selecionada
                var seqCampanhaOferta = ingressanteVO.Ofertas.First().SeqCampanhaOferta;
                var specCampanhaOfertaItem = new SMCSeqSpecification<CampanhaOferta>(seqCampanhaOferta);
                var seqFormacaoEspecificaCampanhaOfertaItem = CampanhaOfertaDomainService.SearchProjectionByKey(specCampanhaOfertaItem,
                    p => p.Itens.FirstOrDefault().SeqFormacaoEspecifica);

                // RN_CAM_054 3.1
                // Caso a CampanhaOfertaItem tenha formação, utiliza esta
                if (seqFormacaoEspecificaCampanhaOfertaItem.HasValue)
                {
                    return new List<IngressanteFormacaoEspecificaVO>() {
                    new IngressanteFormacaoEspecificaVO()
                    {
                        SeqFormacaoEspecifica = seqFormacaoEspecificaCampanhaOfertaItem.Value,
                        SeqFormacaoEspecificaOrigem = seqFormacaoEspecificaCampanhaOfertaItem
                    }};
                }
                // Caso contrário, tenta recuperar as formações do curso responsável pelo curso da campanha oferta item
                // RN_CAM_054 3.1.1
                else
                {
                    return CarregarFormacoesEspecificasEntidadeResponsavel(seqCampanhaOferta);
                }
            }
            return null;
        }

        /// <summary>
        /// Recupera a origem do colaborador na participação de orientação informada
        /// </summary>
        /// <param name="seqInstituicaoExternaLogada">Sequencial da instituição externa que representa a instuição logada</param>
        /// <param name="ingressanteOrientacaoVO">Participação de orientação a ser avaliada</param>
        /// <returns>Interno caso a instituição externa da participação informada seja a mesma da instuição logada ou externo caso contrário</returns>
        private OrigemColaborador RecuperarOrigemColaborador(long seqInstituicaoExternaLogada, IngressanteOrientacaoVO ingressanteOrientacaoVO)
        {
            return ingressanteOrientacaoVO?.SeqInstituicaoExterna == seqInstituicaoExternaLogada ? OrigemColaborador.Interno : OrigemColaborador.Externo;
        }

        /// <summary>
        /// RN_CAM_054 3.1.1
        /// </summary>
        private List<IngressanteFormacaoEspecificaVO> CarregarFormacoesEspecificasEntidadeResponsavel(long seqCampanhaOferta)
        {
            // Recupera o curso vinculado à oferta de campanha selecionada
            var seqCurso = CampanhaOfertaDomainService.SearchProjectionByKey(new SMCSeqSpecification<CampanhaOferta>(seqCampanhaOferta),
                p => p.Itens.FirstOrDefault()
                            .CursoOfertaLocalidadeTurno
                            .CursoOfertaLocalidade
                            .CursoOferta
                            .Curso
                            .Seq);

            // Recupera as formações específicas do curso com sua hierarquia
            var formacaoEspecificaCurso = FormacaoEspecificaDomainService.BuscarFormacoesEspecificasCursoComHierarquia(seqCurso);

            // Agrupa por tipo, filtrando apenas as que possuem apenas um item dentro do grupo (não é para considerar as que possuem mais de um do mesmo tipo)
            var grupo = formacaoEspecificaCurso.Select(s => s.SeqTipoFormacaoEspecifica).GroupBy(g => g).Where(f => f.Count() == 1).SelectMany(f => f);

            if (grupo.Any())
            {
                var retorno = new List<IngressanteFormacaoEspecificaVO>();
                // Busca de obrigatoriedade pelas formações específicas
                var spec = new SMCContainsSpecification<InstituicaoTipoEntidadeFormacaoEspecifica, long>(f => f.SeqTipoFormacaoEspecifica, grupo.ToArray());
                var obrigatoriedadeInfoEspecifica = InstituicaoTipoEntidadeFormacaoEspecificaDomainService.SearchProjectionBySpecification(spec,
                    p => new
                    {
                        p.SeqTipoFormacaoEspecifica,
                        p.ObrigatorioAssociacaoIngressante
                    });

                // Retorna apenas as que são únicas no curso (pelo tipo) e estão parametrizadas para serem obrigatórias para ingressante
                foreach (var itefe in obrigatoriedadeInfoEspecifica.Where(f => f.ObrigatorioAssociacaoIngressante))
                {
                    var seqFormacaoEspecifica = formacaoEspecificaCurso.FirstOrDefault(f => f.SeqTipoFormacaoEspecifica == itefe.SeqTipoFormacaoEspecifica).Seq;
                    retorno.Add(new IngressanteFormacaoEspecificaVO()
                    {
                        SeqFormacaoEspecifica = seqFormacaoEspecifica
                    });
                }
                return retorno;
            }
            return null;
        }

        /// <summary>
        /// Recupera o ingressante com seu processo seletivo para validação de vagas
        /// </summary>
        /// <param name="seq">Sequencial do ingressante</param>
        /// <returns>Dados do ingressante e do processo seletivo</returns>
        public Ingressante BuscarIngressanteProcessoSeletivo(long seq)
        {
            // Recupera os dados do ingressante
            var ingressante = SearchByKey(new SMCSeqSpecification<Ingressante>(seq), IncludesIngressante.ProcessoSeletivo);

            return ingressante;
        }

        /// <summary>
        /// Validação da liberação de ingressante no financeiro segundo a regra RN_ALN_043 2.2.1
        /// </summary>
        /// <param name="seqsConvocados"></param>
        public void ValidarLiberacaoMatriculaFinanceiro(long[] seqsConvocados)
        {
            var mensagens = new List<string>();
            var dadosIngressantes = SearchProjectionBySpecification(new IngressanteFilterSpecification() { SeqsConvocados = seqsConvocados }, p => new
            {
                p.Seq,
                p.FormaIngresso.TipoVinculoAluno.TipoVinculoAlunoFinanceiro,
                TokenNivelEnsino = p.NivelEnsino.Token,
                SeqsBeneficioDeferido = p.Beneficios.Where(w => w.HistoricoSituacoes
                    .FirstOrDefault(f => f.Atual).SituacaoChancelaBeneficio == SituacaoChancelaBeneficio.Deferido)
                    .Select(s => s.SeqBeneficio)
                    .ToList(),
                AnoCicloLetivo = p.CampanhaCicloLetivo.CicloLetivo.Ano,
                NumeroCicloLetivo = p.CampanhaCicloLetivo.CicloLetivo.Numero,
                Escalonamento = p.SolicitacoesServico.FirstOrDefault()
                    .GrupoEscalonamento.Itens.OrderBy(o => o.Escalonamento.ProcessoEtapa.Ordem).Select(s => new
                    {
                        s.SeqGrupoEscalonamento,
                        s.Escalonamento.DataInicio,
                        s.Escalonamento.DataFim,
                        s.GrupoEscalonamento.NumeroDivisaoParcelas
                    }).FirstOrDefault()
            }).ToList();

            foreach (var grupoEscalonamento in dadosIngressantes.GroupBy(g => g.Escalonamento.SeqGrupoEscalonamento))
            {
                var codigosServicoOrigem = new Dictionary<string, HashSet<long>>();
                IngressantesLiberacaoMatriculaData dadosIngressaneValidacaoFinanceiro = null;
                var parcelasDiscipliaIsolada = new HashSet<short>();
                var parcelasMestrado = new HashSet<short>();
                var parcelasDoutorado = new HashSet<short>();
                foreach (var dadosIngressante in grupoEscalonamento)
                {
                    var dadosOrigem = PessoaAtuacaoDomainService.RecuperaDadosOrigem(dadosIngressante.Seq);
                    if (!codigosServicoOrigem.ContainsKey(dadosIngressante.TokenNivelEnsino))
                    {
                        codigosServicoOrigem.Add(dadosIngressante.TokenNivelEnsino, new HashSet<long>());
                    }
                    codigosServicoOrigem[dadosIngressante.TokenNivelEnsino].Add(dadosOrigem.CodigoServicoOrigem);
                    if (dadosIngressaneValidacaoFinanceiro == null)
                    {
                        dadosIngressaneValidacaoFinanceiro = new IngressantesLiberacaoMatriculaData()
                        {
                            AnoCicloLetivo = dadosIngressante.AnoCicloLetivo,
                            NumeroCicloLetivo = dadosIngressante.NumeroCicloLetivo,
                            SeqOrigem = dadosOrigem.SeqOrigem,
                            SeqTipoVinculoAluno = dadosOrigem.SeqTipoVinculoAluno,
                            DataInicioPeriodo = dadosIngressante.Escalonamento.DataInicio,
                            DataFimPeriodo = dadosIngressante.Escalonamento.DataFim
                        };
                    }
                    if (dadosIngressante.SeqsBeneficioDeferido.SMCAny())
                    {
                        ContarParcelas(dadosIngressante.SeqsBeneficioDeferido,
                                       dadosOrigem.SeqNivelEnsino,
                                       dadosIngressante.TipoVinculoAlunoFinanceiro,
                                       dadosIngressante.TokenNivelEnsino,
                                       dadosIngressante.Escalonamento.NumeroDivisaoParcelas.GetValueOrDefault(),
                                       ref parcelasDiscipliaIsolada,
                                       ref parcelasMestrado,
                                       ref parcelasDoutorado);
                    }
                }

                dadosIngressaneValidacaoFinanceiro.CodigoServicoOrigem = codigosServicoOrigem
                    .SelectMany(s => s.Value)
                    .Distinct()
                    .ToList();
                mensagens.AddRange(ValidarLiberacaoMatriculaIngressanteParcelas(
                    ref dadosIngressaneValidacaoFinanceiro,
                    parcelasDiscipliaIsolada,
                    (dados, parcelas) => dados.QuantidadeParcelasDisciplinaIsolada = parcelas));

                dadosIngressaneValidacaoFinanceiro.CodigoServicoOrigem = codigosServicoOrigem
                    .Where(w => w.Key == TOKEN_NIVEL_ENSINO.MESTRADO_ACADEMICO || w.Key == TOKEN_NIVEL_ENSINO.MESTRADO_PROFISSIONAL)
                    .SelectMany(s => s.Value)
                    .Distinct()
                    .ToList();
                mensagens.AddRange(ValidarLiberacaoMatriculaIngressanteParcelas(
                    ref dadosIngressaneValidacaoFinanceiro,
                    parcelasMestrado,
                    (dados, parcelas) => dados.QuantidadeParcelasMestrado = parcelas));

                dadosIngressaneValidacaoFinanceiro.CodigoServicoOrigem = codigosServicoOrigem
                    .Where(w => w.Key == TOKEN_NIVEL_ENSINO.DOUTORADO_ACADEMICO || w.Key == TOKEN_NIVEL_ENSINO.DOUTORADO_PROFISSIONAL)
                    .SelectMany(s => s.Value)
                    .Distinct()
                    .ToList();
                mensagens.AddRange(ValidarLiberacaoMatriculaIngressanteParcelas(
                    ref dadosIngressaneValidacaoFinanceiro,
                    parcelasDoutorado,
                    (dados, parcelas) => dados.QuantidadeParcelasDoutorado = parcelas));
            }
            if (mensagens.SMCAny())
            {
                throw new SMCApplicationException(string.Join("<br />", mensagens.Distinct().OrderBy(o => o)));
            }
        }

        #region  [ CargaIngressante ]

        /// <summary>
        /// Processa um inscrito gerando um novo registro de ingressante e marcando o inscrito como importado no GPI
        /// </summary>
        /// <param name="inscrito">Dados do inscrito</param>
        /// <param name="seqEntidadeInstituicao">Sequencial da instituição</param>
        /// <param name="seqChamada">Sequencial da chamada</param>
        /// <returns>Sequenciais das ofertas importadas</returns>
        public void ProcessarInscrito(PessoaIntegracaoData inscrito, long seqEntidadeInstituicao, long seqChamada)
        {
            List<long> ofertasImportadas;
            using (var unitOfWork = SMCUnitOfWork.Begin())
            {
                // Cria o convocado
                var convocado = ConvocadoDomainService.ProcessarConvocado(inscrito, seqChamada);

                // Cria ou atualiza a pessoa
                var seqPessoa = PessoaDomainService.ProcessarInscrito(inscrito, seqEntidadeInstituicao);

                // Cria um novo registro de ingressante
                CriarIngressante(inscrito, seqChamada, seqEntidadeInstituicao, convocado.Seq, seqPessoa);

                // Caso não ocorra nenhum erro na importação, marca todas as ofertas como importadas para enviar para o GPI.
                ofertasImportadas = inscrito.Ofertas.Select(f => f.SeqInscricaoOferta).ToList();

                // FIX: O método de atualizar a exportação no GPI não é executado em uma transação pois utiliza ExecuteSqlCommandAsync para salvar as
                // modificações no banco.
                // Informa ao GPI quais inscrições oferta foram importadas com sucesso.
                IntegracaoService.AtualizarExportacaoInscricao(ofertasImportadas);

                unitOfWork.Commit();
            }
        }

        /// <summary>
        /// Valida a quantidade de vagas por oferta para o ingressante caso o vínculo não exija curso
        /// </summary>
        /// <param name="ingressanteVO">Dados do ingressante</param>
        /// <exception cref="OfertasSemVagasException">Caso alguma das ofertas validadas não tenha vagas</exception>
        public void ValidarBloqueioPessoa(IngressanteVO ingressanteVO)
        {
            const int tipoDesligamentoAluno = 1;
            var filtro = new PessoaDesligadaFiltroData()
            {
                Data = DateTime.Today,
                TipoDesligamento = tipoDesligamentoAluno
            };

            if (ingressanteVO.TipoNacionalidade != TipoNacionalidade.Estrangeira)
            {
                if (!string.IsNullOrEmpty(ingressanteVO.Cpf))
                {
                    filtro.Cpf = ingressanteVO.Cpf.SMCRemoveNonDigits();
                }
                //else if (!string.IsNullOrEmpty(ingressanteVO.NumeroPassaporte))
                //{
                //    filtro.Passaporte = ingressanteVO.NumeroPassaporte?.Trim();
                //}
                else
                {
                    throw new SMCApplicationException("O CPF ou passaporte do ingressante deve ser informado");
                }

                if (PessoaService.VerificarDadosPessoa(filtro))
                {
                    throw new IngressanteBloqueioPessoaException();
                }
            }
        }

        private void CriarIngressante(PessoaIntegracaoData inscrito, long seqChamada, long seqInstituicaoEnsino, long seqConvocado, long seqPessoa)
        {
            var seqOfertas = inscrito.Ofertas.Select(f => f.SeqOferta);
            var dados = ChamadaDomainService.SearchProjectionByKey(new SMCSeqSpecification<Chamada>(seqChamada),
                                            x => new
                                            {
                                                x.SeqGrupoEscalonamento,
                                                x.Convocacao.SeqCampanhaCicloLetivo,
                                                x.Convocacao.CampanhaCicloLetivo.SeqCicloLetivo,
                                                x.Convocacao.ProcessoSeletivo.SeqFormaIngresso,
                                                x.Convocacao.SeqProcessoSeletivo,
                                                x.Convocacao.ProcessoSeletivo.SeqTipoVinculoAluno,
                                                x.Convocacao.ProcessoSeletivo.Campanha.SeqEntidadeResponsavel,

                                                ProcessoSeletivoOferta = x.Convocacao.ProcessoSeletivo.Ofertas.Where(f => seqOfertas.Contains(f.SeqHierarquiaOfertaGpi.Value)) //    f.SeqHierarquiaOfertaGpi == seqPrimeiraOferta)
                                                                    .Select(f => new
                                                                    {
                                                                        f.SeqHierarquiaOfertaGpi,
                                                                        f.SeqCampanhaOferta,
                                                                        f.CampanhaOferta.TipoOferta.ExigeCursoOfertaLocalidadeTurno,
                                                                        CampanhaOfertaItens = f.CampanhaOferta.Itens,
                                                                        TipoOferta = f.CampanhaOferta.TipoOferta.Token,
                                                                        f.CampanhaOferta.Itens.FirstOrDefault().SeqColaborador
                                                                    })
                                            });

            var ingressanteVO = ConverteCargaIngressanteVO(inscrito);
            ingressanteVO.SeqInstituicaoEnsino = seqInstituicaoEnsino;
            ingressanteVO.SeqGrupoEscalonamento = dados.SeqGrupoEscalonamento;
            ingressanteVO.SeqCampanhaCicloLetivo = dados.SeqCampanhaCicloLetivo;
            ingressanteVO.SeqCicloLetivo = dados.SeqCicloLetivo;
            ingressanteVO.SeqFormaIngresso = dados.SeqFormaIngresso.GetValueOrDefault();
            ingressanteVO.SeqNivelEnsino = BuscarSeqNivelEnsino(seqChamada,
                                                                seqOfertas.First(),
                                                                inscrito.TokenNivelEnsino,
                                                                dados.ProcessoSeletivoOferta.First().ExigeCursoOfertaLocalidadeTurno);
            ingressanteVO.SeqProcessoSeletivo = dados.SeqProcessoSeletivo;
            ingressanteVO.SeqTipoVinculoAluno = dados.SeqTipoVinculoAluno.GetValueOrDefault();
            ingressanteVO.SeqPessoa = seqPessoa;

            ingressanteVO.SeqConvocado = seqConvocado;
            ingressanteVO.SeqEntidadeResponsavel = dados.SeqEntidadeResponsavel;

            ValidarBloqueioPessoa(ingressanteVO);

            // Recupera o processo referente a campanha ciclo e processo seletivo, utilizado como parâmetro no lookup de grupo de escalonamento
            var filtroCampanha = new ProcessoFiltroVO() { SeqCampanhaCicloLetivo = ingressanteVO.SeqCampanhaCicloLetivo, SeqProcessoSeletivo = ingressanteVO.SeqProcessoSeletivo };
            ingressanteVO.SeqProcesso = ProcessoDomainService.BuscarProcesso(filtroCampanha)?.Seq ?? 0;

            // RN_CAM_54 (As validações desta regra estão no IngressanteDomainService)
            foreach (var oferta in inscrito.Ofertas)
            {
                var processoSeletivoOferta = dados.ProcessoSeletivoOferta.FirstOrDefault(f => f.SeqHierarquiaOfertaGpi == oferta.SeqOferta);

                ingressanteVO.Ofertas.Add(new IngressanteOfertaVO()
                {
                    SeqInscricaoOfertaGpi = oferta.SeqInscricaoOferta,
                    SeqCampanhaOferta = processoSeletivoOferta.SeqCampanhaOferta,
                    SeqCampanhaOfertaOrigem = processoSeletivoOferta.SeqCampanhaOferta,
                    SeqCampanhaOfertaItem = (processoSeletivoOferta.CampanhaOfertaItens.Count == 1) ?
                                                    processoSeletivoOferta.CampanhaOfertaItens.First().Seq as long? :
                                                    null
                });
            }

            // RN_CAM_051
            if (dados.ProcessoSeletivoOferta.First().TipoOferta == TOKEN_TIPO_OFERTA.ORIENTADOR)
            {
                ingressanteVO.SeqOrientador = dados.ProcessoSeletivoOferta.First().SeqColaborador;
            }

            // Verifica se o ingressante já existia no banco
            var ingressante = SearchProjectionByKey(new IngressanteConvocadoSpecification() { SeqConvocado = seqConvocado },
                                                    x => new
                                                    {
                                                        x.Seq,
                                                        x.Ofertas,
                                                        HistoricosSituacaoSolicitacao = x.SolicitacoesServico.Where(f => f is SolicitacaoMatricula)
                                                                                    .SelectMany(s => s.Etapas.SelectMany(d => d.HistoricosSituacao))
                                                    });
            if (ingressante != null)
            {
                ingressanteVO.Seq = ingressante.Seq;
                ingressanteVO.Ofertas.AddRange(ingressante.Ofertas.TransformList<IngressanteOfertaVO>());

                // Verifica se a solicitação de matricula do ingressante referente ao convocado encontrado já foi iniciada.
                var seqSituacaoEtapaSgf = ingressante
                    .HistoricosSituacaoSolicitacao
                    .OrderByDescending(f => f.DataInclusao)
                    .First(f => !f.DataExclusao.HasValue)
                    .SeqSituacaoEtapaSgf;
                var etapa = EtapaService.BuscarSituacaoEtapa(seqSituacaoEtapaSgf);
                if (etapa.Situacao.Token != TOKEN_SITUACAO_ETAPA_SGF.AGUARDANDO_SOLICITACAO_MATRICULA && etapa.Situacao.Token != TOKEN_SITUACAO_ETAPA_SGF.SOLICITACAO_MATRICULA_INICIADA)
                {
                    throw new IngressanteComSituacaoMatriculaInvalidaException(etapa.Situacao.Descricao);
                }
            }

            SalvarIngressante(ingressanteVO);
        }

        private IngressanteVO ConverteCargaIngressanteVO(PessoaIntegracaoData inscrito)
        {
            var ingressanteVO = inscrito.Transform<IngressanteVO>();
            ingressanteVO.Ativo = true;
            ingressanteVO.OrigemIngressante = OrigemIngressante.SelecionadoGPI;
            // Apaga as ofertas que foram mapeadas. O mapeamento correto será feito posteriormente.
            ingressanteVO.Ofertas = new List<IngressanteOfertaVO>();

            ingressanteVO.RacaCor = SMCEnumHelper.GetEnumSafely<RacaCor?>(inscrito.RacaCor);

            // Converte os dados que não são string para os seus tipos corretos.
            ingressanteVO.TipoPisPasep = SMCEnumHelper.GetEnumSafely<TipoPisPasep?>(inscrito.TipoPisPasep);
            if (DateTime.TryParse(inscrito.DataPisPasep, out DateTime dataPisPasep))
                ingressanteVO.DataPisPasep = dataPisPasep;

            ingressanteVO.TipoDocumentoMilitar = SMCEnumHelper.GetEnumSafely<TipoDocumentoMilitar?>(inscrito.TipoDocumentoMilitar);

            if (bool.TryParse(inscrito.NecessidadeEspecial, out bool necessidadeEspecial))
                ingressanteVO.NecessidadeEspecial = necessidadeEspecial;
            ingressanteVO.TipoNecessidadeEspecial = SMCEnumHelper.GetEnumSafely<TipoNecessidadeEspecial?>(inscrito.TipoNecessidadeEspecial);

            // Remove quaisquer dados repetidos cadastrados pelo usuário
            RemoveDuplicates(ingressanteVO);

            // Converte os dados da filiação
            ingressanteVO.Filiacao = new List<PessoaFiliacaoVO>();
            if (!string.IsNullOrWhiteSpace(inscrito.NomeMae))
            {
                ingressanteVO.Filiacao.Add(new PessoaFiliacaoVO
                {
                    TipoParentesco = TipoParentesco.Mae,
                    Nome = inscrito.NomeMae
                });
            }
            if (!string.IsNullOrWhiteSpace(inscrito.NomePai))
            {
                ingressanteVO.Filiacao.Add(new PessoaFiliacaoVO
                {
                    TipoParentesco = TipoParentesco.Pai,
                    Nome = inscrito.NomePai
                });
            }

            return ingressanteVO;
        }

        private long BuscarSeqNivelEnsino(long seqChamada, long seqOferta, string tokenNivelEnsino, bool exigeCursoOfertaLocalidadeTurno)
        {
            if (exigeCursoOfertaLocalidadeTurno)
            {
                // Busca o nível de ensino
                return ChamadaDomainService.SearchProjectionByKey(new SMCSeqSpecification<Chamada>(seqChamada),
                                                x => x.Convocacao.ProcessoSeletivo.Ofertas.FirstOrDefault(f => f.SeqHierarquiaOfertaGpi == seqOferta)
                                                        .CampanhaOferta.Itens.FirstOrDefault().CursoOfertaLocalidadeTurno.CursoOfertaLocalidade.CursoOferta.Curso.SeqNivelEnsino);
            }
            else
            {
                // Se nao exigir curso oferta localidade turno, busca a entidade e os cursos associadas a ela.
                var seqEntidadeResponsavel = ChamadaDomainService.SearchProjectionByKey(new SMCSeqSpecification<Chamada>(seqChamada),
                                                x => x.Convocacao.ProcessoSeletivo.Ofertas.FirstOrDefault(f => f.SeqHierarquiaOfertaGpi == seqOferta)
                                                        .CampanhaOferta.Campanha.SeqEntidadeResponsavel);

                // Busca os sequenciais dos cursos da entidade.
                var seqsCurso = HierarquiaEntidadeItemDomainService.BuscarHierarquiaEntidadeItensPorEntidadeVisaoOganizacional(seqEntidadeResponsavel, TOKEN_TIPO_ENTIDADE_EXTERNADA.CURSO);

                // Busca o seq do nivel de ensino e os cursos oferta.
                var cursos = CursoDomainService.SearchProjectionBySpecification(new SMCContainsSpecification<Curso, long>(x => x.Seq, seqsCurso.ToArray()),
                                                                        x => new
                                                                        {
                                                                            x.SeqNivelEnsino,
                                                                            x.CursosOferta
                                                                        });
                // Filtra os cursos por apenas os que estão ativos.
                var cursosAtivos = cursos.Where(x => x.CursosOferta.Any(f => f.Ativo));

                // Se possuir apenas um curso ativo, retorna o nivel de ensino deste
                if (cursosAtivos.Count() == 1)
                {
                    return cursos.FirstOrDefault().SeqNivelEnsino;
                }
                else
                {
                    // Se possuir mais de um curso, retorna o nivel de ensino do curso cujo token é NIVEL_ENSINO no formulário de inscrição.
                    return NivelEnsinoDomainService.SearchProjectionByKey(new NivelEnsinoFilterSpecification() { Token = tokenNivelEnsino }, x => x.Seq);
                }
            }
        }

        private static void RemoveDuplicates(IngressanteVO ingressanteVO)
        {
            // Remove telefones duplicados
            ingressanteVO.Telefones = ingressanteVO.Telefones.SMCDistinct(g => new { g.CodigoArea, g.CodigoPais, g.Numero, g.TipoTelefone }).ToList();

            // Remove endereços eletronicos duplicados
            ingressanteVO.EnderecosEletronicos = ingressanteVO.EnderecosEletronicos.SMCDistinct(f => new { f.DescricaoEnderecoEletronico, f.TipoEnderecoEletronico }).ToList();
        }

        #endregion [ CargaIngressante ]

        /// <summary>
        /// Valida a liberação de matricula de um ingressante com a quantidade de parcelas dos seus benefícios
        /// </summary>
        /// <param name="data">Dados para liberação de matrícula sem a quantidade de parcelas dos benefícios</param>
        /// <param name="tipoVinculo">Tipo de vínculo do aluno</param>
        /// <param name="quantidadeParcelasGrupoEscalonamento">Quantidade de parcelas do grupo de escalonamento da solicitação de serviço do ingressante</param>
        /// <param name="seqNivelEnsino">Seq do nível de ensino do ingressante</param>
        /// <param name="tokenNivelEnsino">Token do nível de ensino do ingressante</param>
        /// <param name="seqsBeneficiosDeferidos">Sequenciais dos benefícios deferidos do ingressante</param>
        private void ValidarLiberacaoMatriculaIngressanteBeneficio(
            IngressantesLiberacaoMatriculaData data,
            TipoVinculoAlunoFinanceiro tipoVinculo,
            int? quantidadeParcelasGrupoEscalonamento,
            long seqNivelEnsino,
            string tokenNivelEnsino,
            List<long> seqsBeneficiosDeferidos)
        {
            if (tipoVinculo == TipoVinculoAlunoFinanceiro.RegimeDisciplinaIsolada)
            {
                data.QuantidadeParcelasDisciplinaIsolada = quantidadeParcelasGrupoEscalonamento;
                try
                {
                    FinanceiroService.ValidarLiberacaoMatriculaIngressantes(data);
                }
                catch (SMCApplicationException ex)
                {
                    throw new SMCApplicationException(string.Format(MessagesResource.MSG_QuantidadeParcelas, ex.SMCLastInnerException().Message, quantidadeParcelasGrupoEscalonamento));
                }
            }
            if (!seqsBeneficiosDeferidos.SMCAny())
            {
                FinanceiroService.ValidarLiberacaoMatriculaIngressantes(data);
            }
            else
            {
                var specInstituicaoNivelBeneficio = new InstituicaoNivelBeneficioFilterSpecification()
                {
                    SeqNivelEnsino = seqNivelEnsino,
                    SeqsBeneficio = seqsBeneficiosDeferidos
                };
                var parcelasBeneficios = InstituicaoNivelBeneficioDomainService
                    .SearchProjectionBySpecification(specInstituicaoNivelBeneficio, p => p.NumeroParcelasPadraoCondicaoPagamento)
                    .Distinct()
                    .ToList();
                if (!parcelasBeneficios.SMCAny())
                {
                    parcelasBeneficios.Add(null);
                }
                var mensagems = new List<string>();
                foreach (var parcelas in parcelasBeneficios)
                {
                    if (tokenNivelEnsino == TOKEN_NIVEL_ENSINO.MESTRADO_ACADEMICO)
                    {
                        data.QuantidadeParcelasMestrado = parcelas;
                    }
                    else if (tokenNivelEnsino == TOKEN_NIVEL_ENSINO.DOUTORADO_ACADEMICO)
                    {
                        data.QuantidadeParcelasDoutorado = parcelas;
                    }
                    try
                    {
                        FinanceiroService.ValidarLiberacaoMatriculaIngressantes(data);
                    }
                    catch (SMCApplicationException ex)
                    {
                        mensagems.Add(string.Format(MessagesResource.MSG_QuantidadeParcelas, ex.SMCLastInnerException().Message, parcelas));
                    }
                }
                if (mensagems.SMCAny())
                {
                    throw new SMCApplicationException(string.Join("<br>", mensagems.Distinct().OrderBy(o => o)));
                }
            }
        }

        private void ContarParcelas(List<long> seqsBeneficioDeferido,
                                    long seqNivelEnsino,
                                    TipoVinculoAlunoFinanceiro tipoVinculoAlunoFinanceiro,
                                    string tokenNivelEnsino,
                                    short quantidadeParcelasGrupoEscalonamento,
                                    ref HashSet<short> parcelasDiscipliaIsolada,
                                    ref HashSet<short> parcelasMestrado,
                                    ref HashSet<short> parcelasDoutorado)
        {
            if (tipoVinculoAlunoFinanceiro == TipoVinculoAlunoFinanceiro.RegimeDisciplinaIsolada)
            {
                parcelasDiscipliaIsolada.Add(quantidadeParcelasGrupoEscalonamento);
                return;
            }
            if (!seqsBeneficioDeferido.SMCAny())
            {
                return;
            }
            foreach (var seqBeneficio in seqsBeneficioDeferido)
            {
                var specInstinuicaoNivelBeneficio = new InstituicaoNivelBeneficioFilterSpecification()
                {
                    SeqNivelEnsino = seqNivelEnsino,
                    SeqBeneficio = seqBeneficio
                };
                var quantidadeParcelas = InstituicaoNivelBeneficioDomainService
                    .SearchProjectionBySpecification(specInstinuicaoNivelBeneficio, p => p.NumeroParcelasPadraoCondicaoPagamento)
                    .FirstOrDefault();
                if (quantidadeParcelas == null)
                {
                    continue;
                }
                if (tokenNivelEnsino == TOKEN_NIVEL_ENSINO.MESTRADO_ACADEMICO)
                {
                    parcelasMestrado.Add(quantidadeParcelas.Value);
                }
                else if (tokenNivelEnsino == TOKEN_NIVEL_ENSINO.DOUTORADO_ACADEMICO)
                {
                    parcelasDoutorado.Add(quantidadeParcelas.Value);
                }
            }
        }

        private List<string> ValidarLiberacaoMatriculaIngressanteParcelas(
            ref IngressantesLiberacaoMatriculaData dadosIngressaneValidacaoFinanceiro,
            IEnumerable<short> quantidadesParcelas,
            Action<IngressantesLiberacaoMatriculaData, int> setarValorTipoParcela
            )
        {
            var mensagens = new List<string>();
            foreach (var quantidadeParcelas in quantidadesParcelas)
            {
                dadosIngressaneValidacaoFinanceiro.QuantidadeParcelasDisciplinaIsolada = null;
                dadosIngressaneValidacaoFinanceiro.QuantidadeParcelasMestrado = null;
                dadosIngressaneValidacaoFinanceiro.QuantidadeParcelasDoutorado = null;
                setarValorTipoParcela(dadosIngressaneValidacaoFinanceiro, quantidadeParcelas);
                try
                {
                    FinanceiroService.ValidarLiberacaoMatriculaIngressantes(dadosIngressaneValidacaoFinanceiro);
                }
                catch (SMCApplicationException ex)
                {
                    mensagens.Add(string.Format(MessagesResource.MSG_QuantidadeParcelas, ex.SMCLastInnerException().Message, quantidadeParcelas));
                }
            }
            return mensagens;
        }
    }
}