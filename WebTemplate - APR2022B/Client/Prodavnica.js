export class Prodavnica{
    constructor(id,naziv,zarada,sastojci){
        this.id=id;
        this.naziv=naziv;
        this.zarada=zarada;
        this.sastojci=sastojci;
        this.kontejner=null;
        this.stoId=[];
        this.stoOznaka=[];
        this.stoVrednost=[];
    }

    async iscrtaj(host){

        let sveK=document.createElement("div");
        sveK.className="sveK";
        this.kontejner=sveK;
        host.appendChild(sveK);

        let nazivK=document.createElement("div");
        nazivK.className="nazivK";
        sveK.appendChild(nazivK);

        let doleK=document.createElement("div");
        doleK.className="doleK"
        sveK.appendChild(doleK);

        let formaK=document.createElement("div");
        formaK.className="formaK";
        doleK.appendChild(formaK);

        let stoloviK=document.createElement("div");
        stoloviK.className="stoloviK";
        doleK.appendChild(stoloviK);

        this.iscrtajNaziv(nazivK);

        let oznakaK=document.createElement("div");
        oznakaK.className="oznakaK";
        formaK.appendChild(oznakaK);

        let oznakaL=document.createElement("label");
        oznakaL.className="oznakaL";
        oznakaL.innerHTML="Sto: ";
        oznakaK.appendChild(oznakaL);

        let nizOznaka=[]
        const response=await fetch("https://localhost:7193/Ispit/VratiStolove/"+this.id)
        const data=await response.json();
        data.forEach(info=>{
            info.stoOznaka.forEach(x=>{
                nizOznaka.push(x);
            })
        })
        //console.log(nizOznaka)

        let oznakaSelect=document.createElement("select");
        oznakaSelect.className="oznakaSelect";
        nizOznaka.forEach(x=>{
            let opcija=document.createElement("option");
            opcija.innerHTML=x;
            opcija.value=x;
            oznakaSelect.appendChild(opcija);
        })
        oznakaK.appendChild(oznakaSelect);

        let sastojakK=document.createElement("div");
        sastojakK.className="sastojakK";
        formaK.appendChild(sastojakK);

        let sastojakL=document.createElement("label");
        sastojakL.className="sastojakL";
        sastojakL.innerHTML="Sastojak: "
        sastojakK.appendChild(sastojakL);

        let sastojakSelect=document.createElement("select");
        sastojakSelect.className="sastojakSelect";
        this.sastojci.forEach(x=>{
            let opcija=document.createElement("option");
            opcija.innerHTML=x;
            opcija.value=x;
            sastojakSelect.appendChild(opcija);
        })
        sastojakK.appendChild(sastojakSelect);

        let kolicinaK=document.createElement("div");
        kolicinaK.className="kolicinaK";
        formaK.appendChild(kolicinaK);

        let kolicinaL=document.createElement("label");
        kolicinaL.className="kolicinaL";
        kolicinaL.innerHTML="Kolicina: "
        kolicinaK.appendChild(kolicinaL);

        let kolicinaI=document.createElement("input");
        kolicinaI.className="kolicinaI";
        kolicinaI.type="number";
        kolicinaI.min=0;
        kolicinaI.max=5;
        kolicinaK.appendChild(kolicinaI);

        let dugmeK=document.createElement("div");
        dugmeK.className="dugmeK";
        formaK.appendChild(dugmeK);

        let dodajBtn=document.createElement("button");
        dodajBtn.className="dodajBtn";
        dodajBtn.innerHTML="Dodaj";
        dugmeK.appendChild(dodajBtn);

        dugmeK.onclick=(ev)=>this.crtajStolove(stoloviK,nazivK);

        const response2=await fetch("https://localhost:7193/Ispit/VratiSveStoloveZaPrikaz/"+this.id);
        const data2=await response2.json();
        data2.forEach(x=>{
            this.stoId=x.stoId;
            this.stoOznaka=x.stoOznaka;
            this.stoVrednost=x.stoVrednost;
        })

        // this.stoOznaka.forEach((x,index)=>{
        //     let y=this.stoVrednost[index];

        //     let stoK=document.createElement("div");
        //     stoK.className="stoK";
        //     stoloviK.appendChild(stoK);

        //     let stoL=document.createElement("label");
        //     stoL.className="stoL";
        //     stoL.innerHTML="Sto: "+x;
        //     stoK.appendChild(stoL);

        //     let sendvicK=document.createElement("div");
        //     sendvicK.className="sendvicK";
        //     stoK.appendChild(sendvicK);

        //     let donjaZemicka=document.createElement("div");
        //     donjaZemicka.className="zemicka";
        //     sendvicK.appendChild(donjaZemicka);

        //     let gornjaZemicka=document.createElement("div");
        //     gornjaZemicka.className="zemicka";
        //     sendvicK.appendChild(gornjaZemicka);

        //     let stoKolK=document.createElement("div");
        //     stoKolK.className="stoKolK";
        //     stoK.appendChild(stoKolK);

        //     let stoKolL=document.createElement("label");
        //     stoKolL.className="stoKolL";
        //     stoKolL.innerHTML=y;
        //     stoKolK.appendChild(stoKolL);

        //     let isporuciK=document.createElement("div");
        //     isporuciK.className="isporuciK";
        //     stoK.appendChild(isporuciK);

        //     let isporuciBtn=document.createElement("button")
        //     isporuciBtn.className="isporuciBtn";
        //     isporuciBtn.innerHTML="Isporuci"
        //     isporuciBtn.value=x;
        //     isporuciK.appendChild(isporuciBtn);

        //     isporuciBtn.onclick=(ev)=>this.isporuci(stoloviK,nazivK);
        // })


        this.crtajStolove(stoloviK,nazivK);
    }

    isporuci(host1,host2){

        let odabraniSto=document.querySelector(".isporuciBtn").value;
        console.log(odabraniSto);

        fetch("https://localhost:7193/Ispit/Isporuci/"+odabraniSto+"/"+this.id,{
            method:"PUT"
        }).then(data=>{data.json().then(info=>{
            this.stoOznaka.forEach((x,index)=>{
                if(x===odabraniSto){
                    this.stoVrednost[index]=0;
                }
            })
        })})
        this.iscrtajNaziv(host2);
        this.crtajStolove(host1);
        
    }

    iscrtajNaziv(host){

        host.innerHTML="";

        let nazivL=document.createElement("label");
        nazivL.className="nazivL";
        nazivL.innerHTML=this.naziv+" - "+this.zarada;
        host.appendChild(nazivL);
    }

    async crtajStolove(host1,host2){
        host1.innerHTML=""
        
        this.stoOznaka.forEach((x,index)=>{
            let y=this.stoVrednost[index];

            let stoK=document.createElement("div");
            stoK.className="stoK";
            host1.appendChild(stoK);

            let stoL=document.createElement("label");
            stoL.className="stoL";
            stoL.innerHTML="Sto: "+x;
            stoK.appendChild(stoL);

            let sendvicK=document.createElement("div");
            sendvicK.className="sendvicK";
            stoK.appendChild(sendvicK);

            let donjaZemicka=document.createElement("div");
            donjaZemicka.className="zemicka";
            sendvicK.appendChild(donjaZemicka);

            let gornjaZemicka=document.createElement("div");
            gornjaZemicka.className="zemicka";
            sendvicK.appendChild(gornjaZemicka);

            let stoKolK=document.createElement("div");
            stoKolK.className="stoKolK";
            stoK.appendChild(stoKolK);

            let stoKolL=document.createElement("label");
            stoKolL.className="stoKolL";
            stoKolL.innerHTML=y;
            stoKolK.appendChild(stoKolL);

            let isporuciK=document.createElement("div");
            isporuciK.className="isporuciK";
            stoK.appendChild(isporuciK);

            let isporuciBtn=document.createElement("button")
            isporuciBtn.className="isporuciBtn";
            isporuciBtn.innerHTML="Isporuci"
            isporuciBtn.value=x;
            isporuciK.appendChild(isporuciBtn);

            isporuciBtn.onclick=(ev)=>this.isporuci(host1,host2);
        })

    }


}