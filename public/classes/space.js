export class Space {
    #id
    #name
    #type
    #perimeter
    #area

    constructor(id, space, type, perimeter, area){
        this.#id = id
        this.#name = space
        this.#type = type
        this.#perimeter = perimeter
        this.#area = area
    }

    toString() {
        return `Space - 
            Id: ${this.getId()}, 
            Name: ${this.getName()}, 
            Type: ${this.getType()}, 
            Perimeter: ${this.getPerimeter()}, 
            Area: ${this.getArea()}`
    }

    getId(){
        return this.#id
    }

    setId(id){
        this.#id = id
    }

    getName(){
        return this.#name
    }

    setName(name){
        this.#name = name
    }

    getType(){
        return this.#type
    }

    setType(type){
        this.#type = type
    }

    getPerimeter(){
        return this.#perimeter
    }

    setPerimeter(perimeter){
        this.#perimeter = perimeter
    }

    getArea(){
        return this.#area
    }

    setArea(area){
        this.#area = area
    }
}