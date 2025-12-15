using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using ExpectedConditions = SeleniumExtras.WaitHelpers.ExpectedConditions;
using SMC.Framework.Test; //Framework de testes
using System.Threading;
using System;
using Xunit; //Usado no Fact

namespace SMC.Academico.Tests.ADM.CNC //Arvore onde esta o arquivo
{
    public class GrupoRegistro : SMCSeleniumUnitTest //Declara o nome da classe e recebe a SMCSeleniumUnitTest
    {

        [Fact] //Deve ser declarada para teste unitario
        public void GrupoRegistro_Inclusao() //Colocar o nome da tela a ser testada
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
                driver.FindElement(By.Name("Descricao")).SendKeys("Pós Graduação - Diploma Digital");
                driver.FindElement(By.Name("NumeroUltimoRegistro")).Click();
                driver.FindElement(By.Name("NumeroUltimoRegistro")).Clear();
                driver.FindElement(By.Name("NumeroUltimoRegistro")).SendKeys("100");
                driver.FindElement(By.Id("btnSalvarVPIN")).Click();

                //Checando a mensagem de Sucesso (compara a mensagem que está descrita com a que vai aparecer no aplicativo)
                Assert.True(CheckMessage("Grupo Registro incluído com sucesso."), "Mensagem esperada não exibida");
                driver.Driver.Close();

            });

        }

        [Fact] //Deve ser declarada para teste unitario
        public void GrupoRegistroInclusaoDuplicada() //Colocar o nome da tela a ser testada
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
                driver.FindElement(By.Name("Descricao")).SendKeys("Pós Graduação - Diploma Digital");
                driver.FindElement(By.Name("NumeroUltimoRegistro")).Click();
                driver.FindElement(By.Name("NumeroUltimoRegistro")).Clear();
                driver.FindElement(By.Name("NumeroUltimoRegistro")).SendKeys("100");
                driver.FindElement(By.Id("btnSalvarVPIN")).Click();

                //Checando a mensagem de Sucesso (compara a mensagem que está descrita com a que vai aparecer no aplicativo)
                Assert.True(CheckMessage("Já existe grupo de registro com esta descrição nesta instituição de ensino."), "Mensagem esperada não exibida");
                driver.Driver.Close();
            });

        }
            
         [Fact] //Deve ser declarada para teste unitario
         public void GrupoRegistro_Alteracao() //Colocar o nome da tela a ser testada
             {
         base.ExecuteTest((driver) => //chama o browser e coloca o link correto
         {
        //Maximiza o Browser
        // driverNavigator.Manage().Window.Maximize();
        Login(driver); // realiza o login como administrador

        //Colar o script aqui

          driver.FindElement(By.Name("Descricao")).Click();
          driver.FindElement(By.Name("Descricao")).Clear();
          driver.FindElement(By.Name("Descricao")).SendKeys("Pós");
          driver.FindElement(By.Id("BotaoPesquisarVP")).Click();
          driver.FindElement(By.XPath("//button[@id='DynamicBotaoEdit0']/i")).Click();
          driver.FindElement(By.Name("Descricao")).Click();
          driver.FindElement(By.Name("Descricao")).Clear();
          driver.FindElement(By.Name("Descricao")).SendKeys("Pós Graduação - Diploma");
          driver.FindElement(By.Id("btnSalvarVPIE")).Click();
          Assert.True(CheckMessage("Grupo Registro alterado com sucesso."), "Mensagem esperada não exibida");
          driver.Driver.Close();

         });

         }
         

           [Fact] //Deve ser declarada para teste unitario
           public void GrupoRegistro_Exclusao() //Colocar o nome da tela a ser testada
           {
           base.ExecuteTest((driver) => //chama o browser e coloca o link correto
           {
           //Maximiza o Browser
           // driverNavigator.Manage().Window.Maximize();
           Login(driver); // realiza o login como administrador

               //Colar o script aqui

               driver.FindElement(By.Name("Descricao")).Click();
               driver.FindElement(By.Name("Descricao")).Clear();
               driver.FindElement(By.Name("Descricao")).SendKeys("Pós");
               driver.FindElement(By.Id("BotaoPesquisarVP")).Click();
               driver.FindElement(By.XPath("//a[@id='DynamicBotaoExcluir1']/i")).Click();
               driver.FindElement(By.Id("BotaoPadraoPerguntaSim")).Click();
               Assert.True(CheckMessage("Grupo Registro excluído com sucesso"), "Mensagem esperada não exibida");
               driver.Driver.Close();
           });

        }


        [Fact]
        [Trait("Ordenado", "CRUD")]
           public void TesteOrdenadoCRUD()
         {

            GrupoRegistro_Inclusao();
            GrupoRegistroInclusaoDuplicada();
            GrupoRegistro_Alteracao();
            GrupoRegistro_Exclusao();

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
                driver.GoToUrl2(Consts.SERVIDOR_QUALIDADE_ADM + "CNC/GrupoRegistro");

            }

        }
    }

