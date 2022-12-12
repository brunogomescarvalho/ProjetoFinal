import { DatePipe } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { catchError, map, of, Subject, switchMap, take } from 'rxjs';
import { AppRoutingModule } from 'src/app/app-routing.module';
import { ICliente, IResposta } from 'src/app/app.model';
import { ClienteService } from '../../cliente-service';


@Component({
  selector: 'app-cliente-form',
  templateUrl: './cliente-form.component.html',
  styleUrls: ['./cliente-form.component.css']
})
export class ClienteFormComponent implements OnInit {

  public form!: FormGroup;
  public resposta!: IResposta;
  public error$ = new Subject<IResposta>();
  solicitacaoRespondida!: boolean;
  cadastrarNovoCliente: boolean = true;

  constructor(
    private service: ClienteService,
    private route: ActivatedRoute,
    private routeModule: AppRoutingModule,
    private router: Router,
    private datePipe: DatePipe) { }

  ngOnInit(): void {

    if (this.route.snapshot.routeConfig?.path != this.routeModule.pathFormCliente) {
      this.cadastrarNovoCliente = false;
    }

    this.route.params.pipe(map((params: any) => params.id),
      switchMap(id => id != null ? this.service.obterPorId(id) : of())).
      subscribe(cliente => this.carregarDadosClientes(cliente))

    this.form = new FormGroup({
      id: new FormControl(null),
      nome: new FormControl(null, [Validators.required, Validators.minLength(3)]),
      cpf: new FormControl(null, [Validators.required, Validators.pattern('[0-9]{3}\.[0-9]{3}\.[0-9]{3}\-[0-9]{2}')]),
      dataNascimento: new FormControl(null, [Validators.required])

    });

  }

  public enviar() {
    if (this.form.valid) {

      const cliente = {
        id: this.form.value.id == null ? 0 : this.form.value.id,
        cpf: this.form.value.cpf,
        nome: this.form.value.nome,
        dataNascimento: this.form.value.dataNascimento,
      }
      console.log(cliente);
      this.solicitacaoRespondida = false;

      if (cliente.id == 0) {
        this.cadastrar(cliente)
      }
      else {
        this.editar(cliente);
      }
    }
  }

  private respostaHttp(error: any) {
    this.error$.next(error);
    console.log(error.error);
    this.resposta = error.error;
  }

  private cadastrar(cliente: ICliente) {
    this.service.inserirCliente(cliente).pipe(take(1),
      catchError(error => {
        this.respostaHttp(error);
        return of(this.resposta)
      })).subscribe((resposta: IResposta) => {
        this.solicitacaoRespondida = true;
        this.resposta = resposta;
        this.form.reset();
      });

  }

  private editar(cliente: ICliente) {
    this.service.editarCliente(cliente).pipe(take(1),
      catchError(error => {
        this.respostaHttp(error);
        return of(this.resposta)
      })).subscribe((resposta: IResposta) => {
        this.solicitacaoRespondida = true;
        this.resposta = resposta;
        this.form.reset();
        this.carregarDadosClientes(cliente);
      });
  }

  private carregarDadosClientes(cliente: ICliente) {
    this.form.setValue({
      id: cliente.id,
      cpf: cliente.cpf,
      nome: cliente.nome,
      dataNascimento: this.datePipe.transform(cliente.dataNascimento,'yyyy-MM-dd')
    })

  }

  public voltar() {
    this.router.navigate(['gerenciar'], { relativeTo: this.route.parent })
  }

}
