using SMC.Academico.Common.Areas.ORT.Enums;
using SMC.Academico.Common.Constants;
using SMC.Academico.ServiceContract.Areas.ORT.Data;
using SMC.Framework.Model;
using SMC.Framework.Service;
using System.Collections.Generic;
using System.ServiceModel;

namespace SMC.Academico.ServiceContract.Areas.ORT.Interfaces
{
    [ServiceContract(Namespace = NAMESPACES.SERVICE)]
    public interface IPublicacaoBdpService : ISMCService
    {
        //void AlterarSituacao(long seqPublicacaoBdp, SituacaoTrabalhoAcademico situacao);

        void RetornarSituacaoAlunoBdp(long seqPublicacaoBdp);

        void LiberarConferenciaBiblioteca(long seqPublicacaoBdp);

        void LiberarConsulta(long seqPublicacaoBdp);

        /// <summary>
        /// Buscar publicações bdps do aluno
        /// </summary>
        /// <param name="seqAluno">Sequencial do Aluno</param>
        /// <returns>Lista das publicações bdps do aluno</returns>
        List<PublicacaoBdpData> BuscarPublicacoesBdpsAluno(long seqAluno);

        /// <summary>
        /// Buscar publicação Bdp
        /// </summary>
        /// <param name="seq">Sequencial do publicação bdp</param>
        /// <returns>Publicação Bdp/returns>
        PublicacaoBdpData BuscarPubicacaoBdp(long seq);

        /// <summary>
        /// Editar publicacao bdp
        /// </summary>
        /// <param name="model">Dados a serem salvos</param>
        /// <returns>Sequencial da publicação BDP</returns>
        long SalvarPublicacaoBdp(PublicacaoBdpData model);

        /// <summary>
        /// Autoriza a publicação BDP
        /// </summary>
        /// <param name="model"></param>
        void AutorizarPublicacaoBdp(PublicacaoBdpData model);

        /// <summary>
        /// Dados para exibir autorização da publicação bdp
        /// </summary>
        /// <param name="seq">Sequencial da publicação bdp</param>
        /// <returns>Dados do aluno</returns>
        PublicacaoBdpAutorizacaoData DadosAutorizacaoPublicacaoBdp(long seq);

        bool ValidarLiberacaoConferenciaBiblioteca(long seqPublicacaoBdp);

        /// <summary>
        /// Retorna a lista de tipos de autorização para o aluno baseado na configuração de programa
        /// </summary>
        /// <param name="seqAluno">Seq do aluno para consulta</param>
        /// <param name="diasAutorizacaoParcial">Dias de autorizacao parcial para filtrar</param>
        /// <returns></returns>
        List<SMCDatasourceItem> BuscarTiposAutorizacao(long seqAluno, short? diasAutorizacaoParcial);

        /// <summary>
        /// Busca os dados relativos a publicação informada
        /// </summary>
        /// <param name="seqPublicacaoBdp">Seq da publicação</param>
        /// <returns>Retorna dados da ficha catalográfica</returns>
        FichaCatalograficaData BuscarDadosImpressaoFichaCatalografica(long seqPublicacaoBdp);

        void NotificarBibliotecaTrabalhoComMudanca(MudancaTipoTrabalhoAcademicoSATData filtro);
    }
}