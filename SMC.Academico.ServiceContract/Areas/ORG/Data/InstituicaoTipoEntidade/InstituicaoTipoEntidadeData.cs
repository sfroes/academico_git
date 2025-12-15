using SMC.Framework;
using SMC.Framework.Mapper;
using System.Collections.Generic;

namespace SMC.Academico.ServiceContract.Areas.ORG.Data
{
    public class InstituicaoTipoEntidadeData : ISMCMappable, ISMCSeq​​
    {
        public long Seq { get; set; }

        public long SeqInstituicaoEnsino { get; set; }

        public long SeqTipoEntidade { get; set; }

        public bool LogotipoVisivel { get; set; }

        public bool UnidadeSeoVisivel { get; set; }

        public bool? LogotipoObrigatorio { get; set; }

        public bool? UnidadeSeoObrigatorio { get; set; }

        public bool SiglaVisivel { get; set; }

        public bool? SiglaObrigatoria { get; set; }

        public bool NomeReduzidoVisivel { get; set; }

        public bool UnidadeAgdVisivel { get; set; }

        public bool? NomeReduzidoObrigatorio { get; set; }

        public bool NomeComplementarVisivel { get; set; }

        public bool? NomeComplementarObrigatorio { get; set; }

        public bool? UnidadeAgdObrigatorio { get; set; }

        public bool UnidadeGpiVisivel { get; set; }

        public bool? UnidadeGpiObrigatorio { get; set; }

        public bool UnidadeNotificacaoVisivel { get; set; }

        public bool? UnidadeNotificacaoObrigatorio { get; set; }

        public bool UnidadeFormularioVisivel { get; set; }

        public bool? UnidadeFormularioObrigatorio { get; set; }

        public List<InstituicaoTipoEntidadeEnderecoData> TiposEndereco { get; set; }

        public List<InstituicaoTipoEntidadeTelefoneData> TiposTelefone { get; set; }

        public List<InstituicaoTipoEntidadeEnderecoEletronicoData> TiposEnderecoEletronico { get; set; }

        public List<InstituicaoTipoEntidadeSituacaoData> Situacoes { get; set; }
    }
}