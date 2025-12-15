using SMC.Academico.Common.Areas.APR.Enums;
using SMC.Academico.ServiceContract.Areas.APR.Data;
using SMC.Framework;
using SMC.Framework.Mapper;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SMC.Academico.ReportHost.Areas.TUR.Models
{
    public class BancaAgendadaVO : ISMCMappable
    {
        public long SeqAplicacaoAvaliacao { get; set; }
        public long SeqEntidadeCurso { get; set; }
        public string NomeCursoLocalidade { get; set; }
        public string DescricaoTurno { get; set; }
        public string NomeAluno { get; set; }
        public string DescricaoTipoTrabalho { get; set; }
        public long SeqTipoEvento { get; set; }
        public string DescricaoTipoEvento { get; set; }
        public string DescricaoLocal { get; set; }
        public DateTime DataInicioAplicacaoAvaliacao { get; set; }
        public string DescricaoTitulo { get; set; }
        public string DescricaoEscalaApuracaoItem { get; set; }
        public DateTime? DataCancelamento { get; set; }
        public string DescricaoCancelamento { get; set; }
        public decimal? Nota { get; set; }
        public string NomeEntidadeResponsavel { get; set; }
        public string DescricaoComponenteCurricular { get; set; }
        public string PotencialPropriedadeIntelectual { get; set; }
        public OrdenacaoBancasAgendadasRelatorio OrdenacaoBancasAgendadasRelatorio { get; set; }

        #region Navigation Properties

        public IList<BancasAgendadasAreaConhecimentoData> AreasConhecimento { get; set; }
        public IList<MembroBancaExaminadoraData> MembrosBancaExaminadora { get; set; }

        #endregion Navigation Properties

        #region Readonly

        public string DataCancelamentoString
        {
            get
            {
                if (DataCancelamento.HasValue)
                    return DataCancelamento.Value.ToShortDateString();

                return string.Empty;
            }
        }

        /// <summary>
        /// Membro da banca no formato Nome - Tipo de Membro - (Instituição - Complemento)
        /// </summary>
        public string MembrosBancaString
        {
            get
            {
                var membrosBancaFormatado = MembrosBancaExaminadora
                    .Where(c => c.TipoMembroBanca != TipoMembroBanca.Orientador)
                    .Select(c =>
                    {
                        //var tipoMembroBanca = c.TipoMembroBanca.HasValue &&
                        //                      (c.TipoMembroBanca == TipoMembroBanca.Orientador || c.TipoMembroBanca == TipoMembroBanca.Coorientador) ?
                        //                     $"({c.TipoMembroBanca.SMCGetDescription()})" : "";

                        var tipoMembroBanca = $"({c.TipoMembroBanca.SMCGetDescription()})";

                        // Utiliza o nome da instituição externa do colaborador do SGA ou da instituição externa do membro da banca
                        var instituicao = string.IsNullOrEmpty(c.Instituicao) ? c.NomeInstituicaoExterna : c.Instituicao;
                        instituicao = string.IsNullOrEmpty(instituicao) ? "" :
                            string.IsNullOrEmpty(c.ComplementoInstituicao) ?
                                $"({instituicao})" :
                                $"({instituicao} - {c.ComplementoInstituicao})";
                        return $"{c.Nome} {tipoMembroBanca} {instituicao}";
                    });
                return string.Join(Environment.NewLine, membrosBancaFormatado);
            }
        }

        public string NomeOrientador
        {
            get
            {
                var orientador = MembrosBancaExaminadora
                   .FirstOrDefault(c => c.TipoMembroBanca == TipoMembroBanca.Orientador);

                return orientador?.Nome;
            }
        }

        public string AreasConhecimentoString
        {
            get
            {
                var areaConhecimentoFormatado = AreasConhecimento
                    .Select(c =>
                    {
                        return c.AreaConhecimentoDescricao;
                    });
                return string.Join(System.Environment.NewLine, areaConhecimentoFormatado);
            }
        }

        #endregion Readonly
    }
}