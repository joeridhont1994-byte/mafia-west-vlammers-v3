(()=>{"use strict";
const $=id=>document.getElementById(id);
function panel(title,html){const p=$("panel"),t=$("panelTitle"),b=$("panelBody");if(!p||!t||!b)return;t.textContent=title;b.innerHTML=html;p.classList.add("open");p.style.setProperty("display","block","important");p.style.setProperty("visibility","visible","important");p.style.setProperty("opacity","1","important");p.style.setProperty("z-index","99999","important");p.scrollTop=0;b.scrollTop=0}
const call=(name,fallback,title)=>{try{const fn=window[name];if(typeof fn==="function"){fn();return}panel(title||name,fallback||"<p>Dit onderdeel kon niet geladen worden.</p>")}catch(e){console.error(name,e);panel("⚠️ Fout","<p>Dit onderdeel kon niet openen.</p>")}};
const handlers={
missions:()=>{try{panel("💼 Missies",typeof window.missionsHtml==="function"?window.missionsHtml():"<p>Missies konden niet geladen worden.</p>")}catch(e){panel("💼 Missies","<p>Missies konden niet geladen worden.</p>")}},
bank:()=>{try{panel("🏦 Bank",typeof window.bankHtml==="function"?window.bankHtml():"<p>Bank kon niet geladen worden.</p>")}catch(e){panel("🏦 Bank","<p>Bank kon niet geladen worden.</p>")}},
transport:()=>call("showTransport","<p>Transport kon niet geladen worden.</p>","🚚 Transport"),
garage:()=>call("showGarage","<p>Garage kon niet geladen worden.</p>","🚗 Garage"),
hospital:()=>call("showHospital","<p>Ziekenhuis kon niet geladen worden.</p>","🏥 Ziekenhuis"),
properties:()=>call("showProperties","<p>Vastgoed kon niet geladen worden.</p>","🏠 Vastgoed"),
shop:()=>{try{panel("🧰 Uitrusting",typeof window.shopHtml==="function"?window.shopHtml():"<p>Winkel kon niet geladen worden.</p>")}catch(e){panel("🧰 Uitrusting","<p>Winkel kon niet geladen worden.</p>")}},
inventory:()=>call("showInventory","<p>Inventaris kon niet geladen worden.</p>","🎒 Inventaris"),
weapons:()=>{try{panel("🔫 Wapenwinkel",typeof window.weaponShopHtml==="function"?window.weaponShopHtml():"<p>Wapenwinkel kon niet geladen worden.</p>")}catch(e){panel("🔫 Wapenwinkel","<p>Wapenwinkel kon niet geladen worden.</p>")}},
leaderboard:()=>call("showLeaderboard",null,"🏆 Ranglijst"),players:()=>call("showPlayers",null,"👥 Spelers"),notifications:()=>call("showNotifications",null,"🔔 Meldingen"),clan:()=>call("showFamily",null,"🏴 Familie"),chat:()=>call("showChat",null,"💬 Chat"),feedback:()=>call("showFeedback",null,"📝 Feedback"),mycity:()=>call("showMyCity",null,"🏙️ Mijn Stad"),admin:()=>call("adminPanel",null,"👑 Admin"),
profile:()=>{if(typeof window.showPlayer==="function"){const n=window.state&&window.state.name?window.state.name:($("playerName")?.textContent||"");window.showPlayer(encodeURIComponent(n))}}
};
document.addEventListener("pointerup",e=>{const b=e.target.closest&&e.target.closest("[data-action]");if(!b)return;const fn=handlers[b.dataset.action];if(!fn)return;e.preventDefault();e.stopImmediatePropagation();fn()},true);
document.addEventListener("click",e=>{const b=e.target.closest&&e.target.closest("[data-action]");if(!b)return;e.preventDefault();e.stopImmediatePropagation()},true);
const close=$("closePanel");if(close)close.addEventListener("pointerup",e=>{e.preventDefault();e.stopImmediatePropagation();const p=$("panel");if(p){p.classList.remove("open");p.style.removeProperty("display");p.style.removeProperty("visibility");p.style.removeProperty("opacity");p.style.removeProperty("z-index")}},true);
})();