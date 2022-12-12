import { KeyValuePipe } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import {FormControl, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { catchError, map, of, Subject, switchMap, take } from 'rxjs';
import { IPedido, IResposta, StatusPedido } from 'src/app/app.model';
import { PedidoService } from '../pedido-service';


@Component({
  selector: 'app-pedido-status',
  templateUrl: './pedido-status.component.html',
  styleUrls: ['./pedido-status.component.css']
})
export class PedidoStatusComponent implements OnInit {

  public form!: FormGroup;
  public resposta!: IResposta;
  public error$ = new Subject<IResposta>();
  public solicitacaoRespondida!: boolean;
  public pedido!: IPedido;
  public statusPedido?:string[];
 

  constructor(private pedidoService: PedidoService, private route: ActivatedRoute, private router: Router) {}

  ngOnInit(): void {

    this.statusPedido = Object.keys(StatusPedido).filter(key=>isNaN(+key));

    this.carregarPedido();
    this.form = new FormGroup({
      id: new FormControl(null),
      statusAtual: new FormControl(null),
      statusAlterar: new FormControl(null, [Validators.required]),
    });
  }

  private carregarPedido() {
    this.route.params.pipe(map((params: any) => params.id),
      switchMap(id => id != null ? this.pedidoService.obterPorId(id) : of())).
      subscribe(pedido => this.mostrarPedido(pedido));
  }

  private mostrarPedido(pedido: IPedido) {
    this.form.setValue({
      id: pedido.id,
      statusAtual: pedido.statusPedido,
      statusAlterar: null,
    })
  }

  public enviar() {
    this.solicitacaoRespondida = false;
    if (this.form.valid) {
      this.pedido = {
        id: this.form.value.id,
        statusPedido: this.form.value.statusAlterar,
      }
      this.alterarStatus(this.pedido);

    }
  }
  private alterarStatus(pedido: IPedido) {
    this.pedidoService.alterarStatus(pedido.id, <number>pedido.statusPedido).pipe(take(1),
      catchError(error => {
        this.respostaHttp(error);
        return of(this.resposta)
      })).subscribe((resposta: IResposta) => {
        this.solicitacaoRespondida = true;
        this.resposta = resposta;
        this.form.reset();
        this.carregarPedido();

      });
  }

  public voltar() {
    this.router.navigate(['gerenciar'], { relativeTo: this.route.parent })
  }

  private respostaHttp(error: any) {
    this.error$.next(error);
    console.log(error.error);
    this.resposta = error.error;
  }

  public enumString(statusNr: number): string {

    var enumString: string = '';
    this.statusPedido = Object.keys(StatusPedido).filter(key => isNaN(+key));
    for (let status of this.statusPedido) {
      if (statusNr == this.statusPedido.indexOf(status)) {
        enumString = status
      }
    }
    return enumString
  }


}




