using SMC.Framework.Mapper;

namespace SMC.Academico.Domain.Areas.CAM.ValueObjects
{
    public class CampanhaCopiaConvocacaoProcessoSeletivoItemVO : ISMCMappable
    {
        public bool Checked { get; set; }

        public long Seq { get; set; }

        public long SeqProcessoSeletivo { get; set; }

        public string Descricao { get; set; }

        public long SeqCicloLetivo { get; set; }

        public string DescricaoCicloLetivo { get; set; }
    }
}