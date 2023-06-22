//Export it's necessary to export a class
export class Space {
    constructor(space, type, area, perimeter){
        this.space = space
        this.type = type
        this.area = area
        this.perimeter = perimeter
    }

    toString() {
        return `Espaço: ${this.space}, Tipo: ${this.type}, Área: ${this.area}, Perímetro: ${this.perimeter}`
    }
}