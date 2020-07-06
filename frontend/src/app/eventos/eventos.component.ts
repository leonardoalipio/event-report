import { Component, OnInit } from '@angular/core'
import { EventoService } from '../_services/evento.service'
import { Evento } from '../_models/Evento'
import { FormGroup, Validators, FormBuilder } from '@angular/forms'
import { BsLocaleService } from 'ngx-bootstrap/datepicker'
import { ptBrLocale, defineLocale } from 'ngx-bootstrap/chronos'
defineLocale('pt-br', ptBrLocale)

@Component({
  selector: 'app-eventos',
  templateUrl: './eventos.component.html',
  styleUrls: ['./eventos.component.css']
})
export class EventosComponent implements OnInit {

  constructor(
    private eventoService: EventoService,
    private formBuilder: FormBuilder,
    private localeService: BsLocaleService
    ) {

    this.localeService.use('pt-br')

  }

  ngOnInit() {
    this.validation()
    this.getEventos()
  }

  eventosFiltrados: Evento[]
  eventos: Evento[]
  evento: Evento
  imgLargura = 80
  imgMargem = 3
  mostrarImagem = false
  registerForm: FormGroup
  typeSave = ''
  modalTitle = ''
  bodyDeletarEvento = ''

  _filtroLista: string
  public get filtroLista() : string {
    return this._filtroLista
  }
  public set filtroLista(value : string) {
    this._filtroLista = value
    this.eventosFiltrados = this.filtroLista ? this.filtrarEvento(this.filtroLista) : this.eventos
  }

  openModal(template: any) {
    this.registerForm.reset()
    template.show()
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

  validation() {
    this.registerForm = this.formBuilder.group({
      tema: [null, [Validators.required, Validators.minLength(4), Validators.maxLength(50)]],
      local: [null, Validators.required],
      dataEvento: [null, Validators.required],
      qtdPessoas: [Number, [Validators.required, Validators.max(120000)]],
      imageURL: [null, Validators.required],
      telefone: [null, Validators.required],
      email: [null, [Validators.required, Validators.email]],
    })
  }

  novoEvento(template: any) {
    this.modalTitle = 'Novo Evento'
    this.typeSave = 'post'
    this.openModal(template)
  }

  confirmeDelete(template: any) {
    this.eventoService.deleteEvento(this.evento.id).subscribe(
      () => {
          template.hide();
          this.getEventos();
        }, error => {
          console.log(error);
        }
    );
  }

  excluirEvento(evento: Evento, template: any) {
    this.openModal(template);
    this.evento = evento;
    this.bodyDeletarEvento = `
      Tem certeza que deseja excluir o Evento: ${evento.tema} \n Código: ${evento.id}
    `;
  }

  editEvento(template: any, evento: Evento) {
    this.modalTitle = `Editar Evento ${evento.tema}`
    this.typeSave = 'put'
    this.openModal(template)
    this.evento = evento
    this.registerForm.patchValue(evento)
  }

  salvarAlteracao(template: any) {

    if (this.registerForm.valid) {
      if (this.typeSave === 'post') {
        this.evento = Object.assign({}, this.registerForm.value)
        this.eventoService.postEvento(this.evento).subscribe(
          () => {
            template.hide()
            this.getEventos()
          }, error => console.log(error)
        )
      } else if (this.typeSave === 'put') {
        this.evento = Object.assign({id: this.evento.id}, this.registerForm.value)
        this.eventoService.putEvento(this.evento).subscribe(
          () => {
            template.hide()
            this.getEventos()
          }, error => console.log(error)
        )
      }
    }

  }

  getEventos() {
    this.eventoService.getAllEventos().subscribe((response: Evento[]) => {
      this.eventos = response
      this.eventosFiltrados = this.eventos
      console.log(response)
    }, error => {
      console.log(error)
    })
  }

}
