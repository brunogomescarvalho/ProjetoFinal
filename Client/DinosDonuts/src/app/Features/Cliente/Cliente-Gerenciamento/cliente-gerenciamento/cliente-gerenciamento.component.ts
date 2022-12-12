import { Component, OnInit } from '@angular/core';
import { ICliente, IResposta } from 'src/app/app.model';
import { ClienteService } from '../../cliente-service';
import { catchError, of, Subject, take } from 'rxjs';
import { ActivatedRoute, Router } from '@angular/router';
import { DialogConfirmComponent } from 'src/app/Features/dialog-confirm/dialog-confirm.component';
import { MatDialog } from '@angular/material/dialog';


@Component({
  selector: 'app-cliente-gerenciamento',
  templateUrl: './cliente-gerenciamento.component.html',
  styleUrls: ['./cliente-gerenciamento.component.css']
})


export class ClienteGerenciamentoComponent implements OnInit {

  listaClientes!: ICliente[];
  clienteSelecionado!: ICliente;
  resposta!: IResposta;
  error$ = new Subject<IResposta>();
  solicitacaoRespondida !: boolean;
  
  constructor(
    private clienteService: ClienteService,
    private router: Router,
    private route: ActivatedRoute,
    public dialog: MatDialog) { }

  ngOnInit(): void {
    this.mostrarClientes();
  }


  private mostrarClientes() {
    this.solicitacaoRespondida = false;
    this.listaClientes = [];
    this.clienteService.obterClientes().pipe(take(1),
      catchError(error => {
        this.respostaHttp(error);
        return of()
      }))
      .subscribe((clientes: ICliente[]) => {
        this.listaClientes = clientes
        this.solicitacaoRespondida = true
      });
  }

  public editarCliente(cliente: ICliente) {
    this.router.navigate(['editar', cliente.id], { relativeTo: this.route.parent })
    console.log(cliente);

  }

  public excluirCliente(cliente: ICliente) {
    this.solicitacaoRespondida = false;
    this.clienteService.excluirCliente(cliente.id!).pipe(take(1),
      catchError(error => {
        this.respostaHttp(error);
        return of(this.resposta)
      }))
      .subscribe((resposta: IResposta) => {
        this.mostrarClientes();
        this.solicitacaoRespondida = true;
        this.resposta = resposta;

      });
  }

  public openDialog(cliente: ICliente): void {

    const dialogRef = this.dialog.open(DialogConfirmComponent, {
      data: {
        titulo: `Excluir Cliente`,
        msg: `Tem certeza que gostaria de excluir o cliente: Id: ${cliente.id} - ${cliente.nome}?`,
      }

    });

    dialogRef.afterClosed().subscribe(result => {
      if (result) {
        console.log(result, cliente)
        this.excluirCliente(cliente)
      }
    })
  }

  private respostaHttp(error: any) {
    this.error$.next(error);
    console.log(error.error);
    this.resposta = error.error;
  }

}
