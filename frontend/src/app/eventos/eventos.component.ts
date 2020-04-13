import { Component, OnInit, TemplateRef } from '@angular/core';
import { EventoService } from '../_services/evento.service';
import { Evento } from '../_models/Evento';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';

@Component({
  selector: 'app-eventos',
  templateUrl: './eventos.component.html',
  styleUrls: ['./eventos.component.css']
})
export class EventosComponent implements OnInit {

  constructor(
    private eventoService: EventoService,
    private modalService: BsModalService
    ) { }

  ngOnInit() {
    this.getEventos()
  }

  eventosFiltrados: Evento[];
  eventos: Evento[];
  imgLargura = 80;
  imgMargem = 3;
  mostrarImagem = false;
  modalRef: BsModalRef;

  _filtroLista: string;
  public get filtroLista() : string {
    return this._filtroLista;
  }
  public set filtroLista(value : string) {
    this._filtroLista = value;
    this.eventosFiltrados = this.filtroLista ? this.filtrarEvento(this.filtroLista) : this.eventos;
  }

  openModal(template: TemplateRef<any>) {
    this.modalRef = this.modalService.show(template)
  }

  alternarImagem(){
    this.mostrarImagem = !this.mostrarImagem
  }

  filtrarEvento(filtroLista: string): Evento[] {
    filtroLista = filtroLista.toLocaleLowerCase()
    return this.eventos.filter(
      (evento: any) => evento.tema.toLocaleLowerCase().indexOf(filtroLista) !== -1
    )
  }

  getEventos() {
    this.eventoService.getAllEventos().subscribe((response: Evento[]) => {
      this.eventos = response
      this.eventosFiltrados = this.eventos
      console.log('eventos', this.eventos)
    }, error => {
      console.log(error)
    })
  }

}
