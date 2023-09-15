export class Mec{
    constructor(id,lokacija,datum,s1,s2,pS11,pS12
        ,pS21,pS22,igraci)
        {
            this.id=id;
            this.lokacija=lokacija;
            this.datum=datum;
            this.s1=s1;
            this.s2=s2;
            this.pS11=pS11;
            this.pS12=pS12;
            this.pS21=pS21;
            this.pS22=pS22;
            this.igraci=igraci;
            this.kontejner=null;
        }
    
    iscrtaj(host){
        let ceoTurnir=document.createElement("div");
        this.kontejner=ceoTurnir;
        ceoTurnir.className="ceoTurnir";
        host.appendChild(ceoTurnir);

        let infoKontejner=document.createElement("div");
        infoKontejner.className="infoKontejner";
        ceoTurnir.appendChild(infoKontejner);

        let turnirLab=document.createElement("label");
        turnirLab.className="turnirLab";
        turnirLab.innerHTML="Lokacija: "+this.lokacija+"<br>"+"Vreme: "+this.datum;
        infoKontejner.appendChild(turnirLab);

        let donjiDeo=document.createElement("div");
        donjiDeo.className="donjiDeo";
        ceoTurnir.appendChild(donjiDeo);

        let prviIgracKontejner=document.createElement("div");
        prviIgracKontejner.className="prviIgracKontejner";
        donjiDeo.appendChild(prviIgracKontejner);
        let prviSlika=document.createElement("img");
        prviSlika.className="slika";
        prviSlika.src="k.png";
        prviIgracKontejner.appendChild(prviSlika);
        let prviLab=document.createElement("label");
        prviLab.className="prviLab";
        prviLab.innerHTML="<br>Ime: "+this.igraci[0].ime+
        "<br>"+"Godine: "+this.igraci[0].godine+
        "<br>"+"ATP rang: "+this.igraci[0].rank;
        prviIgracKontejner.appendChild(prviLab);

        let rezultatKontejner=document.createElement("div");
        rezultatKontejner.className="rezultatKontejner";
        donjiDeo.appendChild(rezultatKontejner);
        this.iscrtajRezultat(rezultatKontejner);

        let drugiIgracKontejner=document.createElement("div");
        drugiIgracKontejner.className="drugiIgracKontejner";
        donjiDeo.appendChild(drugiIgracKontejner);
        let drugiSlika=document.createElement("img");
        drugiSlika.className="slika";
        drugiSlika.src="g.png";
        drugiIgracKontejner.appendChild(drugiSlika);
        let drugiLab=document.createElement("label");
        drugiLab.className="drugiLab";
        drugiLab.innerHTML="<br>Ime: "+this.igraci[1].ime+
        "<br>"+"Godine: "+this.igraci[1].godine+
        "<br>"+"ATP rang: "+this.igraci[1].rank;
        drugiIgracKontejner.appendChild(drugiLab);

    }

    iscrtajRezultat(host){
        let rezultatLab=document.createElement("label");
        rezultatLab.className="rezultatLab";
        rezultatLab.innerHTML="Rezultat<br>";
        host.appendChild(rezultatLab);

        let setoviLab=document.createElement("label");
        setoviLab.className="setoviLab";
        setoviLab.innerHTML=this.s1+" - "+this.s2+"<br>";
        host.appendChild(setoviLab);

        let poeniPoSetovimaLab=document.createElement("label");
        poeniPoSetovimaLab.className="poeniPoSetovimaLab";
        poeniPoSetovimaLab.innerHTML="("+this.pS11+" - "+this.pS12+")"+", "+"("+this.pS21+" - "+this.pS22+")"+"<br>";
        host.appendChild(poeniPoSetovimaLab);

        let dugmadKontejner=document.createElement("div");
        dugmadKontejner.className="dugmadKontejner";
        host.appendChild(dugmadKontejner);

        let btn1=document.createElement("button");
        btn1.name=this.igraci[0];
        btn1.innerHTML="+";
        dugmadKontejner.appendChild(btn1);

        let btn2=document.createElement("button");
        btn2.name=this.igraci[1];
        btn2.innerHTML="+";
        dugmadKontejner.appendChild(btn2);

        btn1.onclick=(ev)=>this.prvi(host);
        btn2.onclick=(ev)=>this.drugi(host);
    }

    prvi(host){
        fetch("https://localhost:7193/Ispit/DodajPoenPrviIgrac"+this.id,{
            method:"PUT"
        }).then(data=>{
            if(data.status==400){
                alert("Kraj Meca")
            }
            data.json().then(info=>{
                if(this.s1+this.s2===2){
                    alert("Kraj Meca");
                }
                if(this.s1===0 && this.s2===0)
                {
                    this.pS11++;
                    if(this.pS11===6){
                        this.s1++;
                    }
                }
                else
                {
                    this.pS21++;
                    if(this.pS21===6){
                        this.s1++;
                    }
                }
                this.iscrtajRezultat(host);
            })
        })   
    }

    drugi(host){
        fetch("https://localhost:7193/Ispit/DodajPoenDrugiIgrac"+this.id,{
            method:"PUT"
        }).then(data=>{
            if(data.status==400){
                alert("Kraj Meca")
            }
            data.json().then(info=>{
                if(this.s1+this.s2===2){
                    alert("Kraj Meca");
                }
                if(this.s2===0 && this.s1===0)
                {
                    this.pS12++;
                    if(this.pS12===6){
                        this.s2++;
                    }
                }
                else
                {
                    this.pS222++;
                    if(this.pS22===6){
                        this.s2++;
                    }
                }
                this.iscrtajRezultat(host);
            })
        })   
    }
}