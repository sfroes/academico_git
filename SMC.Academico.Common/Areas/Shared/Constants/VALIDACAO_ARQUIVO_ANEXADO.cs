namespace SMC.Academico.Common.Areas.Shared.Constants
{
    public class VALIDACAO_ARQUIVO_ANEXADO
    {
        /// <summary>
        /// Tamanho máximo de arquivo para anexar, em bytes
        /// </summary>
        public const int TAMANHO_MAXIMO_ARQUIVO_ANEXADO = 26214400; //25MB

        /// <summary>
        /// 10MB, em bytes
        /// </summary>
        public const int TAMANHO_MAXIMO_10MB = 10485760;

        /// <summary>
        /// 5MB, em bytes
        /// </summary>
        public const int TAMANHO_MAXIMO_5MB = 5242880;

        /// <summary>
        /// Extensões de arquivos permitidas ao anexar arquivos nas telas de parceria e termo de intercâmbio.
        /// </summary>
        public const string EXTENSOES_PERMITIDAS_PARCERIA_TERMO_INTERCAMBIO = ".doc,.docx,.xls,.xlsx,.jpg,.jpeg,.png,.pdf,.rar,.zip,.ps";

        /// <summary>
        /// Extensões de arquivos permitidas ao anexar arquivos na tela de entrega de documento digital.
        /// </summary>
        public const string EXTENSOES_PERMITIDAS_ENTREGA_DOCUMENTO_DIGITAL = ".doc,.docx,.xls,.xlsx,.jpg,.jpeg,.png,.pdf,.rar,.zip,.ps";

        /// <summary>
        /// Extensões de arquivos executáveis mais comuns (inclui dll, apesar de não ser executável)
        /// </summary>
        public const string EXTENSOES_ARQUIVOS_EXECUTAVEIS = ".cmd,.bat,.scr,.exe,.vbs,.ws,.dll,.com,.jar";
    }
}