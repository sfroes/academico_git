using SMC.Academico.Common.Areas.ORG.Includes;
using SMC.Academico.Domain.Areas.CAM.DomainServices;
using SMC.Academico.Domain.Areas.CAM.Models;
using SMC.Academico.Domain.Areas.CSO.DomainServices;
using SMC.Academico.Domain.Areas.CSO.Models;
using SMC.Academico.Domain.Areas.ORG.Models;
using SMC.Academico.Domain.Areas.ORG.Specifications;
using SMC.Academico.Domain.Areas.TUR.DomainServices;
using SMC.Framework.Domain;
using SMC.Framework.Model;
using SMC.Framework.Specification;
using SMC.Framework.Util;
using System.Collections.Generic;
using System.Linq;

namespace SMC.Academico.Domain.Areas.ORG.DomainServices
{
    public class InstituicaoNivelDomainService : AcademicoContextDomain<InstituicaoNivel>
    {
        #region [ DomainService ]

        private CursoDomainService CursoDomainService => Create<CursoDomainService>();
        private EntidadeDomainService EntidadeDomainService => Create<EntidadeDomainService>();
        private ProcessoSeletivoDomainService ProcessoSeletivoDomainService => Create<ProcessoSeletivoDomainService>();
        private TurmaDomainService TurmaDomainService => Create<TurmaDomainService>();

        #endregion [ DomainService ]

        /// <summary>
        /// Buscar o sequencial nivel ensino pelo sequencial instituição nível ensino
        /// </summary>
        /// <param name="SeqInstituicaoNivelEnsino"></param>
        /// <returns>Sequencial nivel de ensino</returns>
        public long BuscarSequencialNivelEnsino(long seqInstituicaoNivelEnsino)
        {
            var seqNivelEnsino = this.SearchProjectionByKey(new SMCSeqSpecification<InstituicaoNivel>(seqInstituicaoNivelEnsino), s => s.SeqNivelEnsino);
            return seqNivelEnsino;
        }

        /// <summary>
        /// Buscar o sequencial instituição nivel ensino pelo sequencial nível ensino e sequencial instituição
        /// </summary>
        /// <param name="seqNivelEnsino">Sequencial do nível de ensino</param>
        /// <param name="seqInstituicaoEnsino">Sequencial da instituição de ensino</param>
        /// <returns>Sequencial instituição nivel de ensino</returns>
        public long BuscarSequencialInstituicaoNivelEnsino(long seqNivelEnsino, long seqInstituicaoEnsino)
        {
            var spec = new InstituicaoNivelFilterSpecification()
            {
                SeqNivelEnsino = seqNivelEnsino,
                SeqInstituicaoEnsino = seqInstituicaoEnsino
            };
            var SeqInstituicaoNivelEnsino = this.SearchProjectionBySpecification(spec, s => s.Seq).FirstOrDefault();
            return SeqInstituicaoNivelEnsino;
        }

        /// <summary>
        /// Buscar informações de uma instituição x nivel de ensino
        /// </summary>
        /// <param name="seqNivelEnsino">Sequencial do nivel de ensino</param>
        /// <param name="seqInstituicaoEnsino">Sequencial da instituição de ensino</param>
        /// <returns>Dados da instituição x nível de ensino</returns>
        public InstituicaoNivel BuscarInstituicaoNivelEnsino(long seqNivelEnsino, long seqInstituicaoEnsino)
        {
            var spec = new InstituicaoNivelFilterSpecification()
            {
                SeqNivelEnsino = seqNivelEnsino,
                SeqInstituicaoEnsino = seqInstituicaoEnsino
            };
            var includes = IncludesInstituicaoNivel.NivelEnsino | IncludesInstituicaoNivel.InstituicaoEnsino;
            return this.SearchByKey(spec, includes);
        }

        /// <summary>
        /// Busca a instituição nível de um curso
        /// </summary>
        /// <param name="seqCurso">Sequencial do curso</param>
        /// <returns>Dados da instituição nível</returns>
        public InstituicaoNivel BuscarInstituicaoNivelPorCurso(long seqCurso)
        {
            // Busca a instiuição e nível de ensino do curso
            var spec = new SMCSeqSpecification<Curso>(seqCurso);
            var dadosCurso = this.CursoDomainService.SearchProjectionByKey(spec, p => new
            {
                SeqNivelEnsino = p.SeqNivelEnsino,
                SeqInstituicaoEnsino = p.SeqInstituicaoEnsino
            });
            return BuscarInstituicaoNivelEnsino(dadosCurso.SeqNivelEnsino, dadosCurso.SeqInstituicaoEnsino.GetValueOrDefault());
        }

        /// <summary>
        /// Busca a instituição nível de um nível ensino
        /// </summary>
        /// <param name="seqNivelEnsino">Sequencial do nível de ensino</param>
        /// <returns>Dados da nível de ensino na instituição</returns>
        public InstituicaoNivel BuscarInstituicaoNivelPorNivelEnsino(long seqNivelEnsino)
        {
            return this.SearchByKey(new InstituicaoNivelFilterSpecification() { SeqNivelEnsino = seqNivelEnsino }, IncludesInstituicaoNivel.NivelEnsino | IncludesInstituicaoNivel.InstituicaoEnsino);
        }

        /// <summary>
        /// Busca a configuração de instituição nível de uma turma
        /// </summary>
        /// <param name="seqTurma">Sequencial da turma</param>
        /// <returns>Dados da instituição nível</returns>
        public InstituicaoNivel BuscarInstituicaoNivelPorTurma(long seqTurma)
        {
            // Busca a instiuição e nível de ensino do turma e instiuição de ensino
            var dadosTurma = TurmaDomainService.SearchProjectionByKey(seqTurma, p => new
            {
                p.ConfiguracoesComponente.FirstOrDefault(f => f.Principal)
                 .RestricoesTurmaMatriz.FirstOrDefault(f => f.OfertaMatrizPrincipal)
                 .CursoOfertaLocalidadeTurno
                 .CursoOfertaLocalidade
                 .CursoOferta
                 .Curso
                 .SeqNivelEnsino,
                p.ConfiguracoesComponente.FirstOrDefault(f => f.Principal)
                 .RestricoesTurmaMatriz.FirstOrDefault(f => f.OfertaMatrizPrincipal)
                 .CursoOfertaLocalidadeTurno
                 .CursoOfertaLocalidade
                 .CursoOferta
                 .Curso
                 .SeqInstituicaoEnsino
            });
            return BuscarInstituicaoNivelEnsino(dadosTurma.SeqNivelEnsino, dadosTurma.SeqInstituicaoEnsino.GetValueOrDefault());
        }

        /// <summary>
        /// Busca a lista de níveis de ensino com reconhecimento LDB da instituição de ensino logada para popular um Select
        /// </summary>
        /// <returns>Lista de níveis de ensino com sequencial do NivelEnsino</returns>
        public List<SMCDatasourceItem> BuscarNiveisEnsinoReconhecidoLDBSelect()
        {
            // Busca o nivel de ensino com reconhecimento LDB
            InstituicaoNivelFilterSpecification specNivel = new InstituicaoNivelFilterSpecification()
            {
                ReconhecidoLDB = true
            };

            var lista = this.SearchBySpecification(specNivel, IncludesInstituicaoNivel.NivelEnsino).OrderBy(o => o.NivelEnsino.Descricao);
            List<SMCDatasourceItem> retorno = new List<SMCDatasourceItem>();
            foreach (var item in lista)
            {
                retorno.Add(new SMCDatasourceItem(item.SeqNivelEnsino, item.NivelEnsino.Descricao));
            }
            return retorno;
        }

        public List<SMCDatasourceItem> BuscarNiveisEnsinoPorEntidadeSelect(long seqEntidade)
        {
            var seqIE = EntidadeDomainService.SearchProjectionByKey(new SMCSeqSpecification<Entidade>(seqEntidade), a => a.SeqInstituicaoEnsino);

            InstituicaoNivelFilterSpecification specNivel = new InstituicaoNivelFilterSpecification()
            {
                SeqInstituicaoEnsino = seqIE
            };

            var lista = this.SearchBySpecification(specNivel, IncludesInstituicaoNivel.NivelEnsino).OrderBy(o => o.NivelEnsino.Descricao);
            List<SMCDatasourceItem> retorno = new List<SMCDatasourceItem>();
            foreach (var item in lista)
            {
                retorno.Add(new SMCDatasourceItem(item.SeqNivelEnsino, item.NivelEnsino.Descricao));
            }
            return retorno;
        }


        /// <summary>
        /// Busca a lista de níveis de ensino com reconhecimento LDB da instituição de ensino logada para popular um Select
        /// </summary>
        /// <returns>Lista de níveis de ensino com sequencial do NivelEnsino</returns>
        public List<SMCDatasourceItem> BuscarNiveisEnsinoPorCicloLetivoSelect(long seqCicloLetivo)
        {
            // Busca o nivel de ensino com reconhecimento LDB
            InstituicaoNivelFilterSpecification specNivel = new InstituicaoNivelFilterSpecification()
            {
                ReconhecidoLDB = true
            };

            var lista = this.SearchBySpecification(specNivel, IncludesInstituicaoNivel.NivelEnsino).OrderBy(o => o.NivelEnsino.Descricao);
            List<SMCDatasourceItem> retorno = new List<SMCDatasourceItem>();
            foreach (var item in lista)
            {
                retorno.Add(new SMCDatasourceItem(item.SeqNivelEnsino, item.NivelEnsino.Descricao));
            }
            return retorno;
        }

        /// <summary>
        /// Busca os níveis de ensino associados ao ciculo letivo do processo informado
        /// </summary>
        /// <param name="seqProcessoSeletivo">Sequencial do processo</param>
        /// <returns>Dados dos níveis de ensino encontrados</returns>
        public List<SMCDatasourceItem> BuscarNiveisEnsinoPorProcessoSeletivoSelect(long seqProcessoSeletivo)
        {
            // Recupera os níveis de ensino associados ao ciclo letivo do processo selecionado
            var specProcesso = new SMCSeqSpecification<ProcessoSeletivo>(seqProcessoSeletivo);
            var seqsNiveis = ProcessoSeletivoDomainService.SearchProjectionByKey(specProcesso, p => p.NiveisEnsino.Select(s => s.SeqNivelEnsino))?.ToArray() ?? new long[] { };

            // Refaz a consulta pela InstituicaoNivel para aplicar o filtro RN_USG_004 - Filtro por Nível de Ensino
            var specNiveis = new SMCContainsSpecification<InstituicaoNivel, long>(p => p.SeqNivelEnsino, seqsNiveis);
            specNiveis.SetOrderBy(o => o.NivelEnsino.Descricao);

            return SearchProjectionBySpecification(specNiveis, p => new SMCDatasourceItem()
            {
                Seq = p.SeqNivelEnsino,
                Descricao = p.NivelEnsino.Descricao
            }).ToList();
        }

        public List<SMCDatasourceItem> BuscarTipoOrgaoReguladorInstituicaoNivelEnsino(long seqNivelEnsino, long seqInstituicaoEnsino)
        {
            var instituicaoNivelEnsino = BuscarInstituicaoNivelEnsino(seqNivelEnsino, seqInstituicaoEnsino);

            var retorno = new List<SMCDatasourceItem>();

            if (instituicaoNivelEnsino != null)
                retorno.Add(new SMCDatasourceItem() { Seq = (long)instituicaoNivelEnsino.TipoOrgaoRegulador, Descricao = SMCEnumHelper.GetDescription(instituicaoNivelEnsino.TipoOrgaoRegulador) });

            return retorno;
        }

        public List<SMCDatasourceItem> BuscarTipoOrgaoReguladorInstituicao(long seqInstituicaoEnsino)
        {
            var spec = new InstituicaoNivelFilterSpecification() { SeqInstituicaoEnsino = seqInstituicaoEnsino };
            var tiposOrgaoRegulador = this.SearchProjectionBySpecification(spec, s => s.TipoOrgaoRegulador).Distinct().ToList();

            var retorno = new List<SMCDatasourceItem>();

            if (tiposOrgaoRegulador != null)
            {
                foreach (var tipoOrgaoRegulador in tiposOrgaoRegulador)
                    retorno.Add(new SMCDatasourceItem() { Seq = (long)tipoOrgaoRegulador, Descricao = SMCEnumHelper.GetDescription(tipoOrgaoRegulador) });
            }

            return retorno;
        }
    }
}