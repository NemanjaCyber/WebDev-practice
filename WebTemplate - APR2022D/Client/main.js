import { Prodavnica } from "./Prodavnica.js";

const host=document.body;

const response=await fetch("https://localhost:7193/Ispit/VratiProdavniceSaTipovimaIBrendovima");
const data=await response.json();

for( let a of data){
    //console.log(a);
    let p=new Prodavnica(a.id,a.naziv,a.tipovi,a.brendovi);
    //console.log(p);
    p.iscrtaj(host);
}