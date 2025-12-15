using System;
using System.Linq;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SMC.Framework.Test; //Framework de testes
using System.Threading;
using Xunit; //Usado no Fact

namespace SMC.Academico.Tests.ADM.CNC //Arvore onde esta o arquivo
{

    public class DocumentoConclusao : SMCSeleniumUnitTest //Declara o nome da classe e recebe a SMCSeleniumUnitTest
    {

       
        [Fact] //Deve ser declarada para teste unitario
        public void DocumentoConclusao_PesquisaporDepartamento() //Colocar o nome da tela a ser testada
        {
            base.ExecuteTest((driver) => //chama o browser e coloca o link correto
            {
                //Maximiza o Browser
                // driverNavigator.Manage().Window.Maximize();

                Login(driver); // realiza o login como administrador

                //Colar o script aqui

                driver.FindElement(By.XPath("//div[@id='SeqsEntidadesResponsaveis']/div/a")).Click();
                driver.FindElement(By.Id("SeqsEntidadesResponsaveisSearch")).Click();
                driver.FindElement(By.Id("SeqsEntidadesResponsaveisSearch")).Clear();
                driver.FindElement(By.Id("SeqsEntidadesResponsaveisSearch")).SendKeys("Departamento de Direito");
                driver.FindElement(By.XPath("//div[@id='SeqsEntidadesResponsaveis']/div[2]/ul/li[10]/span")).Click();
                driver.FindElement(By.CssSelector("[title='Pesquisar'][type='submit']")).Click();
                Thread.Sleep(6000);
                driver.Driver.Close();
            });
        }


        [Fact] //Deve ser declarada para teste unitario
        public void DocumentoConclusao_PesquisaporCPF() //Colocar o nome da tela a ser testada
        {
            base.ExecuteTest((driver) => //chama o browser e coloca o link correto
            {
                //Maximiza o Browser
                // driverNavigator.Manage().Window.Maximize();

                Login(driver); // realiza o login como administrador

                //Colar o script aqui

                driver.FindElement(By.XPath("//button[@id='SeqPessoa_botao_modal']/i")).Click();
                driver.FindElement(By.Name("Cpf")).Click();
                driver.FindElement(By.Name("Cpf")).Clear();
                driver.FindElement(By.Name("Cpf")).SendKeys("101.553.056-70");
                driver.FindElement(By.Id("DataSelectorPesquisar0")).Click();
                driver.FindElement(By.Id("DataSelector_gridDataSelector0")).Click();
                driver.FindElement(By.Id("smc-dataselector-SeqPessoa")).Click();
                driver.FindElement(By.CssSelector("[title='Pesquisar'][type='submit']")).Click();
                Thread.Sleep(6000);
                driver.Driver.Close();

            });
        }

        [Fact] //Deve ser declarada para teste unitario
        public void DocumentoConclusao_PesquisaporRetornodeCPF() //Colocar o nome da tela a ser testada
        {
            base.ExecuteTest((driver) => //chama o browser e coloca o link correto
            {
                //Maximiza o Browser
                // driverNavigator.Manage().Window.Maximize();

                Login(driver); // realiza o login como administrador

                //Colar o script aqui

                driver.FindElement(By.XPath("//button[@id='SeqPessoa_botao_modal']/i")).Click();
                driver.FindElement(By.Name("Cpf")).Click();
                driver.FindElement(By.Name("Cpf")).Clear();
                driver.FindElement(By.Name("Cpf")).SendKeys("101.553.056-70");
                driver.FindElement(By.Id("DataSelectorPesquisar0")).Click();
                driver.FindElement(By.Id("DataSelector_gridDataSelector0")).Click();
                driver.FindElement(By.Id("smc-dataselector-SeqPessoa")).Click();
                driver.FindElement(By.CssSelector("[title='Pesquisar'][type='submit']")).Click();
                driver.Driver.Close();

                /* fazer o teste toda até a pesquisa e depois faço a comparação*/
                //String
                //teste = driver.FindElement(By.XPath("//div[@id='gridDocumentoConclusao_Grid').GetValue();/* a getvalue indica que o que tiver na variável e coloque na variável teste*/



                //Imprime no campo a variável teste                
                //driver.FindElement(By.Id("gridDocumentoConclusao_Grid")).SendKeys(teste.ToString()); /* onde eu recebo a variável.*/


                /* se teste for cpf diferente do esperado  entao emite a mensagem*/
            });
        }

        [Fact] //Deve ser declarada para teste unitario
        public void DocumentoConclusao_Pesquisaportipodocumento() //Colocar o nome da tela a ser testada
        {
            base.ExecuteTest((driver) => //chama o browser e coloca o link correto
            {
                //Maximiza o Browser
                // driverNavigator.Manage().Window.Maximize();

                Login(driver); // realiza o login como administrador

                //Colar o script aqui

                driver.FindElement(By.Id("select_SeqTipoDocumentoAcademico")).Click();
                new SMCSelectElement(driver.FindElement(By.Id("select_SeqTipoDocumentoAcademico"))).SelectByText("Diploma Digital");
                driver.FindElement(By.CssSelector("[title='Pesquisar'][type='submit']")).Click();
                Thread.Sleep(6000);
                driver.Driver.Close();



            });
        }

        [Fact] //Deve ser declarada para teste unitario
        public void DocumentoConclusao_Pesquisaportiposituacao() //Colocar o nome da tela a ser testada
        {
            base.ExecuteTest((driver) => //chama o browser e coloca o link correto
            {
                //Maximiza o Browser
                // driverNavigator.Manage().Window.Maximize();

                Login(driver); // realiza o login como administrador

                //Colar o script aqui

                driver.FindElement(By.Id("select_SeqSituacaoDocumentoAcademico")).Click();
                new SMCSelectElement(driver.FindElement(By.Id("select_SeqSituacaoDocumentoAcademico"))).SelectByText("Entregue");
                driver.FindElement(By.CssSelector("[title='Pesquisar'][type='submit']")).Click();
                /*Thread.Sleep(6000);*/
                driver.Driver.Close();


            });
        }

        [Fact]
        [Trait("Ordenado", "CRUD")]
        public void TesteOrdenadoCRUD()
        {

            DocumentoConclusao_PesquisaporDepartamento();
            DocumentoConclusao_PesquisaporCPF();
            DocumentoConclusao_PesquisaporRetornodeCPF();
            DocumentoConclusao_Pesquisaportipodocumento();
            DocumentoConclusao_Pesquisaportiposituacao();
                 
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
            driver.GoToUrl2(Consts.SERVIDOR_QUALIDADE_ADM + "CNC/DocumentoConclusao");

        }
    }
}

