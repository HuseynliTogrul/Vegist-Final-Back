const shopBasket = document.querySelectorAll(".shopBasket")
const productModal = document.querySelector(".productModal")

const overlay = document.querySelector(".overlay");
const closeModalBtn = document.querySelector("#close-search i");


const openModal = function () {
    productModal.classList.remove("hidden");
    overlay.classList.remove("hidden");
};

shopBasket.forEach((basket) => {
    basket.addEventListener("click", openModal);
})

const closeModal = function () {
    productModal.classList.add("hidden");
    overlay.classList.add("hidden");
};

closeModalBtn.addEventListener("click", closeModal);

overlay.addEventListener("click", closeModal);