import { Keep } from "./Keep.js";

export class VaultKeep extends Keep{

  constructor(data){
    super(data.Keep)
    this.vaultKeepId = data.id
    this.vaultId = data.vaultId
    this.creatorId = data.creatorId
  }
}