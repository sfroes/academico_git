using SMC.Academico.Domain.Areas.PES.DomainServices;
using SMC.Academico.Domain.Areas.PES.Specifications;
using SMC.Academico.Domain.Areas.PES.ValueObjects;
using SMC.Academico.ServiceContract.Areas.PES.Data;
using SMC.Academico.ServiceContract.Areas.PES.Interfaces;
using SMC.Framework.Extensions;
using SMC.Framework.Model;
using SMC.Framework.Service;

namespace SMC.Academico.Service.Areas.PES.Services
{
    public class ReferenciaFamiliarService : SMCServiceBase, IReferenciaFamiliarService
    {
        #region [ DomainService ]

        private ReferenciaFamiliarDomainService ReferenciaFamiliarDomainService
        {
            get { return this.Create<ReferenciaFamiliarDomainService>(); }
        }

        #endregion

        /// <summary>
        /// Buscar uma referência familiar
        /// </summary>
        /// <param name="seq">Sequencial de referência familiar</param>
        /// <returns>registro de referência familiare</returns>
        public ReferenciaFamiliarData BuscarReferenciaFamiliar(long seq)
        {
            var registro = ReferenciaFamiliarDomainService.BuscarReferenciaFamiliar(seq);
            return registro.Transform<ReferenciaFamiliarData>();
        }

        /// <summary>
        /// Buscar uma lista referências familiares da pessoa atuação
        /// </summary>
        /// <param name="filtros">Filtros para pesquisa</param>
        /// <returns>Lista de referências familiares</returns>
        public SMCPagerData<ReferenciaFamiliarData> BuscarReferenciasFamiliares(ReferenciaFamiliarFiltroData filtros)
        {
            var registro = ReferenciaFamiliarDomainService.BuscarReferenciasFamiliares(filtros.Transform<ReferenciaFamiliarFilterSpecification>());
            return registro.Transform<SMCPagerData<ReferenciaFamiliarData>>();
        }

        /// <summary>
        /// Grava uma referência familiar aplicando suas validações e preenchendo o campo EnderecoEletronico com o e-mail informado
        /// </summary>
        /// <param name="referenciaFamiliar">Referência familiar a ser gravada</param>
        /// <returns>Sequencial da referência familiar gravado</returns>
        public long SalvarReferenciaFamiliar(ReferenciaFamiliarData referenciaFamiliar)
        {
            return ReferenciaFamiliarDomainService.SalvarReferenciaFamiliar(referenciaFamiliar.Transform<ReferenciaFamiliarVO>());
        }

        /// <summary>
        /// Exclui um registro de referência familiar se tiver alguma outra cadastrada para mesmo pessoa atuação,
        /// </summary>
        /// <param name="seq">Sequencial da referência familiar</param>
        public void ExcluirReferenciaFamiliar(long seq)
        {
            ReferenciaFamiliarDomainService.ExcluirReferenciaFamiliar(seq);
        }
    }
}

