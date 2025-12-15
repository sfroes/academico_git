using SMC.Academico.Common.Areas.CUR.Enums;
using SMC.Academico.Common.Areas.CUR.Exceptions;
using SMC.Academico.Common.Areas.CUR.Includes;
using SMC.Academico.Domain.Areas.CUR.Models;
using SMC.Academico.Domain.Areas.CUR.Specifications;
using SMC.Academico.Domain.Areas.CUR.ValueObjects;
using SMC.Academico.Domain.Areas.TUR.DomainServices;
using SMC.Academico.Domain.Areas.TUR.Specifications;
using SMC.Academico.Domain.Helpers;
using SMC.Framework.Domain;
using SMC.Framework.Extensions;
using SMC.Framework.Model;
using SMC.Framework.Specification;
using System.Collections.Generic;
using System.Linq;

namespace SMC.Academico.Domain.Areas.CUR.DomainServices
{
    public class GrupoConfiguracaoComponenteDomainService : AcademicoContextDomain<GrupoConfiguracaoComponente>
    {
        #region [ DomainService ]

        private ConfiguracaoComponenteDomainService ConfiguracaoComponenteDomainService
        {
            get { return this.Create<ConfiguracaoComponenteDomainService>(); }
        }

        private GrupoCurricularComponenteDomainService GrupoCurricularComponenteDomainService
        {
            get { return this.Create<GrupoCurricularComponenteDomainService>(); }
        }

        private InstituicaoNivelTipoComponenteCurricularDomainService InstituicaoNivelTipoComponenteCurricularDomainService
        {
            get { return this.Create<InstituicaoNivelTipoComponenteCurricularDomainService>(); }
        }

        private TurmaConfiguracaoComponenteDomainService TurmaConfiguracaoComponenteDomainService
        {
            get { return this.Create<TurmaConfiguracaoComponenteDomainService>(); }
        }


        private GrupoConfiguracaoComponenteItemDomainService GrupoConfiguracaoComponenteItemDomainService
        {
            get { return this.Create<GrupoConfiguracaoComponenteItemDomainService>(); }
        }

        #endregion [ DomainService ]

        /// <summary>
        /// Buscar os grupos de configurações do componente com itens de configuração para listagem
        /// </summary>
        /// <param name="filtros">Sequencial do componente curricular sendo o unico filtro</param>
        /// <returns>SMCPagerData da lista de configuração de componente</returns>
        public SMCPagerData<GrupoConfiguracaoComponenteVO> BuscarGruposConfiguracoesComponentes(GrupoConfiguracaoComponenteFilterSpecification filtros)
        {
            var includes = IncludesGrupoConfiguracaoComponente.Itens_ConfiguracaoComponente_ComponenteCurricular_NiveisEnsino;
            int total = 0;
            var grupos = this.SearchBySpecification(filtros, out total, includes);

            List<GrupoConfiguracaoComponenteVO> listResult = new List<GrupoConfiguracaoComponenteVO>();
            foreach (var item in grupos.ToList())
            {
                var grupoConfiguracao = item.Transform<GrupoConfiguracaoComponenteVO>();

                grupoConfiguracao.Itens = item.Itens.Select(i => new GrupoConfiguracaoComponenteItemVO()
                {
                    Seq = i.Seq,
                    SeqConfiguracaoComponente = i.SeqConfiguracaoComponente,
                    ConfiguracaoComponenteCodigo = i.ConfiguracaoComponente.Codigo,
                    ConfiguracaoComponenteDescricao = i.ConfiguracaoComponente.Descricao,
                    ConfiguracaoComponenteDescricaoComplementar = i.ConfiguracaoComponente.DescricaoComplementar,
                    ComponenteCurricularCargaHoraria = i.ConfiguracaoComponente.ComponenteCurricular.CargaHoraria,
                    ComponenteCurricularCredito = i.ConfiguracaoComponente.ComponenteCurricular.Credito,
                    FormatoCargaHoraria = InstituicaoNivelTipoComponenteCurricularDomainService.SearchProjectionByKey(new InstituicaoNivelTipoComponenteCurricularFilterSpecification()
                    {
                        SeqNivelEnsino = i.ConfiguracaoComponente.ComponenteCurricular.NiveisEnsino.Where(s => s.Responsavel == true).Select(s => s.SeqNivelEnsino).FirstOrDefault(),
                        SeqTipoComponenteCurricular = i.ConfiguracaoComponente.ComponenteCurricular.SeqTipoComponenteCurricular
                    }, h => h.FormatoCargaHoraria)
                }).ToList();

                listResult.Add(grupoConfiguracao);
            }

            return new SMCPagerData<GrupoConfiguracaoComponenteVO>(listResult, total);
        }

        /// <summary>
        /// Salvar um grupo de configuração do componente
        /// </summary>
        /// <param name="grupoConfiguracaoComponente"></param>
        /// <returns>Sequencial do grupo de configuração do componente</returns>
        public long SalvarGrupoConfiguracaoComponente(GrupoConfiguracaoComponente grupoConfiguracaoComponente)
        {
            foreach (var item in grupoConfiguracaoComponente.Itens)
            {
                item.ConfiguracaoComponente = ConfiguracaoComponenteDomainService.SearchByKey(new SMCSeqSpecification<ConfiguracaoComponente>(item.SeqConfiguracaoComponente),
                                        IncludesConfiguracaoComponente.ComponenteCurricular |
                                        IncludesConfiguracaoComponente.DivisoesComponente_TipoDivisaoComponente_Modalidade);
            }

            // Validar o grupo de configuração do componente
            ValidarGrupoConfiguracaoComponente(grupoConfiguracaoComponente);

            //Salva o objeto grupo configuração componente
            this.SaveEntity(grupoConfiguracaoComponente);

            return grupoConfiguracaoComponente.Seq;
        }

        private void ValidarGrupoConfiguracaoComponente(GrupoConfiguracaoComponente grupoConfiguracaoComponente)
        {
            if (grupoConfiguracaoComponente == null)
                return;

            //RN_CUR_064_1 - Verifica se foi associada pelo menos duas configurações diferentes
            if (grupoConfiguracaoComponente.Itens == null || grupoConfiguracaoComponente.Itens.GroupBy(x => x.ConfiguracaoComponente.SeqComponenteCurricular).Count() < 2)
                throw new GrupoConfiguracaoComponenteDiferentesException();

            //RN_CUR_064_2 - Verifica se os componentes tem mesma carga horária (carga horária diferente não é permitido).
            var configuracoesComponente = grupoConfiguracaoComponente.Itens.Select(x => x.ConfiguracaoComponente);
            if (configuracoesComponente.Select(y => y.ComponenteCurricular)
                                       .GroupBy(z => z.CargaHoraria)
                                       .Count() > 1)
                throw new GrupoConfiguracaoComponenteCargaHorariaException();

            //RN_CUR_064_3 - Verifica se as configurações de um grupo possuem divisões equivalentes
            //(mesmos tipos de divisão, com mesma modalidade, mesma carga-horária e mesma valor do flag permite grupos).
            var divisoesComponente = configuracoesComponente.SelectMany(x => x.DivisoesComponente);

            var NaoPossuiDivisaoEquivalente = false;

            NaoPossuiDivisaoEquivalente = (divisoesComponente.GroupBy(x => x.SeqTipoDivisaoComponente).Count() > 1)
                                       || (divisoesComponente.GroupBy(x => x.TipoDivisaoComponente.SeqModalidade).Count() > 1)
                                       || (divisoesComponente.GroupBy(x => x.CargaHoraria).Count() > 1)
                                       || (divisoesComponente.GroupBy(x => x.PermiteGrupo).Count() > 1);

            if (NaoPossuiDivisaoEquivalente)
                throw new GrupoConfiguracaoComponenteDivisoesEquivalentesException();

            //RN_CUR_064_4 - Verifica se as configurações de um grupo possuem os mesmos tipos de organização,
            //com mesma descrição e mesma carga horária
            var NaoPossuiTipoOrganizacaoEquivalente = false;

            //TODO: Voltar ao revisar a regra RN_CUR_064
            NaoPossuiTipoOrganizacaoEquivalente = configuracoesComponente.GroupBy(x => x.ComponenteCurricular.TipoOrganizacao.GetValueOrDefault()).Count() > 1
                                               //|| configuracoesComponente.GroupBy(x => x.Descricao).Count() > 1
                                               || configuracoesComponente.GroupBy(x => x.ComponenteCurricular.CargaHoraria.GetValueOrDefault()).Count() > 1;

            if (NaoPossuiTipoOrganizacaoEquivalente)
                throw new GrupoConfiguracaoComponenteTipoOrganizacaoException();

            //RN_CUR_064_5 - Verifica se as configurações de um grupo não pertencem a componentes de um mesmo currículo
            if (grupoConfiguracaoComponente.Itens.GroupBy(x => x.ConfiguracaoComponente.SeqComponenteCurricular).Any(y => y.Count() > 1))
                throw new GrupoConfiguracaoComponenteCurriculoException();

            var seqsComponentesValidacao = grupoConfiguracaoComponente.Itens.Select(x => x.ConfiguracaoComponente.SeqComponenteCurricular).ToList();
            var spec = new GrupoCurricularComponenteFilterSpecification() { SeqComponentesCurriculares = seqsComponentesValidacao };

            var gruposCurriculares = this.GrupoCurricularComponenteDomainService.SearchProjectionBySpecification(spec,
                                            p => new
                                            {
                                                ChaveDupla = p.GrupoCurricular.SeqCurriculo + ";" + p.SeqComponenteCurricular,
                                                SeqCurriculo = p.GrupoCurricular.SeqCurriculo,
                                                SeqComponenteCurricular = p.SeqComponenteCurricular
                                            }).GroupBy(g => g.ChaveDupla);

            var gruposCurriculos = gruposCurriculares.GroupBy(g => g.First().SeqCurriculo);

            //(|| gruposCurriculos.Count() < seqsComponentesValidacao.Count() - regra removida pois uma configuração de componente pode não ter grupo curricular 
            if (gruposCurriculos.Any(y => y.Count() > 1))
                throw new GrupoConfiguracaoComponenteCurriculoException();

            //RN_CUR_064_6 - Verifica se as configurações de um grupo não pertencem a outro grupo
            foreach (var item in grupoConfiguracaoComponente.Itens)
            {
                var specConfiguracaoComponente = new GrupoConfiguracaoComponenteFilterSpecification() { SeqConfiguracaoComponente = item.SeqConfiguracaoComponente };
                var registroConfiguracaoComponente = this.SearchBySpecification(specConfiguracaoComponente);
                if (registroConfiguracaoComponente.Where(w => w.Seq != grupoConfiguracaoComponente.Seq).Count() > 0)
                    throw new GrupoConfiguracaoComponenteGrupoException();
            }

            //RN_CUR_064_7 Verifica se uma configuração, cujo componente foi marcado para exigir componente substituto,
            //só pode ser associado a um grupo cujas configurações também são de componentes marcados para exigir componente substituto
            if (grupoConfiguracaoComponente.Itens.GroupBy(s => s.ConfiguracaoComponente.ComponenteCurricular.ExigeAssuntoComponente).Count() != 1)
                throw new GrupoConfiguracaoAssuntoComponenteException();

            //RN_CUR_064_8 Verifica se uma configuração excluída não está associada a uma turma com compartilhamento de configuração de componente
            //TODO: Voltar quando tiver fluxo de turma
        }

        /// <summary>
        /// Buscar os grupos de configurações do componente compartilhados para um configuração
        /// </summary>  
        /// <param name="seqConfiguracaoComponente">Sequencial da configuração do componente</param>
        /// <returns>Lista de grupos com itens configuração do tipo compartilhado</returns>
        public List<GrupoConfiguracaoComponenteVO> BuscarGrupoConfiguracaoComponentePorConfiguracaoCompartilhado(long seqConfiguracaoComponente)
        {
            FilterHelper.DesativarFiltros(this);
            var includes = IncludesGrupoConfiguracaoComponente.Itens_ConfiguracaoComponente_ComponenteCurricular_NiveisEnsino
                            | IncludesGrupoConfiguracaoComponente.Itens_ConfiguracaoComponente_ComponenteCurricular_EntidadesResponsaveis_Entidade;
            var spec = new GrupoConfiguracaoComponenteFilterSpecification()
            {
                SeqConfiguracaoComponente = seqConfiguracaoComponente,
                TipoGrupoConfiguracaoComponente = TipoGrupoConfiguracaoComponente.Compartilhamento
            };
            var grupos = this.SearchBySpecification(spec, includes);

            List<GrupoConfiguracaoComponenteVO> listResult = new List<GrupoConfiguracaoComponenteVO>();
            foreach (var item in grupos.ToList())
            {
                var grupoConfiguracao = item.Transform<GrupoConfiguracaoComponenteVO>();

                grupoConfiguracao.Itens = item.Itens.Where(w => w.SeqConfiguracaoComponente != seqConfiguracaoComponente).Select(i => new GrupoConfiguracaoComponenteItemVO()
                {
                    Seq = i.Seq,
                    SeqConfiguracaoComponente = i.SeqConfiguracaoComponente,
                    ConfiguracaoComponenteCodigo = i.ConfiguracaoComponente.Codigo,
                    ConfiguracaoComponenteDescricao = i.ConfiguracaoComponente.Descricao,
                    ConfiguracaoComponenteDescricaoComplementar = i.ConfiguracaoComponente.DescricaoComplementar,
                    ComponenteCurricularCargaHoraria = i.ConfiguracaoComponente.ComponenteCurricular.CargaHoraria,
                    ComponenteCurricularCredito = i.ConfiguracaoComponente.ComponenteCurricular.Credito,
                    SeqComponenteCurricular = i.ConfiguracaoComponente.SeqComponenteCurricular,
                    ComponenteCurricularEntidadesSigla = i.ConfiguracaoComponente.ComponenteCurricular.EntidadesResponsaveis.Select(s => s.Entidade.Sigla),
                    FormatoCargaHoraria = InstituicaoNivelTipoComponenteCurricularDomainService.SearchProjectionByKey(new InstituicaoNivelTipoComponenteCurricularFilterSpecification()
                    {
                        SeqNivelEnsino = i.ConfiguracaoComponente.ComponenteCurricular.NiveisEnsino.Where(s => s.Responsavel == true).Select(s => s.SeqNivelEnsino).FirstOrDefault(),
                        SeqTipoComponenteCurricular = i.ConfiguracaoComponente.ComponenteCurricular.SeqTipoComponenteCurricular
                    }, h => h.FormatoCargaHoraria)
                }).ToList();

                listResult.Add(grupoConfiguracao);
            }
            FilterHelper.AtivarFiltros(this);
            return listResult;
        }


        public void ExcluirGrupoConfiguracaoComponente(GrupoConfiguracaoComponente grupoConfiguracaoComponente)
        {
            // Verifica se já existe alguma turma compartilhada com 2 ou mais componentes do grupo
            // sendo excluido
            string query = string.Format(
                          @"select	top 5
	                            t.cod_turma as CodigoTurma, 
	                            count(gcc.seq_grupo_configuracao_componente) as QuantidadeConfiguracao
                            from	TUR.turma t
                            join	TUR.turma_configuracao_componente tcc
		                            on t.seq_turma = tcc.seq_turma
                            join	CUR.grupo_configuracao_componente_item gcc 
		                            on	gcc.seq_configuracao_componente = tcc.seq_configuracao_componente
                            where	gcc.seq_grupo_configuracao_componente = {0}
                            group by t.cod_turma
                            having count(*) > 1
                            order by t.cod_turma desc", grupoConfiguracaoComponente.Seq);
            var turmas = this.RawQuery<TurmaCompartilhadaGrupoVO>(query).ToList();
            if (turmas.Any())
            {
                var listaCodigos = turmas.Select(t => t.CodigoTurma).ToList();
                var listaStr = string.Join(", ", listaCodigos);
                throw new ConfiguracaoComponenteComTurmaException(listaStr);
            }

            // Se não tem turma compartilhada, realiza a exclusão.
            this.DeleteEntity(grupoConfiguracaoComponente);
        }
    }
}