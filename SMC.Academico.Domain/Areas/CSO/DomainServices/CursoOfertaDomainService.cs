using SMC.Academico.Common.Areas.CSO.Constants;
using SMC.Academico.Common.Areas.CSO.Exceptions;
using SMC.Academico.Common.Areas.CSO.Includes;
using SMC.Academico.Common.Areas.ORG.Enums;
using SMC.Academico.Domain.Areas.APR.DomainServices;
using SMC.Academico.Domain.Areas.APR.Specifications;
using SMC.Academico.Domain.Areas.CSO.Models;
using SMC.Academico.Domain.Areas.CSO.Resources;
using SMC.Academico.Domain.Areas.CSO.Specifications;
using SMC.Academico.Domain.Areas.CSO.ValueObjects;
using SMC.Academico.Domain.Areas.PES.DomainServices;
using SMC.Academico.Domain.Areas.SRC.DomainServices;
using SMC.Academico.Domain.Areas.SRC.Models;
using SMC.Framework;
using SMC.Framework.Extensions;
using SMC.Framework.Model;
using SMC.Framework.Specification;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SMC.Academico.Domain.Areas.CSO.DomainServices
{
    public class CursoOfertaDomainService : AcademicoContextDomain<CursoOferta>
    {
        #region [ DomainService ]

        private CursoDomainService CursoDomainService => Create<CursoDomainService>();

        private CursoFormacaoEspecificaDomainService CursoFormacaoEspecificaDomainService => Create<CursoFormacaoEspecificaDomainService>();

        private CursoOfertaLocalidadeDomainService CursoOfertaLocalidadeDomainService => Create<CursoOfertaLocalidadeDomainService>();

        private FormacaoEspecificaDomainService FormacaoEspecificaDomainService => Create<FormacaoEspecificaDomainService>();

        private GrauAcademicoDomainService GrauAcademicoDomainService => Create<GrauAcademicoDomainService>();

        private HistoricoEscolarDomainService HistoricoEscolarDomainService => Create<HistoricoEscolarDomainService>();

        private InstituicaoNivelTipoOfertaCursoDomainService InstituicaoNivelTipoOfertaCursoDomainService => Create<InstituicaoNivelTipoOfertaCursoDomainService>();

        private PessoaAtuacaoDomainService PessoaAtuacaoDomainService => Create<PessoaAtuacaoDomainService>();

        private ProcessoDomainService ProcessoDomainService => Create<ProcessoDomainService>();

        #endregion [ DomainService ]

        /// <summary>
        /// Busca cursos oferta de acordo com o sequencial
        /// </summary>
        /// <param name="seq">Sequencial de curso oferta</param>
        /// <returns>Objeto curso oferta</returns>
        public CursoOfertaVO BuscarCursoOferta(long seq)
        {
            var registroVo = new CursoOfertaVO();

            var includes = IncludesCursoOferta.Curso |
                           IncludesCursoOferta.Curso_HierarquiasEntidades |
                           IncludesCursoOferta.Curso_HistoricoSituacoes_SituacaoEntidade |
                           IncludesCursoOferta.Curso_NivelEnsino;

            var registro = this.SearchByKey(new SMCSeqSpecification<CursoOferta>(seq), includes);

            if (registro != null)
            {
                registroVo = registro.Transform<CursoOfertaVO>();
            }

            return registroVo;
        }

        public List<CursoOfertaVO> BuscarCursosOferta(long[] seqs)
        {
            var registroVo = new CursoOfertaVO();

            var includes = IncludesCursoOferta.Curso |
                           IncludesCursoOferta.Curso_HierarquiasEntidades |
                           IncludesCursoOferta.Curso_HistoricoSituacoes_SituacaoEntidade |
                           IncludesCursoOferta.Curso_NivelEnsino;

            var ret = this.SearchBySpecification(new SMCContainsSpecification<CursoOferta, long>(x => x.Seq, seqs), includes);

            return ret.TransformList<CursoOfertaVO>();
        }

        /// <summary>
        /// Buscar os cursos ofertas que foram cadastrados para os cursos de mesma hierarquia
        /// </summary>
        /// <param name="filtros">Filtros da listagem de cursos ofertas</param>
        /// <returns>SMCPagerData de cursos fertas</returns>
        public SMCPagerData<CursoOfertaVO> BuscarCursoOfertasLookup(CursoOfertaFilterSpecification filtros)
        {
            int total = 0;
            var includes = IncludesCursoOferta.Curso |
                           IncludesCursoOferta.Curso_HierarquiasEntidades |
                           IncludesCursoOferta.Curso_HistoricoSituacoes_SituacaoEntidade |
                           IncludesCursoOferta.Curso_NivelEnsino;

            // Os registros retornados no resultado da pesquisa deverão ser ordenados por default na seguinte ordem:
            //Nível de Ensino / Curso / Oferta de Curso.
            filtros.SetOrderBy(o => o.Curso.NivelEnsino.Descricao)
                   .SetOrderBy(o => o.Curso.Nome)
                   .SetOrderBy(o => o.Descricao);

            var cursoOfertas = this.SearchBySpecification(filtros, out total, includes);

            return new SMCPagerData<CursoOfertaVO>(cursoOfertas.TransformList<CursoOfertaVO>(), total);
        }

        /// <summary>
        /// Buscar as ofertas de curso ativas para o curso informado
        /// </summary>
        /// <param name="seqCurso">Sequencial do curso</param>
        /// <returns>Select item com os valores de ofertas para o curso</returns>
        public List<SMCDatasourceItem> BuscarCursoOfertasAtivasSelect(long seqCurso)
        {
            var specCursoOferta = new CursoOfertaFilterSpecification() { SeqCurso = seqCurso, Ativo = true };

            var ofertasAtivasCurso = this.SearchProjectionBySpecification(specCursoOferta, p => new SMCDatasourceItem() { Seq = p.Seq, Descricao = p.Descricao })
                .OrderBy(o => o.Descricao);

            return ofertasAtivasCurso.ToList();
        }

        /// <summary>
        /// Verficia se já foram cadastrados tipos de curso para o nível de ensino do curso informado
        /// </summary>
        /// <param name="seqCurso">Sequencial do curso</param>
        /// <returns>Modelo de curso oferta caso os pré-requisitos sejam atendidos</returns>
        public CursoOfertaVO VerificarDependenciasCursoOferta(long seqCurso)
        {
            var retorno = new CursoOfertaVO();

            var cursoFormacaoEspecifica = this.CursoFormacaoEspecificaDomainService.BuscarCursoFormacaoEspecificaSelect(seqCurso);
            var specCurso = new SMCSeqSpecification<Curso>(seqCurso);
            var curso = this.CursoDomainService.SearchProjectionByKey(specCurso, p => new { p.NivelEnsino.Descricao, p.Nome });

            if (cursoFormacaoEspecifica.SMCCount() == 0)
                throw new OfertaCursoSemFormacaoEspecificaException(curso.Nome);
            if (this.InstituicaoNivelTipoOfertaCursoDomainService.BuscarInstituicaoNivelTipoOfertaCursoSelect(seqCurso).SMCCount() == 0)
                throw new InstituicaoNivelTipoOfertaCursoNaoAssociadoException(curso.Descricao);

            retorno.Ativo = true;
            retorno.DataLiberacao = DateTime.Now;
            retorno.Descricao = curso.Nome;

            return retorno;
        }

        /// <summary>
        /// Recpera a máscara de nome de uma formação específica segundo a regra RN_CSO_023 - Mascara - Oferta de Curso
        /// </summary>
        /// <param name="seqFormacaoEspecifica">Sequencial da formação específica selecionada</param>
        /// <param name="seqCurso">Sequencial do curso selecionado</param>
        /// <returns>Máscara do curso oferta segundo a regra RN_CSO_023</returns>
        public string RecuperarMascaraCursoOferta(long seqFormacaoEspecifica, long seqCurso)
        {
            var specFormacao = new SMCSeqSpecification<FormacaoEspecifica>(seqFormacaoEspecifica);
            var dadosFormacao = this.FormacaoEspecificaDomainService.SearchProjectionByKey(specFormacao, p => new
            {
                p.Descricao,
                p.TipoFormacaoEspecifica.Token
            });

            var specCurso = new SMCSeqSpecification<Curso>(seqCurso);
            var dadosCurso = this.CursoDomainService.SearchProjectionByKey(specCurso, p => new
            {
                p.Nome,
                p.TipoCurso
            });

            if (dadosCurso != null && seqFormacaoEspecifica == 0)
                return dadosCurso.Nome;
            else if (dadosFormacao == null)
                return string.Empty;

            var tokensMascaraFormacaoEspecifica = new string[]
            {
                TOKEN_TIPO_FORMACAO_ESPECIFICA.AREA_CONCENTRACAO,
                TOKEN_TIPO_FORMACAO_ESPECIFICA.LINHA_PESQUISA,
                TOKEN_TIPO_FORMACAO_ESPECIFICA.AREA_TEMATICA,
                TOKEN_TIPO_FORMACAO_ESPECIFICA.APROFUNDAMENTO,
                TOKEN_TIPO_FORMACAO_ESPECIFICA.LINHA_FORMACAO,
                TOKEN_TIPO_FORMACAO_ESPECIFICA.EIXO_TEMATICO,
                TOKEN_TIPO_FORMACAO_ESPECIFICA.HABILITACAO,
                TOKEN_TIPO_FORMACAO_ESPECIFICA.ENFASE
            };

            if (tokensMascaraFormacaoEspecifica.Contains(dadosFormacao.Token))
                return string.Format(MessagesResource.MascaraCursoCompletaSemGrau, dadosCurso.Nome, dadosFormacao.Descricao);
            else if (dadosFormacao.Token == TOKEN_TIPO_FORMACAO_ESPECIFICA.CURSO)
                return dadosFormacao.Descricao;
            else
                return string.Empty;
        }

        /// <summary>
        /// Buscar as ofertas de curso para o processo informado
        /// </summary>
        /// <param name="seqProcesso">Sequencial do processo</param>
        /// <returns>Select item com os valores de ofertas para o processo</returns>
        public List<SMCDatasourceItem> BuscarCursosOfertasPorProcessoSelect(long seqProcesso)
        {
            var processo = this.ProcessoDomainService.SearchProjectionByKey(new SMCSeqSpecification<Processo>(seqProcesso), p =>
                new
                {
                    SeqProcesso = p.Seq,
                    SeqsEntidadesResponsaveis = p.UnidadesResponsaveis.Select(u => (long?)u.SeqEntidadeResponsavel).ToList(),
                    SeqsNiveisEnsino = p.Configuracoes.SelectMany(c => c.NiveisEnsino).Select(n => n.SeqNivelEnsino).ToList()
                });

            var spec = new CursoOfertaFilterSpecification()
            {
                SeqsEntidadesResponsaveis = processo.SeqsEntidadesResponsaveis,
                SeqNivelEnsino = processo.SeqsNiveisEnsino
            };

            spec.SetOrderBy(w => w.Descricao);

            return this.SearchProjectionBySpecification(spec, c => new SMCDatasourceItem()
            {
                Seq = c.Seq,
                Descricao = c.Descricao
            }).ToList();
        }

        /// <summary>
        /// Buscar as ofertas de curso para o aluno de acordo com o histórico escolar
        /// </summary>
        /// <param name="seqPessoaAtuacao">Sequencial da pessoa atuação aluno</param>
        /// <returns>Select item com os valores de ofertas que posssuem histórico escolar</returns>
        public List<SMCDatasourceItem> BuscarCursosOfertasPorAlunoHistoricoEscolarSelect(long seqPessoaAtuacao)
        {
            var seqPessoa = PessoaAtuacaoDomainService.BuscarPessoaAtuacao(seqPessoaAtuacao).SeqPessoa;

            var filtroAluno = new HistoricoEscolarFilterSpecification() { SeqPessoa = seqPessoa, DisciplinaIsolada = false };

            var registro = this.HistoricoEscolarDomainService.SearchProjectionBySpecification(filtroAluno,
                p => new SMCDatasourceItem()
                {
                    Seq = p.AlunoHistorico.CursoOfertaLocalidadeTurno.CursoOfertaLocalidade.SeqCursoOferta,
                    Descricao = p.AlunoHistorico.CursoOfertaLocalidadeTurno.CursoOfertaLocalidade.CursoOferta.Descricao
                });

            return registro.SMCDistinct(d => d.Seq).ToList();
        }

        public long SalvarCursoOferta(CursoOfertaVO modelo)
        {
            ValidarModelo(modelo);

            CursoOferta dominio = modelo.Transform<CursoOferta>();

            this.SaveEntity(dominio);

            return dominio.Seq;
        }

        private void ValidarModelo(CursoOfertaVO modelo)
        {
            if (modelo.Ativo && modelo.SeqFormacaoEspecifica.HasValue && modelo.DataLiberacao.HasValue)
            {
                var cursoFormacaoEspecifica = this.CursoFormacaoEspecificaDomainService.SearchBySpecification(new CursoFormacaoEspecificaFilterSpecification() { SeqCurso = modelo.SeqCurso, SeqFormacaoEspecifica = modelo.SeqFormacaoEspecifica.Value }).FirstOrDefault();

                if (cursoFormacaoEspecifica != null && !(modelo.DataLiberacao >= cursoFormacaoEspecifica.DataInicioVigencia && (!cursoFormacaoEspecifica.DataFimVigencia.HasValue || modelo.DataLiberacao <= cursoFormacaoEspecifica.DataFimVigencia)))
                    throw new OfertaCursoAtivaFormacaoEspecificaCursoNaoVigenteException();
            }

            if (modelo.Seq != 0 && !modelo.Ativo)
            {
                var cursosOfertaLocalidade = this.CursoOfertaLocalidadeDomainService.SearchBySpecification(new CursoOfertaLocalidadeFilterSpecification() { SeqCursoOferta = modelo.Seq },
                    IncludesCursoOfertaLocalidade.HistoricoSituacoes_SituacaoEntidade).ToList();

                if (cursosOfertaLocalidade.Any(a => a.SituacaoAtual.CategoriaAtividade == CategoriaAtividade.Ativa || a.SituacaoAtual.CategoriaAtividade == CategoriaAtividade.EmAtivacao))
                    throw new OfertaCursoComLocalidadeAtivaEmAtivacaoException();
            }
        }

        public string BuscarDescricaoDocumentoConclusao(long seqCurso)
        {
            var filtro = new CursoOfertaFilterSpecification { SeqCurso = seqCurso };
            return this.SearchBySpecification(filtro).ToList().Select(s => s.DescricaoDocumentoConclusao).FirstOrDefault();
        }
    }
}