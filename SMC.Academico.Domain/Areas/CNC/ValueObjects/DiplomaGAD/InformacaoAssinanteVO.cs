using SMC.Framework.Mapper;
using System;

namespace SMC.Academico.Domain.Areas.CNC.ValueObjects
{
    public class InformacaoAssinanteVO : ISMCMappable
    {
        public string Cargo { get; set; } //enum Reitor, Reitor em Exercício, Responsável pelo registro, Coordenador de Curso, Subcoordenador de Curso, Coordenador de Curso em exercício, Chefe da área de registro de diplomas, Chefe em exercício da área de registro de diplomas
        public string Cpf { get; set; }
        public string OutroCargo { get; set; }
    }
}
