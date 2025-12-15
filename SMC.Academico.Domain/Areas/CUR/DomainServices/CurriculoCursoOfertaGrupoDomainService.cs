using SMC.Academico.Common.Areas.CUR.Enums;
using SMC.Academico.Common.Areas.CUR.Includes;
using SMC.Academico.Domain.Areas.CUR.Models;
using SMC.Academico.Domain.Areas.CUR.Specifications;
using SMC.Academico.Domain.Areas.CUR.ValueObjects;
using SMC.Academico.Domain.Areas.SRC.ValueObjects;
using SMC.Framework;
using SMC.Framework.Extensions;
using SMC.Framework.Model;
using SMC.Framework.Specification;
using SMC.Framework.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SMC.Academico.Domain.Areas.CUR.DomainServices
{
    public class CurriculoCursoOfertaGrupoDomainService : AcademicoContextDomain<CurriculoCursoOfertaGrupo>
    {
        #region [ DomainService ]

        private CurriculoCursoOfertaDomainService CurriculoCursoOfertaDomainService { get => Create<CurriculoCursoOfertaDomainService>(); }
        private DivisaoMatrizCurricularComponenteDomainService DivisaoMatrizCurricularComponenteDomainService { get => Create<DivisaoMatrizCurricularComponenteDomainService>(); }
        private DivisaoMatrizCurricularGrupoDomainService DivisaoMatrizCurricularGrupoDomainService { get => Create<DivisaoMatrizCurricularGrupoDomainService>(); }
        private GrupoCurricularDomainService GrupoCurricularDomainService { get => Create<GrupoCurricularDomainService>(); }
        private MatrizCurricularDomainService MatrizCurricularDomainService { get => Create<MatrizCurricularDomainService>(); }
        private InstituicaoNivelTipoComponenteCurricularDomainService InstituicaoNivelTipoComponenteCurricularDomainService => Create<InstituicaoNivelTipoComponenteCurricularDomainService>();

        #endregion [ DomainService ]

        /// <summary>
        /// Exclui a associação com o grupo curricular (e sua hierarquia) e também as divisões de componentes filhos dos grupos
        /// nas matrizes associadas à oferta de curso excluída
        /// </summary>
        /// <param name="seq">Sequencial do grupo currícular curso oferta</param>
        public void ExcluirCurriculoCursoOfertaGrupo(long seq)
        {
            //TODO: Refactory ajuste para utilizar o próprio seq, alterar restante do método
            var seqsCurriculoCursoOfertaGrupo = this.SearchProjectionByKey(new SMCSeqSpecification<CurriculoCursoOfertaGrupo>(seq), p => new { p.SeqCurriculoCursoOferta, p.SeqGrupoCurricular });
            long seqCurriculoCursoOferta = seqsCurriculoCursoOfertaGrupo.SeqCurriculoCursoOferta;
            long seqGrupoCurricular = seqsCurriculoCursoOfertaGrupo.SeqGrupoCurricular;

            //Recupera a lista de grupos curriculares filhos para apagar a hierarquia completa
            var listIdsGrupoCurricular = GrupoCurricularDomainService.BuscarGruposCurricularesHierarquia(seqGrupoCurricular);

            using (var unitOfWork = SMCUnitOfWork.Begin())
            {
                foreach (var item in listIdsGrupoCurricular)
                {
                    var spec = new CurriculoCursoOfertaGrupoFilterSpecification()
                    {
                        SeqCurriculoCursoOferta = seqCurriculoCursoOferta,
                        SeqGrupoCurricular = item
                    };

                    //Verifica se ja foi cadastrado algum grupo nesse curriculo curso oferta grupo
                    var registroAntigo = this.SearchBySpecification(spec).FirstOrDefault();

                    if (registroAntigo != null)
                    {
                        this.DeleteEntity(registroAntigo);

                        // Recuperas as configurações de componentes filhas do grupo curricular nas matrizes desta oferta de curso
                        var specDivisaoMatrizCurricularComponente = new DivisaoMatrizCurricularComponenteFilterSpecification()
                        {
                            SeqCurriculoCursoOferta = seqCurriculoCursoOferta,
                            SeqGrupoCurricular = item
                        };
                        var divisoesMatrizCurricularComponente = DivisaoMatrizCurricularComponenteDomainService.SearchBySpecification(specDivisaoMatrizCurricularComponente);

                        foreach (var divisaoMatrizCurricularComponente in divisoesMatrizCurricularComponente)
                        {
                            DivisaoMatrizCurricularComponenteDomainService.DeleteEntity(divisaoMatrizCurricularComponente);
                        }
                    }
                }

                unitOfWork.Commit();
            }
        }

        /// <summary>
        /// Busca as quantidades de créditos e carga horária disponíveis para associação de grupos à uma oferta de curso da matriz
        /// </summary>
        /// <param name="seqCurriculoCursoOferta">Sequencial do currículo curso oferta</param>
        /// <returns>Quantidades disponíveis para associação de componentes currículares</returns>
        public CurriculoCursoOfertaGrupoVO BuscarQuantidadesDisponiveis(long seqCurriculoCursoOferta)
        {
            var headerCurriculoCursoOferta = this.CurriculoCursoOfertaDomainService.BuscarCurriculoCursoOfertaCabecalho(seqCurriculoCursoOferta, true);
            var quantidadesDisponiveis = new CurriculoCursoOfertaGrupoValorVO();

            if (headerCurriculoCursoOferta.QuantidadeCreditoObrigatorio.HasValue)
                quantidadesDisponiveis.QuantidadeCreditoObrigatorio = headerCurriculoCursoOferta.QuantidadeCreditoObrigatorio - headerCurriculoCursoOferta.QuantidadeAssociadaCreditoObrigatorio;
            if (headerCurriculoCursoOferta.QuantidadeCreditoOptativo.HasValue)
                quantidadesDisponiveis.QuantidadeCreditoOptativo = headerCurriculoCursoOferta.QuantidadeCreditoOptativo - headerCurriculoCursoOferta.QuantidadeAssociadaCreditoOptativo;
            if (headerCurriculoCursoOferta.QuantidadeHoraAulaObrigatoria.HasValue)
                quantidadesDisponiveis.QuantidadeHoraAulaObrigatoria = headerCurriculoCursoOferta.QuantidadeHoraAulaObrigatoria - headerCurriculoCursoOferta.QuantidadeAssociadaHoraAulaObrigatoria;
            if (headerCurriculoCursoOferta.QuantidadeHoraAulaOptativa.HasValue)
                quantidadesDisponiveis.QuantidadeHoraAulaOptativa = headerCurriculoCursoOferta.QuantidadeHoraAulaOptativa - headerCurriculoCursoOferta.QuantidadeAssociadaHoraAulaOptativa;
            if (headerCurriculoCursoOferta.QuantidadeHoraRelogioObrigatoria.HasValue)
                quantidadesDisponiveis.QuantidadeHoraRelogioObrigatoria = headerCurriculoCursoOferta.QuantidadeHoraRelogioObrigatoria - headerCurriculoCursoOferta.QuantidadeAssociadaHoraRelogioObrigatoria;
            if (headerCurriculoCursoOferta.QuantidadeHoraRelogioOptativa.HasValue)
                quantidadesDisponiveis.QuantidadeHoraRelogioOptativa = headerCurriculoCursoOferta.QuantidadeHoraRelogioOptativa - headerCurriculoCursoOferta.QuantidadeAssociadaHoraRelogioOptativa;

            return new CurriculoCursoOfertaGrupoVO() { QuantidadesDisponiveis = quantidadesDisponiveis };
        }

        /// <summary>
        /// Calcula o valor de um grupo curricular em carga horária e créditos
        /// </summary>
        /// <param name="seqCurriculoCursoOferta">Sequncial da oferta de curso pai do CurriculoCursoOfertaGrupo</param>
        /// <param name="seqGrupoCurricular">Sequencial do grupo curricular a ser calculado</param>
        /// <param name="incremental">Define se deverá desconsiderar o valor de subgrupos e componentes vinculadoas anteriormente</param>
        /// <returns>Valor de carga horária e créditos dos componentes associados ao grupo curricular e seus subgrupos</returns>
        public CurriculoCursoOfertaGrupoValorVO BuscarValorCurriculoCursoOfertaGrupo(long seqCurriculoCursoOferta, long seqGrupoCurricular, bool incremental)
        {
            var seqCurriculo = this.CurriculoCursoOfertaDomainService.SearchProjectionByKey(new SMCSeqSpecification<CurriculoCursoOferta>(seqCurriculoCursoOferta), p => p.SeqCurriculo);
            var gruposCurriculo = this.GrupoCurricularDomainService.BuscarGruposCurricularesTree(seqCurriculo);

            // Caso seja incremental desconsidera os grupos já associados ao CurriculoCursoOferta
            if (incremental)
            {
                var seqsGruposCurriculoCursoOferta = this.GrupoCurricularDomainService
                    .BuscarGruposCurricularesTreeCurriculoCursoOferta(seqCurriculoCursoOferta)
                    .Select(s => new { s.SeqGrupoCurricular, s.SeqComponenteCurricular })
                    .ToList();

                // Filtra todos os grupos e componentes do curículo que ainda não estejam associados
                gruposCurriculo = gruposCurriculo
                    .Where(w =>
                        !seqsGruposCurriculoCursoOferta
                            .Any(a => a.SeqGrupoCurricular == w.SeqGrupoCurricular && a.SeqComponenteCurricular == w.SeqComponenteCurricular))
                    .ToList();
            }

            var grupoACalcular = BuscarGrupoSubgruposComponentes(gruposCurriculo, seqGrupoCurricular);
            return SomarQuantidadesCurriculoCursoOfertaGrupo(grupoACalcular);
        }

        /// <summary>
        /// Filtra um grupo curricular com seus sub grupos e componentes
        /// </summary>
        /// <param name="gruposComponentes">Lista com grupos curriculares e componentes</param>
        /// <param name="seqCurriculoOfertaGrupo">Sequencial do grupo curricular raiz</param>
        /// <returns>Subgrupos e componentes do grupo informado</returns>
        public List<GrupoCurricularListaVO> BuscarGrupoSubgruposComponentes(IEnumerable<GrupoCurricularListaVO> gruposComponentes, long seqCurriculoOfertaGrupo)
        {
            var retorno = new List<GrupoCurricularListaVO>();
            IEnumerable<GrupoCurricularListaVO> itens = gruposComponentes.Where(w => w.SeqGrupoCurricular == seqCurriculoOfertaGrupo && w.SeqComponenteCurricular == null);
            IEnumerable<long?> seqsPais;
            while (itens.Any())
            {
                retorno.AddRange(itens);
                seqsPais = itens.Select(s => (long?)s.Seq).ToList();
                itens = gruposComponentes.Where(w => seqsPais.Contains(w.SeqPai));
            }

            return retorno;
        }

        /// <summary>
        /// Soma as quantidades de créditos e horas aula de um grupo curriular, seus subgrupos e componentes segundo a regra RN_CUR_020
        /// </summary>
        /// <param name="listaCompleta">Lista dos grupos curriculares com seus subgrupos e componentes</param>
        /// <returns>Total de créditos e horas aulas dos grupos e componentes</returns>
        public CurriculoCursoOfertaGrupoValorVO SomarQuantidadesCurriculoCursoOfertaGrupo(IEnumerable<GrupoCurricularListaVO> listaCompleta, bool considerarCargaHoraria = false)
        {
            var hierarquiaCurriculoCursoOfertaGrupo = MontarHirarquiaGrupoCurricular(listaCompleta);
            // Cria um item de raiz que não afeta o total apenas para poder usar a função recursiva
            var raizHierarquiaCurriculoCursoOfertaGrupo = new GrupoCurricularListaVO()
            {
                SeqGrupoCurricular = -1,
                FormatoConfiguracaoGrupo = FormatoConfiguracaoGrupo.Nenhum,
                Itens = hierarquiaCurriculoCursoOfertaGrupo
            };
            return SomarQuantidadesCurriculoCursoOfertaGrupoHierarquia(raizHierarquiaCurriculoCursoOfertaGrupo, considerarCargaHoraria).Single(s => s.SeqGrupoCurricular == -1);
        }

        /// <summary>
        /// Sequencial do currículo curso oferta para calcular os valores de carga horária e créditos
        /// </summary>
        /// <param name="seqCurriculoCursoOferta">Sequencial do currículo curso oferta</param>
        /// <returns>Somatório dos valores dos grupos currículares numa lista plana</returns>
        public List<CurriculoCursoOfertaGrupoValorVO> BuscarValoresCurriculoCursoOfertaGrupoPorOferta(long seqCurriculoCursoOferta)
        {
            var gruposCurriculo = this.GrupoCurricularDomainService.BuscarGruposCurricularesTreeCurriculoCursoOferta(seqCurriculoCursoOferta).ToList();
            var hieraquiaGrupos = MontarHirarquiaGrupoCurricular(gruposCurriculo);
            // Cria um item de raiz que não afeta o total apenas para poder usar a função recursiva
            var raiz = new GrupoCurricularListaVO()
            {
                SeqGrupoCurricular = -1,
                Formato = FormatoCargaHoraria.Nenhum,
                Itens = hieraquiaGrupos
            };
            // Ignora a raiz no retorno para devolver a lista expandida
            return SomarQuantidadesCurriculoCursoOfertaGrupoHierarquia(raiz).Where(w => w.SeqGrupoCurricular != -1).ToList();
        }

        /// <summary>
        /// Lista plana com os grupos, subgrupos e componentes a serem organizados
        /// </summary>
        /// <param name="gruposComponentes">Lista original com grupos, subgrupos e compnoenentes</param>
        /// <returns>Lista de grupos raiz com seus itens aninhados</returns>
        public List<GrupoCurricularListaVO> MontarHirarquiaGrupoCurricular(IEnumerable<GrupoCurricularListaVO> gruposComponentes)
        {
            foreach (var grupo in gruposComponentes.Where(w => w.SeqComponenteCurricular == null))
            {
                grupo.Itens = gruposComponentes.Where(w => w.SeqPai == grupo.Seq).ToList();
            }
            return gruposComponentes.Where(w => w.SeqComponenteCurricular == null && w.SeqPai == null).ToList();
        }

        /// <summary>
        /// Soma as quantidades de créditos e horas aula de um grupo curriular, seus subgrupos e componentes já organizados numa hierarquia
        /// </summary>
        /// <param name="grupoCurricular">Grupo curriculare com seus subgrupos e componentes</param>
        /// <returns>Total de créditos e horas aulas do grupo com seus subgrupos e componentes</returns>
        public IEnumerable<CurriculoCursoOfertaGrupoValorVO> SomarQuantidadesCurriculoCursoOfertaGrupoHierarquia(GrupoCurricularListaVO grupoCurricular, bool considerarCargaHoraria = false)
        {
            // Define o tratamento para o grupo segundo seu formato
            switch (grupoCurricular.FormatoConfiguracaoGrupo)
            {
                // Caso seja crédito ou carga horária, já retorna seu valor e termina a iteração
                case FormatoConfiguracaoGrupo.Credito:
                    var creditosGrupo = new CurriculoCursoOfertaGrupoValorVO() { SeqGrupoCurricular = grupoCurricular.SeqGrupoCurricular.Value };
                    if (grupoCurricular.Obrigatorio)
                        creditosGrupo.QuantidadeCreditoObrigatorio = (grupoCurricular.QuantidadeCreditos ?? 0);
                    else
                        creditosGrupo.QuantidadeCreditoOptativo = (grupoCurricular.QuantidadeCreditos ?? 0);
                    yield return creditosGrupo;
                    yield break;

                case FormatoConfiguracaoGrupo.CargaHoraria:
                    var cargaHorariaGrupo = new CurriculoCursoOfertaGrupoValorVO() { SeqGrupoCurricular = grupoCurricular.SeqGrupoCurricular.Value };
                    if (grupoCurricular.Obrigatorio)
                    {
                        cargaHorariaGrupo.QuantidadeHoraAulaObrigatoria = (grupoCurricular.QuantidadeHoraAula ?? 0);
                        cargaHorariaGrupo.QuantidadeHoraRelogioObrigatoria = (grupoCurricular.QuantidadeHoraRelogio ?? 0);
                    }
                    else
                    {
                        cargaHorariaGrupo.QuantidadeHoraAulaOptativa = (grupoCurricular.QuantidadeHoraAula ?? 0);
                        cargaHorariaGrupo.QuantidadeHoraRelogioOptativa = (grupoCurricular.QuantidadeHoraRelogio ?? 0);
                    }
                    yield return cargaHorariaGrupo;
                    yield break;

                // Caso contrário, soma os valores dos itens do grupo
                default:
                    var totalAcumulado = new CurriculoCursoOfertaGrupoValorVO() { SeqGrupoCurricular = grupoCurricular.SeqGrupoCurricular.GetValueOrDefault() };
                    foreach (var item in grupoCurricular.Itens)
                    {
                        // Caso seja um componente
                        if (item.SeqComponenteCurricular.HasValue || item.SeqGrupoComponenteCurricular.HasValue)
                        {
                            if (grupoCurricular.Obrigatorio)
                            {
                                totalAcumulado.QuantidadeCreditoObrigatorio = (totalAcumulado.QuantidadeCreditoObrigatorio ?? 0) + (item.Credito ?? 0);
                                if ((item.Credito ?? 0) == 0 || considerarCargaHoraria)
                                {
                                    if (item.Formato == FormatoCargaHoraria.HoraAula)
                                        totalAcumulado.QuantidadeHoraAulaObrigatoria = (totalAcumulado.QuantidadeHoraAulaObrigatoria ?? 0) + (item.CargaHoraria ?? 0);
                                    else
                                        totalAcumulado.QuantidadeHoraRelogioObrigatoria = (totalAcumulado.QuantidadeHoraRelogioObrigatoria ?? 0) + (item.CargaHoraria ?? 0);
                                }
                            }
                            else
                            {
                                totalAcumulado.QuantidadeCreditoOptativo = (totalAcumulado.QuantidadeCreditoOptativo ?? 0) + (item.Credito ?? 0);
                                if ((item.Credito ?? 0) == 0 || considerarCargaHoraria)
                                {
                                    if (item.Formato == FormatoCargaHoraria.HoraAula)
                                        totalAcumulado.QuantidadeHoraAulaOptativa = (totalAcumulado.QuantidadeHoraAulaOptativa ?? 0) + (item.CargaHoraria ?? 0);
                                    else
                                        totalAcumulado.QuantidadeHoraRelogioOptativa = (totalAcumulado.QuantidadeHoraRelogioOptativa ?? 0) + (item.CargaHoraria ?? 0);
                                }
                            }
                        }
                        // Caso seja um subgrupo
                        else
                        {
                            // Soma os valores dos subgrupos no total acumulado
                            foreach (var subGrupo in SomarQuantidadesCurriculoCursoOfertaGrupoHierarquia(item, considerarCargaHoraria))
                            {
                                if (subGrupo.QuantidadeCreditoObrigatorio.HasValue)
                                    totalAcumulado.QuantidadeCreditoObrigatorio = (totalAcumulado.QuantidadeCreditoObrigatorio ?? 0) + (subGrupo.QuantidadeCreditoObrigatorio ?? 0);
                                if (subGrupo.QuantidadeCreditoOptativo.HasValue)
                                    totalAcumulado.QuantidadeCreditoOptativo = (totalAcumulado.QuantidadeCreditoOptativo ?? 0) + (subGrupo.QuantidadeCreditoOptativo ?? 0);
                                if (subGrupo.QuantidadeHoraAulaObrigatoria.HasValue)
                                    totalAcumulado.QuantidadeHoraAulaObrigatoria = (totalAcumulado.QuantidadeHoraAulaObrigatoria ?? 0) + (subGrupo.QuantidadeHoraAulaObrigatoria ?? 0);
                                if (subGrupo.QuantidadeHoraAulaOptativa.HasValue)
                                    totalAcumulado.QuantidadeHoraAulaOptativa = (totalAcumulado.QuantidadeHoraAulaOptativa ?? 0) + (subGrupo.QuantidadeHoraAulaOptativa ?? 0);
                                if (subGrupo.QuantidadeHoraRelogioObrigatoria.HasValue)
                                    totalAcumulado.QuantidadeHoraRelogioObrigatoria = (totalAcumulado.QuantidadeHoraRelogioObrigatoria ?? 0) + (subGrupo.QuantidadeHoraRelogioObrigatoria ?? 0);
                                if (subGrupo.QuantidadeHoraRelogioOptativa.HasValue)
                                    totalAcumulado.QuantidadeHoraRelogioOptativa = (totalAcumulado.QuantidadeHoraRelogioOptativa ?? 0) + (subGrupo.QuantidadeHoraRelogioOptativa ?? 0);
                                // Retorna o total do subgrupo para disponibilizar também os valores individuais
                                yield return subGrupo;
                            }
                        }
                    }
                    // Retorna o total acumulado do grupo atual e seus subgrupos
                    yield return totalAcumulado;
                    yield break;
            }
        }

        public void SalvarCurriculoCursoOfertaGrupo(CurriculoCursoOfertaGrupo curriculoCursoOfertaGrupo)
        {
            //Recupera a lista de grupos curriculares filhos para gravar a hierarquia completa
            var listIdsGrupoCurricular = GrupoCurricularDomainService.BuscarGruposCurricularesHierarquia(curriculoCursoOfertaGrupo.SeqGrupoCurricular);

            foreach (var item in listIdsGrupoCurricular)
            {
                var spec = new CurriculoCursoOfertaGrupoFilterSpecification()
                {
                    SeqCurriculoCursoOferta = curriculoCursoOfertaGrupo.SeqCurriculoCursoOferta,
                    SeqGrupoCurricular = item
                };

                //Verifica se ja foi cadastrado algum grupo nesse curriculo curso oferta grupo
                var registroAntigo = this.SearchBySpecification(spec).FirstOrDefault();

                if (registroAntigo == null)
                {
                    var registro = new CurriculoCursoOfertaGrupo();
                    registro.SeqCurriculoCursoOferta = curriculoCursoOfertaGrupo.SeqCurriculoCursoOferta;
                    registro.SeqGrupoCurricular = item;
                    registro.Obrigatorio = curriculoCursoOfertaGrupo.Obrigatorio;
                    registro.ExibidoHistoricoEscolar = curriculoCursoOfertaGrupo.ExibidoHistoricoEscolar;
                    registro.DesconsiderarIntegralizacao = curriculoCursoOfertaGrupo.DesconsiderarIntegralizacao;
                    this.SaveEntity(registro);
                }
            }

            // Apenas se for um registro gravado anteriormente atualiza os próprios campos
            if (!curriculoCursoOfertaGrupo.IsNew())
            {
                this.SaveEntity(curriculoCursoOfertaGrupo);
            }
        }

        /// <summary>
        /// Busca um grupo curricular curso oferta
        /// </summary>
        /// <param name="filtroVO">Dados do filtro</param>
        /// <returns>Dados do grupo currícular curso oferta</returns>
        public CurriculoCursoOfertaGrupo BuscarCurriculoCursoOfertaGrupo(CurriculoCursoOfertaGrupoFiltroVO filtroVO)
        {
            var spec = filtroVO.Transform<CurriculoCursoOfertaGrupoFilterSpecification>();
            if (filtroVO.SeqAluno.HasValue && filtroVO.SeqCicloLetivo.HasValue)
            {
                spec.SeqCurriculoCursoOferta = CurriculoCursoOfertaDomainService.BuscarCurriculoCursoOfertaPorAluno(filtroVO.SeqAluno.Value, filtroVO.SeqCicloLetivo.Value)?.Seq ?? 0;
            }
            return this.SearchByKey(spec);
        }

        /// <summary>
        /// Busca os curriculo curso ofertas grupos com seus respectivos grupos curriculares de uma matriz curricular
        /// </summary>
        /// <param name="seq">Sequencial do curriculo curso oferta grupo</param>
        /// <param name="seqMatrizCurricular">Sequencial da matriz curricular</param>
        /// <returns>Dados dos grupos curriculares</returns>
        public List<SMCDatasourceItem> BuscarCurriculoCursoOfertasGruposSelect(long seq, long seqMatrizCurricular)
        {
            var gruposMatrizCurricular = this.MatrizCurricularDomainService
                .SearchByKey(new SMCSeqSpecification<MatrizCurricular>(seqMatrizCurricular)
                , IncludesMatrizCurricular.CurriculoCursoOferta_GruposCurriculares_GrupoCurricular
                | IncludesMatrizCurricular.ConfiguracoesComponente_GrupoCurricularComponente);

            var gruposComDivisao = gruposMatrizCurricular.ConfiguracoesComponente
                                                         .Where(w => w.SeqDivisaoMatrizCurricular != null)
                                                         .Select(s => s.GrupoCurricularComponente.SeqGrupoCurricular)
                                                         .Distinct();

            var gruposJaAssociados = this.DivisaoMatrizCurricularGrupoDomainService.SearchProjectionBySpecification(
                                            new DivisaoMatrizCurricularGrupoFilterSpecification() { SeqMatrizCurricular = seqMatrizCurricular },
                                            p => p.CurriculoCursoOfertaGrupo.SeqGrupoCurricular).Distinct();

            var gruposDisponiveis = gruposMatrizCurricular.CurriculoCursoOferta
                                                          .GruposCurriculares
                                                          .Where(w => !gruposComDivisao.Contains(w.SeqGrupoCurricular))
                                                          .Distinct();

            if (seq == 0)
                gruposDisponiveis = gruposDisponiveis.Where(w => !gruposJaAssociados.Contains(w.SeqGrupoCurricular)).Distinct();

            List<SMCDatasourceItem> retorno = gruposDisponiveis.Select(s => new SMCDatasourceItem() { Seq = s.Seq, Descricao = s.GrupoCurricular.Descricao }).ToList();
            return retorno;
        }

        /// <summary>
        /// Busca o tipo de configuração e as quantidades de um curriculo curso oferta grupo
        /// </summary>
        /// <param name="seq">Sequencial do curriculo curso oferta grupo</param>
        /// <returns>Dados do curriculo curso oferta grupo</returns>
        public CurriculoCursoOfertaGrupoVO BuscarCurriculoCursoOfertaGrupoComTipoConfiguracao(long seq)
        {
            var curriculaCursoOfertaGrupo = this.SearchByKey(new SMCSeqSpecification<CurriculoCursoOfertaGrupo>(seq),
                IncludesCurriculoCursoOfertaGrupo.GrupoCurricular_TipoConfiguracaoGrupoCurricular);

            var curriculaCursoOfertaGrupoVo = new CurriculoCursoOfertaGrupoVO()
            {
                Seq = curriculaCursoOfertaGrupo.Seq,
                SeqTipoConfiguracaoGrupoCurricular = curriculaCursoOfertaGrupo.GrupoCurricular.SeqTipoConfiguracaoGrupoCurricular,
                DescricaoTipoConfiguracaoGrupoCurricular = curriculaCursoOfertaGrupo.GrupoCurricular.TipoConfiguracaoGrupoCurricular.Descricao,
                FormatoConfiguracaoGrupoGrupoCurricular = curriculaCursoOfertaGrupo.GrupoCurricular.FormatoConfiguracaoGrupo
            };

            curriculaCursoOfertaGrupoVo.QuantidadeFormatada = BuscaQuantidadeFormatada(curriculaCursoOfertaGrupoVo.FormatoConfiguracaoGrupoGrupoCurricular, curriculaCursoOfertaGrupo);

            return curriculaCursoOfertaGrupoVo;
        }

        /// <summary>
        /// Calcular os valores totais dos grupos de componentes informados
        /// </summary>
        /// <param name="seqsCurriculoCursoOfertaGrupo">Lista de sequenciais dos curriculos curso oferta grupo</param>
        /// <returns>Objeto com o total de hora, hora-aula e créditos</returns>
        public TotalHoraCreditoVO CalculaHoraCreditoCurriculoCursoOfertaGrupo(long seqInstituicaoEnsino, List<DadosCalculoCurriculoCursoOfertaGrupo> dadosCurriculos)
        {
            float totalHoras = 0;
            float totalHorasAula = 0;
            float totalCreditos = 0;
            float totalCreditosPorHora = 0;
            float totalHorasPorCredito = 0;

            // Quantidade de carga horária para cálculo caso não encontre no componente curricular específico
            // Quando não encontrar componente curricular com parametrização, deve usar a parametrização do tipo Disciplina (Regra da Janice)
            var quantidadeHorasPorCreditoDisciplina = InstituicaoNivelTipoComponenteCurricularDomainService.BuscarQuantidadeHorasPorCreditoDisciplina(seqInstituicaoEnsino);

            var specCurriculoCursoOfertaGrupo = new SMCContainsSpecification<CurriculoCursoOfertaGrupo, long>(g => g.Seq, dadosCurriculos.Select(d => d.SeqCurriculoCursoOfertaGrupo).ToArray());

            var curriculosCursosOfertasGrupos = this.SearchProjectionBySpecification(specCurriculoCursoOfertaGrupo, g => new
            {
                g.GrupoCurricular.QuantidadeHoraRelogio,
                g.GrupoCurricular.QuantidadeHoraAula,
                g.GrupoCurricular.QuantidadeCreditos,
                g.Seq
            }).ToList();

            foreach (var item in curriculosCursosOfertasGrupos)
            {
                // Recupera o total de créditos a serem dispensados para o grupo (informado manualmente)
                var creditoDispensar = dadosCurriculos.FirstOrDefault(d => d.SeqCurriculoCursoOfertaGrupo == item.Seq)?.QuantidadeDispensaGrupo;

                totalHoras += item.QuantidadeHoraRelogio.GetValueOrDefault();
                totalHorasAula += item.QuantidadeHoraAula.GetValueOrDefault();

                if (creditoDispensar.HasValue)
                    totalCreditos += creditoDispensar.Value;
                else
                    totalCreditos += item.QuantidadeCreditos.GetValueOrDefault();

                // Atribui o valor específico do tipo disciplina (regra da Janice)
                if (quantidadeHorasPorCreditoDisciplina.HasValue && quantidadeHorasPorCreditoDisciplina.Value > 0)
                {
                    totalCreditosPorHora += (item.QuantidadeHoraRelogio.GetValueOrDefault() / quantidadeHorasPorCreditoDisciplina.Value);
                    totalHorasPorCredito += (item.QuantidadeCreditos.GetValueOrDefault() * quantidadeHorasPorCreditoDisciplina.Value);
                }
            }

            return new TotalHoraCreditoVO() { TotalHoras = Convert.ToDecimal(totalHoras), TotalHorasAula = Convert.ToDecimal(totalHorasAula), TotalCreditos = Convert.ToDecimal(totalCreditos), TotalCreditosPorHora = Convert.ToDecimal(totalCreditosPorHora), TotalHorasPorCredito = Convert.ToDecimal(totalHorasPorCredito) };
        }

        internal static string BuscaQuantidadeFormatada(FormatoConfiguracaoGrupo? formatoConfiguracaoGrupo, CurriculoCursoOfertaGrupo curriculaCursoOfertaGrupo)
        {
            switch (formatoConfiguracaoGrupo)
            {
                case FormatoConfiguracaoGrupo.CargaHoraria:
                    return $"{curriculaCursoOfertaGrupo.GrupoCurricular.QuantidadeHoraRelogio} hora - {curriculaCursoOfertaGrupo.GrupoCurricular.QuantidadeHoraAula} hora-aula";

                case FormatoConfiguracaoGrupo.Credito:
                    return $"{curriculaCursoOfertaGrupo.GrupoCurricular.QuantidadeCreditos} crédito";

                case FormatoConfiguracaoGrupo.Itens:
                    return $"{curriculaCursoOfertaGrupo.GrupoCurricular.QuantidadeItens} itens";

                default:
                    return string.Empty;
            }
        }
    }
}