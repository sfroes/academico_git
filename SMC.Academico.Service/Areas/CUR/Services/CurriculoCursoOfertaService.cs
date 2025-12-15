using SMC.Academico.Domain.Areas.CUR.DomainServices;
using SMC.Academico.ServiceContract.Areas.CUR.Data;
using SMC.Academico.ServiceContract.Areas.CUR.Interfaces;
using SMC.Framework.Extensions;
using SMC.Framework.Service;

namespace SMC.Academico.Service.Areas.CUR.Services
{
    public class CurriculoCursoOfertaService : SMCServiceBase, ICurriculoCursoOfertaService
    {
        #region [ DomainService ]

        private CurriculoCursoOfertaDomainService CurriculoCursoOfertaDomainService
        {
            get { return this.Create<CurriculoCursoOfertaDomainService>(); }
        }

        #endregion [ DomainService ]

        /// <summary>
        /// Buscar curriculo, curso e curso oferta para o cabeçalho
        /// </summary>
        /// <param name="seq">Sequencial do curriculo curso oferta</param>
        /// <returns>CurriculoCursoOfertaVO com o cabeçalho</returns>
        public CurriculoCursoOfertaData BuscarCurriculoCursoOfertaCabecalho(long seq, bool total)
        {
            var curriculoCursoOferta = CurriculoCursoOfertaDomainService.BuscarCurriculoCursoOfertaCabecalho(seq, total);
            return curriculoCursoOferta.Transform<CurriculoCursoOfertaData>();
        }

        public CurriculoCursoOfertaData BuscarCurriculoCursoOfertaPorAluno(long seqAluno, long seqCicloLetivo)
        {
            return this.CurriculoCursoOfertaDomainService.BuscarCurriculoCursoOfertaPorAluno(seqAluno, seqCicloLetivo).Transform<CurriculoCursoOfertaData>();
        }
    }
}