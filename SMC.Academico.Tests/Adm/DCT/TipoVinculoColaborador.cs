using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using ExpectedConditions = SeleniumExtras.WaitHelpers.ExpectedConditions;
using SMC.Framework.Test; //Framework de testes
using System.Threading;
using Xunit; //Usado no Fact
using System;

namespace SMC.Academico.Tests.ADM.DCT //Arvore onde esta o arquivo
{
    public class TipoVinculoColaborador : SMCSeleniumUnitTest //Declara o nome da classe e recebe a SMCSeleniumUnitTest
    {
        [Fact] //Deve ser declarada para teste unitario
        public void TipoVinculoColaborador_Inclusao() //Colocar o nome da tela a ser testada
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
                driver.FindElement(By.Name("Descricao")).SendKeys("Pesquisador Permanente");
                driver.FindElement(By.Name("Token")).Click();
                driver.FindElement(By.Name("Token")).Click();
                driver.FindElement(By.Name("Token")).Click();
                driver.FindElement(By.Name("Token")).Clear();
                driver.FindElement(By.Name("Token")).SendKeys("APP");
                driver.FindElement(By.Name("ExigeFormacaoAcademica")).Click();
                driver.FindElement(By.Id("select_CodigoPapeVinculoCad")).Click();
                new SMCSelectElement(driver.FindElement(By.Id("select_CodigoPapeVinculoCad"))).SelectByText("Autônomo");
                driver.FindElement(By.Id("btnSalvarVPIN")).Click();

                //Checando a mensagem de Sucesso (compara a mensagem que está descrita com a que vai aparecer no aplicativo)
                //Assert.True(CheckMessage("Tipo de Vínculo de trabalhador incluído com sucesso."), "Mensagem esperada não exibida");
                Assert.True(CheckMessage("Tipo de vínculo do colaborador incluído com sucesso."), "Mensagem esperada não exibida");
                driver.Driver.Close();
            });
        }
        
        [Fact] //Deve ser declarada para teste unitario
        public void TipoVinculoColaborador_Inclusaoduplicada() //Colocar o nome da tela a ser testada
        
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
                driver.FindElement(By.Name("Descricao")).SendKeys("Pesquisador Permanente");
                driver.FindElement(By.Name("Token")).Click();
                driver.FindElement(By.Name("Token")).Click();
                driver.FindElement(By.Name("Token")).Click();
                driver.FindElement(By.Name("Token")).Clear();
                driver.FindElement(By.Name("Token")).SendKeys("APP");
                driver.FindElement(By.Name("ExigeFormacaoAcademica")).Click();
                driver.FindElement(By.Id("select_CodigoPapeVinculoCad")).Click();
                new SMCSelectElement(driver.FindElement(By.Id("select_CodigoPapeVinculoCad"))).SelectByText("Autônomo");
                driver.FindElement(By.Id("btnSalvarVPIN")).Click();

                //Checando a mensagem de Sucesso (compara a mensagem que está descrita com a que vai aparecer no aplicativo)
            Assert.True(CheckMessage("Já existe tipo de vinculo de colaborador cadastrado com esta descrição."), "Mensagem esperada não exibida");
                 driver.Driver.Close();



                });
        }

        [Fact] //Deve ser declarada para teste unitario
        public void TipoVinculoColaborador_Alteracao() //Colocar o nome da tela a ser testada

        {
            base.ExecuteTest((driver) => //chama o browser e coloca o link correto
            {
                //Maximiza o Browser
                // driverNavigator.Manage().Window.Maximize();
                Login(driver); // realiza o login como administrador

                //Colar o script aqui

                driver.FindElement(By.Name("Descricao")).Clear();
                driver.FindElement(By.Name("Descricao")).SendKeys("pesquisador permanente");
                driver.FindElement(By.Id("BotaoPesquisarVP")).Click();
                driver.FindElement(By.XPath("//button[@id='DynamicBotaoEdit0']/i")).Click();
                driver.FindElement(By.Name("Descricao")).Click();
                driver.FindElement(By.Name("Descricao")).Clear();
                driver.FindElement(By.Name("Descricao")).SendKeys("Pesquisador Permanente II");
                driver.FindElement(By.Id("btnSalvarVPIE")).Click();

                //Checando a mensagem de Sucesso (compara a mensagem que está descrita com a que vai aparecer no aplicativo)
                //Assert.True(CheckMessage("Tipo de Vínculo de trabalhador alterado com sucesso."), "Mensagem esperada não exibida");
                Assert.True(CheckMessage("Tipo de vínculo do colaborador alterado com sucesso."), "Messagem esperada não exibida");
                driver.Driver.Close();
            });
        }


        [Fact] //Deve ser declarada para teste unitario
        public void TipoVinculoColaborador_Exclusaonaopermitida() //Colocar o nome da tela a ser testada

        {
            base.ExecuteTest((driver) => //chama o browser e coloca o link correto
            {
            //Maximiza o Browser
            // driverNavigator.Manage().Window.Maximize();
            Login(driver); // realiza o login como administrador

            //Colar o script aqui

            driver.FindElement(By.Id("BotaoPesquisarVP")).Click();
            driver.FindElement(By.Name("Descricao")).Click();
            driver.FindElement(By.Name("Descricao")).Clear();
            driver.FindElement(By.Name("Descricao")).SendKeys("docente permanente");
            driver.FindElement(By.Id("BotaoPesquisarVP")).Click();
            driver.FindElement(By.XPath("//a[@id='DynamicBotaoExcluir1']/i")).Click();
            driver.FindElement(By.Id("BotaoPadraoPerguntaSim")).Click();

            //Checando a mensagem de Sucesso (compara a mensagem que está descrita com a que vai aparecer no aplicativo)
            //Assert.True(CheckMessage("Exclusão não permitida.Já existe vínculo de colaborador cadastrado com este tipo de vínculo"), "Mensagem esperada não exibida");
            //Assert.True(CheckMessage("Exclusão não permitida. Já existe vínculo de colaborador cadastrado com este tipo de vínculo."), "Mensagem esperada não exibida");
            Assert.True(CheckMessage("Exclusão não permitida."), "Mensagem esperada não exibida.");
            driver.Driver.Close();


            });
        }

        [Fact] //Deve ser declarada para teste unitario
        public void TipoVinculoColaborador_Exclusao() //Colocar o nome da tela a ser testada

        {
            base.ExecuteTest((driver) => //chama o browser e coloca o link correto
            {
                //Maximiza o Browser
                // driverNavigator.Manage().Window.Maximize();
                Login(driver); // realiza o login como administrador

                //Colar o script aqui
                                
                driver.FindElement(By.Id("BotaoPesquisarVP")).Click();
                driver.FindElement(By.Name("Descricao")).Click();
                driver.FindElement(By.Name("Descricao")).Clear();
                driver.FindElement(By.Name("Descricao")).SendKeys("Pesquisador Permanente II");
                driver.FindElement(By.XPath("//button[@id='BotaoPesquisarVP']/i")).Click();
                Thread.Sleep(8000);
                driver.FindElement(By.XPath("//a[@id='DynamicBotaoExcluir1']/i")).Click();
                driver.FindElement(By.Id("BotaoPadraoPerguntaSim")).Click();
                Thread.Sleep(8000);

                //
                //Checando a mensagem de Sucesso (compara a mensagem que está descrita com a que vai aparecer no aplicativo)
                //Assert.True(CheckMessage("Tipo de vínculo de colaborador excluído com sucesso"), "Mensagem esperada não exibida");
                
                //Declara a variável teste q ira receber o texto do campo
                string
                teste = driver.FindElement(By.XPath("//div[@id='centro']/div/div/div[2]")).GetValue();
                
                //Compara a mensagem exibida com a esperada:
                Assert.Equal("Tipo de vínculo do colaborador excluído com sucesso.", teste);
                driver.Driver.Close();
            });
        }

        [Fact]
        [Trait("Ordenado", "CRUD")]
        public void TesteOrdenadoCRUD()
        {
            TipoVinculoColaborador_Inclusao();
            TipoVinculoColaborador_Inclusaoduplicada();
            TipoVinculoColaborador_Alteracao();
            TipoVinculoColaborador_Exclusaonaopermitida();
            TipoVinculoColaborador_Exclusao();

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

            driver.GoToUrl2(Consts.SERVIDOR_QUALIDADE_ADM + "DCT/TipoVinculoColaborador"); //coloca o resto do endereço para acessar a pagina do teste

        }
    }
}


