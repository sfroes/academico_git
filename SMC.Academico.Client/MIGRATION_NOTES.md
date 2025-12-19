# Notas de Migração para Angular 19

## Correções Aplicadas

### 1. Dependências
- **ngx-contextmenu**: 20.0.0 → 19.0.0 (compatibilidade com Angular 19)
- **angular-calendar**: 0.32.0 → 0.31.1 (compatibilidade com Angular 19)
- Instalação com `--legacy-peer-deps`

### 2. PrimeNG
- Removido import de `node_modules/primeng/resources/primeng.min.css` do angular.json
- Este arquivo não existe mais no PrimeNG v19

### 3. FormArray Tipagem (Angular 19)
- `apuracao-frequencia.service.ts`: Adicionado tipo `FormArray<FormGroup>` ao método `criarApuracores`
- `lancamento-nota-map.service.ts`: Adicionado tipo `FormArray<FormGroup>` aos métodos `mapearFormAlunos` e `mapearFormApuracoes`

### 4. Componentes Shared
- **SmcTableModule**: Importa `PoTableModule` diretamente de `@po-ui/ng-components`
- **SmcPagerModule**: Importa `PoFieldModule` diretamente de `@po-ui/ng-components`
- **SmcCalendarCustomFormatter**: Construtor agora recebe `DateAdapter` via `inject()`

### 5. PoUiModule
Atualizado em todos os 4 projetos para incluir:
- `PoTableModule` (imports e exports)
- `PoTooltipModule` (imports e exports)
- `PoLoadingModule` (já exportava, agora também importa)

Arquivos atualizados:
- `projects/shared/modules/po-ui.module.ts`
- `projects/smc-sga-professor/src/app/shared/po-ui.module.ts`
- `projects/smc-sga-aluno/src/app/shared/po-ui.module.ts`
- `projects/smc-sga-administrativo/src/app/shared/po-ui.module.ts`

### 6. Patches Removidos
Removidos patches incompatíveis do patch-package:
- `@perfectmemory+ngx-contextmenu+20.0.0.patch`
- `angular-calendar+0.32.0.patch`

## Status por Projeto

### ✅ Administrativo
Compilando com sucesso

### ✅ Professor
Compilando com sucesso após adicionar `SmcTableModule` ao `SharedModule`
- **Solução**: O `SharedModule` do professor não estava exportando o `SmcTableModule`
- Arquivo corrigido: `projects/smc-sga-professor/src/app/shared/shared.module.ts`

### 🔄 Aluno
Em teste...

## Correções Específicas por Projeto

### Professor
Adicionado ao `projects/smc-sga-professor/src/app/shared/shared.module.ts`:
```typescript
import { SmcTableModule } from 'projects/shared/components/smc-table/smc-table.module';

// ... no @NgModule
imports: [..., SmcTableModule],
exports: [..., SmcTableModule]
```

## Próximos Passos

1. ✅ Projeto Administrativo - Funcionando
2. ✅ Projeto Professor - Funcionando
3. 🔄 Projeto Aluno - Em teste
4. Testar funcionalidades em runtime
5. Verificar se há warnings que precisam ser corrigidos
