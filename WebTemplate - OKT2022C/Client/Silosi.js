export class Silosi{
    constructor(silosi,radnici,silosiId,radniciId){
        this.silosi=silosi;
        this.radnici=radnici;
        this.silosiId=silosiId;
        this.radniciId=radniciId;
        this.kontejner=null;
        this.silosiZaPrikaz=[];
    }

    async iscrtaj(host){
        const response=await fetch("https://localhost:7193/Ispit/VratiSveSilose");
        const data=await response.json();
        data.forEach(info=>{
            this.silosiZaPrikaz.push(info);
        })

        console.log(this.silosiZaPrikaz);
        
        let sveK=document.createElement("div");
        sveK.className="sveK";
        this.kontejner=sveK;
        host.appendChild(sveK);

        let leviDeo=document.createElement("div");
        leviDeo.className="leviDeo";
        sveK.appendChild(leviDeo);

        let desniDeo=document.createElement("div");
        desniDeo.className="desniDeo";
        sveK.appendChild(desniDeo);

        let silosK=document.createElement("div");
        silosK.className="silosK";
        leviDeo.appendChild(silosK);

        let silosL=document.createElement("label");
        silosL.className="silosL";
        silosL.innerHTML="Silos: "
        silosK.appendChild(silosL);

        let silosSelect=document.createElement("select");
        silosSelect.className="silosSelect";
        this.silosi.forEach((x,index)=>{
            const y=this.silosiId[index];
            let opcija=document.createElement("option");
            opcija.innerHTML=x;
            opcija.value=y;
            silosSelect.appendChild(opcija);
        })
        silosK.appendChild(silosSelect);

        let kolicinaK=document.createElement("div");
        kolicinaK.className="kolicinaK";
        leviDeo.appendChild(kolicinaK);

        let kolicinaL=document.createElement("label");
        kolicinaL.className="kolicinaL";
        kolicinaL.innerHTML="Kolicina: ";
        kolicinaK.appendChild(kolicinaL);

        let kolicinaI=document.createElement("input");
        kolicinaI.className="kolicinaI";
        kolicinaI.type="number";
        kolicinaI.min=0;
        kolicinaK.appendChild(kolicinaI);

        let vlaznostK=document.createElement("div");
        vlaznostK.className="vlaznostK";
        leviDeo.appendChild(vlaznostK);

        let vlaznostL=document.createElement("label");
        vlaznostL.className="vlaznostL";
        vlaznostL.innerHTML="Vlaznost: "
        vlaznostK.appendChild(vlaznostL);

        let vlaznostI=document.createElement("input");
        vlaznostI.className="vlaznostI";
        vlaznostI.type="number";
        vlaznostI.min=0;
        vlaznostK.appendChild(vlaznostI);

        let dugmeK=document.createElement("div");
        dugmeK.className="dugmeK";
        leviDeo.appendChild(dugmeK);

        let upisiBtn=document.createElement("button");
        upisiBtn.innerHTML="Upisi";
        dugmeK.appendChild(upisiBtn);

        let imeK=document.createElement("div");
        imeK.className="imeK";
        leviDeo.appendChild(imeK);

        let imeL=document.createElement("label");
        imeL.className="imeL";
        imeL.innerHTML="Ime: "
        imeK.appendChild(imeL);

        let imeSelect=document.createElement("select");
        imeSelect.className="imeSelect";
        this.radnici.forEach((x,index)=>{
            let y=this.radniciId[index];
            let opcija=document.createElement("option");
            opcija.innerHTML=x;
            opcija.value=y;
            imeSelect.appendChild(opcija);
        })
        imeK.appendChild(imeSelect);

        upisiBtn.onclick=(ev)=>this.iscrtajSilose(desniDeo);

        
        this.nacrtajPocetneSilose(desniDeo);

    }

    nacrtajPocetneSilose(host){
        host.innerHTML="";
        this.silosiZaPrikaz.forEach(x=>{


            let skalaK=document.createElement("div");
            skalaK.className="skalaK";
            skalaK.style.height=`${(x.trenutnaPopunjenost/x.kapacitet*100)}%`;
            host.appendChild(skalaK);

            let infoK=document.createElement("div");
            infoK.className="infoK";
            skalaK.appendChild(infoK);

            let infoL=document.createElement("label");
            infoL.className="infoL";
            infoL.innerHTML=x.trenutnaPopunjenost+"/"+x.kapacitet+"<br>"+x.vlaznost+"%"
            infoK.appendChild(infoL)

        })
    }

    async iscrtajSilose(host){
        host.innerHTML="";

        let selektovaniSilos=document.querySelector(".silosSelect").value;
        //console.log(selektovaniSilos);

        let unetaKolicina=document.querySelector(".kolicinaI").value;
        //console.log(unetaKolicina);

        let unetaVlaznost=document.querySelector(".vlaznostI").value;
        //console.log(unetaVlaznost);

        let selektovanoIme=document.querySelector(".imeSelect").value;
        //console.log(selektovanoIme);

        if(unetaKolicina=="" && unetaVlaznost==""){
            alert("Morate uneti barem jedan od parametara");
        }

        if(unetaVlaznost<0 || unetaVlaznost>100){
            alert("Nevalidne vrednosti za vlaznost");
        }

        if(unetaVlaznost==""){
            fetch("https://localhost:7193/Ispit/DodajKolicinu/"+unetaKolicina+"/"+selektovaniSilos,{
                method:"PUT"
            }).then(response=>{response.json().then(info=>{
                this.silosiZaPrikaz.forEach(s=>{
                    if(s.idSilos===selektovaniSilos){
                        s.trenutnaPopunjenost+=unetaKolicina;
                    }
                })
                //console.log(this.silosiZaPrikaz);
            })})
        }
        else if(unetaKolicina==""){
            fetch("https://localhost:7193/Ispit/IzmeniVlaznost/"+unetaVlaznost+"/"+selektovaniSilos,{
                method:"PUT"
            }).then(response=>{response.json().then(info=>{
                this.silosiZaPrikaz.forEach(s=>{
                    if(s.idSilos===selektovaniSilos){
                        s.vlaznost=unetaVlaznost;
                    }
                })
                //console.log(this.silosiZaPrikaz);
            })})
        }
        else{
            fetch("https://localhost:7193/Ispit/IzmeniKolicinuIVlaznost/"+unetaKolicina+"/"+unetaVlaznost+"/"+selektovaniSilos,{
                method:"PUT"
            }).then(response=>{response.json().then(info=>{
                this.silosiZaPrikaz.forEach(s=>{
                    if(s.idSilos===selektovaniSilos){
                        s.trenutnaPopunjenost=unetaKolicina;
                        s.vlaznost=unetaVlaznost;
                    }
                })
                //console.log(this.silosiZaPrikaz);
            })})
        }
        console.log(this.silosiZaPrikaz);

        this.nacrtajPocetneSilose(host);

    }
}