const express = require('express')
const app = express()
const path = require('path')
const port = 5500

//Directory for static content
app.use(express.static(path.join(__dirname, 'public')))

//Routes
app.get('/', (req,res)=>{
    res.sendFile(path.join(__dirname, 'public', 'index.html'))
})

app.get('/spaces', (req,res)=>{
    res.sendFile(path.join(__dirname, 'public', 'spaces.html'))
})

//Run server
//http://localhost:'number of server'/
app.listen(port, ()=>{
    console.log('Server running at port: '+port)
})