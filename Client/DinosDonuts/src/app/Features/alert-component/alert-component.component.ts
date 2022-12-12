import { Component, Input, OnInit } from '@angular/core';
import { IResposta } from 'src/app/app.model';

@Component({
  selector: 'app-alert-component',
  templateUrl: './alert-component.component.html',
  styleUrls: ['./alert-component.component.css']
})
export class AlertComponentComponent implements OnInit {
@Input() resposta!: IResposta;
@Input() solicitacaoRespondida !: boolean;

  constructor() { }

  ngOnInit(): void {
  }

}
