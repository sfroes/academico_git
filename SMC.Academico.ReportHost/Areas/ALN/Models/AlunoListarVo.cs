using SMC.Framework.Mapper;
using SMC.Framework.UI.Mvc.DataAnnotations;
using SMC.Framework.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SMC.Academico.ReportHost.Areas.ALN.Models
{
    public class AlunoListarVO : ISMCMappable
    {
        #region [ Dados Pessoais ]

        public long Seq { get; set; }

        public long NumeroRegistroAcademico { get; set; }

        public string Nome { get; set; }

        [SMCCpf]
        public string Cpf { get; set; }

        public string NumeroPassaporte { get; set; }

        public string CpfOuPassaporte { get => string.IsNullOrEmpty(Cpf) ? NumeroPassaporte : SMCMask.ApplyMaskCPF(Cpf); }

        public DateTime DataNascimento { get; set; }

        public string DescricaoSituacaoMatricula { get; set; }

        public bool Falecido { get; set; }

        public List<string> Emails { get; set; }

        public string DescricaoEntidadeResponsavel { get; set; }

        public string DescricaoNivelEnsino { get; set; }

        [SMCValueEmpty("-")]
        public string DescricaoVinculo { get; set; }

        public string DadosVinculo { get; set; }

        public DateTime? DataInicioTermoIntercambio { get; set; }

        public DateTime? DataFimTermoIntercambio { get; set; }

        public string DescricaoInstituicaoExterna { get; set; }

        public bool ExigeParceriaIntercambioIngresso { get; set; }

        public bool ExigePeriodoIntercambioTermo { get; set; }

        public string DescricaoCursoOferta { get; set; }

        public string DescricaoLocalidade { get; set; }

        public string DescricaoTurno { get; set; }

        public DateTime DataAdmissao { get; set; }

        [SMCValueEmpty("-")]
        public DateTime? DataPrevisaoConclusao { get; set; }

        [SMCValueEmpty("-")]
        public DateTime? DataLimiteConclusao { get; set; }

        public string DescricaoFormacaoEspecifica { get; set; }

        [SMCValueEmpty("-")]
        public string TipoOrientacao { get; set; }

        public List<AlunoOrientacaoVO> Orientacoes { get; set; }

        public List<string> OrientacoesFormatadas
        {
            get
            {
                return Orientacoes?.Select(s => $"{s.NomeOrientador} - {SMCEnumHelper.GetDescription(s.TipoParticipacaoOrientacao)}").ToList();
            }
        }

        public List<string> FormacoesEspecificas { get; set; }

        public bool PermiteFormacaoEspecifica { get; set; }

        public string DescricaoParceriaIntercambio
        {
            get
            {
                var descricao = new StringBuilder();
                if (ExigeParceriaIntercambioIngresso && !string.IsNullOrEmpty(DescricaoInstituicaoExterna))
                    descricao.AppendLine($"<b>{"UIResource.Label_InstituicaoExterna"}</b><br />{DescricaoInstituicaoExterna}<br />");
                if (ExigePeriodoIntercambioTermo && DataInicioTermoIntercambio.HasValue && DataFimTermoIntercambio.HasValue)
                    descricao.AppendLine($"<b>{"UIResource.Label_PeriodoIntercambio"}</b><br />{DataInicioTermoIntercambio.Value.ToString("dd/MM/yyyy")} à {DataFimTermoIntercambio.Value.ToString("dd/MM/yyyy")}");
                return descricao.ToString();
            }
        }

        public bool PermiteCancelarMatricula { get; set; }

        [SMCValueEmpty("-")]
        public int? CodigoAlunoMigracao { get; set; }

        #endregion [ Dados Pessoais ]
    }
}