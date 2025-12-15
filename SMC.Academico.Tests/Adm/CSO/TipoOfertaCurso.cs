using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using ExpectedConditions = SeleniumExtras.WaitHelpers.ExpectedConditions;
using SMC.Framework.Test; //Framework de testes
using System.Threading;
using System;
using Xunit; //Usado no Fact


namespace SMC.Academico.Tests.ADM.CSO //Arvore onde esta o arquivo
{

    public class TipoOfertaCurso : SMCSeleniumUnitTest //Declara o nome da classe e recebe a SMCSeleniumUnitTest
    {
        [Fact] //Deve ser declarada para teste unitario
        public void TipoOfertaCurso_Inclusao() //Colocar o nome da tela a ser testada
        {
            base.ExecuteTest((driver) => //chama o browser e coloca o link correto
            {
                //Maximiza o Browser
                // driverNavigator.Manage().Window.Maximize();
                Login(driver); // realiza o login como administrador

                //Colar o script aqui
                driver.FindElement(By.Id("BotaoNovoVP")).Click();
                driver.FindElement(By.Name("Descricao")).Click();
                driver.FindElement(By.Name("Descricao")).Clear();
                driver.FindElement(By.Name("Descricao")).SendKeys("INT II AUTOMAÇÃO");
                driver.FindElement(By.Name("Token")).Click();
                driver.FindElement(By.Name("Token")).Clear();
                driver.FindElement(By.Name("Token")).SendKeys("B30");
                driver.FindElement(By.Id("btnSalvarVPIN")).Click();
                Assert.True(CheckSuccess(), "Era esperado sucesso e ocorreu um erro");
                //Assert.True(CheckMessage("Tipo de Oferta de curso incluído com sucesso."), "Era esperado sucesso e ocorreu um erro");

            });
        }

        [Fact] //Deve ser declarada para teste unitario
        public void TipoOfertaCurso_Alteracao() //Colocar o nome da tela a ser testada
        {
            base.ExecuteTest((driver) => //chama o browser e coloca o link correto
            {
                //Maximiza o Browser
                // driverNavigator.Manage().Window.Maximize();
                Login(driver); // realiza o login como administrador

                //Colar o script aqui
                driver.FindElement(By.Name("Descricao")).Click();
                driver.FindElement(By.Name("Descricao")).Clear();
                driver.FindElement(By.Name("Descricao")).SendKeys("Int II AUTOMAÇÃO");
                driver.FindElement(By.Id("BotaoPesquisarVP")).Click();
                driver.FindElement(By.XPath("//button[@id='DynamicBotaoEdit0']/i")).Click();
                driver.FindElement(By.Name("Token")).Click();
                driver.FindElement(By.Name("Token")).Click();
                driver.FindElement(By.Name("Token")).Clear();
                driver.FindElement(By.Name("Token")).SendKeys("B31");
                driver.FindElement(By.XPath("//button[@id='btnSalvarVPIE']/i")).Click();
                //Assert.True(CheckMessage("Tipo de Oferta de curso alterado com sucesso."), "Era esperado sucesso e ocorreu um erro");
                Assert.True(CheckSuccess(), "Era esperado sucesso e ocorreu um erro");

                //Para fechar o Chrome em segundo plano
                driver.Driver.Close();
            });
        }


        [Fact] //Deve ser declarada para teste unitario
        public void TipoOfertaCurso_InclusaoDuplicada() //Colocar o nome da tela a ser testada
        {
            base.ExecuteTest((driver) => //chama o browser e coloca o link correto
            {
                //Maximiza o Browser
                // driverNavigator.Manage().Window.Maximize();
                Login(driver); // realiza o login como administrador

                //Colar o script aqui
                driver.FindElement(By.Id("BotaoNovoVP")).Click();
                driver.FindElement(By.Name("Descricao")).Click();
                driver.FindElement(By.Name("Descricao")).Clear();
                driver.FindElement(By.Name("Descricao")).SendKeys("Int II AUTOMAÇÃO");
                driver.FindElement(By.Name("Token")).Click();
                driver.FindElement(By.Name("Token")).Clear();
                driver.FindElement(By.Name("Token")).SendKeys("A24");
                driver.FindElement(By.Id("btnSalvarVPIN")).Click();
                Assert.True(CheckMessage("Já existe tipo de oferta de curso cadastrado com esta descrição."), "Era esperado sucesso e ocorreu um erro");
                // Assert.True(CheckSuccess(), "Era esperado sucesso e ocorreu um erro");

                //Para fechar o Chrome em segundo plano
                driver.Driver.Close();
            });
        }



        [Fact] //Deve ser declarada para teste unitario
        public void TipoOfertaCursoExclusaonaopermitida() //Colocar o nome da tela a ser testada
        {
            base.ExecuteTest((driver) => //chama o browser e coloca o link correto
            {
                //Maximiza o Browser
                // driverNavigator.Manage().Window.Maximize();
                Login(driver); // realiza o login como administrador

                //Colar o script aqui
                driver.FindElement(By.Name("Descricao")).Click();
                driver.FindElement(By.Name("Descricao")).Clear();
                driver.FindElement(By.Name("Descricao")).SendKeys("Interno");
                driver.FindElement(By.Id("BotaoPesquisarVP")).Click();
                driver.FindElement(By.XPath("//a[@id='DynamicBotaoExcluir1']/i")).Click();
                driver.FindElement(By.Id("BotaoPadraoPerguntaSim")).Click();
                driver.FindElement(By.XPath("//div[@id='centro']/div/div/div[2]")).Click();

                //Assert.True(CheckMessage("Exclusão não permitida.Tipo de oferta de curso já foi associado a uma instituição de ensino x nível de ensino."), "Era esperado sucesso e ocorreu um erro");

                //Declara a variável teste q ira receber o texto do campo
                string
                teste = driver.FindElement(By.XPath("//div[@id='centro']/div/div/div[2]")).GetValue();
                //Compara a mensagem exibida com a esperada:
                Assert.Equal("Exclusão não permitida. Tipo de oferta de curso já foi associado a uma instituição de ensino x nível de ensino.", teste); 
                              

               //Para fechar o Chrome em segundo plano
               driver.Driver.Close();
            });
        }

        [Fact] //Deve ser declarada para teste unitario
        public void TipoOfertaCursoExclusao() //Colocar o nome da tela a ser testada
        {
            base.ExecuteTest((driver) => //chama o browser e coloca o link correto
            {
                //Maximiza o Browser
                // driverNavigator.Manage().Window.Maximize();
                Login(driver); // realiza o login como administrador

                //Colar o script aqui
                driver.FindElement(By.Name("Descricao")).Click();
                driver.FindElement(By.Name("Descricao")).Clear();
                driver.FindElement(By.Name("Descricao")).SendKeys("Int II AUTOMAÇÃO");
                driver.FindElement(By.Id("BotaoPesquisarVP")).Click();
                driver.FindElement(By.XPath("//a[@id='DynamicBotaoExcluir1']/i")).Click();
                driver.FindElement(By.Id("BotaoPadraoPerguntaSim")).Click();
                //Assert.True(CheckMessage("Tipo de oferta de curso excluído com sucesso"), "Era esperado sucesso e ocorreu um erro");
                Assert.True(CheckSuccess(), "Era esperado sucesso e ocorreu um erro");

                //Para fechar o Chrome em segundo plano
                driver.Driver.Close();
            });
        }

    
[Fact]
// [Trait("Ordenado", "CRUD")]

public void TesteOrdenadoCRUD()
{
    TipoOfertaCurso_Inclusao();
    TipoOfertaCurso_Alteracao();
    TipoOfertaCurso_InclusaoDuplicada();
    TipoOfertaCursoExclusaonaopermitida();
    TipoOfertaCursoExclusao();
                    }
private static void Login(ISMCWebDriver driver)
        {
            //----------------------------------------------------
            driver.GoToUrl(Consts.SERVIDOR_QUALIDADE_ADM);
            //força o sistema a esperar 15 minutos ou até que apareça o campo para login
            WebDriverWait wait = new WebDriverWait((OpenQA.Selenium.IWebDriver)driver, TimeSpan.FromSeconds(90));
            wait.Until(e => e.FindElement(By.Name("LoginCpf")));
            driver.SMCLoginCpf();
            //força o sistema a esperar 15 minutos ou até que apareça a home do SGA
            wait.Until(ExpectedConditions.TextToBePresentInElementLocated(By.XPath("//div[@id='divConteudoPrincipal']/section/h2"), "SGA.Administrativo"));
            driver.GoToUrl2(Consts.SERVIDOR_QUALIDADE_ADM + "CSO/TipoOfertaCurso");

        }
    }
}
