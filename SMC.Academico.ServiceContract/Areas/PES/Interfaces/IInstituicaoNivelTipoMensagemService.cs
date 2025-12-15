using SMC.Academico.ServiceContract.Areas.PES.Data;
using SMC.Framework.Model;
using SMC.Framework.Service;
using System.Collections.Generic;

namespace SMC.Academico.ServiceContract.Areas.PES.Interfaces
{
    public interface IInstituicaoNivelTipoMensagemService : ISMCService
    {
        /// <summary>
        /// Busca um objeto de InstituicaoNivelTipoMensagem.
        /// </summary>
        /// <param name="seq">Sequencial do registro a ser recuperado do banco de dados.</param>
        /// <returns>Objeto referente ao registro no banco de dados.</returns>
        InstituicaoNivelTipoMensagemData BuscarInstituicaoNivelTipoMensagem(long seq);

        /// <summary>
        /// Mensagem padrão de um InstituicaoNivelTipoMensagem.
        /// </summary>
        /// <param name="seq">Sequencial do registro no banco de dados.</param>
        /// <returns>Apensa o campo 'mensagem padrão' do objeto em questão.</returns>
        string BuscarMensagem(long seq);

        /// <summary>
        /// Listar, para seleção, os tipos de mensagens associados à instituição e nível de ensino da pessoa em questão
        /// e que possuem o mesmo tipo de atuação da pesssoa em questão.
        /// </summary>
        /// <param name="seqPessoaAtuacao">Sequencial da Pessoa Atuação para consultar o Tipo de Atuação.</param>
        /// <param name="apenasCadastroManual">Considerar apenas tipos considerados para cadastro manual ou não</param>
        /// <returns>Lista de Tipos de Mensagens.</returns>
        List<SMCDatasourceItem> BuscarTiposMensagemSelect(long seqPessoaAtuacao, bool apenasCadastroManual);

        /// <summary>
        /// Lista os InstituicaoNivelTipoMensagem pelo Seq de um Tipo de Mensagem.
        /// </summary>
        /// <param name="seqTipoMensagem">Sequencial da entidade TipoMensagem</param>
        /// <param name="permiteCadastroManual">Flag para informar se retorna ou não apenas tipos que mensagens que permitem cadastro manual.</param>
        /// <returns>Lista de InstituicaoNivelTipoMensagem</returns>
        List<InstituicaoNivelTipoMensagemData> BuscarInstituicaoNivelTipoMensagens(InstituicaoNivelTipoMensagemFiltroData filtro);

        InstituicaoNivelTipoMensagemData BuscarInstituicaoNivelTipoMensagem(InstituicaoNivelTipoMensagemFiltroData filtro);

        InstituicaoNivelTipoMensagemData BuscarInstituicaoNivelTipoMensagemSemRefinar(InstituicaoNivelTipoMensagemFiltroData filtro);
    }
}