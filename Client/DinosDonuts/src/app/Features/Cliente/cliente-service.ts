import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable, take } from "rxjs";
import { ICliente, IResposta } from "src/app/app.model";

@Injectable()
export class ClienteService {
    private api: string = 'http://localhost:5170';

    constructor(private httpClient: HttpClient) { }



    public obterClientes(): Observable<ICliente[]> {
        return this.httpClient.get<ICliente[]>(`${this.api}/api/cliente`)
    }

    public inserirCliente(cliente: ICliente): Observable<IResposta> {
        return this.httpClient.post<IResposta>(`${this.api}/api/cliente`, cliente)
    }

    public editarCliente(cliente: ICliente): Observable<IResposta> {
        return this.httpClient.patch<IResposta>(`${this.api}/api/cliente/${cliente.id}`,cliente ) 
    }

    public excluirCliente(id: number): Observable<IResposta> {
        return this.httpClient.delete<IResposta>(`${this.api}/api/cliente/${id}`)
    }

    public obterPorId(id: number): Observable<ICliente> {        
        return this.httpClient.get<ICliente>(`${this.api}/api/cliente/${id}`).pipe(take(1));
    }


}