require('dotenv').config({path: '../config.env'})

const express = require('express')
const app = express()
const path = require('path')
const port = 3000
const database = require('./database')

app.use(express.json())

//Directory for static content
app.use(express.static(path.join(__dirname, '../public')))

//Routes
app.get('/', (req,res)=>{
    res.sendFile(path.join(__dirname, '../public', 'index.html'))
})

app.get('/spaces', (req,res)=>{
    res.sendFile(path.join(__dirname, '../public', 'spaces.html'))
})

app.post('/save-space', (req,res)=>{
    const{id, name, type, perimeter, area} = req.body

    database.insertSpace(id, name, type, perimeter, area)
        .then(()=>{
            res.status(200).json({message: 'Spaces saved successfully'})
        })
        .catch(()=>{
            res.status(500).json({error: 'Some error ocurred while saving Spaces'})
        })
})

//Run server
//http://localhost:'number of server'/
app.listen(port, ()=>{
    console.log('Server running at port: '+port)
})