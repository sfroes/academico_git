using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SMC.Framework.Test; //Framework de testes
using System;
using System.Threading;
using Xunit; //Usado no Fact

namespace SMC.Academico.Tests.PROF.TURMAS //Arvore onde esta o arquivo
{

    public class Lancamento_Notas : SMCSeleniumUnitTest
    {


        [Fact]
        public void IncluiProva() //Colocar o nome da tela ou processo a ser automatizado
        {

            //var diaatual = new DateTime(DateTime.Today.Year, DateTime.Today.Month, DateTime.Today.Day, DateTime.Today.Hour, DateTime.Today.Minute);

            var diaposterior = new DateTime(DateTime.Today.Year, DateTime.Today.Month, DateTime.Today.Day).AddDays(+1);
            var dataatual = DateTime.Now.AddMinutes(+2);
            base.ExecuteTest((driver) => //chama o browser e coloca o link correto
            {

              //  var dataatual = new DateTime.Now.ToString();
                Login(driver); // realiza o login como administrador
                
                driver.FindElement(By.Id("ButtonSetDropdown2")).Click();
             
                driver.FindElement(By.CssSelector("[data-behavior='Avaliacao']")).Click();
                driver.FindElement(By.CssSelector("[data-behavior='smc-behavior-novo']")).Click();
                //driver.Driver.FindBySelector("[data-behavior='smc-behavior-novo']").Click();

                driver.FindElement(By.Id("select_TipoAvaliacao")).Click();
                new SMCSelectElement(driver.FindElement(By.Id("select_TipoAvaliacao"))).SelectByText("Prova");
                driver.FindElement(By.Id("select_TipoAvaliacao")).Click();
                driver.FindElement(By.Name("Descricao")).Click();
                driver.FindElement(By.Name("Descricao")).Clear();
                driver.FindElement(By.Name("Descricao")).SendKeys("Prova teste");
                driver.FindElement(By.Name("Valor")).Click();
                driver.FindElement(By.Name("Valor")).Clear();
                driver.FindElement(By.Name("Valor")).SendKeys("10");
                driver.FindElement(By.Id("select_EntregaWeb")).Click();
                new SMCSelectElement(driver.FindElement(By.Id("select_EntregaWeb"))).SelectByText("Sim");
                driver.FindElement(By.Id("select_EntregaWeb")).Click();
                driver.FindElement(By.Name("DataInicioAplicacaoAvaliacao")).Click();
                driver.FindElement(By.Name("DataInicioAplicacaoAvaliacao")).Clear();
                driver.FindElement(By.Name("DataInicioAplicacaoAvaliacao")).SendKeys(dataatual.ToString("dd/MM/yyyy HH:mm"));
                //driver.FindElement(By.Name("DataInicioAplicacaoAvaliacao")).SendKeys(DateTime.Now.ToString("dd/MM/yyyy HH:mm"));
                driver.FindElement(By.Name("DataFimAplicacaoAvaliacao")).SendKeys(diaposterior.ToString("dd/MM/yyyy HH:mm"));
                driver.FindElement(By.Name("QuantidadeMaximaPessoasGrupo")).Click();
                driver.FindElement(By.Name("QuantidadeMaximaPessoasGrupo")).Clear();
                driver.FindElement(By.Name("QuantidadeMaximaPessoasGrupo")).SendKeys("99");
                driver.FindElement(By.Id("BotaoSalvarTemplate")).Click();
                Assert.True(CheckSuccess(), "Era esperado sucesso e ocorreu um erro");

            });
        }

        [Fact]
        public void LancaNotaAvaliacao() //Colocar o nome da tela ou processo a ser automatizado
        {
            base.ExecuteTest((driver) => //chama o browser e coloca o link correto
            {

                //  var dataatual = new DateTime.Now.ToString();
                Login(driver); // realiza o login como administrador
                driver.FindElement(By.Id("ButtonSetDropdown2")).Click();
                driver.FindElement(By.CssSelector("[data-behavior='LancamentoNota']")).Click();
                driver.FindElement(By.CssSelector("[Id='nota.0.0']")).Click();
                driver.FindElement(By.CssSelector("[Id='nota.0.0']>po-field-container>div.po-field-container>div.po-field-container-content>input.po-input")).Clear();
                driver.FindElement(By.CssSelector("[Id='nota.0.0']>po-field-container>div.po-field-container>div.po-field-container-content>input.po-input")).SendKeys("8");
                driver.FindElement(By.CssSelector("[title='Salvar']")).Click();
                //Criei uma espera e com isso e depois verifica se dentro da div que esta o conteudo 
                //Caso de erro ele ira - Timed out after 3 seconds
                //WebDriverWait wait = new WebDriverWait((OpenQA.Selenium.IWebDriver)driver, TimeSpan.FromSeconds(2));
                //wait.Until(ExpectedConditions.TextToBePresentInElementLocated(By.CssSelector("div.po-toaster-success>div.po-toaster-message"), "Lançamento gravado com sucesso!"));
                CheckSuccessAngular();
                //Thread.Sleep(900);
                //string mensagem = driver.FindElement(By.CssSelector("[class='po-clickable po-toaster po-toaster-success po-toaster-top']>[class='po-toaster-message']>span.po-icon-ok.class")).GetValue();
                ////string mensagem = driver.FindElement(By.CssSelector("[div.class='po-clickable po-toaster po-toaster-success po-toaster-top']>div.class>span.class")).GetValue();
                ////Compara a mensagem exibida com a esperada:
                //Assert.Equal("Preenchimento obrigatório", mensagem);

                //  Assert.True(CheckSuccess(), "Era esperado sucesso e ocorreu um erro");*/
            });
        }
        private static void Login(ISMCWebDriver driver)
        {
            //----------------------------------------------------
            driver.GoToUrl(Consts.SERVIDOR_QUALIDADE_PROFESSOR); //recebe o link para acesso
     
            driver.SMCLoginProfessor(); //insere login e senha
       
        }
    }
}
