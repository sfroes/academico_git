using SMC.Academico.Domain.Areas.APR.ValueObjects;
using SMC.Academico.Domain.Areas.CUR.ValueObjects;
using System.Collections.Generic;

namespace SMC.Academico.Domain.Areas.CNC.ValueObjects
{
    public class IntegralizacaoListasGrupoVO
    {
        public List<IntegralizacaoMatrizGrupoVO> ListaGrupos { get; set; }

        public List<IntegralizacaoMatrizCurricularOfertaVO> ListaDados { get; set; }

        public List<ComponentesCreditosVO> ListaHistoricoEscolar { get; set; }

        public List<ComponentesCreditosVO> ListaPlanoEstudoAtual { get; set; }

        public List<ComponentesCreditosVO> ListaPlanoEstudoAntigo { get; set; }

        public List<GrupoCurricularInformacaoVO> ListaBeneficioCondicao { get; set; }

        public List<GrupoCurricularInformacaoFormacaoVO> ListaFormacaoEspecifica { get; set; }

        public List<IntegralizacaoMatrizGrupoVO> ListaGrupoDivisao { get; set; }

        public List<long> ListaBeneficioPessoaAtuacao { get; set; }

        public List<long> ListaCondicoesPessoaAtuacao { get; set; }
    }
}