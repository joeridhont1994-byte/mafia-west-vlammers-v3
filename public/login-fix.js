(()=>{"use strict";
function q(id){return document.getElementById(id)}
async function login(){
 const b=q("loginBtn"),m=q("authMessage"),username=(q("nameInput")?.value||"").trim(),password=q("passwordInput")?.value||"";
 if(!username||!password){if(m)m.textContent="Vul spelersnaam en wachtwoord in.";return}
 if(b){b.disabled=true;b.textContent="INLOGGEN..."} if(m)m.textContent="Verbinding maken...";
 try{
  const r=await fetch(location.origin+"/api/login",{method:"POST",headers:{"Content-Type":"application/json","Accept":"application/json"},credentials:"include",cache:"no-store",body:JSON.stringify({username,password})});
  const raw=await r.text();let d={};try{d=JSON.parse(raw)}catch{}
  if(!r.ok)throw new Error(d.error||("Serverfout "+r.status));
  if(m)m.textContent="Login gelukt ✓";
  const me=await fetch("/api/me",{credentials:"include",cache:"no-store"}); if(!me.ok) throw new Error("Sessie niet actief."); location.reload();
 }catch(e){if(m)m.textContent="Fout: "+(e.message||"Geen verbinding met server");if(b){b.disabled=false;b.textContent="INLOGGEN"}}
}
function init(){
 const b=q("loginBtn"),p=q("passwordInput");
 if(!b)return setTimeout(init,100);
 b.onclick=function(e){e.preventDefault();e.stopPropagation();login()};
 if(p)p.onkeydown=function(e){if(e.key==="Enter"){e.preventDefault();login()}};
}
if(document.readyState==="loading")document.addEventListener("DOMContentLoaded",init);else init();
})();