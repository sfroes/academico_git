using SMC.Academico.Common.Areas.ALN.Enums;
using SMC.Framework.Mapper;
using SMC.Framework.Model;
using System;
using System.Collections.Generic;

namespace SMC.Academico.ServiceContract.Areas.ALN.Data
{
    public class PessoaAtuacaoTermoIntercambioFiltroData : SMCPagerFilterData, ISMCMappable
    {
        public long? SeqParceriaIntercambio { get; set; }

        public string NameDescriptionParceriraIntercabioTipoTermo { get; set; }

        public long? SeqInstituicaoEnsino { get; set; }

        public ParceriaIntercambioInstituicaoExternaData SeqInstituicaoExterna { get; set; }

        public string Nome { get; set; }

        public List<long> SeqEntidadesResponsaveis { get; set; }

        public long? SeqNivelEnsino { get; set; }

        public long? SeqTipoVinculo { get; set; }

        public long? SeqTipoTermoIntercambio { get; set; }

        public TipoMobilidade? TipoMobilidade { get; set; }
        
        public DateTime? DataInicio { get; set; }
        
        public DateTime? DataFim { get; set; }
    }
}