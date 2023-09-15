import { Gradovi } from "./Gradovi.js";

const host=document.body;

const g=await fetch("https://localhost:7193/Ispit/PreuzmiGradove");
const data=await g.json();

for(let g of data){
    const grad=new Gradovi(g.id,g.ime,g.x,g.y,g.meteoroloskiPodaci);
    grad.iscrtaj(host);
}