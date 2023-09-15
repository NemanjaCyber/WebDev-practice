export class Sajt{
    constructor(id,naziv,podrucja,cvetovi,listovi,stabla){
        this.id=id;
        this.naziv=naziv;
        this.podrucja=podrucja;
        this.cvetovi=cvetovi;
        this.listovi=listovi;
        this.stabla=stabla;
        this.biljkeZaPrikaz=[]
        this.kontejner=null;
    }

    iscrtaj(host){

        let podrucaJednom=[];
        let cvetoviJednom=[];
        let listoviJednom=[];
        let stablaJednom=[];

        this.podrucja.forEach(x=>{
            if(!podrucaJednom.includes(x)){
                podrucaJednom.push(x);
            }
        })

        this.cvetovi.forEach(x=>{
            if(!cvetoviJednom.includes(x)){
                cvetoviJednom.push(x);
            }
        })

        this.listovi.forEach(x=>{
            if(!listoviJednom.includes(x)){
                listoviJednom.push(x);
            }
        })

        this.stabla.forEach(x=>{
            if(!stablaJednom.includes(x)){
                stablaJednom.push(x);
            }
        })

        let sveK=document.createElement("div");
        sveK.className="sveK";
        this.kontejner=sveK;
        host.appendChild(sveK);

        let nazivK=document.createElement("div");
        nazivK.className="nazivK";
        sveK.appendChild(nazivK);

        let nazivL=document.createElement("label");
        nazivL.className="nazivL";
        nazivL.innerHTML=this.naziv;
        nazivK.appendChild(nazivL);

        let doleK=document.createElement("div");
        doleK.className="doleK";
        sveK.appendChild(doleK);

        let formaK=document.createElement("div");
        formaK.className="formaK";
        doleK.appendChild(formaK);

        let prikazK=document.createElement("div");
        prikazK.className="prikazK";
        doleK.appendChild(prikazK);

        let podrucjeK=document.createElement("div");
        podrucjeK.className="podrucjeK";
        formaK.appendChild(podrucjeK);

        let podrucjeL=document.createElement("label");
        podrucjeL.className="podrucjeL";
        podrucjeL.innerHTML="Podrucje:  "
        podrucjeK.appendChild(podrucjeL);

        let podrucjeI=document.createElement("input");
        podrucjeI.type="text"
        podrucjeI.className="podrucjeI";
        podrucjeI.defaultValue=podrucaJednom[0];
        podrucjeK.appendChild(podrucjeI);

        let cvetK=document.createElement("div");
        cvetK.className="cvetK";
        formaK.appendChild(cvetK);

        let cvetL=document.createElement("label");
        cvetL.className="cvetL";
        cvetL.innerHTML="Cvet:  ";
        cvetK.appendChild(cvetL);

        let cvetI=document.createElement("input");
        cvetI.type="text";
        cvetI.className="cvetI";
        cvetI.defaultValue=cvetoviJednom[0];
        cvetK.appendChild(cvetI);

        let listK=document.createElement("div");
        listK.className="listK";
        formaK.appendChild(listK);

        let listL=document.createElement("label");
        listL.className="listL";
        listL.innerHTML="List:  ";
        listK.appendChild(listL);

        let listI=document.createElement("input");
        listI.className="listI";
        listI.type="text";
        listI.defaultValue=listoviJednom[0];
        listK.appendChild(listI);

        let stabloK=document.createElement("div");
        stabloK.className="stabloK";
        formaK.appendChild(stabloK);

        let stabloL=document.createElement("label");
        stabloL.className="stabloL";
        stabloL.innerHTML="Stablo:  "
        stabloK.appendChild(stabloL);

        let stabloI=document.createElement("input");
        stabloI.className="stabloI";
        stabloI.type="text";
        stabloI.defaultValue=stablaJednom[0];
        stabloK.appendChild(stabloI);

        let pretraziK=document.createElement("div");
        pretraziK.className="pretraziK";
        formaK.appendChild(pretraziK);

        let pretraziBtn=document.createElement("button");
        pretraziBtn.className="pretraziBtn";
        pretraziBtn.innerHTML="Pretrazi";
        pretraziK.appendChild(pretraziBtn);

        pretraziBtn.onclick=(ev)=>this.iscrtajPrikaz(prikazK);

    }

    async iscrtajPrikaz(host){
        host.innerHTML="";
        this.biljkeZaPrikaz=[];

        let podrucje=document.querySelector(".podrucjeI").value;
        //console.log(podrucje);

        let cvet=document.querySelector(".cvetI").value;
        //console.log(cvet);

        let list=document.querySelector(".listI").value;
        //console.log(list);

        let stablo=document.querySelector(".stabloI").value;
        //console.log(stablo);

        const respone=await fetch("https://localhost:7193/Ispit/Pretrazi/"+podrucje+"/"+cvet+"/"+list+"/"+stablo+"/"+this.id);
        const data=await respone.json();
        data.forEach(info=>{
            info.biljke.forEach(x=>{
                this.biljkeZaPrikaz.push(x);
            })
        })

        if(this.biljkeZaPrikaz.length===0){
            fetch("https://localhost:7193/Ispit/DodajNepoznatuBiljku/"+podrucje+"/"+cvet+"/"+list+"/"+stablo+"/"+this.id,{
                method:"POST"
            }).then(respone=>{respone.json().then(info=>{
                alert("Nepoznata biljka je dodata u tabelu");
            })})
        }
        //console.log(biljkeZaPrikaz);

        this.biljkeZaPrikaz.forEach(b=>{

            let biljkaK=document.createElement("div");
            biljkaK.className="biljkaK";
            host.appendChild(biljkaK);

            let slika=document.createElement("img");
            slika.className="slika";
            slika.src="jesenje-lisce.jpg"
            biljkaK.appendChild(slika);

            let imeK=document.createElement("div");
            imeK.className="imeK";
            biljkaK.appendChild(imeK);

            let imeL=document.createElement("label");
            imeL.className="imeL";
            imeL.innerHTML=b.naziv;
            imeK.appendChild(imeL);

            let kolicinaK=document.createElement("div");
            kolicinaK.className="kolicinaK";
            biljkaK.appendChild(kolicinaK);

            let kolicinaL=document.createElement("label");
            kolicinaL.className="kolicinaL";
            kolicinaL.innerHTML=b.vidjanja;
            kolicinaK.appendChild(kolicinaL);

            biljkaK.onclick=(ev)=>this.dodajVidjanje(host,b.id);

        })
    }

    dodajVidjanje(host,id){
        fetch("https://localhost:7193/Ispit/DodajVidjenje/"+id,{
            method:"PUT"
        }).then(response=>{response.json().then(info=>{
            this.biljkeZaPrikaz.forEach(b=>{
                if(b.id===id){
                    b.vidjanja++;
                }
            })
            this.iscrtajPrikaz(host);
        })})
    }
}