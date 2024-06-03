let img = document.querySelector(".mainImg img");

img.onmousemove = function (e) {
  e.target.style.setProperty('--x', (100 * e.offsetX / e.target.offsetWidth) + '%');
  e.target.style.setProperty('--y', (100 * e.offsetY / e.target.offsetHeight) + '%');
}


// Size product

const SDetailLi = document.querySelectorAll(".SDetailLi");
const sizeSpan = document.querySelector(".sizeSpan")

SDetailLi.forEach((size) => {
  size.addEventListener("click", () => {
    SDetailLi.forEach((resetS) => {
      resetS.style.border = "1px solid white";
    });

    sizeSpan.textContent = size.textContent;
    size.style.border = "2px solid #91b656";
  });
});


// Material product

const MDetailLi = document.querySelectorAll(".MDetailLi");
const materialSpan = document.querySelector(".materialSpan")

MDetailLi.forEach((material) => {
  material.addEventListener("click", () => {
    MDetailLi.forEach((resetM) => {
      resetM.style.border = "1px solid white";
    });

    materialSpan.textContent = material.textContent;
      material.style.border = "2px solid #91b656";
  });
});


// Detail img 
const detailImgSlider = document.querySelectorAll(".detailImg-slider img");

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

const mainImage = document.querySelector(".mainImage")
const slideImage = document.querySelectorAll(".slideImage")
const detailImg = document.querySelector(".detail-img")

const previousBtn = document.querySelector(".previous")
const nextBtn = document.querySelector(".next")

thumbnailClick = (event) => {
  slideImage.forEach(img => {
    img.classList.remove('active')
  })
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


// Product Count +-

const plusCount = document.querySelector(".plusCount");
const minusCount = document.querySelector(".minusCount");
const counter = document.querySelector(".counter");

let count = 1;
counter.value = count;

plusCount.addEventListener("click", () => {
  if (count < 10) {
    count++;
  }
  counter.value = count;

  if(count === 10){
    plusCount.style.cursor = "no-drop";
  }
})

minusCount.addEventListener("click", () => {
  if (count > 1) {
    count--;
  }
  counter.value = count;

  if(count === 1){
    minusCount.style.cursor = "no-drop";
  }
})