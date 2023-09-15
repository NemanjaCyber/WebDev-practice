export class Prodavnica{
    constructor(marke,id){
        this.id=id;
        this.marke=marke;
        this.kontejner=null;
    }

    iscrtaj(host){
        let sveKontejner=document.createElement("div");
        sveKontejner.className="sveKontejner";
        this.kontejner=sveKontejner;
        host.appendChild(sveKontejner);

        let formaKontejner=document.createElement("div");
        formaKontejner.className="formaKontejner";
        sveKontejner.appendChild(formaKontejner);

        let prikazKontejner=document.createElement("div");
        prikazKontejner.className="prikazKontejner";
        sveKontejner.appendChild(prikazKontejner);

        let markeKontejner=document.createElement("div");
        markeKontejner.className="markeKontejner";
        formaKontejner.appendChild(markeKontejner);

        let markeLab=document.createElement("label");
        markeLab.className="markeLab";
        markeLab.innerHTML="Marke: "
        markeKontejner.appendChild(markeLab);

        let markePoJednom=[];
        this.marke.forEach(x=>{
            if(!markePoJednom.includes(x)){
                markePoJednom.push(x);
            }
        })

        let markaSelect=document.createElement("select");
        markaSelect.className="markaSelect";
        markePoJednom.forEach(x=>{
            let opcija=document.createElement("option");
            opcija.value=x;
            opcija.innerHTML=x;
            markaSelect.appendChild(opcija);
        })
        markeKontejner.appendChild(markaSelect);

        let modelKontejner=document.createElement("div");
        modelKontejner.className="modelKontejner";
        formaKontejner.appendChild(modelKontejner);

        let bojaKontejner=document.createElement("div");
        bojaKontejner.className="bojaKontejner";
        formaKontejner.appendChild(bojaKontejner);

        let dugme=document.createElement("button");
        dugme.className="dugme";
        dugme.innerHTML="Pretrazi";
        formaKontejner.appendChild(dugme);

        this.iscrtajModele(modelKontejner,bojaKontejner,prikazKontejner,markaSelect.value,dugme)
        markaSelect.onchange=(ev)=>this.iscrtajModele(modelKontejner,bojaKontejner,prikazKontejner,markaSelect.value,dugme)
    }

    async iscrtajModele(modelKontejner,bojaKontejner,prikazKontejner,marka,dugme){
        modelKontejner.innerHTML=""
        
        let modeliPoJednom=[];

        const response=await fetch("https://localhost:7193/Ispit/VratiModeleZaMarku/"+marka+"/"+this.id);
        const data=await response.json();
        data.forEach(info=>{//info je stvarno ono sto se prikaze u sweger
            info.modeli.forEach(model=>{
                if(!modeliPoJednom.includes(model)){
                    modeliPoJednom.push(model);
                }
            })
        })
        // fetch("https://localhost:7193/Ispit/VratiModeleZaMarku/"+marka+"/"+this.id,{
        // }).then(promise=>{promise.json().then(info=>{
        //     info.forEach(p=>{
        //         p.modeli.forEach(model=>{
        //             if(!modeliPoJednom.includes(model)){
        //                 modeliPoJednom.push(model);
        //             }
        //         })
        //     })
        // })}

        // )

        //console.log(modeliPoJednom);

        let modelLab=document.createElement("lab");
        modelLab.className="modelLab";
        modelLab.innerHTML="Model: "
        modelKontejner.appendChild(modelLab);

        let modelSelect=document.createElement("select");
        modelSelect.className="modelSelect";
        let opcija1=document.createElement("option");
        opcija1.value="/";
        opcija1.innerHTML="";
        modelSelect.appendChild(opcija1);
        modeliPoJednom.forEach(x=>{
            let opcija=document.createElement("option");
            opcija.value=x;
            opcija.innerHTML=x;
            modelSelect.appendChild(opcija);
        })
        modelKontejner.appendChild(modelSelect);

        this.iscrtajBoje(bojaKontejner,prikazKontejner,marka,modelSelect.value,dugme);
        modelSelect.onchange=(ev)=>this.iscrtajBoje(bojaKontejner,prikazKontejner,marka,modelSelect.value,dugme);

    }

    async iscrtajBoje(bojaKontejner,prikazKontejner,marka,model,dugme){
        bojaKontejner.innerHTML=""

        let bojePoJednom=[]

        const response=await fetch("https://localhost:7193/Ispit/VratiBojeZaMarkuModel/"+marka+"/"+model+"/"+this.id);
        const data= await response.json();
        data.forEach(info=>{
            info.boje.forEach(boja=>{
                if(!bojePoJednom.includes(boja)){
                    bojePoJednom.push(boja);
                }
            })
        })

        let bojaLab=document.createElement("label");
        bojaLab.className="bojaLab";
        bojaLab.innerHTML="Boja: "
        bojaKontejner.appendChild(bojaLab);

        let bojaSelect=document.createElement("select");
        bojaSelect.className="bojaSelect";
        let opcija1=document.createElement("option");
        opcija1.value="/"
        opcija1.innerHTML=""
        bojaSelect.appendChild(opcija1);
        bojePoJednom.forEach(x=>{
            let opcija=document.createElement("option");
            opcija.value=x;
            opcija.innerHTML=x;
            bojaSelect.appendChild(opcija);
        })
        bojaKontejner.appendChild(bojaSelect);

        dugme.onclick=(ev)=>this.iscrtajPretragu(prikazKontejner,marka,model,bojaSelect.value);

    }

    async iscrtajPretragu(prikazKontejner,marka,model,boja){
        prikazKontejner.innerHTML=""

        let nizAutomobilaNaOsnovuPretrage=[]

        const response=await fetch("https://localhost:7193/Ispit/VratiPoPretrazi/"+marka+"/"+model+"/"+boja+"/"+this.id);
        const data=await response.json();
        data.forEach(info=>{
            info.forEach(auto=>{
                nizAutomobilaNaOsnovuPretrage.push(auto);
            })
        })

        //console.log(nizAutomobilaNaOsnovuPretrage);

        this.crtanje(prikazKontejner, nizAutomobilaNaOsnovuPretrage);
    }

    crtanje(prikazKontejner, nizAutomobilaNaOsnovuPretrage){
        prikazKontejner.innerHTML=""

        nizAutomobilaNaOsnovuPretrage.forEach(a=>{
            if(a.kolicina!=0){

                let automobilKontejner=document.createElement("div");
                automobilKontejner.className="automobilKontejner";
                prikazKontejner.appendChild(automobilKontejner);

                let aMarkaKontejner=document.createElement("div");
                aMarkaKontejner.className="aMarkaKontejner";
                automobilKontejner.appendChild(aMarkaKontejner);

                let aMarkaLab=document.createElement("label");
                aMarkaLab.className="aMarkaLab";
                aMarkaLab.innerHTML="Marka: "+a.marka;

                let aModelKontejner=document.createElement("div");
                aModelKontejner.className="aModelKontejner";
                automobilKontejner.appendChild(aModelKontejner);

                let aModelLab=document.createElement("label");
                aModelLab.className="aModelLab";
                aModelLab.innerHTML="Model: "+a.model;

                let aSlika=document.createElement("img");
                aSlika.className="aSlika";
                aSlika.src="gg.jpg";
                automobilKontejner.appendChild(aSlika);

                let aKolicina=document.createElement("div");
                aKolicina.className="aKolicina";
                automobilKontejner.appendChild(aKolicina);

                let aKolicinaLab=document.createElement("label")
                aKolicinaLab.className="aKolicinaLab";
                aKolicinaLab.innerHTML="Kolicina: "+a.kolicina;
                automobilKontejner.appendChild(aKolicinaLab);

                let automobilDatum = document.createElement("div");
                    automobilDatum.className="automobilDatum";
                    automobilKontejner.appendChild(automobilDatum);

                    let DatumLabel=document.createElement("label");
                    DatumLabel.className="DatumLabel";
                    DatumLabel.innerHTML="Datum: "+ a.datumPoslednjeProdaje;
                    automobilDatum.appendChild(DatumLabel);

                    let automobilCena = document.createElement("div");
                    automobilCena.className="automobilCena";
                    automobilKontejner.appendChild(automobilCena);

                    let CenaLabel=document.createElement("label");
                    CenaLabel.className="CenaLabel";
                    CenaLabel.innerHTML="Cena: "+ a.cena;
                    automobilCena.appendChild(CenaLabel);


            }
        })
    }

}