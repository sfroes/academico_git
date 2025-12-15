using SMC.Academico.Common.Constants;
using SMC.Academico.Domain.Areas.CSO.DomainServices;
using SMC.Academico.Domain.Areas.CSO.Specifications;
using SMC.Academico.Domain.Areas.CSO.ValueObjects;
using SMC.Academico.Domain.Areas.ORG.DomainServices;
using SMC.Academico.Domain.Areas.ORG.Specifications;
using SMC.Academico.ServiceContract.Areas.CSO.Data;
using SMC.Academico.ServiceContract.Areas.CSO.Interfaces;
using SMC.Academico.ServiceContract.Areas.ORG.Data;
using SMC.Academico.ServiceContract.Areas.ORG.Interfaces;
using SMC.Framework.Extensions;
using SMC.Framework.Model;
using SMC.Framework.Service;
using System.Collections.Generic;
using System.Linq;

namespace SMC.Academico.Service.Areas.CSO.Services
{
    public class CursoUnidadeService : SMCServiceBase, ICursoUnidadeService
    {
        #region [ Serviços ]

        private CursoUnidadeDomainService CursoUnidadeDomainService => this.Create<CursoUnidadeDomainService>();
        private InstituicaoNivelDomainService InstituicaoNivelDomainService => this.Create<InstituicaoNivelDomainService>();

        private IEntidadeService EntidadeService => this.Create<IEntidadeService>();

        #endregion [ Serviços ]

        /// <summary>
        /// Busca as configurações de um CursoUnidade
        /// </summary>
        /// <returns>Dados das configurações de um CursoUnidade</returns>
        public EntidadeData BuscarConfiguracoesCursoUnidade()
        {
            return this.EntidadeService
                .BuscarConfiguracoesEntidadeComClassificacao(TOKEN_TIPO_ENTIDADE_EXTERNADA.CURSO_UNIDADE);
        }

        /// <summary>
        /// Busca as possíveis entidades superiores de Curso Unidade na visão Unidade
        /// </summary>
        /// <returns>SelectItem dos HierarquiaItem que representam as entidades encontradas</returns>
        public List<SMCDatasourceItem> BuscarUnidadesSelect()
        {
            return this.CursoUnidadeDomainService.BuscarUnidadesSelect();
        }

        /// <summary>
        /// Busca as possíveis entidades superiores de Curso Unidade na visão Unidade, e não tenha entidades filhas que também possa ser entidade-pai do tipo CURSO_UNIDADE
        /// </summary>
        /// <returns>SelectItem dos HierarquiaItem que representam as entidades encontradas</returns>
        public List<SMCDatasourceItem> BuscarUnidadesSemEntidadePaiSelect(bool removeEntidadePai = false)
        {
            return this.CursoUnidadeDomainService.BuscarUnidadesSelect(removeEntidadePai);
        }

        /// <summary>
        /// Recuperar os CursoUnidade com seus níveis de ensino, ofertas e turnos
        /// </summary>
        /// <param name="filtros">Filtros para os CursoUnidade</param>
        /// <returns>Lista páginada com os dados dos CursoUnidade com seus níveis de ensino, ofertas e turnos</returns>
        public SMCPagerData<CursoUnidadeListaData> BuscarCursosUnidade(CursoUnidadeFiltroData filtros)
        {
            CursoUnidadeFilterSpecification spec = filtros.Transform<CursoUnidadeFilterSpecification>();

            if (filtros.SeqTipoOrgaoRegulador.HasValue)
            {
                var specInstituicaoNivel = new InstituicaoNivelFilterSpecification { TipoOrgaoRegulador = filtros.SeqTipoOrgaoRegulador };
                var instituicaoNivel = InstituicaoNivelDomainService.SearchBySpecification(specInstituicaoNivel).Select(s => s.SeqNivelEnsino).ToList();
                spec.SeqsNivelEnsino = instituicaoNivel;
            }

            var cursosUnidade = this.CursoUnidadeDomainService.BuscarCursosUnidade(spec).Transform<SMCPagerData<CursoUnidadeListaData>>();

            if (filtros.ExibirPrimeiroCursoOfertaLocalidadeAtivo)
            {
                foreach (var cursoUnidade in cursosUnidade)
                {
                    cursoUnidade.CursosOfertaLocalidade = cursoUnidade.CursosOfertaLocalidade.OrderByDescending(c => c.Ativo).ThenBy(c => c.Nome).ToList();
                }
            }
            else
            {
                foreach (var cursoUnidade in cursosUnidade)
                {
                    cursoUnidade.CursosOfertaLocalidade = cursoUnidade.CursosOfertaLocalidade.OrderBy(c => c.Nome).ToList();
                }
            }

            return cursosUnidade;
        }

        /// <summary>
        /// Recupera um CursoUnidade com suas dependências e configurações
        /// </summary>
        /// <param name="seq">Sequencial do CursoUnidade a ser recuperado</param>
        /// <param name="desativarFiltroHierarquia">Flag para desativar filtro de dados</param>
        /// <returns>Dados do CursoUnidade, dependências e configurações</returns>
        public CursoUnidadeData BuscarCursoUnidade(long seq, bool desativarFiltroHierarquia = false)
        {
            return this.CursoUnidadeDomainService
                .BuscarCursoUnidade(seq, desativarFiltroHierarquia)
                .Transform<CursoUnidadeData>(this.BuscarConfiguracoesCursoUnidade());
        }

        /// <summary>
        /// Recupera um CursoUnidade com suas dependências e configurações com o filtro de dados desativado
        /// </summary>
        /// <param name="seq">Sequencial do CursoUnidade a ser recuperado</param>
        /// <returns>Dados do CursoUnidade, dependências e configurações</returns>
        public CursoUnidadeData BuscarCursoUnidadeFiltroDesativado(long seq)
        {
            return BuscarCursoUnidade(seq, true);
        }

        /// <summary>
        /// Recupera a mascara de curso unidade segundo a regra RN_CSO_026
        /// </summary>
        /// <param name="seqCurso">Sequencial do curso</param>
        /// <param name="seqUnidade">Sequencial do item de hierarquia da unidade responsável</param>
        /// <returns>Mascara segundo a regra RN_CSO_026</returns>
        public string RecuperarMascaraCursoUnidade(long seqCurso, long seqUnidade)
        {
            return this.CursoUnidadeDomainService.RecuperarMascaraCursoUnidade(seqCurso, seqUnidade);
        }

        /// <summary>
        /// Grava um CursoUnidade e suas dependências
        /// </summary>
        /// <param name="cursoUnidadeVo">Dados do CursoUnidade</param>
        /// <returns>Sequencial do CursoUnidade gravado</returns>
        public long SalvarCursoUnidade(CursoUnidadeData cursoUnidade)
        {
            return this.CursoUnidadeDomainService.SalvarCursoUnidade(cursoUnidade.Transform<CursoUnidadeVO>());
        }
    }
}