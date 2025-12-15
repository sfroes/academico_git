using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using ExpectedConditions = SeleniumExtras.WaitHelpers.ExpectedConditions;
using SMC.Framework.Test; //Framework de testes
using System.Threading;
using System;
using Xunit; //Usado no Fact

namespace SMC.Academico.Tests.ADM.CNC //Arvore onde esta o arquivo
{
    public class OrgaoRegistro : SMCSeleniumUnitTest //Declara o nome da classe e recebe a SMCSeleniumUnitTest
    {

        [Fact] //Deve ser declarada para teste unitario
        public void OrgaoRegistro_Inclusao() //Colocar o nome da tela a ser testada
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
                driver.FindElement(By.Name("Descricao")).SendKeys("Pontifícia Universidade Católica do Rio Grande do Norte");
                driver.FindElement(By.Name("Sigla")).Click();
                driver.FindElement(By.Name("Sigla")).Clear();
                driver.FindElement(By.Name("Sigla")).SendKeys("PRN");
                driver.FindElement(By.Id("btnSalvarVPIN")).Click();

                //Checando a mensagem de Sucesso (compara a mensagem que está descrita com a que vai aparecer no aplicativo)
                Assert.True(CheckMessage("Órgão de registro incluído com sucesso."), "Mensagem esperada não exibida");
                driver.Driver.Close();
            });

        }

        [Fact] //Deve ser declarada para teste unitario
        public void OrgaoRegistro_Alteracao() //Colocar o nome da tela a ser testada
        {
            base.ExecuteTest((driver) => //chama o browser e coloca o link correto
            {
                //Maximiza o Browser
                // driverNavigator.Manage().Window.Maximize();
                Login(driver); // realiza o login como administrador

                //Colar o script aqui
                driver.FindElement(By.Name("Sigla")).Click();
                driver.FindElement(By.Name("Sigla")).Clear();
                driver.FindElement(By.Name("Sigla")).SendKeys("PRN");
                driver.FindElement(By.Id("BotaoPesquisarVP")).Click();
                driver.FindElement(By.XPath("//button[@id='DynamicBotaoEdit0']/i")).Click();
                driver.FindElement(By.XPath("//form[@id='frmModalPadrao']/div[2]")).Click();
                driver.FindElement(By.Name("Sigla")).Clear();
                driver.FindElement(By.Name("Sigla")).SendKeys("PURN");
                driver.FindElement(By.XPath("//button[@id='btnSalvarVPIE']/i")).Click();

                Assert.True(CheckMessage("Órgão de registro alterado com sucesso."), "Mensagem esperada não exibida");
                driver.Driver.Close();
            });
        }

        [Fact] //Deve ser declarada para teste unitario
        public void OrgaoRegistro_InclusaoDuplicada() //Colocar o nome da tela a ser testada
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
                driver.FindElement(By.Name("Descricao")).SendKeys("Pontifícia Universidade Católica de Minas Gerais");
                driver.FindElement(By.Name("Sigla")).Click();
                driver.FindElement(By.Name("Sigla")).Clear();
                driver.FindElement(By.Name("Sigla")).SendKeys("PUC/MG");
                driver.FindElement(By.Id("btnSalvarVPIN")).Click();
                //Declara a variável teste q ira receber o texto do campo
                //string
               // teste = driver.FindElement(By.XPath("//div[@id='centro']/div/div/div[2]")).GetValue();
                //Compara a mensagem exibida com a esperada:

                Assert.True(CheckMessage("Já existe orgão de registro cadastrado com esta descrição para esta instituição de ensino."), "Mensagem esperada não exibida");
                //Assert.Equal("Já existe orgão de registro cadastrado com esta descrição para esta instituição de ensino.", teste);
                driver.Driver.Close();
            });
        }

        [Fact] //Deve ser declarada para teste unitario
        public void OrgaoRegistro_Exclusao() //Colocar o nome da tela a ser testada
        {
            base.ExecuteTest((driver) => //chama o browser e coloca o link correto
            {
                //Maximiza o Browser
                // driverNavigator.Manage().Window.Maximize();
                Login(driver); // realiza o login como administrador

                //Colar o script aqui

                driver.FindElement(By.Name("Descricao")).Click();
                driver.FindElement(By.Name("Descricao")).Clear();
                driver.FindElement(By.Name("Descricao")).SendKeys("Pontificia Universidade Católica do Rio Grande do Norte");
                driver.FindElement(By.Id("BotaoPesquisarVP")).Click();
                driver.FindElement(By.XPath("//a[@id='DynamicBotaoExcluir1']/i")).Click();
                driver.FindElement(By.Id("BotaoPadraoPerguntaSim")).Click();
                Assert.True(CheckMessage("Órgão de registro excluído com sucesso."), "Mensagem esperada não exibida");
                driver.Driver.Close();
            });
        }


        [Fact]
         [Trait("Ordenado", "CRUD")]
        public void TesteOrdenadoCRUD()
        
        {
            OrgaoRegistro_Inclusao();
            OrgaoRegistro_Alteracao();
            OrgaoRegistro_InclusaoDuplicada();
            OrgaoRegistro_Exclusao();
             

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
        driver.GoToUrl2(Consts.SERVIDOR_QUALIDADE_ADM + "CNC/OrgaoRegistro");

    }

}
}

















