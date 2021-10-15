const { Socket } = require('socket.io');
const { json } = require('stream/consumers');
const WebSocket = require('ws') 
const {Player} = require('./Class/player.js');
const {FNetworkPackage}=require('./Class/NetworkData');


const wss = new WebSocket.Server({ port: 8080 },()=>{
   console.log('server started')
});

var playerList=[];
var socketList=[];

wss.on('connection', function (socket) {
  var player=new Player();
  playerList[player.id]=player;
  socketList[player.id]=socket;
   socket.on('message', (data) => {
        let obj=JSON.parse(data);
       
        if(obj.RPC=="ClientLogin")
        {
         
       
       
      
          
          let networkPackage=new FNetworkPackage("ClientLoginCallback",player,JSON.stringify(player));
         
          socket.send(JSON.stringify(networkPackage));
 
         return;
        }

        if(obj.RPC=="SendClientPosition")
        {
        //  console.log(obj.client);

          let playerPosition=JSON.parse(obj.data);
          
          let networkPackage=new FNetworkPackage("OnPlayerMove",obj.client,obj.data);
         socket.send(JSON.stringify(networkPackage));
          BroadcastMessageAll(JSON.stringify(networkPackage));
        }

       
  }); 

  socket.on('close',()=>{
    return;
      try {
        if(playerList[this.id]!=undefined)
        {
            console.log(`Se has desconectado ${this.nombre}`);
          delete(playerList[this.id]);
  
        } 
      } catch (error) {
          console.log(error);
      }
     
 

  });
})
wss.on('listening',()=>{
  console.log('listening on 8080')
})


function BroadcastMessageAll(message)
{
  //console.log(socketList);
  for (const [key, value] of Object.entries(socketList)) {
 //   console.log(key);
    //value.send(message);
   }
   
}