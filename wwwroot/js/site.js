// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
// Shrink header on scroll
(function () {
    const header = document.querySelector('.app-header');
    if (!header) return;
  
    let ticking = false;
    function onScroll() {
      if (!ticking) {
        window.requestAnimationFrame(function () {
          const y = window.scrollY || document.documentElement.scrollTop;
          if (y > 12) header.classList.add('shrink');
          else header.classList.remove('shrink');
          ticking = false;
        });
        ticking = true;
      }
    }
  
    window.addEventListener('scroll', onScroll, { passive: true });
    onScroll();
  })();
  