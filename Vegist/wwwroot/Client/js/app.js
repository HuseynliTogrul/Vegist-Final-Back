"use strict";

const overlay = document.querySelector(".overlay")

const leftBtn = document.querySelector(".leftBtn");
const rightBtn = document.querySelector(".rightBtn");


// Scroll

const scroll = document.querySelector(".scroll")

window.addEventListener("scroll", () => {
  if (window.pageYOffset > 100) {
    scroll.classList.add("active")
  } else {
    scroll.classList.remove("active")
  }
})

scroll.addEventListener("click", () => {
  window.scrollTo(0, 0);
})


// Category

const categoryList = document.querySelector(".CategoryList");
const CategoryHeader = document.querySelector(".CategoryHeader");

CategoryHeader.addEventListener("click", () => {
  categoryList.classList.toggle("DisplayNone");
})


// Search

const searchIcon = document.querySelector(".searchIcon");
const searchSection = document.querySelector(".searchSection");
const closeSearch = document.querySelector("#close-search");

searchIcon.addEventListener("click", () => {
  searchSection.style.display = "block";
  document.body.style.overflow = 'hidden';
  overlay.style.display = "block"
})

closeSearch.addEventListener('click', () => {
  searchSection.style.display = 'none';
  document.body.style.overflow = 'auto';
  overlay.style.display = "none"
})

window.onkeydown = function (event) {
  if (event.keyCode == 27) {
    searchSection.style.display = "none";
  }
};


// MenuBar

const menubarIcon = document.querySelector(".menubarIcon i")
const menuMobile = document.querySelector(".menu-mobile")
const menuHeaderIcon = document.querySelector(".menu-header i")

menubarIcon.addEventListener("click", () => {
  if (menubarIcon.style.display === "none") {
    menuMobile.style.display = "none";
    overlay.classList.add("hidden");
  } else {
    menuMobile.style.display = "block";
    document.body.style.overflow = 'hidden';
    overlay.classList.remove("hidden");
  }
})

menuHeaderIcon.addEventListener("click", () => {
  menuMobile.style.display = "none";
  document.body.style.overflow = 'auto';
  overlay.classList.add("hidden");
})

document.addEventListener('keydown', function (event) {
  const key = event.key;
  if (key === "Escape") {
    menuMobile.style.display = "none";
    overlay.classList.add("hidden");
  }
});


//MenuSlider

var swiper = new Swiper(".mySwiper1", {
  slidesPerView: 1,
  spaceBetween: 30,
  loop: true,
  autoplay: {
    delay: 10000,
    disableOnInteraction: false
  },
  breakpoints: {
    768: {
      slidesPerView: 2,
      spaceBetween: 40,
    },
    1024: {
      slidesPerView: 3,
      spaceBetween: 50,
    },
  },
});


// Slider

var swiper = new Swiper(".mySwiper", {
  slidesPerView: 1,
  spaceBetween: 30,
  loop: true,
  autoplay: {
    delay: 5000,
    disableOnInteraction: false
  },
  pagination: {
    el: ".swiper-pagination",
    clickable: true,
  },
  navigation: {
    nextEl: ".swiper-button-next",
    prevEl: ".swiper-button-prev",
  }
});


// CategorySlider

var swiper = new Swiper(".mySwiper2", {
  slidesPerView: 2,
  spaceBetween: 20,
  loop: true,
  autoplay: {
    delay: 5000,
    disableOnInteraction: false
  },
  navigation: {
    nextEl: ".rightBtn",
    prevEl: ".leftBtn",
  },
  breakpoints: {
    768: {
      slidesPerView: 4,
      spaceBetween: 20,
    },
    1024: {
      slidesPerView: 5,
      spaceBetween: 30,
    },
  },
});


//userLogin

const loginHeader = document.querySelector(".loginHeader");
const loginHeaderA = document.querySelector(".loginHeaderA");

loginHeader.addEventListener("click", () => {
  if (loginHeaderA.style.display === "block") {
    loginHeaderA.style.display = "none"
  } else {
    loginHeaderA.style.display = "block"
  }
})


// Footer

const listHeader = document.querySelectorAll(".listHeader")

listHeader.forEach(header => {
  header.addEventListener('click', () => {
    const footerSublist = header.nextElementSibling;
    footerSublist.classList.toggle("show-menu");

    const plusIcon = header.querySelector('.fa-plus');
    const minusIcon = header.querySelector('.fa-minus');

    if (footerSublist.classList.contains('show-menu')) {
      plusIcon.style.display = 'none';
      minusIcon.style.display = 'inline';
    } else {
      plusIcon.style.display = 'inline';
      minusIcon.style.display = 'none';
    }
  });
});


//Shop page js
//Filter

const shopFilter = document.querySelector(".shopFilter");
const shopLeft = document.querySelector(".shop-left");
const closeIcon = document.querySelector(".closeIcon i");

shopFilter.addEventListener("click", () => {
  shopLeft.style.display = "block";
  document.body.style.overflow = 'hidden';
  overlay.classList.remove("hidden");
})

closeIcon.addEventListener("click", () => {
  shopLeft.style.display = "none";
  document.body.style.overflow = 'auto';
  overlay.classList.add("hidden");
})

document.addEventListener('keydown', function (event) {
  const key = event.key;
  if (key === "Escape") {
    shopLeft.style.display = "none";
    overlay.classList.add("hidden");
  }
});


// Product Sort

document.getElementById('sort').addEventListener('change', function () {
  const sortOption = this.value;
  const productsContainer = document.querySelector('.products');
  const products = Array.from(productsContainer.children);

  products.sort((a, b) => {
    const nameA = a.getAttribute('data-name').toLowerCase();
    const nameB = b.getAttribute('data-name').toLowerCase();
    const priceA = parseFloat(a.getAttribute('data-price'));
    const priceB = parseFloat(b.getAttribute('data-price'));

    if (sortOption === 'AZ') {
      return nameA.localeCompare(nameB);
    } else if (sortOption === 'ZA') {
      return nameB.localeCompare(nameA);
    } else if (sortOption === 'priceLowHigh') {
      return priceA - priceB;
    } else if (sortOption === 'priceHighLow') {
      return priceB - priceA;
    } else {
      return 0;
    }
  });

  products.forEach(product => productsContainer.appendChild(product));
});


// Product Filter

document.addEventListener('DOMContentLoaded', function () {

  const categoryFilters = document.querySelectorAll('input[name="category"]');
  const sizeFilters = document.querySelectorAll('input[name="size"]');
  const materialFilters = document.querySelectorAll('input[name="material"]');

  const products = document.querySelectorAll('.productItem');

  categoryFilters.forEach(filter => filter.addEventListener('change', applyFilters));
  sizeFilters.forEach(filter => filter.addEventListener('change', applyFilters));
  materialFilters.forEach(filter => filter.addEventListener('change', applyFilters));

  function applyFilters() {
    const selectedCategories = Array.from(categoryFilters).filter(filter => filter.checked).map(filter => filter.value);
    const selectedSizes = Array.from(sizeFilters).filter(filter => filter.checked).map(filter => filter.value);
    const selectedMaterials = Array.from(materialFilters).filter(filter => filter.checked).map(filter => filter.value);

    products.forEach(product => {
      const productCategory = product.dataset.category;
      const productSize = product.dataset.size;
      const productMaterial = product.dataset.material;

      const categoryMatch = selectedCategories.length === 0 || selectedCategories.includes(productCategory);
      const sizeMatch = selectedSizes.length === 0 || selectedSizes.includes(productSize);
      const materialMatch = selectedMaterials.length === 0 || selectedMaterials.includes(productMaterial);

      if (categoryMatch && sizeMatch && materialMatch) {
        product.style.display = 'block';
      } else {
        product.style.display = 'none';
      }
    });
  }

  applyFilters();
});



