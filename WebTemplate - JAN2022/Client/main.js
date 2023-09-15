import { Produkcija } from "./Produkcija.js";

let nizProdukcija=[];

let glavniKontejner=document.createElement("div");
glavniKontejner.className="glavniKontejner";
document.body.appendChild(glavniKontejner);

function iscrtajInstance(){
    nizProdukcija.forEach(p=>{
        p.iscrtajProdukciju(glavniKontejner);
    })
}

fetch("https://localhost:7193/Ispit/VratiProdukcijeSaKategorijama",{
    method:"GET"
}).then(data=> { data.json().then(info=> {
    info.forEach(p=>{
        let produkcija = new Produkcija(p.id,p.naziv,p.kategorije);
        nizProdukcija.push(produkcija);
    })
    iscrtajInstance();
})})