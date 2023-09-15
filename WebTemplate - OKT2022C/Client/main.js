import { Silosi } from "./Silosi.js";

const host=document.body;

const response=await fetch("https://localhost:7193/Ispit/VratiOznake")
const data=await response.json();

const resposne2=await fetch("https://localhost:7193/Ispit/VratiRadnike")
const data2=await resposne2.json();

let oznakeNiz=[];
let oznakeId=[];
let imenaNiz=[];
let imenaId=[];

for(let a of data){
    oznakeNiz.push(a.oznake);
    oznakeId.push(a.id);
}
for(let a of data2){
    imenaNiz.push(a.imena);
    imenaId.push(a.radnikId);
}
// console.log(oznakeNiz);
// console.log(imenaNiz);
// console.log(imenaId);
// console.log(oznakeId);

let silosiObj=new Silosi(oznakeNiz,imenaNiz,oznakeId,imenaId);
//console.log(silosiObj);

silosiObj.iscrtaj(host);