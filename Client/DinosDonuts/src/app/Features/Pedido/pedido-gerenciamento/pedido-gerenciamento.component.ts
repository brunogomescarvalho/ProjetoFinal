import { Component, OnInit } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { ActivatedRoute, Router } from '@angular/router';
import { catchError, of, Subject, take } from 'rxjs';
import { IPedido, IResposta, StatusPedido } from 'src/app/app.model';
import { __values } from 'tslib';
import { DialogConfirmComponent } from '../../dialog-confirm/dialog-confirm.component';
import { PedidoService } from '../pedido-service';

@Component({
  selector: 'app-pedido-gerenciamento',
  templateUrl: './pedido-gerenciamento.component.html',
  styleUrls: ['./pedido-gerenciamento.component.css']
})
export class PedidoGerenciamentoComponent implements OnInit {

  listaPedidos!: IPedido[];
  resposta!: IResposta;
  error$ = new Subject<IResposta>();
  solicitacaoRespondida !: boolean;
  statusPedido?: any[];
  

  constructor(
    private pedidoService: PedidoService, 
    private router: Router, 
    private route: ActivatedRoute,
    public dialog: MatDialog) { }

  ngOnInit(): void {
    this.mostrarPedidos();
  }

  public alterarStatus(pedido: IPedido) {
    this.router.navigate(['status', pedido.id], { relativeTo: this.route.parent })
    console.log(pedido);
  }

  public acompanharPedido(pedido: IPedido) {
    this.router.navigate(['acompanhar', pedido.id], { relativeTo: this.route.parent })
    console.log(pedido);
  }

  private mostrarPedidos() {
    this.solicitacaoRespondida = false;
    this.listaPedidos = [];
    this.pedidoService.obterPedidos().pipe(take(1),
      catchError(error => {
        this.respostaHttp(error);
        return of()
      }))
      .subscribe((pedido: IPedido[]) => {
        this.listaPedidos = pedido;
        this.solicitacaoRespondida = true
      });
      
  }

  public excluirPedido(pedido: IPedido) {
      this.solicitacaoRespondida = false;
      this.pedidoService.excluirPedido(pedido.id!).pipe(take(1),
        catchError(error => {
          this.respostaHttp(error);
          return of(this.resposta)
        }))
        .subscribe((resposta: IResposta) => {
          this.mostrarPedidos();
          this.solicitacaoRespondida = true;
          this.resposta = resposta;

        });
    }
  
  private respostaHttp(error: any) {
    this.error$.next(error);
    console.log(error.error);
    this.resposta = error.error;
  }

  public enumString(pedido: IPedido): string {
    var enumString: string = '';
    this.statusPedido = Object.keys(StatusPedido).filter(key => isNaN(+key));
    for (let status of this.statusPedido) {
      if (pedido.statusPedido == this.statusPedido.indexOf(status)) {
        enumString = status
      }
    }
    return enumString
  }

  public openDialog(pedido: IPedido): void {
    const dialogRef = this.dialog.open(DialogConfirmComponent, {
      data: {
        titulo:`Excluir Pedido`,
        msg: `Tem certeza que gostaria de excluir o pedido: Nr ${pedido.id} - ${pedido.produto?.descricao} - Total R$ ${pedido.valorTotal}?`,
      }
    });
    dialogRef.afterClosed().subscribe(result => {
      if (result) {
        console.log(result, pedido)
        this.excluirPedido(pedido)
      }
    })
  }
}

