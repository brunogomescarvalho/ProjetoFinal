import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable, take } from "rxjs";
import { IProduto, IResposta } from "src/app/app.model";
@Injectable()


export class ProdutoService{
    
    constructor(private httpClient: HttpClient){}

    private api: string  = 'http://localhost:5170';
   
    public obterProdutos(): Observable<IProduto[]> {
        return this.httpClient.get<IProduto[]>(`${this.api}/api/produto`)
    }

    public inserirProduto(produto: IProduto): Observable<IResposta> {
        return this.httpClient.post<IResposta>(`${this.api}/api/produto`, produto)
    }

    public editarProduto(produto: IProduto): Observable<IResposta> {
        return this.httpClient.patch<IResposta>(`${this.api}/api/produto/${produto.id}`,produto ) 
    }

    public editarAtivo(id: number): Observable<IResposta> {
        return this.httpClient.patch<IResposta>(`${this.api}/api/produto/${id}/ativo`,id)
    }

    public editarQuantidade(id:number, quantidade:number): Observable<IResposta> {
        return this.httpClient.patch<IResposta>(`${this.api}/api/produto/${id}/estoque?quantidade=${quantidade}`,quantidade)
    }

    public obterPorId(id: number): Observable<IProduto> {        
        return this.httpClient.get<IProduto>(`${this.api}/api/produto/${id}`).pipe(take(1));
    }

     public obterProdutosAtivos(): Observable<IProduto[]> {
        return this.httpClient.get<IProduto[]>(`${this.api}/api/produto/ativos`)
    }

}