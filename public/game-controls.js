(()=>{"use strict";
const $=id=>document.getElementById(id);
function panel(title,html){const p=$("panel"),t=$("panelTitle"),b=$("panelBody");if(!p||!t||!b)return;t.textContent=title;b.innerHTML=html;p.classList.add("open");p.scrollTop=0;b.scrollTop=0}
const handlers={
 missions:()=>panel("💼 Missies",typeof missionsHtml==="function"?missionsHtml():"<p>Missies konden niet geladen worden.</p>"),
 bank:()=>panel("🏦 Bank",typeof bankHtml==="function"?bankHtml():"<p>Bank kon niet geladen worden.</p>"),
 transport:()=>typeof showTransport==="function"?showTransport():panel("🚚 Transport","<p>Transport kon niet geladen worden.</p>"),
 garage:()=>typeof showGarage==="function"?showGarage():panel("🚗 Garage","<p>Garage kon niet geladen worden.</p>"),
 hospital:()=>typeof showHospital==="function"?showHospital():panel("🏥 Ziekenhuis","<p>Ziekenhuis kon niet geladen worden.</p>"),
 properties:()=>typeof showProperties==="function"?showProperties():panel("🏠 Vastgoed","<p>Vastgoed kon niet geladen worden.</p>"),
 shop:()=>panel("🧰 Uitrusting",typeof shopHtml==="function"?shopHtml():"<p>Winkel kon niet geladen worden.</p>"),
 inventory:()=>typeof showInventory==="function"?showInventory():panel("🎒 Inventaris","<p>Inventaris kon niet geladen worden.</p>"),
 weapons:()=>panel("🔫 Wapenwinkel",typeof weaponShopHtml==="function"?weaponShopHtml():"<p>Wapenwinkel kon niet geladen worden.</p>"),
 profile:()=>typeof showPlayer==="function"?showPlayer(encodeURIComponent((window.state&&state.name)||"")):null,
 leaderboard:()=>typeof showLeaderboard==="function"?showLeaderboard():null,
 players:()=>typeof showPlayers==="function"?showPlayers():null,
 notifications:()=>typeof showNotifications==="function"?showNotifications():null,
 clan:()=>typeof showFamily==="function"?showFamily():null,
 chat:()=>typeof showChat==="function"?showChat():null,
 feedback:()=>typeof showFeedback==="function"?showFeedback():null,
 mycity:()=>typeof showMyCity==="function"?showMyCity():null,
 admin:()=>typeof adminPanel==="function"?adminPanel():null
};
document.addEventListener("click",e=>{const b=e.target.closest("[data-action]");if(!b)return;const fn=handlers[b.dataset.action];if(!fn)return;e.preventDefault();e.stopImmediatePropagation();try{fn()}catch(err){console.error("control",b.dataset.action,err);panel("⚠️ Fout","<p>Er ging iets mis bij het openen. Probeer opnieuw.</p>")}},true);
const close=$("closePanel");if(close)close.addEventListener("click",()=>{const p=$("panel");if(p)p.classList.remove("open")},true);
})();