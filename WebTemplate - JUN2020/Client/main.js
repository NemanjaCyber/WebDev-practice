import { Mec } from "./Mec.js";

const host=document.body;

const g=await fetch("https://localhost:7193/Ispit/VratiMeceve");
const data=await g.json();

for(let g of data){
    //console.log(g);
    const mec=new Mec(g.id,g.lokacija,g.datum,g.s1
        ,g.s2,g.pS11,g.pS12,g.pS21,g.pS22,g.igraci);
    mec.iscrtaj(host);
}