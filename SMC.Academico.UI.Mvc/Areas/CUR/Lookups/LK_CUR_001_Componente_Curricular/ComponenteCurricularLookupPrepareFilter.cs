using SMC.Academico.Common.Areas.CUR.Enums;
using SMC.Academico.ServiceContract.Areas.CSO.Interfaces;
using SMC.Academico.ServiceContract.Areas.CUR.Interfaces;
using SMC.Academico.ServiceContract.Areas.ORG.Interfaces;
using SMC.Framework;
using SMC.Framework.Model;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.Html;
using System.Collections.Generic;
using System.Linq;

namespace SMC.Academico.UI.Mvc.Areas.CUR.Lookups
{
    public class ComponenteCurricularLookupPrepareFilter : ISMCFilter<ComponenteCurricularLookupFiltroViewModel>
    {
        public ComponenteCurricularLookupFiltroViewModel Filter(SMCControllerBase controllerBase, ComponenteCurricularLookupFiltroViewModel filter)
        {
            // Replica o nível de ensino recebido por parâmetro para o filtro
            if(!filter.SeqInstituicaoNivelResponsavel.HasValue)
                filter.SeqInstituicaoNivelResponsavel = filter.SeqNivelEnsino;

            if (filter.SeqGrupoCurricularComponente.HasValue)
            {
                PreencherFiltroPorGrupoCurricularComponente(controllerBase, filter);
            }
            else if (filter.SeqNivelEnsino.HasValue && filter.SeqTipoComponenteCurricular.HasValue)
            {
                PreencherFiltroPorTipoComponenteCurricular(controllerBase, filter);
            }
            else if (filter.AssuntoComponente.GetValueOrDefault())
            {
                PreencherFiltroPorAssuntoComponente(controllerBase, filter);
            }
            else if (filter.SeqNivelEnsino.HasValue && filter.SeqGrupoCurricular.HasValue)
            {
                PreencherFiltroPorGrupoCurricualar(controllerBase, filter);
            }

            if (filter.SeqInstituicaoNivelResponsavel.HasValue && filter.SeqTipoComponenteCurricular.HasValue)
            {
                var service = controllerBase.Create<IInstituicaoNivelTipoComponenteCurricularService>();

                List <SMCDatasourceItem> listItens = !filter.SeqGrupoCurricular.HasValue ?
                   service.BuscarTipoComponenteCurricularSelect(filter.SeqInstituicaoNivelResponsavel.Value) :
                   service.BuscarTipoComponenteCurricularPorGrupoSelect(filter.SeqGrupoCurricular.Value);

                filter.TiposComponenteCurricular = listItens;
            }

            PreencherFiltroPorSeqTipoComponenteCurricular(controllerBase, filter);

            // Preenche o select de níveis de ensino
            PreencherFiltroNivelEnsino(controllerBase, filter);

            return filter;
        }

        private void PreencherFiltroNivelEnsino(SMCControllerBase controllerBase, ComponenteCurricularLookupFiltroViewModel filter)
        {
            // Recupera o curso responsável pelo componente
            var nivelEnsinoService = controllerBase.Create<IInstituicaoNivelService>();
            filter.NiveisEnsino = nivelEnsinoService.BuscarNiveisEnsinoSelect() ?? new List<SMCDatasourceItem>();
        }

        /// <summary>
        /// Preenche os seguintes relacionados ao grupo curricular componente:
        /// -Instituição nível responsável com o nível de ensino do curso do componente
        /// -Tipos de componente curricular com os tipos do nível do curso do componente
        /// --Seleciona o mesmo tipo de componente do grupo curricular componente informado
        /// -Entidades responsáveis do mesno nível de ensino do curso e tipo do grupo informado
        /// --Seleciona o curso do grupo curricular informado como entidade responsável
        /// </summary>
        /// <param name="controllerBase">Controller do lookup</param>
        /// <param name="filter">Filtro do lookup com o sequencial do grupo curricular componente</param>
        private void PreencherFiltroPorGrupoCurricularComponente(SMCControllerBase controllerBase, ComponenteCurricularLookupFiltroViewModel filter)
        {
            // Recupera o curso responsável pelo componente
            var cursoService = controllerBase.Create<ICursoService>();
            var curso = cursoService.BuscarCursoPorGrupoCurricularComponente(filter.SeqGrupoCurricularComponente.Value);

            // Recupera o instituição nível do curso
            var instituicaoNivelService = controllerBase.Create<IInstituicaoNivelService>();
            var instituicaoNivel = instituicaoNivelService.BuscarInstituicaoNivelPorCurso(curso.Seq);
            filter.SeqInstituicaoNivelResponsavel = instituicaoNivel.SeqNivelEnsino;

            // Recupera os tipos de componentes curriculares do nível ensino
            var InstituicaoNivelTipoComponenteCurricularService = controllerBase.Create<IInstituicaoNivelTipoComponenteCurricularService>();
            filter.TiposComponenteCurricular = InstituicaoNivelTipoComponenteCurricularService.BuscarTipoComponenteCurricularSelect(instituicaoNivel.SeqNivelEnsino);

            // Seleciona o tipo do componente curricular do componente
            var componenteCurricularService = controllerBase.Create<IComponenteCurricularService>();
            var compoenteCurricular = componenteCurricularService
                .BuscarComponenteCurricularPorGrupoCurricularComponente(filter.SeqGrupoCurricularComponente.Value);
            filter.SeqTipoComponenteCurricular = compoenteCurricular.SeqTipoComponenteCurricular;

            // Recupera as entidades responsáveis para o curso e tipo do componente
            filter.EntidadesResponsavel = InstituicaoNivelTipoComponenteCurricularService
                .BuscarEntidadesPorTipoComponenteSelect(instituicaoNivel.SeqNivelEnsino, filter.SeqTipoComponenteCurricular.Value);

            // Recupera a entidade responsável pelo componente do grupo componente informado
            filter.SeqEntidade = compoenteCurricular.EntidadesResponsaveis.FirstOrDefault()?.SeqEntidade;

            // Caso seja informado o parêmetro assunto compoente, substitui o tipo de componente selecionado pelo tipo assunto
            if (filter.AssuntoComponente.GetValueOrDefault())
            {
                PreencherFiltroPorAssuntoComponente(controllerBase, filter);
            }
        }

        private void PreencherFiltroPorAssuntoComponente(SMCControllerBase controllerBase, ComponenteCurricularLookupFiltroViewModel filter)
        {
            if (filter.SeqInstituicaoNivelResponsavel.HasValue)
            {
                var InstituicaoNivelTipoComponenteCurricularService = controllerBase.Create<IInstituicaoNivelTipoComponenteCurricularService>();
                filter.SeqTipoComponenteCurricular = InstituicaoNivelTipoComponenteCurricularService
                    .BuscarInstituicaoNivelTipoComponenteCurricularGestaoDivisao(filter.SeqInstituicaoNivelResponsavel.Value, TipoGestaoDivisaoComponente.AssuntoComponente)
                    ?.SeqTipoComponenteCurricular;
                filter.SeqTipoComponenteCurricularReadOnly = true;
            }
            else if (filter.AssuntoComponente.GetValueOrDefault())
            {
                filter.TiposGestaoDivisaoComponente = new TipoGestaoDivisaoComponente[] { TipoGestaoDivisaoComponente.AssuntoComponente };
            }
        }

        /// <summary>
        /// Preenche os seguintes relacionados ao grupo curricular componente:
        /// -Instituição nível responsável com o nível informado
        /// -Tipos de componente curricular com os tipos do nível informado
        /// --Seleciona o tipo de componente informado
        /// </summary>
        /// <param name="controllerBase">Controller do lookup</param>
        /// <param name="filter">Filtro do lookup com o sequencial do nível de ensino e sequencial do tipo de componente</param>
        private void PreencherFiltroPorTipoComponenteCurricular(SMCControllerBase controllerBase, ComponenteCurricularLookupFiltroViewModel filter)
        {           
            // Recupera os tipos de componentes curriculares do nível ensino
            var InstituicaoNivelTipoComponenteCurricularService = controllerBase.Create<IInstituicaoNivelTipoComponenteCurricularService>();
            filter.TiposComponenteCurricular = InstituicaoNivelTipoComponenteCurricularService.BuscarTipoComponenteCurricularSelect(filter.SeqNivelEnsino.Value);
        }

        /// <summary>
        /// Preenche o array de sequencial dos tipos de componentes:
        /// - Aceitam dispensa quando o parâmetro tipo componente dispensa for informado
        /// - Tipo gestão divisão componente quando o tipo de gestão for informado
        /// </summary>
        /// <param name="controllerBase">Controller do lookup</param>
        /// <param name="filter">Filtro do lookup com o array de sequencial do tipo de componente</param>
        private void PreencherFiltroPorSeqTipoComponenteCurricular(SMCControllerBase controllerBase, ComponenteCurricularLookupFiltroViewModel filter)
        {
            List<long> seqTipoComponentesCurriculares = new List<long>();
            List<long> seqTipoComponentesCurricularesDispensa = new List<long>();
            List<long> seqTipoComponentesCurricularesGestao = new List<long>();

            if (filter.TipoComponenteDispensa.GetValueOrDefault())
            {
                // Recupera os tipos de componentes curriculares com parâmetro aceita dispensa marcado
                var InstituicaoNivelTipoComponenteCurricularService = controllerBase.Create<IInstituicaoNivelTipoComponenteCurricularService>();
                seqTipoComponentesCurricularesDispensa.AddRange(InstituicaoNivelTipoComponenteCurricularService.BuscarTipoComponenteCurricularDispensa());
            }

            if (filter.TiposGestaoDivisaoComponente.SMCAny())
            {
                // Recupera os tipos de componentes curriculares com divisões de mesmo tipo de gestão do parâmetro
                var TipoDivisaoComponenteService = controllerBase.Create<ITipoDivisaoComponenteService>();
                seqTipoComponentesCurricularesGestao.AddRange(TipoDivisaoComponenteService.BuscarTipoComponenteCurricularPorTipoGestaoDivisaoComponente(filter.TiposGestaoDivisaoComponente));
            }

            if (filter.TipoComponenteDispensa.GetValueOrDefault() && filter.TiposGestaoDivisaoComponente.SMCAny())
                seqTipoComponentesCurriculares = seqTipoComponentesCurricularesDispensa.Intersect(seqTipoComponentesCurricularesGestao).ToList();
            else if (filter.TipoComponenteDispensa.GetValueOrDefault())
                seqTipoComponentesCurriculares = seqTipoComponentesCurricularesDispensa;
            else if (filter.TiposGestaoDivisaoComponente.SMCAny())
                seqTipoComponentesCurriculares = seqTipoComponentesCurricularesGestao;

            if (filter.SeqTipoComponentesCurriculares == null && seqTipoComponentesCurriculares.Count > 0)
                filter.SeqTipoComponentesCurriculares = seqTipoComponentesCurriculares.Distinct().ToArray();
        }

        private void PreencherFiltroPorGrupoCurricualar(SMCControllerBase controllerBase, ComponenteCurricularLookupFiltroViewModel filter)
        {            
            filter.TiposComponenteCurricular = controllerBase.Create<IInstituicaoNivelTipoComponenteCurricularService>().BuscarTipoComponenteCurricularPorGrupoSelect(filter.SeqGrupoCurricular.Value);
        }
    }
}