 
using System;

namespace SMC.Academico.Common.Areas.SRC.Constants
{
    /// <summary>
	/// Contém a lista de funcionalidades para o caso de uso UC_SRC_001_01_01.
	/// </summary>
    public static class UC_SRC_001_01_01
    {

        /// <summary>
	    /// Esse caso de uso tem como objetivo a pesquisa de serviços, permitindo também a exclusão de um serviço.
	    /// </summary>
        public const string PESQUISAR_SERVICO = "UC_SRC_001_01_01.Pesquisar_Servico";

    }

    /// <summary>
	/// Contém a lista de funcionalidades para o caso de uso UC_SRC_001_01_02.
	/// </summary>
    public static class UC_SRC_001_01_02
    {

        /// <summary>
	    /// Este caso de uso tem como objetivo a manutenção (inclusão/alteração) de serviços
	    /// </summary>
        public const string MANTER_SERVICO = "UC_SRC_001_01_02.Manter_Servico";

    }

    /// <summary>
	/// Contém a lista de funcionalidades para o caso de uso UC_SRC_001_02_01.
	/// </summary>
    public static class UC_SRC_001_02_01
    {

        /// <summary>
	    /// Este caso de uso tem como objetivo manter o cadastro de tipos de serviço, permitindo todas as funcionalidades de um CRUD básico (consulta, cadastro, alteração e exclusão). É um cadastro dynamic.    
	    /// </summary>
        public const string MANTER_TIPO_SERVICO = "UC_SRC_001_02_01.Manter_Tipo_Servico";

    }

    /// <summary>
	/// Contém a lista de funcionalidades para o caso de uso UC_SRC_002_01_01.
	/// </summary>
    public static class UC_SRC_002_01_01
    {

        /// <summary>
	    /// Permite a execução da funcionalidade de encerramento de etapa
	    /// </summary>
        public const string ENCERRAR_ETAPA = "UC_SRC_002_01_01.Encerrar_Etapa";

        /// <summary>
	    /// Permite a execução da funcionalidade de encerramento de processo
	    /// </summary>
        public const string ENCERRAR_PROCESSO = "UC_SRC_002_01_01.Encerrar_Processo";

        /// <summary>
	    /// Permite a manutenção dos processos do respectivo Serviço aos demais usuários
	    /// </summary>
        public const string MANTER_PROCESSO_PERMITIDO_USUARIOS = "UC_SRC_002_01_01.Manter_Processo_Permitido_Usuarios";

        /// <summary>
	    /// Permite a manutenção dos processos do respectivo Serviço apenas a usuários administradores
	    /// </summary>
        public const string MANTER_PROCESSO_RESTRITO_ADMINISTRADOR = "UC_SRC_002_01_01.Manter_Processo_Restrito_Administrador";

        /// <summary>
	    /// Este caso de uso tem como objetivo pesquisar e listar os processos.
	    /// </summary>
        public const string PESQUISAR_PROCESSOS = "UC_SRC_002_01_01.Pesquisar_Processos";

        /// <summary>
	    /// Permite a execução de JOB para preparação da renovação de matrícula
	    /// </summary>
        public const string PREPARAR_RENOVACAO_MATRICULA = "UC_SRC_002_01_01.Preparar_Renovacao_Matricula";

        /// <summary>
	    /// Permite a execução da funcionalidade de reabertura de processo (exclui a data de encerramento)
	    /// </summary>
        public const string REABRIR_PROCESSO = "UC_SRC_002_01_01.Reabrir_Processo";

    }

    /// <summary>
	/// Contém a lista de funcionalidades para o caso de uso UC_SRC_002_01_02.
	/// </summary>
    public static class UC_SRC_002_01_02
    {

        /// <summary>
	    /// Este caso de uso tem como objetivo visualizar os dados de um processo.
	    /// </summary>
        public const string MANTER_PROCESSO = "UC_SRC_002_01_02.Manter_Processo";

    }

    /// <summary>
	/// Contém a lista de funcionalidades para o caso de uso UC_SRC_002_01_03.
	/// </summary>
    public static class UC_SRC_002_01_03
    {

        /// <summary>
	    /// Este caso de uso tem como objetivo alterar os dados de uma determinada etapa.
	    /// </summary>
        public const string MANTER_ETAPA_PROCESSO = "UC_SRC_002_01_03.Manter_Etapa_Processo";

    }

    /// <summary>
	/// Contém a lista de funcionalidades para o caso de uso UC_SRC_002_01_04.
	/// </summary>
    public static class UC_SRC_002_01_04
    {

        /// <summary>
	    /// Este caso de uso tem como objetivo copiar um processo e suas configurações.
	    /// </summary>
        public const string COPIAR_PROCESSO = "UC_SRC_002_01_04.Copiar_Processo";

    }

    /// <summary>
	/// Contém a lista de funcionalidades para o caso de uso UC_SRC_002_02_01.
	/// </summary>
    public static class UC_SRC_002_02_01
    {

        /// <summary>
	    /// Este caso de  uso tem como objetivo a pesquisa de configurações de um processo, permitindo também sua exclusão.
	    /// </summary>
        public const string PESQUISAR_CONFIGURACAO_PROCESSO = "UC_SRC_002_02_01.Pesquisar_Configuracao_Processo";

    }

    /// <summary>
	/// Contém a lista de funcionalidades para o caso de uso UC_SRC_002_02_02.
	/// </summary>
    public static class UC_SRC_002_02_02
    {

        /// <summary>
	    /// Este caso de uso tem como objetivo a manutenção (inclusão/alteração) de configuração de processo.
	    /// </summary>
        public const string MANTER_CONFIGURACAO_PROCESSO = "UC_SRC_002_02_02.Manter_Configuracao_Processo";

    }

    /// <summary>
	/// Contém a lista de funcionalidades para o caso de uso UC_SRC_002_04_01.
	/// </summary>
    public static class UC_SRC_002_04_01
    {

        /// <summary>
	    /// Este caso de uso tem como objetivo a pesquisa de configurações de etapas de um processo, permitindo também sua exclusão.
	    /// </summary>
        public const string PESQUISAR_CONFIGURACAO_ETAPA = "UC_SRC_002_04_01.Pesquisar_Configuracao_Etapa";

    }

    /// <summary>
	/// Contém a lista de funcionalidades para o caso de uso UC_SRC_002_04_02.
	/// </summary>
    public static class UC_SRC_002_04_02
    {

        /// <summary>
	    /// Este caso de uso tem como objetivo a manutenção (inclusão/alteração) de configuração de etapa.
	    /// </summary>
        public const string MANTER_CONFIGURACAO_ETAPA = "UC_SRC_002_04_02.Manter_Configuracao_Etapa";

    }

    /// <summary>
	/// Contém a lista de funcionalidades para o caso de uso UC_SRC_002_04_03.
	/// </summary>
    public static class UC_SRC_002_04_03
    {

        /// <summary>
	    /// Este caso de uso tem como objetivo a configuração do fluxo de páginas de uma etapa.
	    /// </summary>
        public const string CONFIGURAR_ETAPA_FLUXO_PAGINAS = "UC_SRC_002_04_03.Configurar_Etapa_Fluxo_Paginas";

    }

    /// <summary>
	/// Contém a lista de funcionalidades para o caso de uso UC_SRC_002_04_04.
	/// </summary>
    public static class UC_SRC_002_04_04
    {

        /// <summary>
	    /// Este caso de uso tem como objetivo adicionar uma página a um template de processo.
	    /// </summary>
        public const string CONFIGURAR_ETAPA_ASSOCIAR_PAGINA_TEMPLATE = "UC_SRC_002_04_04.Configurar_Etapa_Associar_Pagina_Template";

    }

    /// <summary>
	/// Contém a lista de funcionalidades para o caso de uso UC_SRC_002_04_05.
	/// </summary>
    public static class UC_SRC_002_04_05
    {

        /// <summary>
	    /// Este caso de uso tem como objetivo a configuração de sessões de texto de uma página do fluxo.
	    /// </summary>
        public const string CONFIGURAR_ETAPA_PAGINA_TEXTO = "UC_SRC_002_04_05.Configurar_Etapa_Pagina_Texto";

    }

    /// <summary>
	/// Contém a lista de funcionalidades para o caso de uso UC_SRC_002_04_06.
	/// </summary>
    public static class UC_SRC_002_04_06
    {

        /// <summary>
	    /// Este caso de uso tem como objetivo a configuração dos arquivos de uma sessão de arquivos de  uma página do fluxo.
	    /// </summary>
        public const string CONFIGURAR_ETAPA_PAGINA_ARQUIVO = "UC_SRC_002_04_06.Configurar_Etapa_Pagina_Arquivo";

    }

    /// <summary>
	/// Contém a lista de funcionalidades para o caso de uso UC_SRC_002_04_07.
	/// </summary>
    public static class UC_SRC_002_04_07
    {

        /// <summary>
	    /// Este caso de uso tem como objetivo a configuração de uma página no fluxo de páginas.
	    /// </summary>
        public const string CONFIGURAR_ETAPA_PAGINA = "UC_SRC_002_04_07.Configurar_Etapa_Pagina";

    }

    /// <summary>
	/// Contém a lista de funcionalidades para o caso de uso UC_SRC_002_04_08.
	/// </summary>
    public static class UC_SRC_002_04_08
    {

        /// <summary>
	    /// Este caso de uso tem como objetivo a pesquisa de bloqueios de uma etapa, permitindo também a exclusão de um bloqueio.
	    /// </summary>
        public const string CONFIGURAR_ETAPA_PESQUISAR_BLOQUEIOS = "UC_SRC_002_04_08.Configurar_Etapa_Pesquisar_Bloqueios";

    }

    /// <summary>
	/// Contém a lista de funcionalidades para o caso de uso UC_SRC_002_04_09.
	/// </summary>
    public static class UC_SRC_002_04_09
    {

        /// <summary>
	    /// Este caso de uso tem como objetivo a manutenção (inclusão/alteração) de um bloqueio em uma etapa.
	    /// </summary>
        public const string CONFIGURAR_ETAPA_MANTER_BLOQUEIOS = "UC_SRC_002_04_09.Configurar_Etapa_Manter_Bloqueios";

    }

    /// <summary>
	/// Contém a lista de funcionalidades para o caso de uso UC_SRC_002_04_10.
	/// </summary>
    public static class UC_SRC_002_04_10
    {

        /// <summary>
	    /// Este caso de uso tem como objetivo a pesquisa de documentos requeridos em  uma etapa, permitindo também a exclusão.
	    /// </summary>
        public const string CONFIGURAR_ETAPA_PESQUISAR_DOCUMENTOS = "UC_SRC_002_04_10.Configurar_Etapa_Pesquisar_Documentos";

    }

    /// <summary>
	/// Contém a lista de funcionalidades para o caso de uso UC_SRC_002_04_11.
	/// </summary>
    public static class UC_SRC_002_04_11
    {

        /// <summary>
	    /// Este caso de uso tem como objetivo a manutenção (inclusão/alteração) de documentos requeridos em uma etapa.
	    /// </summary>
        public const string CONFIGURAR_ETAPA_MANTER_DOCUMENTOS = "UC_SRC_002_04_11.Configurar_Etapa_Manter_Documentos";

    }

    /// <summary>
	/// Contém a lista de funcionalidades para o caso de uso UC_SRC_002_04_12.
	/// </summary>
    public static class UC_SRC_002_04_12
    {

        /// <summary>
	    /// Este caso de uso tem como objetivo a pesquisa de grupos de documentos requeridos em uma etapa, permitindo também a exclusão.
	    /// </summary>
        public const string CONFIGURAR_ETAPA_PESQUISAR_GRUPO_DOCUMENTOS = "UC_SRC_002_04_12.Configurar_Etapa_Pesquisar_Grupo_Documentos";

    }

    /// <summary>
	/// Contém a lista de funcionalidades para o caso de uso UC_SRC_002_04_13.
	/// </summary>
    public static class UC_SRC_002_04_13
    {

        /// <summary>
	    /// Este caso de uso tem como objetivo a manutenção (inclusão/alteração) de grupos de documentos requeridos em uma etapa.
	    /// </summary>
        public const string CONFIGURAR_ETAPA_MANTER_GRUPO_DOCUMENTOS = "UC_SRC_002_04_13.Configurar_Etapa_Manter_Grupo_Documentos";

    }

    /// <summary>
	/// Contém a lista de funcionalidades para o caso de uso UC_SRC_002_05_01.
	/// </summary>
    public static class UC_SRC_002_05_01
    {

        /// <summary>
	    /// Este caso de uso tem como objetivo consultar e excluir os escalonamentos cadastrados por etapa.
	    /// </summary>
        public const string PESQUISAR_ESCALONAMENTO_ETAPA = "UC_SRC_002_05_01.Pesquisar_Escalonamento_Etapa";

    }

    /// <summary>
	/// Contém a lista de funcionalidades para o caso de uso UC_SRC_002_05_02.
	/// </summary>
    public static class UC_SRC_002_05_02
    {

        /// <summary>
	    /// Este caso de uso tem como objetivo cadastrar ou alterar escalonamentos de uma determinada etapa.
	    /// </summary>
        public const string MANTER_ESCALONAMENTO_ETAPA = "UC_SRC_002_05_02.Manter_Escalonamento_Etapa";

    }

    /// <summary>
	/// Contém a lista de funcionalidades para o caso de uso UC_SRC_002_06_01.
	/// </summary>
    public static class UC_SRC_002_06_01
    {

        /// <summary>
	    /// Permite acesso ao comando "Novo Grupo".
	    /// </summary>
        public const string CRIAR_NOVO_GRUPO_ESCALONAMENTO = "UC_SRC_002_06_01.Criar_Novo_Grupo_Escalonamento";

        /// <summary>
	    /// Este caso de uso tem como objetivo pesquisar os grupos de escalonamento de um determinado processo, permitindo também a exclusão.  
	    /// </summary>
        public const string PESQUISAR_GRUPO_ESCALONAMENTO_PROCESSO = "UC_SRC_002_06_01.Pesquisar_Grupo_Escalonamento_Processo";

    }

    /// <summary>
	/// Contém a lista de funcionalidades para o caso de uso UC_SRC_002_06_02.
	/// </summary>
    public static class UC_SRC_002_06_02
    {

        /// <summary>
	    /// Este caso de uso tem como objetivo cadastrar ou alterar grupos de escalonamento de um determinado processo.  
	    /// </summary>
        public const string MANTER_GRUPO_ESCALONAMENTO_PROCESSO = "UC_SRC_002_06_02.Manter_Grupo_Escalonamento_Processo";

    }

    /// <summary>
	/// Contém a lista de funcionalidades para o caso de uso UC_SRC_002_06_03.
	/// </summary>
    public static class UC_SRC_002_06_03
    {

        /// <summary>
	    /// Este caso de uso tem como objetivo exibir cadastrar e alterar parcelas de um escalonamento em um grupo de escalonamento.
	    /// </summary>
        public const string CONFIGURAR_PARCELAS = "UC_SRC_002_06_03.Configurar_Parcelas";

    }

    /// <summary>
	/// Contém a lista de funcionalidades para o caso de uso UC_SRC_002_06_04.
	/// </summary>
    public static class UC_SRC_002_06_04
    {

        /// <summary>
	    /// Este caso de uso tem como objetivo copiar um grupo de escalonamento de um processo.
	    /// </summary>
        public const string COPIAR_GRUPO_ESCALONAMENTO = "UC_SRC_002_06_04.Copiar_Grupo_Escalonamento";

    }

    /// <summary>
	/// Contém a lista de funcionalidades para o caso de uso UC_SRC_002_06_05.
	/// </summary>
    public static class UC_SRC_002_06_05
    {

        /// <summary>
	    /// Este caso de uso tem como objetivo copiar um grupo de escalonamento de um processo.
	    /// </summary>
        public const string ASSOCIAR_SOLICITACAO_SERVICO = "UC_SRC_002_06_05.Associar_solicitacao_servico";

    }

    /// <summary>
	/// Contém a lista de funcionalidades para o caso de uso UC_SRC_002_07_01.
	/// </summary>
    public static class UC_SRC_002_07_01
    {

        /// <summary>
	    /// Este caso de uso tem como objetivo a conferencia de configurações de um processo.
	    /// </summary>
        public const string CONSULTAR_CONFIGURACAO_PROCESSO = "UC_SRC_002_07_01.Consultar_Configuracao_Processo";

    }

    /// <summary>
	/// Contém a lista de funcionalidades para o caso de uso UC_SRC_003_01_01.
	/// </summary>
    public static class UC_SRC_003_01_01
    {

        /// <summary>
	    /// Este caso de uso tem como objetivo manter o cadastro de tipos de notificação, permitindo todas as funcionalidades de um CRUD básico (consulta, cadastro, alteração e exclusão).  É um cadastro dynamic.    
	    /// </summary>
        public const string MANTER_TIPO_NOTIFICACAO = "UC_SRC_003_01_01.Manter_Tipo_Notificacao";

    }

    /// <summary>
	/// Contém a lista de funcionalidades para o caso de uso UC_SRC_003_02_01.
	/// </summary>
    public static class UC_SRC_003_02_01
    {

        /// <summary>
	    /// Este caso de uso tem como objetivo pesquisar as configurações de notificação das etapas de um processo, permitindo também a exclusão.
	    /// </summary>
        public const string PESQUISAR_CONFIGURACAO_NOTIFICACAO = "UC_SRC_003_02_01.Pesquisar_Configuracao_Notificacao";

    }

    /// <summary>
	/// Contém a lista de funcionalidades para o caso de uso UC_SRC_003_02_02.
	/// </summary>
    public static class UC_SRC_003_02_02
    {

        /// <summary>
	    /// Este caso de uso tem como objetivo cadastrar ou alterar uma configuração de notificação de uma etapa de processo.
	    /// </summary>
        public const string MANTER_CONFIGURACAO_NOTIFICACAO = "UC_SRC_003_02_02.Manter_Configuracao_Notificacao";

    }

    /// <summary>
	/// Contém a lista de funcionalidades para o caso de uso UC_SRC_003_02_03.
	/// </summary>
    public static class UC_SRC_003_02_03
    {

        /// <summary>
	    /// Este caso de uso tem como objetivo cadastrar ou alterar os parâmetros de envio de notificação.
	    /// </summary>
        public const string CONFIGURAR_ENVIO_NOTIFICACAO = "UC_SRC_003_02_03.Configurar_Envio_Notificacao";

    }

    /// <summary>
	/// Contém a lista de funcionalidades para o caso de uso UC_SRC_004_01_01.
	/// </summary>
    public static class UC_SRC_004_01_01
    {

        /// <summary>
	    /// Permite a realização de atendimentos para alterações de plano de estudo.
	    /// </summary>
        public const string ALTERACAO_PLANO_ESTUDOS = "UC_SRC_004_01_01.Alteracao_Plano_Estudos";

        /// <summary>
	    /// Permite a realização de atendimentos para alterações de plano de estudo, com origem no SGA.Administrativo.
	    /// </summary>
        public const string ALTERACAO_PLANO_ESTUDOS_ADMINISTRATIVO = "UC_SRC_004_01_01.Alteracao_Plano_Estudos_Administrativo";

        /// <summary>
	    /// Permite a realização de atendimento de solicitações de alteração de tipo de publicação no BDP
	    /// </summary>
        public const string ATENDIMENTO_ALTERACAO_PUBLICACAO_BDP = "UC_SRC_004_01_01.Atendimento_Alteracao_Publicacao_BDP";

        /// <summary>
	    /// Permite a realização de atendimento de solicitações de atividade complementar.
	    /// </summary>
        public const string ATENDIMENTO_ATIVIDADE_COMPLEMENTAR = "UC_SRC_004_01_01.Atendimento_Atividade_Complementar";

        /// <summary>
	    /// Permite a realização de atendimento de solicitações de cancelamento de matrícula.
	    /// </summary>
        public const string ATENDIMENTO_CANCELAMENTO_MATRICULA = "UC_SRC_004_01_01.Atendimento_Cancelamento_Matricula";

        /// <summary>
	    /// Permite a realização de atendimento de solicitações de depósito de Dissertecao/Tese
	    /// </summary>
        public const string ATENDIMENTO_DEPOSITO_DISSERTECAO_TESE = "UC_SRC_004_01_01.Atendimento_Deposito_Dissertecao_Tese";

        /// <summary>
	    /// Permite a realização de atendimentos do inclusão de disciplina eletiva
	    /// </summary>
        public const string ATENDIMENTO_DISCIPLINA_ELETIVA = "UC_SRC_004_01_01.Atendimento_Disciplina_Eletiva";

        /// <summary>
	    /// Permite a realização de atendimento de solicitações de dispensa individual.
	    /// </summary>
        public const string ATENDIMENTO_DISPENSA_INDIVIDUAL = "UC_SRC_004_01_01.Atendimento_Dispensa_Individual";

        /// <summary>
	    /// Permite a realização de atendimento de solicitações de mobilidade (intercâmbio).
	    /// </summary>
        public const string ATENDIMENTO_INTERCAMBIO = "UC_SRC_004_01_01.Atendimento_Intercambio";

        /// <summary>
	    /// Permite a realização de atendimento de matrícula (chancela e efetivação)
	    /// </summary>
        public const string ATENDIMENTO_MATRICULA = "UC_SRC_004_01_01.Atendimento_Matricula";

        /// <summary>
	    /// Permite a realização de atendimentos do tipo padrão
	    /// </summary>
        public const string ATENDIMENTO_PADRAO = "UC_SRC_004_01_01.Atendimento_Padrao";

        /// <summary>
	    /// Permite a realização de atendimento de solicitações de emissão de diploma
	    /// </summary>
        public const string ATENDIMENTO_PROCESSO_EMISSAO_DIPLOMA = "UC_SRC_004_01_01.Atendimento_Processo_Emissao_Diploma";

        /// <summary>
	    /// Permite a realização de atendimento de solicitações de emissão de documento acadêmico. Ex: Declaração de matrícula e Histórico escolar
	    /// </summary>
        public const string ATENDIMENTO_PROCESSO_EMISSAO_DOCUMENTO_ACADEMICO = "UC_SRC_004_01_01.Atendimento_Processo_Emissao_Documento_Academico";

        /// <summary>
	    /// Permite a realização de atendimento de solicitações de emissão de histórico escolar digital (xml)
	    /// </summary>
        public const string ATENDIMENTO_PROCESSO_EMISSAO_HISTORICO_ESCOLAR = "UC_SRC_004_01_01.Atendimento_Processo_Emissao_Historico_Escolar";

        /// <summary>
	    /// Permite a realização de atendimento de solicitações de reabertura de matrícula.  
	    /// </summary>
        public const string ATENDIMENTO_PROCESSO_MATRICULA_REABERTURA = "UC_SRC_004_01_01.Atendimento_Processo_Matricula_Reabertura";

        /// <summary>
	    /// Permite a realização de atendimento de solicitações de prorrogação do prazo de conclusão de curso.
	    /// </summary>
        public const string ATENDIMENTO_PRORROGACAO_PRAZO_CONCLUSAO = "UC_SRC_004_01_01.Atendimento_prorrogacao_prazo_conclusao";

        /// <summary>
	    /// Permite a realização de atendimento de solicitações de retificação de falta
	    /// </summary>
        public const string ATENDIMENTO_RETIFICACAO_FALTA = "UC_SRC_004_01_01.Atendimento_Retificacao_Falta";

        /// <summary>
	    /// Permite a realização de atendimento padrão de solicitações de reabertura de matrícula.
	    /// </summary>
        public const string ATENDIMENTO_SOLICITACAO_REABERTURA = "UC_SRC_004_01_01.Atendimento_Solicitacao_Reabertura";

        /// <summary>
	    /// Permite a realização de atendimento de solicitações de trancamento de matrícula.
	    /// </summary>
        public const string ATENDIMENTO_TRANCAMENTO_MATRICULA = "UC_SRC_004_01_01.Atendimento_Trancamento_Matricula";

        /// <summary>
	    /// <font color="#000080"><i>&lt;Preencher o campo "Name" com o nome do token de segurança que está sendo detalhado.&gt;</i>  <font color="#000080"><i>&lt;O nome do token poderá ser um nome livre, significativo, e deverá ter no máximo 255 caracteres.&gt;</i>  <font color="#000080"><i>  </i><font color="#000080"><i>&lt;Preencher o campo "Alias" com o</i><b><i> nome do token de segurança</i></b><i> que está sendo detalhado.&gt;</i>  <font color="#000080"><i>&lt; Seguir o padrão de nomenclatura indicado:</i>  <font color="#000080"><b><i>UC_SGL_XXX_XX_XX.nome_token</i></b>  <font color="#000080"><i>onde:</i>  <font color="#000080"><i>"</i><b><i>UC_SGL_XXX_XX_XX</i></b><i>" identifica o caso de uso ao qual o token se refere e será aplicado.&gt;</i>  <font color="#000080"><i>&lt;O alias do token deverá ser um nome livre, significativo, sem acentuação, sem espaços e sem caracteres especiais, e condizente com o nome do token (campo "Name" definido acima).</i>  <font color="#000080"><i>O nome deve ter no máximo 255 caracteres.&gt;</i>    &lt;Descreva aqui o objetivo do token&gt;
	    /// </summary>
        public const string ENTREGA_DOCUMENTACAO = "UC_SRC_004_01_01.Entrega_Documentacao";

        /// <summary>
	    /// Este caso de uso tem como objetivo centralizar a gestão de todos os tipos de solicitações de Alunos, Ingressantes e Professores/Pesquisadores. Permitindo a pesquisa das solicitações e inicialização do atendimento.  
	    /// </summary>
        public const string PESQUISAR_SOLICITACAO = "UC_SRC_004_01_01.Pesquisar_solicitacao";

    }

    /// <summary>
	/// Contém a lista de funcionalidades para o caso de uso UC_SRC_004_01_02.
	/// </summary>
    public static class UC_SRC_004_01_02
    {

        /// <summary>
	    /// Este caso de uso tem como objetivo consultar todos os dados de uma solicitação.
	    /// </summary>
        public const string CONSULTAR_SOLICITACAO = "UC_SRC_004_01_02.Consultar_Solicitacao";

    }

    /// <summary>
	/// Contém a lista de funcionalidades para o caso de uso UC_SRC_004_01_03.
	/// </summary>
    public static class UC_SRC_004_01_03
    {

        /// <summary>
	    /// Este caso de uso tem como objetivo identificar o solicitante e o serviço disponível de acordo com as parametrizações para direcionar para a tela parametrizada para a criação da solicitação.
	    /// </summary>
        public const string MANTER_SOLICITACAO = "UC_SRC_004_01_03.Manter_Solicitacao";

    }

    /// <summary>
	/// Contém a lista de funcionalidades para o caso de uso UC_SRC_004_01_04.
	/// </summary>
    public static class UC_SRC_004_01_04
    {

        /// <summary>
	    /// Este caso de uso tem como objetivo consultar os dados pessoais e os dados de contato de um solicitante.
	    /// </summary>
        public const string CONSULTAR_DADOS_SOLICITANTE = "UC_SRC_004_01_04.Consultar_Dados_Solicitante";

    }

    /// <summary>
	/// Contém a lista de funcionalidades para o caso de uso UC_SRC_004_01_05.
	/// </summary>
    public static class UC_SRC_004_01_05
    {

        /// <summary>
	    /// Este caso de uso tem como objetivo consultar os dados de uma notificação .
	    /// </summary>
        public const string CONSULTAR_DADOS_NOTIFICACAO = "UC_SRC_004_01_05.Consultar_dados_notificacao";

    }

    /// <summary>
	/// Contém a lista de funcionalidades para o caso de uso UC_SRC_004_01_06.
	/// </summary>
    public static class UC_SRC_004_01_06
    {

        /// <summary>
	    /// Este caso de uso tem como objetivo consultar o histórico de acesso de páginas de uma etapa.
	    /// </summary>
        public const string CONSULTAR_HISTORICO_NAVEGACAO = "UC_SRC_004_01_06.Consultar_historico_navegacao";

    }

    /// <summary>
	/// Contém a lista de funcionalidades para o caso de uso UC_SRC_004_01_07.
	/// </summary>
    public static class UC_SRC_004_01_07
    {

        /// <summary>
	    /// Este caso de uso tem como objetivo consultar os bloqueios de uma pessoa atuação em uma etapa.
	    /// </summary>
        public const string CONSULTAR_BLOQUEIOS_ETAPA = "UC_SRC_004_01_07.Consultar_bloqueios_etapa";

    }

    /// <summary>
	/// Contém a lista de funcionalidades para o caso de uso UC_SRC_004_01_08.
	/// </summary>
    public static class UC_SRC_004_01_08
    {

        /// <summary>
	    /// Este caso de uso tem como objetivo cancelar uma solicitação de serviço.
	    /// </summary>
        public const string CANCELAR_SOLICITACAO = "UC_SRC_004_01_08.Cancelar_solicitacao";

    }

    /// <summary>
	/// Contém a lista de funcionalidades para o caso de uso UC_SRC_004_01_09.
	/// </summary>
    public static class UC_SRC_004_01_09
    {

        /// <summary>
	    /// Essa solicitação tem como objetivo reabrir uma solicitação.
	    /// </summary>
        public const string REABRIR_SOLICITACAO = "UC_SRC_004_01_09.Reabrir_solicitacao";

    }

    /// <summary>
	/// Contém a lista de funcionalidades para o caso de uso UC_SRC_004_01_10.
	/// </summary>
    public static class UC_SRC_004_01_10
    {

        /// <summary>
	    /// Este caso de uso tem como objetivo consultar os dados pessoais e os dados de contato de um solicitante.
	    /// </summary>
        public const string CONSULTAR_DADOS_ACADEMICOS = "UC_SRC_004_01_10.Consultar_Dados_Academicos";

    }

    /// <summary>
	/// Contém a lista de funcionalidades para o caso de uso UC_SRC_004_01_11.
	/// </summary>
    public static class UC_SRC_004_01_11
    {

        /// <summary>
	    /// Este caso de uso tem como objetivo consultar os dados pessoais e os dados de contato de um solicitante.
	    /// </summary>
        public const string CONSULTAR_INFORMACOES_PROCESSO = "UC_SRC_004_01_11.Consultar_Informacoes_Processo";

    }

    /// <summary>
	/// Contém a lista de funcionalidades para o caso de uso UC_SRC_004_02_01.
	/// </summary>
    public static class UC_SRC_004_02_01
    {

        /// <summary>
	    /// No campo "Prazo de entrega" deve ser permitido selecionar a data corrente do sistema e datas maiores que a data atual.
	    /// </summary>
        public const string CONTROLE_PRAZO_ENTREGA = "UC_SRC_004_02_01.Controle_Prazo_Entrega";

        /// <summary>
	    /// Este caso de uso tem como objetivo visualizar e entregar documentos  requeridos em um processo, registrar a entrega e validá-los.
	    /// </summary>
        public const string REGISTRAR_DOCUMENTOS = "UC_SRC_004_02_01.Registrar_Documentos";

        /// <summary>
	    /// Verificar se o documento requerido em questão foi parametrizado para ser validado por outro setor.     <b>1. </b>Se foi, exibir as situações:  <b>- Aguardando entrega</b>;  - <b>Deferido</b>;  - <b>Indeferido</b>;  - Verificar se o documento pode ser entregue posteriormente. Se sim, exibir também a situação: <b>Pendente</b>.      <b>2. </b>Se não foi, todos os campos deverão ser exibidos desabilitados e preenchidos com os valores atuais da situação do documento em questão.
	    /// </summary>
        public const string VALIDACAO_CRA = "UC_SRC_004_02_01.Validacao_CRA";

        /// <summary>
	    /// Verificar se o documento requerido em questão foi parametrizado para ser validado por outro setor.     <b>1. </b>Se foi, verificar se a situação atual do documento é <b>Deferido</b> ou <b>Indeferido</b>   - Se sim, exibir todos os campos referentes ao documento em questão desabilitados e preenchidos com os valores atuais da situação do documento;   - Se não, exibir as situações:  <b>  - Indeferido</b>  <b>  - Aguardando entrega</b>;  <b>  - Aguardando análise do setor responsável</b>;  <b>  - </b>Verificar se o documento pode ser entregue posteriormente. Se sim, exibir também a situação: <b>Pendente</b>.      <b>2. </b>Se não foi, exibir as situações:  <b>- Aguardando entrega</b>;  - <b>Deferido</b>;  - <b>Indeferido</b>;  - Verificar se o documento pode ser entregue posteriormente. Se sim, exibir também a situação: <b>Pendente</b>.
	    /// </summary>
        public const string VALIDACAO_SECRETARIA = "UC_SRC_004_02_01.Validacao_Secretaria";

    }

    /// <summary>
	/// Contém a lista de funcionalidades para o caso de uso UC_SRC_004_03_01.
	/// </summary>
    public static class UC_SRC_004_03_01
    {

        /// <summary>
	    /// Este caso de uso tem como objetivo apresentar as instruções iniciais para realização de atendimento.
	    /// </summary>
        public const string REALIZAR_ATENDIMENTO_INSTRUCAO_INICIAL = "UC_SRC_004_03_01.Realizar_Atendimento_Instrucao_Inicial";

    }

    /// <summary>
	/// Contém a lista de funcionalidades para o caso de uso UC_SRC_004_04_01.
	/// </summary>
    public static class UC_SRC_004_04_01
    {

        /// <summary>
	    /// Esse caso de uso tem como objetivo a apresentação de instruções inicais da solicitação padrão.
	    /// </summary>
        public const string ABERTURA_SOLICITACAO_INSTRUCAO_INICIAL = "UC_SRC_004_04_01.Abertura_Solicitacao_Instrucao_Inicial";

    }

    /// <summary>
	/// Contém a lista de funcionalidades para o caso de uso UC_SRC_004_05_01.
	/// </summary>
    public static class UC_SRC_004_05_01
    {

        /// <summary>
	    /// Este caso de uso tem como objetivo apresentar formulários que estejam configurados para apresentação na solicitação padrão.
	    /// </summary>
        public const string SOLICITACAO_FORMULARIO = "UC_SRC_004_05_01.Solicitacao_Formulario";

    }

    /// <summary>
	/// Contém a lista de funcionalidades para o caso de uso UC_SRC_004_06_01.
	/// </summary>
    public static class UC_SRC_004_06_01
    {

        /// <summary>
	    /// Este caso de uso tem como objetivo o upload de arquivos de uma solicitação padrão.  
	    /// </summary>
        public const string ABERTURA_SOLICITACAO_UPLOAD_ARQUIVOS = "UC_SRC_004_06_01.Abertura_Solicitacao_Upload_Arquivos";

    }

    /// <summary>
	/// Contém a lista de funcionalidades para o caso de uso UC_SRC_004_07_01.
	/// </summary>
    public static class UC_SRC_004_07_01
    {

        /// <summary>
	    /// Este caso de uso tem como objetivo a apresentação de uma página de confirmação final da solicitação padrão.
	    /// </summary>
        public const string ABERTURA_SOLICITACAO_CONFIRMACAO = "UC_SRC_004_07_01.Abertura_Solicitacao_Confirmacao";

    }

    /// <summary>
	/// Contém a lista de funcionalidades para o caso de uso UC_SRC_004_08_01.
	/// </summary>
    public static class UC_SRC_004_08_01
    {

        /// <summary>
	    /// Esse caso de uso tem como objetivo a apresentação de instruções finais da solicitação padrão.
	    /// </summary>
        public const string ABERTURA_SOLICITACAO_INSTRUCAO_FINAL = "UC_SRC_004_08_01.Abertura_Solicitacao_Instrucao_Final";

    }

    /// <summary>
	/// Contém a lista de funcionalidades para o caso de uso UC_SRC_004_09_01.
	/// </summary>
    public static class UC_SRC_004_09_01
    {

        /// <summary>
	    /// Este caso de uso tem como objetivo emitir o parecer final do atendimento da solicitação.
	    /// </summary>
        public const string REALIZAR_ATENDIMENTO_PARECER = "UC_SRC_004_09_01.Realizar_Atendimento_Parecer";

    }

    /// <summary>
	/// Contém a lista de funcionalidades para o caso de uso UC_SRC_004_10_01.
	/// </summary>
    public static class UC_SRC_004_10_01
    {

        /// <summary>
	    /// Este caso de uso tem como objetivo a solicitação de uma atividade complementar.
	    /// </summary>
        public const string SOLICITACAO_ATIVIDADE_COMPLEMENTAR = "UC_SRC_004_10_01.Solicitacao_Atividade_Complementar";

    }

    /// <summary>
	/// Contém a lista de funcionalidades para o caso de uso UC_SRC_004_11_01.
	/// </summary>
    public static class UC_SRC_004_11_01
    {

        /// <summary>
	    /// Este caso de uso tem como objetivo a seleção de componentes/grupos a serem dispensados na solicitação de dispensa.
	    /// </summary>
        public const string SOLICITACAO_ITENS_DISPENSADOS = "UC_SRC_004_11_01.Solicitacao_Itens_Dispensados";

    }

    /// <summary>
	/// Contém a lista de funcionalidades para o caso de uso UC_SRC_004_12_01.
	/// </summary>
    public static class UC_SRC_004_12_01
    {

        /// <summary>
	    /// Este caso de uso tem como objetivo a seleção de itens que foram cursados (internamente ou externamente) e que dispensam algum componente/grupo.
	    /// </summary>
        public const string SOLICITACAO_ITENS_CURSADOS_DISPENSA = "UC_SRC_004_12_01.Solicitacao_Itens_Cursados_Dispensa";

    }

    /// <summary>
	/// Contém a lista de funcionalidades para o caso de uso UC_SRC_004_13_01.
	/// </summary>
    public static class UC_SRC_004_13_01
    {

        /// <summary>
	    /// Este caso de uso tem como objetivo realizar o atendimento de uma solicitação de intercâmbio.
	    /// </summary>
        public const string ATENDIMENTO_INTERCAMBIO = "UC_SRC_004_13_01.Atendimento_Intercambio";

    }

    /// <summary>
	/// Contém a lista de funcionalidades para o caso de uso UC_SRC_004_14_01.
	/// </summary>
    public static class UC_SRC_004_14_01
    {

        /// <summary>
	    /// Esse caso de uso tem como objetivo a consulta simplificada de uma solicitação.
	    /// </summary>
        public const string CONSULTAR_SOLICITACAO = "UC_SRC_004_14_01.Consultar_solicitacao";

    }

    /// <summary>
	/// Contém a lista de funcionalidades para o caso de uso UC_SRC_004_15_01.
	/// </summary>
    public static class UC_SRC_004_15_01
    {

        /// <summary>
	    /// Este caso de uso tem como objetivo emitir o parecer final do atendimento da solicitação de reabertura de matrícula.
	    /// </summary>
        public const string REALIZAR_ATENDIMENTO_PARECER_ATENDIMENTO_REABERTURA = "UC_SRC_004_15_01.Realizar_Atendimento_Parecer_atendimento_reabertura";

    }

    /// <summary>
	/// Contém a lista de funcionalidades para o caso de uso UC_SRC_004_16_01.
	/// </summary>
    public static class UC_SRC_004_16_01
    {

        /// <summary>
	    /// Este caso de uso permite ao usuário solicitar a alteração da data fim do período de intercâmbio.
	    /// </summary>
        public const string SOLICITACAO_ALTERACAO_PERIODO_INTERCAMBIO = "UC_SRC_004_16_01.Solicitacao_Alteracao_Periodo_Intercambio";

    }

    /// <summary>
	/// Contém a lista de funcionalidades para o caso de uso UC_SRC_004_17_01.
	/// </summary>
    public static class UC_SRC_004_17_01
    {

        /// <summary>
	    /// Esse caso de uso tem como objetivo a pesquisa de grupos de uma solicitação de dispeinsa individual.
	    /// </summary>
        public const string PESQUISAR_AGRUPAMENTO_ITENS_DISPENSA = "UC_SRC_004_17_01.Pesquisar_Agrupamento_Itens_Dispensa";

    }

    /// <summary>
	/// Contém a lista de funcionalidades para o caso de uso UC_SRC_004_17_02.
	/// </summary>
    public static class UC_SRC_004_17_02
    {

        /// <summary>
	    /// Esse caso de uso tem como objetivo a manutenção de grupos de uma solicitação de dispensa individual.
	    /// </summary>
        public const string MANTER_AGRUPAMENTO_ITENS_DISPENSA = "UC_SRC_004_17_02.Manter_Agrupamento_Itens_Dispensa";

    }

    /// <summary>
	/// Contém a lista de funcionalidades para o caso de uso UC_SRC_004_18_01.
	/// </summary>
    public static class UC_SRC_004_18_01
    {

        /// <summary>
	    /// Este caso de uso tem como objetivo analisar se o documento de conclusão poderá ser emitido ou não.
	    /// </summary>
        public const string REALIZAR_ATENDIMENTO_DOCTO_CONCLUSAO_ANALISE_EMISSAO = "UC_SRC_004_18_01.Realizar_Atendimento_Docto_Conclusao_Analise_Emissao";

    }

    /// <summary>
	/// Contém a lista de funcionalidades para o caso de uso UC_SRC_004_18_02.
	/// </summary>
    public static class UC_SRC_004_18_02
    {

        /// <summary>
	    /// Este caso de uso tem como objetivo exibir os dados da documentação acadêmica
	    /// </summary>
        public const string REALIZAR_ATENDIMENTO_DOCTO_CONCLUSAO_CONSULTAR_HISTORICO_ESCOLAR = "UC_SRC_004_18_02.Realizar_Atendimento_Docto_Conclusao_Consultar_Historico_Escolar";

    }

    /// <summary>
	/// Contém a lista de funcionalidades para o caso de uso UC_SRC_004_19_01.
	/// </summary>
    public static class UC_SRC_004_19_01
    {

        /// <summary>
	    /// Este caso de uso tem como objetivo permitir a impressão do documento de conclusão conforme template parametrizado por Instituição x Nível de Ensino x Tipo de Documento x Tipo de Papel.
	    /// </summary>
        public const string REALIZAR_ATENDIMENTO_DOCTO_CONCLUSAO_PARECER_IMPRESSAO = "UC_SRC_004_19_01.Realizar_Atendimento_Docto_Conclusao_Parecer_Impressao";

    }

    /// <summary>
	/// Contém a lista de funcionalidades para o caso de uso UC_SRC_004_20_01.
	/// </summary>
    public static class UC_SRC_004_20_01
    {

        /// <summary>
	    /// Este caso de uso tem como objetivo confirmar as assinaturas e registrar documento.
	    /// </summary>
        public const string REALIZAR_ATENDIMENTO_DOCTO_CONCLUSAO_ASSINATURA_REGISTRO = "UC_SRC_004_20_01.Realizar_Atendimento_Docto_Conclusao_Assinatura_Registro";

    }

    /// <summary>
	/// Contém a lista de funcionalidades para o caso de uso UC_SRC_004_21_01.
	/// </summary>
    public static class UC_SRC_004_21_01
    {

        /// <summary>
	    /// Este caso de uso tem como objetivo permitir a impressão do protoloco de entrega para assinatura do aluno e, possibilidade de confirmar a entrega do documento ou descarte do mesmo.
	    /// </summary>
        public const string REALIZAR_ATENDIMENTO_DOCTO_CONCLUSAO_ENTREGA_DOCUMENTO = "UC_SRC_004_21_01.Realizar_Atendimento_Docto_Conclusao_Entrega_Documento";

    }

    /// <summary>
	/// Contém a lista de funcionalidades para o caso de uso UC_SRC_004_22_01.
	/// </summary>
    public static class UC_SRC_004_22_01
    {

        /// <summary>
	    /// Este caso de uso tem como objetivo listar as taxas parametrizadas para o respectivo serviço e identificação da forma de emissão.
	    /// </summary>
        public const string ABERTURA_SOLICITACAO_COBRANCA_TAXA = "UC_SRC_004_22_01.Abertura_Solicitacao_Cobranca_Taxa";

    }

    /// <summary>
	/// Contém a lista de funcionalidades para o caso de uso UC_SRC_004_23_01.
	/// </summary>
    public static class UC_SRC_004_23_01
    {

        /// <summary>
	    /// Este caso de uso permite ao usuário solicitar a alteração da data fim do período de intercâmbio.
	    /// </summary>
        public const string SOLICITACAO_ALTERACAO_PUBLICACAO_BPD = "UC_SRC_004_23_01.Solicitacao_Alteracao_Publicacao_BPD";

    }

    /// <summary>
	/// Contém a lista de funcionalidades para o caso de uso UC_SRC_004_24_01.
	/// </summary>
    public static class UC_SRC_004_24_01
    {

        /// <summary>
	    /// Este caso de uso tem como objetivo exibir os dados pessoais do solicitante para que o mesmo confirme se os dados estão corretos. Senão, permite anexar os documentos para que os dados sejam atualizados.
	    /// </summary>
        public const string ABERTURA_SOLICITACAO_CONFIRMACAO_DADOS_PESSOAIS = "UC_SRC_004_24_01.Abertura_Solicitacao_Confirmacao_Dados_Pessoais";

    }

    /// <summary>
	/// Contém a lista de funcionalidades para o caso de uso UC_SRC_004_25_01.
	/// </summary>
    public static class UC_SRC_004_25_01
    {

        /// <summary>
	    /// Este caso de uso tem como objetivo realizar o aceite de entrega de documentos de conclusão digitais.
	    /// </summary>
        public const string SOLICITACAO_DOCTO_DIGITAL_ENTREGA = "UC_SRC_004_25_01.Solicitacao_Docto_Digital_Entrega";

    }

    /// <summary>
	/// Contém a lista de funcionalidades para o caso de uso UC_SRC_004_26_01.
	/// </summary>
    public static class UC_SRC_004_26_01
    {

        /// <summary>
	    /// Este caso de uso tem como objetivo realizar o download de documentos de conclusão assinados digitalmente.
	    /// </summary>
        public const string SOLICITACAO_DOCTO_DIGITAL_DOWNLOAD = "UC_SRC_004_26_01.Solicitacao_Docto_Digital_Download";

    }

    /// <summary>
	/// Contém a lista de funcionalidades para o caso de uso UC_SRC_004_27_01.
	/// </summary>
    public static class UC_SRC_004_27_01
    {

        /// <summary>
	    /// Esse caso de uso tem como objetivo realizar a solicitação de retificação de faltas.
	    /// </summary>
        public const string SOLICITACAO_RETIFICACAO_FALTAS = "UC_SRC_004_27_01.Solicitacao_retificacao_faltas";

    }

    /// <summary>
	/// Contém a lista de funcionalidades para o caso de uso UC_SRC_004_28_01.
	/// </summary>
    public static class UC_SRC_004_28_01
    {

        /// <summary>
	    /// Este caso de uso tem como objetivo o upload de arquivos de uma solicitação.
	    /// </summary>
        public const string UPLOAD_ARQUIVO = "UC_SRC_004_28_01.Upload_arquivo";

    }

    /// <summary>
	/// Contém a lista de funcionalidades para o caso de uso UC_SRC_004_29_01.
	/// </summary>
    public static class UC_SRC_004_29_01
    {

        /// <summary>
	    /// Este caso de uso tem como objetivo permitir o atendimento de uma solicitação de entrega de documentos, realizada pelo Portal do Aluno.
	    /// </summary>
        public const string REALIZAR_ATENDIMENTO_ENTREGA_DOCUMENTOS = "UC_SRC_004_29_01.Realizar_Atendimento_Entrega_Documentos";

    }

    /// <summary>
	/// Contém a lista de funcionalidades para o caso de uso UC_SRC_004_30_01.
	/// </summary>
    public static class UC_SRC_004_30_01
    {

        /// <summary>
	    /// Este caso de uso permite ao usuário selecionar o componente curricular para entrega do projeto de qualificação.
	    /// </summary>
        public const string SOLICITACAO_DEPOSITO_PROJETO_QUALIFICACAO = "UC_SRC_004_30_01.SolicitacaoDepositoProjetoQualificacao";

    }

    /// <summary>
	/// Contém a lista de funcionalidades para o caso de uso UC_SRC_005_01_01.
	/// </summary>
    public static class UC_SRC_005_01_01
    {

        /// <summary>
	    /// Esse caso de uso tem como objetivo apresentar uma consulta com a posição consolidada de solicitações de um processo.
	    /// </summary>
        public const string CONSULTAR_POSICAO_CONSOLIDADA = "UC_SRC_005_01_01.Consultar_Posicao_Consolidada";

    }

    /// <summary>
	/// Contém a lista de funcionalidades para o caso de uso UC_SRC_005_02_01.
	/// </summary>
    public static class UC_SRC_005_02_01
    {

        /// <summary>
	    /// Esse caso de uso tem como objetivo apresentar uma consulta com a posição consolidada de solicitações de vários processos.
	    /// </summary>
        public const string CONSULTA_POSICAO_CONSOLIDADA_GERAL = "UC_SRC_005_02_01.Consulta_Posicao_Consolidada_Geral";

    }

    /// <summary>
	/// Contém a lista de funcionalidades para o caso de uso UC_SRC_005_03_01.
	/// </summary>
    public static class UC_SRC_005_03_01
    {

        /// <summary>
	    /// Este caso de uso tem como objetivo exibir os relatórios existentes para o assunto "Serviço" e realizar o filtro e emissão de cada um deles.
	    /// </summary>
        public const string CENTRAL_RELATORIOS_SERVICOS = "UC_SRC_005_03_01.Central_Relatorios_Servicos";

    }

    /// <summary>
	/// Contém a lista de funcionalidades para o caso de uso UC_SRC_005_03_02.
	/// </summary>
    public static class UC_SRC_005_03_02
    {

        /// <summary>
	    /// Este caso de uso tem como objetivo contabilizar as solicitações de todos os processos de um determinado serviço e ciclo letivo filtrados, organizando-as por:  &nbsp;  <ul>   <li>Entidade Responsável (Grupo de Programa)</li>   <li>Entidade do vínculo (Curso/Programa), Etapas do serviço e situações da etapa (Não Iniciada, Em andamento, Finalizada com sucesso, Finalizada sem sucesso e Cancelada).</li>  </ul>
	    /// </summary>
        public const string EXIBIR_RELATORIO_POSICAO_CONSOLIDADA_SERVICO_CICLO_LETIVO = "UC_SRC_005_03_02.Exibir_Relatorio_Posicao_Consolidada_Servico_Ciclo_Letivo";

    }

    /// <summary>
	/// Contém a lista de funcionalidades para o caso de uso UC_SRC_005_03_03.
	/// </summary>
    public static class UC_SRC_005_03_03
    {

        /// <summary>
	    /// Este caso de uso tem como objetivo exibir as solicitações de um processo que possuem algum bloqueio conforme a etapa em que se encontrem.
	    /// </summary>
        public const string EXIBIR_RELATORIO_SOLICITACOES_COM_BLOQUEIOS = "UC_SRC_005_03_03.Exibir_Relatorio_Solicitacoes_Com_Bloqueios";

    }

}

