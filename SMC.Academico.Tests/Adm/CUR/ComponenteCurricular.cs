using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using ExpectedConditions = SeleniumExtras.WaitHelpers.ExpectedConditions;
using SMC.Framework.Test; //Framework de testes
using System.Threading;
using Xunit; //Usado no Fact
using System;


namespace SMC.Academico.Tests.ADM.CSO //Arvore onde esta o arquivo
{

    public class ComponenteCurricular : SMCSeleniumUnitTest //Declara o nome da classe e recebe a SMCSeleniumUnitTest
    {
        [Fact] //Deve ser declarada para teste unitario
        public void ComponenteCurricular_Inclusao() //Colocar o nome da tela a ser testada
        {
            base.ExecuteTest((driver) => //chama o browser e coloca o link correto

            {
                //Maximiza o Browser

                // driverNavigator.Manage().Window.Maximize();

                Login(driver); // realiza o login como administrador

                //Colar o script aqui

                driver.FindElement(By.Id("BotaoNovoVP")).Click();
                driver.FindElement(By.Id("select_SeqInstituicaoNivelResponsavel")).Click();
                new SMCSelectElement(driver.FindElement(By.Id("select_SeqInstituicaoNivelResponsavel"))).SelectByText("Mestrado Acadêmico");
                driver.FindElement(By.Id("select_SeqTipoComponenteCurricular")).Click();
                new SMCSelectElement(driver.FindElement(By.Id("select_SeqTipoComponenteCurricular"))).SelectByText("Atividade Acadêmica");
                driver.FindElement(By.XPath("//a[@id='editorWizard-btnProximo']/i")).Click();
                driver.FindElement(By.Name("Descricao")).Click();
                driver.FindElement(By.Name("Descricao")).Clear();
                driver.FindElement(By.Name("Descricao")).SendKeys("Automação");
                driver.FindElement(By.Name("DescricaoReduzida")).Click();
                driver.FindElement(By.Name("DescricaoReduzida")).Clear();
                driver.FindElement(By.Name("DescricaoReduzida")).SendKeys("AUT");
                driver.FindElement(By.Name("Observacao")).Click();
                driver.FindElement(By.Id("editorWizard-btnProximo")).Click();
                driver.FindElement(By.XPath("//button[@id='NiveisEnsino_DetailBotaoInserirElemento']/i")).Click();
                driver.FindElement(By.Id("select_NiveisEnsino_0__SeqNivelEnsino")).Click();
                new SMCSelectElement(driver.FindElement(By.Id("select_NiveisEnsino_0__SeqNivelEnsino"))).SelectByText("Doutorado Acadêmico");
                Thread.Sleep(1600);
                driver.FindElement(By.XPath("//button[@id='WizardBotaoSalvar1']/i")).Click();
                //driver.FindElement(By.CssSelector("[title='Salvar'][type='submit']")).Click();

                //Checando a mensagem de Sucesso (Verifica se tem a palavra sucesso na mensagem)
                //Assert.True(CheckSuccess(), "Era esperado sucesso e ocorreu um erro");

                Thread.Sleep(1600);
                //Checando a mensagem apresentada (compara a mensagem exibida com a informada)
                Assert.True(CheckMessage("Componente curricular incluído com sucesso."), "Mensagem esperada não exibida");

                /*//Declara a variável teste q ira receber o texto do campo
                string
                teste = driver.FindElement(By.XPath("//div[@id='centro']/div/div/div[2]")).GetValue();
                Thread.Sleep(12000);
                //Compara a mensagem exibida com a esperada:
               Assert.Equal("Componente curricular incluído com sucesso.", teste);*/
                driver.Driver.Close();

            });

        }


        [Fact] //Deve ser declarada para teste unitario
        public void ComponenteCurricular_InclusaoDuplicada() //Colocar o nome da tela a ser testada
        {
            base.ExecuteTest((driver) => //chama o browser e coloca o link correto

            {
                //Maximiza o Browser

                // driverNavigator.Manage().Window.Maximize();

                Login(driver); // realiza o login como administrador

                //Colar o script aqui

                driver.FindElement(By.Id("BotaoNovoVP")).Click();
                driver.FindElement(By.Id("select_SeqInstituicaoNivelResponsavel")).Click();
                new SMCSelectElement(driver.FindElement(By.Id("select_SeqInstituicaoNivelResponsavel"))).SelectByText("Mestrado Acadêmico");
                driver.FindElement(By.Id("select_SeqTipoComponenteCurricular")).Click();
                new SMCSelectElement(driver.FindElement(By.Id("select_SeqTipoComponenteCurricular"))).SelectByText("Atividade Acadêmica");
                driver.FindElement(By.XPath("//a[@id='editorWizard-btnProximo']/i")).Click();
                driver.FindElement(By.Name("Descricao")).Click();
                driver.FindElement(By.Name("Descricao")).Clear();
                driver.FindElement(By.Name("Descricao")).SendKeys("Automação");
                driver.FindElement(By.Name("DescricaoReduzida")).Click();
                driver.FindElement(By.Name("DescricaoReduzida")).Clear();
                driver.FindElement(By.Name("DescricaoReduzida")).SendKeys("AUT");
                driver.FindElement(By.Name("Observacao")).Click();
                driver.FindElement(By.Id("editorWizard-btnProximo")).Click();
                driver.FindElement(By.XPath("//button[@id='NiveisEnsino_DetailBotaoInserirElemento']/i")).Click();
                driver.FindElement(By.Id("select_NiveisEnsino_0__SeqNivelEnsino")).Click();
                new SMCSelectElement(driver.FindElement(By.Id("select_NiveisEnsino_0__SeqNivelEnsino"))).SelectByText("Doutorado Acadêmico");
                Thread.Sleep(1600);
                driver.FindElement(By.XPath("//button[@id='WizardBotaoSalvar1']/i")).Click();
                //driver.FindElement(By.CssSelector("[title='Salvar'][type='submit']")).Click();

                // Assert.True(CheckMessage("Alteração não permitida.Já existe componente com mesmo tipo, descrição, carga-horária, crédito e entidade responsável"), "Era esperado sucesso e ocorreu um erro");

                Thread.Sleep(1600);
                //Checando a mensagem apresentada (compara a mensagem exibida com a informada)
                Assert.True(CheckMessage("Alteração não permitida. Já existe componente com mesmo tipo, descrição, carga-horária, crédito e entidade responsável"), "Mensagem esperada não exibida");
                                          


                /*//Declara a variável teste q ira receber o texto do campo
                string
                 teste = driver.FindElement(By.XPath("//div[@id='centro']/div/div/div[2]")).GetValue();
                //Compara a mensagem exibida com a esperada:
                 Assert.Equal("Alteração não permitida.Já existe componente com mesmo tipo, descrição, carga-horária, crédito e entidade responsável", teste);*/

                driver.Driver.Close();



            });
        }


        [Fact] //Deve ser declarada para teste unitario
        public void ComponenteCurricular_Alteracao() //Colocar o nome da tela a ser testada
        {
            base.ExecuteTest((driver) => //chama o browser e coloca o link correto

            {
                //Maximiza o Browser

                // driverNavigator.Manage().Window.Maximize();

                Login(driver); // realiza o login como administrador

                //Colar o script aqui

                driver.FindElement(By.Name("Codigo")).Click();
                driver.FindElement(By.Name("Codigo")).Clear();
                driver.FindElement(By.Name("Codigo")).SendKeys("286");
                driver.FindElement(By.Id("BotaoPesquisarVP")).Click();
                driver.FindElement(By.XPath("//a[@id='DetailListBotaoAlterar0']/i")).Click();
                driver.FindElement(By.XPath("//div[@id='ExigeAssuntoComponente']/div/div[2]/label/input")).Click();
                Thread.Sleep(1600);
                driver.FindElement(By.Id("BotaoSalvarTemplate")).Click();
                //driver.FindElement(By.CssSelector("[title='Salvar'][type='submit']")).Click();

                //Checando a mensagem de Sucesso (Verifica se tem a palavra sucesso na mensagem)
                Thread.Sleep(1600);
                /*Assert.True(CheckMessage("Alteração não permitida.Já existe componente com mesmo tipo, descrição, carga-horária, crédito e entidade responsável"), "Mensagem esperada não exibida");*/
                //Declara a variável teste q ira receber o texto do campo
                string
                teste = driver.FindElement(By.XPath("//div[@id='centro']/div/div/div[2]")).GetValue();

                //Compara a mensagem exibida com a esperada:
                Assert.Equal("Componente curricular alterado com sucesso.", teste);
                driver.Driver.Close();
            });
        }


        [Fact] //Deve ser declarada para teste unitario
        public void ComponenteCurricular_Exclusao() //Colocar o nome da tela a ser testada
        {
            base.ExecuteTest((driver) => //chama o browser e coloca o link correto

            {
                //Maximiza o Browser

                // driverNavigator.Manage().Window.Maximize();

                Login(driver); // realiza o login como administrador

                //Colar o script aqui

        driver.FindElement(By.Name("Descricao")).Click();
        driver.FindElement(By.Name("Descricao")).Clear();
        driver.FindElement(By.Name("Descricao")).SendKeys("Automação");
        driver.FindElement(By.Id("BotaoPesquisarVP")).Click();
        driver.FindElement(By.XPath("//a[@id='DetailListBotaoExcluir0']/i")).Click();
        driver.FindElement(By.Id("BotaoPadraoPerguntaSim")).Click();
        string
        teste = driver.FindElement(By.XPath("//div[@id='centro']/div/div/div[2]")).GetValue();

            //Compara a mensagem exibida com a esperada:
                Thread.Sleep(1600);
                Assert.Equal("Componente curricular excluído com sucesso.", teste);
                driver.Driver.Close();


            });
        }

        [Fact] //Deve ser declarada para teste unitario
        public void ComponenteCurricular_Exclusaonaopermitida() //Colocar o nome da tela a ser testada
        {
            base.ExecuteTest((driver) => //chama o browser e coloca o link correto

            {
                //Maximiza o Browser
                // driverNavigator.Manage().Window.Maximize();

                Login(driver); // realiza o login como administrador

                //Colar o script aqui
                driver.FindElement(By.Name("Descricao")).Click();
                driver.FindElement(By.Name("Descricao")).Clear();
                driver.FindElement(By.Name("Descricao")).SendKeys("\" Modos de categorização  e Ensino de Biologia: perfil do conceito de vida\"");
                driver.FindElement(By.Id("BotaoPesquisarVP")).Click();
                Thread.Sleep(1600);
                driver.FindElement(By.XPath("//a[@id='DetailListBotaoExcluir0']/i")).Click();
                driver.FindElement(By.Id("BotaoPadraoPerguntaSim")).Click();

                Thread.Sleep(1600);
                //Assert.True(CheckMessage("Exclusão não permitida. Componente curricular já possui lançamento de histórico escolar(componente substituto)."), "Mensagem esperada não exibida");
                Assert.True(CheckMessage("Exclusão não permitida."), "Mensagem esperada não exibida");

                /*
                //Declara a variável teste q ira receber o texto do campo
                string
                teste = driver.FindElement(By.XPath("//div[@id='centro']/div/div/div[2]")).GetValue();

                //Compara a mensagem exibida com a esperada:
                Assert.Equal("Exclusão não permitida.Componente curricular já possui lançamento de histórico escolar(componente substituto).", teste);*/

                driver.Driver.Close();                
            });
        }

        [Fact] //Deve ser declarada para teste unitario
        public void ComponenteCurricular_Inclusao_ComponentecomOrganizacao() //Colocar o nome da tela a ser testada
        {
            base.ExecuteTest((driver) => //chama o browser e coloca o link correto

            {
                //Maximiza o Browser

                // driverNavigator.Manage().Window.Maximize();

                Login(driver); // realiza o login como administrador

                //Colar o script aqui
                driver.FindElement(By.Id("BotaoNovoVP")).Click();
                driver.FindElement(By.Id("select_SeqInstituicaoNivelResponsavel")).Click();
                driver.FindElement(By.Id("select_SeqInstituicaoNivelResponsavel")).Click();
                driver.FindElement(By.Id("select_SeqInstituicaoNivelResponsavel")).Click();
                new SMCSelectElement(driver.FindElement(By.Id("select_SeqInstituicaoNivelResponsavel"))).SelectByText("Mestrado Acadêmico");
                driver.FindElement(By.Id("select_SeqTipoComponenteCurricular")).Click();
                new SMCSelectElement(driver.FindElement(By.Id("select_SeqTipoComponenteCurricular"))).SelectByText("Disciplina");
                driver.FindElement(By.Id("editorWizard-btnProximo")).Click();
                driver.FindElement(By.Name("Descricao")).Click();
                driver.FindElement(By.Name("Descricao")).Clear();
                driver.FindElement(By.Name("Descricao")).SendKeys("Seminário de tese - automação");
                driver.FindElement(By.Name("CargaHoraria")).Click();
                driver.FindElement(By.Name("CargaHoraria")).Clear();
                driver.FindElement(By.Name("CargaHoraria")).SendKeys("30");
                Thread.Sleep(1600);
                driver.FindElement(By.Name("Ementas[0].Ementa")).Click();
                driver.FindElement(By.Name("Ementas[0].Ementa")).Clear();
                driver.FindElement(By.Name("Ementas[0].Ementa")).SendKeys("Ementa automação componente com organização");
                
                driver.FindElement(By.Name("Ementas[0].DataInicio")).Click();
                driver.FindElement(By.XPath("(.//*[normalize-space(text()) and normalize-space(.)='Sáb'])[1]/following::td[20]")).Click();
                driver.FindElement(By.XPath("//a[@id='editorWizard-btnProximo']/i")).Click();
                driver.FindElement(By.Id("select_TipoOrganizacao")).Click();
                new SMCSelectElement(driver.FindElement(By.Id("select_TipoOrganizacao"))).SelectByText("Tópico");
                driver.FindElement(By.XPath("//button[@id='Organizacoes_DetailBotaoInserirElemento']/i")).Click();
                driver.FindElement(By.Name("Organizacoes[0].Descricao")).Click();
                driver.FindElement(By.Name("Organizacoes[0].Descricao")).Clear();
                driver.FindElement(By.Name("Organizacoes[0].Descricao")).SendKeys("organização automação");
                driver.FindElement(By.Name("Organizacoes[0].Ativo")).Click();
                driver.FindElement(By.Id("select_EntidadesResponsaveis_0__SeqEntidade")).Click();
                new SMCSelectElement(driver.FindElement(By.Id("select_EntidadesResponsaveis_0__SeqEntidade"))).SelectByText("Departamento de Administração");
                driver.FindElement(By.Id("WizardBotaoSalvar1")).Click();
                              
                Thread.Sleep(1600);
                //Checando a mensagem apresentada (compara a mensagem exibida com a informada)
                Assert.True(CheckMessage("Componente curricular incluído com sucesso."), "Mensagem esperada não exibida");

                /*//Declara a variável teste q ira receber o texto do campo
                string
                teste = driver.FindElement(By.XPath("//div[@id='centro']/div/div/div[2]")).GetValue();
                Thread.Sleep(12000);
                //Compara a mensagem exibida com a esperada:
                Assert.Equal("Componente curricular incluído com sucesso.", teste);*/

                //Adicionando a configuração do componente
                driver.FindElement(By.XPath("//div[@id='NavigationGroup']/button")).Click();
                Thread.Sleep(1600);
                driver.FindElement(By.LinkText("Configuração de componente curricular")).Click();
                Thread.Sleep(1600);
                driver.FindElement(By.Id("BotaoNovoVP")).Click();
                Thread.Sleep(1600);
                driver.FindElement(By.XPath("//button[@id='DivisoesComponenteDetailPartialInserir']/i")).Click();
                Thread.Sleep(1600);

                driver.FindElement(By.Id("select_SeqTipoDivisaoComponente")).Click();
                new SMCSelectElement(driver.FindElement(By.Id("select_SeqTipoDivisaoComponente"))).SelectByText("Aula Teórica Presencial");
                driver.FindElement(By.Name("CargaHoraria")).Click();
                driver.FindElement(By.Name("CargaHoraria")).Clear();
                driver.FindElement(By.Name("CargaHoraria")).SendKeys("30");
                driver.FindElement(By.Name("CargaHorariaGrade")).Click();
                driver.FindElement(By.Name("CargaHorariaGrade")).Clear();
                driver.FindElement(By.Name("CargaHorariaGrade")).SendKeys("30");
                driver.FindElement(By.Id("select_SeqComponenteCurricularOrganizacao")).Click();
                new SMCSelectElement(driver.FindElement(By.Id("select_SeqComponenteCurricularOrganizacao"))).SelectByText("organização automação");
                driver.FindElement(By.Id("DivisoesComponente_DetailFormBotaoIncluir")).Click();
                driver.FindElement(By.Id("BotaoSalvarTemplate")).Click();
                
                Thread.Sleep(1600);
               /* //Checando a mensagem apresentada (compara a mensagem exibida com a informada)
                Assert.True(CheckMessage("Configuração Componente incluído com sucesso."), "Mensagem esperada não exibida");*/

                //Declara a variável teste q ira receber o texto do campo
                string
                teste = driver.FindElement(By.XPath("//div[@id='centro']/div/div/div[2]")).GetValue();

                //Compara a mensagem exibida com a esperada:
                Assert.Equal("Configuração Componente incluído com sucesso.", teste);



                driver.Driver.Close();

            });

        }

        [Fact] //Deve ser declarada para teste unitario
        public void ComponenteCurricular_Exclusao_ComponentecomOrganizacao() //Colocar o nome da tela a ser testada
        {
            base.ExecuteTest((driver) => //chama o browser e coloca o link correto

            {
                //Maximiza o Browser

                // driverNavigator.Manage().Window.Maximize();

                Login(driver); // realiza o login como administrador

                //Colar o script aqui

                driver.FindElement(By.Name("Descricao")).Click();
                driver.FindElement(By.Name("Descricao")).Clear();
                driver.FindElement(By.Name("Descricao")).SendKeys("Seminário de tese - automação");
                driver.FindElement(By.Id("BotaoPesquisarVP")).Click();
                driver.FindElement(By.XPath("//a[@id='DetailListBotaoExcluir0']/i")).Click();
                driver.FindElement(By.Id("BotaoPadraoPerguntaSim")).Click();
                string
                teste = driver.FindElement(By.XPath("//div[@id='centro']/div/div/div[2]")).GetValue();

                //Compara a mensagem exibida com a esperada:
                Thread.Sleep(1600);
                Assert.Equal("Componente curricular excluído com sucesso.", teste);
                driver.Driver.Close();


            });
        }


        [Fact]
        [Trait("Ordenado", "CRUD")]
        public void TesteOrdenadoCRUD()
        {
            ComponenteCurricular_Inclusao();
            ComponenteCurricular_InclusaoDuplicada();
            ComponenteCurricular_Alteracao();
            ComponenteCurricular_Exclusao();
            ComponenteCurricular_Exclusaonaopermitida();

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
            driver.GoToUrl2(Consts.SERVIDOR_QUALIDADE_ADM + "CUR/ComponenteCurricular");
            

        }

    }
}

