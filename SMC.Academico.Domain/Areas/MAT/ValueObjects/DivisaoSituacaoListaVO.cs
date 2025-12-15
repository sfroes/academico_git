using SMC.Academico.Common.Areas.MAT.Enums;

namespace SMC.Academico.Domain.Areas.MAT.ValueObjects
{
    public class DivisaoSituacaoListaVO
    {
        public string DescricaoDivisao { get; set; }

        public string SituacaoDivisao { get; set; }

        public MatriculaPertencePlanoEstudo SituacaoPlanoEstudo { get; set; }
    }   
}
