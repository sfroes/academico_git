using SMC.Academico.Common.Constants;
using SMC.Framework;
using System.ComponentModel;
using System.Runtime.Serialization;

namespace SMC.Academico.Common.Areas.CUR.Enums
{
    [DataContract(Namespace = NAMESPACES.MODEL)]
    public enum CurriculoCursoOfertaGrupoComponenteObrigatorio : short
    {
        [SMCIgnoreValue]
        [EnumMember]
        [Description("")]
        Nenhum = 0,

        /// <summary>
        /// Mapeado como verdadeiro no domínio
        /// </summary>
        [EnumMember]
        [Description("Obrigatórios")]
        Obrigatorios = 1,

        /// <summary>
        /// Mapeado como falso no domínio
        /// </summary>
        [EnumMember]
        Optativos = 2
    }
}