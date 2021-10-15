const { nanoid } = require("nanoid");

class NetworkClient
{
    constructor()
    {
        this.networkID=nanoid();
      
     
    }
}

class Player
{
    constructor()
    {
        this.name=""; 
        this.networkClient=new NetworkClient();
        this.position={x:0,y:0}
    }
}
module.exports={NetworkClient,Player};

