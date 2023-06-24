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

app.post('/create-space', (req,res)=>{
    const{id, name, type, perimeter, area} = req.body

    database.createSpace(id, name, type, perimeter, area)
        .then(()=>{
            res.status(200).json({message: 'Spaces created successfully'})
        })
        .catch(()=>{
            res.status(500).json({error: 'Some error ocurred while creating Spaces'})
        })
})

app.get('/get-space', (req,res)=>{
    database.readSpace()
        .then((rows)=>{
            res.status(200).json(rows)
        })
        .catch(()=>{
            res.status(500).json({error: 'Erro while getting spaces'})
        })
})

app.delete('/delete-space/:id', (req,res)=>{
    const id = req.params.id

    database.deleteSpace(id)
        .then(()=>{
            res.status(200).json({message: 'Space deleted'})
        })
        .catch(()=>{
            res.status(500).json({error: 'Some error ocurred while deleting space'})
        })
})

//Run server
//http://localhost:'number of server'/
app.listen(port, ()=>{
    console.log('Server running at port: '+port)
})