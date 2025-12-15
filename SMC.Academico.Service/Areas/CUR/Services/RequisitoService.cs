using SMC.Academico.Common.Areas.CUR.Enums;
using SMC.Academico.Domain.Areas.CUR.DomainServices;
using SMC.Academico.Domain.Areas.CUR.Specifications;
using SMC.Academico.Domain.Areas.CUR.ValueObjects;
using SMC.Academico.ServiceContract.Areas.CUR.Data;
using SMC.Academico.ServiceContract.Areas.CUR.Interfaces;
using SMC.Framework.Extensions;
using SMC.Framework.Model;
using SMC.Framework.Service;
using System.Collections.Generic;

namespace SMC.Academico.Service.Areas.CUR.Services
{
    public class RequisitoService : SMCServiceBase, IRequisitoService
	{
		#region [ DomainService ]

		private RequisitoDomainService RequisitoDomainService
		{
			get { return this.Create<RequisitoDomainService>(); }
		}

		#endregion [ DomainService ]

		/// <summary>
		/// Busca as configurações de requisito de acordo com matriz curricular
		/// </summary>
		/// <param name="filtros">Filtros da listagem de requisitos</param>
		/// <returns>Dados da configuração do requisito</returns>
		public RequisitoData BuscarConfiguracoesRequisito(RequisitoFiltroData filtros)
		{
			return new RequisitoData() { SeqDivisaoCurricularItem = filtros.SeqDivisaoCurricularItem, SeqComponenteCurricular = filtros.SeqComponenteCurricular };
		}

		/// <summary>
		/// Buscar o requisito
		/// </summary>
		/// <param name="seq"></param>
		/// <returns>Objeto requisito com seus itens</returns>
		public RequisitoData BuscarRequisito(long seq)
		{
			return this.RequisitoDomainService.BuscarRequisito(seq).Transform<RequisitoData>();
		}

		/// <summary>
		/// Busca os requisitos de acordo com os filtros
		/// </summary>
		/// <param name="filtros">Objeto requisito filtro</param>
		/// <returns>SMCPagerData com a lista de requisitos</returns>
		public SMCPagerData<RequisitoListaData> BuscarRequisitos(RequisitoFiltroData filtros)
		{
			var requisitos = this.RequisitoDomainService.BuscarRequisitos(filtros.Transform<RequisitoFilterSpecification>());

			return requisitos.Transform<SMCPagerData<RequisitoListaData>>();
		}

		/// <summary>
		/// Associar um requisito a uma matriz curricular
		/// </summary>
		/// <param name="seq">Sequencial do requisito</param>
		/// <param name="seqMatrizCurricular">Sequencial da matriz curricular</param>
		public void AssociarRequisito(long seq, long seqMatrizCurricular)
		{
			this.RequisitoDomainService.AssociarRequisito(seq, seqMatrizCurricular);
		}

		/// <summary>
		/// Desassociar um requisito a uma matriz curricular
		/// </summary>
		/// <param name="seq">Sequencial do requisito</param>
		/// <param name="seqMatrizCurricular">Sequencial da matriz curricular</param>
		/// <param name="excluirRequisito">Excluir requisito após desassociar a matriz</param>
		public void DesassociarRequisito(long seq, long seqMatrizCurricular, bool excluirRequisito)
		{
			this.RequisitoDomainService.DesassociarRequisito(seq, seqMatrizCurricular, excluirRequisito);
		}

		/// <summary>
		/// Grava um requisito com seus respectivos itens
		/// </summary>
		/// <param name="requisito">Dados do requisito a ser gravado</param>
		/// <returns>Sequencial do requisito gravado</returns>
		public long SalvarRequisito(RequisitoData requisito)
		{
			return this.RequisitoDomainService.SalvarRequisito(requisito.Transform<RequisitoVO>());
		}

		/// <summary>
		/// Excluir um requisito com seus respectivos itens
		/// </summary>
		/// <param name="requisito">Objeto com sequencial do requisito e da matriz curricular</param>
		public void ExcluirRequisito(RequisitoData requisito)
		{
			this.RequisitoDomainService.ExcluirRequisito(requisito.Seq, requisito.SeqMatrizCurricular);
		}

		/// <summary>
		/// Verificar se o(s) componente(s) selecionados possui(em) pré-Requisito(s) atendidos(s) de acordo com a matriz curricular do aluno em questão
		/// </summary>
		/// <param name="seqAluno">Sequencial do aluno</param>
		/// <param name="seqsDivisoesComponente">Sequencial das divisões a serem verificadas</param>
		/// <param name="seqsConfiguracaoComponente">Sequencial das configurações a serem verificadas</param>
		/// <param name="validaTipoGestao">Se é para validar o tipo de gestão e qual o tipo </param>
		/// <param name="seqSolicitacao">Para validar o có-requisito informar o seqSolicitacao</param>
		public (bool Valido, List<string> MensagensErro) ValidarPreRequisitos(long seqAluno, List<long> seqsDivisoesComponente, List<long> seqsConfiguracaoComponente, TipoGestaoDivisaoComponente? validaTipoGestao, long? seqSolicitacao)
        {
            return RequisitoDomainService.ValidarPreRequisitos(seqAluno, seqsDivisoesComponente, seqsConfiguracaoComponente, validaTipoGestao, seqSolicitacao);
        }
				

		/// <summary>
		/// Buscando os tipos de requisito com base no componente curricular
		/// </summary>
		/// <param name="seqComponenteCurricular">Sequencial do componente curricular</param>
		/// <returns>Lista com os tipos de requisito</returns>
		public List<SMCDatasourceItem> BuscarTiposRequisitoSelect(long? seqComponenteCurricular)
		{
			return RequisitoDomainService.BuscarTiposRequisitoSelect(seqComponenteCurricular);
		}
	}
}