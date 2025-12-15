using SMC.Academico.Common.Areas.ALN.Exceptions;
using SMC.Academico.Common.Areas.CAM.Exceptions;
using SMC.Academico.Common.Areas.PES.Exceptions;
using SMC.Academico.Common.Areas.SRC.Exceptions;
using SMC.Academico.Common.Constants;
using SMC.Academico.ServiceContract.Areas.ALN.Data;
using SMC.Framework.Exceptions;
using SMC.Framework.Model;
using SMC.Framework.Service;
using SMC.Inscricoes.ServiceContract.Areas.INS.Data;
using System.Collections.Generic;
using System.ServiceModel;

namespace SMC.Academico.ServiceContract.Areas.ALN.Interfaces
{
    [ServiceContract(Namespace = NAMESPACES.SERVICE)]
    public interface IIngressanteService : ISMCService
    {
        /// <summary>
        /// Busca um ingressante com suas dependências
        /// </summary>
        /// <param name="seq">Sequencial do ingressante</param>
        /// <returns>Dados do ingressante com suas dependências</returns>
        IngressanteData BuscarIngressante(long seq);

        /// <summary>
        /// Busca o sequencial da solicitação de serviço de matrícula de um ingressante
        /// </summary>
        /// <param name="seqIngressante"></param>
        /// <returns></returns>
        long BuscarSeqSolicitacaoMatricula(long seqIngressante);

        /// <summary>
        /// Busca as atuações de ingressante de uma pessoa com os dados pessoais
        /// </summary>
        /// <param name="filtro">Filtro com o seq da pessoa</param>
        /// <returns>Dados de ingressante da pessoa informada</returns>
        IngressanteData[] BuscarIngressantesPessoa(IngressanteFiltroData filtro);

        List<SMCDatasourceItem> BuscarIngressantesSelect(IngressanteFiltroData filtro);

        SMCPagerData<IngressanteProcessoSeletivoListaData> BuscarIngressantesLista(IngressanteFiltroData filtro);

        IngressanteCabecalhoData BuscarCabecalhoIngressantes(long seqIngressante);

        /// <summary>
        /// Busca os ingressantes com as depêndencias apresentadas na listagem do seu cadastro
        /// </summary>
        /// <param name="filtro">Filtros do ingressante</param>
        /// <returns>Dados paginados de ingressante</returns>
        SMCPagerData<IngressanteListaData> BuscarIngressantes(IngressanteFiltroData filtro);

        /// <summary>
        /// Busca os dados acadêmicos de um ingressante
        /// </summary>
        /// <param name="seq">Sequencial do ingressante</param>
        /// <returns>Dados paginados de ingressante</returns>
        IngressanteListaData BuscarDadosAcademicosIngressante(long seq);

        /// <summary>
        /// Busca o SeqInstituicaoNivelEnsino pelo sequencial do ingressante
        /// </summary>
        /// <param name="seqIngressante"></param>
        /// <returns></returns>
        long BuscarSeqInstituicaoNivelEnsinoPorIngressante(long seqIngressante);

        AssociacaoIngressanteLoteCabecalhoData BuscarCabecalhoAssociacaoIngressanteLote(long seqIngressante);

        AssociacaoFormacaoEspecificaIngressanteData BuscarAssociacaoFormacoesEspecificasIngressante(long seqIngressante);

        long SalvarAssociacaoFormacaoEspecíficaIngressante(long seqInstituicao, AssociacaoFormacaoEspecificaIngressanteData modelo);

        AssociacaoOrientadorIngressanteData BuscarAssociacaoOrientadorIngressante(long seqIngressante);

        long SalvarAssociacaoOrientadorIngressante(AssociacaoOrientadorIngressanteData modelo);

        List<AssociacaoOrientadorIngressanteItemData> BuscarOrientacoesIngressante(AssociacaoOrientadorIngressanteData modelo);

        /// <summary>
        /// Aplica a validação da regra RN_ALN_031  Consistência Vínculo Tipo Termo Intercâmbio
        /// </summary>
        /// <param name="ingressante">Dados do ingressante com nível de ensino, tipo de vínculo e termo de intercâmbio</param>
        /// <returns>Verdaderio caso a regra 31 ocorra</returns>
        bool ConsistenciaVinculoTipoTermoIntercambio(IngressanteData ingressante);

        /// <summary>
        /// Grava o ingressante e suas dependências
        /// </summary>
        /// <param name="ingressante">Dados do ingressante e de suas depêndencias</param>
        /// <returns>Sequencial do ingressante gravado</returns>
        long SalvarIngressante(IngressanteData ingressante);

        /// <summary>
        /// Valida a configuração da atuação de ingressante na instituição e retorna um novo ingressante caso a atuação esteja configurada
        /// </summary>
        /// <returns>Novo ingressante caso esteja configurado</returns>
        /// <exception cref="InstituicaoTipoAtuacaoNaoConfiguradaException">Caso o tipo de atuação não esteja configurado na instituição</exception>
        IngressanteData BuscarConfiguracaoIngressante();

        /// <summary>
        /// Valida se o ingressante têm impedimento segundo a regra RN_ALN_003 - Consistência situação ingressante
        /// </summary>
        /// <param name="seq">Sequencial do ingressante</param>
        /// <returns>Verdadeiro caso a situação do ingressante não permita alterações</returns>
        bool ValidarSituacaoImpeditivaIngressante(long seq);

        /// <summary>
        /// Validação de contatos segundo as regras:
        /// RN_ALN_006 Consistência endereço
        /// RN_ALN_008 Consistência endereço eletrônico
        /// </summary>
        /// <param name="ingressante">Ingressante com os contatos</param>
        /// <exception cref="AtuacaoSemEnderecoException">Caso a regra RN_ALN_006 não seja atendida</exception>
        /// <exception cref="AtuacaoSemEmailException">Caso a regra RN_ALN_008 não seja atendida</exception>
        void ValidarContatosIngressante(IngressanteData ingressante);

        /// <summary>
        /// Valida se se nenhum item do grupo de escalonamento já expirou
        /// </summary>
        /// <param name="seqGrupoEscalonamento">Sequencial do grupo de escalonamento</param>
        /// <exception cref="GrupoEscalonamentoExpiradoException">Caso o grupo de escalonamento tenha um item expirado</exception>
        void ValidarGrupoEscalonamentoIngressante(long seqGrupoEscalonamento);

        /// <summary>
        /// Valida se os termos associados ao ingressante são compatíveis
        /// </summary>
        /// <param name="ingressante">Dados do ingressante</param>
        /// <exception cref="IngressanteTermoIntercambioTipoDiferenteException">Caso os termos tenham mais de um tipo</exception>
        /// <exception cref="IngressanteTermoIntercambioParceriaDiferenteException">Caso os termos tenham mais de uma parceria</exception>
        /// <exception cref="IngressanteTermoIntercambioIntervaloVigenciaException">Caso exista algum intervalo sem vigência entre os termos</exception>
        void ValidarTermosIntercambioIngressante(IngressanteData ingressante);

        /// <summary>
        /// Realiza as validações de acordo com RN_ALN_043 e libera o ingressante para realizar a matrícula
        /// </summary>
        /// <param name="seq">Sequencial do ingressante</param>
        /// <exception cref="LiberacaoIngressaneInvalidaException">Caso o ingressante tenha origem ou situação inválida</exception>
        /// <exception cref="OrientacaoConvocadosException">Caso alguma orientação requerida não seja configurada</exception>
        /// <exception cref="SMCApplicationException">Caso o grupo de escalonamento do ingressante esteja expirado ou uma formação específica requerida não seja associada</exception>
        void LiberarIngressanteMatricula(long seq);

        /// <summary>
        /// Valida as orientações exigidas e participações de orientações obrigatórias segundo o nível de ensino e tipo de vínculo do ingressante
        /// </summary>
        /// <param name="ingressanteData">Ingressante com o nível de ensino, tipo de vínculo e orintações</param>
        /// <exception cref="OrientacoesExigidasPorNivelEnsinoException">Caso algum tipo de orientação exigido não seja informado</exception>
        /// <exception cref="TiposParticipacaoOrientacaoObrigatoriosException">Caso algum tipo de orientação obrigatória não seja informada</exception>
        /// <exception cref="OrigemParticipacaoOrientcaoInvalidaException">Caso alguma participação de orientação esteja associada ao tipo errado de instituição</exception>
        void ValidarOrientacaoIngressante(IngressanteData ingressanteData);

        /// <summary>
        /// Valida a quantidade de vagas por oferta para o ingressante caso o vínculo não exija curso
        /// </summary>
        /// <param name="ingressanteVO">Dados do ingressante</param>
        /// <exception cref="OfertasSemVagasException">Caso alguma das ofertas validadas não tenha vagas</exception>
        void ValidarVagasOfertasDisciplinaIsoladaIngressante(IngressanteData ingressanteData);

        /// <summary>
        /// Exclui um ingressante e suas dependências
        /// </summary>
        /// <param name="seq">Sequencial do ingressante</param>
        void ExcluirIngressante(long seq);

        /// <summary>
        /// Processa um inscrito gerando um novo registro de ingressante e marcando o inscrito como importado no GPI
        /// </summary>
        /// <param name="inscrito">Dados do inscrito</param>
        /// <param name="seqEntidadeInstituicao">Sequencial da instituição</param>
        /// <param name="seqChamada">Sequencial da chamada</param>
        void ProcessarInscrito(PessoaIntegracaoData inscrito, long seqEntidadeInstituicao, long seqChamada);

        /// <summary>
        /// Valida a quantidade de vagas por oferta para o ingressante caso o vínculo não exija curso
        /// </summary>
        /// <param name="ingressanteData">Dados do ingressante</param>
        /// <exception cref="OfertasSemVagasException">Caso alguma das ofertas validadas não tenha vagas</exception>
        void ValidarBloqueioPessoa(IngressanteData ingressanteData);
    }
}