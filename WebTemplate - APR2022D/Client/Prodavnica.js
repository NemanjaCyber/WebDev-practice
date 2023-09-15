export class Prodavnica{
    constructor(id,naziv,tipovi,brendovi){
        this.id=id;
        this.naziv=naziv;
        this.tipovi=tipovi;
        this.brendovi=brendovi;
        this.kontejner=null;
        this.komponenteZaPrikaz=[];
    }

    iscrtaj(host){

        let sveK=document.createElement("div");
        sveK.className="sveK"
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

        let konfiguracijaK=document.createElement("div");
        konfiguracijaK.className="konfiguracijaK";
        sveK.appendChild(konfiguracijaK);

        let tipK=document.createElement("div");
        tipK.className="tipK";
        formaK.appendChild(tipK);

        let tipL=document.createElement("label");
        tipL.className="tipL";
        tipL.innerHTML="Tip:  ";
        tipK.appendChild(tipL);

        let tipSelect=document.createElement("select");
        tipSelect.className="tipSelect";
        this.tipovi.forEach(x=>{
            let opcija=document.createElement("option");
            opcija.innerHTML=x;
            opcija.value=x;
            tipSelect.appendChild(opcija);
        })
        tipK.appendChild(tipSelect);

        let brendK=document.createElement("div");
        brendK.className="brendK";
        formaK.appendChild(brendK);

        let brendL=document.createElement("label");
        brendL.className="brendL";
        brendL.innerHTML="Brend:  ";
        brendK.appendChild(brendL);

        let brendSelect=document.createElement("select");
        brendSelect.className="brendSelect";
        let defOp=document.createElement("option");
        defOp.value="";
        defOp.innerHTML="";
        brendSelect.appendChild(defOp);
        this.brendovi.forEach(x=>{
            let opcija=document.createElement("option");
            opcija.innerHTML=x;
            opcija.value=x;
            brendSelect.appendChild(opcija);
        })
        brendK.appendChild(brendSelect);

        let cenaodK=document.createElement("div");
        cenaodK.className="cenaodK";
        formaK.appendChild(cenaodK);

        let cenaodL=document.createElement("label");
        cenaodL.className="cenaodL";
        cenaodL.innerHTML="Cena od:  ";
        cenaodK.appendChild(cenaodL);

        let cenaodI=document.createElement("input");
        cenaodI.className="cenaodI";
        cenaodI.type="number";
        cenaodI.min=0;
        cenaodK.appendChild(cenaodI);

        let cenadoK=document.createElement("div");
        cenadoK.className="cenadoK";
        formaK.appendChild(cenadoK);

        let cenadoL=document.createElement("label");
        cenadoL.className="cenadoL";
        cenadoL.innerHTML="Cena do:  ";
        cenadoK.appendChild(cenadoL);

        let cenadoI=document.createElement("input");
        cenadoI.className="cenadoI";
        cenadoI.type="number";
        cenadoI.min=0;
        cenadoK.appendChild(cenadoI);

        let dugmeK=document.createElement("div");
        dugmeK.className="dugmeK";
        formaK.appendChild(dugmeK);

        let pretraziBtn=document.createElement("button");
        pretraziBtn.className="pretraziBtn";
        pretraziBtn.innerHTML="Pretrazi"
        dugmeK.appendChild(pretraziBtn);

        pretraziBtn.onclick=(ev)=>this.iscrtajPrikaz(prikazK,konfiguracijaK);
    }

    async iscrtajPrikaz(host,host2){
        host.innerHTML="";
        this.komponenteZaPrikaz=[];

        let tip=document.querySelector(".tipSelect").value;
        console.log(tip);
        let brend=document.querySelector(".brendSelect").value;
        console.log(brend);
        let cenaod=document.querySelector(".cenaodI").value;
        console.log(cenaod);
        let cenado=document.querySelector(".cenadoI").value;
        console.log(cenado);

        if(brend=="" && cenaod=="" && cenado==""){
            const response=await fetch("https://localhost:7193/Ispit/PretraziPoTipu/"+tip+"/"+this.id);
            const data=await response.json()
            data.forEach(info=>{
                info.podaci.forEach(x=>{
                    this.komponenteZaPrikaz.push(x);
                })
            })
            console.log(this.komponenteZaPrikaz);
        }
        else if(cenaod=="" && cenado==""){
            const response=await fetch("https://localhost:7193/Ispit/PretraziPoTipuIBrendu/"+tip+"/"+brend+"/"+this.id);
            const data=await response.json();
            data.forEach(info=>{
                info.podaci.forEach(x=>{
                    this.komponenteZaPrikaz.push(x);
                })
            })
            console.log(this.komponenteZaPrikaz);
        }
        else{
            const response=await fetch("https://localhost:7193/Ispit/PretraziPoSvemu/"+tip+"/"+brend+"/"+cenaod+"/"+cenado+"/"+this.id);
            const data=await response.json()
            data.forEach(info=>{
                info.podaci.forEach(x=>{
                    this.komponenteZaPrikaz.push(x);
                })
            })
            console.log(this.komponenteZaPrikaz);
        }

        this.komponenteZaPrikaz.forEach(k=>{

            let komponentaK=document.createElement("div");
            komponentaK.className="komponentaK"
            host.appendChild(komponentaK);

            let sifraK=document.createElement("div");
            sifraK.className="sifraK";
            komponentaK.appendChild(sifraK);

            let sifraL=document.createElement("label");
            sifraL.className="sifraL";
            sifraL.innerHTML="Sifra: "+k.sifra;
            sifraK.appendChild(sifraL);

            let nazivKK=document.createElement("div");
            nazivKK.className="nazivKK";
            komponentaK.appendChild(nazivKK);

            let nazivKL=document.createElement("label");
            nazivKL.className="nazivKL";
            nazivKL.innerHTML="Naziv: "+k.naziv;
            nazivKK.appendChild(nazivKL);

            let cenaK=document.createElement("div");
            cenaK.className="cenaK";
            komponentaK.appendChild(cenaK);

            let cenaL=document.createElement("label");
            cenaL.className="cenaL";
            cenaL.innerHTML="Cena: "+k.cena+"RSD";
            cenaK.appendChild(cenaL);

            let kolicinaK=document.createElement("div");
            kolicinaK.className="kolicinaK";
            komponentaK.appendChild(kolicinaK);

            let kolicinaL=document.createElement("label");
            kolicinaL.className="kolicinaL";
            kolicinaL.innerHTML="Kolicina: "+k.kolicina;
            kolicinaK.appendChild(kolicinaL);

            let konfigK=document.createElement("div");
            konfigK.className="konfigK";
            komponentaK.appendChild(konfigK);

            let konfBtn=document.createElement("button");
            konfBtn.className="konfBtn";
            konfBtn.innerHTML="Konfigurisi";
            konfigK.appendChild(konfBtn);

            konfBtn.onclick=(ev)=>this.iscrtajKonfiguraciju(host2);
        })
    }

    iscrtajKonfiguraciju(host){
        host.innerHTML=""

        let sifra=document.querySelector(".sifraL").innerHTML;
        console.log(sifra);

        let naziv=document.querySelector(".nazivL").innerHTML;
        console.log(naziv);

        let kolicina=document.querySelector(".kolicinaL").innerHTML;
        console.log(kolicina);

        let cena=document.querySelector(".cenaL").innerHTML;
        console.log(cena);

        
    }
}