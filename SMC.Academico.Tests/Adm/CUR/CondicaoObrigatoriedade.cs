using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SMC.Framework.Test; //Framework de testes
using System.Threading;
using Xunit; //Usado no Fact

namespace SMC.Academico.Tests.ADM.CUR //Arvore onde esta o arquivo
{

    public class Condicao_Obrigatoriedade : SMCSeleniumUnitTest //Declara o nome da classe e recebe a SMCSeleniumUnitTest
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
                driver.FindElement(By.Name("Descricao")).Click();
                driver.FindElement(By.Name("Descricao")).Clear();
                driver.FindElement(By.Name("Descricao")).SendKeys("Condição de obrigatoriedade automação");
                driver.FindElement(By.Id("btnSalvarVPIN")).Click();
                
                Assert.True(CheckSuccess(), "Condição de obrigatoriedade incluído com sucesso.");

                //Para fechar o Chrome em segundo plano
                driver.Driver.Close();

            });
        }

        [Fact] //Deve ser declarada para teste unitario
        public void CondicaoObrigatoriedade_InclusaoDuplicada() //Colocar o nome da tela a ser testada
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
                driver.FindElement(By.Name("Descricao")).SendKeys("Condição de obrigatoriedade automação");
                driver.FindElement(By.Id("btnSalvarVPIN")).Click();

                Assert.True(CheckMessage("Já existe condição de obrigatoriedade cadastrada com esta descrição para esta instituição de ensino."), "Mensagem esperada não exibida");

                //Para fechar o Chrome em segundo plano
                driver.Driver.Close();

            });
        }


        [Fact] //Deve ser declarada para teste unitario
        public void CondicaoObrigatoriedade_Alteracao() //Colocar o nome da tela a ser testada
        {
            base.ExecuteTest((driver) => //chama o browser e coloca o link correto
            {
                //Maximiza o Browser
                // driverNavigator.Manage().Window.Maximize();
                Login(driver); // realiza o login como administrador

                //Colar o script aqui
                driver.FindElement(By.Name("Descricao")).Click();
                driver.FindElement(By.Name("Descricao")).Clear();
                driver.FindElement(By.Name("Descricao")).SendKeys("Condição de obrigatoriedade automação");
                driver.FindElement(By.Id("BotaoPesquisarVP")).Click();
                driver.FindElement(By.XPath("//button[@id='DynamicBotaoEdit0']/i")).Click();
                driver.FindElement(By.Name("Descricao")).Click();
                driver.FindElement(By.Name("Descricao")).Clear();
                driver.FindElement(By.Name("Descricao")).SendKeys("Condição de obrigatoriedade automação alterada");
                driver.FindElement(By.Id("btnSalvarVPIE")).Click();
                //driver.FindElement(By.XPath("//div[@id='centro']/div/div/div[2]")).Click();

                Assert.True(CheckSuccess(), "Condição de obrigatoriedade alterado com sucesso.");

                //Para fechar o Chrome em segundo plano
                driver.Driver.Close();

            });
        }

        [Fact] //Deve ser declarada para teste unitario
        public void CondicaoObrigatoriedade_Exclusao() //Colocar o nome da tela a ser testada
        {
            base.ExecuteTest((driver) => //chama o browser e coloca o link correto
            {
                //Maximiza o Browser
                // driverNavigator.Manage().Window.Maximize();
                Login(driver); // realiza o login como administrador

                //Colar o script aqui
                driver.FindElement(By.Name("Descricao")).Click();
                driver.FindElement(By.Name("Descricao")).Clear();
                driver.FindElement(By.Name("Descricao")).SendKeys("Condição de obrigatoriedade automação");
                
                driver.FindElement(By.Id("BotaoPesquisarVP")).Click();
                driver.FindElement(By.Id("DynamicBotaoExcluir1")).Click();
                driver.FindElement(By.Id("BotaoPadraoPerguntaSim")).Click();

                //Valida mensagem de exclusão com sucesso
                Assert.True(CheckSuccess(), "Condição de obrigatoriedade excluído com sucesso.");

                //Para fechar o Chrome em segundo plano
                driver.Driver.Close();

            });
        }

        [Fact]
        [Trait("Ordenado", "CRUD")]
        public void TesteOrdenadoCRUD()
        {
            CondicaoObrigatoriedade_Inclusao();
            CondicaoObrigatoriedade_InclusaoDuplicada();
            CondicaoObrigatoriedade_Alteracao();
            CondicaoObrigatoriedade_Exclusao();
        }

        private static void Login(ISMCWebDriver driver)
        {
            //----------------------------------------------------
            driver.GoToUrl(Consts.SERVIDOR_QUALIDADE_ADM); //recebe o link para acesso

            //Login
            driver.SMCLoginCpf(); //insere login e senha

            driver.GoToUrl2(Consts.SERVIDOR_QUALIDADE_ADM + "CUR/CondicaoObrigatoriedade"); //coloca o resto do endereço para acessar a pagina do teste

        }
    }
}



