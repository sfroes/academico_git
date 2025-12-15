using SMC.Academico.Common.Areas.ORT.Enums;
using SMC.Academico.ServiceContract.Areas.DCT.Data;
using SMC.Framework;
using SMC.Framework.Mapper;
using SMC.Framework.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMC.Academico.ServiceContract.Areas.ORT.Data
{
    public class OrientacaoColaboradorData : ISMCMappable, ISMCSeq​​
    {
        public long Seq { get; set; }
               
        public long SeqOrientacao { get; set; }
               
        public long SeqColaborador { get; set; }
               
        public TipoParticipacaoOrientacao TipoParticipacaoOrientacao { get; set; }
               
        public DateTime? DataInicioOrientacao { get; set; }

        public DateTime? DataFimOrientacao { get; set; }
               
        public long SeqInstituicaoExterna { get; set; }

        public string DataFormatada { get; set; }

        public string DadosColaboradorCompleto { get; set; }

        public List<SMCDatasourceItem> InstituicoesExternas { get; set; }
                
        public string ColaboradorNameDescriptionField { get; set; }

        public string InstituicaoExternaNameDescriptionField { get; set; }

        public string Nome { get; set; }

        public bool VinculoAtivo { get; set; }
    }
}
