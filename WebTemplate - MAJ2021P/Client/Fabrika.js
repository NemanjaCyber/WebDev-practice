export class Farbika{
    constructor(id,naziv,silosi){
        this.id=id;
        this.naziv=naziv;
        this.silosi=silosi;
        this.kontejner=null;
    }

    iscrtaj(host){

        let sveKontejner=document.createElement("div");
        sveKontejner.className="sveKontejner";
        this.kontejner=sveKontejner;
        host.appendChild(sveKontejner);

        let leviKontejner=document.createElement("div");
        leviKontejner.className="leviKontejner";
        sveKontejner.appendChild(leviKontejner);

        let formaKontejner=document.createElement("div");
        formaKontejner.className="formaKontejner";
        sveKontejner.appendChild(formaKontejner);

        let nazivLab=document.createElement("label")
        nazivLab.className="nazivLab";
        nazivLab.innerHTML=this.naziv;
        leviKontejner.appendChild(nazivLab);

        let silosiKontejner=document.createElement("div")
        silosiKontejner.className="silosiKontejner";
        leviKontejner.appendChild(silosiKontejner);

        this.iscrtajSilose(silosiKontejner);
        this.iscrtajForme(formaKontejner,silosiKontejner);
    }

    iscrtajSilose(host){

        host.innerHTML=""

        this.silosi.forEach(s=>{

            let silosKontejner=document.createElement("div");
            silosKontejner.className="silosKontejner";
            host.appendChild(silosKontejner);

            let skalaKontejner=document.createElement("div");
            skalaKontejner.className="skalaKontejner";
            let visinaSkale=(s.trenutnaPopunjenost/s.kapacitet)*100;
            skalaKontejner.style.height=`${visinaSkale}%`;
            silosKontejner.appendChild(skalaKontejner);

            let infokontejner=document.createElement("div");
            infokontejner.className="infoKontejner";
            silosKontejner.appendChild(infokontejner);

            let podaciLab=document.createElement("label");
            podaciLab.className="podaciLab";
            podaciLab.innerHTML=s.oznaka+"<br>"+s.trenutnaPopunjenost+"t/"+s.kapacitet+"t<br>"
            infokontejner.appendChild(podaciLab);
        })
    }

    iscrtajForme(host,silosiKontejner){
        host.innerHTML=""

        let izborKontejner=document.createElement("div");
        izborKontejner.className="izborkontejner";
        host.appendChild(izborKontejner);

        let silosLab=document.createElement("label");
        silosLab.className="silosLab";
        silosLab.innerHTML="Silos: "
        izborKontejner.appendChild(silosLab);

        let izborSelect=document.createElement("select");
        izborSelect.className="izborSelect";
        this.silosi.forEach(s=>{
            let opcija=document.createElement("option");
            opcija.value=s.idSilos;
            opcija.innerHTML=s.oznaka;
            izborSelect.appendChild(opcija);
        })
        izborKontejner.appendChild(izborSelect);

        let dodajKontejner=document.createElement("div");
        dodajKontejner.className="dodajKontejner";
        host.appendChild(dodajKontejner);

        let kolicinaLab=document.createElement("label");
        kolicinaLab.className="kolicinaLab";
        kolicinaLab.innerHTML="Kolicina: ";
        dodajKontejner.appendChild(kolicinaLab);

        let kolicinaInput=document.createElement("input");
        kolicinaInput.type="number";
        kolicinaInput.min=0;
        dodajKontejner.appendChild(kolicinaInput);

        let sipajBtn=document.createElement("button");
        sipajBtn.className="sipajBtn";
        sipajBtn.innerHTML="Sipaj u silos"
        host.appendChild(sipajBtn);

        sipajBtn.onclick=(ev)=>this.dodaj(izborSelect.value,kolicinaInput.value,silosiKontejner)
    }

    dodaj(izbor,kolicina,kontejnerZaCrtanje){
        this.silosi.forEach(s=>{
            if(s.id==izbor){
                if(s.trenutnaPopunjenost+kolicina>s.kapacitet){
                    alert("Prevelika kolicina");
                }
            }
        })

        fetch("https://localhost:7193/Ispit/SipajUSilos/"+izbor+"/"+kolicina,{
            method:"PUT"
        }).then(response=>{
            response.json().then(data=>{
                this.silosi.forEach(x=>{
                    if(x.id==izbor){
                        x.trenutnaPopunjenost+=parseInt(kolicina);
                    }
                })
                this.iscrtajSilose(kontejnerZaCrtanje);
            })
        })
    }
}