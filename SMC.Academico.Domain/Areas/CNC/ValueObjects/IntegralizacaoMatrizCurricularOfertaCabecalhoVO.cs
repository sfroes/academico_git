using SMC.Academico.Common.Areas.ALN.Enums;
using SMC.Financeiro.Common.Areas.GRA.Enums;
using SMC.Framework.Mapper;
using System.Collections.Generic;

namespace SMC.Academico.Domain.Areas.CNC.ValueObjects
{
    public class IntegralizacaoMatrizCurricularOfertaCabecalhoVO : ISMCMappable
    {
        public long? SeqAluno { get; set; }

        public long? SeqIngressante { get; set; }

        public long RA { get; set; }

        public int? CodigoMigracao { get; set; }

        public string Nome
        {
            get
            {
                //Regra RN_PES_021 e RN_PES_023 de acordo com o sistema que chamou a consulta
                if (string.IsNullOrEmpty(NomeSocial))
                    return NomeRegistro;

                if (VisaoAluno)
                    return NomeSocial;
                else
                    return $"{NomeRegistro} ({NomeSocial})";
            }
        }

        public string NomeRegistro { get; set; }

        public string NomeSocial { get; set; }

        public bool VisaoAluno { get; set; }

        public string Situacao { get; set; }

        public string Vinculo { get; set; }

        public string DescricaoFormaIngresso { get; set; }

        public bool ExibirOfertaMatriz { get; set; }

        public string OfertaMatriz { get; set; }

        public string SituacaoOfertaMatriz { get; set; }

        public string OfertaCurso { get; set; }

        public string Localidade { get; set; }

        public string Turno { get; set; }

        public SituacaoIngressante SituacaoIngressante { get; set; }

        public string OfertaCampanha { get; set; }

        public bool ExibirDisciplinaIsolada { get; set; }

        public string VinculoDisciplinaIsolada { get; set; }

        public long SeqCurso { get; set; }

        public List<IntegralizacaoTipoComponenteCurricularVO> TiposComponentesCurriculares { get; set; }

        public CalculoConclusaoCursoAlunoVO DadosConclusaoCursoAluno { get; set; }
    }
}
