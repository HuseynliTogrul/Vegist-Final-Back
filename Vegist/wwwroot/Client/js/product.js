let img = document.querySelector(".mainImg img");

const sizeDetailLi = document.querySelectorAll(".sizeDetailLi")


img.onmousemove = function (e) {
  e.target.style.setProperty('--x', (100 * e.offsetX / e.target.offsetWidth) + '%');
  e.target.style.setProperty('--y', (100 * e.offsetY / e.target.offsetHeight) + '%');
}

// sizeDetailLi.forEach(size => {
//   size.addEventListener("click", () => {
//     if (size.style.border === "1px solid #91b656") {
//       size.style.border = "1px solid white";
//     } else {
//       size.style.border = "1px solid #91b656"
//     }
//   });
// });

sizeDetailLi.forEach(size => {
  size.addEventListener('click', () => {
    size.classList.add("active");
  })
})