using System;

namespace SMC.Academico.Common.Exceptions
{
    /// <summary>
    /// Solução nova para quando fizer a seleção automática de vínculo após o login.
    /// Essa exception deve ser capturada no método Index da Home, na chamada da seleção automática de vínculo,
    /// uma vez que a seleção automática coloca os dados do usuário em um cookie que só começa a valer na próxima requisição.
    /// Desta maneira, captura-se essa exceção e redireciona o usuário para a action Index novamente (para recarregar o cookie)
    /// </summary>
    public class SGAAuthenticationSuccessRedirectException : Exception
    {
    }
}