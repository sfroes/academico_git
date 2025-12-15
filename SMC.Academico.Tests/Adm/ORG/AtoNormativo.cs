using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using ExpectedConditions = SeleniumExtras.WaitHelpers.ExpectedConditions;
using SMC.Framework.Test; //Framework de testes
using System.Threading;
using Xunit; //Usado no Fact
using System;



namespace SMC.Academico.Tests.ADM.CSO //Arvore onde esta o arquivo
{

    public class AtoNormativo : SMCSeleniumUnitTest //Declara o nome da classe e recebe a SMCSeleniumUnitTest
    {


        [Fact] //Deve ser declarada para teste unitario
        public void AtoNormativo_Inclusao() //Colocar o nome da tela a ser testada
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
                driver.FindElement(By.Id("BotaoNovoVP")).Click();
                driver.FindElement(By.Id("select_SeqAssuntoNormativo")).Click();
                new SMCSelectElement(driver.FindElement(By.Id("select_SeqAssuntoNormativo"))).SelectByText("Autorização");
                driver.FindElement(By.Id("select_SeqTipoAtoNormativo")).Click();
                new SMCSelectElement(driver.FindElement(By.Id("select_SeqTipoAtoNormativo"))).SelectByText("Autorização");
                driver.FindElement(By.Name("NumeroDocumento")).Click();
                driver.FindElement(By.Name("NumeroDocumento")).Clear();
                driver.FindElement(By.Name("NumeroDocumento")).SendKeys("12.212");
                driver.FindElement(By.Name("DataDocumento")).Click();
                driver.FindElement(By.Name("DataDocumento")).Clear();
                driver.FindElement(By.Name("DataDocumento")).SendKeys("08/09/2023");
                driver.FindElement(By.Name("DataPrazoValidade")).Click();

                Thread.Sleep(1100);

              /*  //Declara a variável teste q ira receber o texto do campo Descrição
                string
                teste = driver.FindElement(By.Name("Descricao")).GetValue();

                //Compara a mensagem exibida com a esperada:
                Assert.Equal("Autorização: Autorização nº12212, de 08 / 09 / 2023", teste);*/


                //driver.FindElement(By.XPath("(.//*[normalize-space(text()) and normalize-space(.)='Sáb'])[6]/following::div[5]")).Click();
                driver.FindElement(By.Id("btnSalvarVPIN")).Click();

                //Checando a mensagem de Sucesso (Verifica se tem a palavra sucesso na mensagem)
                Assert.True(CheckSuccess(), "Era esperado sucesso e ocorreu um erro");
                driver.Driver.Close();

            });
        }


        [Fact] //Deve ser declarada para teste unitario
        public void AtoNormativo_Inclusaoduplicada() //Colocar o nome da tela a ser testada
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
                driver.FindElement(By.Id("BotaoNovoVP")).Click();
                driver.FindElement(By.Id("select_SeqAssuntoNormativo")).Click();
                new SMCSelectElement(driver.FindElement(By.Id("select_SeqAssuntoNormativo"))).SelectByText("Autorização");
                driver.FindElement(By.Id("select_SeqTipoAtoNormativo")).Click();
                new SMCSelectElement(driver.FindElement(By.Id("select_SeqTipoAtoNormativo"))).SelectByText("Autorização");
                driver.FindElement(By.Name("NumeroDocumento")).Click();
                driver.FindElement(By.Name("NumeroDocumento")).Clear();
                driver.FindElement(By.Name("NumeroDocumento")).SendKeys("12.212");
                driver.FindElement(By.Name("DataDocumento")).Click();
                driver.FindElement(By.Name("DataDocumento")).SendKeys("08/09/2023");
                driver.FindElement(By.Name("DataPrazoValidade")).Click();
                //driver.FindElement(By.XPath("(.//*[normalize-space(text()) and normalize-space(.)='Sáb'])[6]/following::div[5]")).Click();
                driver.FindElement(By.Id("btnSalvarVPIN")).Click();
                          
                //Declara a variável teste q ira receber o texto do campo
                string
                //teste = driver.FindElement(By.XPath("//div[@id='centro']/div/div/div[2]")).GetValue();
                teste = driver.FindElement(By.XPath("//div[@id='default']/div[2]/div[2]/div[2]/p")).GetValue();

                //Compara a mensagem exibida com a esperada:
                Assert.Equal("Este ato normativo já está cadastrado nesta instituição de ensino.", teste);
                driver.Driver.Close();

            });
        }



    

        [Fact] //Deve ser declarada para teste unitario
        public void AtoNormativo_Alteracao() //Colocar o nome da tela a ser testada
        {
            // try
            // {
            base.ExecuteTest((driver) => //chama o browser e coloca o link correto
            {
                //Maximiza o Browser
                // driverNavigator.Manage().Window.Maximize();
                Login(driver); // realiza o login como administrador

                //COLAR AQUI O SCRIPT GRAVADO
                driver.FindElement(By.Name("NumeroDocumento")).Click();
                driver.FindElement(By.Name("NumeroDocumento")).Clear();
                driver.FindElement(By.Name("NumeroDocumento")).SendKeys("12.212");
                driver.FindElement(By.Id("BotaoPesquisarVP")).Click();
                driver.FindElement(By.XPath("//button[@id='DynamicBotaoEdit0']/i")).Click();
                driver.FindElement(By.Id("DADOSGERAIS")).Click();
                driver.FindElement(By.Name("DataDocumento")).Clear();
                driver.FindElement(By.Name("DataDocumento")).SendKeys("09/09/2023");
                driver.FindElement(By.Id("select_VeiculoPublicacao")).Click();
                driver.FindElement(By.Name("NumeroPublicacao")).Click();
                new SMCSelectElement(driver.FindElement(By.Id("select_VeiculoPublicacao"))).SelectByText("DOU");

                driver.FindElement(By.Name("NumeroPublicacao")).Click();
                driver.FindElement(By.Name("NumeroPublicacao")).Clear();
                driver.FindElement(By.Name("NumeroPublicacao")).SendKeys("111");
                driver.FindElement(By.Name("NumeroSecaoPublicacao")).Clear();
                driver.FindElement(By.Name("NumeroSecaoPublicacao")).SendKeys("2");
                driver.FindElement(By.Name("NumeroPaginaPublicacao")).Clear();
                driver.FindElement(By.Name("NumeroPaginaPublicacao")).SendKeys("3");
                driver.FindElement(By.Name("DataPublicacao")).Clear();
                driver.FindElement(By.Name("DataPublicacao")).SendKeys("09/09/2023");

                driver.FindElement(By.Name("EnderecoEletronico")).SendKeys("http://www.pucminas.br");
                
                //Abrir pasta da rede para incluir o arquivo -> não está abrindo corretamente e por isso foi comentado
                //driver.FindElement(By.CssSelector("[data-type='smc-uploadFile-input'][type='file']")).Click();
                //driver.FindElement(By.CssSelector("[class='smc-uploadFile-input'][type='file']")).Click();
                //driver.FindElement(By.CssSelector("[data-name='ArquivoAnexado'][type='file']")).Click();
                //driver.FindElement(By.Name("files[]")).Click();
                //driver.FindElement(By.Id("ArquivoAnexado")).Click();
                //driver.FindElement(By.Id("ArquivoAnexado")).SendKeys(@"\\GTIWEBDES01\Files\pdf.pdf"); 
                //driver.FindElement(By.CssSelector("[name='files[]'][type='file']")).Click(); 
                //driver.FindElement(By.CssSelector("[title='Pesquisar'][type='submit']")).Click();

                /*    driver.FindElement(By.XPath("//*[@id='Documentos_0__Documentos_0__ArquivoAnexado']/div[1]/input")).SendKeys(@"\\GTIWEBDES01\Files\pdf.pdf");*/

                driver.FindElement(By.Id("btnSalvarVPIE")).Click();

                //Checando a mensagem apresentada (compara a mensagem exibida com a informada)
                Assert.True(CheckMessage("Ato normativo alterado com sucesso."), "Mensagem esperada não exibida");
                driver.Driver.Close();
            });
        }

        

        [Fact] //Deve ser declarada para teste unitario
        public void AtoNormativo_Alteracao_Inclusao_Vinculoentidade() //Colocar o nome da tela a ser testada
        {
            // try
            // {
            base.ExecuteTest((driver) => //chama o browser e coloca o link correto
            {
                //Maximiza o Browser
                // driverNavigator.Manage().Window.Maximize();
                Login(driver); // realiza o login como administrador

                //COLAR AQUI O SCRIPT GRAVADO
                driver.FindElement(By.Name("NumeroDocumento")).Click();
                driver.FindElement(By.Name("NumeroDocumento")).Clear();
                driver.FindElement(By.Name("NumeroDocumento")).SendKeys("12.212");
                driver.FindElement(By.Id("BotaoPesquisarVP")).Click();
                                
                driver.FindElement(By.Id("ButtonSetDropdown1")).Click();
                driver.FindElement(By.Id("DynamicBotao_2")).Click();


                //driver.Driver.Navigate().GoToUrl("https://web-qualidade.pucminas.br/SGA.Administrativo/ORG/AssociacaoEntidades?seqAtoNormativo=7B622C1F5AC72B8D");
                driver.FindElement(By.Id("BotaoNovoVP")).Click();
                driver.FindElement(By.XPath("//button[@id='LookupEntidade_botao_modal']/i")).Click();
                driver.FindElement(By.Id("DataSelector_gridDataSelector2")).Click();
                driver.FindElement(By.XPath("//button[@id='smc-dataselector-LookupEntidade']/i")).Click();
                driver.FindElement(By.Id("btnSalvarVPIN")).Click();

                //Checando a mensagem apresentada (compara a mensagem exibida com a informada)
                Assert.True(CheckMessage("Ato Normativo Entidade incluído com sucesso."), "Mensagem esperada não exibida");
                driver.Driver.Close();
            });
        }

        [Fact] //Deve ser declarada para teste unitario
        public void AtoNormativo_Alteracao_Exclusao_Vinculoentidade() //Colocar o nome da tela a ser testada
        {
            // try
            // {
            base.ExecuteTest((driver) => //chama o browser e coloca o link correto
            {
                //Maximiza o Browser
                // driverNavigator.Manage().Window.Maximize();
                Login(driver); // realiza o login como administrador

                //COLAR AQUI O SCRIPT GRAVADO
                driver.FindElement(By.Name("NumeroDocumento")).Click();
                driver.FindElement(By.Name("NumeroDocumento")).Clear();
                driver.FindElement(By.Name("NumeroDocumento")).SendKeys("12.212");
                driver.FindElement(By.Id("BotaoPesquisarVP")).Click();


                driver.FindElement(By.Id("ButtonSetDropdown1")).Click();
                driver.FindElement(By.Id("DynamicBotao_2")).Click();


                //driver.Driver.Navigate().GoToUrl("https://web-qualidade.pucminas.br/SGA.Administrativo/ORG/AssociacaoEntidades?seqAtoNormativo=7B622C1F5AC72B8D");

                driver.FindElement(By.Id("DynamicBotaoExcluir1")).Click(); //Entidade que está sendo excluída e só pode ter ela: A comunidade de fé, dom para cada pessoa

                driver.FindElement(By.Id("BotaoPadraoPerguntaSim")).Click();

                //Checando a mensagem apresentada (compara a mensagem exibida com a informada)
                Assert.True(CheckMessage("Ato Normativo Entidade excluído com sucesso."), "Mensagem esperada não exibida");
                driver.Driver.Close();
            });
        }

        [Fact] //Deve ser declarada para teste unitario
        public void AtoNormativo_Exclusao() //Colocar o nome da tela a ser testada
        {
            // try
            // {
            base.ExecuteTest((driver) => //chama o browser e coloca o link correto
            {
                //Maximiza o Browser
                // driverNavigator.Manage().Window.Maximize();
                Login(driver); // realiza o login como administrador


                //COLAR AQUI O SCRIPT GRAVADO
                driver.FindElement(By.Name("NumeroDocumento")).Click();
                driver.FindElement(By.Name("NumeroDocumento")).Clear();
                driver.FindElement(By.Name("NumeroDocumento")).SendKeys("12.212");
                driver.FindElement(By.Id("BotaoPesquisarVP")).Click();
                driver.FindElement(By.CssSelector("[title='Excluir'][id='DynamicBotaoExcluir1']")).Click();
                driver.FindElement(By.CssSelector("[title='Sim'][id='BotaoPadraoPerguntaSim']")).Click();

                //Checando a mensagem apresentada (compara a mensagem exibida com a informada)
                //Assert.True(CheckMessage("Ato normativo excluído com sucesso."), "Mensagem esperada não exibida");

                //Checando a mensagem de Sucesso (Verifica se tem a palavra sucesso na mensagem)
                Assert.True(CheckSuccess(), "Era esperado sucesso e ocorreu um erro");
                driver.Driver.Close();

                /* //Declara a variável teste q ira receber o texto do campo
                 string
                 teste = driver.FindElement(By.XPath("//div[@id='centro']/div/div/div[2]")).GetValue();

                 //Compara a mensagem exibida com a esperada:
                 Assert.Equal("Ato normativo excluído com sucesso.", teste);*/


            });
        }
        
        [Fact] //Deve ser declarada para teste unitario
        public void AtoNormativo_Inclusaonaopermitida_Codigozerado() //Colocar o nome da tela a ser testada
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
                driver.FindElement(By.Id("BotaoNovoVP")).Click();
                driver.FindElement(By.Id("select_SeqAssuntoNormativo")).Click();
                new SMCSelectElement(driver.FindElement(By.Id("select_SeqAssuntoNormativo"))).SelectByText("Autorização");
                driver.FindElement(By.Id("select_SeqTipoAtoNormativo")).Click();
                new SMCSelectElement(driver.FindElement(By.Id("select_SeqTipoAtoNormativo"))).SelectByText("Autorização");
                driver.FindElement(By.Name("NumeroDocumento")).Click();
                driver.FindElement(By.Name("NumeroDocumento")).Clear();
                driver.FindElement(By.Name("NumeroDocumento")).SendKeys("0");
                driver.FindElement(By.Name("DataDocumento")).Click();
                driver.FindElement(By.Name("DataDocumento")).SendKeys("08/09/2023");
                driver.FindElement(By.Name("DataPrazoValidade")).Click();
                /*driver.FindElement(By.Name("NumeroSecaoPublicacao")).Clear();
                driver.FindElement(By.Name("NumeroSecaoPublicacao")).SendKeys("0");
                driver.FindElement(By.Name("NumeroPaginaPublicacao")).Clear();
                driver.FindElement(By.Name("NumeroPaginaPublicacao")).SendKeys("0");*/


                //driver.FindElement(By.XPath("(.//*[normalize-space(text()) and normalize-space(.)='Sáb'])[6]/following::div[5]")).Click();
                driver.FindElement(By.Id("btnSalvarVPIN")).Click();

                //Declara a variável teste q ira receber o texto do campo
                string
                teste = driver.FindElement(By.XPath("//div[@id='default']/div[2]/div[2]/div[2]/p")).GetValue();

                //Compara a mensagem exibida com a esperada:
                Assert.Equal("Operação não permitida. Não é aceito número de identificação igual a " + '\u0022' + "0" + '\u0022', teste);
                driver.Driver.Close();

            });
        }

        [Fact] //Deve ser declarada para teste unitario
        public void AtoNormativo_Inclusaonaopermitida_Secaozerada() //Colocar o nome da tela a ser testada
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
                driver.FindElement(By.Id("BotaoNovoVP")).Click();
                driver.FindElement(By.Id("select_SeqAssuntoNormativo")).Click();
                new SMCSelectElement(driver.FindElement(By.Id("select_SeqAssuntoNormativo"))).SelectByText("Autorização");
                driver.FindElement(By.Id("select_SeqTipoAtoNormativo")).Click();
                new SMCSelectElement(driver.FindElement(By.Id("select_SeqTipoAtoNormativo"))).SelectByText("Autorização");
                driver.FindElement(By.Name("NumeroDocumento")).Click();
                driver.FindElement(By.Name("NumeroDocumento")).Clear();
                driver.FindElement(By.Name("NumeroDocumento")).SendKeys("999");
                driver.FindElement(By.Name("DataDocumento")).Click();
                driver.FindElement(By.Name("DataDocumento")).SendKeys("08/09/2023");
                driver.FindElement(By.Name("DataPrazoValidade")).Click();
                driver.FindElement(By.Id("select_VeiculoPublicacao")).Click();
                new SMCSelectElement(driver.FindElement(By.Id("select_VeiculoPublicacao"))).SelectByText("DOU");

                driver.FindElement(By.Name("NumeroPublicacao")).Click();
                driver.FindElement(By.Name("NumeroPublicacao")).Clear();
                driver.FindElement(By.Name("NumeroPublicacao")).SendKeys("111");
                driver.FindElement(By.Name("NumeroSecaoPublicacao")).Clear();
                driver.FindElement(By.Name("NumeroSecaoPublicacao")).SendKeys("0");
                driver.FindElement(By.Name("NumeroPaginaPublicacao")).Clear();
                driver.FindElement(By.Name("NumeroPaginaPublicacao")).SendKeys("3");
                driver.FindElement(By.Name("DataPublicacao")).Clear();
                driver.FindElement(By.Name("DataPublicacao")).SendKeys("09/09/2023");



                //driver.FindElement(By.XPath("(.//*[normalize-space(text()) and normalize-space(.)='Sáb'])[6]/following::div[5]")).Click();
                driver.FindElement(By.Id("btnSalvarVPIN")).Click();

                //Declara a variável teste q ira receber o texto do campo
                string
                teste = driver.FindElement(By.XPath("//div[@id='default']/div[2]/div[2]/div[2]/p")).GetValue();

                //Compara a mensagem exibida com a esperada:
                Assert.Equal("Operação não permitida. Não é aceito número de identificação igual a " + '\u0022' + "0" + '\u0022', teste);
                driver.Driver.Close();

            });
        }
        [Fact] //Deve ser declarada para teste unitario
        public void AtoNormativo_Inclusaonaopermitida_Paginazerada() //Colocar o nome da tela a ser testada
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
                driver.FindElement(By.Id("BotaoNovoVP")).Click();
                driver.FindElement(By.Id("select_SeqAssuntoNormativo")).Click();
                new SMCSelectElement(driver.FindElement(By.Id("select_SeqAssuntoNormativo"))).SelectByText("Autorização");
                driver.FindElement(By.Id("select_SeqTipoAtoNormativo")).Click();
                new SMCSelectElement(driver.FindElement(By.Id("select_SeqTipoAtoNormativo"))).SelectByText("Autorização");
                driver.FindElement(By.Name("NumeroDocumento")).Click();
                driver.FindElement(By.Name("NumeroDocumento")).Clear();
                driver.FindElement(By.Name("NumeroDocumento")).SendKeys("888");
                driver.FindElement(By.Name("DataDocumento")).Click();
                driver.FindElement(By.Name("DataDocumento")).SendKeys("08/09/2023");
                driver.FindElement(By.Name("DataPrazoValidade")).Click();

                driver.FindElement(By.Id("select_VeiculoPublicacao")).Click();
                new SMCSelectElement(driver.FindElement(By.Id("select_VeiculoPublicacao"))).SelectByText("DOU");

                driver.FindElement(By.Name("NumeroPublicacao")).Click();
                driver.FindElement(By.Name("NumeroPublicacao")).Clear();
                driver.FindElement(By.Name("NumeroPublicacao")).SendKeys("111");
                driver.FindElement(By.Name("NumeroSecaoPublicacao")).Clear();
                driver.FindElement(By.Name("NumeroSecaoPublicacao")).SendKeys("2");
                driver.FindElement(By.Name("NumeroPaginaPublicacao")).Clear();
                driver.FindElement(By.Name("NumeroPaginaPublicacao")).SendKeys("0");
                driver.FindElement(By.Name("DataPublicacao")).Clear();
                driver.FindElement(By.Name("DataPublicacao")).SendKeys("09/09/2023");


                //driver.FindElement(By.XPath("(.//*[normalize-space(text()) and normalize-space(.)='Sáb'])[6]/following::div[5]")).Click();
                driver.FindElement(By.Id("btnSalvarVPIN")).Click();

                //Declara a variável teste q ira receber o texto do campo
                string
                teste = driver.FindElement(By.XPath("//div[@id='default']/div[2]/div[2]/div[2]/p")).GetValue();

                //Compara a mensagem exibida com a esperada:
                Assert.Equal("Operação não permitida. Não é aceito número de identificação igual a " + '\u0022' + "0" + '\u0022', teste);
                driver.Driver.Close();
            });
        }

        [Fact]
        [Trait("Ordenado", "CRUD")]
        public void TesteOrdenadoCRUD()
        {
            AtoNormativo_Inclusao();
            AtoNormativo_Inclusaoduplicada();
            AtoNormativo_Inclusaonaopermitida_Codigozerado();
            AtoNormativo_Inclusaonaopermitida_Secaozerada();
            AtoNormativo_Inclusaonaopermitida_Paginazerada();
            AtoNormativo_Alteracao();
            AtoNormativo_Alteracao_Inclusao_Vinculoentidade();
            AtoNormativo_Alteracao_Exclusao_Vinculoentidade();
            AtoNormativo_Exclusao();
          


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

            driver.GoToUrl2(Consts.SERVIDOR_QUALIDADE_ADM +"ORG/AtoNormativo"); //coloca o resto do endereço para acessar a pagina do teste


        }
    }
}


