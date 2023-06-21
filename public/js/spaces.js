import {Space} from '../../classes/space.js';

let space = [];
let saveSpace = true;   //Used to define if alterations was made.


//### FOR SCREEN OBJECTS ###
let listSpace = document.querySelector('select#listSpace');

let spaceInput = document.querySelector('input#space');
let typeInput = document.querySelector('input#type');
let areaInput = document.querySelector('input#area');
let perimeterInput = document.querySelector('input#perimeter');

//### FOR EVENT LISTENER ###
let addBtn  = document.querySelector('button#addBtn');
let deleteBtn = document.querySelector('button#deleteBtn');
let orderBtn = document.querySelector('button#orderBtn');
let changeBtn = document.querySelector('button#changeBtn');

addBtn.addEventListener('click', getInputs);
deleteBtn.addEventListener('click', deleteSpace);
orderBtn.addEventListener('click', order);
changeBtn.addEventListener('click', change);
saveBtn.addEventListener('click', save);

listSpace.addEventListener('click', selectSpace);
listSpace.addEventListener('keydown', navigate);

window.addEventListener('beforeunload', unload)

//### GET HEADER ###
fetch('../header.html')
    .then(response => response.text())
    .then(data => {
        const pageHeader = document.querySelector('div#pageHeader')
        pageHeader.innerHTML = data;
    }) 


//### FUNCTIONS FOR BUTTONS ###
//Get the inputs entered by user
function getInputs() {
    //Add the inputs in the space array on last position
    space.push(new Space(spaceInput.value, typeInput.value, areaInput.value, perimeterInput.value));
    saveSpace = false;
    insertSelect();


    clear();
}

function order() {
    //Put the vector in order considering the space name.
    space.sort((a,b) => {
        if(a.space<b.space){
            return -1;
        }
        else if(a.space>b.space){
            return 1;
        }
        else
            return 0;
    });
    saveSpace = false;

    insertSelect();

}

//Change the values
function change() {
    let selectedItem = listSpace.selectedIndex;

    space[selectedItem].space = spaceInput.value;
    space[selectedItem].type = typeInput.value;
    space[selectedItem].area = areaInput.value;
    space[selectedItem].perimeter = perimeterInput.value;
    saveSpace = false;

    clear();
}

//Delete the selected space
function deleteSpace() {
    let selected = listSpace.selectedIndex;

    //javaScript allow to delete the vector position. In this case, it's not necessary prevent null vector.
    space.splice(selected, 1);
    saveSpace = false;

    insertSelect();
    
}

//Put the data selected space in the labels
function selectSpace() {
    let selectedItem = listSpace.selectedIndex;

    spaceInput.value = space[selectedItem].space
    typeInput.value = space[selectedItem].type
    areaInput.value = space[selectedItem].area
    perimeterInput.value = space[selectedItem].perimeter
}

//Create a space list and show in the select item
function insertSelect() {
    let option = [];
    
    //Erase the content existed in listSpace
    listSpace.innerHTML = '';

    //Create the options, and insert the content in listSpace
    for (let index = 0; index < space.length; index++) {
        option.push(document.createElement('option'));
        option[index].innerHTML = space[index].space;
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
            spaceInput.value = space[selectedItem].space
            typeInput.value = space[selectedItem].type
            areaInput.value = space[selectedItem].area
            perimeterInput.value = space[selectedItem].perimeter
        }

        
    }
}

function save() {
    //Here will stay the save code to BD.
    saveSpace = true;
}

function unload(event) {
    if(saveSpace != true){
        event.preventDefault();
        event.returnValue = '';
    }
    
}
