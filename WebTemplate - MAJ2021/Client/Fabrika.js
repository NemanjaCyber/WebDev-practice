export class Fabrika{
    constructor(id,naziv,silosi){
        this.id=id;
        this.naziv=naziv;
        this.silosi=silosi;
        this.kontejner=null;
    }

    iscrtajFabriku(host){

        let fabrikaKontejner=document.createElement("div");
        fabrikaKontejner.classList.add("fabrikaKontejner");
        host.appendChild(fabrikaKontejner);

        let leviDeo=document.createElement("div");
        leviDeo.classList.add("leviDeo");
        fabrikaKontejner.appendChild(leviDeo);

        let nazivLab=document.createElement("label");
        nazivLab.classList.add("nazivLab");
        nazivLab.textContent=this.naziv;
        leviDeo.appendChild(nazivLab);

        let silosiKontejner=document.createElement("div");
        silosiKontejner.classList.add("silosiKontejner");
        leviDeo.appendChild(silosiKontejner);

        this.iscrtajSilose(silosiKontejner);
    }

    iscrtajSilose(host){
        
        this.silosi.forEach(s=>{

            let silosKontejner=document.createElement("div");
            silosKontejner.classList.add("silosKontejner");
            host.appendChild(silosKontejner);

            let infoKontejner=document.createElement("div");
            infoKontejner.classList.add("infoKontejener");
            silosKontejner.appendChild(infoKontejner);

            let podaciLab=document.createElement("label");
            podaciLab.classList.add("podaciLab");
            podaciLab.textContent=s.oznaka + "<br>" + s.trenutnaPopunjenost + "/" + s.kapacitet;
            infoKontejner.appendChild(podaciLab);
            
            let skalaKontejner=document.createElement("div");
            skalaKontejner.classList.add("skalaKontejner");
            silosKontejner.appendChild(skalaKontejner);
            let procenat=s.trenutnaPopunjenost/s.kapacitet*100;
            skalaKontejner.style.height=`${procenat}%`;

        })
    }
}