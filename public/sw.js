const CACHE="mafia-wv-v3-shell-20261001";
const SHELL=["/","/index.html","/style.css","/cinematic.css","/rebuild-game.css","/rebuild.js","/phone.js","/manifest.json"];
self.addEventListener("install",event=>{
  event.waitUntil(caches.open(CACHE).then(cache=>Promise.allSettled(SHELL.map(url=>cache.add(new Request(url,{cache:"reload"}))))).then(()=>self.skipWaiting()));
});
self.addEventListener("activate",event=>{
  event.waitUntil(caches.keys().then(keys=>Promise.all(keys.filter(k=>k!==CACHE).map(k=>caches.delete(k)))).then(()=>self.clients.claim()));
});
self.addEventListener("fetch",event=>{
  const req=event.request;
  if(req.method!=="GET"||req.url.includes("/api/"))return;
  if(req.mode==="navigate"){
    event.respondWith(fetch(req,{cache:"no-store"}).then(res=>{
      if(res.ok){const copy=res.clone();caches.open(CACHE).then(c=>c.put("/index.html",copy));}
      return res;
    }).catch(()=>caches.match("/index.html").then(r=>r||caches.match("/"))));
    return;
  }
  event.respondWith(caches.match(req).then(cached=>{
    const fresh=fetch(req,{cache:"no-store"}).then(res=>{
      if(res.ok){const copy=res.clone();caches.open(CACHE).then(c=>c.put(req,copy));}
      return res;
    }).catch(()=>cached);
    return cached||fresh;
  }));
});