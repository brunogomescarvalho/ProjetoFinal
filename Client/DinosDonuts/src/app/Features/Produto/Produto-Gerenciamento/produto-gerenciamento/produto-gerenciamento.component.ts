import { Component, OnInit } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { ActivatedRoute, Router } from '@angular/router';
import { catchError, of, Subject, take } from 'rxjs';
import { IProduto, IResposta } from 'src/app/app.model';
import { DialogConfirmComponent } from 'src/app/Features/dialog-confirm/dialog-confirm.component';
import { ProdutoService } from '../../ProdutoService';

@Component({
  selector: 'app-produto-gerenciamento',
  templateUrl: './produto-gerenciamento.component.html',
  styleUrls: ['./produto-gerenciamento.component.css']
})
export class ProdutoGerenciamentoComponent implements OnInit {

  listaProdutos!: IProduto[];
  produtoSelecionado!: IProduto;
  resposta!: IResposta;
  error$ = new Subject<IResposta>();
  solicitacaoRespondida !: boolean;
  
  constructor(
    private produtoService: ProdutoService,
    private router: Router,
    private route: ActivatedRoute,
    public dialog: MatDialog) { }

  ngOnInit(): void {
    this.mostrarProdutos();
  }

  private mostrarProdutos() {
    this.solicitacaoRespondida = false;
    this.listaProdutos = [];
    this.produtoService.obterProdutos().pipe(take(1),
      catchError(error => {
        this.respostaHttp(error);
        return of()
      }))
      .subscribe((produto: IProduto[]) => {
        this.listaProdutos = produto
        this.solicitacaoRespondida = true
      });
  }

  private respostaHttp(error: any) {
    this.error$.next(error);
    console.log(error.error);
    this.resposta = error.error;
  }

  public editarProduto(produto: IProduto) {
    this.router.navigate(['editar', produto.id], { relativeTo: this.route.parent })
    console.log(produto);
  }

  public editarEstoque(produto: IProduto) {
    this.router.navigate(['estoque', produto.id], { relativeTo: this.route.parent })
    console.log(produto);
  }

  public alterarAtivo(produto: IProduto) {
    this.solicitacaoRespondida = false;
    this.produtoService.editarAtivo(produto.id!).pipe(take(1),
      catchError(error => {
        this.respostaHttp(error);
        return of(this.resposta)
      }))
      .subscribe((resposta: IResposta) => {
        this.mostrarProdutos();
        this.solicitacaoRespondida = true;
        this.resposta = resposta;

      });
  }


  public openDialog(produto: IProduto): void {

    var opcao = produto.ativo == true ? 'Desativar' : 'Ativar';

    const dialogRef = this.dialog.open(DialogConfirmComponent, {
      data: {
        titulo:`${opcao} Produto`,
        msg: `Tem certeza que gostaria de ${opcao} o produto: ${produto.descricao}?`,
      }

    });

    dialogRef.afterClosed().subscribe(result => {
      if (result) {
        console.log(result, produto)
        this.alterarAtivo(produto)
      }
    })
  }

}
