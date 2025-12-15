using SMC.Framework.Mapper;

namespace SMC.Academico.ServiceContract.Areas.CAM.Data
{
    public class CampanhaCopiaConvocacaoProcessoSeletivoItemData : ISMCMappable
    {
        public bool Checked { get; set; }

        public long Seq { get; set; }

        public long SeqProcessoSeletivo { get; set; }

        public string Descricao { get; set; }

        public long SeqCicloLetivo { get; set; }

        public string DescricaoCicloLetivo { get; set; }
    }
}