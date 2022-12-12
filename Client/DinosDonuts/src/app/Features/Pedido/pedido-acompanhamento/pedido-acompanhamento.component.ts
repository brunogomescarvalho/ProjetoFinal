import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { map, of, switchMap } from 'rxjs';
import { IPedido, StatusPedido } from 'src/app/app.model';
import { PedidoService } from '../pedido-service';

@Component({
  selector: 'app-pedido-acompanhamento',
  templateUrl: './pedido-acompanhamento.component.html',
  styleUrls: ['./pedido-acompanhamento.component.css']
})
export class PedidoAcompanhamentoComponent implements OnInit {

  public pedido?: IPedido
  public status!: StatusPedido[]

  constructor(private pedidoService: PedidoService, private route: ActivatedRoute,private router: Router) { }

  ngOnInit(): void {

    this.carregarPedido();
  }

  private carregarPedido() {
    this.route.params.pipe(map((params: any) => params.id),
      switchMap(id => id != null ? this.pedidoService.obterPorId(id) : of())).
      subscribe(pedido => { this.pedido=pedido });
  }

  public voltar() {
    this.router.navigate(['gerenciar'], { relativeTo: this.route.parent })
  }

}
