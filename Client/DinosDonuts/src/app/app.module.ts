import { NgModule } from '@angular/core';
import { ReactiveFormsModule } from '@angular/forms';
import { BrowserModule } from '@angular/platform-browser';
import { HttpClientModule } from '@angular/common/http';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { ClienteService } from './Features/Cliente/cliente-service';
import { ClienteFormComponent } from './Features/Cliente/Cliente-Formulario/cliente-form/cliente-form.component';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { ClienteGerenciamentoComponent } from './Features/Cliente/Cliente-Gerenciamento/cliente-gerenciamento/cliente-gerenciamento.component';
import { ProdutoFormComponent } from './Features/Produto/Produto-Formulario/produto-form/produto-form.component';
import { ProdutoGerenciamentoComponent } from './Features/Produto/Produto-Gerenciamento/produto-gerenciamento/produto-gerenciamento.component';
import { ProdutoService } from './Features/Produto/ProdutoService';
import { PedidoFormComponent } from './Features/Pedido/pedido-form/pedido-form.component';
import { PedidoGerenciamentoComponent } from './Features/Pedido/pedido-gerenciamento/pedido-gerenciamento.component';
import { ProdutoEstoqueComponent } from './Features/Produto/produto-estoque/produto-estoque.component';
import { MatProgressBarModule } from '@angular/material/progress-bar';
import { AlertComponentComponent } from './Features/alert-component/alert-component.component';
import { PedidoAcompanhamentoComponent } from './Features/Pedido/pedido-acompanhamento/pedido-acompanhamento.component';
import { PedidoStatusComponent } from './Features/Pedido/pedido-status/pedido-status.component'
import { PedidoService } from './Features/Pedido/pedido-service';
import {MatDialogModule} from '@angular/material/dialog';
import { DialogConfirmComponent } from './Features/dialog-confirm/dialog-confirm.component'
import { BrowserAnimationsModule} from '@angular/platform-browser/animations'
import { DatePipe } from '@angular/common';



@NgModule({
  declarations: [
    AppComponent,
    ClienteFormComponent,
    ClienteGerenciamentoComponent,
    ProdutoFormComponent,
    ProdutoGerenciamentoComponent,
    PedidoFormComponent,
    PedidoGerenciamentoComponent,
    ProdutoEstoqueComponent,
    AlertComponentComponent,
    PedidoAcompanhamentoComponent,
    PedidoStatusComponent,
    DialogConfirmComponent,
   

  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    ReactiveFormsModule,
    HttpClientModule,
    NgbModule,
    MatProgressBarModule,
    MatDialogModule,
    BrowserAnimationsModule
  ],
  providers: [
    ClienteService, 
    ProdutoService, 
    PedidoService, 
    DatePipe], 
  bootstrap: [AppComponent]
})
export class AppModule { }
