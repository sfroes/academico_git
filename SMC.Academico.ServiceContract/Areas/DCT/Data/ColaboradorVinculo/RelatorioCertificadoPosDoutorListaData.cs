using SMC.Framework.Mapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMC.Academico.ServiceContract.Areas.DCT.Data
{
    public class RelatorioCertificadoPosDoutorListaData : ISMCMappable
    {
        public string DataEmissao { get; set; }

        public string NomeColaboradorPosDoutorando { get; set; }

        public string DataInicioVinculo { get; set; }

        public string DataFimVinculo { get; set; }

        public string NomeEntidadeResponsavel { get; set; }

        public string NomeProfessorResponsavel { get; set; }

        public string TituloPesquisa { get; set; }
    }
}
