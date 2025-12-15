using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using ExpectedConditions = SeleniumExtras.WaitHelpers.ExpectedConditions;
using SMC.Framework.Test;
using System;
using System.Threading;
using Xunit; //Usado no Fact
using System.Linq;

namespace SMC.Academico.Tests.ADM.ORT
{

    public class Trabalho : SMCSeleniumUnitTest
    {

        //[Fact]
        public void TrabalhoAcademico_1_Inserir() //Não insere trabalho Dissertação com data de depósito e nível Mestrado Acadêmico porque não cumpriu os requisitos
        {
            base.ExecuteTest((driver) =>
            {
                //Maximiza o Browser
                // driverNavigator.Manage().Window.Maximize();

                Login(driver);
                //-------------------------------------------------------------------------------------------------
                //No passo 2 esse script seleciona o primeiro aluno da lista. Isso gera um erro na inclusão do trabalho se o aluno ja tiver cadastrado um trabalho desse tipo.

                //Passo 1
                driver.FindElement(By.Id("BotaoNovoVP")).Click();
                driver.FindElement(By.Name("Titulo")).Click();
                driver.FindElement(By.Name("Titulo")).Clear();
                driver.FindElement(By.Name("Titulo")).SendKeys("Título do Trabalho Acadêmico cadastrado pela automação");
                driver.FindElement(By.Id("select_SeqNivelEnsino")).Click();
                new SMCSelectElement(driver.FindElement(By.Id("select_SeqNivelEnsino"))).SelectByText("Mestrado Acadêmico");
                driver.FindElement(By.Id("select_SeqNivelEnsino")).Click();
                driver.FindElement(By.Id("select_SeqTipoTrabalho")).Click();
                new SMCSelectElement(driver.FindElement(By.Id("select_SeqTipoTrabalho"))).SelectByText("Dissertação");
                driver.FindElement(By.Id("select_SeqTipoTrabalho")).Click();
                //driver.FindElement(By.Id("DataDepositoSecretaria")).Click();
                driver.FindElement(By.Name("DataDepositoSecretaria")).Click();
                driver.FindElement(By.Name("DataDepositoSecretaria")).SendKeys(DateTime.Now.ToString("dd/MM/yyyy"));
                driver.FindElement(By.Name("DataDepositoSecretaria")).Click();

                driver.FindElement(By.Id("editorWizard-btnProximo")).Click();
                

                //Passo 2 
                //Usado no teste para selecionar o primeiro aluno que aparecer, mas nem sempre o aluno pode incluir trabalho. Se não puder use o trecho abaixo para aluno específico
                /*driver.FindElement(By.Id("Autores_0__SeqAluno_botao_modal")).Click();
                driver.FindElement(By.Id("DataSelector_gridDataSelector0")).Click();
                driver.FindElement(By.Id("smc-dataselector-SeqAluno")).Click();
                driver.FindElement(By.Id("editorWizard-btnProximo")).Click();*/

                //Usado no teste para selecionar um aluno específico
                driver.FindElement(By.XPath("//button[@id='Autores_0__SeqAluno_botao_modal']/i")).Click();
                driver.FindElement(By.Name("Nome")).Click();
                driver.FindElement(By.Name("Nome")).Clear();
                driver.FindElement(By.Name("Nome")).SendKeys("ADRIANA FERNANDES BALBI");
                driver.FindElement(By.Id("DataSelectorPesquisar0")).Click();
                driver.FindElement(By.Id("DataSelector_gridDataSelector0")).Click();
                driver.FindElement(By.Id("smc-dataselector-SeqAluno")).Click();
                driver.FindElement(By.Id("editorWizard-btnProximo")).Click();

                //Passo 3
                driver.FindElement(By.Id("select_DivisoesComponente_0__SeqDivisaoComponente")).Click();
                new SMCSelectElement(driver.FindElement(By.Id("select_DivisoesComponente_0__SeqDivisaoComponente"))).SelectByText("DEFESA DE DISSERTAÇÃO");
                driver.FindElement(By.Id("select_DivisoesComponente_0__SeqDivisaoComponente")).Click();
                driver.FindElement(By.Id("editorWizard-btnProximo")).Click();
                
                //Passo 4
                driver.FindElement(By.Id("divConteudoPrincipalEstrutura")).Click();
                driver.FindElement(By.Id("WizardBotaoSalvar1")).Click();
               
                //Checando mensagem de sucesso
                Assert.True(CheckMessage("não cumpriu os requisitos dos componentes curriculares abaixo:"), "Era esperado sucesso e ocorreu um erro");
                //driver.FindElement(By.Id("BotaoWinNo")).Click();

                driver.FindElement(By.CssSelector("[title='Não'][type='button']")).Click();

                driver.FindElement(By.Id("WizardBotaoCancelar1")).Click();

                //Para fechar o Chrome em segundo plano
                driver.Driver.Close();
            });
        }

        [Fact]
        public void TrabalhoAcademico_2_Inserir_semdatadeposito() //Insere trabalho Qualificação sem data de depósito e nível Mestrado Acadêmico
        {
            base.ExecuteTest((driver) =>
            {
                //Maximiza o Browser
                // driverNavigator.Manage().Window.Maximize();

                Login(driver);
                //-------------------------------------------------------------------------------------------------
                //No passo 2 esse script seleciona o primeiro aluno da lista. Isso NÃO gera erro na inclusão do trabalho se o aluno ja tiver cadastrado um trabalho desse tipo.

                //Passo 1
                driver.FindElement(By.Id("BotaoNovoVP")).Click();
                driver.FindElement(By.Name("Titulo")).Click();
                driver.FindElement(By.Name("Titulo")).Clear();
                driver.FindElement(By.Name("Titulo")).SendKeys("Trabalho Acadêmico cadastrado pela automação sem data de depósito");
                driver.FindElement(By.Id("select_SeqNivelEnsino")).Click();
                new SMCSelectElement(driver.FindElement(By.Id("select_SeqNivelEnsino"))).SelectByText("Mestrado Acadêmico");
                driver.FindElement(By.Id("select_SeqNivelEnsino")).Click();
                driver.FindElement(By.Id("select_SeqTipoTrabalho")).Click();
                new SMCSelectElement(driver.FindElement(By.Id("select_SeqTipoTrabalho"))).SelectByText("Projeto de Qualificação");
                driver.FindElement(By.Id("select_SeqTipoTrabalho")).Click();

                /*driver.FindElement(By.Name("DataDepositoSecretaria")).Click();
                driver.FindElement(By.Name("DataDepositoSecretaria")).SendKeys(DateTime.Now.ToString("dd/MM/yyyy"));
                driver.FindElement(By.Name("DataDepositoSecretaria")).Click();*/
                driver.FindElement(By.Id("editorWizard-btnProximo")).Click();

                //Passo 2 
                //Usado no teste para selecionar o primeiro aluno que aparecer, mas nem sempre o aluno pode incluir trabalho.Se não puder use o trecho abaixo para aluno específico
                /*driver.FindElement(By.Id("Autores_0__SeqAluno_botao_modal")).Click();
                driver.FindElement(By.Id("DataSelector_gridDataSelector0")).Click();
                driver.FindElement(By.Id("smc-dataselector-SeqAluno")).Click();
                driver.FindElement(By.Id("editorWizard-btnProximo")).Click();*/

                //Usado no teste para selecionar um aluno específico
                driver.FindElement(By.XPath("//button[@id='Autores_0__SeqAluno_botao_modal']/i")).Click();
                driver.FindElement(By.Name("Nome")).Click();
                driver.FindElement(By.Name("Nome")).Clear();
                //driver.FindElement(By.Name("Nome")).SendKeys("ADRIANA FERNANDES BALBI");
                driver.FindElement(By.Name("Nome")).SendKeys("Cassio da Silva Moraes Afonso");
                driver.FindElement(By.Id("DataSelectorPesquisar0")).Click();
                driver.FindElement(By.Id("DataSelector_gridDataSelector0")).Click();
                driver.FindElement(By.Id("smc-dataselector-SeqAluno")).Click();
                driver.FindElement(By.Id("editorWizard-btnProximo")).Click();

                //Passo 3
                driver.FindElement(By.Id("select_DivisoesComponente_0__SeqDivisaoComponente")).Click();
                new SMCSelectElement(driver.FindElement(By.Id("select_DivisoesComponente_0__SeqDivisaoComponente"))).SelectByText("QUALIFICAÇÃO DE DISSERTAÇÃO");
                //new SMCSelectElement(driver.FindElement(By.Id("select_DivisoesComponente_0__SeqDivisaoComponente"))).SelectByText("SEMINÁRIO DE PROJETO DE PESQUISA II");
                driver.FindElement(By.Id("select_DivisoesComponente_0__SeqDivisaoComponente")).Click();
                driver.FindElement(By.Id("editorWizard-btnProximo")).Click();

                //Passo 4
                driver.FindElement(By.Id("divConteudoPrincipalEstrutura")).Click();
                driver.FindElement(By.Id("WizardBotaoSalvar1")).Click();

                //Checando mensagem de sucesso
                Assert.True(CheckSuccess(), "Era esperado sucesso e ocorreu um erro");

                //Para fechar o Chrome em segundo plano
                driver.Driver.Close();
            });
        }

        [Fact]
        public void TrabalhoAcademico_2_InserirVerificaCampoObrigatorio()
        {
            base.ExecuteTest((driver) =>
            {
                //Maximiza o Browser
                // driverNavigator.Manage().Window.Maximize();

                Login(driver);
                //-------------------------------------------------------------------------------------------------
                //No passo 2 esse script seleciona o primeiro aluno da lista

                //Passo 1
                driver.FindElement(By.Id("BotaoNovoVP")).Click();
                driver.FindElement(By.Id("editorWizard-btnProximo")).Click();
                driver.FindElement(By.Name("Titulo")).CheckErrorMessage("Preenchimento obrigatório");
                driver.FindElement(By.Id("select_SeqNivelEnsino")).CheckErrorMessage("Seleção obrigatória");
                driver.FindElement(By.Id("select_SeqTipoTrabalho")).CheckErrorMessage("Seleção obrigatória");


                driver.FindElement(By.Name("Titulo")).Click();
                driver.FindElement(By.Name("Titulo")).Clear();
                driver.FindElement(By.Name("Titulo")).SendKeys("Trabalho Acadêmico cadastrado pela automação");
                driver.FindElement(By.Id("select_SeqNivelEnsino")).Click();
                new SMCSelectElement(driver.FindElement(By.Id("select_SeqNivelEnsino"))).SelectByText("Mestrado Acadêmico");
                //driver.FindElement(By.Id("select_SeqNivelEnsino")).Click();
                //driver.FindElement(By.Id("editorWizard-btnProximo")).Click();


                Thread.Sleep(3500);
                //driver.FindElement(By.Id("select_SeqTipoTrabalho")).CheckErrorMessage("Seleção obrigatória");
                //driver.FindElement(By.Id("select_SeqTipoTrabalho")).Click();
                new SMCSelectElement(driver.FindElement(By.Id("select_SeqTipoTrabalho"))).SelectByText("Projeto de Qualificação");
                driver.FindElement(By.Id("select_SeqTipoTrabalho")).Click();

                driver.FindElement(By.Id("editorWizard-btnProximo")).Click();

               /* Como não pode inserir manual Dissertação e Tese, essa parte não será mais executada
                driver.FindElement(By.Name("DataDepositoSecretaria")).CheckErrorMessage("Preenchimento obrigatório");
                driver.FindElement(By.Name("DataDepositoSecretaria")).Click();
                driver.FindElement(By.Name("DataDepositoSecretaria")).SendKeys(DateTime.Now.ToString("dd/MM/yyyy"));
                driver.FindElement(By.Name("DataDepositoSecretaria")).Click();
                driver.FindElement(By.Id("editorWizard-btnProximo")).Click();*/


                //Passo 2 
                /*WebDriverWait wait = new WebDriverWait((OpenQA.Selenium.IWebDriver)driver, TimeSpan.FromSeconds(2));
                wait.Until(ExpectedConditions.TextToBePresentInElementLocated(By.Id("editorWizard-btnAnterior"), "Anterior"));

                driver.FindElement(By.Id("editorWizard-btnAnterior")).Click();

                //Voltou para o Passo 1
                WebDriverWait wait1 = new WebDriverWait((OpenQA.Selenium.IWebDriver)driver, TimeSpan.FromSeconds(2));
                wait1.Until(ExpectedConditions.TextToBePresentInElementLocated(By.XPath("//div[@id='divViewWizard']/div[8]/div/p"), "A alteração do nível de ensino fará com que aluno(s) e componente(s) já informados sejam removidos."));
               
                new SMCSelectElement(driver.FindElement(By.Id("select_SeqTipoTrabalho"))).SelectByText("Projeto de Qualificação");
                driver.FindElement(By.Id("select_SeqTipoTrabalho")).Click();
                driver.FindElement(By.Id("editorWizard-btnProximo")).Click();*/

                //Passo 2
                /*driver.FindElement(By.Id("editorWizard-btnProximo")).Click();
                
                driver.FindElement(By.XPath("//span[@id='parsley-id-195']/span")).Click();
                 Thread.Sleep(3200);

                driver.FindElement(By.Name("Autores_0__SeqAluno_display_text")).CheckErrorMessage("Preenchimento obrigatório");*/

                //Selecionar o aluno, exclui e tenta ir para o próximo passo
                /*   driver.FindElement(By.Id("Autores_0__SeqAluno_botao_modal")).Click();
                   driver.FindElement(By.Id("DataSelector_gridDataSelector0")).Click();
                   driver.FindElement(By.Id("smc-dataselector-SeqAluno")).Click();
                   driver.FindElement(By.Id("LookupClear0")).Click();
                   driver.FindElement(By.Id("editorWizard-btnProximo")).Click();
                   driver.FindElement(By.Name("Autores_0__SeqAluno_display_text")).CheckErrorMessage("Preenchimento obrigatório"); */

                /* //Selecionar o aluno e vai para o próximo passo
                 driver.FindElement(By.Id("Autores_0__SeqAluno_botao_modal")).Click();
                 driver.FindElement(By.Id("DataSelector_gridDataSelector0")).Click();
                 driver.FindElement(By.Id("smc-dataselector-SeqAluno")).Click();
                 driver.FindElement(By.Id("editorWizard-btnProximo")).Click();*/

                //Clica no próximo e valida mensagem de obrigatoriedade
                driver.FindElement(By.Id("editorWizard-btnProximo")).Click();
                driver.FindElement(By.Id("editorWizard-btnProximo")).Click();
                driver.FindElement(By.Id("editorWizard-btnProximo")).Click();
                driver.FindElement(By.XPath("//a[@id='editorWizard-btnProximo']/i")).Click();
                 Thread.Sleep(1500);
                driver.FindElement(By.Id("smc-lookup-body-Autores_0__SeqAluno")).CheckErrorMessage("Preenchimento obrigatório");
                //driver.FindElement(By.Name("Autores_0__SeqAluno_display_text")).CheckErrorMessage("Preenchimento obrigatório");
                
                //Usado no teste para selecionar um aluno específico
                driver.FindElement(By.XPath("//button[@id='Autores_0__SeqAluno_botao_modal']/i")).Click();
                driver.FindElement(By.Name("Nome")).Click();
                driver.FindElement(By.Name("Nome")).Clear();


                driver.FindElement(By.Name("Nome")).SendKeys("Yukari Miyata"); //Aldira Abreu Gomes, Alex José de Almeida");
                driver.FindElement(By.Id("DataSelectorPesquisar0")).Click();
                driver.FindElement(By.Id("DataSelector_gridDataSelector0")).Click();
                driver.FindElement(By.Id("smc-dataselector-SeqAluno")).Click();
                driver.FindElement(By.Id("editorWizard-btnProximo")).Click();

                //Passo 3
                driver.FindElement(By.Id("select_DivisoesComponente_0__SeqDivisaoComponente")).Click();
                new SMCSelectElement(driver.FindElement(By.Id("select_DivisoesComponente_0__SeqDivisaoComponente"))).SelectByText("Selecionar...");
                driver.FindElement(By.Id("editorWizard-btnProximo")).Click();
                
                Thread.Sleep(1200);
                driver.FindElement(By.Id("select_DivisoesComponente_0__SeqDivisaoComponente")).CheckErrorMessage("Seleção obrigatória");
                Thread.Sleep(1200);

                driver.FindElement(By.Id("select_DivisoesComponente_0__SeqDivisaoComponente")).Click();
                new SMCSelectElement(driver.FindElement(By.Id("select_DivisoesComponente_0__SeqDivisaoComponente"))).SelectByText("EXAME DE QUALIFICAÇÃO DE MESTRADO");
                driver.FindElement(By.Id("editorWizard-btnProximo")).Click();
                                
                //Passo 4
                driver.FindElement(By.Id("divConteudoPrincipalEstrutura")).Click();
                driver.FindElement(By.Id("WizardBotaoCancelar1")).Click();

                //Para fechar o Chrome em segundo plano
                driver.Driver.Close();


            });
        }

        [Fact]
        public void TrabalhoAcademico_2_InserirTrabalhoUnico () //Tenta inserir trabalho que só permite uma inclusão e exibe mensagem não permitindo
        {
            base.ExecuteTest((driver) =>
            {
            //Maximiza o Browser
            // driverNavigator.Manage().Window.Maximize();

            Login(driver);
            //-------------------------------------------------------------------------------------------------
                //Passo 1
                driver.FindElement(By.Id("BotaoNovoVP")).Click();
                driver.FindElement(By.Name("Titulo")).Click();
                driver.FindElement(By.Name("Titulo")).Clear();
                driver.FindElement(By.Name("Titulo")).SendKeys("Teste de automação");
                new SMCSelectElement(driver.FindElement(By.Id("select_SeqNivelEnsino"))).SelectByText("Mestrado Acadêmico");
                new SMCSelectElement(driver.FindElement(By.Id("select_SeqTipoTrabalho"))).SelectByText("Dissertação");
                Thread.Sleep(600);
                driver.FindElement(By.Name("DataDepositoSecretaria")).Click();
                driver.FindElement(By.Name("DataDepositoSecretaria")).Clear();
                driver.FindElement(By.Name("DataDepositoSecretaria")).SendKeys(DateTime.Now.ToString("dd/MM/yyyy"));
                driver.FindElement(By.Name("DataDepositoSecretaria")).SendKeys(Keys.Enter);
                driver.FindElement(By.Id("editorWizard-btnProximo")).Click();

                //Passo 2
                driver.FindElement(By.Id("Autores_0__SeqAluno_botao_modal")).Click();
                driver.FindElement(By.Name("Nome")).SendKeys("Aldria Natalia Rodrigues");

                //driver.FindElement(By.Name("Nome")).SendKeys("Guilherme Guimaraes Laborao");
                //driver.FindElement(By.Name("Nome")).SendKeys("Fabricia Gracielle Santos");
                //driver.FindElement(By.Name("NumeroRegistroAcademico")).SendKeys("2760"); //Esse aluno 2760 é a Fabricia Gracielle Santos
                //driver.FindElement(By.Name("NumeroRegistroAcademico")).SendKeys("27581"); //Esse aluno agora, só tem um trabalho...isso mudou na base de novo
                //driver.FindElement(By.Name("NumeroRegistroAcademico")).SendKeys("27580");//esse aluno não existe mais...
                
                driver.FindElement(By.Id("DataSelectorPesquisar0")).Click();
                driver.FindElement(By.Name("DataSelector_gridDataSelector")).Click();
                driver.FindElement(By.Id("smc-dataselector-SeqAluno")).Click();

                /*driver.FindElement(By.Name("Autores_0__SeqAluno_display_text")).Click();
                driver.FindElement(By.Name("Autores_0__SeqAluno_display_text")).Clear();
                driver.FindElement(By.Name("Autores_0__SeqAluno_display_text")).SendKeys("27581");*/
                Thread.Sleep(120);
               // driver.FindElement(By.Name("Autores_0__SeqAluno_display_text")).SendKeys(Keys.Enter);
                driver.FindElement(By.Id("editorWizard-btnProximo")).Click();

                //Passo 3
                new SMCSelectElement(driver.FindElement(By.Id("select_DivisoesComponente_0__SeqDivisaoComponente"))).SelectByText("DEFESA DE DISSERTAÇÃO");
                driver.FindElement(By.Id("editorWizard-btnProximo")).Click();

                //Passo 4
                driver.FindElement(By.Id("divConteudoPrincipalEstrutura")).Click();
                driver.FindElement(By.Id("WizardBotaoSalvar1")).Click();

                //Quando o aluno não cursou todas as disciplinas necessárias
                //driver.FindElement(By.Id("BotaoWinYes")).Click();

                //Checando mensagem de sucesso

              
                //Assert.True(CheckMessage("Inclusão/Alteração não permitida. Já existe um trabalho acadêmico deste tipo cadastrado para o aluno informado."), "Mensagem esperada não exibida");
                Assert.True(CheckMessage("Inclusão/Alteração não permitida."), "Mensagem esperada não exibida");

                //Para fechar o Chrome em segundo plano
                driver.Driver.Close();
            });
        }

        [Fact]
        public void TrabalhoAcademico_3_Alterar_semdatadeposito() //Altera o registro que foi inserido sem data de depósito
        {
            base.ExecuteTest((driver) =>
            {
                //Maximiza o Browser
                // driverNavigator.Manage().Window.Maximize();

                Login(driver);
                //-------------------------------------------------------------------------------------------------
                //Altera o título e o componente do trabalho
                driver.FindElement(By.Name("Titulo")).Click();
                driver.FindElement(By.Name("Titulo")).Clear();
                driver.FindElement(By.Name("Titulo")).SendKeys("Trabalho Acadêmico cadastrado pela automação sem data de depósito");
                driver.FindElement(By.Id("BotaoPesquisarVP")).Click();
                
                //driver.FindElement(By.XPath("//a[@id='DetailListBotaoAlterar0']/i")).Click();
                driver.FindElement(By.CssSelector("[title='Alterar'][id='DetailedListBotaoItem0']")).Click();

                driver.FindElement(By.Name("Titulo")).Click();
                driver.FindElement(By.Name("Titulo")).Clear();
                driver.FindElement(By.Name("Titulo")).SendKeys("Trabalho Acadêmico cadastrado pela automação ALTERADO");
                driver.FindElement(By.Id("select_DivisoesComponente_0__SeqDivisaoComponente")).Click();
                new SMCSelectElement(driver.FindElement(By.Id("select_DivisoesComponente_0__SeqDivisaoComponente"))).SelectByText("SEMINÁRIO DE PROJETO DE PESQUISA I");
                driver.FindElement(By.Id("select_DivisoesComponente_0__SeqDivisaoComponente")).Click();
                driver.FindElement(By.Id("BotaoSalvarTemplate")).Click();

                //Checando mensagem de sucesso
                Assert.True(CheckSuccess(), "Era esperado sucesso e ocorreu um erro");

                //Para fechar o Chrome em segundo plano
                driver.Driver.Close();
            });
        }

        [Fact]
        public void TrabalhoAcademico_4_ExclusaoNaoPermitida()
        {
            base.ExecuteTest((driver) =>
            {
                //Maximiza o Browser
                // driverNavigator.Manage().Window.Maximize();

                Login(driver);
                //-------------------------------------------------------------------------------------------------
                //Pesquisa os trabalhos do ciclo 2/2020, alunos formados
                driver.FindElement(By.XPath("//button[@id='SeqCicloLetivo_botao_modal']/i")).Click();
                driver.FindElement(By.Name("AnoCiclo")).Clear();
                driver.FindElement(By.Name("AnoCiclo")).SendKeys("2020");
                driver.FindElement(By.Name("NumeroCiclo")).Click();
                driver.FindElement(By.Name("NumeroCiclo")).Clear();
                driver.FindElement(By.Name("NumeroCiclo")).SendKeys("2");
                driver.FindElement(By.Id("DataSelectorPesquisar0")).Click();
                driver.FindElement(By.Id("DataSelector_gridDataSelector0")).Click();
                driver.FindElement(By.Id("smc-dataselector-SeqCicloLetivo")).Click();
                driver.FindElement(By.Id("select_SeqTipoSituacao")).Click();
                new SMCSelectElement(driver.FindElement(By.Id("select_SeqTipoSituacao"))).SelectByText("Selecionar...");
                driver.FindElement(By.Id("select_SeqTipoSituacao")).Click();
                new SMCSelectElement(driver.FindElement(By.Id("select_SeqTipoSituacao"))).SelectByText("Formado");
                driver.FindElement(By.Id("select_SeqTipoTrabalho")).Click();
                new SMCSelectElement(driver.FindElement(By.Id("select_SeqTipoTrabalho"))).SelectByText("Projeto de Qualificação");
                driver.FindElement(By.Id("select_SeqTipoTrabalho")).Click();

                driver.FindElement(By.Id("BotaoPesquisarVP")).Click();

                //driver.FindElement(By.XPath("//a[@id='DetailedListBotaoItem0']/i")).Click();
                //driver.FindElement(By.Id("DetailedListBotaoItem0")).Click();

                //driver.FindElement(By.XPath("//a[@id='DetailedListBotaoItem0']/i")).Click();
                //driver.FindElement(By.Id("BotaoPadraoPerguntaSim")).Click();

                driver.FindElement(By.XPath("//a[@id='DetailedListBotaoItem1']/i")).Click();
                driver.FindElement(By.Id("BotaoPadraoPerguntaSim")).Click();

                //Checando mensagem 
                Assert.True(CheckMessage("Exclusão não permitida. Este trabalho já possui avaliação cadastrada para seu componente curricular."), "Era esperado sucesso e ocorreu um erro");
                //Assert.True(CheckMessage("Exclusão não permitida. TipoTrabalho já foi associado"), "Era esperado sucesso e ocorreu um erro");

                //Para fechar o Chrome em segundo plano
                driver.Driver.Close();
            });
        }

        [Fact]
        public void TrabalhoAcademico_5_Excluir()
        {
            base.ExecuteTest((driver) =>
            {
                //Maximiza o Browser
                // driverNavigator.Manage().Window.Maximize();

                Login(driver);
                //-------------------------------------------------------------------------------------------------
               
                driver.FindElement(By.Name("Titulo")).Click();
                driver.FindElement(By.Name("Titulo")).Clear();
                driver.FindElement(By.Name("Titulo")).SendKeys("Trabalho Acadêmico cadastrado pela automação");
                driver.FindElement(By.Id("BotaoPesquisarVP")).Click();
                driver.FindElement(By.Id("DetailedListBotaoItem1")).Click();
                driver.FindElement(By.Id("BotaoPadraoPerguntaSim")).Click();
                               
                //Checando mensagem 
                Assert.True(CheckSuccess(), "Era esperado sucesso e ocorreu um erro");

                //Para fechar o Chrome em segundo plano
                driver.Driver.Close();
            });
        }
        [Fact]
        public void TrabalhoAcademico_6_EmissaoComprovanteEntrega()
        {
            base.ExecuteTest((driver) =>
            {
            //Maximiza o Browser
            // driverNavigator.Manage().Window.Maximize();

            Login(driver);
            //-------------------------------------------------------------------------------------------------

            //Pesquisa os trabalhos do aluno Abner Mário Oliveira Teixeira Cicarini
            driver.FindElement(By.XPath("//button[@id='SeqAluno_botao_modal']/i")).Click();
            driver.FindElement(By.Name("Nome")).Click();
            driver.FindElement(By.Name("Nome")).Clear();
            //driver.FindElement(By.Name("Nome")).SendKeys("Adriana Sales Diniz");
            driver.FindElement(By.Name("Nome")).SendKeys("Abner Márcio Oliveira Teixeira Cicarini");
            driver.FindElement(By.Id("DataSelectorPesquisar0")).Click();
            driver.FindElement(By.Id("DataSelector_gridDataSelector0")).Click();
            driver.FindElement(By.Id("smc-dataselector-SeqAluno")).Click();
            driver.FindElement(By.Id("select_SeqTipoTrabalho")).Click();
            new SMCSelectElement(driver.FindElement(By.Id("select_SeqTipoTrabalho"))).SelectByText("Dissertação");
            driver.FindElement(By.Id("select_SeqTipoTrabalho")).Click();
            driver.FindElement(By.Id("BotaoPesquisarVP")).Click();

            driver.FindElement(By.XPath("//div[@id='ButtonSetDropdown1']/button")).Click();
            driver.FindElement(By.Id("DetailedListBotaoItem3")).Click();
                

            //Para colocar o foco do Selenium na última aba aberta
            driver.Driver.SwitchTo().Window(driver.Driver.WindowHandles.Last());
            String url = driver.Driver.Url;
            Assert.Equal("https://web-qualidade.pucminas.br/SGA.Administrativo/ORT/RelatorioEmitirComprovanteEntrega?seqTrabalho=200539094A0C72D4", url);
            //Assert.Equal("https://web-desenvolvimento.pucminas.br/SGA.Administrativo/ORT/RelatorioEmitirComprovanteEntrega?seqTrabalho=200539094A0C72D4", url);

                //Para fechar o Chrome em segundo plano
                driver.Driver.Close();

            });
        }

        [Fact]
        public void TrabalhoAcademico_7_EmissaoAtaDefesa()
        {
            base.ExecuteTest((driver) =>
            {
                //Maximiza o Browser
                // driverNavigator.Manage().Window.Maximize();

                Login(driver);
                //-------------------------------------------------------------------------------------------------

                //Pesquisa os trabalhos do ciclo 1/2021, da aluna formada Adriana Sales Diniz
                driver.FindElement(By.XPath("//button[@id='SeqAluno_botao_modal']/i")).Click();
                driver.FindElement(By.Name("NumeroRegistroAcademico")).Click();
                driver.FindElement(By.Name("NumeroRegistroAcademico")).Clear();
                driver.FindElement(By.Name("NumeroRegistroAcademico")).SendKeys("1175");

                driver.FindElement(By.Name("Nome")).Click();
                driver.FindElement(By.Name("Nome")).Clear();
                driver.FindElement(By.Name("Nome")).SendKeys("Adriana Sales Diniz");
                
                driver.FindElement(By.Id("DataSelectorPesquisar0")).Click();
                driver.FindElement(By.Id("DataSelector_gridDataSelector0")).Click();
                driver.FindElement(By.Id("smc-dataselector-SeqAluno")).Click();
                driver.FindElement(By.Id("select_SeqTipoTrabalho")).Click();
                new SMCSelectElement(driver.FindElement(By.Id("select_SeqTipoTrabalho"))).SelectByText("Dissertação");
                driver.FindElement(By.Id("select_SeqTipoTrabalho")).Click();
                driver.FindElement(By.Id("BotaoPesquisarVP")).Click();

                driver.FindElement(By.XPath("//div[@id='ButtonSetDropdown1']/button")).Click();
                //driver.FindElement(By.Id("DetailedListBotaoItem2")).Click();
                driver.FindElement(By.Id("DetailedListBotaoItem1")).Click();
                driver.FindElement(By.CssSelector("[title='Avaliações do trabalho'][data-type='smc-button-element']")).Click();

                driver.FindElement(By.Id("ButtonSetDropdown1")).Click();
                Thread.Sleep(5000);
                driver.FindElement(By.Id("DetailedListBotaoItem4")).Click();

                driver.FindElement(By.CssSelector("[title='Emitir ata de defesa'][data-type='smc-button-element']")).Click();

                Thread.Sleep(5000);

                //Para colocar o foco do Selenium na última aba aberta
                driver.Driver.SwitchTo().Window(driver.Driver.WindowHandles.Last());
                String url = driver.Driver.Url;

                // Log para depuração
                Console.WriteLine("URL gerada: " + url);

                //Assert.Equal("https://web-qualidade.pucminas.br/SGA.Administrativo/Home/DownloadFileGuid?guidFile=9d69247a-449e-4e77-8d99-7983636a5e0d", url);
                              
                // Verifica se a URL tem o formato esperado
                Assert.StartsWith("https://web-qualidade.pucminas.br/SGA.Administrativo/Home/DownloadFileGuid?guidFile=", url);


                Thread.Sleep(5000);

                //Para fechar o Chrome em segundo plano
                driver.Driver.Close();
            });
        }

        [Fact]
        public void TrabalhoAcademico_8_AlteracaoAtaDefesa()
        {
            base.ExecuteTest((driver) =>
            {
                //Maximiza o Browser
                // driverNavigator.Manage().Window.Maximize();

                Login(driver);
                //-------------------------------------------------------------------------------------------------

                //Pesquisa os trabalhos do ciclo 1/2021, da aluna formada Adriana Sales Diniz
                driver.FindElement(By.XPath("//button[@id='SeqAluno_botao_modal']/i")).Click();
                driver.FindElement(By.Name("Nome")).Click();
                driver.FindElement(By.Name("Nome")).Clear();
                driver.FindElement(By.Name("Nome")).SendKeys("Adriana Sales Diniz");
                driver.FindElement(By.Id("DataSelectorPesquisar0")).Click();
                driver.FindElement(By.Id("DataSelector_gridDataSelector0")).Click();
                driver.FindElement(By.Id("smc-dataselector-SeqAluno")).Click();
                driver.FindElement(By.Id("select_SeqTipoTrabalho")).Click();
                new SMCSelectElement(driver.FindElement(By.Id("select_SeqTipoTrabalho"))).SelectByText("Dissertação");
                driver.FindElement(By.Id("select_SeqTipoTrabalho")).Click();
                driver.FindElement(By.Id("BotaoPesquisarVP")).Click();

                driver.FindElement(By.Id("ButtonSetDropdown1")).Click(); //engrenagem
                driver.FindElement(By.Id("DetailedListBotaoItem2")).Click(); //avaliações do trabalho
                driver.FindElement(By.Id("ButtonSetDropdown1")).Click(); //engrenagem
                driver.FindElement(By.Id("DetailedListBotaoItem6")).Click(); //alterar ata
                Thread.Sleep(6000);
/*
                //driver.FindElement(By.Name("files[]")).Click(150);//clica no botão selecionar arquivos
                driver.FindElement(By.ClassName("smc-uploadFile-controles")).Click(); //clica no botão selecionar arquivos
                                                
                //Comando para anexar arquivos que estão salvos no servidor. 
                driver.FindElement(By.XPath("//*[@id='Documentos_0__Documentos_0__ArquivoAnexado']/div[1]/input")).SendKeys(@"\\GTIWEBDES01\Files\pdf.pdf");
                
                driver.FindElement(By.CssSelector("[title='Salvar'][type='submit']")).Click(); */

                // Clica no botão selecionar arquivos
                driver.FindElement(By.ClassName("smc-uploadFile-controles")).Click();

                // Anexa o arquivo salvo no servidor
                var fileInput = driver.FindElement(By.CssSelector("#Documentos_0__Documentos_0__ArquivoAnexado input[type='file']"));
                fileInput.SendKeys(@"\\GTIWEBDES01\Files\pdf.pdf");

                // Clica no botão salvar
                driver.FindElement(By.CssSelector("[title='Salvar'][type='submit']")).Click();



                /*
                //driver.FindElement(By.Id("DetailedListBotaoItem2")).Click();
                driver.FindElement(By.Id("DetailedListBotaoItem1")).Click();
                driver.FindElement(By.Id("ButtonSetDropdown1")).Click();
                driver.FindElement(By.Id("DetailedListBotaoItem4")).Click();*/

                //Para colocar o foco do Selenium na última aba aberta
                /*driver.Driver.SwitchTo().Window(driver.Driver.WindowHandles.Last());
                String url = driver.Driver.Url;
                Assert.Equal("https://web-qualidade.pucminas.br/SGA.Administrativo/Home/DownloadFileGuid?guidFile=9d69247a-449e-4e77-8d99-7983636a5e0d", url);*/

                //Para fechar o Chrome em segundo plano
                driver.Driver.Close();
            });
        }




        [Fact]
        [Trait("Ordenado", "CRUD")]
        public void TesteOrdenadoCRUD()
        {
            // TrabalhoAcademico_1_Inserir(); //dissertação não é inserido manual mais, agora apenas pelo portal
            //TrabalhoAcademico_2_Inserir_semdatadeposito(); //trabalho não é mais incluído manualmente, apenas via solicitação
            //TrabalhoAcademico_2_InserirVerificaCampoObrigatorio(); //trabalho não é mais incluído manualmente, apenas via solicitação
            //TrabalhoAcademico_2_InserirTrabalhoUnico(); //trabalho não é mais incluído manualmente, apenas via solicitação
            // TrabalhoAcademico_3_Alterar_semdatadeposito();//trabalho não é mais incluído manualmente, apenas via solicitação
            //TrabalhoAcademico_4_ExclusaoNaoPermitida(); // confirmar se a exclusão vai poder ser feita
            //TrabalhoAcademico_5_Excluir(); //confirmar se a exclusão vai poder ser feita
            TrabalhoAcademico_6_EmissaoComprovanteEntrega();
            TrabalhoAcademico_7_EmissaoAtaDefesa();
            //TrabalhoAcademico_8_AlteracaoAtaDefesa();
        }

        private static void Login(ISMCWebDriver driver)
        {
            //----------------------------------------------------
            driver.GoToUrl(Consts.SERVIDOR_QUALIDADE_ADM);
            //driver.GoToUrl(Consts.SERVIDOR_DESENVOLVIMENTO_ADM);
            //força o sistema a esperar 15 minutos ou até que apareça o campo para login
            WebDriverWait wait = new WebDriverWait((OpenQA.Selenium.IWebDriver)driver, TimeSpan.FromSeconds(900));
            wait.Until(e => e.FindElement(By.Name("LoginCpf")));
            driver.SMCLoginCpf();
            //força o sistema a esperar 15 minutos ou até que apareça a home do SGA
            wait.Until(ExpectedConditions.TextToBePresentInElementLocated(By.XPath("//div[@id='divConteudoPrincipal']/section/h2"), "SGA.Administrativo"));
            driver.GoToUrl2(Consts.SERVIDOR_QUALIDADE_ADM + "ORT/TrabalhoAcademico"); //coloca o resto do endereço para acessar a pagina do teste
            //driver.GoToUrl2(Consts.SERVIDOR_DESENVOLVIMENTO_ADM + "ORT/TrabalhoAcademico"); //coloca o resto do endereço para acessar a pagina do teste

        }
    }
}

