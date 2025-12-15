/**
 * @usedBy SmcTreeViewComponent
 *
 * @description
 *
 * Interface para definição dos itens base do componente `smc-tree-view`.
 */
export interface SmcTreeViewBaseModel {
  seq: string,
  seqPai?: string,
  descricao: string,
  itemFolha: boolean,
  selecionavel: boolean
}
