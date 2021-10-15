var express = require('express')
var path = require('path');
var app = express()

app.use(express.urlencoded({extended: true,limit:1024*1024*10,type:'application/x-www-form-urlencoded'}));
 app.use(express.json({limit:1024*1024*10, type:'application/json'})) // To parse the incoming requests with JSON payloads
 app.use(express.static(path.join(__dirname,'public')));


 var players=[];
app.post('/',async function(req,res){
 
    let json = await JSON.parse(JSON.stringify(req.body));
    players.push(json);

    if(players.length>=6)
    {
       let randNumber= Math.round(Math.random()*10)  ;
       console.log(`el numero al azar fue ${randNumber}`);
       players.forEach(element => {
         
           if(Math.round(element.numero)==randNumber)
           {
              console.log(`Ha ganado ${element.nombre} con el numero ${element.numero}`);

           }else{
            console.log(`Ha perdido ${element.nombre} con el numero ${element.numero}`);
 
           }
       });



    } 
    console.log(`Se ha conectado ${ json.nombre} con el numero ${ json.numero}` );

    res.send( json.nombre);

 
});
 

app.get('/', function (req, res) {
 return;
     var jsonObject={};
    jsonObject.ServerName="JsonName";
    jsonObject.Time=123; 
    res.send(JSON.stringify(jsonObject));
   

   })
   

var server = app.listen(3000, function () {
    var host = server.address().address
    var port = server.address().port
   
    console.log("Example app listening at http://%s:%s", host, port)
 })
 