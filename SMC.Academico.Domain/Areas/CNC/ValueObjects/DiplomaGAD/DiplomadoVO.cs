using SMC.Framework.Mapper;
using System;

namespace SMC.Academico.Domain.Areas.CNC.ValueObjects
{
    public class DiplomadoVO : ISMCMappable
    {
        public string Id { get; set; }
        public string Nome { get; set; }
        public string NomeSocial { get; set; }
        public string Sexo { get; set; } //enum F, M
        public string Nacionalidade { get; set; }
        public NaturalidadeVO Naturalidade { get; set; }
        public string Cpf { get; set; }
        public RgVO Rg { get; set; }
        public OutroDocumentoIdentificacaoVO OutroDocumentoIdentificacao { get; set; }
        public DateTimeOffset? DataNascimento { get; set; }
    }
}
