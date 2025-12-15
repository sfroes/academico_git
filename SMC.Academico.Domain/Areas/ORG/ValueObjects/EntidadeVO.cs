using SMC.Academico.Domain.Areas.ORG.Models;
using SMC.Academico.Domain.Models;
using SMC.DadosMestres.Common.Areas.PES.Enums;
using SMC.Framework;
using SMC.Framework.Mapper;
using SMC.Framework.Model;
using SMC.Localidades.Common.Areas.LOC.Enums;
using System;
using System.Collections.Generic;

namespace SMC.Academico.Domain.Areas.ORG.ValueObjects
{
    public class EntidadeVO : ISMCMappable
    {
        #region Primitive Properties

        public long Seq { get; set; }

        public long? SeqProcessoUnidadeResponsavel { get; set; }

        public long? SeqInstituicaoEnsino { get; set; }

        public long SeqTipoEntidade { get; set; }

        public string Nome { get; set; }

        public string Sigla { get; set; }

        public string NomeReduzido { get; set; }

        public string NomeComplementar { get; set; }

        public long? SeqArquivoLogotipo { get; set; }

        public int? CodigoUnidadeSeo { get; set; }

        public long? SeqUnidadeResponsavelAgd { get; set; }

        public long? SeqUnidadeResponsavelGpi { get; set; }
        public long? SeqUnidadeResponsavelNotificacao { get; set; }

        public long? SeqUnidadeResponsavelFormulario { get; set; }

        #endregion Primitive Properties

        #region Visiblidade

        public bool LogotipoVisivel { get; set; }

        public bool? LogotipoObrigatorio { get; set; }

        public bool SiglaVisivel { get; set; }

        public bool? SiglaObrigatoria { get; set; }

        public bool UnidadeSeoVisivel { get; set; }

        public bool UnidadeAgdVisivel { get; set; }

        public bool UnidadeGpiVisivel { get; set; }
        public bool UnidadeNotificacaoVisivel { get; set; }
        public bool UnidadeFormularioVisivel { get; set; }

        public bool? UnidadeSeoObrigatorio { get; set; }

        public bool? UnidadeAgdObrigatorio { get; set; }

        public bool? UnidadeGpiObrigatorio { get; set; }

        public bool? UnidadeNotificacaoObrigatorio { get; set; }

        public bool? UnidadeFormularioObrigatorio { get; set; }

        public bool NomeReduzidoVisivel { get; set; }

        public bool? NomeReduzidoObrigatorio { get; set; }

        public bool NomeComplementarVisivel { get; set; }

        public bool? NomeComplementarObrigatorio { get; set; }

        public bool PermiteVinculoColaborador { get; set; }

        public bool HabilitaEnderecos
        {
            get { return this.TiposEnderecos.SMCCount() > 0; }
        }

        public bool HabilitaTelefones
        {
            get { return this.TiposTelefone.SMCCount() > 0; }
        }

        public bool HabilitaEnderecosEletronicos
        {
            get { return this.TiposEnderecoEletronico.SMCCount() > 0; }
        }

        public bool Ativa { get; set; }

        #endregion Visiblidade

        #region Propriedades Partial

        /// <summary>
        /// Sequencial da situação atual
        /// </summary>
        public long SeqSituacaoAtual { get; set; }

        /// <summary>
        /// Data inicial da situação atual
        /// </summary>
        public DateTime DataInicioSituacaoAtual { get; set; }

        /// <summary>
        /// Data final da situação atual
        /// </summary>
        public DateTime? DataFimSituacaoAtual { get; set; }

        #endregion Propriedades Partial

        #region Navigation Properties

        public SMCUploadFile ArquivoLogotipo { get; set; }

        public Entidade InstituicaoEnsino { get; set; }

        public TipoEntidade TipoEntidade { get; set; }

        public IList<Endereco> Enderecos { get; set; }

        public IList<EnderecoEletronico> EnderecosEletronicos { get; set; }

        public IList<Telefone> Telefones { get; set; }

        public IList<EntidadeClassificacao> Classificacoes { get; set; }

        #endregion Navigation Properties

        #region DataSources

        public List<TipoEndereco> TiposEnderecos { get; set; }

        public List<SMCDatasourceItem<string>> TiposEnderecoEletronico { get; set; }

        public List<SMCDatasourceItem<string>> TiposTelefone { get; set; }

        #endregion DataSources

        /// <summary>
        /// Classificações divididas por hierarquia
        /// </summary>
        public List<EntidadeHierarquiaClassificacaoVO> HierarquiasClassificacoes { get; set; }

        #region [Aba Ato Normativo - BI_ORG_002 - Atos Normativos da Entidade]

        public List<AtoNormativoVisualizarVO> AtoNormativo { get; set; }

        public bool AtivaAbaAtoNormativo { get; set; }

        public bool HabilitaColunaGrauAcademico { get; set; }

        #endregion [Aba Ato Normativo - BI_ORG_002 - Atos Normativos da Entidade]
    }
}