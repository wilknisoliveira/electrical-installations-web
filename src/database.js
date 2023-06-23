const mysql = require('mysql2')

const connection = mysql.createConnection({
    host: process.env.DB_HOST,
    user: process.env.DB_USER,
    password: process.env.DB_PASSWORD,
    database: process.env.DB_NAME,
})

function createSpace(id, name, type, perimeter, area){
    return new Promise((resolve, reject)=>{
        connection.query('INSERT INTO space (id_space, name_space, type_space, perimeter_space, area_space) VALUES(?, ?, ?, ?, ?)', [id, name, type, perimeter, area], (error, results)=>{
            if(error){
                reject(error)
            }else{
                resolve()
            }
        })
    })
}

function getSpace(){
    return new Promise((resolve, reject)=>{
        connection.query('SELECT * FROM space', (error, results)=>{
            if(error){
                reject(error);
            }else{
                resolve(results);
            }
        })
    })
}


module.exports = {createSpace, getSpace}