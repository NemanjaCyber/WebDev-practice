import { Prodavnica } from "./Prodavnica.js";

const host=document.body;

const response=await fetch("https://localhost:7193/Ispit/VratiProdavniceSaSastojcima")
const data=await response.json();

for(let a of data){
    //console.log(a);
    let prod=new Prodavnica(a.prodavnicaId,a.prodavnicaNaziv,a.prodavnicaZarada,a.sastojci);
    console.log(prod);
    prod.iscrtaj(host);
}