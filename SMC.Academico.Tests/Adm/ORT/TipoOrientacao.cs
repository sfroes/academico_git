using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using ExpectedConditions = SeleniumExtras.WaitHelpers.ExpectedConditions;
using SMC.Framework.Test; //Framework de testes
using System.Threading;
using Xunit; //Usado no Fact
using System;

namespace SMC.Academico.Tests.ADM.ORT
{
    public class TipoOrientacao : SMCSeleniumUnitTest
    {
        // private bool phantomJS = false;

        [Fact]
        public void TipoOrientacao_1_Inserir()
        {
            base.ExecuteTest((driver) =>
            {
                //Maximiza o Browser
                // driverNavigator.Manage().Window.Maximize();

                Login(driver);
                //-------------------------------------------------------------------------------------------------
                driver.FindElement(By.Id("BotaoNovoVP")).Click();
                driver.FindElement(By.Name("Descricao")).Click();
                driver.FindElement(By.Name("Descricao")).Clear();
                driver.FindElement(By.Name("Descricao")).SendKeys("Tipo de Orientação Automatizado");
                driver.FindElement(By.Name("Token")).Click();
                driver.FindElement(By.Name("Token")).Clear();
                driver.FindElement(By.Name("Token")).SendKeys("TIPO_ORIENTACAO_AUT");
                driver.FindElement(By.Name("TrabalhoConclusaoCurso")).Click();
                driver.FindElement(By.Name("PermiteInclusaoManual")).Click();
                driver.FindElement(By.Name("PermiteManutencaoManual")).Click();
                driver.FindElement(By.Name("OrientacaoTurma")).Click();
                driver.FindElement(By.Name("NumeroPrioridadeChancelaMatricula")).Click();
                driver.FindElement(By.Name("NumeroPrioridadeChancelaMatricula")).Clear();
                driver.FindElement(By.Name("NumeroPrioridadeChancelaMatricula")).SendKeys("0");
                driver.FindElement(By.Id("btnSalvarVPIN")).Click();
                //Checando mensagem de sucesso
                Assert.True(CheckSuccess(), "Era esperado sucesso e ocorreu um erro");

                //Para fechar o Chrome em segundo plano
                driver.Driver.Close();
            });
        }

        [Fact]
        public void TipoOrientacao_2_Inserir_CamposObrigatorios()
        {
            base.ExecuteTest((driver) =>
            {
                //Maximiza o Browser
                // driverNavigator.Manage().Window.Maximize();

                Login(driver);
                //-------------------------------------------------------------------------------------------------
                driver.FindElement(By.Id("BotaoNovoVP")).Click();
                driver.FindElement(By.Id("btnSalvarVPIN")).Click();
                //Checando mensagem de preenchimento obrigatório
                driver.FindElement(By.Name("Descricao")).CheckErrorMessage("Preenchimento obrigatório");
                driver.FindElement(By.Name("Token")).CheckErrorMessage("Preenchimento obrigatório");

                //Para fechar o Chrome em segundo plano
                driver.Driver.Close();
            });
        }

        [Fact]
        public void TipoOrientacao_3_Alterar()
        {
            base.ExecuteTest((driver) =>
            {
                Login(driver);
                //-----------------------------------------------------------------------------------------
                //colar o script
                driver.FindElement(By.Name("Descricao")).Click();
                driver.FindElement(By.Name("Descricao")).Clear();
                driver.FindElement(By.Name("Descricao")).SendKeys("Tipo de Orientação Automatizado");
                driver.FindElement(By.Id("BotaoPesquisarVP")).Click();
                driver.FindElement(By.Id("DynamicBotaoEdit0")).Click();
                driver.FindElement(By.Name("Descricao")).Click();
                
                driver.FindElement(By.Name("Descricao")).Clear();
                driver.FindElement(By.Name("Descricao")).SendKeys("Tipo de Orientação alterado pela automação");
                driver.FindElement(By.Name("Token")).Click();
                driver.FindElement(By.Name("Token")).Clear();
                driver.FindElement(By.Name("Token")).SendKeys("ORIENT_PROV_AUTOMACAO");

                //Para selecionar a opção Não
                driver.FindElement(By.CssSelector("[name='TrabalhoConclusaoCurso'][title='Não']")).Click();
                driver.FindElement(By.CssSelector("[name='PermiteInclusaoManual'][title='Não']")).Click();
                driver.FindElement(By.CssSelector("[name='PermiteManutencaoManual'][title='Não']")).Click();
                driver.FindElement(By.CssSelector("[name='OrientacaoTurma'][title='Não']")).Click();
                
                driver.FindElement(By.Name("NumeroPrioridadeChancelaMatricula")).Click();
                driver.FindElement(By.Name("NumeroPrioridadeChancelaMatricula")).Clear();
                driver.FindElement(By.Name("NumeroPrioridadeChancelaMatricula")).SendKeys("1");
                driver.FindElement(By.Id("btnSalvarVPIE")).Click();
             
                //Checando mensagem de sucesso
                Assert.True(CheckSuccess(), "Era esperado sucesso e ocorreu um erro");

                //Para fechar o Chrome em segundo plano
                driver.Driver.Close();
            });
        }

        [Fact]
        public void TipoOrientacao_4_ExcluisaoNaoPermitida_Jaexisteorientacao()
        {
            base.ExecuteTest((driver) =>
            {
                Login(driver);
                //----------------------------------------------
                //colar o script
                driver.FindElement(By.Name("Descricao")).Click();
                driver.FindElement(By.Name("Descricao")).Clear();
                driver.FindElement(By.Name("Descricao")).SendKeys("Orientação em Intercâmbio");
                driver.FindElement(By.Id("BotaoPesquisarVP")).Click();
                driver.FindElement(By.Id("DynamicBotaoExcluir1")).Click();
                                
                driver.FindElement(By.Id("BotaoPadraoPerguntaSim")).Click();

                //Checando mensagem de sucesso
                Assert.True(CheckMessage("Exclusão não permitida. Já existe orientação deste tipo de orientação."), "Era esperado sucesso e ocorreu um erro");

                //Para fechar o Chrome em segundo plano
                driver.Driver.Close();
            });
        }

        [Fact]
        public void TipoOrientacao_5_ExcluisaoNaoPermitida_UtilizadoTipoDivisaoCompCurricular()
        {
            base.ExecuteTest((driver) =>
            {
                Login(driver);
                //----------------------------------------------
                //colar o script
                driver.FindElement(By.Name("Descricao")).Click();
                driver.FindElement(By.Name("Descricao")).Clear();
                driver.FindElement(By.Name("Descricao")).SendKeys("Orientação de Turma");
                driver.FindElement(By.Id("BotaoPesquisarVP")).Click();
                driver.FindElement(By.Id("DynamicBotaoExcluir1")).Click();

                driver.FindElement(By.Id("BotaoPadraoPerguntaSim")).Click();

                //Checando mensagem de sucesso
                Assert.True(CheckMessage("Exclusão não permitida. Tipo de orientação já foi utilizado no cadastro de tipo de divisão de componente curricular."), "Era esperado sucesso e ocorreu um erro");

                //Para fechar o Chrome em segundo plano
                driver.Driver.Close();
            });
        }


        [Fact]
        public void TipoOrientacao_6_Excluir()
        {
            base.ExecuteTest((driver) =>
            {
                Login(driver);
                //----------------------------------------------
                //colar o script
                driver.FindElement(By.Name("Descricao")).Click();
                driver.FindElement(By.Name("Descricao")).Clear();
                driver.FindElement(By.Name("Descricao")).SendKeys("Tipo de Orientação alterado pela automação");
                driver.FindElement(By.Id("BotaoPesquisarVP")).Click();
                driver.FindElement(By.Id("DynamicBotaoExcluir1")).Click();

                driver.FindElement(By.Id("BotaoPadraoPerguntaSim")).Click();

                //Checando mensagem de sucesso
                Assert.True(CheckSuccess(), "Era esperado sucesso e ocorreu um erro");

                //Para fechar o Chrome em segundo plano
                driver.Driver.Close();
            });
        }
        [Fact]
        [Trait("Ordenado", "CRUD")]
        public void TesteOrdenadoCRUD()
        {
            TipoOrientacao_1_Inserir();
            TipoOrientacao_2_Inserir_CamposObrigatorios();
            TipoOrientacao_3_Alterar();
            TipoOrientacao_4_ExcluisaoNaoPermitida_Jaexisteorientacao();
            TipoOrientacao_5_ExcluisaoNaoPermitida_UtilizadoTipoDivisaoCompCurricular();
            TipoOrientacao_6_Excluir();
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
            wait.Until(ExpectedConditions.TextToBePresentInElementLocated(By.XPath("//div[@id='divConteudoPrincipal']/section/h2"), "SGA.Administrativo"));
            driver.GoToUrl2(Consts.SERVIDOR_QUALIDADE_ADM + "/ORT/TipoOrientacao");
            //----------------------------------------------------
        }
     
    }
}
