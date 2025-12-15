using SMC.Academico.ServiceContract.Areas.CSO.Data;
using SMC.Framework.Service;

namespace SMC.Academico.ServiceContract.Areas.CSO.Interfaces
{
    public interface IProgramaPropostaService : ISMCService
    {
        /// <summary>
        /// Recupera a configuração de um formulário de proposta de programa
        /// </summary>
        /// <returns>Dados da configuração do formuláro de proposta</returns>
        ProgramaPropostaData BuscarConfiguracoesFormularioProposta();

        /// <summary>
        /// Salva uma proposta para um programa
        /// </summary>
        long SalvarProgramaProposta(ProgramaPropostaData model);

        /// <summary>
        /// Busca uma proposta de programa para edição
        /// </summary>
        /// <param name="seq">Sequencial da Proposta do Programa</param>
        /// <returns>Proposta do programa</returns>
        ProgramaPropostaData BuscarProgramaProposta(long seq);
    }
}