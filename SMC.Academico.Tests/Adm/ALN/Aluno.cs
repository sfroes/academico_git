using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using ExpectedConditions = SeleniumExtras.WaitHelpers.ExpectedConditions;
using SMC.Framework.Test;
using Xunit;
using System.Threading;
using System;

namespace SMC.Academico.Tests.ADM.ALN
{
    public class Aluno : SMCSeleniumUnitTest
    {
        [Fact]
        public (string dadosVinculo, string email, string cpf, string passaporte, string nivelEnsino, string entidadeResponsavel, string dataPrevisaoConclusao) Pesquisar()
        {
            string email = string.Empty;
            string cpf = string.Empty;
            string passaporte = string.Empty;
            string nivelEnsino = string.Empty;
            string dadosVinculo = string.Empty;
            string entidadeResponsavel = string.Empty;
            string dataPrevisaoConclusao = string.Empty;

            base.ExecuteTest((driver) =>
            {
                //Maximiza o Browser
                // driverNavigator.Manage().Window.Maximize();

                Login(driver);
                //-------------------------------------------------------------------------------------------------

                driver.FindElement(By.Id("select_SeqTipoVinculoAluno")).Click();
                new SMCSelectElement(driver.FindElement(By.Id("select_SeqTipoVinculoAluno"))).SelectByText("Curso Regular");
                driver.FindElement(By.Id("select_SeqTipoVinculoAluno")).Click();
                //Thread.Sleep(6000);
                /*driver.FindElement(By.XPath("//div[@id='SeqsSituacaoMatriculaCicloLetivo']/div/a")).Click();
                driver.FindElement(By.XPath("//div[@id='SeqsSituacaoMatriculaCicloLetivo']/div/a")).Click();
                driver.FindElement(By.XPath("//div[@id='SeqsSituacaoMatriculaCicloLetivo']/div/a")).Click();
                driver.FindElement(By.Id("SeqsSituacaoMatriculaCicloLetivoSearch")).Click();
                driver.FindElement(By.Id("frmViewPadrao")).Submit();
                driver.FindElement(By.Id("SeqsSituacaoMatriculaCicloLetivoSearch")).Clear();
                //driver.FindElement(By.Id("SeqsSituacaoMatriculaCicloLetivoSearch")).SendKeys("matriculado");*/

                driver.FindElement(By.XPath("//button[@id='SeqCicloLetivoSituacaoMatricula_botao_modal']/i")).Click();
                driver.FindElement(By.Name("AnoCiclo")).Clear();
                driver.FindElement(By.Name("AnoCiclo")).SendKeys("2023");
                driver.FindElement(By.Name("NumeroCiclo")).Click();
                driver.FindElement(By.Name("NumeroCiclo")).Clear();
                driver.FindElement(By.Name("NumeroCiclo")).SendKeys("2");
                driver.FindElement(By.Id("DataSelectorPesquisar0")).Click();
                
                driver.FindElement(By.Id("DataSelector_gridDataSelector0")).Click();
                driver.FindElement(By.Id("smc-dataselector-SeqCicloLetivoSituacaoMatricula")).Click();
                driver.FindElement(By.XPath("//div[@id='SeqsSituacaoMatriculaCicloLetivo']/div/a")).Click();
                driver.FindElement(By.XPath("//div[@id='SeqsSituacaoMatriculaCicloLetivo']/div[2]/ul/li[9]/span")).Click();
                
                /*driver.FindElement(By.XPath("//div[@id='SeqsSituacaoMatriculaCicloLetivo']/div[2]/ul/li[9]")).Click();
                driver.FindElement(By.XPath("//div[@id='SeqsSituacaoMatriculaCicloLetivo']/div[2]/ul/li[9]/span")).Click();*/
                                
                driver.FindElement(By.Id("BotaoPesquisarVP")).Click();

                /* driver.FindElement(By.Id("SeqsSituacaoMatriculaCicloLetivo")).Click();
                 driver.FindElement(By.Id("SeqsSituacaoMatriculaCicloLetivoSearch")).Click();
                 driver.FindElement(By.Id("SeqsSituacaoMatriculaCicloLetivoSearch")).Clear();
                 driver.FindElement(By.Id("SeqsSituacaoMatriculaCicloLetivoSearch")).SendKeys("Matriculado");
                 driver.FindElement(By.CssSelector("[data-value='6']")).Click();
                 driver.FindElement(By.Id("BotaoPesquisarVP")).Click();*/
                 driver.FindElement(By.Id("DetailListBotaoAlterar0")).Click();

                cpf = driver.FindElement(By.Name("Cpf")).GetValue();
                passaporte = driver.FindElement(By.Name("NumeroPassaporte")).GetValue();
                nivelEnsino = driver.FindElement(By.XPath("(.//*[normalize-space(text()) and normalize-space(.)='Nível de ensino'])[1]/following::p[1]")).GetValue();
                dadosVinculo = driver.FindElement(By.XPath("(.//*[normalize-space(text()) and normalize-space(.)='Dados do vínculo'])[1]/following::p[1]")).GetValue();
                entidadeResponsavel = driver.FindElement(By.XPath("(.//*[normalize-space(text()) and normalize-space(.)='Entidade responsável'])[1]/following::p[1]")).GetValue();
                dataPrevisaoConclusao = driver.FindElement(By.XPath("(.//*[normalize-space(text()) and normalize-space(.)='Data de previsão de conclusão'])[1]/following::p[1]")).GetValue();

                driver.FindElement(By.XPath("(.//*[normalize-space(text()) and normalize-space(.)='Dados Pessoais'])[1]/following::span[1]")).Click();
                email = driver.FindElement(By.XPath("(.//*[normalize-space(text()) and normalize-space(.)='Endereços eletrônicos'])[1]/following::p[2]")).GetValue();
            });

            return (email, cpf, passaporte, nivelEnsino, dadosVinculo, entidadeResponsavel, dataPrevisaoConclusao);
        }

        [Fact]
        public (string dadosVinculo, string email, string cpf, string passaporte, string nivelEnsino, string entidadeResponsavel, string dataPrevisaoConclusao) Pesquisar2()
        {
            string email = string.Empty;
            string cpf = string.Empty;
            string passaporte = string.Empty;
            string nivelEnsino = string.Empty;
            string dadosVinculo = string.Empty;
            string entidadeResponsavel = string.Empty;
            string dataPrevisaoConclusao = string.Empty;
            string nome = string.Empty;

            base.ExecuteTest((driver) =>
            {
                //Maximiza o Browser
                // driverNavigator.Manage().Window.Maximize();

                Login(driver);
                //-------------------------------------------------------------------------------------------------
                //Pesquisa Aluno de Mestrado Acadêmico, Curso Regular e Matriculado, retorna Nome, CPF, Passaporte, Nível de ensino, dados do vínculo, entidade responsável e no
                driver.FindElement(By.Id("select_SeqNivelEnsino")).Click();
                new SMCSelectElement(driver.FindElement(By.Id("select_SeqNivelEnsino"))).SelectByText("Mestrado Acadêmico");
                driver.FindElement(By.Id("select_SeqNivelEnsino")).Click();

                driver.FindElement(By.Id("select_SeqTipoVinculoAluno")).Click();
                new SMCSelectElement(driver.FindElement(By.Id("select_SeqTipoVinculoAluno"))).SelectByText("Curso Regular");
                driver.FindElement(By.Id("select_SeqTipoVinculoAluno")).Click();

                driver.FindElement(By.Id("SeqsSituacaoMatriculaCicloLetivo")).Click();
                driver.FindElement(By.Id("SeqsSituacaoMatriculaCicloLetivoSearch")).Click();
                driver.FindElement(By.Id("SeqsSituacaoMatriculaCicloLetivoSearch")).Clear();
                driver.FindElement(By.Id("SeqsSituacaoMatriculaCicloLetivoSearch")).SendKeys("Matriculado");
                driver.FindElement(By.CssSelector("[data-value='6']")).Click();

                driver.FindElement(By.Id("BotaoPesquisarVP")).Click();
                driver.FindElement(By.Id("DetailListBotaoAlterar0")).Click();

                cpf = driver.FindElement(By.Name("Cpf")).GetValue();
                passaporte = driver.FindElement(By.Name("NumeroPassaporte")).GetValue();
                nivelEnsino = driver.FindElement(By.XPath("(.//*[normalize-space(text()) and normalize-space(.)='Nível de ensino'])[1]/following::p[1]")).GetValue();
                dadosVinculo = driver.FindElement(By.XPath("(.//*[normalize-space(text()) and normalize-space(.)='Dados do vínculo'])[1]/following::p[1]")).GetValue();
                entidadeResponsavel = driver.FindElement(By.XPath("(.//*[normalize-space(text()) and normalize-space(.)='Entidade responsável'])[1]/following::p[1]")).GetValue();
                dataPrevisaoConclusao = driver.FindElement(By.XPath("(.//*[normalize-space(text()) and normalize-space(.)='Data de previsão de conclusão'])[1]/following::p[1]")).GetValue();

                driver.FindElement(By.XPath("(.//*[normalize-space(text()) and normalize-space(.)='Dados Pessoais'])[1]/following::span[1]")).Click();
                email = driver.FindElement(By.XPath("(.//*[normalize-space(text()) and normalize-space(.)='Endereços eletrônicos'])[1]/following::p[2]")).GetValue();
                nome = driver.FindElement(By.XPath("(.//*[normalize-space(text()) and normalize-space(.)='Nome'])[1]/following::p[1]")).GetValue();
            });

            return (nome, email, cpf, passaporte, nivelEnsino, dadosVinculo, entidadeResponsavel);
        }




        [Fact]
        public void Aluno_2_AssociaBeneficio()
        {
            
            base.ExecuteTest((driver) =>
            {
                               
                Login(driver);

                //Pega o primeiro dia do mês e adiciona mais 3 anos, para nao ter risco de ter benefício já associado no período do teste
                var primeiroDiaMes = new DateTime(DateTime.Today.Year + 3, DateTime.Today.Month, 1);
                // Pega o último dia do mês, obrigatorio para o campo Fim do periodo
                var ultimoDiaMes = primeiroDiaMes.AddMonths(1).AddDays(-1);

                //Selecionando o tipo de vínculo Curso Regular
                driver.FindElement(By.Id("select_SeqTipoVinculoAluno")).Click();
                new SMCSelectElement(driver.FindElement(By.Id("select_SeqTipoVinculoAluno"))).SelectByText("Curso Regular");
                driver.FindElement(By.Id("select_SeqTipoVinculoAluno")).Click();

                //Selecionando o último ciclo cadastrado
                //driver.FindElement(By.XPath("//button[@id='SeqCicloLetivoSituacaoMatricula_botao_modal']/i")).Click();
                //driver.FindElement(By.Id("SeqCicloLetivoSituacaoMatricula_botao_modal")).Click(); Tirar esse comentário quando o último ciclo tiver aluno matriculado e retirar a seleção do ciclo 1/2023

                //Selecionando o ciclo 1/2023 
                driver.FindElement(By.Id("SeqCicloLetivoSituacaoMatricula_botao_modal")).Click();
                driver.FindElement(By.Name("AnoCiclo")).Clear();
                driver.FindElement(By.Name("AnoCiclo")).SendKeys("2023");
                driver.FindElement(By.Name("NumeroCiclo")).Click();
                driver.FindElement(By.Name("NumeroCiclo")).Clear();
                driver.FindElement(By.Name("NumeroCiclo")).SendKeys("2");//Última linhas da seleção do ciclo 1/2023
                

                driver.FindElement(By.Id("DataSelectorPesquisar0")).Click();
                driver.FindElement(By.Id("DataSelector_gridDataSelector0")).Click();
                driver.FindElement(By.Id("smc-dataselector-SeqCicloLetivoSituacaoMatricula")).Click();

                /*   //Seleciona a situação Matriculado 1 (esse código funciona mas troquei pelo 2 para tentar impedir que dê timeout)
                   driver.FindElement(By.XPath("//div[@id='SeqsSituacaoMatriculaCicloLetivo']/div/a")).Click();

                   driver.FindElement(By.Id("SeqsSituacaoMatriculaCicloLetivoSearch")).Click();
                   driver.FindElement(By.Id("SeqsSituacaoMatriculaCicloLetivoSearch")).Clear();
                   driver.FindElement(By.Id("SeqsSituacaoMatriculaCicloLetivoSearch")).SendKeys("Matriculado");

                   driver.FindElement(By.XPath("//div[@id='SeqsSituacaoMatriculaCicloLetivo']/div[2]/ul/li[9]")).Click();
                   driver.FindElement(By.Id("BotaoPesquisarVP")).Click();*/

                //Seleciona a situação Matriculado 2
                driver.FindElement(By.XPath("//div[@id='SeqsSituacaoMatriculaCicloLetivo']/div/a")).Click();
                driver.FindElement(By.XPath("//div[@id='SeqsSituacaoMatriculaCicloLetivo']/div[2]/ul/li[9]/span")).Click();
                driver.FindElement(By.Id("BotaoPesquisarVP")).Click();
                
                //Clica na engrenagem para inserir o benefício
                driver.FindElement(By.CssSelector("div[class='dropdown smc-buttonset-hide-more']")).Click();
                driver.FindElement(By.Id("DetailedListBotaoItem0")).Click();
                driver.FindElement(By.Id("BotaoNovoVP")).Click();
                driver.FindElement(By.Id("select_SeqBeneficio")).Click();
                new SMCSelectElement(driver.FindElement(By.Id("select_SeqBeneficio"))).SelectByText("Convênio - VALE Tomada de Decisão");
                driver.FindElement(By.Id("select_SeqBeneficio")).Click();
                
                driver.FindElement(By.Name("DataInicioVigencia")).Click();
                driver.FindElement(By.Name("DataInicioVigencia")).Clear();
              //driver.FindElement(By.Name("DataInicioVigencia")).SendKeys(primeiroDiaMes.ToString("dd/MM/yyyy"));
                driver.FindElement(By.Name("DataInicioVigencia")).SendKeys("01/04/2026");
                driver.FindElement(By.Name("DataFimVigencia")).Click();
                driver.FindElement(By.Name("DataFimVigencia")).Clear();
              //driver.FindElement(By.Name("DataFimVigencia")).SendKeys(ultimoDiaMes.ToString("dd/MM/yyyy"));
                driver.FindElement(By.Name("DataFimVigencia")).SendKeys("31/12/2026");

            driver.FindElement(By.Name("DataFimVigencia")).Submit();
                driver.FindElement(By.Name("ExibeValoresTermoAdesao")).Click();
                //driver.FindElement(By.Id("btnSalvarVPIN")).Click();
                //driver.FindElement(By.CssSelector("[title='Salvar'][type='submit']")).Click();

                //Verifica mensagem de gravação com sucesso
                //Assert.True(CheckSuccess(), "Era esperado sucesso e ocorreu um erro");

                //Declara a variável teste q ira receber o texto do campo
                string
                teste = driver.FindElement(By.XPath("//div[@id='centro']/div/div/div[2]")).GetValue();

                //Compara a mensagem exibida com a esperada:
                Assert.Equal("Pessoa atuação benefício incluído com sucesso.", teste);

                //Para fechar o Chrome em segundo plano
                driver.Driver.Close(); 
            });
        }
        [Fact]
        public void Aluno_2a_IncluiBeneficio()
        {
            base.ExecuteTest((driver) =>
            {
                Login(driver);
                //Pesquisar alunos de curso reguar e matriculados
                driver.FindElement(By.Id("select_SeqTipoVinculoAluno")).Click();
                Thread.Sleep(1600);
                new SMCSelectElement(driver.FindElement(By.Id("select_SeqTipoVinculoAluno"))).SelectByText("Curso Regular");
                driver.FindElement(By.Id("select_SeqTipoVinculoAluno")).Click();
                driver.FindElement(By.Id("SeqCicloLetivoSituacaoMatricula_botao_modal")).Click();
                driver.FindElement(By.Name("AnoCiclo")).Clear();
                driver.FindElement(By.Name("AnoCiclo")).SendKeys("2024");
                driver.FindElement(By.Name("NumeroCiclo")).Click();
                driver.FindElement(By.Name("NumeroCiclo")).Clear();
                driver.FindElement(By.Name("NumeroCiclo")).SendKeys("1");
                driver.FindElement(By.Id("DataSelectorPesquisar0")).Click();
                driver.FindElement(By.Id("DataSelector_gridDataSelector0")).Click();
                driver.FindElement(By.Id("smc-dataselector-SeqCicloLetivoSituacaoMatricula")).Click();
                driver.FindElement(By.XPath("//div[@id='SeqsSituacaoMatriculaCicloLetivo']/div/a")).Click();
                driver.FindElement(By.Id("SeqsSituacaoMatriculaCicloLetivoSearch")).Click();
                driver.FindElement(By.Id("SeqsSituacaoMatriculaCicloLetivoSearch")).Clear();
                driver.FindElement(By.Id("SeqsSituacaoMatriculaCicloLetivoSearch")).SendKeys("MATRICULADO");
                driver.FindElement(By.XPath("//div[@id='SeqsSituacaoMatriculaCicloLetivo']/div[2]/ul/li[9]/span")).Click();
                driver.FindElement(By.Id("BotaoPesquisarVP")).Click();
                

                //Inserindo o benefício
                driver.FindElement(By.Id("ButtonSetDropdown2")).Click();
                driver.FindElement(By.CssSelector("[title = 'Benefício'][data-type = 'smc-button-element']")).Click();

                driver.FindElement(By.CssSelector("[title='Associar benefício'][id='BotaoNovoVP']")).Click();

                new SMCSelectElement(driver.FindElement(By.Id("select_SeqBeneficio"))).SelectByText("Convênio - VALE Tomada de Decisão");
                driver.FindElement(By.Id("select_SeqBeneficio")).Click();

                driver.FindElement(By.Name("DataInicioVigencia")).Click();
                driver.FindElement(By.Name("DataInicioVigencia")).Clear();
                //driver.FindElement(By.Name("DataInicioVigencia")).SendKeys(primeiroDiaMes.ToString("dd/MM/yyyy"));
                driver.FindElement(By.Name("DataInicioVigencia")).SendKeys("01/04/2026");
                driver.FindElement(By.Name("DataFimVigencia")).Click();
                driver.FindElement(By.Name("DataFimVigencia")).Clear();
                //driver.FindElement(By.Name("DataFimVigencia")).SendKeys(ultimoDiaMes.ToString("dd/MM/yyyy"));
                driver.FindElement(By.Name("DataFimVigencia")).SendKeys("31/12/2026");

                driver.FindElement(By.Name("DataFimVigencia")).Submit();
                driver.FindElement(By.Name("ExibeValoresTermoAdesao")).Click();
                //driver.FindElement(By.Id("btnSalvarVPIN")).Click();
                //driver.FindElement(By.CssSelector("[title='Salvar'][type='submit']")).Click();

                //Verifica mensagem de gravação com sucesso
                //Assert.True(CheckSuccess(), "Era esperado sucesso e ocorreu um erro");

                //Declara a variável teste q ira receber o texto do campo
                string
                teste = driver.FindElement(By.XPath("//div[@id='centro']/div/div/div[2]")).GetValue();

                //Compara a mensagem exibida com a esperada:
                Assert.Equal("Pessoa atuação benefício incluído com sucesso.", teste);

                //Para fechar o Chrome em segundo plano
                driver.Driver.Close();


                Thread.Sleep(1000);
            });
        }



        [Fact]
        public void Aluno_3_ExcluiBeneficio()
        {
            base.ExecuteTest((driver) =>
            {
                Login(driver);
                //Selecionando o tipo de vínculo Curso Regular
                driver.FindElement(By.Id("select_SeqTipoVinculoAluno")).Click();
                new SMCSelectElement(driver.FindElement(By.Id("select_SeqTipoVinculoAluno"))).SelectByText("Curso Regular");
                driver.FindElement(By.Id("select_SeqTipoVinculoAluno")).Click();

                //Selecionando o último ciclo cadastrado
                //driver.FindElement(By.XPath("//button[@id='SeqCicloLetivoSituacaoMatricula_botao_modal']/i")).Click();  Tirar esse comentário quando o último ciclo tiver aluno matriculado e retirar a seleção do ciclo 1/2022

                //Selecionando o ciclo 1/2023
                driver.FindElement(By.Id("SeqCicloLetivoSituacaoMatricula_botao_modal")).Click();
                driver.FindElement(By.Name("AnoCiclo")).Clear();
                driver.FindElement(By.Name("AnoCiclo")).SendKeys("2023");
                driver.FindElement(By.Name("NumeroCiclo")).Click();
                driver.FindElement(By.Name("NumeroCiclo")).Clear();
                driver.FindElement(By.Name("NumeroCiclo")).SendKeys("2");//Última linhas da seleção do ciclo 1/2023

                driver.FindElement(By.Id("DataSelectorPesquisar0")).Click();
                driver.FindElement(By.Id("DataSelector_gridDataSelector0")).Click();
                driver.FindElement(By.Id("smc-dataselector-SeqCicloLetivoSituacaoMatricula")).Click();

                //Seleciona a situação Matriculado
                driver.FindElement(By.XPath("//div[@id='SeqsSituacaoMatriculaCicloLetivo']/div/a")).Click();
                driver.FindElement(By.Id("SeqsSituacaoMatriculaCicloLetivoSearch")).Click();
                driver.FindElement(By.Id("SeqsSituacaoMatriculaCicloLetivoSearch")).Clear();
                driver.FindElement(By.Id("SeqsSituacaoMatriculaCicloLetivoSearch")).SendKeys("Matriculado");
                driver.FindElement(By.XPath("//div[@id='SeqsSituacaoMatriculaCicloLetivo']/div[2]/ul/li[9]")).Click();
                driver.FindElement(By.Id("BotaoPesquisarVP")).Click();

                /*
                driver.FindElement(By.XPath("//div[@id='SeqsSituacaoMatriculaCicloLetivo']/div/a")).Click();
                driver.FindElement(By.XPath("//div[@id='SeqsSituacaoMatriculaCicloLetivo']/div/a")).Click();
                driver.FindElement(By.XPath("//div[@id='SeqsSituacaoMatriculaCicloLetivo']/div/a")).Click();
                driver.FindElement(By.Id("SeqsSituacaoMatriculaCicloLetivoSearch")).Click();
                driver.FindElement(By.Id("frmViewPadrao")).Submit();
                driver.FindElement(By.Id("SeqsSituacaoMatriculaCicloLetivoSearch")).Clear();
                driver.FindElement(By.Id("SeqsSituacaoMatriculaCicloLetivoSearch")).SendKeys("matriculado");*/


                /*driver.FindElement(By.Id("SeqsSituacaoMatriculaCicloLetivo")).Click();
                driver.FindElement(By.Id("SeqsSituacaoMatriculaCicloLetivoSearch")).Click();
                driver.FindElement(By.Id("SeqsSituacaoMatriculaCicloLetivoSearch")).Clear();
                driver.FindElement(By.Id("SeqsSituacaoMatriculaCicloLetivoSearch")).SendKeys("Matriculado");
                driver.FindElement(By.CssSelector("[data-value='6']")).Click();*/

                driver.FindElement(By.Id("BotaoPesquisarVP")).Click();
                driver.FindElement(By.CssSelector("div[class='dropdown smc-buttonset-hide-more']")).Click();
                driver.FindElement(By.Id("DetailedListBotaoItem0")).Click();
                driver.FindElement(By.Id("DetailedListBotaoItem1")).Click();
                driver.FindElement(By.Name("Justificativa")).Click();
                driver.FindElement(By.Name("Justificativa")).SendKeys("teste");
                driver.FindElement(By.CssSelector("button[class='smc-btn-salvar-destaque']")).Click();
                
                // Assert.True(CheckSuccess(), "Era esperado sucesso e ocorreu um erro");

                //Declara a variável teste q ira receber o texto do campo
                string
                teste = driver.FindElement(By.XPath("//div[@id='centro']/div/div/div[2]")).GetValue();

                //Compara a mensagem exibida com a esperada:
                Assert.Equal("Pessoa atuação benefício excluído com sucesso.", teste);

                //Para fechar o Chrome em segundo plano
                driver.Driver.Close();

            });
        }

        [Fact]
        public void Aluno_4_ValidaAtualizacaoHistorico() 
        //Verifica se o histórico de cancelamento de renovação de matrícula foi inserido e se foi excluído o histórico com situação Apto para matrícula. 
        //Rotina executada pelo encerramento de processo.

        {
            base.ExecuteTest((driver) =>
            {
                //Maximiza o Browser
                // driverNavigator.Manage().Window.Maximize();

                Login(driver);
                //Pesquisa alunos no ciclo letivo 1/2019 e que estão com a situação Cancelado por não efetivação da matrícula
                driver.FindElement(By.Name("SeqCicloLetivoIngresso_display_text")).Click();
                driver.FindElement(By.Name("SeqCicloLetivoIngresso_display_text")).Clear();
                driver.FindElement(By.Name("SeqCicloLetivoIngresso_display_text")).SendKeys("1/2018"); //falta filtro por situação do processo (método a ser criado), para identificar o aluno


                driver.FindElement(By.XPath("//div[@id='SeqsSituacaoMatriculaCicloLetivo']/div/a")).Click();
                driver.FindElement(By.XPath("//div[@id='SeqsSituacaoMatriculaCicloLetivo']/div/a")).Click();
                driver.FindElement(By.XPath("//div[@id='SeqsSituacaoMatriculaCicloLetivo']/div/a")).Click();
                driver.FindElement(By.Id("SeqsSituacaoMatriculaCicloLetivoSearch")).Click();
                driver.FindElement(By.Id("frmViewPadrao")).Submit();
                driver.FindElement(By.Id("SeqsSituacaoMatriculaCicloLetivoSearch")).Clear();
                driver.FindElement(By.Id("SeqsSituacaoMatriculaCicloLetivoSearch")).SendKeys("matriculado");



                /*driver.FindElement(By.Id("SeqsSituacaoMatriculaCicloLetivo")).Click();
                driver.FindElement(By.Id("SeqsSituacaoMatriculaCicloLetivoSearch")).Click();
                driver.FindElement(By.Id("SeqsSituacaoMatriculaCicloLetivoSearch")).Clear();
                driver.FindElement(By.Id("SeqsSituacaoMatriculaCicloLetivoSearch")).SendKeys("Matriculado");
                driver.FindElement(By.Id("frmViewPadrao")).Submit();*/

                driver.FindElement(By.Id("BotaoPesquisarVP")).Click();
                driver.FindElement(By.Id("ButtonSetDropdown2")).Click();
                driver.FindElement(By.Id("DetailedListBotaoItem7")).Click();
                //driver.FindElement(By.Id("ciclo82")).Click(); está ocorrendo erro nessa linha abri bug 31311

                //falta verificar se as situações existentes estão corretas (Excluída: Apto para matrícula excluída e Inserida: Cancelado por não efetivação da matrícula 

                //Para fechar o Chrome em segundo plano
                driver.Driver.Close();
            });

        }

        [Fact]
        public void Aluno_5_IdentificaSituacoesFuturas()
        //Identifica as situações futuras de um aluno. Quando o processo for encerrado, as situações futuras devem ser excluídas.
        
        {
            base.ExecuteTest((driver) =>
            {
                //Maximiza o Browser
                // driverNavigator.Manage().Window.Maximize();

                Login(driver);
                driver.FindElement(By.Name("NumeroRegistroAcademico")).Click();
                driver.FindElement(By.Name("NumeroRegistroAcademico")).Clear();
                driver.FindElement(By.Name("NumeroRegistroAcademico")).SendKeys("3143");
                driver.FindElement(By.Id("BotaoPesquisarVP")).Click();
                driver.FindElement(By.XPath("(.//*[normalize-space(text()) and normalize-space(.)='Alterar'])[1]/following::button[1]")).Click();
                driver.FindElement(By.Id("DetailedListBotaoItem7")).Click();

                //falta verificar se as situações futuras foram excluídas
                //Para fechar o Chrome em segundo plano
                driver.Driver.Close();
            });

        }
        /*
                [Fact]
                public void GetEmail()
                //Busca o e-mail do aluno para poder realizar login no portal aluno

                {
                    base.ExecuteTest((driver) =>
                    {
                        //Maximiza o Browser
                        // driverNavigator.Manage().Window.Maximize();

                        Login(driver);
                        driver.FindElement(By.Id("SeqsSituacaoMatriculaCicloLetivo")).Click();
                        driver.FindElement(By.Id("SeqsSituacaoMatriculaCicloLetivoSearch")).Click();
                        driver.FindElement(By.Id("SeqsSituacaoMatriculaCicloLetivoSearch")).Clear();
                        driver.FindElement(By.Id("SeqsSituacaoMatriculaCicloLetivoSearch")).SendKeys("Matriculado");

                        driver.FindElement(By.Id("frmViewPadrao")).Submit();
                        driver.FindElement(By.Id("frmViewPadrao")).Click();


                        driver.FindElement(By.Id("BotaoPesquisarVP")).Click();
                        driver.FindElement(By.Id("fieldsetPesquisaViewPadrao")).Click();
                        driver.FindElement(By.Id("BotaoPesquisarVP")).Click();
                        driver.FindElement(By.Id("DetailListBotaoAlterar")).Click();

                        driver.FindElement(By.Id("EnderecosEletronicos_botao_modal")).Click();
                        driver.FindElement(By.CssSelector("button[id='1']")).Click();

                        driver.FindElement(By.Name("EnderecoEletronico.Descricao")).Click();

                        //falta verificar se as situações futuras foram excluídas

                        //Para fechar o Chrome em segundo plano
                        driver.Driver.Close();

                    });

                }

            */

        [Fact]
        [Trait("Ordenado", "CRUD")]
        public void TesteOrdenadoCRUD()
        {
           //Pesquisar();
            Aluno_2a_IncluiBeneficio();
            Aluno_3_ExcluiBeneficio();
            //Aluno_4_ValidaAtualizacaoHistorico();
            //Aluno_5_IdentificaSituacoesFuturas();
        }

        private static void Login(ISMCWebDriver driver)
        {
            //----------------------------------------------------
            driver.GoToUrl(Consts.SERVIDOR_QUALIDADE_ADM);
            //força o sistema a esperar 15 minutos ou até que apareça o campo para login
            WebDriverWait wait = new WebDriverWait((OpenQA.Selenium.IWebDriver)driver, TimeSpan.FromSeconds(900));
            wait.Until(e => e.FindElement(By.Name("Login")));
            driver.SMCLoginCpf();
            //força o sistema a esperar 15 minutos ou até que apareça a home do SGA
            wait.Until(ExpectedConditions.TextToBePresentInElementLocated(By.XPath("//div[@id='divConteudoPrincipal']/section/h2"), "SGA.Administrativo"));

            driver.GoToUrl2(Consts.SERVIDOR_QUALIDADE_ADM + "ALN/Aluno");
            //----------------------------------------------------
        }



    }
}
