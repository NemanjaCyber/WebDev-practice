export class Gradovi{
    constructor(id,ime,x,y,meteoroloskiPodaci){
        this.id=id;
        this.ime=ime;
        this.x=x;
        this.y=y;
        this.meteoroloskiPodaci=meteoroloskiPodaci;
        this.kontejner=null;
    }

    iscrtaj(host){

        let gradskiKontejner=document.createElement("div");
        this.kontejner=gradskiKontejner;
        gradskiKontejner.className="gradskiKontejner";
        host.appendChild(gradskiKontejner);

        let leviKontejner=document.createElement("div");
        leviKontejner.className="leviKontejner";
        gradskiKontejner.appendChild(leviKontejner);

        let desniKontejner=document.createElement("div");
        desniKontejner.className="desniKontejner";
        gradskiKontejner.appendChild(desniKontejner);

        let gradInfoKontejner=document.createElement("div");
        gradInfoKontejner.className="gradInfoKontejner";
        gradInfoKontejner.innerHTML="Grad "+this.ime+" ("+this.x+" N, "+this.y+" E), godina 2020";
        leviKontejner.appendChild(gradInfoKontejner);
    
        let gradSelectKontejner=document.createElement("div");
        gradSelectKontejner.className="gradSelectKontejner";
        leviKontejner.appendChild(gradSelectKontejner);
        let niz=["Temperatura","Padavine","Suncani Dani"];
        niz.forEach(n=>{
            let btn=document.createElement("input");
            btn.type="radio";
            btn.className="izbor";
            btn.name=this.ime;
            btn.value=n;
            gradSelectKontejner.appendChild(btn);

            let labela=document.createElement("label");
            labela.innerHTML=n;
            gradSelectKontejner.appendChild(labela);

            if(n==="Temperatura")
            {
                btn.checked=true;
            }

        })

        let dugmeKontejner=document.createElement("div");
        dugmeKontejner.className="dugmeKontejner";
        leviKontejner.appendChild(dugmeKontejner);
        let prikaziBtn=document.createElement("button");
        prikaziBtn.className="prikaziBtn";
        prikaziBtn.innerHTML="Prikazi";
        dugmeKontejner.appendChild(prikaziBtn);

        let meseciKontejner=document.createElement("div");
        meseciKontejner.className="meseciKontejner";
        leviKontejner.appendChild(meseciKontejner);

        this.iscrtajMesece(meseciKontejner);
        prikaziBtn.onclick=(ev)=>this.iscrtajMesece(meseciKontejner);
    }

    iscrtajMesece(host){

        let opcija=document.querySelector(".izbor:checked").value;

        this.meteoroloskiPodaci.forEach(e=>{

            let mesecKontejner=document.createElement("div");
            mesecKontejner.className="mesecKontejner";
            host.appendChild(mesecKontejner);

            if(opcija==="Temperatura"){
                let mesecInfo=document.createElement("label");
                mesecInfo.className="mesecInfo";
                mesecInfo.innerHTML=e.nazivMeseca +"<br>"+e.temperatura;
                mesecKontejner.appendChild(mesecInfo);
            
                let mesecSkala=document.createElement("div");
                mesecSkala.className="mesecSkala";
                mesecSkala.style.height=`${e.temperatura}%`;
                mesecKontejner.appendChild(mesecSkala);
            }

            if(opcija==="Padavine"){
                let mesecInfo=document.createElement("label");
                mesecInfo.className="mesecInfo";
                mesecInfo.innerHTML=e.nazivMeseca+"<br>"+e.kolicinaPadavina;
                mesecKontejner.appendChild(mesecInfo);
    
                let mesecSkala = document.createElement("div");
                mesecSkala.className="mesecSkala";
                mesecSkala.style.height=`${e.kolicinaPadavina/100}%`;
                mesecKontejner.appendChild(mesecSkala);
            }

            if(opcija==="Suncani Dani"){
                let mesecInfo=document.createElement("label");
                mesecInfo.className="mesecInfo";
                mesecInfo.innerHTML=e.nazivMeseca+"<br>"+e.suncaniDani;
                mesecKontejner.appendChild(mesecInfo);
    
                let mesecSkala = document.createElement("div");
                mesecSkala.className="mesecSkala";
                mesecSkala.style.height=`${e.suncaniDani}%`;
                mesecKontejner.appendChild(mesecSkala);

                
            }
        })
    }
}