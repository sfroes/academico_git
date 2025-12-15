using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using ExpectedConditions = SeleniumExtras.WaitHelpers.ExpectedConditions;
using SMC.Academico.Tests.ALUNO.ALN;
using SMC.Framework.Test;
using System;
using System.Threading;
using Xunit;

namespace SMC.Academico.Tests.ADM.APR
{
    public class CriterioAprovacao : SMCSeleniumUnitTest
    {
        // private bool phantomJS = false;

        [Fact]
        public void AlunoLogin()
        {
            base.ExecuteTest((driver) =>
            {
                Login(driver);

                driver.GoToUrl2("https://web-qualidade.pucminas.br/SGA.Administrativo/ALN/Aluno/Editar?seq=185CFFE4A576B18481B5307AA6E9AFC6");

                var cpf = driver.FindElement(By.Name("Cpf")).GetValue();

                driver.FindElement(By.XPath("(.//*[normalize-space(text()) and normalize-space(.)='Dados Pessoais'])[1]/following::span[1]")).Click();

                var email = driver.FindElement(By.XPath("(.//*[normalize-space(text()) and normalize-space(.)='Endereços eletrônicos'])[1]/following::p[2]")).GetValue();


                var aluno = new TesteAluno(email);
                aluno.Inserir();
                aluno.Excluir();

 
              //  throw new System.Exception("Value: " + email);
            });

            //https://web-qualidade.pucminas.br/SGA.Administrativo/ALN/Aluno/Editar?seq=185CFFE4A576B18481B5307AA6E9AFC6
        }

        [Fact]
        public void Inserir()
        {
            base.ExecuteTest((driver) =>
            {
                //Maximiza o Browser
                // driverNavigator.Manage().Window.Maximize();

                Login(driver);
                //-------------------------------------------------------------------------------------------------

                driver.FindElement(By.Id("BotaoNovoVP")).Click();
                driver.FindElement(By.Name("Descricao")).Clear();
                driver.FindElement(By.Name("Descricao")).SendKeys("teste");
                driver.FindElement(By.Name("ApuracaoFrequencia")).Click();
                driver.FindElement(By.Name("PercentualFrequenciaAprovado")).Click();
                driver.FindElement(By.Name("PercentualFrequenciaAprovado")).Clear();
                driver.FindElement(By.Name("PercentualFrequenciaAprovado")).SendKeys("10");
                driver.FindElement(By.Id("select_TipoArredondamento")).Click();
                new SMCSelectElement(driver.FindElement(By.Id("select_TipoArredondamento"))).SelectByText("Arredondar para o teto");
                driver.FindElement(By.Id("select_TipoArredondamento")).Click();
                driver.FindElement(By.Name("ApuracaoNota")).Click();
                driver.FindElement(By.Name("NotaMaxima")).Click();
                driver.FindElement(By.Name("NotaMaxima")).Clear();
                driver.FindElement(By.Name("NotaMaxima")).SendKeys("100");
                driver.FindElement(By.Name("PercentualNotaAprovado")).Click();
                driver.FindElement(By.Name("PercentualNotaAprovado")).Clear();
                driver.FindElement(By.Name("PercentualNotaAprovado")).SendKeys("60");
                driver.FindElement(By.Id("BotaoSalvarTemplate")).Click();

                //Checando a mensagem de Sucesso
                Assert.True(CheckSuccess(), "Era esperado sucesso e ocorreu um erro");

                //Para fechar o Chrome em segundo plano
                driver.Driver.Close();
            });
        }


        [Fact]
        public void Inserirduplicado()
        {
            base.ExecuteTest((driver) =>
            {
                //Maximiza o Browser
                // driverNavigator.Manage().Window.Maximize();

                Login(driver);
                //-------------------------------------------------------------------------------------------------

                driver.FindElement(By.Id("BotaoNovoVP")).Click();
                driver.FindElement(By.Name("Descricao")).Clear();
                driver.FindElement(By.Name("Descricao")).SendKeys("Aprovado/reprovado");
                driver.FindElement(By.Name("ApuracaoFrequencia")).Click();
                driver.FindElement(By.Name("PercentualFrequenciaAprovado")).Click();
                driver.FindElement(By.Name("PercentualFrequenciaAprovado")).Clear();
                driver.FindElement(By.Name("PercentualFrequenciaAprovado")).SendKeys("10");
                driver.FindElement(By.Id("select_TipoArredondamento")).Click();
                new SMCSelectElement(driver.FindElement(By.Id("select_TipoArredondamento"))).SelectByText("Arredondar para o teto");
                driver.FindElement(By.Id("select_TipoArredondamento")).Click();
                driver.FindElement(By.Name("ApuracaoNota")).Click();
                driver.FindElement(By.Name("NotaMaxima")).Click();
                driver.FindElement(By.Name("NotaMaxima")).Clear();
                driver.FindElement(By.Name("NotaMaxima")).SendKeys("100");
                driver.FindElement(By.Name("PercentualNotaAprovado")).Click();
                driver.FindElement(By.Name("PercentualNotaAprovado")).Clear();
                driver.FindElement(By.Name("PercentualNotaAprovado")).SendKeys("60");
                driver.FindElement(By.Id("BotaoSalvarTemplate")).Click();

                //Checando a mensagem de Sucesso
                
                Assert.True(CheckMessage("Já existe critério de aprovação cadastrado com esta descrição."), "Era esperado sucesso e ocorreu um erro");

                //Para fechar o Chrome em segundo plano
                driver.Driver.Close();
            });
        }



        [Fact]
        public void Inserircriterioconceitoescalaconceito ()
        {
            base.ExecuteTest((driver) =>
            {
                //Maximiza o Browser
                // driverNavigator.Manage().Window.Maximize();

                Login(driver);
                //-------------------------------------------------------------------------------------------------
                driver.FindElement(By.Id("BotaoNovoVP")).Click();
                driver.FindElement(By.Name("Descricao")).Clear();
                driver.FindElement(By.Name("Descricao")).SendKeys("teste2");
                driver.FindElement(By.XPath("(.//*[normalize-space(text()) and normalize-space(.)='Sim'])[1]/following::label[1]")).Click();
                driver.FindElement(By.XPath("(.//*[normalize-space(text()) and normalize-space(.)='Sim'])[2]/following::label[1]")).Click();
                driver.FindElement(By.Name("SeqEscalaApuracao")).Click();
                new SMCSelectElement(driver.FindElement(By.Name("SeqEscalaApuracao"))).SelectByText("Conceito de A a D/Percentual de 0 a 100 com indefinido");
                driver.FindElement(By.Name("SeqEscalaApuracao")).Click();
                driver.FindElement(By.Id("BotaoSalvarTemplate")).Click();
                //Checando a mensagem de validação
               Assert.True(CheckMessage("Não é permitido cadastrar um critério de aprovação que não tem apuração de nota com escala de apuração do tipo \"Conceito\"."), "Era esperado sucesso e ocorreu um erro");

                //Para fechar o Chrome em segundo plano
                driver.Driver.Close();
            });
        }

        [Fact]
        public void Inserircriterionotaescalaconceito()
        {
            base.ExecuteTest((driver) =>
            {
                //Maximiza o Browser
                // driverNavigator.Manage().Window.Maximize();

                Login(driver);
                //-------------------------------------------------------------------------------------------------

                driver.FindElement(By.Id("BotaoNovoVP")).Click();
                driver.FindElement(By.Name("Descricao")).Clear();
                driver.FindElement(By.Name("Descricao")).SendKeys("teste2");
                driver.FindElement(By.XPath("(.//*[normalize-space(text()) and normalize-space(.)='Sim'])[1]/following::label[2]")).Click();
                driver.FindElement(By.Name("ApuracaoNota")).Click();
                driver.FindElement(By.Name("NotaMaxima")).Click();
                driver.FindElement(By.Name("NotaMaxima")).Clear();
                driver.FindElement(By.Name("NotaMaxima")).SendKeys("100");
                driver.FindElement(By.Name("PercentualNotaAprovado")).Click();
                driver.FindElement(By.Name("PercentualNotaAprovado")).Clear();
                driver.FindElement(By.Name("PercentualNotaAprovado")).SendKeys("60");
                driver.FindElement(By.Id("select_SeqEscalaApuracao")).Click();
                new SMCSelectElement(driver.FindElement(By.Id("select_SeqEscalaApuracao"))).SelectByText("Aprovado/reprovado");
                driver.FindElement(By.Id("select_SeqEscalaApuracao")).Click();
                driver.FindElement(By.Id("BotaoSalvarTemplate")).Click();
               

                //Checando a mensagem de validação
                Assert.True(CheckMessage("Não é permitido cadastrar um critério de aprovação que tem apuração de nota com escala de apuração diferente do tipo \"Conceito\"."), "Era esperado sucesso e ocorreu um erro");
                
                //Para fechar o Chrome em segundo plano
                driver.Driver.Close();
            });
        }



        [Fact]
        public void Alterar()
        {
            base.ExecuteTest((driver) =>
            {
                Login(driver);
                //-----------------------------------------------------------------------------------------
                
                driver.FindElement(By.Name("Descricao")).Click();
                driver.FindElement(By.Name("Descricao")).Clear();
                driver.FindElement(By.Name("Descricao")).SendKeys("teste");

                driver.FindElement(By.XPath("(.//*[normalize-space(text()) and normalize-space(.)='Pesquisar'])[1]/i[1]")).Click();
                driver.FindElement(By.XPath("(.//*[normalize-space(text()) and normalize-space(.)='Alterar'])[1]/i[1]")).Click();

                driver.FindElement(By.Name("Descricao")).Click();
                driver.FindElement(By.Name("Descricao")).Clear();
                driver.FindElement(By.Name("Descricao")).SendKeys("teste1");
                driver.FindElement(By.XPath("(.//*[normalize-space(text()) and normalize-space(.)='Não'])[1]/input[1]")).Click();
                driver.FindElement(By.XPath("(.//*[normalize-space(text()) and normalize-space(.)='Não'])[2]/input[1]")).Click();
                driver.FindElement(By.Name("SeqEscalaApuracao")).Click();
                new SMCSelectElement(driver.FindElement(By.Name("SeqEscalaApuracao"))).SelectByText("Aprovado/reprovado");
                driver.FindElement(By.Name("SeqEscalaApuracao")).Click();
                driver.FindElement(By.Id("BotaoSalvarTemplate")).Click();

                //Checando a mensagem de Sucesso
                Assert.True(CheckSuccess(), "Mensagem de sucesso não exibida");

                //Para fechar o Chrome em segundo plano
                driver.Driver.Close();
            });
        }

        [Fact]
        public void Excluir()
        {
            base.ExecuteTest((driver) =>
            {
                Login(driver);
                //----------------------------------------------

                driver.FindElement(By.Name("Descricao")).Click();

                driver.FindElement(By.Name("Descricao")).Clear();
                driver.FindElement(By.Name("Descricao")).SendKeys("teste1");

                driver.FindElement(By.Id("BotaoPesquisarVP")).Click();
         
                driver.FindElement(By.XPath("(.//*[normalize-space(text()) and normalize-space(.)='Excluir'])[2]/i[1]")).Click();
                driver.FindElement(By.Id("BotaoPadraoPerguntaSim")).Click();

                //Checando a mensagem de Sucesso
                Assert.True(CheckSuccess(), "Mensagem de sucesso não exibida");

                //Para fechar o Chrome em segundo plano
                driver.Driver.Close();
            });
        }

        [Fact]
        [Trait("Ordenado", "CRUD")]
        public void TesteOrdenadoCRUD()
        {
            Inserir();
            Inserirduplicado();
            Inserircriterioconceitoescalaconceito();
            Inserircriterionotaescalaconceito();
            Alterar();
            Excluir();
        }

        private static void Login(ISMCWebDriver driver)
        {
            //----------------------------------------------------
            driver.GoToUrl(Consts.SERVIDOR_QUALIDADE_ADM);
            //força o sistema a esperar 15 minutos ou até que apareça o campo para login
            WebDriverWait wait = new WebDriverWait((OpenQA.Selenium.IWebDriver)driver, TimeSpan.FromSeconds(900));
            wait.Until(e => e.FindElement(By.Name("LoginCpf")));
            driver.SMCLoginCpf();
            //força o sistema a esperar 15 minutos ou até que apareça a home do SGA
            wait.Until(ExpectedConditions.TextToBePresentInElementLocated(By.XPath("//div[@id='divConteudoPrincipal']/section/h2"), "SGA.Administrativo"));

            driver.GoToUrl2(Consts.SERVIDOR_QUALIDADE_ADM + "/APR/CriterioAprovacao");

        }


    }
}
