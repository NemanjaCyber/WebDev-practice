export class Produkcija{
    constructor(id,naziv,kategorije){
        this.id=id;
        this.naziv=naziv;
        this.kategorije=kategorije;
        this.filmoviZaSerch=[];
        this.kontejner=null;
    }

    iscrtajProdukciju(host){

        let kategorijePoJednom=[];
        this.kategorije.forEach(k=>{
            if(!kategorijePoJednom.includes(k))
            kategorijePoJednom.push(k);
        })

        let produkcijaKontejner=document.createElement("div");
        produkcijaKontejner.className="produkcijaKontejner";
        this.kontejner=produkcijaKontejner;
        host.appendChild(produkcijaKontejner);

        let nazivKontejner=document.createElement("div");
        nazivKontejner.className="nazivKontejner";
        produkcijaKontejner.appendChild(nazivKontejner);

        let labNaziv=document.createElement("label");
        labNaziv.textContent=this.naziv;
        nazivKontejner.appendChild(labNaziv);

        let formaKontejner=document.createElement("div");
        formaKontejner.className="formaKontejner";
        produkcijaKontejner.appendChild(formaKontejner);

        let prikazKontejner=document.createElement("div");
        prikazKontejner.className="prikazKontejner";
        produkcijaKontejner.appendChild(prikazKontejner);

        let kategorijaKontejner=document.createElement("div");
        kategorijaKontejner.className="kategorijaKontejner";
        formaKontejner.appendChild(kategorijaKontejner);

        let labKategorija=document.createElement("label");
        labKategorija.textContent="Kategorija: ";
        kategorijaKontejner.appendChild(labKategorija);

        let selectKategorija=document.createElement("select");
        this.kategorije.forEach(p=>{
            let opcijaKategorija=document.createElement("option");
            opcijaKategorija.value=p;
            kategorijaKontejner.textContent=p;
            selectKategorija.appendChild(opcijaKategorija);
        })
        kategorijaKontejner.appendChild(selectKategorija);

        let donjiDeoForme=document.createElement("div");
        donjiDeoForme.className="donjiDeoForme";
        formaKontejner.appendChild(donjiDeoForme);

        this.nastaviCrtanje(donjiDeoForme,prikazKontejner,selectKategorija.value);
        selectKategorija.onchange=(ev)=>this.nastaviCrtanje(donjiDeoForme,prikazKontejner,selectKategorija.value);
    }

    nastaviCrtanje(donjiDeoForme,prikazKontejner,kategorije){

        fetch("https://localhost:7193/Ispit/VratiFilmoveZaKateogrije/"+kategorije+"/"+this.id,{
            method: "GET"
        }).then(data=>data.json()
        .then(info=>{
            info.forEach(f=>{
                f.filmovi.forEach(k=>{
                    this.filmoviZaSerch.push(k);
                })

                let filmoviKontejner=document.createElement("div");
                filmoviKontejner.className="filmoviKontejner";
                donjiDeoForme.appendChild(filmoviKontejner);

                let labFilmovi=document.createElement("lab");
                labFilmovi.textContent="Filmovi: ";
                filmoviKontejner.appendChild(labFilmovi);

                let selectFilmovi=document.createElement("select");
                this.filmoviZaSerch.forEach(f=>{
                    let opcijaFilm=document.createElement("option");
                    opcijaFilm.value=f.id;
                    opcijaFilm.textContent=f.naziv;
                    selectFilmovi.appendChild(opcijaFilm);
                })
                filmoviKontejner.appendChild(selectFilmovi);
            
                let ocenaKontejner=document.createElement("div");
                ocenaKontejner.className="ocenaKontejner";
                donjiDeoForme.appendChild(ocenaKontejner);

                let labOcena=document.createElement("label");
                labOcena.textContent="Ocena: ";
                ocenaKontejner.appendChild(labOcena);

                let inputOcena=document.createElement("input");
                inputOcena.type="number";
                ocenaKontejner.appendChild(inputOcena);

                let btnOceni=document.createElement("button");
                btnOceni.textContent="Snimi ocenu";
                donjiDeoForme.appendChild(btnOceni);
                btnOceni.onclick=(ev)=>this.oceni(selectFilmovi.value,inputOcena.value,prikazKontejner)
            
                this.iscrtajPrikaz(prikazKontejner);
            })
        }))
    }

    oceni(id, ocena, prikazKontejner){
        fetch("https://localhost:7193/Ispit/DodajOcenu/"+id+"/"+ocena,{
            method:"PUT"
        }).then(data=>{data.json().then(info=>{
            this.filmoviZaSerch.forEach(p=>{
                if(p.id===id)
                {
                    p.ukupnaOcena+=parseInt(ocena);
                    p.brOcena++;
                }
            })
            alert("Upisana ocena");
            this.iscrtajPrikaz(prikazKontejner);
        })})
    }

    iscrtajPrikaz(prikazKontejner){

        prikazKontejner.innerHTML="";
        
        let najgori=this.filmoviZaSerch[0];
        let srednji=this.filmoviZaSerch[0];
        let najbolji=this.filmoviZaSerch[0];
        let pozicija=0;

        this.filmoviZaSerch.forEach(p=>{
            if(p.ukupnaOcena/p.brOcena<najgori.ukupnaOcena/najgori.brOcena){
                najgori=p;
            }
            if(p.ukupnaOcena/p.brOcena>najgori.ukupnaOcena/najgori.brOcena){
                najbolji=p;
            }
             pozicija=parseInt(this.filmoviZaSerch.length/2);
             srednji=this.filmoviZaSerch[pozicija];
        })

        
        let niz = [najgori,srednji,najbolji];
        
        niz.forEach(f=>{

            let prosecnaOcena=((f.ukupnaOcena/f.brOcena)/10)*100;
            
            let filmPrikaz=document.createElement("div");
            filmPrikaz.className="filmPrikaz";

            let nazivFilma= document.createElement("label");
            nazivFilma.className="nazivFilma";
            nazivFilma.innerHTML=f.naziv;

            filmPrikaz.appendChild(nazivFilma);

            

            let skalaFilma = document.createElement("div");
            skalaFilma.className="skalaFilma";
            skalaFilma.style.height=`${prosecnaOcena}%`;

            filmPrikaz.appendChild(skalaFilma);

            let prosecnaLabel=document.createElement("label");
            prosecnaLabel.innerHTML="Prosecna ocena: "+ f.ukupnaOcena/f.brOcena

            filmPrikaz.appendChild(prosecnaLabel)




            prikazKontejner.appendChild(filmPrikaz);
        })

    }
}