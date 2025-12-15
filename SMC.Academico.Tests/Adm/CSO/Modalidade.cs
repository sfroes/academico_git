using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using ExpectedConditions = SeleniumExtras.WaitHelpers.ExpectedConditions;
using SMC.Framework.Test; //Framework de testes
using System.Threading;
using Xunit; //Usado no Fact
using System;
using SMC.Academico;



namespace SMC.Academico.Tests.ADM.CSO //Arvore onde esta o arquivo
{

    public class Modalidade : SMCSeleniumUnitTest //Declara o nome da classe e recebe a SMCSeleniumUnitTest
    {


        [Fact] //Deve ser declarada para teste unitario
        public void Modalidade_Inclusao() //Colocar o nome da tela a ser testada
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
                driver.FindElement(By.Name("Descricao")).SendKeys("Híbrido");
                driver.FindElement(By.Name("DescricaoXSD")).Click();
                driver.FindElement(By.Name("DescricaoXSD")).Clear();
                driver.FindElement(By.Name("DescricaoXSD")).SendKeys("HIB");
                driver.FindElement(By.Id("btnSalvarNovoVPIN")).Click();
                driver.FindElement(By.Name("Descricao")).Click();
                driver.FindElement(By.Name("Descricao")).Click();
                driver.FindElement(By.Name("Descricao")).Clear();
                driver.FindElement(By.Name("Descricao")).SendKeys("Automação");
                driver.FindElement(By.Name("DescricaoXSD")).Click();
                driver.FindElement(By.Name("DescricaoXSD")).Clear();
                driver.FindElement(By.Name("DescricaoXSD")).SendKeys("AUT");
                driver.FindElement(By.Id("btnSalvarVPIN")).Click();

                Assert.True(CheckSuccess(), "Era esperado sucesso e ocorreu um erro");

                //Para fechar o Chrome em segundo plano
                driver.Driver.Close();
            });
        }

        [Fact] //Deve ser declarada para teste unitario
        public void Modalidade_Inclusaoduplicada() //Colocar o nome da tela a ser testada
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
                driver.FindElement(By.Name("Descricao")).SendKeys("Híbrido");
                driver.FindElement(By.Name("DescricaoXSD")).Click();
                driver.FindElement(By.Name("DescricaoXSD")).Clear();
                driver.FindElement(By.Name("DescricaoXSD")).SendKeys("HIB");
                driver.FindElement(By.Id("btnSalvarVPIN")).Click();

                // Assert.True(CheckSuccess(), "Era esperado sucesso e ocorreu um erro");
                Assert.True(CheckMessage("Já existe modalidade cadastrada com esta descrição."), "Mensagem esperada não exibida");

                //Para fechar o Chrome em segundo plano
                driver.Driver.Close();
            });
        }

        [Fact] //Deve ser declarada para teste unitario
        public void Modalidade_Alteracao() //Colocar o nome da tela a ser testada
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
                driver.FindElement(By.Name("Descricao")).SendKeys("Híbrido");
                //driver.FindElement(By.Id("frmViewPadrao")).Submit();

                
                driver.FindElement(By.Id("BotaoPesquisarVP")).Click();
                driver.FindElement(By.Id("DynamicBotaoEdit0")).Click();

                //driver.FindElement(By.XPath("//button[@id='DynamicBotaoEdit0']/i")).Click();
                //driver.FindElement(By.Id("DynamicBotaoEdit0")).Click();
                //driver.FindElement(By.CssSelector("[title='Alterar'][type='button']")).Click();

                driver.FindElement(By.Name("DescricaoXSD")).Click();
                driver.FindElement(By.Name("DescricaoXSD")).Clear();
                driver.FindElement(By.Name("DescricaoXSD")).SendKeys("HIBR");
                //driver.FindElement(By.XPath("//button[@id='btnSalvarVPIE']/i")).Click();
                driver.FindElement(By.Id("btnSalvarVPIE")).Click();

                Assert.True(CheckMessage("Modalidade alterado com sucesso"), "Mensagem esperada não exibida");

                //Para fechar o Chrome em segundo plano
                driver.Driver.Close();

            });
        }


        [Fact] //Deve ser declarada para teste unitario
        public void Modalidade_Exclusao() //Colocar o nome da tela a ser testada
        {
            // try
            // {
            base.ExecuteTest((driver) => //chama o browser e coloca o link correto
            {
                //Maximiza o Browser
                // driverNavigator.Manage().Window.Maximize();
                Login(driver); // realiza o login como administrador


                //COLAR AQUI O SCRIPT GRAVADO
                //Excluindo registro com a descrição 'Automação'
                driver.FindElement(By.Name("Descricao")).Click();
                driver.FindElement(By.Name("Descricao")).Clear();
                driver.FindElement(By.Name("Descricao")).SendKeys("Automação");
                driver.FindElement(By.Id("BotaoPesquisarVP")).Click();
                
                driver.FindElement(By.XPath("//a[@id='DynamicBotaoExcluir1']/i")).Click();
                driver.FindElement(By.Id("DynamicBotaoExcluir1")).Click();

                //driver.FindElement(By.XPath("//div[@id='default']/div[3]/div[2]/div[3]/div/div/button")).Click();
                driver.FindElement(By.Id("BotaoPadraoPerguntaSim")).Click();
                
                Assert.True(CheckMessage("Modalidade excluído com sucesso"), "Mensagem esperada não exibida");

                //Excluindo registro com a descrição 'Híbrido'
                driver.FindElement(By.Name("Descricao")).Click();
                driver.FindElement(By.Name("Descricao")).Clear();
                driver.FindElement(By.Name("Descricao")).SendKeys("Híbrido");
                driver.FindElement(By.Id("BotaoPesquisarVP")).Click();

                driver.FindElement(By.XPath("//a[@id='DynamicBotaoExcluir1']/i")).Click();
                driver.FindElement(By.Id("DynamicBotaoExcluir1")).Click();

                //driver.FindElement(By.XPath("//div[@id='default']/div[3]/div[2]/div[3]/div/div/button")).Click();
                driver.FindElement(By.Id("BotaoPadraoPerguntaSim")).Click();

                Assert.True(CheckMessage("Modalidade excluído com sucesso"), "Mensagem esperada não exibida");

                //Para fechar o Chrome em segundo plano
                driver.Driver.Close();
            });
        }

        [Fact] //Deve ser declarada para teste unitario
        public void Modalidade_Exclusaonaopermitida() //Colocar o nome da tela a ser testada
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
                driver.FindElement(By.Name("Descricao")).SendKeys("Presencial");
                driver.FindElement(By.Id("BotaoPesquisarVP")).Click();
                driver.FindElement(By.XPath("//a[@id='DynamicBotaoExcluir1']/i")).Click();
                driver.FindElement(By.Id("BotaoPadraoPerguntaSim")).Click();

                //Assert.True(CheckMessage("Modalidade excluído com sucesso"), "Mensagem esperada não exibida");
                Assert.True(CheckMessage("Exclusão não permitida. Modalidade já foi utilizada no cadastro de tipo de divisão de componente curricular."), "Mensagem esperada não exibida");

                //Para fechar o Chrome em segundo plano
                driver.Driver.Close();
            });
        }

        [Fact]
        [Trait("Ordenado", "CRUD")]
        public void TesteOrdenadoCRUD()
        {
            Modalidade_Inclusao();
            Modalidade_Inclusaoduplicada();
            Modalidade_Alteracao();
            Modalidade_Exclusao();
            Modalidade_Exclusaonaopermitida();

        }

        private static void Login(ISMCWebDriver driver)
        {
            //----------------------------------------------------
            driver.GoToUrl(Consts.SERVIDOR_QUALIDADE_ADM);
            //força o sistema a esperar 15 minutos ou até que apareça o campo para login
            WebDriverWait wait = new WebDriverWait((OpenQA.Selenium.IWebDriver)driver, TimeSpan.FromSeconds(900));
            wait.Until(e => e.FindElement(By.Name("LoginCpf")));
            driver.SMCLoginCpf(); //insere login e senha
            //força o sistema a esperar 15 minutos ou até que apareça a home do SGA
           // wait.Until(ExpectedConditions.TextToBePresentInElementLocated(By.XPath("//div[@id='divConteudoPrincipal']/section/h2"), "SGA.Administrativo"));//

            driver.GoToUrl2(Consts.SERVIDOR_QUALIDADE_ADM + "CSO/Modalidade"); //coloca o resto do endereço para acessar a pagina do teste


        }
    }
}
