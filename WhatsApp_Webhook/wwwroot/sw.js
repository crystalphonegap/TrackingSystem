
self.addEventListener('install', event => {
    console.log('service worker has been installed');
  
});
self.addEventListener('activate', event => {
    console.log('service worker has been activate');
});
self.addEventListener('fetch', event => {
    console.log('service worker has been fetch');
});