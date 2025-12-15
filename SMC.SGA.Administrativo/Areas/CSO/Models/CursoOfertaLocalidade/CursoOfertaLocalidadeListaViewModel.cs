using SMC.Formularios.Common.Areas.TMP.Enums;
using SMC.Framework;
using SMC.Framework.UI.Mvc;
using System.Collections.Generic;

namespace SMC.SGA.Administrativo.Areas.CSO.Models
{
    public class CursoOfertaLocalidadeListaViewModel : SMCViewModelBase, ISMCSeq
    {
        public long Seq { get; set; }

        public long SeqCursoUnidade { get; set; }

        public long SeqTipoEntidade { get; set; }

        public string DescricaoGrauAcademico { get; set; }

        public string NomeCurso { get; set; }

        public string NomeUnidade { get; set; }

        public string Nome { get; set; }

        public bool Ativo { get; set; }

        public string Descricao
        {
            get
            {
                string retorno = string.Empty;

                if (!string.IsNullOrEmpty(Nome))
                    retorno = $"{Nome}";

                //if (!string.IsNullOrEmpty(NomeUnidade))
                //    if (retorno.Length > 0)
                //        retorno += $" - {NomeUnidade}";
                //    else
                //        retorno = NomeUnidade;

                if (Turnos.SMCCount() > 0)
                    if (retorno.Length > 0)
                        retorno += $" - {string.Join(" / ", Turnos ?? new List<CursoOfertaLocalidadeTurnoViewModel>())}";
                    else
                        retorno = string.Join(" / ", Turnos ?? new List<CursoOfertaLocalidadeTurnoViewModel>());

                return retorno;
            }
        }

        public List<CursoOfertaLocalidadeTurnoViewModel> Turnos { get; set; }
    }
}