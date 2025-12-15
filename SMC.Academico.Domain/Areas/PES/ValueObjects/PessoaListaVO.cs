using SMC.DadosMestres.Common.Areas.PES.Enums;
using SMC.Framework;
using SMC.Framework.Mapper;
using System;
using System.Collections.Generic;

namespace SMC.Academico.Domain.Areas.PES.ValueObjects
{
    public class PessoaListaVO : ISMCMappable, ISMCSeq
    {
        public long Seq { get; set; }

        public TipoNacionalidade TipoNacionalidade { get; set; }

        public int CodigoPaisNacionalidade { get; set; }

        [SMCMapMethod("RecuperarNomeAtual")]
        public string Nome { get; set; }

        [SMCMapProperty("Pessoa.Cpf")]
        public string Cpf { get; set; }

        [SMCMapProperty("Pessoa.NumeroPassaporte")]
        public string NumeroPassaporte { get; set; }

        public DateTime? DataNascimento { get; set; }

        [SMCMapMethod("RecuperarSexoAtual")]
        public Sexo Sexo { get; set; }

        public bool Selecionado { get; set; }

        public List<PessoaFiliacaoVO> Filiacao { get; set; }

        public List<PessoaAtuacaoVO> Atuacoes { get; set; }
    }
}