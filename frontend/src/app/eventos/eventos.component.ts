import { Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-eventos',
  templateUrl: './eventos.component.html',
  styleUrls: ['./eventos.component.css']
})
export class EventosComponent implements OnInit {

  _filtroLista: string;
  public get filtroLista() : string {
    return this._filtroLista;
  }
  public set filtroLista(value : string) {
    this._filtroLista = value;
    this.eventosFiltrados = this.filtroLista ? this.filtrarEvento(this.filtroLista) : this.eventos;
  }

  eventosFiltrados: any = [];
  eventos: any = [];
  imgLargura = 80;
  imgMargem = 3;
  mostrarImagem = false;

  constructor(private http: HttpClient) { }

  ngOnInit() {
    this.getEventos()
  }

  alternarImagem(){
    this.mostrarImagem = !this.mostrarImagem
  }

  filtrarEvento(filtroLista: string): any {
    filtroLista = filtroLista.toLocaleLowerCase();
    return this.eventos.filter(
      evento => evento.tema.toLocaleLowerCase().indexOf(filtroLista) !== -1
    );
  }

  getEventos() {
    this.http.get('http://localhost:5000/api/evento').subscribe(response => {
      this.eventos = response
      console.log('eventos', this.eventos)
    }, error => {
      console.log(error)
    })
  }

}
