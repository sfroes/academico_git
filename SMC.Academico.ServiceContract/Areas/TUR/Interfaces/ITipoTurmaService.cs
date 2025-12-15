using SMC.Academico.ServiceContract.Areas.TUR.Data;
using SMC.Framework.Model;
using SMC.Framework.Service;
using System.Collections.Generic;

namespace SMC.Academico.ServiceContract.Areas.TUR.Interfaces
{
    public interface ITipoTurmaService : ISMCService
    {
        /// <summary>
        /// Busca o tipo de turma pelo sequencial
        /// </summary>
        /// <param name="seq"></param>
        /// <returns>Registro tipo de turma</returns>
        TipoTurmaData BuscarTipoTurma(long seq);

        /// <summary>
        /// Busca o select de tipo de turma
        /// </summary>
        /// <returns>Lista de tipos de turma</returns>
        List<SMCDatasourceItem> BuscarTiposTurmasSelect();

        /// <summary>
        /// Busca o select de tipo de turma curricular de acordo com o seqConfiguracaoComponente para obter o instituição nível
        /// </summary>
        /// <param name="seqConfiguracaoComponenteHidden">Sequencial da configuração do componente selecionado</param>
        /// <returns>Lista de tipos de turma com instituição nível associado</returns>
        List<SMCDatasourceItem> BuscarTiposTurmasPorConfiguracaoComponenteSelect(long seqConfiguracaoComponenteHidden);

        /// <summary>
        /// Verifica se existe associação do tipo de turma com a configuração de componente selecionada como principal
        /// </summary>
        /// <param name="seqTipoTurma"></param>
        /// <param name="seqConfiguracaoComponente"></param>
        /// <returns>Retorna se exite associação</returns>
        bool VerificarTipoTurmaInstituicaoNivel(long seqTipoTurma, long seqConfiguracaoComponente);

        /// <summary>
        /// RN 39 - Se o tipo de turma estiver parametrizado para não permitir associação de oferta de matriz, verirficar se a
        /// configuração principal é de um componente que exige associação de assunto de componete.Caso seja, abortar a
        /// operação e enviar a seguinte mensagem: "Não é possível prosseguir. Para o tipo de turma em questão, é necessário selecionar uma configuração de
        /// componente que seja de um componente que não exige associação de assunto de componente."
        /// </summary>
        /// <param name="seqTipoTurma"></param>
        /// <param name="seqConfiguracaoComponente"></param>
        /// <returns>Retorna se permite este tipo de turma com a configuração principal</returns>
        bool VerificarTipoTurmaConfiguracaoAssunto(long seqTipoTurma, long seqConfiguracaoComponente);
        
        List<TipoTurmaData> BuscarTiposTurmasPorComponenteCurricular(long seqComponenteCurricular, List<long> seqsMatrizesCurricularesOferta);
    }
}
