import { Lote } from './Lote';
import { RedeSocial } from './RedeSocial';
import { Palestrante } from './Palestrante';

export interface Evento {

  id: number;
  local: string;
  dataEvento: Date;
  tema: string;
  qtdPessoas: number;
  lote: Lote[];
  redeSociais: RedeSocial[];
  telefone: string;
  imageURL: string;
  email: string;
  palestrantesEventos: Palestrante[];

}
