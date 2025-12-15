using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using ExpectedConditions = SeleniumExtras.WaitHelpers.ExpectedConditions;
using SMC.Framework.Test; //Framework de testes
using System.Threading;
using Xunit; //Usado no Fact
using System;



namespace SMC.Academico.Tests.ADM.CSO //Arvore onde esta o arquivo
{

    public class DivisaoCurricular: SMCSeleniumUnitTest //Declara o nome da classe e recebe a SMCSeleniumUnitTest
    {


        [Fact] //Deve ser declarada para teste unitario
        public void DivisaoCurricular_Inclusao() //Colocar o nome da tela a ser testada
        {
            // try
            // {
            base.ExecuteTest((driver) => //chama o browser e coloca o link correto
            {
                //Maximiza o Browser
                // driverNavigator.Manage().Window.Maximize();
                Login(driver); // realiza o login como administrador


                //COLAR AQUI O SCRIPT GRAVADO

                driver.FindElement(By.Id("BotaoNovoVP")).Click();
                driver.FindElement(By.Name("Descricao")).Click();
                driver.FindElement(By.Name("Descricao")).Clear();

                driver.FindElement(By.Name("Descricao")).SendKeys("Divisão Curricular para teste automação");
                driver.FindElement(By.Id("select_SeqNivelEnsino")).SendKeys("Doutorado");
                driver.FindElement(By.Name("Itens[0].Descricao")).SendKeys("Descrição do item da divisão");

                //driver.FindElement(By.XPath("//button[@id='btnSalvarVPIN']/i")).Click();
                driver.FindElement(By.CssSelector("[title='Salvar'][type='submit']")).Click();

                Assert.True(CheckMessage("Divisão curricular incluído com sucesso."), "Mensagem esperada não exibida");
                driver.Driver.Close();

            });
        }

        [Fact] //Deve ser declarada para teste unitario
        public void DivisaoCurricular_Inclusaoduplicada() //Colocar o nome da tela a ser testada
        {
            // try
            // {
            base.ExecuteTest((driver) => //chama o browser e coloca o link correto
            {
                //Maximiza o Browser
                // driverNavigator.Manage().Window.Maximize();
                Login(driver); // realiza o login como administrador


                //COLAR AQUI O SCRIPT GRAVADO

                driver.FindElement(By.Id("BotaoNovoVP")).Click();
                driver.FindElement(By.Name("Descricao")).Click();
                driver.FindElement(By.Name("Descricao")).Clear();
                driver.FindElement(By.Name("Descricao")).SendKeys("Divisão Curricular para teste automação");
                driver.FindElement(By.Id("select_SeqNivelEnsino")).SendKeys("Doutorado");
                driver.FindElement(By.Name("Itens[0].Descricao")).SendKeys("Descrição do item da divisão");

                driver.FindElement(By.CssSelector("[title='Salvar'][type='submit']")).Click();
                //driver.FindElement(By.XPath("//button[@id='btnSalvarVPIN']/i")).Click();

                //Assert.True(CheckMessage("Já existe divisão curricular cadastrada com esta descrição para este nível de ensino e instituição."), "Mensagem esperada não exibida");

                //Outra forma de validar a mensagem exibida(pode substituir a checagem de mensagem de sucesso e a checagem de mensagem apresentada:
                //Declara a variável teste q ira receber o texto do campo
                string
                teste = driver.FindElement(By.XPath("//div[@id='centro']/div/div/div[2]")).GetValue();

                //Compara a mensagem exibida com a esperada:
                Assert.Equal("Já existe divisão curricular cadastrada com esta descrição para este nível de ensino e instituição.", teste);
                driver.Driver.Close();
            });
        }

        [Fact] //Deve ser declarada para teste unitario
        public void DivisaoCurricular_Alteracao() //Colocar o nome da tela a ser testada
        {
            // try
            // {
            base.ExecuteTest((driver) => //chama o browser e coloca o link correto
            {
                //Maximiza o Browser
                // driverNavigator.Manage().Window.Maximize();
                Login(driver); // realiza o login como administrador


                //COLAR AQUI O SCRIPT GRAVADO

                driver.FindElement(By.Name("Descricao")).Click();
                driver.FindElement(By.Name("Descricao")).Clear();
                driver.FindElement(By.Name("Descricao")).SendKeys("Divisão Curricular para teste automação");
                driver.FindElement(By.Id("BotaoPesquisarVP")).Click();

                //driver.FindElement(By.XPath("//button[@id='DynamicBotaoEdit0']/i")).Click();
                driver.FindElement(By.Id("DynamicBotaoEdit0")).Click();
                //driver.FindElement(By.CssSelector("[title='Alterar'][type='submit']")).Click();

                driver.FindElement(By.Id("divViewEdit")).Click();
                driver.FindElement(By.Id("divViewEdit")).Click();
                driver.FindElement(By.Name("Descricao")).Clear();
                driver.FindElement(By.Name("Descricao")).SendKeys("Divisão Curricular para teste automação alterada");
                
                driver.FindElement(By.CssSelector("[title='Salvar'][type='submit']")).Click();
                //driver.FindElement(By.Id("btnSalvarVPIE")).Click();

               Assert.True(CheckMessage("Divisão curricular alterado com sucesso."), "Mensagem esperada não exibida");
                driver.Driver.Close();


            });
        }


        [Fact] //Deve ser declarada para teste unitario
        public void DivisaoCurricular_Exclusao() //Colocar o nome da tela a ser testada
        {
            // try
            // {
            base.ExecuteTest((driver) => //chama o browser e coloca o link correto
            {
                //Maximiza o Browser
                // driverNavigator.Manage().Window.Maximize();
                Login(driver); // realiza o login como administrador


                //COLAR AQUI O SCRIPT GRAVADO

                driver.FindElement(By.Name("Descricao")).Click();
                driver.FindElement(By.Name("Descricao")).Clear();
                driver.FindElement(By.Name("Descricao")).SendKeys("Divisão Curricular para teste automação alterada");
                driver.FindElement(By.Id("BotaoPesquisarVP")).Click();

                driver.FindElement(By.Id("DynamicBotaoExcluir1")).Click();
                //driver.FindElement(By.XPath("//a[@id='DynamicBotaoExcluir1']/i")).Click();
                driver.FindElement(By.Id("BotaoPadraoPerguntaSim")).Click();

                Assert.True(CheckMessage("Divisão curricular excluído com sucesso."), "Mensagem esperada não exibida");
                driver.Driver.Close();

            });
        }
        /*
        [Fact] //Deve ser declarada para teste unitario
        public void DivisaoCurricular_Exclusaonaopermitida() //Colocar o nome da tela a ser testada
        {
            // try
            // {
            base.ExecuteTest((driver) => //chama o browser e coloca o link correto
            {
                //Maximiza o Browser
                // driverNavigator.Manage().Window.Maximize();
                Login(driver); // realiza o login como administrador


                //COLAR AQUI O SCRIPT GRAVADO

                driver.FindElement(By.Name("Descricao")).Clear();
                driver.FindElement(By.Name("Descricao")).SendKeys("Semestre");
                driver.FindElement(By.XPath("//button[@id='BotaoPesquisarVP']/i")).Click();
                driver.FindElement(By.XPath("//a[@id='DynamicBotaoExcluir1']/i")).Click();
                driver.FindElement(By.Id("BotaoPadraoPerguntaSim")).Click();


                Assert.True(CheckMessage("Exclusão não permitida. Tipo de divisão curricular já possui "), "Mensagem esperada não exibida");

            });
        }*/

        [Fact]
        [Trait("Ordenado", "CRUD")]
        public void TesteOrdenadoCRUD()
        {
            DivisaoCurricular_Inclusao();
            DivisaoCurricular_Inclusaoduplicada();
            DivisaoCurricular_Alteracao();
            DivisaoCurricular_Exclusao();
           


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

            driver.GoToUrl2(Consts.SERVIDOR_QUALIDADE_ADM + "CUR/DivisaoCurricular"); //coloca o resto do endereço para acessar a pagina do teste


        }
    }
}

