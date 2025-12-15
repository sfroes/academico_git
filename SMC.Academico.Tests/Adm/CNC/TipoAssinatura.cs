using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SMC.Framework.Test; //Framework de testes
using System.Threading;
using Xunit; //Usado no Fact

namespace SMC.Academico.Tests.ADM.CNC //Arvore onde esta o arquivo
{

    public class TipoAssinatura : SMCSeleniumUnitTest //Declara o nome da classe e recebe a SMCSeleniumUnitTest
    {

        [Fact] //Deve ser declarada para teste unitario
        public void TipoAssinatura_Inserir() //Colocar o nome da tela a ser testada
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
                    driver.FindElement(By.Name("Descricao")).SendKeys("Automação");
                    driver.FindElement(By.Id("btnSalvarVPIN")).Click();
                //Checando a mensagem de Sucesso (compara a mensagem que está descrita com a que vai aparecer no aplicativo)
              Assert.True(CheckMessage("Tipo de Assinatura incluída com sucesso."), "Mensagem esperada não exibida");
            });
        }

        [Fact] //Deve ser declarada para teste unitario
        public void TipoAssinatura_NaoPermitirInserirDescricaoDuplicada() //Colocar o nome da tela a ser testada
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
            driver.FindElement(By.Name("Descricao")).SendKeys("Automação");
            driver.FindElement(By.Id("btnSalvarVPIN")).Click();
            

             Assert.True(CheckMessage("Já existe tipo de assinatura cadastrada com esta descrição para esta instituição de ensino"), "Mensagem esperada não exibida");

            });

        }

        [Fact] //Deve ser declarada para teste unitario
        public void TipoAssinatura_Inclusaocamposobrigatórios() //Colocar o nome da tela a ser testada
        {
            base.ExecuteTest((driver) => //chama o browser e coloca o link correto
            {
                //Maximiza o Browser
                // driverNavigator.Manage().Window.Maximize();
                Login(driver); // realiza o login como administrador

                //Colar o script aqui
                                
                    driver.FindElement(By.Id("BotaoNovoVP")).Click();
                    driver.FindElement(By.Id("btnSalvarVPIN")).Click();


                //Declara a variável teste q ira receber o texto do campo
                string
                teste = driver.FindElement(By.CssSelector("span[id='parsley-id-21']")).GetValue();

                //Compara a mensagem exibida com a esperada:
                //Assert.Equal("Preenchimento Obrigatório", teste);

                //Validar campo obrigatório. Informar nome do campo e mensagem de obrigatoriedade. 
                driver.FindElement(By.Name("Descricao")).CheckErrorMessage("Preenchimento obrigatório");

            });

        }

        [Fact] //Deve ser declarada para teste unitario
            public void TipoAssinatura_Alteracao() //Colocar o nome da tela a ser testada
            {
                base.ExecuteTest((driver) => //chama o browser e coloca o link correto
                {
                    //Maximiza o Browser
                    // driverNavigator.Manage().Window.Maximize();
                    Login(driver); // realiza o login como administrador

                    //Colar o script aqui
                    driver.FindElement(By.XPath("//button[@id='DynamicBotaoEdit0']/i")).Click();
                    driver.FindElement(By.Id("divViewEdit")).Click();
                    driver.FindElement(By.Name("Descricao")).Clear();
                    driver.FindElement(By.Name("Descricao")).SendKeys("Assinatura Reitoria Automação");
                    driver.FindElement(By.Id("btnSalvarVPIE")).Click();

                    //Checando a mensagem de Sucesso (Verifica se tem a palavra sucesso na mensagem)
                    Assert.True(CheckSuccess(), "Era esperado sucesso e ocorreu um erro");

                });
                                    
        }

        [Fact] //Deve ser declarada para teste unitario
        public void TipoAssinatura_Exclusao() //Colocar o nome da tela a ser testada
        {
            base.ExecuteTest((driver) => //chama o browser e coloca o link correto
            {
                //Maximiza o Browser
                // driverNavigator.Manage().Window.Maximize();

                Login(driver); // realiza o login como administrador

                //Colar o script aqui
                driver.FindElement(By.Name("Descricao")).Click();
                driver.FindElement(By.Name("Descricao")).Clear();
                driver.FindElement(By.Name("Descricao")).SendKeys("Automação");
                driver.FindElement(By.Id("BotaoPesquisarVP")).Click();
                driver.FindElement(By.XPath("//a[@id='DynamicBotaoExcluir1']/i")).Click();
                driver.FindElement(By.Id("BotaoPadraoPerguntaSim")).Click();

                Assert.True(CheckMessage("Tipo de Assinatura excluída com sucesso."), "Mensagem esperada não exibida");

            });
        }

       
        /*[Fact]
        // [Trait("Ordenado", "CRUD")]
        public void TesteOrdenadoCRUD()
        {

          TipoAssinatura_Inserir();
            TipoAssinatura_NaoPermitirInserirDescricaoDuplicada();
            TipoAssinatura_Inclusaocamposobrigatórios();
            TipoAssinatura_Alteracao();
            TipoAssinatura_Exclusao();
        }
        */

        private static void Login(ISMCWebDriver driver)
        {
            //----------------------------------------------------
            driver.GoToUrl(Consts.SERVIDOR_QUALIDADE_ADM); //recebe o link para acesso

            //Login
            driver.SMCLoginCpf(); //insere login e senha

            driver.GoToUrl2(Consts.SERVIDOR_QUALIDADE_ADM + "CNC/TipoAssinatura"); //coloca o resto do endere�o para acessar a pagina do teste

        }
    }
}
