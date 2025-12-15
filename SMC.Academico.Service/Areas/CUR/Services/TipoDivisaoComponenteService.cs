using SMC.Academico.Common.Areas.CUR.Enums;
using SMC.Academico.Domain.Areas.CUR.DomainServices;
using SMC.Academico.Domain.Areas.CUR.Models;
using SMC.Academico.Domain.Areas.CUR.Specifications;
using SMC.Academico.ServiceContract.Areas.CUR.Data;
using SMC.Academico.ServiceContract.Areas.CUR.Interfaces;
using SMC.Framework.Extensions;
using SMC.Framework.Model;
using SMC.Framework.Service;
using SMC.Framework.Specification;
using System.Collections.Generic;

namespace SMC.Academico.Service.Areas.CUR.Services
{
    public class TipoDivisaoComponenteService : SMCServiceBase, ITipoDivisaoComponenteService
    {
        #region [ DomainService ]

        private TipoDivisaoComponenteDomainService TipoDivisaoComponenteDomainService
        {
            get { return this.Create<TipoDivisaoComponenteDomainService>(); }
        }

        #endregion [ DomainService ]

        /// <summary>
        /// Buscar o Tipo Divisão Componente
        /// </summary>
        /// <param name="seqTipoDivisaoComponente">Sequencial do Tipo Divisão Componente</param>
        /// <returns>Tipo Divisão Componente</returns>
        public TipoDivisaoComponenteData BuscarTipoDivisaoComponente(long seqTipoDivisaoComponente)
        {
            var spec = new SMCSeqSpecification<TipoDivisaoComponente>(seqTipoDivisaoComponente);
            return this.TipoDivisaoComponenteDomainService.SearchByKey(spec).Transform<TipoDivisaoComponenteData>();
        }

        /// <summary>
        /// Busca os tipos de divisão de componente de um tipo de componente
        /// </summary>
        /// <param name="seqTipoComponenteCurricular">Sequencial do tipo de componente</param>
        /// <returns>Lista de tipos de divisão para select</returns>
        public List<SMCDatasourceItem> BuscarTipoDivisaoComponenteSelect(long seqTipoComponenteCurricular)
        {
            return this.TipoDivisaoComponenteDomainService.BuscarTipoDivisaoComponenteSelect(seqTipoComponenteCurricular);
        }

        /// <summary>
        /// Busca os tipo divisão componente de acordo com o componente curricular
        /// </summary>
        /// <param name="seqComponenteCurricular">Sequencia do Componente Curricular selecionado</param>
        /// <returns>Lista de tipos de divisão do componente</returns>
        public List<SMCDatasourceItem> BuscarTipoDivisaoComponentePorComponenteSelect(long seqComponenteCurricular)
        {
            return this.TipoDivisaoComponenteDomainService.BuscarTipoDivisaoComponentePorComponenteSelect(seqComponenteCurricular);
        }

        /// <summary>
        /// Busca o tipo divisão componente por divisão de componente
        /// </summary>
        /// <param name="seqDivisaoComponente">Sequencia do divisão componente</param>
        /// <returns>Dados tipo divisão componente</returns>
        public TipoDivisaoComponenteData BuscarTipoDivisaoComponentePorDivisaoComponente(long seqDivisaoComponente)
        {
            return this.TipoDivisaoComponenteDomainService.BuscarTipoDivisaoComponentePorDivisaoComponente(seqDivisaoComponente).Transform<TipoDivisaoComponenteData>();
        }

        /// <summary>
        /// Busca os tipo componente curricular de acordo com o parâmetro de tipos gestão divisão componente
        /// </summary>
        /// <param name="tiposGestaoDivisaoComponente">Tipos de gestão divisão componente informados como parâmetro</param>
        /// <returns>Lista de sequenciais tipos componente curricular</returns>
        public List<long> BuscarTipoComponenteCurricularPorTipoGestaoDivisaoComponente(TipoGestaoDivisaoComponente[] tiposGestaoDivisaoComponente)
        {
            return TipoDivisaoComponenteDomainService.BuscarTipoComponenteCurricularPorTipoGestaoDivisaoComponente(tiposGestaoDivisaoComponente);
        }

        public List<SMCDatasourceItem> BuscarTiposDivisaoComponenteAlunoComGestao(long seqAluno, TipoGestaoDivisaoComponente tipoGestao)
        {
            return TipoDivisaoComponenteDomainService.BuscarTiposDivisaoComponenteAlunoComGestao(seqAluno, tipoGestao);
        }
    }
}