using SMC.Framework.Test;
using Xunit;

namespace SMC.Academico.Tests.ALUNO.ALN
{
    public class TesteAluno : SMCSeleniumUnitTest
    {
        // private bool phantomJS = false;

        private string _email = "";

        public TesteAluno(string email = "")
        {
            _email = email;
        }

        [Fact]

        public void Inserir()
        {
            base.ExecuteTest((driver) =>
            {
                //Maximiza o Browser
                // driverNavigator.Manage().Window.Maximize();

                Login(driver, _email);
                //-------------------------------------------------------------------------------------------------

            });
        }

        [Fact]

        public void Excluir()
        {
        }

            private static void Login(ISMCWebDriver driver, string email = "hugobrito@pucminas.br")
        {
            //----------------------------------------------------
            driver.GoToUrl(Consts.SERVIDOR_QUALIDADE_ALUNO);
            //Login
            driver.SMCLoginEmail(email);

            driver.GoToUrl2(Consts.SERVIDOR_QUALIDADE_ALUNO + "");
            //----------------------------------------------------
        }


    }
}
