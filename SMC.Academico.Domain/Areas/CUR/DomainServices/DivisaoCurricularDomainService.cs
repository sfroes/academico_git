using SMC.Academico.Common.Areas.CUR.Includes;
using SMC.Academico.Domain.Areas.CUR.Models;
using SMC.Academico.Domain.Areas.CUR.Specifications;
using SMC.Academico.Domain.Areas.CUR.Validators;
using SMC.Academico.Domain.Areas.CUR.ValueObjects;
using SMC.Academico.Domain.Areas.ORG.DomainServices;
using SMC.Framework.Domain;
using SMC.Framework.Extensions;
using SMC.Framework.Model;
using SMC.Framework.Specification;
using System.Collections.Generic;
using System.Linq;

namespace SMC.Academico.Domain.Areas.CUR.DomainServices
{
    public class DivisaoCurricularDomainService : AcademicoContextDomain<DivisaoCurricular>
    {
        #region [ DomainService ] 
        
        private CurriculoCursoOfertaDomainService CurriculoCursoOfertaDomainService
        {
            get { return this.Create<CurriculoCursoOfertaDomainService>(); }
        }

        private InstituicaoNivelDomainService InstituicaoNivelDomainService
        {
            get { return this.Create<InstituicaoNivelDomainService>(); }
        }

        #endregion

        /// <summary>
        /// Buscar a divisão curricular e o nivel de ensino
        /// </summary>
        /// <param name="seq"></param>        
        /// <returns>Objeto divisão curricular</returns>
        public DivisaoCurricularVO BuscarDivisaoCurricular(long seq)
        {
            DivisaoCurricularVO divisaoCurricularVO = this.SearchProjectionByKey(new SMCSeqSpecification<DivisaoCurricular>(seq),
                                                                            p => new DivisaoCurricularVO() {
                                                                                Seq = p.Seq,
                                                                                SeqInstituicaoEnsino = p.SeqInstituicaoEnsino,
                                                                                Descricao = p.Descricao,
                                                                                SeqNivelEnsino = p.SeqNivelEnsino,
                                                                                SeqTipoDivisaoCurricular = p.SeqTipoDivisaoCurricular,
                                                                                DescricaoTipoDivisaoCurricular = p.TipoDivisaoCurricular.Descricao,
                                                                                SeqRegimeLetivo = p.SeqRegimeLetivo,
                                                                                DescricaoRegimeLetivo = p.RegimeLetivo.Descricao,
                                                                                DescricaoNivelEnsino = p.NivelEnsino.Descricao,
                                                                                Itens = p.Itens.Select(s => new DivisaoCurricularItemVO() {
                                                                                                       Seq = s.Seq,
                                                                                                       Descricao = s.Descricao,
                                                                                                       Numero = s.Numero,
                                                                                                        }).ToList()
                                                                            });  

            return divisaoCurricularVO;
        }
             
        /// <summary>
        /// Buscar lista de divisões curriculares cadastradas para o nível de ensino do curso 
        /// </summary>
        /// <param name="seqCurso">Sequencia do curso</param>
        /// <returns>Lista de divisões curriculares do mesmo nível de ensino</returns>
        public List<SMCDatasourceItem> BuscarDivisoesCurricularesPorCurriculoCursoOferta(long seqCurriculoCursoOferta)
        {
            var spec = new SMCSeqSpecification<CurriculoCursoOferta>(seqCurriculoCursoOferta);
            var filtro = CurriculoCursoOfertaDomainService.SearchProjectionByKey(spec, x => new DivisaoCurricularFilterSpecification()
            {
                SeqInstituicaoEnsino = x.CursoOferta.Curso.SeqInstituicaoEnsino,
                SeqNivelEnsino = x.CursoOferta.Curso.SeqNivelEnsino
            });

            var listaDivisao = this.SearchProjectionBySpecification(filtro, s => new SMCDatasourceItem() { Seq = s.Seq, Descricao = s.Descricao }).ToList();
            return listaDivisao;
        }

        /// <summary>
        /// Salvar uma divisão curricular
        /// </summary>
        /// <param name="divisaoCurricularVO"></param>
        /// <returns>Sequencial da Divisão Curricular</returns>
        public long SalvarDivisaoCurricular(DivisaoCurricularVO divisaoCurricularVO)
        {   
            var divisaoCurricular = divisaoCurricularVO.Transform<DivisaoCurricular>();

            base.SaveEntity(divisaoCurricular, new DivisaoCurricularValidator());

            return divisaoCurricular.Seq;
        }
    }
}
