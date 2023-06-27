import {Space} from '../classes/space.js'

let space = []

//### FOR SCREEN OBJECTS ###
let listSpace = document.querySelector('select#listSpace')

let spaceInput = document.querySelector('input#space')
let typeInput = document.querySelector('input#type')
let areaInput = document.querySelector('input#area')
let perimeterInput = document.querySelector('input#perimeter')

//### FOR EVENT LISTENER ###
let addBtn  = document.querySelector('button#addBtn')
let deleteBtn = document.querySelector('button#deleteBtn')
let orderBtn = document.querySelector('button#orderBtn')
let changeBtn = document.querySelector('button#changeBtn')

addBtn.addEventListener('click', getInputs)
deleteBtn.addEventListener('click', deleteSpace)
orderBtn.addEventListener('click', order)
changeBtn.addEventListener('click', change)

listSpace.addEventListener('click', selectSpace)
listSpace.addEventListener('keydown', navigate)

//### GET HEADER ###
fetch('header.html')
    .then(response => response.text())
    .then(data => {
        const pageHeader = document.querySelector('div#pageHeader')
        pageHeader.innerHTML = data;
    }) 

readDB()


//### FUNCTIONS FOR BUTTONS ###
//Get the inputs entered by user
function getInputs() {
    //Add the inputs in the space array on last position
    let spaceId = 0
    let lastId = []
    if (space.length == [])
        spaceId = 0
    else{
        for (let index = 0; index < space.length; index++)
            lastId.push(space[index].getId())
            
        spaceId = Math.max(...lastId) + 1
    }

    space.push(new Space(spaceId, spaceInput.value, typeInput.value, areaInput.value, perimeterInput.value));
    insertSelect();


    clear();

    createDB();
}

function order() {
    //Put the vector in order considering the space name.
    space.sort((a,b) => {
        if(a.getName()<b.getName()){
            return -1;
        }
        else if(a.getName()>b.getName()){
            return 1;
        }
        else
            return 0;
    });

    insertSelect();

}

//Change the values
function change() {
    let selectedItem = listSpace.selectedIndex;

    space[selectedItem].setName(spaceInput.value)
    space[selectedItem].setType(typeInput.value)
    space[selectedItem].setPerimeter(perimeterInput.value)
    space[selectedItem].setArea(areaInput.value)

    clear();
    insertSelect();

    updateDB(selectedItem);
}

//Delete the selected space
function deleteSpace() {
    let selected = listSpace.selectedIndex;
    let id = space[selected].getId()

    console.log('id: '+id)
    deleteDB(id)

    //javaScript allow to delete the vector position. In this case, it's not necessary prevent null vector.
    space.splice(selected, 1)

    insertSelect()
}

//Put the data selected space in the labels
function selectSpace() {
    let selectedItem = listSpace.selectedIndex;

    spaceInput.value = space[selectedItem].getName()
    typeInput.value = space[selectedItem].getType()
    perimeterInput.value = space[selectedItem].getPerimeter()
    areaInput.value = space[selectedItem].getArea()
}

//Create a space list and show in the select item
function insertSelect() {
    let option = [];
    
    //Erase the content existed in listSpace
    listSpace.innerHTML = '';

    //Create the options, and insert the content in listSpace
    for (let index = 0; index < space.length; index++) {
        option.push(document.createElement('option'));
        option[index].innerHTML = space[index].getName();
        listSpace.appendChild(option[index]);   
    }
    
}

//Clear the content in input items.
function clear() {
    spaceInput.value = '';
    typeInput.value  = '';
    areaInput.value  = '';
    perimeterInput.value  = '';
    spaceInput.focus();
}

//Other way to select the space is navigate using the keyboard.
function navigate(event) {
    if(event.keyCode == 40 || event.keyCode == 38){
        let selectedItem;

        if(event.keyCode == 40)
            selectedItem = listSpace.selectedIndex+1;
        else if(event.keyCode == 38)
            selectedItem = listSpace.selectedIndex-1;
        
        console.log(selectedItem);

        if(selectedItem<0 || selectedItem>space.length-1){}
        else{
            spaceInput.value = space[selectedItem].getName()
            typeInput.value = space[selectedItem].getType()
            perimeterInput.value = space[selectedItem].getPerimeter()
            areaInput.value = space[selectedItem].getArea()
        }

        
    }
}

function createDB() {
    (async()=>{
        console.log('Creating space to database')

        let id = space[space.length-1].getId()
        let name = space[space.length-1].getName()
        let type = space[space.length-1].getType()
        let perimeter = space[space.length-1].getPerimeter()
        let area = space[space.length-1].getArea()

        await fetch('/create-space',{
            method: 'POST',
            headers: {
                'Content-type': 'application/json'
            },
            body: JSON.stringify({id, name, type, perimeter, area}),
        })
            .then((response)=>response.json())
            .then((data)=>{
                console.log(data.message)
            })
            .catch((error)=>{
                console.error('Some error happen', error)
            })
    })()
}

function readDB(){
    let spaceDB
    (async()=>{
        console.log('Getting spaces from database')

        await fetch('/get-space')
            .then((response)=>response.json())
            .then((data)=>{
                spaceDB = Object.values(data)
            })
            .catch((error)=>{
                console.error('Some error happen while getting spaces', error)
            })
        
        for (let index = 0; index < spaceDB.length; index++) {
            space.push(new Space(
                spaceDB[index].id_space, 
                spaceDB[index].name_space, 
                spaceDB[index].type_space, 
                spaceDB[index].perimeter_space,
                spaceDB[index].area_space, 
            ));
        }
        insertSelect();
    })()
}

function updateDB(row){
    console.log('Updating space to database')

    let id = space[row].getId()
    let name = space[row].getName()
    let type = space[row].getType()
    let perimeter = space[row].getPerimeter()
    let area = space[row].getArea()

    fetch('/update-space', {
        method: 'PUT',
        headers: {
            'Content-type' : 'application/json'
        },
        body: JSON.stringify({id, name, type, area, perimeter}),
    })
        .then((response)=>response.json())
        .then((data)=>{
            console.log(data.message)
        })
        .catch((error)=>{
            console.log('Some error happen', error)
        })
}

function deleteDB(id){
    console.log('Deleting space from database')

    fetch(`/delete-space/${id}`, {method: 'DELETE'})
        .then((response)=>response.json)
        .then((data)=>{
            console.log(data.message)
        })
        .catch((error)=>{
            console.log('Some error happen while deleting space')
        })
}