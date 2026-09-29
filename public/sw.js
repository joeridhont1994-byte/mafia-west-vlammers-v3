const CACHE="mafia-wv-v8-121";
const ASSETS=["/","/style.css?v=20260929-121","/app.js?v=20260929-121","/manifest.json"];

self.addEventListener("install",event=>{
  event.waitUntil(
    caches.open(CACHE)
      .then(cache=>cache.addAll(ASSETS))
      .then(()=>self.skipWaiting())
  );
});

self.addEventListener("activate",event=>{
  event.waitUntil(
    caches.keys()
      .then(keys=>Promise.all(keys.filter(key=>key!==CACHE).map(key=>caches.delete(key))))
      .then(()=>self.clients.claim())
  );
});

self.addEventListener("fetch",event=>{
  if(event.request.method!=="GET" || event.request.url.includes("/api/")) return;

  const requestUrl=new URL(event.request.url);
  const isAppShell=requestUrl.pathname==="/" ||
    requestUrl.pathname==="/app.js" ||
    requestUrl.pathname==="/style.css" ||
    requestUrl.pathname==="/manifest.json";

  if(isAppShell){
    event.respondWith(
      fetch(event.request,{cache:"no-store"})
        .then(response=>{
          const copy=response.clone();
          caches.open(CACHE).then(cache=>cache.put(event.request,copy));
          return response;
        })
        .catch(()=>caches.match(event.request).then(r=>r||caches.match("/")))
    );
    return;
  }

  event.respondWith(
    caches.match(event.request).then(cached=>{
      const network=fetch(event.request).then(response=>{
        const copy=response.clone();
        caches.open(CACHE).then(cache=>cache.put(event.request,copy));
        return response;
      });
      return cached||network;
    })
  );
});
