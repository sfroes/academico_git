using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using ExpectedConditions = SeleniumExtras.WaitHelpers.ExpectedConditions;
using SMC.Framework.Test; //Framework de testes
using System.Threading;
using System;
using Xunit; //Usado no Fact


namespace SMC.Academico.Tests.ADM.ALN //Arvore onde esta o arquivo
{

    public class CondicaodeObrigatoriedade : SMCSeleniumUnitTest //Declara o nome da classe e recebe a SMCSeleniumUnitTest
    {

        [Fact] //Deve ser declarada para teste unitario

        public void CondicaoObrigatoriedade_Inclusao() //Colocar o nome da tela a ser testada
        {
            base.ExecuteTest((driver) => //chama o browser e coloca o link correto
            {
                //Maximiza o Browser
                // driverNavigator.Manage().Window.Maximize();
                Login(driver); // realiza o login como administrador

                //Colar o script aqui

                driver.FindElement(By.Id("BotaoNovoVP")).Click();
                driver.FindElement(By.Id("Descricao")).Click();
                driver.FindElement(By.Name("Descricao")).Click();
                driver.FindElement(By.Name("Descricao")).Click();
                driver.FindElement(By.Name("Descricao")).Clear();
                driver.FindElement(By.Name("Descricao")).SendKeys("Obrigatoriedade para quem não tem proficiência");
                driver.FindElement(By.Id("btnSalvarVPIN")).Click();

                Assert.True(CheckSuccess(), "Era esperado sucesso e ocorreu um erro");

                //Para fechar o Chrome em segundo plano
                driver.Driver.Close();
            });
        }


        [Fact] //Deve ser declarada para teste unitario

        public void CondicaodeObrigatoriedade_InclusaoDuplicada() //Colocar o nome da tela a ser testada
        {
            base.ExecuteTest((driver) => //chama o browser e coloca o link correto
            {
                //Maximiza o Browser
                // driverNavigator.Manage().Window.Maximize();
                Login(driver); // realiza o login como administrador

                //Colar o script aqui

                driver.FindElement(By.Id("BotaoNovoVP")).Click();
                driver.FindElement(By.Id("Descricao")).Click();
                driver.FindElement(By.Name("Descricao")).Click();
                driver.FindElement(By.Name("Descricao")).Click();
                driver.FindElement(By.Name("Descricao")).Clear();
                driver.FindElement(By.Name("Descricao")).SendKeys("Obrigatoriedade para quem não tem proficiência");
                driver.FindElement(By.Id("btnSalvarVPIN")).Click();

                Assert.True(CheckMessage("Já existe condição de obrigatoriedade cadastrada com esta descrição para esta instituição de ensino."), "Mensagem esperada não exibida");

                //Para fechar o Chrome em segundo plano
                driver.Driver.Close();
            });
        }



        [Fact] //Deve ser declarada para teste unitario

        public void CondicaodeObrigatoriedade_Alteracao() //Colocar o nome da tela a ser testada
        {
            base.ExecuteTest((driver) => //chama o browser e coloca o link correto
            {
                //Maximiza o Browser
                // driverNavigator.Manage().Window.Maximize();
                Login(driver); // realiza o login como administrador

                //Colar o script aqui

                driver.FindElement(By.Name("Descricao")).Click();
                driver.FindElement(By.Name("Descricao")).Clear();
                driver.FindElement(By.Name("Descricao")).SendKeys("Obrigatoriedade para quem não tem proficiência");
                driver.FindElement(By.Id("BotaoPesquisarVP")).Click();
                driver.FindElement(By.XPath("//button[@id='DynamicBotaoEdit0']/i")).Click();
                driver.FindElement(By.Name("Descricao")).Click();
                driver.FindElement(By.Name("Descricao")).Clear();
                driver.FindElement(By.Name("Descricao")).SendKeys("Obrigatoriedade para quem não tem proficiência II");
                driver.FindElement(By.Id("btnSalvarVPIE")).Click();

                //Assert.True(CheckMessage("Condição de obrigatoriedade alterado com sucesso."), "Mensagem esperada não exibida");

                //Checando a mensagem de Sucesso (Verifica se tem a palavra sucesso na mensagem)
                Assert.True(CheckSuccess(), "Era esperado sucesso e ocorreu um erro");

                /*//Declara a variável teste q ira receber o texto do campo
                string
                teste = driver.FindElement(By.XPath("//div[@id='centro']/div/div/div[2]")).GetValue();
                        
                //Compara a mensagem exibida com a esperada:
                Assert.Equal("Condição de obrigatoriedade alterado com sucesso.", teste);

                //Para fechar o Chrome em segundo plano*/
                driver.Driver.Close();
            });
        }

        [Fact] //Deve ser declarada para teste unitario

        public void CondicaodeObrigatoriedade_Exclusao() //Colocar o nome da tela a ser testada
        {
            base.ExecuteTest((driver) => //chama o browser e coloca o link correto
            {
                //Maximiza o Browser
                // driverNavigator.Manage().Window.Maximize();
                Login(driver); // realiza o login como administrador

                //Colar o script aqui

                driver.FindElement(By.Name("Descricao")).Click();
                driver.FindElement(By.Name("Descricao")).Clear();
                driver.FindElement(By.Name("Descricao")).SendKeys("Obrigatoriedade para quem não tem proficiência II");
                driver.FindElement(By.Id("BotaoPesquisarVP")).Click();
                driver.FindElement(By.XPath("//a[@id='DynamicBotaoExcluir1']/i")).Click();
                driver.FindElement(By.Id("BotaoPadraoPerguntaSim")).Click();


                //Assert.True(CheckSuccess(), "Era esperado sucesso e ocorreu um erro");

                //Checando a mensagem de Sucesso (Verifica se tem a palavra sucesso na mensagem)
                Assert.True(CheckSuccess(), "Era esperado sucesso e ocorreu um erro");


                //Para fechar o Chrome em segundo plano
                driver.Driver.Close();
            });
        }


        /* [Fact] //Deve ser declarada para teste unitario

         public void CondicaodeObrigatoriedade_Exclusaonaopermitida() //Colocar o nome da tela a ser testada
         {
             base.ExecuteTest((driver) => //chama o browser e coloca o link correto
             {
                 //Maximiza o Browser
                 // driverNavigator.Manage().Window.Maximize();
                 Login(driver); // realiza o login como administrador

                 //Colar o script aqui

                 driver.FindElement(By.Name("Seq")).Click();
                 driver.FindElement(By.Name("Seq")).Clear();
                 driver.FindElement(By.Name("Seq")).SendKeys("4");
                 driver.FindElement(By.Id("BotaoPesquisarVP")).Click();
                 driver.FindElement(By.XPath("//a[@id='DynamicBotaoExcluir1']/i")).Click();
                 driver.FindElement(By.Id("BotaoPadraoPerguntaSim")).Click();

                 Assert.True(CheckSuccess(), "Era esperado sucesso e ocorreu um erro");

                 //Para fechar o Chrome em segundo plano
                driver.Driver.Close();


             });
         }
        */



        [Fact]
        [Trait("Ordenado", "CRUD")]
        public void TesteOrdenadoCRUD()
        {
            CondicaoObrigatoriedade_Inclusao();
            CondicaodeObrigatoriedade_InclusaoDuplicada();
            CondicaodeObrigatoriedade_Alteracao();
            CondicaodeObrigatoriedade_Exclusao();

        }


        private static void Login(ISMCWebDriver driver)
        {
            //----------------------------------------------------
            driver.GoToUrl(Consts.SERVIDOR_QUALIDADE_ADM);
            //força o sistema a esperar 15 minutos ou até que apareça o campo para login
            WebDriverWait wait = new WebDriverWait((OpenQA.Selenium.IWebDriver)driver, TimeSpan.FromSeconds(90));
            wait.Until(e => e.FindElement(By.Name("Login")));
            driver.SMCLoginCpf();
            //força o sistema a esperar 15 minutos ou até que apareça a home do SGA
            wait.Until(ExpectedConditions.TextToBePresentInElementLocated(By.XPath("//div[@id='divConteudoPrincipal']/section/h2"), "SGA.Administrativo"));
            driver.GoToUrl2(Consts.SERVIDOR_QUALIDADE_ADM + "CUR/CondicaoObrigatoriedade");
        }

    }
}
