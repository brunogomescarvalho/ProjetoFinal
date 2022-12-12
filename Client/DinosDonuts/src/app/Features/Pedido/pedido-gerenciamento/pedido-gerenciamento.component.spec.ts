import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PedidoGerenciamentoComponent } from './pedido-gerenciamento.component';

describe('PedidoGerenciamentoComponent', () => {
  let component: PedidoGerenciamentoComponent;
  let fixture: ComponentFixture<PedidoGerenciamentoComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ PedidoGerenciamentoComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(PedidoGerenciamentoComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
