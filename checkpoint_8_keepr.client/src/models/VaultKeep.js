import { Keep } from "./Keep.js";

export class VaultKeep {

  constructor(data) {
    this.keepId = data.keepId
    this.vaultKeepId = data.id
    this.vaultId = data.vaultId
    this.creatorId = data.creatorId
  }
}