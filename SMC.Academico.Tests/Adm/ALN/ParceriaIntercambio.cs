using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using ExpectedConditions = SeleniumExtras.WaitHelpers.ExpectedConditions;
using SMC.Framework.Test;
using System;
using System.Threading;
using Xunit;

namespace SMC.Academico.Tests.ADM.ALN
{
    public class ParceriaIntercambio : SMCSeleniumUnitTest
    {
        [Fact]
        [Trait("Cenário", "Administrativo - Incluir Parceria de Intercâmbio")]

        public void ParceriaIntercambio_1_Inserir()
        {            
            base.ExecuteTest((driver) =>
            {
                //Maximiza o Browser
                // driverNavigator.Manage().Window.Maximize();

                Login(driver);
                //-------------------------------------------------------------------------------------------------

                driver.FindElement(By.Id("BotaoNovoVP")).Click();
                driver.FindElement(By.Name("Descricao")).Clear();
                driver.FindElement(By.Name("Descricao")).SendKeys("Teste QA Parceria Intercâmbio Incluir");
                driver.FindElement(By.Id("select_TipoParceriaIntercambio")).Click();
                new SMCSelectElement(driver.FindElement(By.Id("select_TipoParceriaIntercambio"))).SelectByText("Acordo");
                driver.FindElement(By.Id("select_TipoParceriaIntercambio")).Click();
                driver.FindElement(By.Id("select_ProcessoNegociacao")).Click();
                new SMCSelectElement(driver.FindElement(By.Id("select_ProcessoNegociacao"))).SelectByText("Sim");
                driver.FindElement(By.Id("select_ProcessoNegociacao")).Click();
               

                /* Salvar sem informar data início
                driver.FindElement(By.Id("BotaoSalvarTemplate")).Click();
                // driver.FindElement(By.Id("ParceriaIntercambioVigenciaViewModel_DetailFormBotaoIncluir")).Click();
                driver.FindElement(By.Name("DataInicio")).CheckErrorMessage("Preenchimento obrigatório");*/
                // Preenche a data de início (informando apenas data inicio) e continuar
                var primeiroDiaMes = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1);
                driver.FindElement(By.Id("VigenciasDetailPartialInserir")).Click();
                driver.FindElement(By.Name("DataInicio")).Click();
                driver.FindElement(By.Name("DataInicio")).Clear();
                driver.FindElement(By.Name("DataInicio")).SendKeys(primeiroDiaMes.ToString("dd/MM/yyyy"));
                driver.FindElement(By.Id("Vigencias_DetailFormBotaoIncluir")).Click();
                
                //Selecionar tipos de termo de intercâmbio.
                driver.FindElement(By.Id("select_TiposTermo_0__SeqTipoTermoIntercambio")).Click();
                new SMCSelectElement(driver.FindElement(By.Id("select_TiposTermo_0__SeqTipoTermoIntercambio"))).SelectByText("Cotutela");
                driver.FindElement(By.Id("select_TiposTermo_0__SeqTipoTermoIntercambio")).Click();
                driver.FindElement(By.Name("TiposTermo[0].Ativo")).Click();
                driver.FindElement(By.Id("TiposTermo_DetailBotaoInserirElemento")).Click();
                driver.FindElement(By.Id("select_TiposTermo_1__SeqTipoTermoIntercambio")).Click();
                new SMCSelectElement(driver.FindElement(By.Id("select_TiposTermo_1__SeqTipoTermoIntercambio"))).SelectByText("Sanduiche");
                driver.FindElement(By.Id("select_TiposTermo_1__SeqTipoTermoIntercambio")).Click();
                driver.FindElement(By.Name("TiposTermo[1].Ativo")).Click();

                driver.FindElement(By.Id("TiposTermo_DetailBotaoInserirElemento")).Click();
                driver.FindElement(By.Id("select_TiposTermo_2__SeqTipoTermoIntercambio")).Click();
                new SMCSelectElement(driver.FindElement(By.Id("select_TiposTermo_2__SeqTipoTermoIntercambio"))).SelectByText("Intercâmbio");
                driver.FindElement(By.Id("select_TiposTermo_2__SeqTipoTermoIntercambio")).Click();
                driver.FindElement(By.Name("TiposTermo[2].Ativo")).Click();


                //Incluir Instituição.
                //Primeira Instituição.
                driver.FindElement(By.Id("InstituicoesExternas_0__SeqInstituicaoExterna_botao_modal")).Click();
                driver.FindElement(By.Name("Nome")).Click();
                driver.FindElement(By.Name("Nome")).Clear();
                driver.FindElement(By.Name("Nome")).SendKeys("ABEU - CENTRO UNIVERSITÁRIO");
                driver.FindElement(By.Id("DataSelectorPesquisar0")).Click();
                driver.FindElement(By.Id("DataSelector_gridDataSelector0")).Click();
                driver.FindElement(By.Id("smc-dataselector-SeqInstituicaoExterna")).Click();
                driver.FindElement(By.Name("InstituicoesExternas[0].Ativo")).Click();
                //Segunda Intituição.
                driver.FindElement(By.Id("InstituicoesExternas_DetailBotaoInserirElemento")).Click();
                driver.FindElement(By.Id("InstituicoesExternas_1__SeqInstituicaoExterna_botao_modal")).Click();
                driver.FindElement(By.Name("Nome")).Click();
                driver.FindElement(By.Name("Nome")).Clear();
                driver.FindElement(By.Name("Nome")).SendKeys("Academia Militar das Agulhas Negras");
                driver.FindElement(By.Id("DataSelectorPesquisar0")).Click();
                driver.FindElement(By.Id("DataSelector_gridDataSelector0")).Click();
                driver.FindElement(By.Id("smc-dataselector-SeqInstituicaoExterna")).Click();
                driver.FindElement(By.Name("InstituicoesExternas[1].Ativo")).Click();
                
                //Salvar inclusão.
                driver.FindElement(By.Id("BotaoSalvarTemplate")).Click();

                //Checando a mensagem de Sucesso
                Assert.True(CheckMessage("Parceria de intercâmbio incluído com sucesso."), "Mensagem esperada não exibida");

                //Para fechar o Chrome em segundo plano
                driver.Driver.Close();

            });

        }

        [Fact]
        [Trait("Cenário", "Administrativo - Alterar Parceria de Intercâmbio")]

        public void ParceriaIntercambio_2_Alterar()
        {
            base.ExecuteTest((driver) =>
            {
                //Maximiza o Browser
                // driverNavigator.Manage().Window.Maximize();

                Login(driver);
                //-------------------------------------------------------------------------------------------------

                //Pesquisar Parceria de Intercâmbio
                driver.FindElement(By.Name("Descricao")).Click();
                driver.FindElement(By.Name("Descricao")).Clear();
                driver.FindElement(By.Name("Descricao")).SendKeys("Teste QA Parceria Intercâmbio Incluir");
                driver.FindElement(By.Id("BotaoPesquisarVP")).Click();

                driver.FindElement(By.Id("DetailListBotaoAlterar0")).Click();

                //Alterar Descrição.
                driver.FindElement(By.Name("Descricao")).Click();
                driver.FindElement(By.Name("Descricao")).Clear();
                driver.FindElement(By.Name("Descricao")).SendKeys("Teste QA Parceria Intercâmbio Alterar");

                //Alterar Tipo de parceria.
                driver.FindElement(By.Id("select_TipoParceriaIntercambio")).Click();
                new SMCSelectElement(driver.FindElement(By.Id("select_TipoParceriaIntercambio"))).SelectByText("Convênio");
                driver.FindElement(By.Id("select_TipoParceriaIntercambio")).Click();
                
                //Alterar Processo de renovação.
                driver.FindElement(By.Id("select_ProcessoNegociacao")).Click();
                new SMCSelectElement(driver.FindElement(By.Id("select_ProcessoNegociacao"))).SelectByText("Não");
                driver.FindElement(By.Id("select_ProcessoNegociacao")).Click();

                //Alterar Períodos de vigência.
                //driver.FindElement(By.Id("VigenciasDetailPartialInserir")).Click();
                driver.FindElement(By.Id("Vigencias_MasterDetailBotaoEditModal0")).Click();
                var primeiroDiaMes = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1);
                var ultimoDiaMes = primeiroDiaMes.AddMonths(1).AddDays(-1);
                driver.FindElement(By.Name("DataInicio")).Click();
                driver.FindElement(By.Name("DataInicio")).Clear();
                driver.FindElement(By.Name("DataInicio")).SendKeys(primeiroDiaMes.ToString("dd/MM/yyyy"));
                driver.FindElement(By.Name("DataFim")).Click();
                driver.FindElement(By.Name("DataFim")).Clear();
                driver.FindElement(By.Name("DataFim")).SendKeys(ultimoDiaMes.ToString("dd/MM/yyyy"));
                driver.FindElement(By.Id("Vigencias_DetailFormBotaoIncluir")).Click();

                //Incluindo segundo período de vigência.
                driver.FindElement(By.Id("VigenciasDetailPartialInserir")).Click();
                var dataInicio = primeiroDiaMes.AddMonths(2);
                var dataFim = ultimoDiaMes.AddMonths(2);
                driver.FindElement(By.Name("DataInicio")).Click();
                driver.FindElement(By.Name("DataInicio")).SendKeys(dataInicio.ToString("dd/MM/yyyy"));
                driver.FindElement(By.Name("DataFim")).Click();
                driver.FindElement(By.Name("DataFim")).SendKeys(dataFim.ToString("dd/MM/yyyy"));
                driver.FindElement(By.Id("Vigencias_DetailFormBotaoIncluir")).Click();

                //Alterar Tipo de Termo de intercâmbio.
                driver.FindElement(By.XPath("(.//*[normalize-space(text()) and normalize-space(.)='Não'])[2]/input[1]")).Click();

                //Excluir Tipo de Termo.
                driver.FindElement(By.XPath("(.//*[normalize-space(text()) and normalize-space(.)='Excluir'])[3]/i[1]")).Click();
                //driver.FindElement(By.Id("DetailListBotaoExcluir0")).Click();

                //Alterar Instituição de Ensino.
                driver.FindElement(By.Id("InstituicoesExternas_0__SeqInstituicaoExterna_botao_modal")).Click();
                driver.FindElement(By.Name("Nome")).Click();
                driver.FindElement(By.Name("Nome")).Clear();
                driver.FindElement(By.Name("Nome")).SendKeys("ACADEMIA NACIONAL DE POLÍCIA - ANP");
                driver.FindElement(By.Id("DataSelectorPesquisar0")).Click();
                driver.FindElement(By.Id("DataSelector_gridDataSelector0")).Click();
                driver.FindElement(By.Id("smc-dataselector-SeqInstituicaoExterna")).Click();              
                driver.FindElement(By.XPath("(.//*[normalize-space(text()) and normalize-space(.)='Não'])[4]/input[1]")).Click();

                //Excluir Instituição de Ensino.            
                driver.FindElement(By.Id("InstituicoesExternas_DetailBotaoExcluirElemento1")).Click();

                //Salvar alterações.
                driver.FindElement(By.Id("BotaoSalvarTemplate")).Click();

                //Checando a mensagem de Sucesso 
                Assert.True(CheckMessage("Parceria de intercâmbio alterado com sucesso."), "Mensagem esperada não exibida");

                //Para fechar o Chrome em segundo plano
                driver.Driver.Close();
            });

        }

        [Fact]
        [Trait("Cenário", "Administrativo - Excluir Parceria de Intercâmbio")]

        public void ParceriaIntercambio_3_Excluir()
        {
            base.ExecuteTest((driver) =>
            {
                //Maximiza o Browser
                // driverNavigator.Manage().Window.Maximize();

                Login(driver);
                //-------------------------------------------------------------------------------------------------

                //Pesquisar Parceria de Intercâmbio
                driver.FindElement(By.Name("Descricao")).Click();
                driver.FindElement(By.Name("Descricao")).Clear();
                driver.FindElement(By.Name("Descricao")).SendKeys("Teste QA Parceria Intercâmbio Alterar");
                driver.FindElement(By.Id("BotaoPesquisarVP")).Click();
                driver.FindElement(By.Id("DetailListBotaoExcluir0")).Click();
                driver.FindElement(By.Id("BotaoPadraoPerguntaSim")).Click();


                //Checando a mensagem de Sucesso
                Assert.True(CheckMessage("Parceria de intercâmbio excluído com sucesso."), "Mensagem esperada não exibida");

                //Para fechar o Chrome em segundo plano
                driver.Driver.Close();

            });

        }

        [Fact]
        [Trait("Ordenado", "CRUD")]
        public void TesteOrdenadoCRUD()
        {
            ParceriaIntercambio_1_Inserir();
            ParceriaIntercambio_2_Alterar();
            ParceriaIntercambio_3_Excluir();
         }


        private static void Login(ISMCWebDriver driver)
        {
            //----------------------------------------------------
            driver.GoToUrl(Consts.SERVIDOR_QUALIDADE_ADM);
            //força o sistema a esperar 15 minutos ou até que apareça o campo para login
            WebDriverWait wait = new WebDriverWait((OpenQA.Selenium.IWebDriver)driver, TimeSpan.FromSeconds(900));
            wait.Until(e => e.FindElement(By.Name("LoginCpf")));
            driver.SMCLoginCpf();
            //força o sistema a esperar 15 minutos ou até que apareça a home do SGA
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.TextToBePresentInElementLocated(By.XPath("//div[@id='divConteudoPrincipal']/section/h2"), "SGA.Administrativo"));

            driver.GoToUrl2(Consts.SERVIDOR_QUALIDADE_ADM + "ALN/ParceriaIntercambio");
            //----------------------------------------------------
        }


    }
}
