using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using ExpectedConditions = SeleniumExtras.WaitHelpers.ExpectedConditions;
using SMC.Framework.Test; //Framework de testes
using System;
using System.Threading;
using Xunit; //Usado no Fact

namespace SMC.Academico.Tests.ADM //Arvore onde esta o arquivo
{

    public class LoginServidor: SMCSeleniumUnitTest
    {


        [Fact]
        [Trait ("Login", "Abrealas")]
        public void Login_Servidor() //Colocar o nome da tela ou processo a ser automatizado
        {
            base.ExecuteTest((driver) => //chama o browser e coloca o link correto
            {
                driver.GoToUrl(Consts.SERVIDOR_QUALIDADE_ADM);
                WebDriverWait wait = new WebDriverWait((OpenQA.Selenium.IWebDriver)driver, TimeSpan.FromSeconds(3500));
                //IWebElement firstResult = 
                wait.Until(e => e.FindElement(By.Name("LoginCpf")));
                driver.SMCLoginCpf();
              //  driver.GoToUrl2(Consts.SERVIDOR_QUALIDADE_ADM + "ALN/Aluno");
                wait.Until(ExpectedConditions.TextToBePresentInElementLocated(By.XPath("//div[@id='divConteudoPrincipal']/section/h2"), "SGA.Administrativo"));

              //  WebDriverWait wait = new WebDriverWait((OpenQA.Selenium.IWebDriver)driver, TimeSpan.FromSeconds(2));
              //  wait.Until(ExpectedConditions.TextToBePresentInElementLocated(By.XPath("//div[@id='divConteudoPrincipal']/section/h2"), "SGA.Administrativo"));



            });
        }
      /*  private static void Login(ISMCWebDriver driver)
        {
            //----------------------------------------------------
            driver.GoToUrl(Consts.SERVIDOR_QUALIDADE_ADM); //recebe o link para acesso

            //Login
            driver.SMCLoginCpf(); //insere login e senha

            driver.GoToUrl2(Consts.SERVIDOR_QUALIDADE_ADM); //coloca o resto do endereço para acessar a pagina do teste

        }*/
    }
}
