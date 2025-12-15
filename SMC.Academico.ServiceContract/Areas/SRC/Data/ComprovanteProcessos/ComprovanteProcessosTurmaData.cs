using SMC.Academico.Common.Areas.MAT.Enums;
using SMC.Framework;
using SMC.Framework.Mapper;

namespace SMC.Academico.ServiceContract.Areas.SRC.Data
{
    public class ComprovanteProcessosTurmaData : ISMCMappable
    {
        public long Seq { get; set; }

        public string Descricao { get; set; }

        public long SeqDivisao { get; set; }

        public string DescricaoDivisao { get; set; }

        public string Situacao { get; set; }

        public MotivoSituacaoMatricula? Motivo { get; set; }

        public string SituacaoMotivo
        {
            get
            {
                if (Motivo == null || string.IsNullOrEmpty(Motivo.SMCGetDescription()))
                    return Situacao;

                return $"{Situacao} - {Motivo.SMCGetDescription()}";
            }
        }        
    }
}
