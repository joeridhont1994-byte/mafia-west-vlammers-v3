const VERSION="mwv-login-clean-20260930-1";
self.addEventListener("install",event=>{self.skipWaiting();});
self.addEventListener("activate",event=>{event.waitUntil((async()=>{const keys=await caches.keys();await Promise.all(keys.map(k=>caches.delete(k)));await self.clients.claim();})());});
self.addEventListener("fetch",event=>{
 const req=event.request;
 if(req.method!=="GET")return;
 const url=new URL(req.url);
 if(url.origin===self.location.origin){
   event.respondWith(fetch(req,{cache:"no-store"}).catch(()=>new Response("Offline",{status:503,headers:{"Content-Type":"text/plain"}})));
 }
});