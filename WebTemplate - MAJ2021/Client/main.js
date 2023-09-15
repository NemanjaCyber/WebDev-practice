import { Fabrika } from "./Fabrika.js";

let nizFabrika=[];

let glavniKontejner=document.createElement("div");
glavniKontejner.classList="glavniKontejner";
document.body.appendChild(glavniKontejner);

let p =await fetch("https://localhost:7193/Ispit/VratiFabrike");
let podaci=await p.json();

for(let f in podaci){
    const ff=new Fabrika(f.id,f.naziv,f.silosi)
    ff.iscrtajFabriku(glavniKontejner);
}