using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using ExpectedConditions = SeleniumExtras.WaitHelpers.ExpectedConditions;
using SMC.Framework.Test; //Framework de testes
using System.Threading;
using Xunit; //Usado no Fact
using System;



namespace SMC.Academico.Tests.ADM.CNC //Arvore onde esta o arquivo
{

    public class Curso : SMCSeleniumUnitTest //Declara o nome da classe e recebe a SMCSeleniumUnitTest
    {


        [Fact] //Deve ser declarada para teste unitario
        public void Curso_Inclusao() //Colocar o nome da tela a ser testada
        {
            //try
            {
                base.ExecuteTest((driver) => //chama o browser e coloca o link correto
                {
                //Maximiza o Browser
                // driverNavigator.Manage().Window.Maximize();
                Login(driver); // realiza o login como administrador

                //Colar o script aqui

                driver.FindElement(By.Id("BotaoNovoVP")).Click();
                    driver.FindElement(By.Id("select_SeqNivelEnsino")).Click();
                    new SMCSelectElement(driver.FindElement(By.Id("select_SeqNivelEnsino"))).SelectByText("Mestrado Acadêmico");
                    driver.FindElement(By.Id("editorWizard-btnProximo")).Click();
                    driver.FindElement(By.Name("Nome")).Click();
                    driver.FindElement(By.Name("Nome")).Clear();
                    driver.FindElement(By.Name("Nome")).SendKeys("Automação");
                    driver.FindElement(By.Name("Sigla")).Click();
                    driver.FindElement(By.Name("Sigla")).Clear();
                    driver.FindElement(By.Name("Sigla")).SendKeys("AUT");
                    driver.FindElement(By.Name("NomeReduzido")).Click();
                    driver.FindElement(By.Name("NomeReduzido")).Clear();
                    driver.FindElement(By.Name("NomeReduzido")).SendKeys("Auto");
                    driver.FindElement(By.Id("select_TipoCurso")).Click();
                    new SMCSelectElement(driver.FindElement(By.Id("select_TipoCurso"))).SelectByText("Normal");
                    driver.FindElement(By.Id("select_HierarquiasEntidades_0__SeqItemSuperior")).Click();
                    new SMCSelectElement(driver.FindElement(By.Id("select_HierarquiasEntidades_0__SeqItemSuperior"))).SelectByText("Departamento de Administração");
                    driver.FindElement(By.Id("select_HierarquiasEntidades_0__SeqItemSuperior")).Click();
                    new SMCSelectElement(driver.FindElement(By.Id("select_HierarquiasEntidades_0__SeqItemSuperior"))).SelectByText("Departamento de Engenharia de Software e Sistemas de Informação");
                    driver.FindElement(By.Id("select_SeqSituacaoAtual")).Click();
                    new SMCSelectElement(driver.FindElement(By.Id("select_SeqSituacaoAtual"))).SelectByText("Extinto");
                    driver.FindElement(By.Id("select_SeqSituacaoAtual")).Click();
                    new SMCSelectElement(driver.FindElement(By.Id("select_SeqSituacaoAtual"))).SelectByText("Em atividade");
                    // O selectbyText foi feito duas vezes porque o Primeiro não selecionou
                    Thread.Sleep(6000);
                    driver.FindElement(By.Id("editorWizard-btnProximo")).Click();
                    driver.FindElement(By.XPath("//button[@id='Enderecos_DetailBotaoInserirElemento']/i")).Click();
                    driver.FindElement(By.Id("select_Enderecos_0__TipoEndereco")).Click();
                    new SMCSelectElement(driver.FindElement(By.Id("select_Enderecos_0__TipoEndereco"))).SelectByText("Comercial");
                    driver.FindElement(By.Name("Enderecos[0].Cep")).Click();
                    driver.FindElement(By.Name("Enderecos[0].Cep")).Clear();
                    driver.FindElement(By.Name("Enderecos[0].Cep")).SendKeys("30720-490");
                    driver.FindElement(By.Name("Enderecos[0].Logradouro")).Click();
                    driver.FindElement(By.Name("Enderecos[0].Numero")).Clear();
                    Thread.Sleep(6000);
                    driver.FindElement(By.Name("Enderecos[0].Numero")).SendKeys("160");
                    driver.FindElement(By.Name("Enderecos[0].Correspondencia")).Click();
                    driver.FindElement(By.XPath("//button[@id='EnderecosEletronicos_DetailBotaoInserirElemento']/i")).Click();
                    driver.FindElement(By.Id("select_EnderecosEletronicos_0__DescricaoTipoEnderecoEletronico")).Click();
                    new SMCSelectElement(driver.FindElement(By.Id("select_EnderecosEletronicos_0__DescricaoTipoEnderecoEletronico"))).SelectByText("E-mail");
                    driver.FindElement(By.Name("EnderecosEletronicos[0].Descricao")).Click();
                    driver.FindElement(By.Name("EnderecosEletronicos[0].Descricao")).Clear();
                    driver.FindElement(By.Name("EnderecosEletronicos[0].Descricao")).SendKeys("automacao@gmail.com");
                    driver.FindElement(By.XPath("//button[@id='Telefones_DetailBotaoInserirElemento']/i")).Click();
                    driver.FindElement(By.Id("select_Telefones_0__DescricaoTipoTelefone")).Click();
                    new SMCSelectElement(driver.FindElement(By.Id("select_Telefones_0__DescricaoTipoTelefone"))).SelectByText("Comercial");
                    driver.FindElement(By.Name("Telefones[0].CodigoArea")).Click();
                    driver.FindElement(By.Name("Telefones[0].CodigoArea")).Clear();
                    driver.FindElement(By.Name("Telefones[0].CodigoArea")).SendKeys("31");
                    driver.FindElement(By.Name("Telefones[0].Numero")).Click();
                    driver.FindElement(By.Name("Telefones[0].Numero")).Clear();
                    driver.FindElement(By.Name("Telefones[0].Numero")).SendKeys("99999-9999");
                    driver.FindElement(By.XPath("//a[@id='editorWizard-btnProximo']/i")).Click();

                    // driver.FindElement(By.Id("Hierarquias_0__Classificacoes_botao_modal")).Click();
                    // driver.FindElement(By.Id("6a324264-fac9-499b-bf98-8b88ebba08aa")).Click();
                    driver.FindElement(By.Id("editorWizard-btnProximo")).Click();
                    Thread.Sleep(4000);
                    driver.FindElement(By.Id("WizardBotaoSalvar1")).Click();

                //Checando a mensagem de Sucesso 
                    Assert.True(CheckMessage("Curso incluído com sucesso."), "Era esperado sucesso e ocorreu um erro");

                driver.Driver.Close();

                });
            }
           
        }

        [Fact] //Deve ser declarada para teste unitario
        public void Curso_InclusaoDuplicada() //Colocar o nome da tela a ser testada
        {
            //try
            {
                base.ExecuteTest((driver) => //chama o browser e coloca o link correto
                {
                    //Maximiza o Browser
                    // driverNavigator.Manage().Window.Maximize();
                    Login(driver); // realiza o login como administrador

                    //Colar o script aqui

                driver.FindElement(By.Id("BotaoNovoVP")).Click();
                driver.FindElement(By.Id("select_SeqNivelEnsino")).Click();
                new SMCSelectElement(driver.FindElement(By.Id("select_SeqNivelEnsino"))).SelectByText("Mestrado Acadêmico");
                driver.FindElement(By.Id("editorWizard-btnProximo")).Click();
                driver.FindElement(By.Name("Nome")).Click();
                driver.FindElement(By.Name("Nome")).Clear();
                driver.FindElement(By.Name("Nome")).SendKeys("Automação");
                driver.FindElement(By.Name("Sigla")).Click();
                driver.FindElement(By.Name("Sigla")).Clear();
                driver.FindElement(By.Name("Sigla")).SendKeys("AUT");
                driver.FindElement(By.Name("NomeReduzido")).Click();
                driver.FindElement(By.Name("NomeReduzido")).Clear();
                driver.FindElement(By.Name("NomeReduzido")).SendKeys("Auto");
                driver.FindElement(By.Id("select_TipoCurso")).Click();
                new SMCSelectElement(driver.FindElement(By.Id("select_TipoCurso"))).SelectByText("Normal");
                driver.FindElement(By.Id("select_HierarquiasEntidades_0__SeqItemSuperior")).Click();
                new SMCSelectElement(driver.FindElement(By.Id("select_HierarquiasEntidades_0__SeqItemSuperior"))).SelectByText("Departamento de Administração");
                driver.FindElement(By.Id("select_HierarquiasEntidades_0__SeqItemSuperior")).Click();
                new SMCSelectElement(driver.FindElement(By.Id("select_HierarquiasEntidades_0__SeqItemSuperior"))).SelectByText("Departamento de Engenharia de Software e Sistemas de Informação");
                driver.FindElement(By.Id("select_SeqSituacaoAtual")).Click();
                new SMCSelectElement(driver.FindElement(By.Id("select_SeqSituacaoAtual"))).SelectByText("Extinto");
                driver.FindElement(By.Id("select_SeqSituacaoAtual")).Click();
                new SMCSelectElement(driver.FindElement(By.Id("select_SeqSituacaoAtual"))).SelectByText("Em atividade");
                // O selectbyText foi feito duas vezes porque o Primeiro não selecionou
                driver.FindElement(By.Id("editorWizard-btnProximo")).Click();
                driver.FindElement(By.XPath("//button[@id='Enderecos_DetailBotaoInserirElemento']/i")).Click();
                driver.FindElement(By.Id("select_Enderecos_0__TipoEndereco")).Click();
                new SMCSelectElement(driver.FindElement(By.Id("select_Enderecos_0__TipoEndereco"))).SelectByText("Comercial");
                driver.FindElement(By.Name("Enderecos[0].Cep")).Click();
                driver.FindElement(By.Name("Enderecos[0].Cep")).Clear();
                driver.FindElement(By.Name("Enderecos[0].Cep")).SendKeys("30720-490");
                driver.FindElement(By.Name("Enderecos[0].Logradouro")).Click();
                driver.FindElement(By.Name("Enderecos[0].Numero")).Clear();
                Thread.Sleep(4000);
                driver.FindElement(By.Name("Enderecos[0].Numero")).SendKeys("160");
                driver.FindElement(By.Name("Enderecos[0].Correspondencia")).Click();
                driver.FindElement(By.XPath("//button[@id='EnderecosEletronicos_DetailBotaoInserirElemento']/i")).Click();
                driver.FindElement(By.Id("select_EnderecosEletronicos_0__DescricaoTipoEnderecoEletronico")).Click();
                new SMCSelectElement(driver.FindElement(By.Id("select_EnderecosEletronicos_0__DescricaoTipoEnderecoEletronico"))).SelectByText("E-mail");
                driver.FindElement(By.Name("EnderecosEletronicos[0].Descricao")).Click();
                driver.FindElement(By.Name("EnderecosEletronicos[0].Descricao")).Clear();
                driver.FindElement(By.Name("EnderecosEletronicos[0].Descricao")).SendKeys("automacao@gmail.com");
                driver.FindElement(By.XPath("//button[@id='Telefones_DetailBotaoInserirElemento']/i")).Click();
                driver.FindElement(By.Id("select_Telefones_0__DescricaoTipoTelefone")).Click();
                new SMCSelectElement(driver.FindElement(By.Id("select_Telefones_0__DescricaoTipoTelefone"))).SelectByText("Comercial");
                driver.FindElement(By.Name("Telefones[0].CodigoArea")).Click();
                driver.FindElement(By.Name("Telefones[0].CodigoArea")).Clear();
                driver.FindElement(By.Name("Telefones[0].CodigoArea")).SendKeys("31");
                driver.FindElement(By.Name("Telefones[0].Numero")).Click();
                driver.FindElement(By.Name("Telefones[0].Numero")).Clear();
                driver.FindElement(By.Name("Telefones[0].Numero")).SendKeys("99999-9999");
                driver.FindElement(By.XPath("//a[@id='editorWizard-btnProximo']/i")).Click();

                // driver.FindElement(By.Id("Hierarquias_0__Classificacoes_botao_modal")).Click();
                // driver.FindElement(By.Id("6a324264-fac9-499b-bf98-8b88ebba08aa")).Click();
                Thread.Sleep(1600);
                driver.FindElement(By.Id("editorWizard-btnProximo")).Click();
                Thread.Sleep(4000);
                driver.FindElement(By.Id("WizardBotaoSalvar1")).Click();

                //Checando a mensagem de Sucesso 
                Assert.True(CheckMessage("Já existe entidade cadastrada com este nome."), "Era esperado sucesso e ocorreu um erro");
                    
                driver.Driver.Close();

                });
            }

        }

        [Fact] //Deve ser declarada para teste unitario
        public void Curso_Alteracao() //Colocar o nome da tela a ser testada
        {
            base.ExecuteTest((driver) => //chama o browser e coloca o link correto
            {
                //Maximiza o Browser
                // driverNavigator.Manage().Window.Maximize();
                Login(driver); // realiza o login como administrador

       //Colar o script aqui

        driver.FindElement(By.Name("Nome")).Click();
        driver.FindElement(By.Name("Nome")).Clear();
        driver.FindElement(By.Name("Nome")).SendKeys("Automação");
        driver.FindElement(By.Id("BotaoPesquisarVP")).Click();
        driver.FindElement(By.XPath("//a[@id='DetailListBotaoAlterar0']/i")).Click();
        driver.FindElement(By.Name("Nome")).Click();
        driver.FindElement(By.Name("Nome")).Clear();
        driver.FindElement(By.Name("Nome")).SendKeys("Automação II");
        Thread.Sleep(1600);
        driver.FindElement(By.XPath("//button[@id='BotaoSalvarTemplate']/i")).Click();
        Thread.Sleep(1600);
        Assert.True(CheckSuccess(), "Era esperado sucesso e ocorreu um erro");


            });
        }

        [Fact] //Deve ser declarada para teste unitario
        public void Curso_Exclusao() //Colocar o nome da tela a ser testada
        {
            base.ExecuteTest((driver) => //chama o browser e coloca o link correto
            {
                //Maximiza o Browser
                // driverNavigator.Manage().Window.Maximize();
                Login(driver); // realiza o login como administrador

       //Colar o script aqui
        driver.FindElement(By.Name("Nome")).Click();
        driver.FindElement(By.Name("Nome")).Clear();
        driver.FindElement(By.Name("Nome")).SendKeys("Automação");
        driver.FindElement(By.Id("BotaoPesquisarVP")).Click();
        driver.FindElement(By.XPath("//a[@id='DetailListBotaoExcluir0']/i")).Click();
        driver.FindElement(By.Id("BotaoPadraoPerguntaSim")).Click();

        Assert.True(CheckMessage("Curso excluído com sucesso."), "Era esperado sucesso e ocorreu um erro");
                                
                driver.Driver.Close();
            });
        }

        [Fact] //Deve ser declarada para teste unitario
        public void Curso_Exclusaonaopermitida() //Colocar o nome da tela a ser testada
        {
            base.ExecuteTest((driver) => //chama o browser e coloca o link correto
            {
                //Maximiza o Browser
                // driverNavigator.Manage().Window.Maximize();
                Login(driver); // realiza o login como administrador

                //Colar o script aqui

                driver.FindElement(By.Name("Nome")).Click();
                driver.FindElement(By.Name("Nome")).Clear();
                driver.FindElement(By.Name("Nome")).SendKeys("administração");
                driver.FindElement(By.XPath("//button[@id='BotaoPesquisarVP']/i")).Click();
                driver.FindElement(By.XPath("//a[@id='DetailListBotaoExcluir0']/i")).Click();
                driver.FindElement(By.Id("BotaoPadraoPerguntaSim")).Click();
                Assert.True(CheckMessage("Exclusão não permitida. Curso x oferta já possui curso x oferta x localidade cadastrado"), "Era esperado sucesso e ocorreu um erro");
                                          

               driver.Driver.Close();
            });
        }



        [Fact]
        [Trait("Ordenado", "CRUD")]
        public void TesteOrdenadoCRUD()
        {
            Curso_Inclusao();
            Curso_InclusaoDuplicada();
            Curso_Alteracao();
            Curso_Exclusao();
            Curso_Exclusaonaopermitida();
        }


        private static void Login(ISMCWebDriver driver)
        {
            //----------------------------------------------------
            driver.GoToUrl(Consts.SERVIDOR_QUALIDADE_ADM); //recebe o link para acesso

            //Login
            driver.SMCLoginCpf(); //insere login e senha

            driver.GoToUrl2(Consts.SERVIDOR_QUALIDADE_ADM + "CSO/Curso"); //coloca o resto do endere�o para acessar a pagina do teste

        }
    }
}
