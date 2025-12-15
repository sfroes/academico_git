using System;
using System.Linq;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SMC.Framework.Test; //Framework de testes
using System.Threading;
using Xunit; //Usado no Fact

namespace SMC.Academico.Tests.ADM.CNC //Arvore onde esta o arquivo
{

    public class NumeroserieInvalido : SMCSeleniumUnitTest //Declara o nome da classe e recebe a SMCSeleniumUnitTest
    {

        [Fact] //Deve ser declarada para teste unitario
        public void NumeroSerieInvalido_Inclusao() //Colocar o nome da tela a ser testada
        {
            base.ExecuteTest((driver) => //chama o browser e coloca o link correto
            {
                //Maximiza o Browser
                // driverNavigator.Manage().Window.Maximize();

                Login(driver); // realiza o login como administrador

                //Colar o script aqui

                driver.FindElement(By.Id("BotaoNovoVP")).Click();
                driver.FindElement(By.Id("select_SeqTipoDocumentoAcademico")).Click();
                new  SMCSelectElement(driver.FindElement(By.Id("select_SeqTipoDocumentoAcademico"))).SelectByText("Diploma Digital");
                driver.FindElement(By.Id("select_SeqGrupoNumeroSerie")).Click();
                driver.FindElement(By.Id("select_SeqGrupoNumeroSerie")).Click();
                driver.FindElement(By.Id("select_SeqTipoDocumentoAcademico")).Click();
                new SMCSelectElement(driver.FindElement(By.Id("select_SeqTipoDocumentoAcademico"))).SelectByText("Diploma");
                driver.FindElement(By.Id("select_SeqGrupoNumeroSerie")).Click();
                new SMCSelectElement(driver.FindElement(By.Id("select_SeqGrupoNumeroSerie"))).SelectByText("Diplomas - Graduação");
                driver.FindElement(By.XPath("//div[@id='default']/div")).Click();
                driver.FindElement(By.Name("NumeroSerie")).Clear();
                driver.FindElement(By.Name("NumeroSerie")).SendKeys("10");
                driver.FindElement(By.Id("select_SeqMotivoNumeroSerieInvalido")).Click();
                new SMCSelectElement(driver.FindElement(By.Id("select_SeqMotivoNumeroSerieInvalido"))).SelectByText("Defeitos durante a impressão");
                driver.FindElement(By.Name("Observacao")).Click();
                driver.FindElement(By.XPath("//button[@id='btnSalvarVPIN']/i")).Click();

                //Checando a mensagem de Sucesso (compara a mensagem que está descrita com a que vai aparecer no aplicativo)
                Assert.True(CheckMessage("Número de Série Inválido incluído com sucesso."), "Mensagem esperada não exibida");
            });
        }

        [Fact] //Deve ser declarada para teste unitario

        public void NumeroSerieInvalido_InclusaoDuplicadanaopermitida() //Colocar o nome da tela a ser testada
        {
            base.ExecuteTest((driver) => //chama o browser e coloca o link correto
            {
                //Maximiza o Browser
                // driverNavigator.Manage().Window.Maximize();
                Login(driver); // realiza o login como administrador

                //Colar o script aqui

                driver.FindElement(By.Id("BotaoNovoVP")).Click();
                driver.FindElement(By.Id("select_SeqTipoDocumentoAcademico")).Click();
                new SMCSelectElement(driver.FindElement(By.Id("select_SeqTipoDocumentoAcademico"))).SelectByText("Diploma Digital");
                driver.FindElement(By.Id("select_SeqGrupoNumeroSerie")).Click();
                driver.FindElement(By.Id("select_SeqGrupoNumeroSerie")).Click();
                driver.FindElement(By.Id("select_SeqTipoDocumentoAcademico")).Click();
                new SMCSelectElement(driver.FindElement(By.Id("select_SeqTipoDocumentoAcademico"))).SelectByText("Diploma");
                driver.FindElement(By.Id("select_SeqGrupoNumeroSerie")).Click();
                new SMCSelectElement(driver.FindElement(By.Id("select_SeqGrupoNumeroSerie"))).SelectByText("Diplomas - Graduação");
                driver.FindElement(By.XPath("//div[@id='default']/div")).Click();
                driver.FindElement(By.Name("NumeroSerie")).Clear();
                driver.FindElement(By.Name("NumeroSerie")).SendKeys("10");
                driver.FindElement(By.Id("select_SeqMotivoNumeroSerieInvalido")).Click();
                new SMCSelectElement(driver.FindElement(By.Id("select_SeqMotivoNumeroSerieInvalido"))).SelectByText("Defeitos durante a impressão");
                driver.FindElement(By.Name("Observacao")).Click();
                driver.FindElement(By.XPath("//button[@id='btnSalvarVPIN']/i")).Click();
                //Checando a mensagem de Sucesso (compara a mensagem que está descrita com a que vai aparecer no aplicativo)
                Assert.True(CheckMessage("Operação não permitida. Este número de série já foi invalidado neste grupo de número de série."), "Mensagem esperada não exibida");

            });
        }

        [Fact] //Deve ser declarada para teste unitario

        public void NumeroSerieInvalido_Alteracao() //Colocar o nome da tela a ser testada
        {
            base.ExecuteTest((driver) => //chama o browser e coloca o link correto
            {
                //Maximiza o Browser
                // driverNavigator.Manage().Window.Maximize();
                Login(driver); // realiza o login como administrador

                //Colar o script aqui

                //driver.FindElement(By.XPath("//ul[@id='ulMegaMenuPainelMenuLevel221']/li[5]/ul/li[4]/a")).Click();
                driver.FindElement(By.Name("NumeroSerie")).Clear();
                driver.FindElement(By.Name("NumeroSerie")).SendKeys("10");
                driver.FindElement(By.Id("BotaoPesquisarVP")).Click();
                driver.FindElement(By.XPath("//button[@id='DynamicBotaoEdit0']/i")).Click();
                driver.FindElement(By.Id("select_SeqMotivoNumeroSerieInvalido")).Click();
                new SMCSelectElement(driver.FindElement(By.Id("select_SeqMotivoNumeroSerieInvalido"))).SelectByText("Erros ao inserir carimbo no verso do documento");
                driver.FindElement(By.XPath("//button[@id='btnSalvarVPIE']/i")).Click();
                driver.FindElement(By.Id("select_SeqGrupoNumeroSerie")).Click();
                new SMCSelectElement(driver.FindElement(By.Id("select_SeqGrupoNumeroSerie"))).SelectByText("Diplomas - Graduação");
                driver.FindElement(By.Id("btnSalvarVPIE")).Click();


                Assert.True(CheckMessage("Número de Série Inválido alterado com sucesso."), "Mensagem esperada não exibida");

            });
        }


        [Fact] //Deve ser declarada para teste unitario

        public void NumeroSerieInvalido_Exclusao() //Colocar o nome da tela a ser testada
        {
            base.ExecuteTest((driver) => //chama o browser e coloca o link correto
            {
                //Maximiza o Browser
                // driverNavigator.Manage().Window.Maximize();
                Login(driver); // realiza o login como administrador

                //Colar o script aqui

                driver.FindElement(By.XPath("//a[@id='DynamicBotaoExcluir1']/i")).Click();
                driver.FindElement(By.Id("BotaoPadraoPerguntaSim")).Click();

                //Checando a mensagem de Sucesso (compara a mensagem que está descrita com a que vai aparecer no aplicativo)
                Assert.True(CheckMessage("Número de Série Inválido excluído com sucesso."), "Mensagem esperada não exibida");

            });
        }


       /* [Fact]
        // [Trait("Ordenado", "CRUD")]
        public void TesteOrdenadoCRUD()

        {
            NumeroSerieInvalido_Inclusao();
            NumeroSerieInvalido_InclusaoDuplicadanaopermitida();
            NumeroSerieInvalido_Alteracao();
            NumeroSerieInvalido_Exclusao();


        }*/

        private static void Login(ISMCWebDriver driver)
        {
            //----------------------------------------------------
            driver.GoToUrl(Consts.SERVIDOR_QUALIDADE_ADM); //recebe o link para acesso

            //Login
            driver.SMCLoginCpf(); //insere login e senha

            driver.GoToUrl2(Consts.SERVIDOR_QUALIDADE_ADM + "CNC/NumeroSerieInvalido"); //coloca o resto do endere�o para acessar a pagina do teste

        }
    }
}

