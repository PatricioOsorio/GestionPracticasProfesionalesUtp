// ==================================
// ASIDE
// ==================================
document.addEventListener('DOMContentLoaded', function (event) {
  const showNavbar = (toggleId, navId, bodyId, headerId) => {
    const toggle = document.getElementById(toggleId),
      nav = document.getElementById(navId),
      bodypd = document.getElementById(bodyId),
      headerpd = document.getElementById(headerId);

    // Validate that all variables exist
    if (toggle && nav && bodypd && headerpd) {
      toggle.addEventListener('click', () => {
        // show navbar
        nav.classList.toggle('show');
        // change icon
        toggle.classList.toggle('bx-x');
        // add padding to body
        bodypd.classList.toggle('body-pd');
        // add padding to header
        headerpd.classList.toggle('body-pd');
      });
    }
  };

  showNavbar('header-toggle', 'nav-bar', 'body-pd', 'header');

  // Link active
  const linkColor = document.querySelectorAll('.nav_link');

  function colorLink() {
    if (linkColor) {
      linkColor.forEach((l) => l.classList.remove('active'));
      this.classList.add('active');
    }
  }
  linkColor.forEach((l) => l.addEventListener('click', colorLink));

});

// ==================================
// AOS
// ==================================
document.addEventListener('DOMContentLoaded', (e) => {
  AOS.init();
});

// ==================================
// SCROLL TOP
// ==================================
const $btnScrollTop = document.querySelector('.scrollTop');

window.addEventListener('scroll', (e) => {
  let scrollTop = window.pageYOffset || document.documentElement.scrollTop;
  let viewportHeight = window.innerHeight;

  scrollTop > viewportHeight
    ? $btnScrollTop.classList.remove('hidden')
    : $btnScrollTop.classList.add('hidden');
});
$btnScrollTop.addEventListener('click', (e) => {
  if (e.target.matches('.scrollTop') || e.target.matches(`.scrollTop *`)) {
    window.scrollTo({
      top: 0,
      behavior: 'smooth',
    });
  }
});

// ==================================
// TOAST
// ==================================
const showToast = (message, icon = 'success', time = 3000) => {
  const Toast = Swal.mixin({
    toast: true,
    position: 'top-end',
    showConfirmButton: false,
    showCloseButton: true,
    timer: time,
    timerProgressBar: true,
    didOpen: (toast) => {
      toast.addEventListener('mouseenter', Swal.stopTimer);
      toast.addEventListener('mouseleave', Swal.resumeTimer);
    },
  });

  Toast.fire({
    icon: icon,
    title: message,
  });
};

// ==================================
// SWEETALERT2
// ==================================

document.addEventListener('click', (e) => {
  // Alerta borrar rol
  if (e.target.matches('#AlertaEliminarRol')) {
    Swal.fire({
      title: `¿Eliminar rol: <span class="text-primary">${e.target.dataset.role}</span>?`,
      text: 'No podrás revertir esto.',
      icon: 'warning',
      showCloseButton: true,
      showCancelButton: true,
      confirmButtonText: 'Sí, bórralo.',
      cancelButtonText: '¡No, cancelar!',
      confirmButtonColor: 'var(--bs-danger)',
      cancelButtonColor: 'var(--bs-secondary)',
    }).then((result) => {
      if (result.isConfirmed) {
        $.ajax({
          type: 'POST',
          url: '/Superadmin/DeleteRole',
          data: { id: e.target.dataset.id },
          cache: false,
          success: function (response) {
            Swal.fire({
              title: '¡Eliminado!',
              text: 'El rol ha sido eliminado.',
              icon: 'success',
              confirmButtonColor: 'var(--bs-primary)',
            }).then(function () {
              location.href = '/Superadmin/ReadRoles';
            });
          },
          error: function (error) {
            showToast("No es posible eliminar el rol, existen usuarios dentro del rol", "error", 8000)
          },
        });
      }
    });
  }

  // Alerta borrar usuario
  if (e.target.matches('#AlertaEliminarUsuario')) {
    Swal.fire({
      title: `¿Eliminar usuario: <span class="text-primary">${e.target.dataset.username}</span>?`,
      text: 'No podrás revertir esto.',
      icon: 'warning',
      showCloseButton: true,
      showCancelButton: true,
      confirmButtonText: 'Sí, bórralo.',
      cancelButtonText: '¡No, cancelar!',
      confirmButtonColor: 'var(--bs-danger)',
      cancelButtonColor: 'var(--bs-secondary)',
    }).then((result) => {
      if (result.isConfirmed) {
        $.ajax({
          type: 'POST',
          url: '/Superadmin/DeleteUser',
          data: { id: e.target.dataset.id },
          cache: false,
          success: function (response) {
            Swal.fire({
              title: '¡Eliminado!',
              text: 'El usuario ha sido eliminado.',
              icon: 'success',
              confirmButtonColor: 'var(--bs-primary)',
            }).then(function () {
              location.href = '/Superadmin/Index';
            });
          },
        });
      }
    });
  }
});

