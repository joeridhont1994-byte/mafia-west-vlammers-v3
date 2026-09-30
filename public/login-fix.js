(()=>{"use strict";
const q=id=>document.getElementById(id);
function openGame(u){
 try{
   if(typeof window.applyUser==="function"){window.applyUser(u);return;}
   if(typeof applyUser==="function"){applyUser(u);return;}
 }catch(e){console.error("applyUser failed",e)}
 try{
   if(typeof state==="object"&&state&&u){state.name=u.username;state.money=u.money;state.bank=u.bank;state.energy=u.energy;state.xp=u.xp;state.level=u.level;state.car=u.car;state.equipment=u.equipment;state.isAdmin=!!u.isAdmin;}
 }catch(e){}
 const ls=q("loginScreen"),gs=q("gameScreen"),nav=q("nav"),pn=q("playerName");
 if(pn&&u)pn.textContent=u.username||"Speler";
 if(ls)ls.classList.add("hidden");
 if(gs)gs.classList.remove("hidden");
 if(nav)nav.classList.remove("hidden");
 try{if(typeof render==="function")render()}catch(e){}
}
async function login(){
 const b=q("loginBtn"),m=q("authMessage"),username=(q("nameInput")?.value||"").trim(),password=q("passwordInput")?.value||"";
 if(!username||!password){if(m)m.textContent="Vul spelersnaam en wachtwoord in.";return;}
 if(b){b.disabled=true;b.textContent="INLOGGEN...";} if(m)m.textContent="Verbinding maken...";
 try{
  const r=await fetch("/api/login",{method:"POST",headers:{"Content-Type":"application/json","Accept":"application/json"},credentials:"include",cache:"no-store",body:JSON.stringify({username,password})});
  const raw=await r.text();let d={};try{d=JSON.parse(raw)}catch{}
  if(!r.ok)throw new Error(d.error||("Serverfout "+r.status));
  if(!d.user)throw new Error("Loginantwoord ongeldig.");
  if(m)m.textContent="Login gelukt ✓";
  openGame(d.user);
 }catch(e){
  console.error("login failed",e);
  if(m)m.textContent="Fout: "+(e.message||"Geen verbinding met server");
 }finally{if(b){b.disabled=false;b.textContent="INLOGGEN";}}
}
function init(){
 const b=q("loginBtn"),p=q("passwordInput");
 if(!b)return setTimeout(init,100);
 b.onclick=e=>{e.preventDefault();e.stopImmediatePropagation();login();};
 if(p)p.onkeydown=e=>{if(e.key==="Enter"){e.preventDefault();login();}};
}
if(document.readyState==="loading")document.addEventListener("DOMContentLoaded",init);else init();
})();