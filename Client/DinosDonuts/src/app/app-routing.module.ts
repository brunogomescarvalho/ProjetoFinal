import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { ClienteFormComponent } from './Features/Cliente/Cliente-Formulario/cliente-form/cliente-form.component';
import { ClienteGerenciamentoComponent } from './Features/Cliente/Cliente-Gerenciamento/cliente-gerenciamento/cliente-gerenciamento.component';
import { HomeComponent } from './Features/Home/home/home.component';
import { PedidoFormComponent } from './Features/Pedido/pedido-form/pedido-form.component';
import { ProdutoFormComponent } from './Features/Produto/Produto-Formulario/produto-form/produto-form.component';
import { ProdutoGerenciamentoComponent } from './Features/Produto/Produto-Gerenciamento/produto-gerenciamento/produto-gerenciamento.component';
import{ PedidoGerenciamentoComponent} from './Features/Pedido/pedido-gerenciamento/pedido-gerenciamento.component';
import { ProdutoEstoqueComponent } from './Features/Produto/produto-estoque/produto-estoque.component';
import { PedidoStatusComponent } from './Features/Pedido/pedido-status/pedido-status.component';
import { PedidoAcompanhamentoComponent } from './Features/Pedido/pedido-acompanhamento/pedido-acompanhamento.component';

const routes: Routes = [

  {
    path: 'cliente',
    children: [
      {
        path: 'formulario',
        component: ClienteFormComponent,
      },
      {
        path: 'gerenciar',
        component: ClienteGerenciamentoComponent,
      },
      {
        path: 'editar/:id',
        component: ClienteFormComponent,
      }

    ]
  },

  {
    path: 'produto',
    children: [
      {
        path: 'formulario',
        component: ProdutoFormComponent,
      },
      {
        path: 'gerenciar',
        component: ProdutoGerenciamentoComponent,

      },
      {
        path: 'editar/:id',
        component: ProdutoFormComponent,
      },
      {
        path: 'estoque/:id',
        component: ProdutoEstoqueComponent,
      },
    ]
  },
  {
    path: 'pedido',
    children: [
      {
        path: 'formulario',
        component: PedidoFormComponent,
      },
      {
        path: 'gerenciar',
        component: PedidoGerenciamentoComponent,
      },
      {
        path: 'status/:id',
        component: PedidoStatusComponent,
      },
      {
        path: 'acompanhar/:id',
        component: PedidoAcompanhamentoComponent,
      }
     
    ]
  },
  {
    path: 'home',
    component: HomeComponent
  },
 
 
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule {

  public readonly pathFormCliente: string = 'formulario' 
  public readonly pathFormProduto: string = 'formulario' 
}
