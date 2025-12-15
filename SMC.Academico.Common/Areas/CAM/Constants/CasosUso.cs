 
using System;

namespace SMC.Academico.Common.Areas.CAM.Constants
{
    /// <summary>
	/// Contém a lista de funcionalidades para o caso de uso UC_CAM_001_01_01.
	/// </summary>
    public static class UC_CAM_001_01_01
    {

        /// <summary>
	    /// Este caso de uso tem como objetivo pesquisar as campanhas, permitindo também a exclusão.
	    /// </summary>
        public const string PESQUISAR_CAMPANHA = "UC_CAM_001_01_01.Pesquisar_Campanha";

    }

    /// <summary>
	/// Contém a lista de funcionalidades para o caso de uso UC_CAM_001_01_02.
	/// </summary>
    public static class UC_CAM_001_01_02
    {

        /// <summary>
	    /// Este caso de uso tem como objetivo cadastrar e alterar os dados de uma campanha.
	    /// </summary>
        public const string MANTER_CAMPANHA = "UC_CAM_001_01_02.Manter_Campanha";

    }

    /// <summary>
	/// Contém a lista de funcionalidades para o caso de uso UC_CAM_001_01_03.
	/// </summary>
    public static class UC_CAM_001_01_03
    {

        /// <summary>
	    /// Este caso de uso tem como objetivo pesquisar as ofertas associadas a uma campanha, permitindo também a exclusão.
	    /// </summary>
        public const string PESQUISAR_OFERTA_CAMPANHA = "UC_CAM_001_01_03.Pesquisar_Oferta_Campanha";

    }

    /// <summary>
	/// Contém a lista de funcionalidades para o caso de uso UC_CAM_001_01_04.
	/// </summary>
    public static class UC_CAM_001_01_04
    {

        /// <summary>
	    /// Este caso de uso tem como objetivo associar e desassociar em lote ofertas de uma campanha.
	    /// </summary>
        public const string ASSOCIAR_OFERTA_CAMPANHA = "UC_CAM_001_01_04.Associar_Oferta_Campanha";

        /// <summary>
	    /// ESTE CASO DE USO DEVERÁ SER IMPLEMENTADO SOMENTE PARA A GRADUAÇÃO.  ELE DEVERÁ SER REVISADO  Este caso de uso tem como objetivo vincular outros itens a um item de campanha associado previamente, para que sejam ofertados de forma agrupada, em um único item. Também permite desvincular um item que tenha sido associado de forma agrupada.
	    /// </summary>
        public const string MANTER_ASSOCIACAO_OFERTA_CAMPANHA = "UC_CAM_001_01_04.Manter_Associacao_Oferta_Campanha";

    }

    /// <summary>
	/// Contém a lista de funcionalidades para o caso de uso UC_CAM_001_01_05.
	/// </summary>
    public static class UC_CAM_001_01_05
    {

        /// <summary>
	    /// Este caso de uso tem como objetivo configurar as vagas das ofertas associdas à campanha.
	    /// </summary>
        public const string CONFIGURAR_VAGAS_OFERTAS_CAMPANHA = "UC_CAM_001_01_05.Configurar_Vagas_Ofertas_Campanha";

    }

    /// <summary>
	/// Contém a lista de funcionalidades para o caso de uso UC_CAM_001_01_06.
	/// </summary>
    public static class UC_CAM_001_01_06
    {

        /// <summary>
	    /// Este caso de uso tem como objetivo copiar uma campanha.
	    /// </summary>
        public const string COPIAR_CAMPANHA = "UC_CAM_001_01_06.Copiar_Campanha";

    }

    /// <summary>
	/// Contém a lista de funcionalidades para o caso de uso UC_CAM_001_02_01.
	/// </summary>
    public static class UC_CAM_001_02_01
    {

        /// <summary>
	    /// Este caso de uso tem como objetivo pesquisar os processos seletivos, permitindo também a exclusão.
	    /// </summary>
        public const string PESQUISAR_PROCESSO_SELETIVO = "UC_CAM_001_02_01.Pesquisar_Processo_Seletivo";

    }

    /// <summary>
	/// Contém a lista de funcionalidades para o caso de uso UC_CAM_001_02_02.
	/// </summary>
    public static class UC_CAM_001_02_02
    {

        /// <summary>
	    /// Este caso de uso tem como objetivo cadastrar e alterar os dados de um processo seletivo.
	    /// </summary>
        public const string MANTER_PROCESSO_SELETIVO = "UC_CAM_001_02_02.Manter_Processo_Seletivo";

    }

    /// <summary>
	/// Contém a lista de funcionalidades para o caso de uso UC_CAM_001_02_03.
	/// </summary>
    public static class UC_CAM_001_02_03
    {

        /// <summary>
	    /// Este caso de uso tem como objetivo pesquisar as ofertas de um processo seletivo, permitindo também a exclusão.
	    /// </summary>
        public const string PESQUISAR_OFERTA_PROCESSO_SELETIVO = "UC_CAM_001_02_03.Pesquisar_Oferta_Processo_Seletivo";

    }

    /// <summary>
	/// Contém a lista de funcionalidades para o caso de uso UC_CAM_001_02_04.
	/// </summary>
    public static class UC_CAM_001_02_04
    {

        /// <summary>
	    /// Este caso de uso tem como objetivo associar e desassociar em lote ofertas de um processo seletivo.
	    /// </summary>
        public const string ASSOCIAR_OFERTA_PROCESSO_SELETIVO = "UC_CAM_001_02_04.Associar_Oferta_Processo_Seletivo";

    }

    /// <summary>
	/// Contém a lista de funcionalidades para o caso de uso UC_CAM_001_02_05.
	/// </summary>
    public static class UC_CAM_001_02_05
    {

        /// <summary>
	    /// Este caso de uso tem como objetivo configurar as vagas das ofertas associdas ao processo seletivo.
	    /// </summary>
        public const string CONFIGURAR_VAGAS_OFERTAS_PROCESSO_SELETIVO = "UC_CAM_001_02_05.Configurar_Vagas_Ofertas_Processo_Seletivo";

    }

    /// <summary>
	/// Contém a lista de funcionalidades para o caso de uso UC_CAM_001_03_01.
	/// </summary>
    public static class UC_CAM_001_03_01
    {

        /// <summary>
	    /// Esse caso de uso tem como objetivo permitir a consulta dos dados dos candidatos.
	    /// </summary>
        public const string CONSULTAR_CANDIDATO = "UC_CAM_001_03_01.Consultar_Candidato";

    }

    /// <summary>
	/// Contém a lista de funcionalidades para o caso de uso UC_CAM_001_04_01.
	/// </summary>
    public static class UC_CAM_001_04_01
    {

        /// <summary>
	    /// Esse caso de uso tem como objetivo consultar a posição consolidada dos candidatos.
	    /// </summary>
        public const string CONSULTAR_POSICAO_CONSOLIDADA = "UC_CAM_001_04_01.Consultar_Posicao_Consolidada";

    }

    /// <summary>
	/// Contém a lista de funcionalidades para o caso de uso UC_CAM_001_05_01.
	/// </summary>
    public static class UC_CAM_001_05_01
    {

        /// <summary>
	    /// Este caso de uso tem como objetivo pesquisar as convocações e suas respectivas chamadas, permitindo também a exclusão.
	    /// </summary>
        public const string PESQUISAR_CONVOCACAO = "UC_CAM_001_05_01.Pesquisar_Convocacao";

    }

    /// <summary>
	/// Contém a lista de funcionalidades para o caso de uso UC_CAM_001_05_02.
	/// </summary>
    public static class UC_CAM_001_05_02
    {

        /// <summary>
	    /// Este caso de uso tem como objetivo cadastrar e alterar os dados de uma convocação e de suas respectivas chamadas.
	    /// </summary>
        public const string MANTER_CONVOCACAO = "UC_CAM_001_05_02.Manter_Convocacao";

    }

    /// <summary>
	/// Contém a lista de funcionalidades para o caso de uso UC_CAM_001_05_03.
	/// </summary>
    public static class UC_CAM_001_05_03
    {

        /// <summary>
	    /// Este caso de uso tem como objetivo pesquisar as ofertas de uma convocação, permitindo também a exclusão.
	    /// </summary>
        public const string PESQUISAR_OFERTA_CONVOCACAO = "UC_CAM_001_05_03.Pesquisar_Oferta_Convocacao";

    }

    /// <summary>
	/// Contém a lista de funcionalidades para o caso de uso UC_CAM_001_05_04.
	/// </summary>
    public static class UC_CAM_001_05_04
    {

        /// <summary>
	    /// Este caso de uso tem como objetivo associar e desassociar em lote ofertas de uma convocação.
	    /// </summary>
        public const string ASSOCIAR_OFERTA_CONVOCACAO = "UC_CAM_001_05_04.Associar_Oferta_Convocacao";

    }

    /// <summary>
	/// Contém a lista de funcionalidades para o caso de uso UC_CAM_001_05_05.
	/// </summary>
    public static class UC_CAM_001_05_05
    {

        /// <summary>
	    /// Este caso de uso tem como objetivo configurar as vagas das ofertas associdas à convocação.
	    /// </summary>
        public const string CONFIGURAR_VAGAS_OFERTAS_CONVOCACAO = "UC_CAM_001_05_05.Configurar_Vagas_Ofertas_Convocacao";

    }

    /// <summary>
	/// Contém a lista de funcionalidades para o caso de uso UC_CAM_001_05_06.
	/// </summary>
    public static class UC_CAM_001_05_06
    {

        /// <summary>
	    /// Este caso de uso tem como objetivo cosultar e selecionar candidatos excedentes para a convocação.
	    /// </summary>
        public const string PESQUISAR_CONVOCACAO_EXECENTES = "UC_CAM_001_05_06.Pesquisar_Convocacao_Execentes";

    }

    /// <summary>
	/// Contém a lista de funcionalidades para o caso de uso UC_CAM_001_05_07.
	/// </summary>
    public static class UC_CAM_001_05_07
    {

        /// <summary>
	    /// Esse caso de uso tem como objetivo convocar um candidato.
	    /// </summary>
        public const string PESQUISAR_CONVOCACAO_EXECENTES = "UC_CAM_001_05_07.Pesquisar_Convocacao_Execentes";

    }

    /// <summary>
	/// Contém a lista de funcionalidades para o caso de uso UC_CAM_001_05_08.
	/// </summary>
    public static class UC_CAM_001_05_08
    {

        /// <summary>
	    /// Esse caso de uso tem como objetivo visualizar os impedimentos para liberação de matrícula.
	    /// </summary>
        public const string VISUALIZAR_IMPEDIMENTOS = "UC_CAM_001_05_08.Visualizar_Impedimentos";

    }

    /// <summary>
	/// Contém a lista de funcionalidades para o caso de uso UC_CAM_001_06_01.
	/// </summary>
    public static class UC_CAM_001_06_01
    {

        /// <summary>
	    /// Este caso de uso tem como objetivo pesquisar os tipos de oferta, permitindo também a exclusão.
	    /// </summary>
        public const string PESQUISAR_TIPO_OFERTA = "UC_CAM_001_06_01.Pesquisar_Tipo_Oferta";

    }

    /// <summary>
	/// Contém a lista de funcionalidades para o caso de uso UC_CAM_001_06_02.
	/// </summary>
    public static class UC_CAM_001_06_02
    {

        /// <summary>
	    /// Este caso de uso tem como objetivo cadastrar e alterar os dados de um tipo de oferta.
	    /// </summary>
        public const string MANTER_TIPO_OFERTA = "UC_CAM_001_06_02.Manter_Tipo_Oferta";

    }

    /// <summary>
	/// Contém a lista de funcionalidades para o caso de uso UC_CAM_001_07_01.
	/// </summary>
    public static class UC_CAM_001_07_01
    {

        /// <summary>
	    /// Este caso de uso tem como objetivo pesquisar os tipos de processo seletivo, permitindo também a exclusão.
	    /// </summary>
        public const string PESQUISAR_TIPO_PROCESSO_SELETIVO = "UC_CAM_001_07_01.Pesquisar_Tipo_Processo_Seletivo";

    }

    /// <summary>
	/// Contém a lista de funcionalidades para o caso de uso UC_CAM_001_07_02.
	/// </summary>
    public static class UC_CAM_001_07_02
    {

        /// <summary>
	    /// Este caso de uso tem como objetivo cadastrar e alterar os dados de um tipo de processo seletivo.
	    /// </summary>
        public const string MANTER_TIPO_PROCESSO_SELETIVO = "UC_CAM_001_07_02.Manter_Tipo_Processo_Seletivo";

    }

    /// <summary>
	/// Contém a lista de funcionalidades para o caso de uso UC_CAM_002_01_01.
	/// </summary>
    public static class UC_CAM_002_01_01
    {

        /// <summary>
	    /// Este caso de uso tem como objetivo consultar os ciclos letivos, permitindo também a exclusão.
	    /// </summary>
        public const string PESQUISAR_CICLO_LETIVO = "UC_CAM_002_01_01.Pesquisar_Ciclo_Letivo";

    }

    /// <summary>
	/// Contém a lista de funcionalidades para o caso de uso UC_CAM_002_01_02.
	/// </summary>
    public static class UC_CAM_002_01_02
    {

        /// <summary>
	    /// Este caso de uso tem como objetivo cadastrar ou alterar os dados de um ciclo letivo.
	    /// </summary>
        public const string MANTER_CICLO_LETIVO = "UC_CAM_002_01_02.Manter_Ciclo_Letivo";

    }

    /// <summary>
	/// Contém a lista de funcionalidades para o caso de uso UC_CAM_002_01_03.
	/// </summary>
    public static class UC_CAM_002_01_03
    {

        /// <summary>
	    /// Este caso de uso tem como objetivo consultar a parametrização de tipo de evento letivo, permitindo também a exclusão.
	    /// </summary>
        public const string PESQUISAR_PARAMETRIZACAO_TIPO_EVENTO_LETIVO = "UC_CAM_002_01_03.Pesquisar_Parametrizacao_Tipo_Evento_Letivo";

    }

    /// <summary>
	/// Contém a lista de funcionalidades para o caso de uso UC_CAM_002_01_04.
	/// </summary>
    public static class UC_CAM_002_01_04
    {

        /// <summary>
	    /// Este caso de uso tem como objetivo cadastrar ou alterar os dados de uma parametrização de tipo de evento letivo.
	    /// </summary>
        public const string MANTER_PARAMETRIZACAO_TIPO_EVENTO_LETIVO = "UC_CAM_002_01_04.Manter_Parametrizacao_Tipo_Evento_Letivo";

    }

    /// <summary>
	/// Contém a lista de funcionalidades para o caso de uso UC_CAM_002_01_05.
	/// </summary>
    public static class UC_CAM_002_01_05
    {

        /// <summary>
	    /// Este caso de uso tem como objetivo cadastrar um ciclo letivo através da cópia dos dados de um ciclo letivo já existente.   
	    /// </summary>
        public const string COPIAR_CICLO_LETIVO = "UC_CAM_002_01_05.Copiar_Ciclo_Letivo";

    }

    /// <summary>
	/// Contém a lista de funcionalidades para o caso de uso UC_CAM_002_02_01.
	/// </summary>
    public static class UC_CAM_002_02_01
    {

        /// <summary>
	    /// Este caso de uso tem como objetivo manter o cadastro de regime letivo, permitindo todas as funcionalidades de um CRUD básico (consulta, cadastro, alteração e exclusão)    É um cadastro dynamic.
	    /// </summary>
        public const string MANTER_REGIME_LETIVO = "UC_CAM_002_02_01.Manter_Regime_Letivo";

    }

    /// <summary>
	/// Contém a lista de funcionalidades para o caso de uso UC_CAM_002_03_01.
	/// </summary>
    public static class UC_CAM_002_03_01
    {

        /// <summary>
	    /// Este caso de uso tem como objetivo consultar os eventos letivos de um ciclo letivo, permitindo também a exclusão.
	    /// </summary>
        public const string PESQUISAR_EVENTO_LETIVO = "UC_CAM_002_03_01.Pesquisar_Evento_Letivo";

    }

    /// <summary>
	/// Contém a lista de funcionalidades para o caso de uso UC_CAM_002_03_02.
	/// </summary>
    public static class UC_CAM_002_03_02
    {

        /// <summary>
	    /// Este caso de uso tem como objetivo cadastrar ou alterar os dados de um evento letivo do ciclo letivo.
	    /// </summary>
        public const string MANTER_EVENTO_LETIVO = "UC_CAM_002_03_02.Manter_Evento_Letivo";

    }

    /// <summary>
	/// Contém a lista de funcionalidades para o caso de uso UC_CAM_002_03_03.
	/// </summary>
    public static class UC_CAM_002_03_03
    {

        /// <summary>
	    /// Este caso de uso tem como objetivo copiar eventos letivos de um ciclo letivo para outro ciclo letivo
	    /// </summary>
        public const string COPIAR_EVENTO_LETIVO = "UC_CAM_002_03_03.Copiar_Evento_Letivo";

    }

    /// <summary>
	/// Contém a lista de funcionalidades para o caso de uso UC_CAM_002_03_04.
	/// </summary>
    public static class UC_CAM_002_03_04
    {

        /// <summary>
	    /// Este caso de uso tem como objetivo alterar eventos letivos em lote.
	    /// </summary>
        public const string ALTERAR_EVENTO_LETIVO_LOTE = "UC_CAM_002_03_04.Alterar_Evento_Letivo_Lote";

    }

    /// <summary>
	/// Contém a lista de funcionalidades para o caso de uso UC_CAM_002_03_05.
	/// </summary>
    public static class UC_CAM_002_03_05
    {

        /// <summary>
	    /// Este caso de uso tem como objetivo visualizar os detalhes de um evento letivo.
	    /// </summary>
        public const string VISUALIZAR_EVENTO_LETIVO = "UC_CAM_002_03_05.Visualizar_Evento_Letivo";

    }

    /// <summary>
	/// Contém a lista de funcionalidades para o caso de uso UC_CAM_003_01_01.
	/// </summary>
    public static class UC_CAM_003_01_01
    {

        /// <summary>
	    /// Este caso de uso tem como objetivo manter o cadastro de regime letivo por instituição e nível, permitindo todas as funcionalidades de um CRUD básico (consulta, cadastro, alteração e exclusão)    É um cadastro dynamic.
	    /// </summary>
        public const string MANTER_REGIME_LETIVO_INSTITUICAO_NIVEL = "UC_CAM_003_01_01.Manter_Regime_Letivo_Instituicao_Nivel";

    }

    /// <summary>
	/// Contém a lista de funcionalidades para o caso de uso UC_CAM_003_02_01.
	/// </summary>
    public static class UC_CAM_003_02_01
    {

        /// <summary>
	    /// Este caso de uso tem como objetivo consultar os tipos de evento por instituição, permitindo também a exclusão.  
	    /// </summary>
        public const string PESQUISAR_PARAMETROS_INSTITUICAO_TIPO_EVENTO = "UC_CAM_003_02_01.Pesquisar_Parametros_Instituicao_Tipo_Evento";

    }

    /// <summary>
	/// Contém a lista de funcionalidades para o caso de uso UC_CAM_003_02_02.
	/// </summary>
    public static class UC_CAM_003_02_02
    {

        /// <summary>
	    /// Este caso de uso tem como objetivo cadastrar ou alterar os tipos de evento por instituição
	    /// </summary>
        public const string MANTER_PARAMETROS_INSTITUICAO_TIPO_EVENTO = "UC_CAM_003_02_02.Manter_Parametros_Instituicao_Tipo_Evento";

    }

}

