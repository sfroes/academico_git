using SMC.Framework.Mapper;
using SMC.Financeiro.Common.Areas.GRA.Enums;
using System;

namespace SMC.Academico.ServiceContract.Areas.ALN.Data
{
    public class IdentidadeEstudantilData : ISMCMappable
    {
        public long Seq { get; set; }

        public string Nome { get; set; }

        public string NomeSocial { get; set; }

        public int NumeroVia { get; set; }

        public long NumeroRegistroAcademico { get; set; }

        public DateTime DataValidade { get; set; }

        public string DescricaoCurso { get; set; }

        public string DescricaoUnidade { get; set; }

        public string Observacoes { get; set; }

        public string Codigo { get; set; }

        public string CodigoBarra { get; set; }

        public bool Frente { get; set; }

        public TipoVinculoAlunoFinanceiro TipoVinculoAlunoFinanceiro { get; set; }

        public DateTime? DataAdmissao { get; set; }
    }
}