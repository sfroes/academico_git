using SMC.Academico.Domain.Areas.ALN.ValueObjects;
using SMC.Academico.Domain.Areas.DCT.ValueObjects;
using SMC.Financeiro.Common.Areas.GRA.Enums;
using SMC.Framework.Mapper;
using System;
using System.Collections.Generic;

namespace SMC.Academico.Domain.Areas.PES.ValueObjects
{
    public class IdentidadeEstudantilVO : ISMCMappable
    {
        public long? SeqAluno { get; set; }

        public long? NumeroRegistroAcademico { get; set; }

        public long? SeqColaborador { get; set; }

        public long SeqPessoa { get; set; }

        public long SeqCicloLetivo { get; set; }

        public long? SeqPrograma { get; set; }

        public long NumeroVia { get; set; }

        public string Nome { get; set; }

        public int? CodigoAlunoMigracao { get; set; }

        /// <summary>
        /// "{CodigoAlunoMigracao}-{DV}"
        /// </summary>
        public string RegistroDV { get; set; }

        public DateTime? DataValidade { get; set; }

        public string DescricaoCurso { get; set; }

        public string DescricaoTipoEntidadeResponsavel { get; set; }

        public string DescricaoEntidadeResponsavel { get; set; }

        public string DescricaoUnidade { get; set; }

        public string Observacoes { get; set; }

        public string Codigo { get; set; }

        public TipoVinculoAlunoFinanceiro TipoVinculoAlunoFinanceiro { get; set; }

        public bool Frente { get; set; }

        public long SeqInstituicaoEnsino { get; set; }

        public List<PlanoEstudoItemVO> PlanoEstudoItens { get; set; }

        public DateTime? DataPrevistaConclusaoIngressante { get; set; }

        public ColaboradorVinculoIdentidadeAcademicaVO ColaboradorVinculo { get; set; }

        public DateTime? DataAdmissao { get; set; }
    }
}