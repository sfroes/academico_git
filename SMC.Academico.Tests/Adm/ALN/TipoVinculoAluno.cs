using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using ExpectedConditions = SeleniumExtras.WaitHelpers.ExpectedConditions;
using SMC.Framework.Test; //Framework de testes
using System.Threading;
using System;
using Xunit; //Usado no Fact


namespace SMC.Academico.Tests.ADM.ALN //Arvore onde esta o arquivo
{

    public class TipoVinculoAluno : SMCSeleniumUnitTest //Declara o nome da classe e recebe a SMCSeleniumUnitTest
    {

        [Fact] //Deve ser declarada para teste unitario

        public void TipoVinculoAluno_Inclusao() //Colocar o nome da tela a ser testada
        {
            base.ExecuteTest((driver) => //chama o browser e coloca o link correto
            {
            //Maximiza o Browser
            // driverNavigator.Manage().Window.Maximize();
            Login(driver); // realiza o login como administrador

                //Colar o script aqui
                driver.FindElement(By.Id("BotaoNovoVP")).Click();
                driver.FindElement(By.Name("Descricao")).Clear();
                driver.FindElement(By.Name("Descricao")).SendKeys("Curso Regular II");
                driver.FindElement(By.Id("select_TipoVinculoAlunoFinanceiro")).Click();
                new SMCSelectElement(driver.FindElement(By.Id("select_TipoVinculoAlunoFinanceiro"))).SelectByText("Curso Regular");
                driver.FindElement(By.Name("FormasIngresso[0].Descricao")).Click();
                driver.FindElement(By.Name("FormasIngresso[0].Descricao")).Clear();
                driver.FindElement(By.Name("FormasIngresso[0].Descricao")).SendKeys("Vestibular");
                driver.FindElement(By.Name("FormasIngresso[0].Token")).Click();
                driver.FindElement(By.Name("FormasIngresso[0].Token")).Clear();
                driver.FindElement(By.Name("FormasIngresso[0].Token")).SendKeys("ABD");
                driver.FindElement(By.Id("select_FormasIngresso_0__TipoFormaIngresso")).Click();
                Thread.Sleep(1600);
                new SMCSelectElement(driver.FindElement(By.Id("select_FormasIngresso_0__TipoFormaIngresso"))).SelectByText("Processo seletivo externo");
                driver.FindElement(By.Id("BotaoSalvarTemplate")).Click();


                //Checando a mensagem de Sucesso (compara a mensagem que está descrita com a que vai aparecer no aplicativo)
                Assert.True(CheckMessage("Tipo de vínculo incluído com sucesso."), "Mensagem esperada não exibida");

                //Para fechar o Chrome em segundo plano
                driver.Driver.Close();

            });
        }

        [Fact] //Deve ser declarada para teste unitario

        public void TipoVinculoAluno_InclusaoDuplicada() //Colocar o nome da tela a ser testada
        {
            base.ExecuteTest((driver) => //chama o browser e coloca o link correto
            {
                //Maximiza o Browser
                // driverNavigator.Manage().Window.Maximize();
                Login(driver); // realiza o login como administrador

                //Colar o script aqui
                driver.FindElement(By.Id("BotaoNovoVP")).Click();
                driver.FindElement(By.Name("Descricao")).Clear();
                driver.FindElement(By.Name("Descricao")).SendKeys("Curso Regular II");
                driver.FindElement(By.Id("select_TipoVinculoAlunoFinanceiro")).Click();
                new SMCSelectElement(driver.FindElement(By.Id("select_TipoVinculoAlunoFinanceiro"))).SelectByText("Curso Regular");
                driver.FindElement(By.Name("FormasIngresso[0].Descricao")).Click();
                driver.FindElement(By.Name("FormasIngresso[0].Descricao")).Clear();
                driver.FindElement(By.Name("FormasIngresso[0].Descricao")).SendKeys("Vestibular");
                driver.FindElement(By.Name("FormasIngresso[0].Token")).Click();
                driver.FindElement(By.Name("FormasIngresso[0].Token")).Clear();
                driver.FindElement(By.Name("FormasIngresso[0].Token")).SendKeys("ABD");
                driver.FindElement(By.Id("select_FormasIngresso_0__TipoFormaIngresso")).Click();
                new SMCSelectElement(driver.FindElement(By.Id("select_FormasIngresso_0__TipoFormaIngresso"))).SelectByText("Processo seletivo externo");
                driver.FindElement(By.Id("BotaoSalvarTemplate")).Click();


                //Checando a mensagem de Sucesso (compara a mensagem que está descrita com a que vai aparecer no aplicativo)
                Assert.True(CheckMessage("Já existe tipo de vínculo com esta descrição"), "Mensagem esperada não exibida");

                //Para fechar o Chrome em segundo plano
                driver.Driver.Close();

            });
        }

        [Fact] //Deve ser declarada para teste unitario

        public void TipoVinculoAluno_Alteracao() //Colocar o nome da tela a ser testada
        {
            base.ExecuteTest((driver) => //chama o browser e coloca o link correto
            {
                //Maximiza o Browser
                // driverNavigator.Manage().Window.Maximize();
                Login(driver); // realiza o login como administrador

                //Colar o script aqui

                driver.FindElement(By.Name("Descricao")).Click();
                driver.FindElement(By.Name("Descricao")).Clear();
                driver.FindElement(By.Name("Descricao")).SendKeys("Curso Regular II");
                driver.FindElement(By.Id("BotaoPesquisarVP")).Click();
                driver.FindElement(By.XPath("//a[@id='DetailListBotaoAlterar0']/i")).Click();
                driver.FindElement(By.Name("Descricao")).Click();
                driver.FindElement(By.Name("Descricao")).Clear();
                driver.FindElement(By.Name("Descricao")).SendKeys("Curso Regular I");
                driver.FindElement(By.XPath("//button[@id='BotaoSalvarTemplate']/i")).Click();

                //Checando a mensagem de Sucesso (compara a mensagem que está descrita com a que vai aparecer no aplicativo)
                Assert.True(CheckMessage("Tipo de vínculo alterado com sucesso."), "Mensagem esperada não exibida");

                //Para fechar o Chrome em segundo plano
                driver.Driver.Close();

            });
        }

        [Fact] //Deve ser declarada para teste unitario

        public void TipoVinculoAluno_Exclusao() //Colocar o nome da tela a ser testada
        {
            base.ExecuteTest((driver) => //chama o browser e coloca o link correto
            {
                //Maximiza o Browser
                // driverNavigator.Manage().Window.Maximize();
                Login(driver); // realiza o login como administrador

                //Colar o script aqui

                driver.FindElement(By.Name("Descricao")).Click();
                driver.FindElement(By.Name("Descricao")).Clear();
                driver.FindElement(By.Name("Descricao")).SendKeys("Curso Regular I");
                driver.FindElement(By.Id("BotaoPesquisarVP")).Click();
                driver.FindElement(By.XPath("//a[@id='DetailListBotaoExcluir0']/i")).Click();
                driver.FindElement(By.Id("BotaoPadraoPerguntaSim")).Click();

                //Checando a mensagem de Sucesso (compara a mensagem que está descrita com a que vai aparecer no aplicativo)
                Assert.True(CheckMessage("Tipo de vínculo excluído com sucesso."), "Mensagem esperada não exibida");

                //Para fechar o Chrome em segundo plano
                driver.Driver.Close();
            });
        }

        [Fact] //Deve ser declarada para teste unitario

            public void TipoVinculoAluno_ExclusaoNaoPermitida() //Colocar o nome da tela a ser testada
            {
                base.ExecuteTest((driver) => //chama o browser e coloca o link correto
                {
                    //Maximiza o Browser
                    // driverNavigator.Manage().Window.Maximize();
                    Login(driver); // realiza o login como administrador

                    //Colar o script aqui

                    driver.FindElement(By.XPath("//button[@id='BotaoLimparVP']/i")).Click();
                    driver.FindElement(By.Name("Descricao")).Click();
                    driver.FindElement(By.Name("Descricao")).Clear();
                    driver.FindElement(By.Name("Descricao")).SendKeys("Curso Regular");
                    driver.FindElement(By.Id("BotaoPesquisarVP")).Click();
                    driver.FindElement(By.XPath("//a[@id='DetailListBotaoExcluir0']/i")).Click();
                    driver.FindElement(By.Id("BotaoPadraoPerguntaSim")).Click();

                    //Checando a mensagem de Sucesso (compara a mensagem que está descrita com a que vai aparecer no aplicativo)
                    Assert.True(CheckMessage("Exclusão não permitida. Já existe processo seletivo cadastrado com esta forma de ingresso."), "Mensagem esperada não exibida");

                    //Para fechar o Chrome em segundo plano
                    driver.Driver.Close();


                });
        }

        [Fact]
        [Trait("Ordenado", "CRUD")]
        public void TesteOrdenadoCRUD()
        {
            TipoVinculoAluno_Inclusao();
            TipoVinculoAluno_InclusaoDuplicada();
            TipoVinculoAluno_Alteracao();
            TipoVinculoAluno_Exclusao();
            TipoVinculoAluno_ExclusaoNaoPermitida();
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
                driver.GoToUrl2(Consts.SERVIDOR_QUALIDADE_ADM + "ALN/TipoVinculoAluno");

            }
        }
    }
