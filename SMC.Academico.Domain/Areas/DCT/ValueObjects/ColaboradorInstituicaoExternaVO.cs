using SMC.Framework;
using SMC.Framework.Mapper;

namespace SMC.Academico.Domain.Areas.DCT.ValueObjects
{
    public class ColaboradorInstituicaoExternaVO : ISMCSeq, ISMCMappable
    {
        public long Seq { get; set; }

        public long SeqColaborador { get; set; }

        public long SeqInstituicaoExterna { get; set; }

        [SMCMapProperty("InstituicaoExterna.Sigla")]
        public string Sigla { get; set; }

        [SMCMapProperty("InstituicaoExterna.Nome")]
        public string Nome { get; set; }

        public bool Ativo { get; set; }

        public string NomeSigla
        {
            get
            {
                return (!string.IsNullOrEmpty(Sigla)) ? $"{Sigla} - {Nome}" : Nome;
            }
        }
    }
}