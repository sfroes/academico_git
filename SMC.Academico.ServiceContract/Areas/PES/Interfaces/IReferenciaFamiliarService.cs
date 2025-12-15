using SMC.Academico.ServiceContract.Areas.PES.Data;
using SMC.Framework.Model;
using SMC.Framework.Service;
using System.Collections.Generic;

namespace SMC.Academico.ServiceContract.Areas.PES.Interfaces
{
    public interface IReferenciaFamiliarService : ISMCService
    {
        /// <summary>
        /// Buscar uma referência familiar
        /// </summary>
        /// <param name="seq">Sequencial de referência familiar</param>
        /// <returns>registro de referência familiare</returns>
        ReferenciaFamiliarData BuscarReferenciaFamiliar(long seq);

        /// <summary>
        /// Buscar uma lista referências familiares da pessoa atuação
        /// </summary>
        /// <param name="filtros">Filtros para pesquisa</param>
        /// <returns>Lista de referências familiares</returns>
        SMCPagerData<ReferenciaFamiliarData> BuscarReferenciasFamiliares(ReferenciaFamiliarFiltroData filtros);

        /// <summary>
        /// Grava uma referência familiar aplicando suas validações e preenchendo o campo EnderecoEletronico com o e-mail informado
        /// </summary>
        /// <param name="referenciaFamiliar">Referência familiar a ser gravada</param>
        /// <returns>Sequencial da referência familiar gravado</returns>
        long SalvarReferenciaFamiliar(ReferenciaFamiliarData referenciaFamiliar);

        /// <summary>
        /// Exclui um registro de referência familiar se tiver alguma outra cadastrada para mesmo pessoa atuação,
        /// </summary>
        /// <param name="seq">Sequencial da referência familiar</param>
        void ExcluirReferenciaFamiliar(long seq);
    }
}
