import { environment } from '../../../environments/environment'
import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { map } from 'rxjs/operators';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class FrontService {
  constructor(private http: HttpClient) { }

  carregarConteudo(path: string): Observable<string> {
    const url = environment.frontUrl + path;
    return this.http.get(url, { responseType: 'text' })
      .pipe(
        map(response => (response as string))
      );
  }
}
