import { Injectable } from '@angular/core';
import { ActivatedRouteSnapshot, Resolve, RouterStateSnapshot } from '@angular/router';
import { Observable } from 'rxjs';
import { LancamentoNotaService } from '../../services/lancamento-nota.service';
import { LancamentoNotaModel } from '../models/lancamento-nota.model';
import { SmcLoadService } from 'projects/shared/services/load/smc-load.service';

@Injectable({
  providedIn: 'root',
})
export class LancamentoNotaResolverService implements Resolve<LancamentoNotaModel> {
  constructor(private service: LancamentoNotaService, private loadingService: SmcLoadService) {}

  resolve(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): Observable<LancamentoNotaModel> {
    this.loadingService.startLoading();
    const seqOrigemAvaliacao = route.queryParamMap.get('seqOrigemAvaliacao');
    return this.service.buscarLancamento(seqOrigemAvaliacao);
  }
}
