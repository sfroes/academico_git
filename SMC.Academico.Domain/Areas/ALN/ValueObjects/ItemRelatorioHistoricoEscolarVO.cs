using SMC.Academico.Domain.Areas.APR.ValueObjects;
using SMC.DadosMestres.Common.Areas.PES.Enums;
using SMC.Framework.Mapper;
using SMC.Framework.Model;
using System;
using System.Collections.Generic;

namespace SMC.Academico.Domain.Areas.ALN.ValueObjects
{
    public class ItemRelatorioHistoricoEscolarVO : ISMCMappable
    {
        #region [ Construtor ]

        public ItemRelatorioHistoricoEscolarVO()
        {
            TrabalhosAcademicos = new List<TrabalhosVO>();
            TiposMobilidade = new List<TipoMobilidadeVO>();
            Observacoes = new List<SMCDatasourceItem>();
            AdmissaoObservacao = new List<SMCDatasourceItem>();
            AproveitamentoCreditos = new List<ComponentesCreditosVO>();
            ComponentesConcluidos = new List<ComponentesCreditosVO>();
            ComponentesSemApuracao = new List<ComponentesCreditosVO>();
            ComponentesExame = new List<ComponentesCreditosVO>();
            TotaisComponentesConcluidos = new List<TotaisComponentesCreditosVO>();
        }

        #endregion [ Construtor ]

        #region [ Identificação do curso ]

        public string Unidade { get; set; }

        public string Curso { get; set; }      
        
        public long SeqNivelEnsino { get; set; }

        #endregion [ Identificação do curso ]

        #region [ Identificação do aluno ]

        public long Seq { get; set; }

        public string Nome { get; set; }

        public string Filiacao
        {
            get
            {
                if (!string.IsNullOrEmpty(Pai) && !string.IsNullOrEmpty(Mae))
                {
                    return Pai + " e " + Mae;
                }
                else if (!string.IsNullOrEmpty(Mae))
                {
                    return Mae;
                }
                return Pai;
            }
        }

        public DateTime? DataNascimento { get; set; }

        public TipoNacionalidade Nacionalidade { get; set; }

        //Não vai para o data.
        public int CodigoPaisNacionalidade { get; set; }

        public string Naturalidade { get; set; }

        //Não vai para o data.
        public int? CodigoCidadeNaturalidade { get; set; }

        //Não vai para o data.
        public string UfNaturalidade { get; set; }

        public string RG { get; set; }

        public string Exp_RG { get; set; }

        public string UF_RG { get; set; }

        public string CPF { get; set; }

        public string Passaporte { get; set; }

        //Não vai para o data.
        public string Pai { get; set; }

        //Não vai para o data.
        public string Mae { get; set; }

        public long NumeroRegistroAcademico { get; set; }

        public string DescricaoTipoVinculoAluno { get; set; }

        public string DescricaoPessoaAtuacao { get; set; }

        #endregion [ Identificação do aluno ]

        #region [ Bancas ]

        public List<TrabalhosVO> TrabalhosAcademicos { get; set; }

        public bool EsconderTrabalhosAcademicos { get; set; }

        #endregion [ Bancas ]

        #region [ Admissão ]

        //Não vai para o data.
        public long SeqAlunoHistoricoAtual { get; set; }

        public string FormaAdmissao { get; set; }

        public string Nivel { get; set; }

        public List<SMCDatasourceItem> FormacaoEspecifica { get; set; }

        public DateTime DataAdmissao { get; set; }

        public DateTime? DataCancelamento { get; set; }

        public List<SMCDatasourceItem> AdmissaoObservacao { get; set; }

        public bool EsconderAdmissaoObservacao { get; set; }

        #endregion [ Admissão ]

        #region [ Aproveitamento de créditos ]

        public List<ComponentesCreditosVO> AproveitamentoCreditos { get; set; }

        #endregion [ Aproveitamento de créditos ]

        #region [ Componentes concluídos ]

        public List<ComponentesCreditosVO> ComponentesConcluidos { get; set; }

        #endregion [ Componentes concluídos ]

        #region [ Componentes sem apuração ]

        public List<ComponentesCreditosVO> ComponentesSemApuracao { get; set; }

        #endregion [ Componentes sem apuração ]

        #region [ Componentes de exame ]

        public List<ComponentesCreditosVO> ComponentesExame { get; set; }

        #endregion [ Componentes de exame ]

        #region [ Totais Componentes concluídos ]

        public List<TotaisComponentesCreditosVO> TotaisComponentesConcluidos { get; set; }

        public decimal MediaTotalNota { get; set; }

        #endregion [ Totais Componentes concluídos ]

        #region [ Tipo de mobilidade ]

        public List<TipoMobilidadeVO> TiposMobilidade { get; set; }

        public bool EsconderTiposMobilidade { get; set; }

        #endregion [ Tipo de mobilidade ]

        #region [ Observações ]

        public List<SMCDatasourceItem> Observacoes { get; set; }

        #endregion [ Observações ]

        public bool EsconderAssinatura { get; set; }

        public bool ExibirCredito { get; set; }

        public long SeqInstituicaoEnsino { get; set; }


    }
}