import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ProdutoGerenciamentoComponent } from './produto-gerenciamento.component';

describe('ProdutoGerenciamentoComponent', () => {
  let component: ProdutoGerenciamentoComponent;
  let fixture: ComponentFixture<ProdutoGerenciamentoComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ProdutoGerenciamentoComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ProdutoGerenciamentoComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
