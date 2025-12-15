
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using ExpectedConditions = SeleniumExtras.WaitHelpers.ExpectedConditions;
using SMC.Framework.Test; //Framework de testes
using System.Threading;
using System;
using Xunit; //Usado no Fact


namespace SMC.Academico.Tests.ADM.CNC //Arvore onde esta o arquivo
{

    public class TipoDocumentoAcademico : SMCSeleniumUnitTest //Declara o nome da classe e recebe a SMCSeleniumUnitTest
    {


        [Fact] //Deve ser declarada para teste unitario
        public void TipoDocumentoAcademico_Inclusao() //Colocar o nome da tela a ser testada
        {
            base.ExecuteTest((driver) => //chama o browser e coloca o link correto
            {
                //Maximiza o Browser
                // driverNavigator.Manage().Window.Maximize();
                Login(driver); // realiza o login como administrador

                //Colar o script aqui
                driver.FindElement(By.Id("BotaoNovoVP")).Click();
                driver.FindElement(By.Name("Descricao")).Clear();
                driver.FindElement(By.Name("Descricao")).SendKeys("Diploma de Graduação");
                driver.FindElement(By.Name("Token")).Click();
                driver.FindElement(By.Name("Token")).Clear();
                driver.FindElement(By.Name("Token")).SendKeys("A99");
                driver.FindElement(By.Id("select_GrupoDocumentoAcademico")).Click();
                new SMCSelectElement(driver.FindElement(By.Id("select_GrupoDocumentoAcademico"))).SelectByText("Diploma");
                //driver.FindElement(By.Id("select_TramiteAssinaturas")).Click();
                //new SMCSelectElement(driver.FindElement(By.Id("select_TramiteAssinaturas"))).SelectByText("Assinatura digital");
                driver.FindElement(By.Id("BotaoSalvarTemplate")).Click();
                
                //driver.FindElement(By.CssSelector("[title='Salvar'][type='submit']")).Click();

                //Checando a mensagem de Sucesso (compara a mensagem que está descrita com a que vai aparecer no aplicativo)
                Assert.True(CheckMessage("Tipo de documento acadêmico incluído com sucesso."), "Mensagem esperada não exibida");
                driver.Driver.Close();

            });                     
            }

        [Fact] //Deve ser declarada para teste unitario
        public void TipoDocumentoAcademico_Alteracao() //Colocar o nome da tela a ser testada
        {
            base.ExecuteTest((driver) => //chama o browser e coloca o link correto
            {
                //Maximiza o Browser
                // driverNavigator.Manage().Window.Maximize();
                Login(driver); // realiza o login como administrador

                //Colar o script aqui

                driver.FindElement(By.Name("Descricao")).Click();
                driver.FindElement(By.Name("Descricao")).Clear();
                driver.FindElement(By.Name("Descricao")).SendKeys("Diploma de Graduação");
                driver.FindElement(By.Id("BotaoPesquisarVP")).Click();
                driver.FindElement(By.XPath("//a[@id='DynamicBotaoEdit0']/i")).Click();

                //driver.FindElement(By.Id("select_TramiteAssinaturas")).Click();
                //new SMCSelectElement(driver.FindElement(By.Id("select_TramiteAssinaturas"))).SelectByText("Assinatura manual");

                driver.FindElement(By.XPath("//button[@id='Servicos_DetailBotaoInserirElemento']/i")).Click();
                driver.FindElement(By.Id("select_Servicos_0__SeqServico")).Click();
                new SMCSelectElement(driver.FindElement(By.Id("select_Servicos_0__SeqServico"))).SelectByText("Inclusão de dispensa individual");
                driver.FindElement(By.XPath("//div[@id='divConteudoPrincipalEstrutura']/div[3]")).Click();
                new SMCSelectElement(driver.FindElement(By.Id("select_Servicos_0__SeqServico"))).SelectByText("Cancelamento de matrícula");
                driver.FindElement(By.XPath("//div[@id='divConteudoPrincipalEstrutura']/div[3]")).Click();

                driver.FindElement(By.Id("BotaoSalvarTemplate")).Click();

                //Checando a mensagem de Sucesso (Verifica se tem a palavra sucesso na mensagem)
                //Assert.True(CheckSuccess(), "Era esperado sucesso e ocorreu um erro");

                //Declara a variável teste q ira receber o texto do campo
                string
                teste = driver.FindElement(By.XPath("//div[@id='centro']/div/div/div[2]")).GetValue();

                //Compara a mensagem exibida com a esperada:
                Assert.Equal("Tipo de documento acadêmico alterado com sucesso.", teste);

                driver.Driver.Close();


            });

        }

        [Fact] //Deve ser declarada para teste unitario
        public void TipoDocumentoAcademico_Exclusao() //Colocar o nome da tela a ser testada
        {
            base.ExecuteTest((driver) => //chama o browser e coloca o link correto
            {
                //Maximiza o Browser
                // driverNavigator.Manage().Window.Maximize();
                     Login(driver); // realiza o login como administrador                             
                driver.FindElement(By.Name("Descricao")).Click();
                driver.FindElement(By.Name("Descricao")).Clear();
                driver.FindElement(By.Name("Descricao")).SendKeys("Diploma de Graduação");
                driver.FindElement(By.Id("BotaoPesquisarVP")).Click();
                driver.FindElement(By.Id("DynamicBotaoExcluir1")).Click();
                driver.FindElement(By.Id("BotaoPadraoPerguntaSim")).Click();

                //Checando a mensagem de Sucesso (Verifica se tem a palavra sucesso na mensagem)
                Assert.True(CheckSuccess(), "Era esperado sucesso e ocorreu um erro");
                driver.Driver.Close();

            });

         }

      [Fact] //Deve ser declarada para teste unitario
        public void TipoDocumentoAcademico_Exclusaonaopermitida() //Colocar o nome da tela a ser testada
        {
            base.ExecuteTest((driver) => //chama o browser e coloca o link correto
            {
                //Maximiza o Browser
                // driverNavigator.Manage().Window.Maximize();
                Login(driver); // realiza o login como administrador

                driver.FindElement(By.Name("Descricao")).Click();
                driver.FindElement(By.Name("Descricao")).Clear();
                driver.FindElement(By.Name("Descricao")).SendKeys("Diploma");
                driver.FindElement(By.Id("BotaoPesquisarVP")).Click();
                driver.FindElement(By.XPath("//a[@id='DynamicBotaoExcluir1']/i")).Click();
                driver.FindElement(By.Id("BotaoPadraoPerguntaSim")).Click();
                //Assert.True(CheckSuccess(), "Era esperado sucesso e ocorreu um erro");

                //Declara a variável teste q ira receber o texto do campo
                string
                teste = driver.FindElement(By.XPath("//div[@id='centro']/div/div/div[2]")).GetValue();

                //Compara a mensagem exibida com a esperada:
                Thread.Sleep(1600);
                Assert.Equal("Exclusão não permitida. Existe documento de conclusão cadastrado com este tipo de documento de conclusão.", teste);

                driver.Driver.Close();
            });

        }
      

        [Fact]
         [Trait("Ordenado", "CRUD")]
         public void TesteOrdenadoCRUD()
         {

             TipoDocumentoAcademico_Inclusao();
             TipoDocumentoAcademico_Alteracao();
             TipoDocumentoAcademico_Exclusao();
             TipoDocumentoAcademico_Exclusaonaopermitida();


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
                    driver.GoToUrl2(Consts.SERVIDOR_QUALIDADE_ADM + "CNC/TipoDocumentoAcademico");

                }

            }
}
