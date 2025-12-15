 
using System;

namespace SMC.Academico.Common.Areas.PES.Constants
{
    /// <summary>
	/// Contém a lista de funcionalidades para o caso de uso UC_PES_001_03_01.
	/// </summary>
    public static class UC_PES_001_03_01
    {

        /// <summary>
	    /// Este caso de uso tem como objetivo pesquisar referências familiares de uma pessoa em uma atuação, permitindo também a exclusão.
	    /// </summary>
        public const string PESQUISAR_REFERENCIA_FAMILIAR = "UC_PES_001_03_01.Pesquisar_Referencia_Familiar";

    }

    /// <summary>
	/// Contém a lista de funcionalidades para o caso de uso UC_PES_001_03_02.
	/// </summary>
    public static class UC_PES_001_03_02
    {

        /// <summary>
	    /// Este caso de uso tem como objetivo cadastrar ou alterar referências familiares de uma pessoa em uma atuação.
	    /// </summary>
        public const string MANTER_REFERENCIA_FAMILIAR = "UC_PES_001_03_02.Manter_Referencia_Familiar";

    }

    /// <summary>
	/// Contém a lista de funcionalidades para o caso de uso UC_PES_001_04_01.
	/// </summary>
    public static class UC_PES_001_04_01
    {

        /// <summary>
	    /// Este caso de uso tem como objetivo visualizar e entregar documentos de uma pessoa, registrar a entrega e validá-los.
	    /// </summary>
        public const string VISUALIZAR_DOCUMENTOS_PESSOA_ATUACAO = "UC_PES_001_04_01.Visualizar_Documentos_Pessoa_Atuacao";

    }

    /// <summary>
	/// Contém a lista de funcionalidades para o caso de uso UC_PES_001_04_02.
	/// </summary>
    public static class UC_PES_001_04_02
    {

        /// <summary>
	    /// Este caso de uso tem como objetivo visualizar e entregar documentos de uma pessoa, registrar a entrega e validá-los.
	    /// </summary>
        public const string REGISTRAR_DOCUMENTO = "UC_PES_001_04_02.Registrar_Documento";

        /// <summary>
	    /// Verificar o documento requerido em questão foi parametrizado para ser validado por outro setor, em pelo menos uma das solicitações selecionadas na interface.     <b>1. </b>Se foi, exibir as situações:  <b>- Aguardando entrega</b>;  - <b>Deferido</b>;  - <b>Indeferido</b>;  - Verificar se o documento pode ser entregue posteriormente, em <b>TODAS </b>as solicitações selecionadas na interface. Se sim, exibir também a situação: <b>Pendente</b>.
	    /// </summary>
        public const string VALIDACAO_CRA = "UC_PES_001_04_02.Validacao_CRA";

        /// <summary>
	    /// Verificar se o documento requerido em questão foi parametrizado em todas as solicitações selecionadas na interface para <b>NÃO</b> ser validado por outro setor.     <b>1. </b>Se foi, exibir as situações:  <b>- Aguardando entrega</b>;  - <b>Deferido</b>;  - <b>Indeferido</b>;  - Verificar se o documento pode ser entregue posteriormente, em <b>TODAS </b>as solicitações selecionadas na interface. Se sim, exibir também a situação: <b>Pendente</b>.      <b>2. </b>Se foi parametrizado em pelo menos uma das solicitações selecionadas na interface para ser validado por outro setor, exibir as situações:   <b>- Aguardando entrega</b>;  <b>- Aguardando análise do setor responsável</b>;  - Verificar se o documento pode ser entregue posteriormente, em <b>TODAS </b>as solicitações selecionadas na interface. Se sim, exibir também a situação: <b>Pendente</b>.  
	    /// </summary>
        public const string VALIDACAO_SECRETARIA = "UC_PES_001_04_02.Validacao_Secretaria";

    }

    /// <summary>
	/// Contém a lista de funcionalidades para o caso de uso UC_PES_001_09_01.
	/// </summary>
    public static class UC_PES_001_09_01
    {

        /// <summary>
	    /// Esse caso de uso tem como objetivo selecionar pessoas para emissão da identidade acadêmica.
	    /// </summary>
        public const string PESQUISAR_PESSOA_RELATORIO_IDENTIDADE_ACADEMICA = "UC_PES_001_09_01.Pesquisar_Pessoa_Relatorio_Identidade_Academica";

    }

    /// <summary>
	/// Contém a lista de funcionalidades para o caso de uso UC_PES_001_09_02.
	/// </summary>
    public static class UC_PES_001_09_02
    {

        /// <summary>
	    /// Esse caso de uso tem como objetivo a emissão da identidade acadêmica.
	    /// </summary>
        public const string EXIBIR_RELATORIO_IDENTIDADE_ACADEMICA = "UC_PES_001_09_02.Exibir_Relatorio_Identidade_Academica";

    }

    /// <summary>
	/// Contém a lista de funcionalidades para o caso de uso UC_PES_001_10_01.
	/// </summary>
    public static class UC_PES_001_10_01
    {

        /// <summary>
	    /// Esse caso de uso tem como objetivo permitir que a pessoa (aluno ou professor)consulte as suas credenciais de acesso da instituição    Credenciais de:  - Webmail  - Rede Eduroam wi-fi  - Office 365  - Portal CAPES  - Rede academica (laboratorios)
	    /// </summary>
        public const string CONSULTA_CREDENCIAIS_ACESSO = "UC_PES_001_10_01.Consulta_Credenciais_Acesso";

    }

    /// <summary>
	/// Contém a lista de funcionalidades para o caso de uso UC_PES_001_11_01.
	/// </summary>
    public static class UC_PES_001_11_01
    {

        /// <summary>
	    /// Este caso de uso tem como objetivo visualizar os documentos registrados para uma pessoa-atuação.
	    /// </summary>
        public const string PESQUISAR_DOCUMENTOS_PESSOA_ATUACAO = "UC_PES_001_11_01.Pesquisar_Documentos_Pessoa_Atuacao";

    }

    /// <summary>
	/// Contém a lista de funcionalidades para o caso de uso UC_PES_001_11_02.
	/// </summary>
    public static class UC_PES_001_11_02
    {

        /// <summary>
	    /// Este caso de uso tem como objetivo manter os documentos registrados para uma pessoa-atuação.
	    /// </summary>
        public const string MANTER_DOCUMENTOS_PESSOA_ATUACAO = "UC_PES_001_11_02.Manter_Documentos_Pessoa_Atuacao";

    }

    /// <summary>
	/// Contém a lista de funcionalidades para o caso de uso UC_PES_002_01_01.
	/// </summary>
    public static class UC_PES_002_01_01
    {

        /// <summary>
	    /// Pesquisar as titulações que compoem a formação acadêmica de uma pesssoa x atuação.
	    /// </summary>
        public const string PESQUISAR_FORMACAO_ACADEMICA = "UC_PES_002_01_01.Pesquisar_Formacao_Academica";

    }

    /// <summary>
	/// Contém a lista de funcionalidades para o caso de uso UC_PES_002_01_02.
	/// </summary>
    public static class UC_PES_002_01_02
    {

        /// <summary>
	    /// Tem como objetivo consultar, alterar,incluir e excluir as titulações  que compoem a formação acadêmca de uma pessoa x atuação.
	    /// </summary>
        public const string MANTER_FORMACAO_ACADEMICA = "UC_PES_002_01_02.Manter_Formacao_Academica";

    }

    /// <summary>
	/// Contém a lista de funcionalidades para o caso de uso UC_PES_003_01_01.
	/// </summary>
    public static class UC_PES_003_01_01
    {

        /// <summary>
	    /// Este caso de uso tem como objetivo manter o cadastro de parâmetro de tipo de atuação por Instituição, permitindo todas as funcionalidades de um CRUD básico (consulta, cadastro, alteração e exclusão)    É um cadastro dynamic.
	    /// </summary>
        public const string MANTER_TIPO_ATUACAO_INSTITUICAO = "UC_PES_003_01_01.Manter_Tipo_Atuacao_Instituicao";

    }

    /// <summary>
	/// Contém a lista de funcionalidades para o caso de uso UC_PES_003_02_01.
	/// </summary>
    public static class UC_PES_003_02_01
    {

        /// <summary>
	    /// Este caso de uso tem como objetivo, consultar, cadastrar ou alterar a parametrização de motivo de bloqueio por instituição de ensino, permitindo todas as funcionalidades de um CRUD básico (consulta, cadastro, alteração e exclusão).
	    /// </summary>
        public const string MANTER_MOTIVO_BLOQUEIO_INSTITUICAO = "UC_PES_003_02_01.Manter_Motivo_Bloqueio_Instituicao";

    }

    /// <summary>
	/// Contém a lista de funcionalidades para o caso de uso UC_PES_003_03_01.
	/// </summary>
    public static class UC_PES_003_03_01
    {

        /// <summary>
	    /// Esse caso de uso tem por objetivo pesquisar as configurações de tipo de mensagem por instituição e nível de ensino.
	    /// </summary>
        public const string PESQUISAR_TIPO_MENSAGEM_INSTITUICAO_NIVEL = "UC_PES_003_03_01.Pesquisar_Tipo_Mensagem_Instituicao_Nivel";

    }

    /// <summary>
	/// Contém a lista de funcionalidades para o caso de uso UC_PES_003_03_02.
	/// </summary>
    public static class UC_PES_003_03_02
    {

        /// <summary>
	    /// Esse caso de uso tem como objetivo a associação de um tipo de mensagem a uma instituição x nível de ensino.
	    /// </summary>
        public const string MANTER_TIPO_MENSAGEM_INSTITUICAO_NIVEL = "UC_PES_003_03_02.Manter_Tipo_Mensagem_Instituicao_Nivel";

    }

    /// <summary>
	/// Contém a lista de funcionalidades para o caso de uso UC_PES_003_04_01.
	/// </summary>
    public static class UC_PES_003_04_01
    {

        /// <summary>
	    /// Este caso de uso tem como objetivo, consultar, cadastrar ou alterar a parametrização de tipos de vínculo de funcionário por instituição de ensino, permitindo todas as funcionalidades de um CRUD básico (consulta, cadastro, alteração e exclusão).
	    /// </summary>
        public const string MANTER_TIPO_VINCULO_FUNCIONARIO_INSTITUICAO = "UC_PES_003_04_01.Manter_Tipo_Vinculo_Funcionario_Instituicao";

    }

    /// <summary>
	/// Contém a lista de funcionalidades para o caso de uso UC_PES_003_05_01.
	/// </summary>
    public static class UC_PES_003_05_01
    {

        /// <summary>
	    /// Este caso de uso tem como objetivo manter a associação dos tipos de vínculo de funcionário ao tipo de entidade da Instituição de Ensino logada, permitindo todas as funcionalidades de um CRUD básico (consulta, cadastro, alteração e exclusão).
	    /// </summary>
        public const string MANTER_TIPO_VINCULO_FUNCIONARIO_INSTITUICAO_TIPO_ENTIDADE = "UC_PES_003_05_01.Manter_Tipo_Vinculo_Funcionario_Instituicao_Tipo_Entidade";

    }

    /// <summary>
	/// Contém a lista de funcionalidades para o caso de uso UC_PES_004_01_01.
	/// </summary>
    public static class UC_PES_004_01_01
    {

        /// <summary>
	    /// Este caso de uso tem como objetivo manter o cadastro de tipos de bloqueio permitindo todas as funcionalidades de um CRUD básico (consulta, cadastro, alteração e exclusão)
	    /// </summary>
        public const string MANTER_TIPO_BLOQUEIO = "UC_PES_004_01_01.Manter_Tipo_Bloqueio";

    }

    /// <summary>
	/// Contém a lista de funcionalidades para o caso de uso UC_PES_004_02_01.
	/// </summary>
    public static class UC_PES_004_02_01
    {

        /// <summary>
	    /// Este caso de uso tem como objetivo manter o cadastro de motivos de bloqueio permitindo todas as funcionalidades de um CRUD básico (consulta, cadastro, alteração e exclusão)
	    /// </summary>
        public const string MANTER_MOTIVO_BLOQUEIO = "UC_PES_004_02_01.Manter_Motivo_Bloqueio";

    }

    /// <summary>
	/// Contém a lista de funcionalidades para o caso de uso UC_PES_004_03_01.
	/// </summary>
    public static class UC_PES_004_03_01
    {

        /// <summary>
	    /// Ata Colação Grau Acadêmico - Cadastro
	    /// </summary>
        public const string ATA_COLACAO_GRAU_ACADEMICO_CADASTRO = "UC_PES_004_03_01.Ata_Colacao_Grau_Academico_Cadastro";

        /// <summary>
	    /// Ata Colação Grau Acadêmico - Desbloqueio
	    /// </summary>
        public const string ATA_COLACAO_GRAU_ACADEMICO_DESBLOQUEIO = "UC_PES_004_03_01.Ata_Colacao_Grau_Academico_Desbloqueio";

        /// <summary>
	    /// Entrega Diploma Danificado - Desbloqueio
	    /// </summary>
        public const string ENTREGA_DIPLOMA_DANIFICADO_DESBLOQUEIO = "UC_PES_004_03_01.Entrega_Diploma_Danificado_Desbloqueio";

        /// <summary>
	    /// Material Pendente Biblioteca - Desbloqueio
	    /// </summary>
        public const string MATERIAL_PENDENTE_BIBLIOTECA_DESBLOQUEIO = "UC_PES_004_03_01.Material_Pendente_Biblioteca_Desbloqueio";

        /// <summary>
	    /// Pagamento Taxa Serviço Acadêmico - Desbloqueio
	    /// </summary>
        public const string PAGAMENTO_TAXA_SERVICO_ACADEMICO_DESBLOQUEIO = "UC_PES_004_03_01.Pagamento_Taxa_Servico_Academico_Desbloqueio";

        /// <summary>
	    /// Parcela de Matrícula - Desbloqueio
	    /// </summary>
        public const string PARCELA_MATRICULA_DESBLOQUEIO = "UC_PES_004_03_01.Parcela_Matricula_Desbloqueio";

        /// <summary>
	    /// Parcela de Pré-Matrícula - Desbloqueio
	    /// </summary>
        public const string PARCELA_PRE_MATRICULA_DESBLOQUEIO = "UC_PES_004_03_01.Parcela_Pre_Matricula_Desbloqueio";

        /// <summary>
	    /// Parcela de Serviço Adicional - Desbloqueio
	    /// </summary>
        public const string PARCELA_SERVICO_ADICIONAL_DESBLOQUEIO = "UC_PES_004_03_01.Parcela_Servico_Adicional_Desbloqueio";

        /// <summary>
	    /// Este caso de uso tem como objetivo pesquisar os bloqueios de uma pessoa em uma atuação, permitindo também a exclusão.  
	    /// </summary>
        public const string PESQUISAR_BLOQUEIO = "UC_PES_004_03_01.Pesquisar_Bloqueio";

    }

    /// <summary>
	/// Contém a lista de funcionalidades para o caso de uso UC_PES_004_03_02.
	/// </summary>
    public static class UC_PES_004_03_02
    {

        /// <summary>
	    /// Este caso de uso tem como objetivo cadastrar ou alterar bloqueios de uma pessoa em uma atuação.  
	    /// </summary>
        public const string MANTER_BLOQUEIO = "UC_PES_004_03_02.Manter_Bloqueio";

    }

    /// <summary>
	/// Contém a lista de funcionalidades para o caso de uso UC_PES_004_03_03.
	/// </summary>
    public static class UC_PES_004_03_03
    {

        /// <summary>
	    /// Este caso de uso tem como objetivo cadastrar ou alterar desbloqueios de uma pessoa em uma atuação.
	    /// </summary>
        public const string DESBLOQUEAR = "UC_PES_004_03_03.Desbloquear";

    }

    /// <summary>
	/// Contém a lista de funcionalidades para o caso de uso UC_PES_005_01_01.
	/// </summary>
    public static class UC_PES_005_01_01
    {

        /// <summary>
	    /// Esse caso de uso tem como objetivo pesquisar os tipos de mensagem cadastrados.
	    /// </summary>
        public const string PESQUISAR_TIPO_MENSAGEM = "UC_PES_005_01_01.Pesquisar_Tipo_Mensagem";

    }

    /// <summary>
	/// Contém a lista de funcionalidades para o caso de uso UC_PES_005_01_02.
	/// </summary>
    public static class UC_PES_005_01_02
    {

        /// <summary>
	    /// Esse caso de uso tem como objetivo a inclusão e/ou alteração de um tipo de mensagem.
	    /// </summary>
        public const string MANTER_TIPO_MENSAGEM = "UC_PES_005_01_02.Manter_Tipo_Mensagem";

    }

    /// <summary>
	/// Contém a lista de funcionalidades para o caso de uso UC_PES_005_02_01.
	/// </summary>
    public static class UC_PES_005_02_01
    {

        /// <summary>
	    /// Esse caso de uso tem como objetivo a pesquisa de mensagens de uma pessoa.
	    /// </summary>
        public const string PESQUISAR_MENSAGEM = "UC_PES_005_02_01.Pesquisar_Mensagem";

    }

    /// <summary>
	/// Contém a lista de funcionalidades para o caso de uso UC_PES_005_02_02.
	/// </summary>
    public static class UC_PES_005_02_02
    {

        /// <summary>
	    /// Esse caso de uso tem como objetivo a manutenção (inclusão / alteração) de uma mensagem enviada.
	    /// </summary>
        public const string MANTER_MENSAGEM = "UC_PES_005_02_02.Manter_Mensagem";

        /// <summary>
	    /// O acesso ao token permite inclusão de mensagens para alunos em ciclo letivos anteriores ao atual.
	    /// </summary>
        public const string PERMITIR_OCORRENCIA_CICLO_LETIVO_ANTERIOR = "UC_PES_005_02_02.Permitir_Ocorrencia_Ciclo_Letivo_Anterior";

    }

    /// <summary>
	/// Contém a lista de funcionalidades para o caso de uso UC_PES_005_03_01.
	/// </summary>
    public static class UC_PES_005_03_01
    {

        /// <summary>
	    /// Este caso de uso tem como objetivo manter o cadastro de Tag, permitindo todas as funcionalidades de um CRUD básico (consulta, cadastro, alteração e exclusão)    É um cadastro dynamic.
	    /// </summary>
        public const string MANTER_TAG = "UC_PES_005_03_01.Manter_Tag";

    }

    /// <summary>
	/// Contém a lista de funcionalidades para o caso de uso UC_PES_006_01_01.
	/// </summary>
    public static class UC_PES_006_01_01
    {

        /// <summary>
	    /// Este caso de uso tem como objetivo manter o cadastro de tipos de vínculo de funcionários, permitindo todas as funcionalidades de um CRUD básico (consulta, cadastro, alteração e exclusão)    É um cadastro dynamic.
	    /// </summary>
        public const string MANTER_TIPO_VINCULO_FUNCIONARIO = "UC_PES_006_01_01.Manter_Tipo_Vinculo_Funcionario";

    }

    /// <summary>
	/// Contém a lista de funcionalidades para o caso de uso UC_PES_006_02_01.
	/// </summary>
    public static class UC_PES_006_02_01
    {

        /// <summary>
	    /// Este caso de uso tem como objetivo consultar os funcionários permitindo também a exclusão.
	    /// </summary>
        public const string PESQUISAR_FUNCIONARIO = "UC_PES_006_02_01.Pesquisar_Funcionario";

    }

    /// <summary>
	/// Contém a lista de funcionalidades para o caso de uso UC_PES_006_02_02.
	/// </summary>
    public static class UC_PES_006_02_02
    {

        /// <summary>
	    /// Este caso de uso tem como objetivo cadastrar ou alterar os dados de um funcionário.
	    /// </summary>
        public const string MANTER_FUNCIONARIO = "UC_PES_006_02_02.Manter_Funcionario";

    }

    /// <summary>
	/// Contém a lista de funcionalidades para o caso de uso UC_PES_006_02_03.
	/// </summary>
    public static class UC_PES_006_02_03
    {

        /// <summary>
	    /// Este caso de uso tem como objetivo consultar os vínculos de um funcionário, permitindo também a exclusão.
	    /// </summary>
        public const string PESQUISAR_VINCULO_FUNCIONARIO = "UC_PES_006_02_03.Pesquisar_Vinculo_Funcionario";

    }

    /// <summary>
	/// Contém a lista de funcionalidades para o caso de uso UC_PES_006_02_04.
	/// </summary>
    public static class UC_PES_006_02_04
    {

        /// <summary>
	    /// Este caso de uso tem como objetivo cadastrar ou alterar os dados do vínculo de um funcionário.
	    /// </summary>
        public const string MANTER_VINCULO_FUNCIONARIO = "UC_PES_006_02_04.Manter_Vinculo_Funcionario";

    }

    /// <summary>
	/// Contém a lista de funcionalidades para o caso de uso UC_PES_007_01_01.
	/// </summary>
    public static class UC_PES_007_01_01
    {

        /// <summary>
	    /// Este caso de uso tem o objetivo de pesquisar as configurações de avaliações cadastradas para os programas.
	    /// </summary>
        public const string PESQUISAR_CONFIGURACAO_AVALIACAO = "UC_PES_007_01_01.Pesquisar_Configuracao_Avaliacao";

    }

    /// <summary>
	/// Contém a lista de funcionalidades para o caso de uso UC_PES_007_01_02.
	/// </summary>
    public static class UC_PES_007_01_02
    {

        /// <summary>
	    /// Caso de uso para inclusão e alteração das configurações de avaliação.
	    /// </summary>
        public const string MANTER_CONFIGURACAO_AVALIACAO = "UC_PES_007_01_02.Manter_Configuracao_Avaliacao";

    }

    /// <summary>
	/// Contém a lista de funcionalidades para o caso de uso UC_PES_007_01_03.
	/// </summary>
    public static class UC_PES_007_01_03
    {

        /// <summary>
	    /// Permite consultar as turmas da configuração.
	    /// </summary>
        public const string CONSULTAR_TURMAS_CONFIGURACAO = "UC_PES_007_01_03.Consultar_Turmas_Configuracao";

    }

    /// <summary>
	/// Contém a lista de funcionalidades para o caso de uso UC_PES_007_01_04.
	/// </summary>
    public static class UC_PES_007_01_04
    {

        /// <summary>
	    /// Permite consultar as amostras geradas para a configuração.
	    /// </summary>
        public const string CONSULTAR_AMOSTRAS = "UC_PES_007_01_04.Consultar_Amostras";

    }

    /// <summary>
	/// Contém a lista de funcionalidades para o caso de uso UC_PES_007_01_05.
	/// </summary>
    public static class UC_PES_007_01_05
    {

        /// <summary>
	    /// Permitir a alteração da data de limite de resposta.
	    /// </summary>
        public const string ALTERAR_DATA_LIMITE_RESPOSTA = "UC_PES_007_01_05.Alterar_Data_Limite_Resposta";

    }

    /// <summary>
	/// Contém a lista de funcionalidades para o caso de uso UC_PES_008_01_01.
	/// </summary>
    public static class UC_PES_008_01_01
    {

        /// <summary>
	    /// Este caso de uso tem como objetivo pesquisar notificações enviadas.
	    /// </summary>
        public const string PESQUISAR_NOTIFICACAO = "UC_PES_008_01_01.Pesquisar_Notificacao";

    }

    /// <summary>
	/// Contém a lista de funcionalidades para o caso de uso UC_PES_008_01_02.
	/// </summary>
    public static class UC_PES_008_01_02
    {

        /// <summary>
	    /// Este caso de uso tem o objetivo enviar notificação.
	    /// </summary>
        public const string ENVIAR_NOTIFICACAO = "UC_PES_008_01_02.Enviar_Notificacao";

    }

    /// <summary>
	/// Contém a lista de funcionalidades para o caso de uso UC_PES_008_02_01.
	/// </summary>
    public static class UC_PES_008_02_01
    {

        /// <summary>
	    /// Este caso de uso tem como objetivo exibir as notificações enviadas para a pessoa-atuação.
	    /// </summary>
        public const string VISUALIZAR_NOTIFICACAO_POR_PESSOA_ATUACAO = "UC_PES_008_02_01.Visualizar_Notificacao_Por_Pessoa_Atuacao";

    }

    /// <summary>
	/// Contém a lista de funcionalidades para o caso de uso UC_PES_008_03_01.
	/// </summary>
    public static class UC_PES_008_03_01
    {

        /// <summary>
	    /// Este caso de uso tem como objetivo exibir od dados das notificações enviadas.
	    /// </summary>
        public const string VISUALIZAR_DADOS_ENVIO_NOTIFICACAO = "UC_PES_008_03_01.Visualizar_Dados_Envio_Notificacao";

    }

}

