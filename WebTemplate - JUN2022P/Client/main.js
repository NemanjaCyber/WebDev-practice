import { Prodavnica } from "./Prodavnica.js";

const host=document.body;

const p=await fetch("https://localhost:7193/Ispit/VratiProdavniceSaMarkama")

const podaci=await p.json();

for(let x of podaci){
    console.log(x);
    let prodaja=new Prodavnica(x.marke,x.prodavnicaID);
    prodaja.iscrtaj(host);
}