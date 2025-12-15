using SMC.Academico.Common.Areas.APR.Enums;
using SMC.DadosMestres.Common.Areas.PES.Enums;
using SMC.Framework;
using SMC.Framework.Model;
using System.Collections.Generic;

namespace SMC.Academico.Domain.Areas.APR.ValueObjects
{
    public class MembroBancaExaminadoraVO
    {
        public List<SMCDatasourceItem> InstituicoesExternas { get; set; }

        public List<SMCDatasourceItem> TiposMembroBanca { get; set; }

        public long? Seq { get; set; }

        public long? SeqAplicacaoAvaliacao { get; set; }

        public long? SeqInstituicaoExterna { get; set; }

        public long? SeqColaborador { get; set; }

        public string Nome { get; set; }

        public TipoMembroBanca? TipoMembroBanca { get; set; }

        public string DescricaoTipoMembro { get => TipoMembroBanca.SMCGetDescription(); }

        public string Instituicao { get; set; }

        public bool? Participou { get; set; }

        public Sexo? Sexo { get; set; }

        public string NomeColaborador { get; set; }

        public string NomeInstituicaoExterna { get; set; }

        public string DescricaoMembro { get; set; }

        public bool AvaliacaoPossuiApuracao { get; set; }

        public string ComplementoInstituicao { get; set; }

        public bool? Presidiu { get; set; }
    }
}