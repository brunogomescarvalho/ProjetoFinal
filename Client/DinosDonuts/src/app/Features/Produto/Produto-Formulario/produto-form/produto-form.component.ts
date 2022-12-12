import { DatePipe } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { catchError, map, of, Subject, switchMap, take } from 'rxjs';
import { AppRoutingModule } from 'src/app/app-routing.module';
import { IProduto, IResposta } from 'src/app/app.model';
import { ProdutoService } from '../../ProdutoService';

@Component({
  selector: 'app-produto-form',
  templateUrl: './produto-form.component.html',
  styleUrls: ['./produto-form.component.css']
})
export class ProdutoFormComponent implements OnInit {

  public form!: FormGroup;
  public resposta!: IResposta;
  public error$ = new Subject<IResposta>();
  solicitacaoRespondida!: boolean;
  cadastrarNovoProduto: boolean = true;

  constructor(
    private produtoService: ProdutoService,
    private route: ActivatedRoute,
    private routeModule: AppRoutingModule,
    private router: Router,
    private datePipe: DatePipe) { }

  ngOnInit(): void {


    if (this.route.snapshot.routeConfig?.path != this.routeModule.pathFormProduto) {
      this.cadastrarNovoProduto = false;
    }

    this.route.params.pipe(map((params: any) => params.id),
      switchMap(id => id != null ? this.produtoService.obterPorId(id) : of())).
      subscribe(produto => this.carregarDadosProdutos(produto))

    this.form = new FormGroup({
      id: new FormControl(null),
      descricao: new FormControl(null, [Validators.required, Validators.minLength(3)]),
      preco: new FormControl(null, [Validators.required, Validators.min(0)]),
      dataValidade: new FormControl(null, [Validators.required])

    });

  }

  public enviar() {
    if (this.form.valid) {

      const produto = {
        id: this.form.value.id == null ? 0 : this.form.value.id,
        descricao: this.form.value.descricao,
        preco: this.form.value.preco,
        dataValidade: this.form.value.dataValidade,
      }
      console.log(produto);
      this.solicitacaoRespondida = false;

      if (produto.id == 0) {
        this.cadastrar(produto)
      }
      else {
        this.editar(produto);
      }
    }
  }

  private cadastrar(produto: IProduto) {
    this.produtoService.inserirProduto(produto).pipe(take(1),
      catchError(error => {
        this.respostaHttp(error);
        return of(this.resposta)
      })).subscribe((resposta: IResposta) => {
        this.solicitacaoRespondida = true;
        this.resposta = resposta;
        this.form.reset();
      });
  }

  private editar(produto: IProduto) {
    this.produtoService.editarProduto(produto).pipe(take(1),
      catchError(error => {
        this.respostaHttp(error);
        return of(this.resposta)
      })).subscribe((resposta: IResposta) => {
        this.solicitacaoRespondida = true;
        this.resposta = resposta;
        this.form.reset();
        this.carregarDadosProdutos(produto);
      });
  }

  private respostaHttp(error: any) {
    this.error$.next(error);
    console.log(error.error);
    this.resposta = error.error;
  }


  private carregarDadosProdutos(produto: IProduto) {
    this.form.setValue({
      id: produto.id,
      descricao: produto.descricao,
      preco: produto.preco,
      dataValidade: this.datePipe.transform(produto.dataValidade, 'yyyy-MM-dd')
    })

  }

  public voltar() {
    this.router.navigate(['gerenciar'], { relativeTo: this.route.parent })
  }

}
