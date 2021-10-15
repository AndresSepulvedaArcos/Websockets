const { nanoid } = require("nanoid");

class Player
{
    constructor()
    {
        this.networkID=nanoid();
        this.name; 
     
    }
}

module.exports={Player};

