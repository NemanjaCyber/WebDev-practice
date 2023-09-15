import { Farbika } from "./Fabrika.js";

const host=document.body

const response=await fetch("https://localhost:7193/Ispit/PreuzmiFabrikeSaSilosima")
const data=await response.json();

for(let f of data){
    //console.log(f);
    let fabrika=new Farbika(f.id,f.naziv,f.silosi);
    fabrika.iscrtaj(host);
}