import { Produkcija } from "./Produkcija.js";

const host=document.body;

const p=await fetch("https://localhost:7193/Ispit/VratiProdukcijeSaKategorijama");
const podaci=await p.json();

for(let e of podaci){
    //console.log(e);
    let kuca=new Produkcija(e.produkcijskaKucaID,e.produkcijsaKucaNaziv,e.kategorije)
    kuca.iscrtaj(host);
}
    
