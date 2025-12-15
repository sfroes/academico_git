using SMC.Framework.Mapper;
using System;

namespace SMC.Academico.ServiceContract.Areas.CSO.Data
{
    public class TitulacaoCursoFormacaoEspecificaData : ISMCMappable
    {
        public long Seq { get; set; }

        public long SeqCursoFormacaoEspecifica { get; set; }

        [SMCMapProperty("Titulacao.DescricaoMasculino")]
        public string DescricaoMasculino { get; set; }

        [SMCMapProperty("Titulacao.DescricaoFeminino")]
        public string DescricaoFeminino { get; set; }

        public long SeqTitulacao { get; set; }

        public bool Ativo { get; set; }

        public string DescricaoTitulacao
        {
            get
            {
                var retorno = string.Empty;

                if (!string.IsNullOrEmpty(DescricaoMasculino))
                    retorno += DescricaoMasculino;
                if (!string.IsNullOrEmpty(DescricaoFeminino))
                    retorno += string.IsNullOrEmpty(DescricaoMasculino) ? DescricaoFeminino : $" / {DescricaoFeminino}";

                return retorno;
            }
        }
    }
}