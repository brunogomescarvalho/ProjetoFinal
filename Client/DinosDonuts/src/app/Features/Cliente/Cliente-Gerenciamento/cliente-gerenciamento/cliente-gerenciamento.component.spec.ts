import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ClienteGerenciamentoComponent } from './cliente-gerenciamento.component';

describe('ClienteGerenciamentoComponent', () => {
  let component: ClienteGerenciamentoComponent;
  let fixture: ComponentFixture<ClienteGerenciamentoComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ClienteGerenciamentoComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ClienteGerenciamentoComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
