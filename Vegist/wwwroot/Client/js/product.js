let img = document.querySelector(".mainImg img");

img.onmousemove = function (e) {
  e.target.style.setProperty('--x', (100 * e.offsetX / e.target.offsetWidth) + '%');
  e.target.style.setProperty('--y', (100 * e.offsetY / e.target.offsetHeight) + '%');
}


const SDetailLi = document.querySelectorAll(".SDetailLi");
const MDetailLi = document.querySelectorAll(".MDetailLi");

const detailImgSlider = document.querySelectorAll(".detailImg-slider img");

const mainImage = document.querySelector(".mainImage")
const slideImage = document.querySelectorAll(".slideImage")
const detailImg = document.querySelector(".detail-img")

const previousBtn = document.querySelector(".previous")
const nextBtn = document.querySelector(".next")



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


//Product images

thumbnailClick = (event) => {
  slideImage.forEach(img => {
    img.classList.remove('active')
  })
  // event.target.parentElement.classList.add('active')
  mainImage.src = event.target.src.replace('-thumbnail', '')
}

slideImage.forEach(img => {
  img.addEventListener('click', thumbnailClick)
})

nextBtnClick = () => {
  let imageIndex = getCurrentImageIndex()
  imageIndex++
  if (imageIndex > 4) {
    imageIndex = 1
  }
  setMainImage(imageIndex)
}

previousBtnClick = () => {
  let imageIndex = getCurrentImageIndex()
  imageIndex--
  if (imageIndex < 1) {
    imageIndex = 4
  }
  setMainImage(imageIndex)
}

getCurrentImageIndex = () => {
  const imageIndex = parseInt(mainImage.src.split('\\').pop().split('/').pop().replace('.webp', '').replace('product', ''))
  return imageIndex
}

setMainImage = (imageIndex) => {
  mainImage.src = `assets/images/product${imageIndex}.webp`
  slideImage.forEach(img => {
    img.classList.remove('active')
  })
  slideImage[imageIndex - 1].classList.add('active')
}

nextBtn.addEventListener('click', nextBtnClick)
previousBtn.addEventListener('click', previousBtnClick)