using SMC.Framework.UI.Mvc;

namespace SMC.SGA.Administrativo.Areas.SRC.Models
{
    public class UsuarioSelecaoViewModel : SMCViewModelBase
    {
        public long Seq { get; set; }

        public string Nome { get; set; }

        public int? CodigoPessoa { get; set; }

        public string NomeFormatado
        {
            get
            {
                var nomeFormatado = string.Empty;

                if (CodigoPessoa.HasValue)
                    nomeFormatado = $"{CodigoPessoa} - {Nome}";
                else
                    nomeFormatado = Nome;

                return nomeFormatado;
            }
        }
    }
}