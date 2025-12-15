using SMC.Academico.Common.Areas.PES.Includes;
using SMC.Academico.Domain.Areas.PES.DomainServices;
using SMC.Academico.Domain.Areas.PES.Models;
using SMC.Academico.Domain.Areas.PES.Specifications;
using SMC.Academico.ServiceContract.Areas.PES.Data;
using SMC.Academico.ServiceContract.Areas.PES.Interfaces;
using SMC.Framework;
using SMC.Framework.Extensions;
using SMC.Framework.Model;
using SMC.Framework.Service;
using SMC.Framework.Specification;

namespace SMC.Academico.Service.Areas.PES.Services
{
	public class PessoaTelefoneService : SMCServiceBase, IPessoaTelefoneService
	{
		#region [ Services ]

		private PessoaTelefoneDomainService PessoaTelefoneDomainService
		{
			get { return this.Create<PessoaTelefoneDomainService>(); }
		}

		#endregion [ Services ]

		public SMCPagerData<PessoaTelefoneLookupData> BuscarPessoaTelefonesLookup(PessoaTelefoneFiltroLookupData filtro)
		{
			return this.PessoaTelefoneDomainService.BuscarPessoaTelefonesLookup(filtro.Transform<PessoaTelefoneFilterSpecification>()).Transform<SMCPagerData<PessoaTelefoneLookupData>>();
		}

		public PessoaTelefoneData BuscarPessoaTelefone(long seq)
		{
			return this.PessoaTelefoneDomainService
				.SearchByKey(new SMCSeqSpecification<PessoaTelefone>(seq), IncludesPessoaTelefone.Telefone)
				.Transform<PessoaTelefoneData>();
		}

		public long SalvarTelefonePessoa(PessoaTelefoneData telefoneData)
		{
			var telefone = telefoneData.Transform<PessoaTelefone>();

			// Segundo solicitação Task 30558, salvar o telefone sem máscara.
			if (telefone != null && telefone.Telefone != null && telefone.Telefone.Numero != null)
				telefone.Telefone.Numero = telefone.Telefone.Numero.SMCRemoveNonDigits();

			this.PessoaTelefoneDomainService.SaveEntity(telefone);
			return telefone.Seq;
		}
	}
}