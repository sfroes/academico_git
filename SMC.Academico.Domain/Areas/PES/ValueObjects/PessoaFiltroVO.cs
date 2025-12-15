using SMC.Academico.Common.Areas.PES.Enums;
using SMC.DadosMestres.Common.Areas.PES.Enums;
using SMC.Framework.Mapper;
using SMC.Framework.Model;
using System;

namespace SMC.Academico.Domain.Areas.PES.ValueObjects
{
    public class PessoaFiltroVO : SMCPagerFilterData, ISMCMappable
    {
        public long? Seq { get; set; }

        public TipoNacionalidade? TipoNacionalidade { get; set; }

        public string Cpf { get; set; }

        public string NumeroPassaporte { get; set; }

        public string Nome { get; set; }

        public DateTime? DataNascimento { get; set; }

        public bool? DadosPessoaisCadastrados { get; set; }

        public long? SeqUsuarioSAS { get; set; }

        public TipoAtuacao? TipoAtuacao { get; set; }
    }
}