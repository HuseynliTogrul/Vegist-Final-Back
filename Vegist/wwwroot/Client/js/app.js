const categoryList = document.querySelector(".CategoryList");
const CategoryHeader = document.querySelector(".CategoryHeader");

const searchIcon = document.querySelector(".searchIcon");
const searchSection = document.querySelector(".searchSection");

const searchButton = document.querySelector(".search button");

const productIcon = document.querySelector(".productIcon i")

const menubarIcon = document.querySelector(".menubarIcon i")
const menuMobile = document.querySelector(".menu-mobile")
const menuHeaderIcon = document.querySelector(".menu-header i")


window.onkeydown = function (event) {
  if (event.keyCode == 27) {
    searchSection.style.display = "none";
  }
};

CategoryHeader.addEventListener("click", () => {
  categoryList.classList.toggle("DisplayNone");
})

searchIcon.addEventListener("click", () => {
  if (searchSection.style.display === 'block') {
    searchSection.style.display = 'none';
  } else {
    searchSection.style.display = 'block';
  }
})

searchButton.addEventListener("click", () => {
  searchSection.style.display = "none";
})

menubarIcon.addEventListener("click", () => {
  if (menubarIcon.style.display === "none") {
    menuMobile.style.display = "none";
  } else {
    menuMobile.style.display = "block";
  }
})

menuHeaderIcon.addEventListener("click", () => {
  menuMobile.style.display = "none";
})




// let slideIndex = 1;
// showSlides(slideIndex);

// function plusSlides(n) {
//   showSlides(slideIndex += n);
// }

// function currentSlide(n) {
//   showSlides(slideIndex = n);
// }

// function showSlides(n) {
//   let i;
//   let slides = document.getElementsByClassName(".slide");
//   let point = document.getElementsByClassName("slidePoint p");
//   if (n > slides.length) {slideIndex = 1}
//   if (n < 1) {slideIndex = slides.length}
//   for (i = 0; i < slides.length; i++) {
//     slides[i].style.display = "none";
//   }
//   for (i = 0; i < point.length; i++) {
//     point[i].className = point[i].className.replace(" active", "");
//   }
//   slides[slideIndex-1].style.display = "block";
//   point[slideIndex-1].className += " active";
// }


$('.responsive').slick({
  dots: true,
  infinite: false,
  speed: 300,
  slidesToShow: 1,
  slidesToScroll: 1,
  infinite: true,
  responsive: [
    {
      breakpoint: 1024,
      settings: {
        slidesToShow: 1,
        slidesToScroll: 1,
        infinite: true,
        dots: true
      }
    },
    {
      breakpoint: 600,
      settings: {
        slidesToShow: 2,
        slidesToScroll: 2
      }
    },
    {
      breakpoint: 480,
      settings: {
        slidesToShow: 3,
        slidesToScroll: 3
      }
    }
  ]
});