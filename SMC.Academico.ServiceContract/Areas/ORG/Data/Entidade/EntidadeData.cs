using SMC.Academico.ServiceContract.Data;
using SMC.DadosMestres.Common.Areas.PES.Enums;
using SMC.Framework;
using SMC.Framework.Mapper;
using SMC.Framework.Model;
using SMC.Localidades.Common.Areas.LOC.Enums;
using System;
using System.Collections.Generic;

namespace SMC.Academico.ServiceContract.Areas.ORG.Data
{
    public class EntidadeData : ISMCMappable, ISMCSeq​​
    {
        public long? SeqInstituicaoEnsino { get; set; }

        public long? SeqProcessoUnidadeResponsavel { get; set; }

        public long SeqTipoEntidade { get; set; }

        [SMCMapProperty("TipoEntidade.Descricao")]
        public string DescricaoTipoEntidade { get; set; }

        public SMCUploadFile ArquivoLogotipo { get; set; }

        public long? SeqArquivoLogotipo { get; set; }

        public long Seq { get; set; }

        public string Nome { get; set; }

        public string Sigla { get; set; }

        public string NomeReduzido { get; set; }

        public string NomeComplementar { get; set; }

        public int? CodigoUnidadeSeo { get; set; }

        public List<EnderecoData> Enderecos { get; set; }

        public List<EnderecoEletronicoData> EnderecosEletronicos { get; set; }

        public List<TelefoneData> Telefones { get; set; }

        [SMCMapProperty("Hierarquias")]
        public List<EntidadeHierarquiaClassificacaoData> HierarquiasClassificacoes { get; set; }

        public long SeqSituacaoAtual { get; set; }

        public DateTime DataInicioSituacaoAtual { get; set; }

        public DateTime? DataFimSituacaoAtual { get; set; }

        public long? SeqUnidadeResponsavelAgd { get; set; }

        public long? SeqUnidadeResponsavelGpi { get; set; }

        public long? SeqUnidadeResponsavelNotificacao { get; set; }

        public long? SeqUnidadeResponsavelFormulario { get; set; }

        public long[] SeqClassificacoesEntidadeResponsavel { get; set; }

        #region Configurações de Visibilidade e Obrigatoriedade

        public bool LogotipoVisivel { get; set; }

        public bool SiglaVisivel { get; set; }

        public bool UnidadeSeoVisivel { get; set; }

        public bool UnidadeAgdVisivel { get; set; }

        public bool UnidadeGpiVisivel { get; set; }

        public bool UnidadeNotificacaoVisivel { get; set; }

        public bool UnidadeFormularioVisivel { get; set; }

        public bool NomeReduzidoVisivel { get; set; }

        public bool NomeComplementarVisivel { get; set; }

        public bool LogotipoObrigatorio { get; set; }

        public bool SiglaObrigatoria { get; set; }

        public bool UnidadeSeoObrigatorio { get; set; }

        public bool UnidadeAgdObrigatorio { get; set; }

        public bool UnidadeGpiObrigatorio { get; set; }
        public bool UnidadeNotificacaoObrigatorio { get; set; }

        public bool UnidadeFormularioObrigatorio { get; set; }

        public bool NomeReduzidoObrigatorio { get; set; }

        public bool NomeComplementarObrigatorio { get; set; }

        public bool HabilitaEnderecos { get; set; }

        public bool HabilitaTelefones { get; set; }

        public bool HabilitaEnderecosEletronicos { get; set; }

        #endregion Configurações de Visibilidade e Obrigatoriedade

        #region DataSources

        public List<TipoEndereco> TiposEnderecos { get; set; }

        public List<SMCDatasourceItem<string>> TiposTelefone { get; set; }

        public List<SMCDatasourceItem<string>> TiposEnderecoEletronico { get; set; }

        #endregion DataSources

        #region [Aba Ato Normativo - BI_ORG_002 - Atos Normativos da Entidade]

        public List<AtoNormativoVisualizarData> AtoNormativo { get; set; }

        public bool AtivaAbaAtoNormativo { get; set; }

        public bool HabilitaColunaGrauAcademico { get; set; }

        #endregion [Aba Ato Normativo - BI_ORG_002 - Atos Normativos da Entidade]
    }
}