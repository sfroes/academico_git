using SMC.Academico.Common.Areas.CUR.Includes;
using SMC.Academico.Domain.Areas.ALN.DomainServices;
using SMC.Academico.Domain.Areas.ALN.Models;
using SMC.Academico.Domain.Areas.CUR.Models;
using SMC.Academico.Domain.Areas.CUR.ValueObjects;
using SMC.Framework.Domain;
using SMC.Framework.Specification;
using System.Linq;

namespace SMC.Academico.Domain.Areas.CUR.DomainServices
{
    public class CurriculoCursoOfertaDomainService : AcademicoContextDomain<CurriculoCursoOferta>
    {
        #region [ DomainService ]

        private GrupoCurricularDomainService GrupoCurricularDomainService
        {
            get { return this.Create<GrupoCurricularDomainService>(); }
        }

        private CurriculoCursoOfertaGrupoDomainService CurriculoCursoOfertaGrupoDomainService
        {
            get { return this.Create<CurriculoCursoOfertaGrupoDomainService>(); }
        }

        private AlunoDomainService AlunoDomainService { get => Create<AlunoDomainService>(); }

        #endregion [ DomainService ]

        /// <summary>
        /// Buscar curriculo, curso e curso oferta para o cabeçalho
        /// </summary>
        /// <param name="seq">Sequencial do curriculo curso oferta</param>
        /// <returns>CurriculoCursoOfertaVO com o cabeçalho</returns>
        public CurriculoCursoOfertaVO BuscarCurriculoCursoOfertaCabecalho(long seq, bool total)
        {
            var curriculoCursoOferta = this.SearchProjectionByKey(new SMCSeqSpecification<CurriculoCursoOferta>(seq), registro =>
                new CurriculoCursoOfertaVO()
                {
                    Seq = registro.Seq,
                    SeqCurriculo = registro.SeqCurriculo,
                    SeqCurso = registro.Curriculo.SeqCurso,
                    SeqCursoOferta = registro.SeqCursoOferta,
                    CurriculoCodigo = registro.Curriculo.Codigo,
                    CurriculoDescricao = registro.Curriculo.Descricao,
                    CurriculoDescricaoComplementar = registro.Curriculo.DescricaoComplementar,
                    CursoSigla = registro.Curriculo.Curso.Sigla,
                    CursoDescricao = registro.Curriculo.Curso.Nome,
                    CursoOfertaDescricao = registro.CursoOferta.Descricao,
                    QuantidadeHoraAulaObrigatoria = registro.QuantidadeHoraAulaObrigatoria,
                    QuantidadeHoraRelogioObrigatoria = registro.QuantidadeHoraRelogioObrigatoria,
                    QuantidadeCreditoObrigatorio = registro.QuantidadeCreditoObrigatorio,
                    QuantidadeHoraAulaOptativa = registro.QuantidadeHoraAulaOptativa,
                    QuantidadeHoraRelogioOptativa = registro.QuantidadeHoraRelogioOptativa,
                    QuantidadeCreditoOptativo = registro.QuantidadeCreditoOptativo,
                    Ativo = registro.Curriculo.Ativo
                });

            if (total)
            {
                // Recupera todos grupos curriculares e componentes associados ao currículo
                var gruposCurricularesComponentes = this.GrupoCurricularDomainService.BuscarGruposCurricularesTreeCurriculoCursoOferta(seq).ToList();

                // Soma os valores dos grupos e componenetes
                CurriculoCursoOfertaGrupoValorVO quantidadeTotal = this.CurriculoCursoOfertaGrupoDomainService
                    .SomarQuantidadesCurriculoCursoOfertaGrupo(gruposCurricularesComponentes);

                if (curriculoCursoOferta.QuantidadeCreditoObrigatorio.HasValue)
                    curriculoCursoOferta.QuantidadeAssociadaCreditoObrigatorio = quantidadeTotal.QuantidadeCreditoObrigatorio ?? 0;
                if (curriculoCursoOferta.QuantidadeHoraAulaObrigatoria.HasValue)
                    curriculoCursoOferta.QuantidadeAssociadaHoraAulaObrigatoria = quantidadeTotal.QuantidadeHoraAulaObrigatoria ?? 0;
                if (curriculoCursoOferta.QuantidadeHoraRelogioObrigatoria.HasValue)
                    curriculoCursoOferta.QuantidadeAssociadaHoraRelogioObrigatoria = quantidadeTotal.QuantidadeHoraRelogioObrigatoria ?? 0;

                if (curriculoCursoOferta.QuantidadeCreditoOptativo.HasValue)
                    curriculoCursoOferta.QuantidadeAssociadaCreditoOptativo = quantidadeTotal.QuantidadeCreditoOptativo ?? 0;
                if (curriculoCursoOferta.QuantidadeHoraAulaOptativa.HasValue)
                    curriculoCursoOferta.QuantidadeAssociadaHoraAulaOptativa = quantidadeTotal.QuantidadeHoraAulaOptativa ?? 0;
                if (curriculoCursoOferta.QuantidadeHoraRelogioOptativa.HasValue)
                    curriculoCursoOferta.QuantidadeAssociadaHoraRelogioOptativa = quantidadeTotal.QuantidadeHoraRelogioOptativa ?? 0;
            }

            return curriculoCursoOferta;
        }

        /// <summary>
        /// Buscar curriculo, curso e curso oferta com o curso relacionado
        /// </summary>
        /// <param name="seq">Sequencial do curriculo curso oferta</param>
        /// <returns>CurriculoCursoOferta com curso relacionado</returns>
        public CurriculoCursoOferta BuscarCurriculoCursoOferta(long seq)
        {
            var curriculoCursoOferta = this.SearchByKey(new SMCSeqSpecification<CurriculoCursoOferta>(seq), IncludesCurriculoCursoOferta.CursoOferta_Curso |
                                                IncludesCurriculoCursoOferta.CursoOferta_CursosOfertaLocalidade_Modalidade |
                                                IncludesCurriculoCursoOferta.CursoOferta_CursosOfertaLocalidade_HierarquiasEntidades_ItemSuperior |
                                                IncludesCurriculoCursoOferta.CursoOferta_CursosOfertaLocalidade_CursoUnidade_HierarquiasEntidades_ItemSuperior);

            return curriculoCursoOferta;
        }

        /// <summary>
        /// Buscar curriculo, curso e curso oferta com o grupo curricular relacionado
        /// </summary>
        /// <param name="seq">Sequencial do curriculo curso oferta</param>
        /// <returns>Curriculo curso oferta com grupo curricular relacionado</returns>
        public CurriculoCursoOferta BuscarCurriculoCursoOfertaGrupo(long seq)
        {
            var curriculoCursoOferta = this.SearchByKey(new SMCSeqSpecification<CurriculoCursoOferta>(seq), IncludesCurriculoCursoOferta.GruposCurriculares);

            return curriculoCursoOferta;
        }

        /// <summary>
        /// Recupera o currículo de um aluno pelo plano de ensino no histórico atual do cilco letivo informado
        /// </summary>
        /// <param name="seqAluno">Sequencial do aluno</param>
        /// <param name="seqCicloLetivo">Sequencial do ciclo letivo</param>
        /// <returns>Currículo configurado no plano de ensino do aluno do histórico ciclo letivo do histórico atual</returns>
        public CurriculoCursoOferta BuscarCurriculoCursoOfertaPorAluno(long seqAluno, long seqCicloLetivo)
        {
            var specAluno = new SMCSeqSpecification<Aluno>(seqAluno);
            return AlunoDomainService.SearchProjectionByKey(specAluno, p => p
                        .Historicos.FirstOrDefault(w => w.Atual && !w.DataExclusao.HasValue)
                        .HistoricosCicloLetivo.FirstOrDefault(w => w.SeqCicloLetivo == seqCicloLetivo)
                        .PlanosEstudo.FirstOrDefault(w => w.Atual)
                        .MatrizCurricularOferta.MatrizCurricular
                        .CurriculoCursoOferta);
        }
    }
}