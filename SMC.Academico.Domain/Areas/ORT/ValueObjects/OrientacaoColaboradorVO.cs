using SMC.Academico.Common.Areas.ORT.Enums;
using SMC.Academico.Domain.Areas.DCT.Models;
using SMC.Framework.Mapper;
using SMC.Framework.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMC.Academico.Domain.Areas.ORT.ValueObjects
{
    public class OrientacaoColaboradorVO : ISMCMappable
    {
        public long Seq { get; set; }

        public long SeqOrientacao { get; set; }

        public long SeqColaborador { get; set; }

        public TipoParticipacaoOrientacao TipoParticipacaoOrientacao { get; set; }

        public DateTime? DataInicioOrientacao { get; set; }

        public DateTime? DataFimOrientacao { get; set; }

        public long SeqInstituicaoExterna { get; set; }

        public Colaborador Colaborador { get; set; }

        public string Nome { get; set; }

        public string NomeSocial { get; set; }

        public string DataFormatada { get; set; }

        public string DadosColaboradorCompleto { get; set; }

        public List<SMCDatasourceItem> InstituicoesExternas { get; set; }

        [SMCMapProperty("Colaborador.DadosPessoais.Nome")]
        public string ColaboradorNameDescriptionField { get; set; }

        public string InstituicaoExternaNameDescriptionField { get; set; }

        public bool VinculoAtivo { get; set; }
    }
}
