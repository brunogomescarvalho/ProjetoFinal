import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { of, Subject } from 'rxjs';
import { catchError, take } from 'rxjs/operators';
import { INovoPedido, IProduto, IResposta } from 'src/app/app.model';
import { ProdutoService } from '../../Produto/ProdutoService';
import { PedidoService } from '../pedido-service';

@Component({
  selector: 'app-pedido-form',
  templateUrl: './pedido-form.component.html',
  styleUrls: ['./pedido-form.component.css']
})
export class PedidoFormComponent implements OnInit {

  form!: FormGroup;
  listaProdutos!: IProduto[];
  resposta!: IResposta;
  error$ = new Subject<IResposta>();
  solicitacaoRespondida !: boolean;


  constructor(private produtoService: ProdutoService, private pedidoService: PedidoService) { }

  ngOnInit(): void {
    this.obterProdutos();

    this.form = new FormGroup({
      cpfCliente: new FormControl(null, [ Validators.pattern('[0-9]{3}\.[0-9]{3}\.[0-9]{3}\-[0-9]{2}')]),
      idProduto: new FormControl(null, [Validators.required]),
      quantidade: new FormControl(null, [Validators.required, Validators.min(0)]),
    })
  }

  public obterProdutos() {
    this.solicitacaoRespondida = false;
    this.listaProdutos = [];
    this.produtoService.obterProdutosAtivos().pipe(take(1), catchError(error => {
      this.respostaHttp(error);
      return of()
    }))
      .subscribe((produtos: IProduto[]) => {
        this.listaProdutos = produtos;
        this.solicitacaoRespondida = true;
      })
  }

  public incluirPedido() {
    this.solicitacaoRespondida = false;
    if (this.form.valid) {
      const novoPedido: INovoPedido = {
        cpfCliente: this.form.value.cpfCliente == null ? "0" : this.form.value.cpfCliente,
        idProduto: this.form.value.idProduto.id,
        quantidade: this.form.value.quantidade
      }

      this.pedidoService.inserirPedido(novoPedido).pipe(take(1), catchError(error => {
        this.respostaHttp(error);
        return of(this.resposta);
      })).subscribe((resposta: IResposta) => {
        this.resposta = resposta;
        this.form.reset();
        this.solicitacaoRespondida = true;
      })
    }
  }
  
  private respostaHttp(error: any) {
    this.error$.next(error);
    console.log(error.error);
    this.resposta = error.error;
  }

}
