import { Sajt } from "./Sajt.js";

const host =document.body;

const respone=await fetch("https://localhost:7193/Ispit/VratiSajtoveSaParametrima")
const data=await respone.json();

for(let a of data){
    //console.log(a);
    let s=new Sajt(a.id,a.naziv,a.podrucja,a.cvetovi,a.listovi,a.stabla);
    //console.log(s);
    s.iscrtaj(host);
}