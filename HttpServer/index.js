 
const WebSocket = require('ws') 
const {NetworkClient, Player} = require('./Class/player.js');
const {FNetworkPackage}=require('./Class/NetworkData');


const wss = new WebSocket.Server({ port: 8080 },()=>{
   console.log('server started')
});

var playerList=[];
var socketList=[];

wss.on('connection', function (socket) {
  var player=new Player();
  var playerID=player.networkClient.networkID;
  playerList[playerID]=player;
  socketList[playerID]=socket;

 
   socket.on('message', (data) => {
        let obj=JSON.parse(data);
       
        if(obj.RPC=="ClientLogin")
        { 
          let networkPackage=new FNetworkPackage("ClientLoginCallback",player.networkClient,JSON.stringify(player.networkClient));
         
          socket.send(JSON.stringify(networkPackage));

          OnPlayerSpawn(playerID);
 
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
     
      try {
        if(playerList[playerID]!=undefined)
        {
            console.log(`Se has desconectado ${playerID}`);
          delete(playerList[playerID]);
          delete(socketList[playerID]);
        } 
      } catch (error) {
          console.log(error);
      }
     
 

  });
})
wss.on('listening',()=>{
  console.log('listening on 8080')
})

function OnPlayerSpawn(playerID)
{
 
  let playerArray=[];
  for (const playerId in playerList) {
    playerArray.push(playerList[playerId]);
  }

  console.log(  JSON.stringify(playerArray));
  let networkPackage= new FNetworkPackage("OnPlayerSpawn",playerList[playerID].networkClient,JSON.stringify(playerArray));
  socketList[playerID].send(JSON.stringify(networkPackage));
  console.log(JSON.stringify(playerList));


}

function BroadcastMessageAll(message)
{
  //console.log(socketList);
  for (const [key, value] of Object.entries(socketList)) {
 //   console.log(key);
    //value.send(message);
   }
   
}