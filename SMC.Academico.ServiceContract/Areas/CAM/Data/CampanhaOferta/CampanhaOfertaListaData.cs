using SMC.Framework.Mapper;

namespace SMC.Academico.ServiceContract.Areas.CAM.Data
{
    public class CampanhaOfertaListaData : ISMCMappable
    {
        public long Seq { get; set; }

        public string TipoOferta { get; set; }

        public string TipoOfertaToken { get; set; }

        public string Oferta { get; set; }

        public int Vagas { get; set; }

        public int Ocupadas { get; set; }

        public bool PossuiVinculoProcessoSeletivo { get; set; }

        /// <summary>
        /// Vagas registradas na base de dados
        /// </summary>
        public int VagasBase { get; set; }

        /// <summary>
        /// Resultado da diferença das vagas inseridas com a quantidade de 
        /// vagas registrada na base
        /// </summary>
        public int VagasDiferenca { get; set; }

        /// <summary>
        /// RN_CAM_069 - Usar vagas disponíveis na campanha
        /// VagasDisponiveis = (VagasBase - Ocupadas)
        /// Subtração das vagas da oferta com a quantidade de ingressantes
        /// cuja situação é diferente de "Desistente" e "Cancelado (Prouni)
        /// </summary>
        public int Disponiveis { get; set; }

    }
}
