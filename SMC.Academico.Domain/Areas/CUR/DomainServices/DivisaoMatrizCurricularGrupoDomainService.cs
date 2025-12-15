using SMC.Academico.Common.Areas.CUR.Enums;
using SMC.Academico.Common.Areas.CUR.Exceptions;
using SMC.Academico.Common.Areas.CUR.Includes;
using SMC.Academico.Domain.Areas.CUR.Models;
using SMC.Academico.Domain.Areas.CUR.Specifications;
using SMC.Academico.Domain.Areas.CUR.ValueObjects;
using SMC.Framework;
using SMC.Framework.Domain;
using SMC.Framework.Extensions;
using SMC.Framework.Model;
using SMC.Framework.Specification;
using SMC.Framework.UnitOfWork;
using System.Collections.Generic;
using System.Linq;

namespace SMC.Academico.Domain.Areas.CUR.DomainServices
{
    public class DivisaoMatrizCurricularGrupoDomainService : AcademicoContextDomain<DivisaoMatrizCurricularGrupo>
    {
        #region [ DomainServices ]

        private CurriculoCursoOfertaGrupoDomainService CurriculoCursoOfertaGrupoDomainService
        {
            get { return this.Create<CurriculoCursoOfertaGrupoDomainService>(); }
        }

        #endregion [ DomainServices ]

        /// <summary>
        /// Buscar uma lista de divisao matriz curricular configuracao componente pelo sequencial do curriculo curso oferta grupo
        /// </summary>
        /// <param name="seqCurriculoCursoOfertaGrupo">Sequencia do curriculo curso oferta grupo</param>
        /// <returns>Matriz curricular configuracao componente recuperado</returns>
        public MatrizCurricularConfiguracaoComponenteVO BuscarMatrizCurricularConfiguracaoComponente(long seqCurriculoCursoOfertaGrupo)
        {
            var filtro = new DivisaoMatrizCurricularGrupoFilterSpecification() { SeqCurriculoCursoOfertaGrupo = seqCurriculoCursoOfertaGrupo };
            var includes = IncludesDivisaoMatrizCurricularGrupo.DivisaoMatrizCurricular_DivisaoCurricularItem
                         | IncludesDivisaoMatrizCurricularGrupo.CurriculoCursoOfertaGrupo_GrupoCurricular_TipoConfiguracaoGrupoCurricular;
            var divisoes = this.SearchBySpecification(filtro, includes);
            var grupo = divisoes.First().CurriculoCursoOfertaGrupo;
            return new MatrizCurricularConfiguracaoComponenteVO()
            {
                Seq = grupo.Seq,
                SeqCurriculoCursoOfertaGrupo = grupo.Seq,
                DescricaoCurriculoCursoOfertaGrupo = grupo.GrupoCurricular.Descricao,
                DescricaoTipoConfiguracaoGrupoCurricular = grupo.GrupoCurricular.TipoConfiguracaoGrupoCurricular.Descricao,
                FormatoConfiguracaoGrupoGrupoCurricular = grupo.GrupoCurricular.FormatoConfiguracaoGrupo,
                QuantidadeFormatada = CurriculoCursoOfertaGrupoDomainService.BuscaQuantidadeFormatada(grupo.GrupoCurricular.FormatoConfiguracaoGrupo, grupo),
                DivisaoMatrizCurricularGrupo = divisoes.Where(w => w.SeqCurriculoCursoOfertaGrupo == grupo.Seq)
                        .TransformList<MatrizCurricularConfiguracaoComponenteDivisaoVO>()
            };
        }

        /// <summary>
        /// Buscar as matrizes curriculares configuracao componentes que atendam os filtros informados e com a paginação correta
        /// </summary>
        /// <param name="filtros">Filtros da listagem de matriz curricular configuracao componente</param>
        /// <returns>SMCPagerData com a lista de matriz curricular configuracao componente</returns>
        public SMCPagerData<MatrizCurricularConfiguracaoComponenteVO> BuscarMatrizesCurricularesConfiguracoesComponente(DivisaoMatrizCurricularGrupoFilterSpecification filtros)
        {
            int total;
            var includes = IncludesDivisaoMatrizCurricularGrupo.DivisaoMatrizCurricular_DivisaoCurricularItem
                         | IncludesDivisaoMatrizCurricularGrupo.CurriculoCursoOfertaGrupo_GrupoCurricular_TipoConfiguracaoGrupoCurricular;
            var divisoes = this.SearchBySpecification(filtros, out total, includes);
            var grupos = divisoes.Select(s => s.CurriculoCursoOfertaGrupo).SMCDistinct(p => p.Seq);
            var retorno = new List<MatrizCurricularConfiguracaoComponenteVO>();
            foreach (var grupo in grupos)
            {
                retorno.Add(new MatrizCurricularConfiguracaoComponenteVO()
                {
                    Seq = grupo.Seq,
                    SeqCurriculoCursoOfertaGrupo = grupo.Seq,
                    DescricaoCurriculoCursoOfertaGrupo = grupo.GrupoCurricular.Descricao,
                    DescricaoTipoConfiguracaoGrupoCurricular = grupo.GrupoCurricular.TipoConfiguracaoGrupoCurricular.Descricao,
                    FormatoConfiguracaoGrupoGrupoCurricular = grupo.GrupoCurricular.FormatoConfiguracaoGrupo,
                    QuantidadeFormatada = CurriculoCursoOfertaGrupoDomainService.BuscaQuantidadeFormatada(grupo.GrupoCurricular.FormatoConfiguracaoGrupo, grupo),
                    DivisaoMatrizCurricularGrupo = divisoes.Where(w => w.SeqCurriculoCursoOfertaGrupo == grupo.Seq)
                        .TransformList<MatrizCurricularConfiguracaoComponenteDivisaoVO>()
                });
            }
            return new SMCPagerData<MatrizCurricularConfiguracaoComponenteVO>(retorno, total);
        }

        /// <summary>
        /// Salva uma matriz curricular configuracao componente
        /// </summary>
        /// <param name="matrizCurricularConfiguracaoComponente">Dados da matriz curricular configuracao componente a gravada</param>
        /// <returns>Sequencial da matriz curricular configuracao componente gravada</returns>
        public long SalvarMatrizCurricularConfiguracaoComponente(MatrizCurricularConfiguracaoComponenteVO matrizCurricularConfiguracaoComponente)
        {
            ValidarQuantidade(matrizCurricularConfiguracaoComponente);

            var divisoesMatrizCurricularGravar = new List<DivisaoMatrizCurricularGrupo>();
            var divisoesMatrizCurricular = this.SearchBySpecification(new DivisaoMatrizCurricularGrupoFilterSpecification()
            {
                SeqCurriculoCursoOfertaGrupo = matrizCurricularConfiguracaoComponente.SeqCurriculoCursoOfertaGrupo
            }).ToList();

            foreach (var divisaoVo in matrizCurricularConfiguracaoComponente.DivisaoMatrizCurricularGrupo)
            {
                // Recupera para atualização ou cria um novo item
                var divisaoGravar = divisoesMatrizCurricular.FirstOrDefault(f => f.Seq == divisaoVo.Seq) ?? new DivisaoMatrizCurricularGrupo();
                divisaoGravar.SeqCurriculoCursoOfertaGrupo = matrizCurricularConfiguracaoComponente.SeqCurriculoCursoOfertaGrupo;
                divisaoGravar.SeqDivisaoMatrizCurricular = divisaoVo.SeqDivisaoMatrizCurricular;
                divisaoGravar.QuantidadeCreditos = divisaoVo.QuantidadeCreditos;
                divisaoGravar.QuantidadeHoraAula = divisaoVo.QuantidadeHoraAula;
                divisaoGravar.QuantidadeHoraRelogio = divisaoVo.QuantidadeHoraRelogio;
                divisaoGravar.QuantidadeItens = divisaoVo.QuantidadeItens;
                divisoesMatrizCurricularGravar.Add(divisaoGravar);
            }

            using (var tran = SMCUnitOfWork.Begin())
            {
                // Grava todas divisões de matriz curricular grupo criadas ou alteradas
                divisoesMatrizCurricularGravar.SMCForEach(f => this.SaveEntity(f));
                // Exclui as divisões de matriz curricular grupo que não estiverem no vo
                divisoesMatrizCurricular.Select(s => s.Seq).Except(divisoesMatrizCurricularGravar.Select(s => s.Seq)).SMCForEach(f => this.DeleteEntity(f));
                tran.Commit();
            }

            return matrizCurricularConfiguracaoComponente.SeqCurriculoCursoOfertaGrupo;
        }

        /// <summary>
        /// Exclui todas as divisões matriz curricular grupo de um curriculo curso oferta grupo
        /// </summary>
        /// <param name="seqCurriculoCursoOfertaGrupo">Sequencial do curriculo curso oferta grupo</param>
        public void ExcluirMatrizCurricularConfiguracaoComponente(long seqCurriculoCursoOfertaGrupo)
        {
            var filtro = new DivisaoMatrizCurricularGrupoFilterSpecification() { SeqCurriculoCursoOfertaGrupo = seqCurriculoCursoOfertaGrupo };
            var divisoes = this.SearchBySpecification(filtro);

            using (ISMCUnitOfWork transacao = SMCUnitOfWork.Begin())
            {
                divisoes.SMCForEach(f => this.DeleteEntity(f));

                transacao.Commit();
            }
        }

        private void ValidarQuantidade(MatrizCurricularConfiguracaoComponenteVO matrizCurricularConfiguracaoComponente)
        {
            if (matrizCurricularConfiguracaoComponente.DivisaoMatrizCurricularGrupo.SMCCount() == 0)
                throw new MatrizCurricularConfiguracaoComponenteNenhumaDivisaoException();

            var specGrupo = new SMCSeqSpecification<CurriculoCursoOfertaGrupo>(matrizCurricularConfiguracaoComponente.SeqCurriculoCursoOfertaGrupo);
            var totaisGrupo = this.CurriculoCursoOfertaGrupoDomainService
                .SearchProjectionByKey(specGrupo, p => new
                {
                    p.GrupoCurricular.FormatoConfiguracaoGrupo,
                    p.GrupoCurricular.QuantidadeCreditos,
                    p.GrupoCurricular.QuantidadeHoraAula,
                    p.GrupoCurricular.QuantidadeHoraRelogio,
                    p.GrupoCurricular.QuantidadeItens
                });
            var divisoes = matrizCurricularConfiguracaoComponente.DivisaoMatrizCurricularGrupo;
            switch (totaisGrupo.FormatoConfiguracaoGrupo)
            {
                case FormatoConfiguracaoGrupo.CargaHoraria:
                    if (divisoes.Sum(s => s.QuantidadeHoraAula) != totaisGrupo.QuantidadeHoraAula ||
                        divisoes.Sum(s => s.QuantidadeHoraRelogio) != totaisGrupo.QuantidadeHoraRelogio)
                        throw new DivisaoMatrizCurricularGrupoException();
                    break;

                case FormatoConfiguracaoGrupo.Credito:
                    if (divisoes.Sum(s => s.QuantidadeCreditos) != totaisGrupo.QuantidadeCreditos)
                        throw new DivisaoMatrizCurricularGrupoException();
                    break;

                case FormatoConfiguracaoGrupo.Itens:
                    if (divisoes.Sum(s => s.QuantidadeItens) != totaisGrupo.QuantidadeItens)
                        throw new DivisaoMatrizCurricularGrupoException();
                    break;
            }
        }
    }
}