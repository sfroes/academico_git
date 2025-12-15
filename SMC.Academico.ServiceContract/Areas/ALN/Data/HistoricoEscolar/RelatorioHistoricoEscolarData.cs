using SMC.Academico.ServiceContract.Areas.APR.Data;
using SMC.DadosMestres.Common.Areas.PES.Enums;
using SMC.Framework.Mapper;
using SMC.Framework.Model;
using System;
using System.Collections.Generic;

namespace SMC.Academico.ServiceContract.Areas.ALN.Data
{
    public class RelatorioHistoricoEscolarData : ISMCMappable
    {
        public long Seq { get; set; }

        public string Unidade { get; set; }

        public string Curso { get; set; }

        public string DescricaoTipoVinculoAluno { get; set; }

        public string DescricaoPessoaAtuacao { get; set; }

        public string Nome { get; set; }

        public string Filiacao { get; set; }

        public DateTime DataNascimento { get; set; }

        public TipoNacionalidade Nacionalidade { get; set; }

        public string Naturalidade { get; set; }

        public string RG { get; set; }

        public string Exp_RG { get; set; }

        public string UF_RG { get; set; }

        public string CPF { get; set; }

        public long NumeroRegistroAcademico { get; set; }

        public string Passaporte { get; set; }

        public string FormaAdmissao { get; set; }

        public string Nivel { get; set; }

        public List<SMCDatasourceItem> FormacaoEspecifica { get; set; }

        public DateTime DataAdmissao { get; set; }

        public DateTime? DataCancelamento { get; set; }

        public List<SMCDatasourceItem> AdmissaoObservacao { get; set; }

        public bool EsconderAdmissaoObservacao { get; set; }

        [SMCMapForceFromTo]
        public List<TrabalhosData> TrabalhosAcademicos { get; set; }

        public bool EsconderTrabalhosAcademicos { get; set; }

        public bool EsconderAproveitamentoCreditos
        {
            get { return !(AproveitamentoCreditos != null && AproveitamentoCreditos.Count > 0); }
        }

        [SMCMapForceFromTo]
        public List<ComponentesCreditosData> AproveitamentoCreditos { get; set; }

        public bool EsconderComponentesConcluidos
        {
            get { return !(ComponentesConcluidos != null && ComponentesConcluidos.Count > 0); }
        }

        [SMCMapForceFromTo]
        public List<ComponentesCreditosData> ComponentesConcluidos { get; set; }

        public bool EsconderComponentesSemApuracao
        {
            get { return !(ComponentesSemApuracao != null && ComponentesSemApuracao.Count > 0); }
        }

        [SMCMapForceFromTo]
        public List<ComponentesCreditosData> ComponentesSemApuracao { get; set; }

        public bool EsconderComponentesExame
        {
            get { return !(ComponentesExame != null && ComponentesExame.Count > 0); }
        }

        [SMCMapForceFromTo]
        public List<ComponentesCreditosData> ComponentesExame { get; set; }
        
        [SMCMapForceFromTo]
        public List<TotaisComponentesCreditosData> TotaisComponentesConcluidos { get; set; }

        [SMCMapForceFromTo]
        public decimal MediaTotalNota { get; set; }

        [SMCMapForceFromTo]
        public List<TipoMobilidadeData> TiposMobilidade { get; set; }

        public bool EsconderTiposMobilidade { get; set; }

        public bool EsconderAssinatura { get; set; }

        public List<SMCDatasourceItem> Observacoes { get; set; }

        public bool ExibirCredito { get; set; }

        public long SeqInstituicaoEnsino { get; set; }

    }
}