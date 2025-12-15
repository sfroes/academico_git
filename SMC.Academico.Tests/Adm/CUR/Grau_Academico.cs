using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using ExpectedConditions = SeleniumExtras.WaitHelpers.ExpectedConditions;
using SMC.Framework.Test; //Framework de testes
using System.Threading;
using Xunit; //Usado no Fact
using System;



namespace SMC.Academico.Tests.ADM.CSO //Arvore onde esta o arquivo
{

    public class Grau_Academico : SMCSeleniumUnitTest //Declara o nome da classe e recebe a SMCSeleniumUnitTest
    {


        [Fact] //Deve ser declarada para teste unitario
        public void GrauAcademico_1_Inserir() //Colocar o nome da tela a ser testada
        {
           // try
           // {
                base.ExecuteTest((driver) => //chama o browser e coloca o link correto
                {
                    //Maximiza o Browser
                    // driverNavigator.Manage().Window.Maximize();
                    Login(driver); // realiza o login como administrador

                    //COLAR AQUI O SCRIPT GRAVADO
                    driver.FindElement(By.Id("BotaoNovoVP")).Click();
                    driver.FindElement(By.Name("Descricao")).Click();
                    driver.FindElement(By.Name("Descricao")).Clear();
                    driver.FindElement(By.Name("Descricao")).SendKeys("Novo Grau de teste");
                    driver.FindElement(By.Id("NiveisEnsino_botao_modal")).Click();
                    driver.FindElement(By.CssSelector("span[class='fancytree-checkbox fa smc-ico-check smc-treeview-selector']")).Click();
                    driver.FindElement(By.Id("smc-dataselector-NiveisEnsino")).Click();
                    driver.FindElement(By.Id("btnSalvarVPIN")).Click();
  
                    //Checando se apareceu um sucesso
                    
                    Assert.True(CheckSuccess(), "Era esperado sucesso e ocorreu um erro");

                    //Checando se apareceu um erro
                    //Assert.False(CheckSuccess(), "Era esperado ERRO e ocorreu um SUCESSO");
                    driver.Driver.Close();
                });

           // }
        }
       
        [Fact]
        public void GrauAcademico_2_Editar() //Colocar o nome da tela a ser testada
        {
            base.ExecuteTest((driver) => //chama o browser e coloca o link correto
            {
                //Maximiza o Browser
                // driverNavigator.Manage().Window.Maximize();
                Login(driver); // realiza o login como administrador

                //COLAR AQUI O SCRIPT GRAVADO
                driver.FindElement(By.Name("Descricao")).Click();
                driver.FindElement(By.Name("Descricao")).Clear();
                driver.FindElement(By.Name("Descricao")).SendKeys("Novo Grau de teste");
                driver.FindElement(By.Id("BotaoPesquisarVP")).Click();
                driver.FindElement(By.Id("DynamicBotaoEdit0")).Click();
                driver.FindElement(By.Name("Descricao")).Clear();
                driver.FindElement(By.Name("Descricao")).SendKeys("Novo Grau de teste edit");
                driver.FindElement(By.Id("NiveisEnsino_botao_modal")).Click();
                driver.FindElement(By.CssSelector("span[class='fancytree-checkbox fa smc-ico-check smc-treeview-selector']")).Click();
                driver.FindElement(By.Id("smc-dataselector-NiveisEnsino")).Click();
                driver.FindElement(By.Id("btnSalvarVPIE")).Click();
                Assert.True(CheckSuccess(), "Era esperado sucesso e ocorreu um erro");
                driver.Driver.Close();

            });
        }[Fact]
         public void GrauAcademico_3_Excluir() //Colocar o nome da tela a ser testada
         {
            base.ExecuteTest((driver) => //chama o browser e coloca o link correto
            {
                //Maximiza o Browser
                // driverNavigator.Manage().Window.Maximize();
                Login(driver); // realiza o login como administrador

                //COLAR AQUI O SCRIPT GRAVADO


                driver.FindElement(By.Name("Descricao")).Click();
            driver.FindElement(By.Name("Descricao")).Clear();
            driver.FindElement(By.Name("Descricao")).SendKeys("novo grau de teste edit");
            driver.FindElement(By.Id("BotaoPesquisarVP")).Click();
            driver.FindElement(By.XPath("//a[@id='DynamicBotaoExcluir1']/i")).Click();
            driver.FindElement(By.Id("BotaoPadraoPerguntaSim")).Click();

                driver.FindElement(By.XPath("//div[@id='centro']/div/div/div")).Click();
                
                //Declara a variável teste q ira receber o texto do campo
                string
                teste = driver.FindElement(By.XPath("//div[@id='centro']/div/div/div[2]")).GetValue();
                
                //teste = driver.FindElement(By.CssSelector("[data-type='smc-modal-content']")).GetValue();
                //    driver.FindElement(By.CssSelector("[title='Pesquisar'][type='submit']")).Click();


                //Compara a mensagem exibida com a esperada:
                Assert.Equal("Grau acadêmico excluído com sucesso.", teste);

                    /*Assert.True(CheckSuccess(), "Era esperado sucesso e ocorreu um erro");*/

                    driver.Driver.Close();
                });            
         }

        [Fact]
        [Trait("Ordenado", "CRUD")]
        public void TesteOrdenadoCRUD()
        {
            GrauAcademico_1_Inserir();
            GrauAcademico_2_Editar();
            GrauAcademico_3_Excluir();
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

            driver.GoToUrl2(Consts.SERVIDOR_QUALIDADE_ADM + "CSO/GrauAcademico"); //coloca o resto do endereço para acessar a pagina do teste

        }
    }
}
