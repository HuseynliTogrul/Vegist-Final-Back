let img = document.querySelector(".mainImg img");

img.onmousemove = function (e) {
  e.target.style.setProperty('--x', (100 * e.offsetX / e.target.offsetWidth) + '%');
  e.target.style.setProperty('--y', (100 * e.offsetY / e.target.offsetHeight) + '%');
}


const SDetailLi = document.querySelectorAll(".SDetailLi");
const MDetailLi = document.querySelectorAll(".MDetailLi");

const detailImgSlider = document.querySelectorAll(".detailImg-slider img");

// Size product

SDetailLi.forEach((size) => {
  size.addEventListener("click", () => {
    SDetailLi.forEach((resetsize) => {
      resetsize.style.border = "1px solid white";
    });

    size.style.border =
      size.style.border === "1px solid white" ? "2px solid #91b656" : "1px solid white";
  });
});

// Material product

MDetailLi.forEach((size) => {
  size.addEventListener("click", () => {
    MDetailLi.forEach((resetsize) => {
      resetsize.style.border = "1px solid white";
    });

    size.style.border =
      size.style.border === "1px solid white" ? "2px solid #91b656" : "1px solid white";
  });
});


// Detail img 

detailImgSlider.forEach(imgSlider => {
  imgSlider.addEventListener("click", () => {
    detailImgSlider.forEach((resetimg) => {
      resetimg.style.border = "1px solid white";
    });

    imgSlider.style.border =
      imgSlider.style.border === "1px solid white" ? "1px solid #91b656" : "1px solid white";
  })
})