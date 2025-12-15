using System;
using System.Linq;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SMC.Framework.Test; //Framework de testes
using System.Threading;
using Xunit; //Usado no Fact

namespace SMC.Academico.Tests.ADM.CNC //Arvore onde esta o arquivo
{

    public class Equivalencia : SMCSeleniumUnitTest //Declara o nome da classe e recebe a SMCSeleniumUnitTest
    {
              
       
        [Fact] //Deve ser declarada para teste unitario
        public void Equivalencia_Inclusao() //Colocar o nome da tela a ser testada
        {
            base.ExecuteTest((driver) => //chama o browser e coloca o link correto
            {
                //Maximiza o Browser
                // driverNavigator.Manage().Window.Maximize();

                Login(driver); // realiza o login como administrador

                //Colar o script aqui

              
                Thread.Sleep(6000);
                driver.Driver.Close();
            });
        }


        [Fact]
        public void Equivalencia_2_Inserir_CamposObrigatorios()
        {
            base.ExecuteTest((driver) =>
            {
                //Maximiza o Browser
                // driverNavigator.Manage().Window.Maximize();

                Login(driver);
                //-------------------------------------------------------------------------------------------------
                driver.FindElement(By.Id("BotaoNovoVP")).Click();
                driver.FindElement(By.Id("btnSalvarVPIN")).Click();
                //Checando mensagem de preenchimento obrigatório
                driver.FindElement(By.Name("Descricao")).CheckErrorMessage("Preenchimento obrigatório");
                driver.FindElement(By.Name("Token")).CheckErrorMessage("Preenchimento obrigatório");

                //Para fechar o Chrome em segundo plano
                driver.Driver.Close();
            });
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
            //wait.Until(ExpectedConditions.TextToBePresentInElementLocated(By.XPath("//div[@id='divConteudoPrincipal']/section/h2"), "SGA.Administrativo"));
            driver.GoToUrl2(Consts.SERVIDOR_QUALIDADE_ADM + "/CUR/Dispensa");

        }
    }
}
