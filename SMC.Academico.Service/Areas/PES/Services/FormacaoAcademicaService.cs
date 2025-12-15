using SMC.Academico.Domain.Areas.PES.DomainServices;
using SMC.Academico.Domain.Areas.PES.ValueObjects;
using SMC.Academico.ServiceContract.Areas.PES.Data;
using SMC.Academico.ServiceContract.Areas.PES.Interfaces;
using SMC.Framework.Extensions;
using SMC.Framework.Service;

namespace SMC.Academico.Service.Areas.PES.Services
{
    public class FormacaoAcademicaService : SMCServiceBase, IFormacaoAcademicaService
    {
        #region DomainService

        private FormacaoAcademicaDomainService FormacaoAcademicaDomainService
        {
            get { return this.Create<FormacaoAcademicaDomainService>(); }
        }

        #endregion DomainService


        /// <summary>
        /// Busca os dados de cabeçalho de uma pessoa atuação
        /// </summary>
        /// <param name="seq"></param>
        /// <returns></returns>
        public FormacaoAcademicaCabecalhoData BuscarFormacaoAcademicaCabecalho(long seq)
        {
            return FormacaoAcademicaDomainService.BuscarFormacaoAcademicaCabecalho(seq).Transform<FormacaoAcademicaCabecalhoData>();
        }

        public FormacaoAcademicaInsertedData BuscarFormacaoAcademicaInserted(FormacaoAcademicaInsertedData model)
        {
            return FormacaoAcademicaDomainService.BuscarFormacaoAcademicaInserted(model.Transform<FormacaoAcademicaVO>()).Transform<FormacaoAcademicaInsertedData>();

        }

        public FormacaoAcademicaData BuscarFormacaoAcademica(long seq)
        {
            return FormacaoAcademicaDomainService.BuscarFormacaoAcademica(seq).Transform<FormacaoAcademicaData>();

        }

        public long SalvarFormacaoAcademica(FormacaoAcademicaData formacao)
        {
            return FormacaoAcademicaDomainService.SalvarFormacacaoAcademica(formacao.Transform<FormacaoAcademicaVO>());
        }

        public void ExcluirFormacaoAcademica(FormacaoAcademicaData formacao)
        {
            FormacaoAcademicaDomainService.ExcluirFormacaoAcademica(formacao.Transform<FormacaoAcademicaVO>());
        }

        public bool ValidarTitulacaoMaxima(long seqPessoaAtuacao, bool? titulacaoMaxima, long seq)
        {
            return FormacaoAcademicaDomainService.ValidarTitulacaoMaxima(seqPessoaAtuacao, titulacaoMaxima, seq);
        }
    }
}