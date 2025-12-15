using SMC.Academico.Common.Areas.PES.Enums;
using SMC.Framework.Mapper;
using System.Collections.Generic;

namespace SMC.Academico.Domain.Areas.MAT.ValueObjects
{
    public class RelatorioConsolidadoSituacaoVO : ISMCMappable
    {
        public long? SeqGrupoPrograma { get; set; }

        public string NomeGrupoPrograma { get; set; }

        public long? SeqCurso { get; set; }

        public string NomeCurso { get; set; }

        public long SeqNivelEnsino { get; set; }

        public string DescricaoNivelEnsino { get; set; }

        /*· "Ingressante" - Exibir as siglas de acordo com as possíveis situações do ingressante.*/

        /// <summary>
        /// AL - Aguardando liberação para matrícula
        /// </summary>
        public int? Ingressante_AL { get; set; }

        /// <summary>
        /// AM - Apto para matrícula
        /// </summary>
        public int? Ingressante_AM { get; set; }

        /// <summary>
        /// CA - Cancelado (Prouni)
        /// </summary>
        public int? Ingressante_CA { get; set; }

        /// <summary>
        ///  De - Desistente
        /// </summary>
        public int? Ingressante_DE { get; set; }

        /// <summary>
        ///  MA - Matriculado
        /// </summary>
        public int? Ingressante_MA { get; set; }

        /*· "Aluno" (Calouro/Veterano) - Exibir as siglas de acordo com os tipos de situação de matrícula.*/

        /// <summary>
        ///  AM - APTO_MATRICULA
        /// </summary>
        public int? Calouro_AM { get; set; }

        /// <summary>
        ///  CA - CANCELADO
        /// </summary>
        public int? Calouro_CA { get; set; }

        /// <summary>
        ///  FO - FORMADO
        /// </summary>
        public int? Calouro_FO { get; set; }

        /// <summary>
        ///    MA - MATRICULADO
        /// </summary>
        public int? Calouro_MA { get; set; }

        /// <summary>
        /// NM - NAO_MATRICULADO
        /// </summary>
        public int? Calouro_NM { get; set; }

        /// <summary>
        /// TR - TRANCADO
        /// </summary>
        public int? Calouro_TR { get; set; }

        /// <summary>
        /// TF - TRANSFERIDO
        /// </summary>
        public int? Calouro_TF { get; set; }

        /// <summary>
        ///  AM - APTO_MATRICULA
        /// </summary>
        public int? Veterano_AM { get; set; }

        /// <summary>
        ///  CA - CANCELADO
        /// </summary>
        public int? Veterano_CA { get; set; }

        /// <summary>
        ///  FO - FORMADO
        /// </summary>
        public int? Veterano_FO { get; set; }

        /// <summary>
        ///    MA - MATRICULADO
        /// </summary>
        public int? Veterano_MA { get; set; }

        /// <summary>
        /// NM - NAO_MATRICULADO
        /// </summary>
        public int? Veterano_NM { get; set; }

        /// <summary>
        /// TR - TRANCADO
        /// </summary>
        public int? Veterano_TR { get; set; }

        /// <summary>
        /// TF - TRANSFERIDO
        /// </summary>
        public int? Veterano_TF { get; set; }
    }
}