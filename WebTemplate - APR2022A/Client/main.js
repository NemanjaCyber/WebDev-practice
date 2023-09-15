import { Prodavnica } from "./Prodavnica.js";

const host=document.body

const response=await fetch("https://localhost:7193/Ispit/VratiProdavniceSaBrendovima")
const data=await response.json();

for(let a of data){
    //console.log(a);
    let prod=new Prodavnica(a.id,a.naziv,a.brendovi);
    //console.log(prod);
    prod.iscrtaj(host)
}