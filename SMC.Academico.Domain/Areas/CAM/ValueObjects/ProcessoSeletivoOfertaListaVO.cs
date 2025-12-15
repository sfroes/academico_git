using SMC.Framework.Mapper;

namespace SMC.Academico.Domain.Areas.CAM.ValueObjects
{
    public class ProcessoSeletivoOfertaListaVO : ISMCMappable
    {
        public long Seq { get; set; }

        public long SeqCampanhaOferta { get; set; }

        public string TipoOfertaToken { get; set; }

        public string TipoOferta { get; set; }

        public string Oferta { get; set; }

        public int Vagas { get; set; }

        public int? VagasBase { get; set; }

        public int Ocupadas { get; set; }

        public bool PossuiVinculoConvocacao { get; set; }

        /// <summary>
        /// Resultado da diferença das vagas inseridas com a quantidade de 
        /// vagas registrada na base
        /// </summary>
        public int VagasDiferenca { get; set; }
    }
}
