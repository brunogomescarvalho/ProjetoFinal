import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable, take } from "rxjs";
import { INovoPedido, IPedido, IResposta, StatusPedido } from "src/app/app.model";

@Injectable()
export class PedidoService {
    private api: string = 'http://localhost:5170';

    constructor(private httpClient: HttpClient) { }

    
    public obterPedidos(): Observable<IPedido[]> {
        return this.httpClient.get<IPedido[]>(`${this.api}/api/pedido`)
    }

    public obterPorId(id:number): Observable<IPedido> {
        return this.httpClient.get<IPedido>(`${this.api}/api/pedido/${id}`).pipe(take(1));
    }

    public inserirPedido(pedido: INovoPedido): Observable<IResposta> {
        return this.httpClient.post<IResposta>(`${this.api}/api/pedido`, pedido)
    }

    public alterarStatus(id:number, status:StatusPedido): Observable<IResposta> {
        return this.httpClient.patch<IResposta>(`${this.api}/api/pedido/${id}/status?status=${status}`,status)
    }

    public excluirPedido(id: number): Observable<IResposta> {
        return this.httpClient.delete<IResposta>(`${this.api}/api/pedido/${id}`)
    }
}
