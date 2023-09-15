export class Prodavnica{
    constructor(id,naziv,brendovi){
        this.id=id;
        this.naziv=naziv;
        this.brendovi=brendovi;
        this.kontejner=null;
        this.artikli=[];
    }

    //sve top radi, samo uvek selektuj jedno radio dugme kad pretrazujes...

    iscrtaj(host){
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

        let leviDeo=document.createElement("div");
        leviDeo.className="leviDeo";
        doleK.appendChild(leviDeo);

        let desniDeo=document.createElement("div");
        desniDeo.className="desniDeo";
        doleK.appendChild(desniDeo);

        let brendK=document.createElement("brendK");
        brendK.className="brendK";
        leviDeo.appendChild(brendK);

        let brendL=document.createElement("label");
        brendL.className="brendL";
        brendL.innerHTML="Brend: "
        brendK.appendChild(brendL);

        let brendoviPoJednom=[]
        this.brendovi.forEach(x=>{
            if(!brendoviPoJednom.includes(x)){
                brendoviPoJednom.push(x);
            }
        })

        let brendSelect=document.createElement("select");
        brendSelect.className="brendSelect";
        brendoviPoJednom.forEach(brend=>{
            let opcija=document.createElement("option");
            opcija.value=brend;
            opcija.innerHTML=brend;
            brendSelect.appendChild(opcija);
        })
        brendK.appendChild(brendSelect);

        let cenaOdK=document.createElement("div");
        cenaOdK.className="cenaOdK";
        leviDeo.appendChild(cenaOdK);

        let cenaOdL=document.createElement("label");
        cenaOdL.className="cenaOdL";
        cenaOdL.innerHTML="Cena od: ";
        cenaOdK.appendChild(cenaOdL);

        let cenaOdI=document.createElement("input");
        cenaOdI.className="cenaOdI";
        cenaOdI.type="number";
        cenaOdI.min=0;
        cenaOdK.appendChild(cenaOdI);

        let cenaDoK=document.createElement("div");
        cenaDoK.className="cenaDoK";
        leviDeo.appendChild(cenaDoK);

        let cenaDoL=document.createElement("label");
        cenaDoL.className="cenaDoL";
        cenaDoL.innerHTML="Cena do: ";
        cenaDoK.appendChild(cenaDoL);

        let cenaDoI=document.createElement("input");
        cenaDoI.className="cenaDoI";
        cenaDoI.type="number";
        cenaDoI.min=0;
        cenaDoK.appendChild(cenaDoI);

        let velicinaK=document.createElement("div");
        velicinaK.className="velicinaK";
        leviDeo.appendChild(velicinaK);

        let nizV=["S","M","L"]
        nizV.forEach(x=>{
            let velicinaL=document.createElement("label");
            velicinaL.className="velicinaL";
            velicinaL.innerHTML=x;
            velicinaK.appendChild(velicinaL);
            let radio=document.createElement("input");
            radio.type="radio"
            radio.name="ime"
            radio.value=x;
            velicinaK.appendChild(radio);
        })

        let dugmeK=document.createElement("div");
        dugmeK.className="dugmeK";
        leviDeo.appendChild(dugmeK);

        let pretraziBtn=document.createElement("button");
        pretraziBtn.className="pretraziBtn";
        pretraziBtn.innerHTML="Pretrazi";
        dugmeK.appendChild(pretraziBtn);

        pretraziBtn.onclick=(ev)=>this.iscrtajArtikle(desniDeo)
    }

    async iscrtajArtikle(host){
        host.innerHTML=""
        this.artikli=[];

        let selektovaniBrend=document.querySelector(".brendSelect").value;
        console.log(selektovaniBrend);

        let selektovanaMinCena=document.querySelector(".cenaOdI").value;
        //console.log(selektovanaMinCena);

        let selektovanaMaxCena=document.querySelector(".cenaDoI").value;
        //console.log(selektovanaMaxCena);
        
        let selektovanaVelicina=document.querySelector(`input[name="ime"]:checked`);
        console.log(selektovanaVelicina.value);
        if(selektovanaMinCena=="" && selektovanaMaxCena==""){
            const response=await fetch("https://localhost:7193/Ispit/VratiArtiklePoVelicini/"+selektovanaVelicina.value+"/"+selektovaniBrend+"/"+this.id);
            const data=await response.json();
            data.forEach(info=>{
                info.artikli.forEach(x=>{
                    this.artikli.push(x);
                })
            })
            console.log(this.artikli);
        }
        else{
            const response=await fetch("https://localhost:7193/Ispit/VratiArtikleZaSve/"+selektovanaVelicina.value+"/"+selektovanaMinCena+"/"+selektovanaMaxCena+"/"+selektovaniBrend+"/"+this.id)
            const data=await response.json();
            data.forEach(info=>{
                info.artikli.forEach(x=>{
                    this.artikli.push(x);
                })
            })
            console.log(this.artikli);
        }

        this.artikli.forEach(a=>{

            if(a.kolicina>0){
                let artiklK=document.createElement("div");
                artiklK.className="artiklK";
                host.appendChild(artiklK);
    
                let slika=document.createElement("img");
                slika.className="slika";
                slika.src="jesenje-lisce.jpg"
                artiklK.appendChild(slika);
    
                let sifraK=document.createElement("div");
                sifraK.className="sifraK";
                artiklK.appendChild(sifraK);
    
                let sifraL=document.createElement("label");
                sifraL.className="sifraL";
                sifraL.innerHTML=a.sifra;
                sifraK.appendChild(sifraL);
    
                let kolicinaK=document.createElement("div");
                kolicinaK.className="kolicinaK";
                artiklK.appendChild(kolicinaK);
    
                let kolicinaL=document.createElement("label");
                kolicinaL.className="kolicinaL";
                kolicinaL.innerHTML="KOLICINA: "+a.kolicina;
                kolicinaK.appendChild(kolicinaL);
    
                let cenaK=document.createElement("div");
                cenaK.className="cenaK";
                artiklK.appendChild(cenaK);
    
                let cenaL=document.createElement("label");
                cenaL.className="cenaL";
                cenaL.innerHTML="CENA: "+a.cena+" RSD";
                cenaK.appendChild(cenaL);
    
                let kupiK=document.createElement("div");
                kupiK.className="kupiK";
                artiklK.appendChild(kupiK);
    
                let kupiBtn=document.createElement("button");
                kupiBtn.className="kupiBtn";
                kupiBtn.innerHTML="Kupi";
                kupiK.appendChild(kupiBtn);
    
                kupiBtn.onclick=(ev)=>this.kupi(host,a.idArtikl);

            }
            
        })
        
    }

    kupi(hostara,id){
        fetch("https://localhost:7193/Ispit/KupiArtikl/"+id,{
            method:"PUT"
        }).then(data=>{data.json().then(info=>{
            this.artikli.forEach(x=>{
                if(x.id===id){
                    x.kolicina--;
                }
            })
            alert("Uspesna kupovina.");
            this.iscrtajArtikle(hostara);
        })})
    }
}