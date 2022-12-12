
export interface ICliente {
    id?: number,
    nome: string,
    cpf: string,
    dataNascimento: Date,
    pontos?: number
}

export interface IProduto {
    id?: number,
    descricao?: string,
    preco?: number,
    dataValidade?: Date,
    ativo?: boolean,
    quantidade?: number
}
export interface IResposta {
    statusCode: number,
    message: string
}

export interface INovoPedido {
    cpfCliente: string,
    idProduto: number,
    quantidade: number,
}

export interface IPedido {
    id: number,
    cliente?: ICliente,
    produto?: IProduto,
    quantidade?: number,
    valorTotal?: number,
    dataPedido?: Date,
    statusPedido: StatusPedido 

}
export enum StatusPedido {
    ANDAMENTO,
    TRANSITO,
    FINALIZADO

    


}

