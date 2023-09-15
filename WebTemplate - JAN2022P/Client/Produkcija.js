export class Produkcija{
    constructor(id,produkcijaNaziv,kategorije){
        this.id=id;
        this.produkcijaNaziv=produkcijaNaziv;
        this.kategorije=kategorije;
        this.filmovi=[];
        this.kontejner=null;
    }

    async iscrtaj(host){
        let kategorijePoJednom=[]
        this.kategorije.forEach(x=>{
            if(!kategorijePoJednom.includes(x)){
                kategorijePoJednom.push(x);
            }
        })

        this.kategorije=kategorijePoJednom;

        let sveKontejner=document.createElement("div");
        sveKontejner.className="sveKontejner";
        this.kontejner=sveKontejner;
        host.appendChild(sveKontejner);

        let nazivKontejner=document.createElement("div");
        nazivKontejner.className="nazivKontejner";
        sveKontejner.appendChild(nazivKontejner);

        let nazivLab=document.createElement("label");
        nazivLab.innerHTML=this.produkcijaNaziv;
        nazivLab.className="nazivLab"
        nazivKontejner.appendChild(nazivLab);

        let formaKontejner=document.createElement("div");
        formaKontejner.className="formaKontejner";
        sveKontejner.appendChild(formaKontejner);

        let kategorijaKontejner=document.createElement("div");
        kategorijaKontejner.className="kategorijaKontejner";
        formaKontejner.appendChild(kategorijaKontejner);

        let labKategorija=document.createElement("label");
        labKategorija.innerHTML="Kategorija: ";
        kategorijaKontejner.appendChild(labKategorija);

        //console.log(this.kategorije);

        let selectKategorija=document.createElement("select")
        selectKategorija.className="selectKategorija"
        this.kategorije.forEach(x=>{
            //console.log(x);
            let opcija=document.createElement("option");
            opcija.value=x;
            opcija.textContent=x;
            selectKategorija.appendChild(opcija);
        })
        kategorijaKontejner.appendChild(selectKategorija);

        let donjiDeoForme=document.createElement("div");
        donjiDeoForme.className="donjiDeoForme";
        formaKontejner.appendChild(donjiDeoForme);

        let valueZaSlanje=selectKategorija.value;

         const response = await fetch("https://localhost:7193/Ispit/VratiFilmoveZaKategorije/"+valueZaSlanje+"/"+this.id);
         const data = await response.json();
         data.forEach(info => {
             info.filmovi.forEach(f => {
                 this.filmovi.push(f);
             });
         });

        //  fetch("https://localhost:7193/Ispit/VratiFilmoveZaKategorije/"+selectKategorija.value+"/"+this.id,{
        //  }).then(data=>data.json()).then(info=>{
        //      info.forEach(f=>{
        //          f.filmovi.forEach(k=>{
        //              this.filmovi.push(k);
                    
        //          })
        //      })
        //  })
        
        //console.log(this.filmovi)

        let filmoviKontejner=document.createElement("div");
        filmoviKontejner.className="filmoviKontejner";
        donjiDeoForme.appendChild(filmoviKontejner);

        let filmLab=document.createElement("label");
        filmLab.innerHTML="Filmovi: ";
        filmoviKontejner.appendChild(filmLab);

        let selectFilm=document.createElement("select");
        selectFilm.className="selectFilm"
        this.filmovi.forEach(y=>{
            let opcija2=document.createElement("option");
            opcija2.value=y.id;
            opcija2.innerHTML=y.naziv;
            selectFilm.appendChild(opcija2);
        })
        filmoviKontejner.appendChild(selectFilm);

        let ocenaKontejner=document.createElement("div");
        ocenaKontejner.className="ocenaKontejner";
        donjiDeoForme.appendChild(ocenaKontejner);

        let ocenaLab=document.createElement("label");
        ocenaLab.innerHTML="Ocena: ";
        ocenaKontejner.appendChild(ocenaLab);

        let ocenaInput=document.createElement("input");
        ocenaInput.className="ocenaInput"
        ocenaInput.type="number";
        ocenaInput.min=1;
        ocenaInput.max=10;
        ocenaKontejner.appendChild(ocenaInput);

        let oceniBtn=document.createElement("button");
        oceniBtn.textContent="Snimi Ocenu";
        oceniBtn.className="oceniBtn"
        donjiDeoForme.appendChild(oceniBtn);

        let prikazKontejner=document.createElement("div");
        prikazKontejner.className="prikazKontejner";
        sveKontejner.appendChild(prikazKontejner);

        oceniBtn.onclick=(ev)=>this.oceni(selectFilm.value,ocenaInput.value,prikazKontejner)
    
        this.iscrtajPrikaz(prikazKontejner);
    }

    oceni(id,ocena,prikazKontejner){

        prikazKontejner.innerHTML=""
        fetch("https://localhost:7193/Ispit/DodajOcenu/"+id+"/"+ocena,{
            method:"PUT"
        })
        alert("Upisana ocena")
        this.iscrtajPrikaz(prikazKontejner);

    }

    iscrtajPrikaz(prikazKontejner){
        let najgori=this.filmovi[0];
        let srednji=this.filmovi[0];
        let najbolji=this.filmovi[0];

        this.filmovi.forEach(x=>{
            if(x.zbirOcena/x.brojOcena<najgori.zbirOcena/najgori.brojOcena){
                najgori=x;
            }
            if(x.zbirOcena/x.brojOcena>najgori.zbirOcena/najgori.brojOcena){
                najbolji=x;
            }
            let pozicijaSrednjeg=parseInt(this.filmovi.length/2);
            srednji=this.filmovi[pozicijaSrednjeg];
        })

        let niz=[najgori,srednji,najbolji];

        niz.forEach(f=>{
            let visinaStuba=((f.zbirOcena/f.brojOcena)/10)*100

            let filmKontejner=document.createElement("div");
            filmKontejner.className="filmKontejner";

            let nazivFilma=document.createElement("label");
            nazivFilma.innerHTML=f.naziv;
            filmKontejner.appendChild(nazivFilma);

            let skalaFilma=document.createElement("div");
            skalaFilma.className="skalaFilma";
            skalaFilma.style.height=`${visinaStuba}%`;
            filmKontejner.appendChild(skalaFilma);

            let prosekLab=document.createElement("label");
            prosekLab.innerHTML=((f.zbirOcena/f.brojOcena)/10).toFixed(2);
            filmKontejner.appendChild(prosekLab);

            prikazKontejner.appendChild(filmKontejner);
        })
    }
}