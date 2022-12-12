import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { catchError, map, of, Subject, switchMap, take } from 'rxjs';
import { IProduto, IResposta } from 'src/app/app.model';
import { ProdutoService } from '../ProdutoService';

@Component({
  selector: 'app-produto-estoque',
  templateUrl: './produto-estoque.component.html',
  styleUrls: ['./produto-estoque.component.css']
})
export class ProdutoEstoqueComponent implements OnInit {

  form!: FormGroup;
  resposta!: IResposta;
  produto!: IProduto;
  solicitacaoRespondida!: boolean;
  error$ = new Subject<IResposta>();


  constructor(private service: ProdutoService, private route: ActivatedRoute, private router: Router) { }

  ngOnInit(): void {
    this.solicitacaoRespondida = false;

    this.obterProduto();

    this.form = new FormGroup({
      id: new FormControl(null),
      descricao: new FormControl(null),
      quantidadeEstoque: new FormControl(null),
      quantidade: new FormControl(null, [Validators.required, Validators.min(0)])

    });
  }

  private obterProduto() {
    this.route.params.pipe(map((params: any) => params.id),
      switchMap(id => this.service.obterPorId(id))).
      subscribe(produto => {

        this.form.setValue({
          id: produto.id,
          descricao: produto.descricao,
          quantidadeEstoque: produto.quantidade,
          quantidade: null
         
        });
        this.solicitacaoRespondida = true;
      });
  }

  public enviar() {
    this.solicitacaoRespondida = false;
    if (this.form.valid) {
      this.produto = {
        id: this.form.value.id,
        descricao: this.form.value.descricao,
        quantidade: this.form.value.quantidade,
      }
      this.incluirQuantidade(this.produto);

    }
  }


  private incluirQuantidade(produto: IProduto) {
    this.service.editarQuantidade(produto.id!, produto.quantidade!).pipe(take(1),
      catchError(error => {
        this.respostaHttp(error);
        return of(this.resposta)
      })).subscribe((resposta: IResposta) => {
        this.solicitacaoRespondida = true;
        this.resposta = resposta;
        this.form.reset();
        this.obterProduto();

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

}
