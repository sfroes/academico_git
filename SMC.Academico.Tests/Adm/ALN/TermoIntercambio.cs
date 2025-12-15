using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using ExpectedConditions = SeleniumExtras.WaitHelpers.ExpectedConditions;
using SMC.Framework.Test;
using System;
using System.Threading;
using Xunit;

namespace SMC.Academico.Tests.ADM.ALN
{
    public class TermoIntercambio : SMCSeleniumUnitTest
    {
        //Informar dados que deseja associar ao Termo de Intercâmbio.
        private string _cpf = "019.535.512-11";
        private string _passaporte = "";
        private string _nivelEnsino = "Mestrado Acadêmico";

        [Fact]
        [Trait("Cenário", "Administrativo - Inserir Termo de Intercâmbio - Cotutela")]

        public void InserirCotutela()
        {            
            base.ExecuteTest((driver) =>
            {
                //Chamada de método para inclusão de Parceria de Intercâbio (termo está dentro da parceria).
                var p = new ParceriaIntercambio();
                p.ParceriaIntercambio_1_Inserir();

                //Maximiza o Browser
                // driverNavigator.Manage().Window.Maximize();

                Login(driver);

                //Pesquisa Parceria de Intercâmbio.
                driver.FindElement(By.Name("Descricao")).Click();
                driver.FindElement(By.Name("Descricao")).Clear();
                driver.FindElement(By.Name("Descricao")).SendKeys("Teste QA Parceria Intercâmbio Incluir");
                driver.FindElement(By.Id("BotaoPesquisarVP")).Click();
                //Inserir Termo de Intercâmbio.
                driver.FindElement(By.XPath("(.//*[normalize-space(text()) and normalize-space(.)='Excluir'])[2]/following::button[1]")).Click();
                driver.FindElement(By.Id("DetailedListBotaoItem0")).Click();
                driver.FindElement(By.Id("BotaoNovoVP")).Click();
                driver.FindElement(By.Name("Descricao")).Clear();
                driver.FindElement(By.Name("Descricao")).SendKeys("Teste QA Termo Intercâmbio Cotutela Incluir");
                driver.FindElement(By.Id("select_SeqNivelEnsino")).Click();
                new SMCSelectElement(driver.FindElement(By.Id("select_SeqNivelEnsino"))).SelectByText(_nivelEnsino);
                driver.FindElement(By.Id("select_SeqNivelEnsino")).Click();
                driver.FindElement(By.Id("select_SeqParceriaIntercambioTipoTermo")).Click();
                new SMCSelectElement(driver.FindElement(By.Id("select_SeqParceriaIntercambioTipoTermo"))).SelectByText("Cotutela");
                driver.FindElement(By.Id("select_SeqParceriaIntercambioTipoTermo")).Click();
                driver.FindElement(By.Id("select_SeqParceriaIntercambioInstituicaoExterna")).Click();
                new SMCSelectElement(driver.FindElement(By.Id("select_SeqParceriaIntercambioInstituicaoExterna"))).SelectByText("ABEU - CENTRO UNIVERSITÁRIO");
                driver.FindElement(By.Id("TiposMobilidadeDetailPartialInserir")).Click();
                driver.FindElement(By.Id("select_TipoMobilidade")).Click();
                new SMCSelectElement(driver.FindElement(By.Id("select_TipoMobilidade"))).SelectByText("Saída para outra instituição");
                driver.FindElement(By.Id("select_TipoMobilidade")).Click();
                driver.FindElement(By.Name("QuantidadeVagas")).Click();
                driver.FindElement(By.Name("QuantidadeVagas")).Clear();
                driver.FindElement(By.Name("QuantidadeVagas")).SendKeys("1");
                driver.FindElement(By.Id("Pessoas_DetailBotaoInserirElemento")).Click();

                //Verifica se o aluno possui CPF ou Passaporte e insere o dado no registro.
                if (string.IsNullOrEmpty(_cpf))
                {
                    //Aluno sem CPF preenchido, utiize o passaporte.
                    driver.FindElement(By.Name("Pessoas[0].Passaporte")).Clear();
                    driver.FindElement(By.Name("Pessoas[0].Passaporte")).SendKeys(_passaporte);

                    //Click por causa do BUG do componente (campo obrigatório).
                    driver.FindElement(By.Name("Pessoas[0].Cpf")).Click();
                }
                else
                {
                    //Aluno com CPF preenchido.
                    driver.FindElement(By.Name("Pessoas[0].Cpf")).Clear();
                    driver.FindElement(By.Name("Pessoas[0].Cpf")).SendKeys(_cpf);

                    //Click por causa do BUG do componente (campo obrigatório).
                    driver.FindElement(By.Name("Pessoas[0].Passaporte")).Click();
                }

                driver.FindElement(By.Id("TiposMobilidade_DetailFormBotaoIncluir")).Click();
                //driver.FindElement(By.Id("BotaoSalvarTemplate")).Click();
                
                driver.FindElement(By.CssSelector("[title='Salvar'][type='submit']")).Click();

                //Checando a mensagem de Sucesso
                Assert.True(CheckMessage("Termo de intercâmbio incluído com sucesso."), "Mensagem esperada não exibida");

                //Para fechar o Chrome em segundo plano
                driver.Driver.Close();

            });

        }

        [Fact]
        [Trait("Cenário", "Administrativo - Inserir Termo de Intercâmbio - Tipo Intercâmbio")]

        public void InserirTermo() //Termo a ser usado na inclusão de ingressante com intercâmbio e ingresso direto
        {
            base.ExecuteTest((driver) =>
            {
                //Chamada de método para inclusão de Parceria de Intercâbio (termo está dentro da parceria).
                var p = new ParceriaIntercambio();
                p.ParceriaIntercambio_1_Inserir();

                //Maximiza o Browser
                // driverNavigator.Manage().Window.Maximize();

                Login(driver);

                //Pesquisa Parceria de Intercâmbio.
                driver.FindElement(By.Name("Descricao")).Click();
                driver.FindElement(By.Name("Descricao")).Clear();
                driver.FindElement(By.Name("Descricao")).SendKeys("Teste QA Parceria Intercâmbio Incluir");
                driver.FindElement(By.Id("BotaoPesquisarVP")).Click();
                //Inserir Termo de Intercâmbio.
                driver.FindElement(By.XPath("(.//*[normalize-space(text()) and normalize-space(.)='Excluir'])[2]/following::button[1]")).Click();
                driver.FindElement(By.Id("DetailedListBotaoItem0")).Click();
                driver.FindElement(By.Id("BotaoNovoVP")).Click();
                driver.FindElement(By.Name("Descricao")).Clear();
                driver.FindElement(By.Name("Descricao")).SendKeys("Teste QA Termo Intercâmbio do Tipo Intercâmbio Incluir");
                driver.FindElement(By.Id("select_SeqNivelEnsino")).Click();
                new SMCSelectElement(driver.FindElement(By.Id("select_SeqNivelEnsino"))).SelectByText("Doutorado Acadêmico");
                driver.FindElement(By.Id("select_SeqNivelEnsino")).Click();
                driver.FindElement(By.Id("select_SeqParceriaIntercambioTipoTermo")).Click();
                new SMCSelectElement(driver.FindElement(By.Id("select_SeqParceriaIntercambioTipoTermo"))).SelectByText("Intercâmbio");
                driver.FindElement(By.Id("select_SeqParceriaIntercambioTipoTermo")).Click();
                driver.FindElement(By.Id("select_SeqParceriaIntercambioInstituicaoExterna")).Click();
                new SMCSelectElement(driver.FindElement(By.Id("select_SeqParceriaIntercambioInstituicaoExterna"))).SelectByText("ABEU - CENTRO UNIVERSITÁRIO");

                var primeiroDiaMes = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1);
                var ultimoDiaMes = primeiroDiaMes.AddMonths(1).AddDays(-1);
                
                //Inclusão de periodicidade do Termo
                driver.FindElement(By.Id("VigenciasDetailPartialInserir")).Click();
                driver.FindElement(By.Name("DataInicio")).Click();
                driver.FindElement(By.Name("DataInicio")).Clear();
                driver.FindElement(By.Name("DataInicio")).SendKeys(primeiroDiaMes.ToString("dd/MM/yyyy"));
                driver.FindElement(By.Name("DataFim")).Click();
                driver.FindElement(By.Name("DataFim")).Clear();
                driver.FindElement(By.Name("DataFim")).SendKeys(ultimoDiaMes.ToString("dd/MM/yyyy"));
                driver.FindElement(By.Id("Vigencias_DetailFormBotaoIncluir")).Click();
                            
                driver.FindElement(By.Id("TiposMobilidadeDetailPartialInserir")).Click();
                driver.FindElement(By.Id("select_TipoMobilidade")).Click();
                new SMCSelectElement(driver.FindElement(By.Id("select_TipoMobilidade"))).SelectByText("Ingresso em nossa instituição");
                driver.FindElement(By.Id("select_TipoMobilidade")).Click();
                driver.FindElement(By.Name("QuantidadeVagas")).Click();
                driver.FindElement(By.Name("QuantidadeVagas")).Clear();
                driver.FindElement(By.Name("QuantidadeVagas")).SendKeys("1");
                driver.FindElement(By.Id("Pessoas_DetailBotaoInserirElemento")).Click();
                              
                {   //Aluno com CPF preenchido.
                    driver.FindElement(By.Name("Pessoas[0].Cpf")).Clear();
                    driver.FindElement(By.Name("Pessoas[0].Cpf")).SendKeys("85702919653");

                    //Click por causa do BUG do componente (campo obrigatório).
                    driver.FindElement(By.Name("Pessoas[0].Passaporte")).Click();
                }

                driver.FindElement(By.Id("TiposMobilidade_DetailFormBotaoIncluir")).Click();
                driver.FindElement(By.Id("BotaoSalvarTemplate")).Click();


                //Checando a mensagem de Sucesso
                Assert.True(CheckMessage("Termo de intercâmbio incluído com sucesso."), "Mensagem esperada não exibida");

                //Para fechar o Chrome em segundo plano
                driver.Driver.Close();

            });

        }

        [Fact]
        [Trait("Cenário", "Administrativo - Alterar Termo de Intercâmbio - Cotutela")]

        public void AlterarCotutela()
        {
            base.ExecuteTest((driver) =>
            {
                 //Maximiza o Browser
                // driverNavigator.Manage().Window.Maximize();

                Login(driver);

                //Pesquisa Parceria de Intercâmbio. 
                driver.FindElement(By.Name("Descricao")).Click();
                driver.FindElement(By.Name("Descricao")).Clear();
                driver.FindElement(By.Name("Descricao")).SendKeys("Teste QA Parceria Intercâmbio Incluir");
                driver.FindElement(By.Id("BotaoPesquisarVP")).Click();
                driver.FindElement(By.XPath("(.//*[normalize-space(text()) and normalize-space(.)='Excluir'])[2]/following::i[1]")).Click();
                //Pesquisar Termo de Intercâmbio.
                driver.FindElement(By.Id("DetailedListBotaoItem0")).Click();
                driver.FindElement(By.Name("Descricao")).Click();
                driver.FindElement(By.Name("Descricao")).Clear();
                driver.FindElement(By.Name("Descricao")).SendKeys("Teste QA Termo Intercâmbio Cotutela Incluir");
                driver.FindElement(By.Id("BotaoPesquisarVP")).Click();
                //Alterar Descrição.
                driver.FindElement(By.XPath("(.//*[normalize-space(text()) and normalize-space(.)='Alterar'])[1]/i[1]")).Click();
                driver.FindElement(By.Id("formEditViewPadrao")).Click();
                driver.FindElement(By.Name("Descricao")).Clear();
                driver.FindElement(By.Name("Descricao")).SendKeys("Teste QA Termo Intercâmbio Cotutela Alterar");
                //Alterar Nivel Ensino
                driver.FindElement(By.Id("select_SeqNivelEnsino")).Click();
                new SMCSelectElement(driver.FindElement(By.Id("select_SeqNivelEnsino"))).SelectByText("Mestrado Profissional");
                driver.FindElement(By.Id("select_SeqNivelEnsino")).Click();
                //Altar o tipo de termo e instituição
                driver.FindElement(By.Id("select_SeqParceriaIntercambioTipoTermo")).Click();
                new SMCSelectElement(driver.FindElement(By.Id("select_SeqParceriaIntercambioTipoTermo"))).SelectByText("Intercâmbio");
                driver.FindElement(By.Id("select_SeqParceriaIntercambioTipoTermo")).Click();
                driver.FindElement(By.Id("select_SeqParceriaIntercambioInstituicaoExterna")).Click();
                new SMCSelectElement(driver.FindElement(By.Id("select_SeqParceriaIntercambioInstituicaoExterna"))).SelectByText("ABEU - CENTRO UNIVERSITÁRIO");

                //Editar Tipo de mobilidade.
                driver.FindElement(By.Id("TiposMobilidade_MasterDetailBotaoEditModal0")).Click();
                driver.FindElement(By.Name("QuantidadeVagas")).Click();
                driver.FindElement(By.Name("QuantidadeVagas")).Clear();
                driver.FindElement(By.Name("QuantidadeVagas")).SendKeys("2");
                driver.FindElement(By.Name("Pessoas[0].Cpf")).Click();
                driver.FindElement(By.Name("Pessoas[0].Cpf")).Clear();
                driver.FindElement(By.Name("Pessoas[0].Cpf")).SendKeys("016.427.226-77");
                driver.FindElement(By.Id("Pessoas_DetailBotaoInserirElemento")).Click();
                driver.FindElement(By.Name("Pessoas[1].Cpf")).Clear();
                driver.FindElement(By.Name("Pessoas[1].Cpf")).SendKeys("047.575.126-43");
                //Click por causa do BUG do componente (campo obrigatório).
                driver.FindElement(By.Name("Pessoas[1].Passaporte")).Click();
                driver.FindElement(By.Id("TiposMobilidade_DetailFormBotaoIncluir")).Click();
                driver.FindElement(By.Id("BotaoSalvarTemplate")).Click();

                //Checando a mensagem de Sucesso
                Assert.True(CheckMessage("Termo de intercâmbio alterado com sucesso."), "Mensagem esperada não exibida");

                //Para fechar o Chrome em segundo plano
                driver.Driver.Close();

            });

        }

        [Fact]
        [Trait("Cenário", "Administrativo - Excluir Termo de Intercâmbio - Cotutela")]

        public void ExcluirCotutela()
        {
            base.ExecuteTest((driver) =>
            {
                //Maximiza o Browser
                // driverNavigator.Manage().Window.Maximize();

                Login(driver);

                //Pesquisa Parceria de Intercâmbio. 
                driver.FindElement(By.Name("Descricao")).Click();
                driver.FindElement(By.Name("Descricao")).Clear();
                driver.FindElement(By.Name("Descricao")).SendKeys("Teste QA Parceria Intercâmbio Incluir");
                driver.FindElement(By.Id("BotaoPesquisarVP")).Click();
                driver.FindElement(By.XPath("(.//*[normalize-space(text()) and normalize-space(.)='Excluir'])[2]/following::i[1]")).Click();
                //Pesquisar Termo de Intercâmbio.
                driver.FindElement(By.Id("DetailedListBotaoItem0")).Click();
                driver.FindElement(By.Name("Descricao")).Click();
                driver.FindElement(By.Name("Descricao")).Clear();
                driver.FindElement(By.Name("Descricao")).SendKeys("Teste QA Termo Intercâmbio Cotutela Alterar");
                driver.FindElement(By.Id("BotaoPesquisarVP")).Click();
                //Excluir Termo de Intercâmbio.
                driver.FindElement(By.Id("DetailListBotaoExcluir0")).Click();
                driver.FindElement(By.Id("BotaoPadraoPerguntaSim")).Click();

                //Checando a mensagem de Sucesso
                Assert.True(CheckMessage("Termo de intercâmbio excluído com sucesso."), "Mensagem esperada não exibida");

                //Para fechar o Chrome em segundo plano
                driver.Driver.Close();

            });

        }

        [Fact]
        [Trait("Cenário", "Administrativo - Inserir Termo de Intercâmbio - Sanduiche")]

        public void InserirSanduiche()
        {
            base.ExecuteTest((driver) =>
            {
                //Chamada de método para inclusão de Parceria de Intercâbio (termo está dentro da parceria).
                // var p = new ParceriaIntercambio();
                // p.ParceriaIntercambio_1_Inserir();

                //Maximiza o Browser
                // driverNavigator.Manage().Window.Maximize();

                Login(driver);
                var primeiroDiaMes = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1);
                var ultimoDiaMes = primeiroDiaMes.AddMonths(1).AddDays(-1);

                //Pesquisa Parceria de Intercâmbio.
                driver.FindElement(By.Name("Descricao")).Click();
                driver.FindElement(By.Name("Descricao")).Clear();
                driver.FindElement(By.Name("Descricao")).SendKeys("Teste QA Parceria Intercâmbio Incluir");
                driver.FindElement(By.Id("BotaoPesquisarVP")).Click();
                //Inserir Termo de Intercâmbio.
                driver.FindElement(By.XPath("(.//*[normalize-space(text()) and normalize-space(.)='Excluir'])[2]/following::button[1]")).Click();
                driver.FindElement(By.Id("DetailedListBotaoItem0")).Click();
                driver.FindElement(By.Id("BotaoNovoVP")).Click();
                driver.FindElement(By.Name("Descricao")).Clear();
                driver.FindElement(By.Name("Descricao")).SendKeys("Teste QA Termo Intercâmbio Sanduiche Incluir");
                driver.FindElement(By.Id("select_SeqNivelEnsino")).Click();
                new SMCSelectElement(driver.FindElement(By.Id("select_SeqNivelEnsino"))).SelectByText(_nivelEnsino);
                driver.FindElement(By.Id("select_SeqNivelEnsino")).Click();
                driver.FindElement(By.Id("select_SeqParceriaIntercambioTipoTermo")).Click();
                new SMCSelectElement(driver.FindElement(By.Id("select_SeqParceriaIntercambioTipoTermo"))).SelectByText("Sanduiche");
                driver.FindElement(By.Id("select_SeqParceriaIntercambioTipoTermo")).Click();
                driver.FindElement(By.Id("select_SeqParceriaIntercambioInstituicaoExterna")).Click();
                new SMCSelectElement(driver.FindElement(By.Id("select_SeqParceriaIntercambioInstituicaoExterna"))).SelectByText("ABEU - CENTRO UNIVERSITÁRIO");
                driver.FindElement(By.Id("VigenciasDetailPartialInserir")).Click();
                driver.FindElement(By.Name("DataInicio")).Click();
                driver.FindElement(By.Name("DataInicio")).Clear();
                driver.FindElement(By.Name("DataInicio")).SendKeys(primeiroDiaMes.ToString("dd/MM/yyyy"));
                driver.FindElement(By.Name("DataFim")).Click();
                driver.FindElement(By.Name("DataFim")).Clear();
                driver.FindElement(By.Name("DataFim")).SendKeys(ultimoDiaMes.ToString("dd/MM/yyyy"));
                driver.FindElement(By.Id("Vigencias_DetailFormBotaoIncluir")).Click();
                driver.FindElement(By.Id("TiposMobilidadeDetailPartialInserir")).Click();
                driver.FindElement(By.Id("select_TipoMobilidade")).Click();
                new SMCSelectElement(driver.FindElement(By.Id("select_TipoMobilidade"))).SelectByText("Saída para outra instituição");
                driver.FindElement(By.Id("select_TipoMobilidade")).Click();
                driver.FindElement(By.Name("QuantidadeVagas")).Click();
                driver.FindElement(By.Name("QuantidadeVagas")).Clear();
                driver.FindElement(By.Name("QuantidadeVagas")).SendKeys("1");
                driver.FindElement(By.Id("Pessoas_DetailBotaoInserirElemento")).Click();

                //Verifica se o aluno possui CPF ou Passaporte e insere o dado no registro.
                if (string.IsNullOrEmpty(_cpf))
                {
                    //Aluno sem CPF preenchido, utiize o passaporte.
                    driver.FindElement(By.Name("Pessoas[0].Passaporte")).Clear();
                    driver.FindElement(By.Name("Pessoas[0].Passaporte")).SendKeys(_passaporte);

                    //Click por causa do BUG do componente (campo obrigatório).
                    driver.FindElement(By.Name("Pessoas[0].Cpf")).Click();
                }
                else
                {
                    //Aluno com CPF preenchido.
                    driver.FindElement(By.Name("Pessoas[0].Cpf")).Clear();
                    driver.FindElement(By.Name("Pessoas[0].Cpf")).SendKeys(_cpf);

                    //Click por causa do BUG do componente (campo obrigatório).
                    driver.FindElement(By.Name("Pessoas[0].Passaporte")).Click();
                }

                driver.FindElement(By.Id("TiposMobilidade_DetailFormBotaoIncluir")).Click();
                driver.FindElement(By.Id("BotaoSalvarTemplate")).Click();


                //Checando a mensagem de Sucesso
                Assert.True(CheckMessage("Termo de intercâmbio incluído com sucesso."), "Mensagem esperada não exibida");

                //Para fechar o Chrome em segundo plano
                driver.Driver.Close();

            });

        }

        [Fact]
        [Trait("Cenário", "Administrativo - Alterar Termo de Intercâmbio - Sanduiche")]

        public void AlterarSanduiche()
        {
            base.ExecuteTest((driver) =>
            {
                //Maximiza o Browser
                // driverNavigator.Manage().Window.Maximize();

                Login(driver);

                //Pesquisa Parceria de Intercâmbio. 
                driver.FindElement(By.Name("Descricao")).Click();
                driver.FindElement(By.Name("Descricao")).Clear();
                driver.FindElement(By.Name("Descricao")).SendKeys("Teste QA Parceria Intercâmbio Incluir");
                driver.FindElement(By.Id("BotaoPesquisarVP")).Click();
                driver.FindElement(By.XPath("(.//*[normalize-space(text()) and normalize-space(.)='Excluir'])[2]/following::i[1]")).Click();
                //Pesquisar Termo de Intercâmbio.
                driver.FindElement(By.Id("DetailedListBotaoItem0")).Click();
                driver.FindElement(By.Name("Descricao")).Click();
                driver.FindElement(By.Name("Descricao")).Clear();
                driver.FindElement(By.Name("Descricao")).SendKeys("Teste QA Termo Intercâmbio Sanduiche Incluir");
                driver.FindElement(By.Id("BotaoPesquisarVP")).Click();
                //Alterar Descrição.
                driver.FindElement(By.XPath("(.//*[normalize-space(text()) and normalize-space(.)='Alterar'])[1]/i[1]")).Click();
                driver.FindElement(By.Id("formEditViewPadrao")).Click();
                driver.FindElement(By.Name("Descricao")).Clear();
                driver.FindElement(By.Name("Descricao")).SendKeys("Teste QA Termo Intercâmbio SAnduiche Alterar");
                //Alterar Nivel Ensino
                driver.FindElement(By.Id("select_SeqNivelEnsino")).Click();
                new SMCSelectElement(driver.FindElement(By.Id("select_SeqNivelEnsino"))).SelectByText("Mestrado Profissional");
                driver.FindElement(By.Id("select_SeqNivelEnsino")).Click();
                driver.FindElement(By.Id("select_SeqParceriaIntercambioTipoTermo")).Click();
                new SMCSelectElement(driver.FindElement(By.Id("select_SeqParceriaIntercambioTipoTermo"))).SelectByText("Sanduiche");
                driver.FindElement(By.Id("select_SeqParceriaIntercambioTipoTermo")).Click();
                //Alterar Período de Vigência
                driver.FindElement(By.Id("Vigencias_MasterDetailBotaoEditModal0")).Click();
                var primeiroDiaMes = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 2);
                var ultimoDiaMes = primeiroDiaMes.AddMonths(1).AddDays(-2);
                driver.FindElement(By.Name("DataInicio")).Click();
                driver.FindElement(By.Name("DataInicio")).Clear();
                driver.FindElement(By.Name("DataInicio")).SendKeys(primeiroDiaMes.ToString("dd/MM/yyyy"));
                driver.FindElement(By.Name("DataFim")).Click();
                driver.FindElement(By.Name("DataFim")).Clear();
                driver.FindElement(By.Name("DataFim")).SendKeys(ultimoDiaMes.ToString("dd/MM/yyyy"));
                driver.FindElement(By.Id("Vigencias_DetailFormBotaoIncluir")).Click();
                //Editar Tipo de mobilidade.
                driver.FindElement(By.Id("TiposMobilidade_MasterDetailBotaoEditModal2")).Click();
                driver.FindElement(By.Id("select_TipoMobilidade")).Click();
                new SMCSelectElement(driver.FindElement(By.Id("select_TipoMobilidade"))).SelectByText("Saída para outra instituição");
                driver.FindElement(By.Id("select_TipoMobilidade")).Click();
                driver.FindElement(By.Name("QuantidadeVagas")).Click();
                driver.FindElement(By.Name("QuantidadeVagas")).Clear();
                driver.FindElement(By.Name("QuantidadeVagas")).SendKeys("2");
                
                driver.FindElement(By.Name("Pessoas[0].Cpf")).Click();
                driver.FindElement(By.Name("Pessoas[0].Cpf")).Clear();
                driver.FindElement(By.Name("Pessoas[0].Cpf")).SendKeys("016.427.226-77");
                driver.FindElement(By.Id("Pessoas_DetailBotaoInserirElemento")).Click();
                driver.FindElement(By.Name("Pessoas[1].Cpf")).Clear();
                driver.FindElement(By.Name("Pessoas[1].Cpf")).SendKeys("047.575.126-43");
                driver.FindElement(By.Name("Pessoas[1].Passaporte")).Click();
                driver.FindElement(By.Id("TiposMobilidade_DetailFormBotaoIncluir")).Click();
               


                driver.FindElement(By.Id("BotaoSalvarTemplate")).Click();

                //Checando a mensagem de Sucesso
                Assert.True(CheckMessage("Termo de intercâmbio alterado com sucesso."), "Mensagem esperada não exibida");

                //Para fechar o Chrome em segundo plano
                driver.Driver.Close();

            });

        }


        [Fact]
        [Trait("Cenário", "Administrativo - Excluir Termo de Intercâmbio - Sanduiche")]

        public void ExcluirSanduiche()
        {
            base.ExecuteTest((driver) =>
            {
                //Maximiza o Browser
                // driverNavigator.Manage().Window.Maximize();

                Login(driver);

                //Pesquisa Parceria de Intercâmbio. 
                driver.FindElement(By.Name("Descricao")).Click();
                driver.FindElement(By.Name("Descricao")).Clear();
                driver.FindElement(By.Name("Descricao")).SendKeys("Teste QA Parceria Intercâmbio Incluir");
                driver.FindElement(By.Id("BotaoPesquisarVP")).Click();
                driver.FindElement(By.XPath("(.//*[normalize-space(text()) and normalize-space(.)='Excluir'])[2]/following::i[1]")).Click();
                //Pesquisar Termo de Intercâmbio.
                driver.FindElement(By.Id("DetailedListBotaoItem0")).Click();
                driver.FindElement(By.Name("Descricao")).Click();
                driver.FindElement(By.Name("Descricao")).Clear();
                driver.FindElement(By.Name("Descricao")).SendKeys("Teste QA Termo Intercâmbio Sanduiche Alterar");
                driver.FindElement(By.Id("BotaoPesquisarVP")).Click();
                //Excluir Termo de Intercâmbio.
                driver.FindElement(By.Id("DetailListBotaoExcluir0")).Click();
                driver.FindElement(By.Id("BotaoPadraoPerguntaSim")).Click();

                //Checando a mensagem de Sucesso
                Assert.True(CheckMessage("Termo de intercâmbio excluído com sucesso."), "Mensagem esperada não exibida");

                //Para fechar o Chrome em segundo plano
                driver.Driver.Close();

            });

        }

        [Fact]
        [Trait("Cenário", "Administrativo - Excluir Termo de Intercâmbio - Intercâmbio")]

        public void ExcluirIntercambio()
        {
            base.ExecuteTest((driver) =>
            {
                //Maximiza o Browser
                // driverNavigator.Manage().Window.Maximize();

                Login(driver);

                //Pesquisa Parceria de Intercâmbio. 
                driver.FindElement(By.Name("Descricao")).Click();
                driver.FindElement(By.Name("Descricao")).Clear();
                driver.FindElement(By.Name("Descricao")).SendKeys("Teste QA Parceria Intercâmbio Incluir");
                driver.FindElement(By.Id("BotaoPesquisarVP")).Click();
                driver.FindElement(By.XPath("(.//*[normalize-space(text()) and normalize-space(.)='Excluir'])[2]/following::i[1]")).Click();
                //Pesquisar Termo de Intercâmbio.
                driver.FindElement(By.Id("DetailedListBotaoItem0")).Click();
                driver.FindElement(By.Name("Descricao")).Click();
                driver.FindElement(By.Name("Descricao")).Clear();
                driver.FindElement(By.Name("Descricao")).SendKeys("Teste QA Termo Intercâmbio do Tipo Intercâmbio Incluir");
                driver.FindElement(By.Id("BotaoPesquisarVP")).Click();
                //Excluir Termo de Intercâmbio.
                driver.FindElement(By.Id("DetailListBotaoExcluir0")).Click();
                driver.FindElement(By.Id("BotaoPadraoPerguntaSim")).Click();

                //Checando a mensagem de Sucesso
                Assert.True(CheckMessage("Termo de intercâmbio excluído com sucesso."), "Mensagem esperada não exibida");

                //Para fechar o Chrome em segundo plano
                driver.Driver.Close();

            });

        }

        [Fact]
        [Trait("Cenário", "Administrativo - Inserir Termo de Intercâmbio com Pesquisa Aluno - Cotutela")]

        public void InserirCotutelaAlunoPesquisar()
        {
            //Maximiza o Browser
            // driverNavigator.Manage().Window.Maximize();

            //Pesquisa Aluno
            var admSolicitacao = new SMC.Academico.Tests.ADM.ALN.Aluno();
            (_, _cpf, _passaporte, _nivelEnsino, _, _, _) = admSolicitacao.Pesquisar();

            //-------------------------------------------------------------------------------------------------
            //Executa o script para inserir a Parceriade de Intercâmbio.
            //Posteriormente insere o Termo de Intercâmbio (associado a parceria).
            var parceria = new ParceriaIntercambio();
            parceria.ParceriaIntercambio_1_Inserir();
            //-------------------------------------------------------------------------------------------------

            //Inserindo o Termo de Intercâmbio passando os dados da pesquisa do aluno.
            InserirCotutela();

        }

        [Fact]
        [Trait("Cenário", "Administrativo - Inserir Termo de Intercâmbio com Pesquisa Aluno - Sanduiche")]

        public void InserirSanduicheAlunoPesquisar()
        {
            //Maximiza o Browser
            // driverNavigator.Manage().Window.Maximize();

            //Pesquisa Aluno
            var admSolicitacao = new SMC.Academico.Tests.ADM.ALN.Aluno();
            (_, _cpf, _passaporte, _nivelEnsino, _, _, _) = admSolicitacao.Pesquisar();

            //-------------------------------------------------------------------------------------------------
            //Executa o script para inserir a Parceriade de Intercâmbio.
            //Posteriormente insere o Termo de Intercâmbio (associado a parceria).
            var parceria = new ParceriaIntercambio();
            parceria.ParceriaIntercambio_1_Inserir();
            //-------------------------------------------------------------------------------------------------

            //Inserindo o Termo de Intercâmbio passando os dados da pesquisa do aluno.
            InserirSanduiche();
        }

        [Fact]
        [Trait("Ordenado", "CRUD")]
        public void TesteOrdenadoCRUD()
        {
            InserirCotutela();
            AlterarCotutela();
            ExcluirCotutela();
            InserirSanduiche();
            AlterarSanduiche();
            ExcluirSanduiche();

            //depende do script de alteração e exclusão de parceria de intercâmbio para excluir da base
            var ParceriaIntercambioAlterar = new ParceriaIntercambio();
            ParceriaIntercambioAlterar.ParceriaIntercambio_2_Alterar();

            var ParceriaIntercambioExcluir = new ParceriaIntercambio();
            ParceriaIntercambioExcluir.ParceriaIntercambio_3_Excluir();

            
        }


        private static void Login(ISMCWebDriver driver)
        {
            //----------------------------------------------------
            /* driver.GoToUrl(Consts.SERVIDOR_QUALIDADE_ADM);
             //força o sistema a esperar 15 minutos ou até que apareça o campo para login
             WebDriverWait wait = new WebDriverWait((OpenQA.Selenium.IWebDriver)driver, TimeSpan.FromSeconds(900));
             wait.Until(e => e.FindElement(By.Name("Login")));
             driver.SMCLoginCpf();
             //força o sistema a esperar 15 minutos ou até que apareça a home do SGA
             wait.Until(ExpectedConditions.TextToBePresentInElementLocated(By.XPath("//div[@id='divConteudoPrincipal']/section/h2"), "SGA.Administrativo"));*/
            //----------------------------------------------------
            driver.GoToUrl(Consts.SERVIDOR_QUALIDADE_ADM); //recebe o link para acesso

            //Login
            driver.SMCLoginCpf(); //insere login e senha
                       

            driver.GoToUrl2(Consts.SERVIDOR_QUALIDADE_ADM + "ALN/ParceriaIntercambio");
            //-------------------------------------------------------------------------------------------------
        }


    }
}

/********************************************************************        ATENÇÃO        ********************************************************************/
/* Após a execução deste script, verficiar de existe parametrização na tela "Associação de Vínculo por Instituição e Nível de Ensino" (SGA Administrativo), 
   menu Parâmetros por Nível de Ensino > Aluno > Tipo de Vínculo de Aluno x Nível de Ensino.
   A parametrização deve considerar o Nível de ensino, Vínculo (Curso Regular), Tipo de termo de intercâmbio e o Tipo de orintação (Orientação em Intercâmbio).
   Após verificar a existência da parametrização, executar o script para realização do atendimento de intercâmbio*/
