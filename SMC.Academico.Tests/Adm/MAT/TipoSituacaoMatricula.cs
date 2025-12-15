using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using ExpectedConditions = SeleniumExtras.WaitHelpers.ExpectedConditions;
using SMC.Framework.Test; //Framework de testes
using System.Threading;
using System;
using Xunit; //Usado no Fact


namespace SMC.Academico.Tests.ADM.MAT //Arvore onde esta o arquivo
{

    public class TipoSituacaoMatricula : SMCSeleniumUnitTest //Declara o nome da classe e recebe a SMCSeleniumUnitTest
    {

        [Fact] //Deve ser declarada para teste unitario

        public void TipoSituacaoMatricula_Inclusao() //Colocar o nome da tela a ser testada
        {
            base.ExecuteTest((driver) => //chama o browser e coloca o link correto
            {
                //Maximiza o Browser
                // driverNavigator.Manage().Window.Maximize();
                Login(driver); // realiza o login como administrador

                //Colar o script aqui
                                
                driver.FindElement(By.Id("BotaoNovoVP")).Click();
                driver.FindElement(By.Name("Descricao")).Clear();
                driver.FindElement(By.Name("Descricao")).SendKeys("Apto para Matrícula 2022");
                driver.FindElement(By.Id("select_VinculoAlunoAtivo")).Click();
                new SMCSelectElement(driver.FindElement(By.Id("select_VinculoAlunoAtivo"))).SelectByText("Sim");
                driver.FindElement(By.Name("Token")).Click();
                driver.FindElement(By.Name("Token")).Clear();
                driver.FindElement(By.Name("Token")).SendKeys("Apto para Matrícula 2022");
                driver.FindElement(By.Name("SituacoesMatricula[0].Descricao")).Click();
                driver.FindElement(By.Name("SituacoesMatricula[0].Descricao")).Clear();
                driver.FindElement(By.Name("SituacoesMatricula[0].Descricao")).SendKeys("Apto para Matrícula 2022");
                driver.FindElement(By.Name("SituacoesMatricula[0].Token")).Click();
                driver.FindElement(By.Name("SituacoesMatricula[0].Token")).Clear();
                driver.FindElement(By.Name("SituacoesMatricula[0].Token")).SendKeys("Apto_Matricula_2022");
                driver.FindElement(By.XPath("//button[@id='BotaoSalvarTemplate']/i")).Click();
                driver.FindElement(By.Name("SituacoesMatricula[0].Token")).Click();
                driver.FindElement(By.Name("SituacoesMatricula[0].Token")).Click();
                driver.FindElement(By.XPath("//div[@id='SituacoesMatricula']/div[3]/div[2]/div/div")).Click();
                driver.FindElement(By.Name("SituacoesMatricula[0].Token")).Clear();
                driver.FindElement(By.Name("SituacoesMatricula[0].Token")).SendKeys("ABC");
                driver.FindElement(By.Id("BotaoSalvarTemplate")).Click();
                driver.FindElement(By.Name("Token")).Click();
                driver.FindElement(By.Name("Token")).Click();
                driver.FindElement(By.Name("Token")).Clear();
                driver.FindElement(By.Name("Token")).SendKeys("Apto_Matrícula");
                driver.FindElement(By.Id("BotaoSalvarTemplate")).Click();
                driver.FindElement(By.Id("formEditViewPadrao")).Click();
                driver.FindElement(By.Name("Token")).Clear();
                driver.FindElement(By.Name("Token")).SendKeys("ABC");
                driver.FindElement(By.Id("BotaoSalvarTemplate")).Click();

                //Checando a mensagem de Sucesso (compara a mensagem que está descrita com a que vai aparecer no aplicativo)
                Assert.True(CheckMessage("Tipo de situação de matrícula incluído com sucesso."), "Mensagem esperada não exibida");
                driver.Driver.Close();

            });
        }
        [Fact] //Deve ser declarada para teste unitario

        public void TipoSituacaoMatricula_InclusaoDuplicada() //Colocar o nome da tela a ser testada
        {
            base.ExecuteTest((driver) => //chama o browser e coloca o link correto
            {
                //Maximiza o Browser
                // driverNavigator.Manage().Window.Maximize();
                Login(driver); // realiza o login como administrador

                //Colar o script aqui

                driver.FindElement(By.Id("BotaoNovoVP")).Click();
                driver.FindElement(By.Name("Descricao")).Clear();
                driver.FindElement(By.Name("Descricao")).SendKeys("Apto para Matrícula 2022");
                driver.FindElement(By.Id("select_VinculoAlunoAtivo")).Click();
                new SMCSelectElement(driver.FindElement(By.Id("select_VinculoAlunoAtivo"))).SelectByText("Sim");
                driver.FindElement(By.Name("Token")).Click();
                driver.FindElement(By.Name("Token")).Clear();
                driver.FindElement(By.Name("Token")).SendKeys("Apto para Matrícula 2022");
                driver.FindElement(By.Name("SituacoesMatricula[0].Descricao")).Click();
                driver.FindElement(By.Name("SituacoesMatricula[0].Descricao")).Clear();
                driver.FindElement(By.Name("SituacoesMatricula[0].Descricao")).SendKeys("Apto para Matrícula 2022");
                driver.FindElement(By.Name("SituacoesMatricula[0].Token")).Click();
                driver.FindElement(By.Name("SituacoesMatricula[0].Token")).Clear();
                driver.FindElement(By.Name("SituacoesMatricula[0].Token")).SendKeys("Apto_Matricula_2022");
                driver.FindElement(By.XPath("//button[@id='BotaoSalvarTemplate']/i")).Click();
                driver.FindElement(By.Name("SituacoesMatricula[0].Token")).Click();
                driver.FindElement(By.Name("SituacoesMatricula[0].Token")).Click();
                driver.FindElement(By.XPath("//div[@id='SituacoesMatricula']/div[3]/div[2]/div/div")).Click();
                driver.FindElement(By.Name("SituacoesMatricula[0].Token")).Clear();
                driver.FindElement(By.Name("SituacoesMatricula[0].Token")).SendKeys("ABC");
                driver.FindElement(By.Id("BotaoSalvarTemplate")).Click();
                driver.FindElement(By.Name("Token")).Click();
                driver.FindElement(By.Name("Token")).Click();
                driver.FindElement(By.Name("Token")).Clear();
                driver.FindElement(By.Name("Token")).SendKeys("Apto_Matrícula");
                driver.FindElement(By.Id("BotaoSalvarTemplate")).Click();
                driver.FindElement(By.Id("formEditViewPadrao")).Click();
                driver.FindElement(By.Name("Token")).Clear();
                driver.FindElement(By.Name("Token")).SendKeys("ABC");
                driver.FindElement(By.Id("BotaoSalvarTemplate")).Click();


                //Checando a mensagem de Sucesso (compara a mensagem que está descrita com a que vai aparecer no aplicativo)
                Assert.True(CheckMessage("Já existe tipo de situação de matrícula com este token."), "Mensagem esperada não exibida");
                driver.Driver.Close();

            });
        }

        [Fact] //Deve ser declarada para teste unitario

        public void TipoSituacaoMatricula_Alteracao() //Colocar o nome da tela a ser testada
        {
            base.ExecuteTest((driver) => //chama o browser e coloca o link correto
            {
                //Maximiza o Browser
                // driverNavigator.Manage().Window.Maximize();
                Login(driver); // realiza o login como administrador

                //Colar o script aqui

                driver.FindElement(By.XPath("//div[@id='divMegaMenu']/ul/li/a")).Click();
                driver.FindElement(By.XPath("//ul[@id='ulMegaMenuPainelMenuLevel211']/li[4]/ul/li[2]/a")).Click();
                driver.FindElement(By.Name("Descricao")).Click();
                driver.FindElement(By.Name("Descricao")).Clear();
                driver.FindElement(By.Name("Descricao")).SendKeys("Apto para Matrícula 2022");
                driver.FindElement(By.Id("BotaoPesquisarVP")).Click();
                driver.FindElement(By.XPath("//a[@id='DetailListBotaoAlterar0']/i")).Click();
                driver.FindElement(By.Name("Token")).Click();
                driver.FindElement(By.Name("Token")).Clear();
                driver.FindElement(By.Name("Token")).SendKeys("ABD");
                driver.FindElement(By.Name("SituacoesMatricula[0].Token")).Click();
                driver.FindElement(By.Name("SituacoesMatricula[0].Token")).Clear();
                driver.FindElement(By.Name("SituacoesMatricula[0].Token")).SendKeys("ABD");
                driver.FindElement(By.Id("BotaoSalvarTemplate")).Click();

                Assert.True(CheckMessage("Tipo de situação de matrícula alterado com sucesso."), "Mensagem esperada não exibida");
                driver.Driver.Close();

            });
        }
     

        [Fact] //Deve ser declarada para teste unitario

        public void TipoSituacaoMatricula_Exclusao() //Colocar o nome da tela a ser testada
        {
            base.ExecuteTest((driver) => //chama o browser e coloca o link correto
            {
                //Maximiza o Browser
                // driverNavigator.Manage().Window.Maximize();
                Login(driver); // realiza o login como administrador

                //Colar o script aqui

                driver.FindElement(By.Name("Descricao")).Click();
                driver.FindElement(By.Name("Descricao")).Clear();
                driver.FindElement(By.Name("Descricao")).SendKeys("Apto para Matrícula 2022");
                driver.FindElement(By.Id("BotaoPesquisarVP")).Click();
                driver.FindElement(By.XPath("//a[@id='DetailListBotaoExcluir0']/i")).Click();
                driver.FindElement(By.Id("BotaoPadraoPerguntaSim")).Click();


                //Checando a mensagem de Sucesso (Verifica se tem a palavra sucesso na mensagem)
                Assert.True(CheckSuccess(), "Tipo de situação de matrícula excluído com sucesso.");
                driver.Driver.Close();

            });
        }


        [Fact] //Deve ser declarada para teste unitario

        public void TipoSituacaoMatricula_Exclusaonaopermitida() //Colocar o nome da tela a ser testada
        {
            base.ExecuteTest((driver) => //chama o browser e coloca o link correto
            {
                //Maximiza o Browser
                // driverNavigator.Manage().Window.Maximize();
                Login(driver); // realiza o login como administrador

                //Colar o script aqui

                driver.FindElement(By.Name("Descricao")).Click();
                driver.FindElement(By.Name("Descricao")).Clear();
                driver.FindElement(By.Name("Descricao")).SendKeys("apto para matrícula");
                driver.FindElement(By.Id("BotaoPesquisarVP")).Click();
                driver.FindElement(By.XPath("//a[@id='DetailListBotaoExcluir0']/i")).Click();
                driver.FindElement(By.Id("BotaoPadraoPerguntaSim")).Click();

                //Assert.True(CheckMessage("Exclusão não permitida. Titulacao já foi associado(a) a outra entidade.FK_curso_formacao_especifica_titulacao_02"), "Mensagem esperada não exibida");
                Assert.True(CheckMessage("Exclusão não permitida. Já existem históricos de situação de aluno com esta situação."), "Mensagem esperada não exibida");
                driver.Driver.Close();


            });
        }


        [Fact]
         [Trait("Ordenado", "CRUD")]
        public void TesteOrdenadoCRUD()
        {
          TipoSituacaoMatricula_Inclusao();
          TipoSituacaoMatricula_InclusaoDuplicada();
          TipoSituacaoMatricula_Alteracao();
          TipoSituacaoMatricula_Exclusao();
          TipoSituacaoMatricula_Exclusaonaopermitida();
          

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
            driver.GoToUrl2(Consts.SERVIDOR_QUALIDADE_ADM + "MAT/TipoSituacaoMatricula");

        }
    }
}


