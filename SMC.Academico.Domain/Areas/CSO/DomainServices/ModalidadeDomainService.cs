using SMC.Academico.Domain.Areas.CSO.Models;
using SMC.Academico.Domain.Areas.CSO.Specifications;
using SMC.Academico.Domain.Areas.CUR.DomainServices;
using SMC.Academico.Domain.Areas.CUR.Models;
using SMC.Framework.Domain;
using SMC.Framework.Model;
using SMC.Framework.Specification;
using System.Collections.Generic;
using System.Linq;

namespace SMC.Academico.Domain.Areas.CSO.DomainServices
{
    public class ModalidadeDomainService : AcademicoContextDomain<Modalidade>
    {
        #region [ DomainService ]

        private CurriculoCursoOfertaDomainService CurriculoCursoOfertaDomainService
        {
            get { return this.Create<CurriculoCursoOfertaDomainService>(); }
        }
        
        private CursoOfertaLocalidadeDomainService CursoOfertaLocalidadeDomainService
        {
            get { return this.Create<CursoOfertaLocalidadeDomainService>(); }
        }

        #endregion

        /// <summary>
        /// Busca modalidades para a listagem de acordo com o curriculo curso oferta da tabela curso oferta localidade
        /// </summary>
        /// <param name="seqCurriculoCursoOferta">Sequencial do curriculo curso oferta</param>
        /// <returns>Lista de modalidades</returns>
        public List<SMCDatasourceItem> BuscarModalidadesPorCurriculoCursoOfertaSelect(long seqCurriculoCursoOferta)
        {
            long seqCursoOferta = this.CurriculoCursoOfertaDomainService.SearchProjectionByKey(new SMCSeqSpecification<CurriculoCursoOferta>(seqCurriculoCursoOferta), s => s.SeqCursoOferta);

            var spec = new CursoOfertaLocalidadeFilterSpecification() { SeqCursoOferta = seqCursoOferta };

            var modalidades = this.CursoOfertaLocalidadeDomainService.SearchProjectionBySpecification(spec, s => new SMCDatasourceItem() { Seq = s.Modalidade.Seq, Descricao = s.Modalidade.Descricao },true).ToList();

            return modalidades;
        }

        /// <summary>
        /// Busca modalidades para a listagem de acordo com o curso oferta da tabela curso oferta localidade
        /// </summary>
        /// <param name="seqCursoOferta">Sequencial do curso oferta</param>
        /// <returns>Lista de modalidades</returns>
        public List<SMCDatasourceItem> BuscarModalidadesPoCursoOfertaSelect(long seqCursoOferta)
        {            
            var spec = new CursoOfertaLocalidadeFilterSpecification() { SeqCursoOferta = seqCursoOferta };

            var modalidades = this.CursoOfertaLocalidadeDomainService.SearchProjectionBySpecification(spec, s => new SMCDatasourceItem() { Seq = s.Modalidade.Seq, Descricao = s.Modalidade.Descricao }, true).ToList();

            return modalidades;
        }
    }
}
