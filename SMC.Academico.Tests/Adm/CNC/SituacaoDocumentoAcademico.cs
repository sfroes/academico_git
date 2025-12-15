using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SMC.Framework.Test; //Framework de testes
using System;
using Xunit; //Usado no Fact
using ExpectedConditions = SeleniumExtras.WaitHelpers.ExpectedConditions;


namespace SMC.Academico.Tests.ADM.CNC //Arvore onde esta o arquivo
{

    public class SituacaoDocumentoAcademico : SMCSeleniumUnitTest //Declara o nome da classe e recebe a SMCSeleniumUnitTest
    {


        [Fact] //Deve ser declarada para teste unitario
        public void SituacaoDocumentoAcademico_Inclusao() //Colocar o nome da tela a ser testada
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
                driver.FindElement(By.Name("Descricao")).SendKeys("Aguardando Aprovação e Assinatura_Automação");
                driver.FindElement(By.Name("Token")).Click();
                driver.FindElement(By.Name("Token")).Clear();
                driver.FindElement(By.Name("Token")).SendKeys("A25");
                driver.FindElement(By.Id("select_ClasseSituacaoDocumento")).Click();
                new SMCSelectElement(driver.FindElement(By.Id("select_ClasseSituacaoDocumento"))).SelectByText("Válido");
                driver.FindElement(By.Id("divViewEdit")).Click();
                driver.FindElement(By.Name("Ordem")).Clear();
                driver.FindElement(By.Name("Ordem")).SendKeys("99");
                driver.FindElement(By.Id("btnSalvarVPIN")).Click();


                //Checando a mensagem de Sucesso (compara a mensagem que está descrita com a que vai aparecer no aplicativo)
                Assert.True(CheckMessage("Situação do documento acadêmico incluída com sucesso."), "Mensagem esperada não exibida");
                driver.Driver.Close();
            });
        }

        [Fact] //Deve ser declarada para teste unitario
        public void SituacaoDocumentoAcademico_Alteracao() //Colocar o nome da tela a ser testada
        {
            base.ExecuteTest((driver) => //chama o browser e coloca o link correto
            {
                //Maximiza o Browser
                // driverNavigator.Manage().Window.Maximize();
                Login(driver); // realiza o login como administrador

                //Colar o script aqui
                driver.FindElement(By.Name("Descricao")).Click();
                driver.FindElement(By.Name("Descricao")).Clear();
                driver.FindElement(By.Name("Descricao")).SendKeys("Aguardando aprovação e assinatura_Automação");
                driver.FindElement(By.Id("BotaoPesquisarVP")).Click();
                driver.FindElement(By.XPath("//button[@id='DynamicBotaoEdit0']/i")).Click();
                driver.FindElement(By.Id("select_ClasseSituacaoDocumento")).Click();
                driver.FindElement(By.Name("Descricao")).Click();
                driver.FindElement(By.Name("Descricao")).Clear();
                driver.FindElement(By.Name("Descricao")).SendKeys("Aguardando Aprovação e Assinatura_Automação");

                driver.FindElement(By.Id("select_ClasseSituacaoDocumento")).Click();
                new SMCSelectElement(driver.FindElement(By.Id("select_ClasseSituacaoDocumento"))).SelectByText("Emissão em andamento");
                driver.FindElement(By.Id("btnSalvarVPIE")).Click();


                //Checando a mensagem de Sucesso (Verifica se tem a palavra sucesso na mensagem)
                Assert.True(CheckSuccess(), "Era esperado sucesso e ocorreu um erro");
                driver.Driver.Close();


            });

        }

        [Fact] //Deve ser declarada para teste unitario
        public void SituacaoDocumentoAcademico_AlteracaoNaoPermitida() //Colocar o nome da tela a ser testada
        {
            base.ExecuteTest((driver) => //chama o browser e coloca o link correto
            {
                //Maximiza o Browser
                // driverNavigator.Manage().Window.Maximize();
                Login(driver); // realiza o login como administrador

                //Colar o script aqui
                driver.FindElement(By.Name("Descricao")).Click();
                driver.FindElement(By.Name("Descricao")).Clear();
                driver.FindElement(By.Name("Descricao")).SendKeys("Válido");
                driver.FindElement(By.Id("BotaoPesquisarVP")).Click();

                driver.FindElement(By.XPath("//button[@id='DynamicBotaoEdit0']/i")).Click();
                driver.FindElement(By.Id("select_ClasseSituacaoDocumento")).Click();
                driver.FindElement(By.Name("Descricao")).Click();
                driver.FindElement(By.Name("Descricao")).Clear();
                driver.FindElement(By.Name("Descricao")).SendKeys("Aprovação e Assinatura");
                driver.FindElement(By.Name("Token")).Click();
                driver.FindElement(By.Name("Token")).Clear();
                driver.FindElement(By.Name("Token")).SendKeys("A24");
                driver.FindElement(By.Id("select_ClasseSituacaoDocumento")).Click();
                new SMCSelectElement(driver.FindElement(By.Id("select_ClasseSituacaoDocumento"))).SelectByText("Válido");
                driver.FindElement(By.Id("divViewEdit")).Click();
                driver.FindElement(By.Name("Ordem")).Clear();
                driver.FindElement(By.Name("Ordem")).SendKeys("3");
                //driver.FindElement(By.Id("btnSalvarVPIN")).Click();
                driver.FindElement(By.CssSelector("[title='Salvar'][type='submit']")).Click();

                //Checando a mensagem de Sucesso (Verifica se tem a palavra sucesso na mensagem)
                /*Assert.True(CheckSuccess(), "Era esperado sucesso e ocorreu um erro");*/

                /* //Declara a variável teste q ira receber o texto do campo
                 string
                 teste = driver.FindElement(By.XPath("//div[@id='centro']/div/div/div[2]")).GetValue();

                 //Compara a mensagem exibida com a esperada:
                 Assert.Equal("Já existe situação de documento de conclusão com esta ordem.", teste);*/

                //Checando a mensagem apresentada (compara a mensagem exibida com a informada)
                Assert.True(CheckMessage("Já existe situação de documento de conclusão com esta ordem."), "Mensagem esperada não exibida");

                driver.Driver.Close();
            });

        }

        [Fact] //Deve ser declarada para teste unitario
        public void SituacaoDocumentoAcademico_Exclusao() //Colocar o nome da tela a ser testada
        {
            base.ExecuteTest((driver) => //chama o browser e coloca o link correto
            {
                //Maximiza o Browser
                // driverNavigator.Manage().Window.Maximize();
                Login(driver); // realiza o login como administrador

                driver.FindElement(By.Name("Descricao")).Click();
                driver.FindElement(By.Name("Descricao")).Clear();
                driver.FindElement(By.Name("Descricao")).SendKeys("Aguardando aprovação e assinatura_Automação");
                driver.FindElement(By.Id("BotaoPesquisarVP")).Click();
                driver.FindElement(By.XPath("//a[@id='DynamicBotaoExcluir1']/i")).Click();
                driver.FindElement(By.Id("BotaoPadraoPerguntaSim")).Click();



                //Checando a mensagem de Sucesso (Verifica se tem a palavra sucesso na mensagem)
                Assert.True(CheckMessage("Situação do documento acadêmico excluída com sucesso."), "Mensagem esperada não exibida");
                driver.Driver.Close();

            });

        }

        [Fact]
        [Trait("Ordenado", "CRUD")]

        public void TesteOrdenadoCRUD()
        {
            SituacaoDocumentoAcademico_Inclusao();
            SituacaoDocumentoAcademico_Alteracao();
            SituacaoDocumentoAcademico_Exclusao();
            SituacaoDocumentoAcademico_AlteracaoNaoPermitida();

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
            driver.GoToUrl2(Consts.SERVIDOR_QUALIDADE_ADM + "CNC/SituacaoDocumentoAcademico");

        }

    }
}
