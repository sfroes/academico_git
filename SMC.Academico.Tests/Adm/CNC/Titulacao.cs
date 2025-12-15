using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using ExpectedConditions = SeleniumExtras.WaitHelpers.ExpectedConditions;
using SMC.Framework.Test; //Framework de testes
using System.Threading;
using System;
using Xunit; //Usado no Fact


namespace SMC.Academico.Tests.ADM.CNC //Arvore onde esta o arquivo
{

    public class Titulacao : SMCSeleniumUnitTest //Declara o nome da classe e recebe a SMCSeleniumUnitTest
    {

        [Fact] //Deve ser declarada para teste unitario
        
        public void Titulacao_Inclusao() //Colocar o nome da tela a ser testada
        {
            base.ExecuteTest((driver) => //chama o browser e coloca o link correto
            {
                //Maximiza o Browser
                // driverNavigator.Manage().Window.Maximize();
                Login(driver); // realiza o login como administrador

                //Colar o script aqui

                driver.FindElement(By.Id("BotaoNovoVP")).Click();
                driver.FindElement(By.Name("DescricaoFeminino")).Clear();
                driver.FindElement(By.Name("DescricaoFeminino")).SendKeys("Especialista em Automação");
                driver.FindElement(By.Name("DescricaoMasculino")).Click();
                driver.FindElement(By.Name("DescricaoMasculino")).Click();
                driver.FindElement(By.Name("DescricaoMasculino")).Clear();
                driver.FindElement(By.Name("DescricaoMasculino")).SendKeys("Especialista em Automação");
                driver.FindElement(By.Name("DescricaoAbreviada")).Click();
                driver.FindElement(By.Name("DescricaoAbreviada")).Clear();
                driver.FindElement(By.Name("DescricaoAbreviada")).SendKeys("EA");
                driver.FindElement(By.Name("SeqCurso_display_text")).Click();
                driver.FindElement(By.Id("BotaoSalvarTemplate")).Click();

                //Checando a mensagem de Sucesso (compara a mensagem que está descrita com a que vai aparecer no aplicativo)
                Assert.True(CheckMessage("Titulação incluído com sucesso."), "Mensagem esperada não exibida");
                driver.Driver.Close();

            });
        }
        [Fact] //Deve ser declarada para teste unitario

        public void Titulacao_Inclusaocomselecaocurso() //Colocar o nome da tela a ser testada
        {
            base.ExecuteTest((driver) => //chama o browser e coloca o link correto
            {
                //Maximiza o Browser
                // driverNavigator.Manage().Window.Maximize();
                Login(driver); // realiza o login como administrador

                //Colar o script aqui

                driver.FindElement(By.Id("BotaoNovoVP")).Click();
                driver.FindElement(By.Name("DescricaoFeminino")).Clear();
                driver.FindElement(By.Name("DescricaoFeminino")).SendKeys("Especialista Biomédica");
                driver.FindElement(By.Name("DescricaoMasculino")).Click();
                driver.FindElement(By.Name("DescricaoMasculino")).Clear();
                driver.FindElement(By.Name("DescricaoMasculino")).SendKeys("Especialista Biomédico");
                driver.FindElement(By.Name("DescricaoAbreviada")).Click();
                driver.FindElement(By.Name("DescricaoAbreviada")).Clear();
                driver.FindElement(By.Name("DescricaoAbreviada")).SendKeys("ESB");
                driver.FindElement(By.Name("DescricaoXSD")).Click();
                driver.FindElement(By.Name("DescricaoXSD")).Clear();
                driver.FindElement(By.Name("DescricaoXSD")).SendKeys("Especialista Biomédico");
                driver.FindElement(By.XPath("//button[@id='SeqCurso_botao_modal']/i")).Click();
                driver.FindElement(By.Name("Nome")).Click();
                driver.FindElement(By.Name("Nome")).Clear();
                driver.FindElement(By.Name("Nome")).SendKeys("biomedicina");
                driver.FindElement(By.Id("DataSelectorPesquisar0")).Click();
                driver.FindElement(By.Id("DataSelector_gridDataSelector0")).Click();
                driver.FindElement(By.XPath("//button[@id='smc-dataselector-SeqCurso']/i")).Click();
                driver.FindElement(By.Id("BotaoSalvarTemplate")).Click();

                //Checando a mensagem de Sucesso (compara a mensagem que está descrita com a que vai aparecer no aplicativo)
                Assert.True(CheckMessage("Titulação incluído com sucesso."), "Mensagem esperada não exibida");
                driver.Driver.Close();

            });
        }

        [Fact] //Deve ser declarada para teste unitario

        public void Titulacao_Alteracao() //Colocar o nome da tela a ser testada
        {
            base.ExecuteTest((driver) => //chama o browser e coloca o link correto
            {
                //Maximiza o Browser
                // driverNavigator.Manage().Window.Maximize();
                Login(driver); // realiza o login como administrador

                //Colar o script aqui

                driver.FindElement(By.Name("Descricao")).Click();
                driver.FindElement(By.Name("Descricao")).Clear();
                driver.FindElement(By.Name("Descricao")).SendKeys("Especialista em Automação");
                driver.FindElement(By.Id("BotaoPesquisarVP")).Click();
                driver.FindElement(By.XPath("//a[@id='DynamicBotaoEdit0']/i")).Click();
                driver.FindElement(By.Name("DescricaoFeminino")).Click();
                driver.FindElement(By.Name("DescricaoFeminino")).Clear();
                driver.FindElement(By.Name("DescricaoFeminino")).SendKeys(" A Especialista em Teste");
                driver.FindElement(By.Name("DescricaoMasculino")).Click();
                driver.FindElement(By.Name("DescricaoMasculino")).Clear();
                driver.FindElement(By.Name("DescricaoMasculino")).SendKeys(" O Especialista em Testes");
                driver.FindElement(By.Id("BotaoSalvarTemplate")).Click();

                Assert.True(CheckSuccess(), "Era esperado sucesso e ocorreu um erro");
                driver.Driver.Close();

            });
        }
        [Fact] //Deve ser declarada para teste unitario

        public void Titulacao_InclusaoDuplicada() //Colocar o nome da tela a ser testada
        {
            base.ExecuteTest((driver) => //chama o browser e coloca o link correto
            {
                //Maximiza o Browser
                // driverNavigator.Manage().Window.Maximize();
                Login(driver); // realiza o login como administrador

                //Colar o script aqui

                driver.FindElement(By.Id("BotaoNovoVP")).Click();
                driver.FindElement(By.Name("DescricaoFeminino")).Clear();
                driver.FindElement(By.Name("DescricaoFeminino")).SendKeys("A Especialista em Teste");
                driver.FindElement(By.Name("DescricaoMasculino")).Click();
                driver.FindElement(By.Name("DescricaoMasculino")).Click();
                driver.FindElement(By.Name("DescricaoMasculino")).Clear();
                driver.FindElement(By.Name("DescricaoMasculino")).SendKeys("Especialista em Teste");
                driver.FindElement(By.Name("DescricaoAbreviada")).Click();
                driver.FindElement(By.Name("DescricaoAbreviada")).Clear();
                driver.FindElement(By.Name("DescricaoAbreviada")).SendKeys("ESA");
                driver.FindElement(By.Name("SeqCurso_display_text")).Click();
                driver.FindElement(By.Id("BotaoSalvarTemplate")).Click();
                Thread.Sleep(5000);
                
                Assert.True(CheckMessage("Já existe titulação cadastrada com este título feminino para este curso ou grau acadêmico."), "Mensagem esperada não exibida");
                driver.Driver.Close();
            });
        }

        [Fact] //Deve ser declarada para teste unitario

        public void Titulacao_Exclusao() //Colocar o nome da tela a ser testada
        {
            base.ExecuteTest((driver) => //chama o browser e coloca o link correto
            {
                //Maximiza o Browser
                // driverNavigator.Manage().Window.Maximize();
                Login(driver); // realiza o login como administrador

                //Colar o script aqui

                driver.FindElement(By.Name("Descricao")).Clear();
                driver.FindElement(By.Name("Descricao")).SendKeys(" A Especialista em Teste");
                driver.FindElement(By.Name("SeqCurso_display_text")).Click();
                driver.FindElement(By.Id("BotaoPesquisarVP")).Click();
                driver.FindElement(By.XPath("//a[@id='DynamicBotaoExcluir1']/i")).Click();
                driver.FindElement(By.Id("BotaoPadraoPerguntaSim")).Click();
                //Checando a mensagem de Sucesso (Verifica se tem a palavra sucesso na mensagem)
                Assert.True(CheckSuccess(), "Era esperado sucesso e ocorreu um erro");

                driver.FindElement(By.Name("Descricao")).Clear();
                driver.FindElement(By.Name("Descricao")).SendKeys("Especialista Biomédica");
                driver.FindElement(By.Name("SeqCurso_display_text")).Click();
                driver.FindElement(By.Id("BotaoPesquisarVP")).Click();
                driver.FindElement(By.XPath("//a[@id='DynamicBotaoExcluir1']/i")).Click();
                driver.FindElement(By.Id("BotaoPadraoPerguntaSim")).Click();
                //Checando a mensagem de Sucesso (Verifica se tem a palavra sucesso na mensagem)
                Assert.True(CheckSuccess(), "Era esperado sucesso e ocorreu um erro");
                driver.Driver.Close();

            });
        }

        [Fact] //Deve ser declarada para teste unitario

        public void Titulacao_Exclusaonaopermitida() //Colocar o nome da tela a ser testada
        {
            base.ExecuteTest((driver) => //chama o browser e coloca o link correto
            {
                //Maximiza o Browser
                // driverNavigator.Manage().Window.Maximize();
                Login(driver); // realiza o login como administrador

                //Colar o script aqui

                driver.FindElement(By.Name("Descricao")).Click();
                driver.FindElement(By.Name("Descricao")).Clear();
                driver.FindElement(By.Name("Descricao")).SendKeys("Arquiteto");
                driver.FindElement(By.Id("BotaoPesquisarVP")).Click();
                driver.FindElement(By.XPath("//a[@id='DynamicBotaoExcluir1']/i")).Click();
                driver.FindElement(By.Id("BotaoPadraoPerguntaSim")).Click();
               
                //Assert.True(CheckMessage("Exclusão não permitida. Titulacao já foi associado(a) a outra entidade.FK_curso_formacao_especifica_titulacao_02"), "Mensagem esperada não exibida");
                Assert.True(CheckMessage("Exclusão não permitida"), "Mensagem esperada não exibida");
                driver.Driver.Close();


            });
        }


        [Fact] //Deve ser declarada para teste unitario

        public void Titulacao_Inclusaocomselecaocursoegrau() //Colocar o nome da tela a ser testada
        {
            base.ExecuteTest((driver) => //chama o browser e coloca o link correto
            {
                //Maximiza o Browser
                // driverNavigator.Manage().Window.Maximize();
                Login(driver); // realiza o login como administrador

                //Colar o script aqui
                                
                driver.FindElement(By.Id("BotaoNovoVP")).Click();
                driver.FindElement(By.Name("DescricaoFeminino")).Clear();
                driver.FindElement(By.Name("DescricaoFeminino")).SendKeys("Especialista em Teste");
                driver.FindElement(By.Name("DescricaoMasculino")).Click();
                driver.FindElement(By.Name("DescricaoMasculino")).Click();
                driver.FindElement(By.Name("DescricaoMasculino")).Clear();
                driver.FindElement(By.Name("DescricaoMasculino")).SendKeys("Especialista em Teste");
                driver.FindElement(By.Name("DescricaoAbreviada")).Click();
                driver.FindElement(By.Name("DescricaoAbreviada")).Clear();
                driver.FindElement(By.Name("DescricaoAbreviada")).SendKeys("EST");
                driver.FindElement(By.XPath("//button[@id='SeqCurso_botao_modal']/i")).Click();
                driver.FindElement(By.Name("Nome")).Click();
                driver.FindElement(By.Name("Nome")).Clear();
                driver.FindElement(By.Name("Nome")).SendKeys("mestrado em administração");
                driver.FindElement(By.Id("DataSelectorPesquisar0")).Click();
                driver.FindElement(By.Id("DataSelector_gridDataSelector0")).Click();
                driver.FindElement(By.XPath("//button[@id='smc-dataselector-SeqCurso']/i")).Click();
                driver.FindElement(By.Id("select_SeqGrauAcademico")).Click();
                new SMCSelectElement(driver.FindElement(By.Id("select_SeqGrauAcademico"))).SelectByText("Mestrado");
                driver.FindElement(By.Id("BotaoSalvarTemplate")).Click();
                


                //Checando a mensagem de Sucesso (compara a mensagem que está descrita com a que vai aparecer no aplicativo)
                //driver.FindElement(By.XPath("//div[@id='centro']/div/div/div[2]")).Click();
                Assert.True(CheckMessage("Não é permitido o cadastro de titulação associado a um curso e grau acadêmico. Defina se a titulação aplica-se somente a um determinado curso ou somente a ofertas de curso com um determinado grau acadêmico."), "Mensagem esperada não exibida");
                //Assert.True(CheckMessage("Não é permitido o cadastro de titulação associado a um curso e grau acadêmico."), "Mensagem esperada não exibida");

                driver.Driver.Close();
            });
        }

        [Fact]
        [Trait("Ordenado", "CRUD")]
        public void TesteOrdenadoCRUD()
        {

            Titulacao_Inclusao();
            Titulacao_Alteracao();
            Titulacao_InclusaoDuplicada();
            Titulacao_Exclusaonaopermitida();
            Titulacao_Inclusaocomselecaocursoegrau();
            Titulacao_Inclusaocomselecaocurso();
            Titulacao_Exclusao();

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
            driver.GoToUrl2(Consts.SERVIDOR_QUALIDADE_ADM + "CNC/Titulacao");

        }
    }
}

        
    