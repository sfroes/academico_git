using SMC.Academico.Common.Areas.ALN.Enums;
using SMC.Academico.Common.Areas.PES.Enums;
using SMC.Academico.Domain.Areas.ALN.Models;
using SMC.Framework.Mapper;
using System.Collections.Generic;

namespace SMC.Academico.Domain.Areas.ALN.ValueObjects
{
    public class PessoaAtuacaoTermoIntercambioListaVO : ISMCMappable
    {
        public long Seq { get; set; }

        public long SeqPessoaAtuacao { get; set; }

        public long SeqTermoIntercambio { get; set; }

        public long? SeqEntidadeResponsavel { get; set; }
        
        public long? SeqNivelEnsino { get; set; }

        public long? SeqTipoVinculo { get; set; }

        public string Nome { get; set; }
        
        public string NomeSocial { get; set; }
        
        public TipoAtuacao TipoAtuacao { get; set; }

        public string TipoVinculoAlunoDescricao { get; set; }

        public string NomeEntidadeResponsavel { get; set; }

        public string DescricaoNivelEnsino { get; set; }
        
        public string DescricaoTermoIntercambio { get; set; }

        public string DescricaoTipoTermo { get; set; }

        public string InstituicaoExternaNome { get; set; }

        public TipoMobilidade TipoMobilidade { get; set; }
        
        public IList<PeriodoIntercambio> PeriodosBanco { get; set; }

        public List<PeriodoIntercambioVO> Periodos { get; set; }
    }
}